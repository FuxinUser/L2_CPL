using Akka.Actor;
using AkkaSysBase;
using AkkaSysBase.Base;
using Controller;
using Controller.Coil;
using MsgConvert;
using MSMQ;
using MSMQ.Core.MSMQ;
using System.Collections.Concurrent;
using System.Collections.Generic;
using static MsgStruct.L2L1Snd;
using System.Timers;
using Core.Util;
using static Core.Define.DBParaDef;
using LogSender;
using System.Linq;
using Controller.Sys;

namespace DataSetup.Actor
{
    public class DtStpMgr : BaseActor
    {

        private IActorRef _selfActor;
        private ICoilController _coilController;
        private ISysController _sysController;

        // Thread 機制 -Preset  Log 要Save取消狀況
        private int MAX_COUNTER = 40;
        private ConcurrentQueue<string> _coilScedules;
        private Timer _tmrSend;
        private int _queueIndex;


        public DtStpMgr(ISysAkkaManager akkaManager, ICoilController coilController, ISysController sysController, ILog log) : base(log)
        {
            _selfActor = akkaManager.GetActor(GetType().Name);
            _coilController = coilController;
            _sysController = sysController;

            _coilController.SetLog(log);
            _sysController.SetLog(log);

            //_sysController.SaveAPStatusToL25("SystemAP_5", "1");

            _coilScedules = new ConcurrentQueue<string>();

            if (_tmrSend == null) _tmrSend = new Timer(3000);
            _queueIndex = 0;
            _tmrSend.Elapsed += TmrSendElapsed;             //Register

            MQPool.GetMQ("DtStpMgr").Receive(x =>
            {
                _selfActor.Tell(x);
            });
            Receive<MQPool.MQMessage>(message => ParsingMQID(message));
        }

        /// <summary>
        /// 解析MQ ID
        /// </summary>     
        private void ParsingMQID(MQPool.MQMessage msg)
        {
            //排程Preset
            if (msg.ID == InfoDataSetup.ScheduleIDsTo201.Event)
            {
            
                ClearStatus();

                var entryCoilIDs = msg.Data as List<string>;
                _log.I("組合Preset201報文", $"組合{entryCoilIDs.Count}筆Preset資料");

                foreach (string coilID in entryCoilIDs)
                {
                    _coilScedules.Enqueue(coilID);
                }

                _tmrSend.Start();

                return;
            }

            // 指定Preset
            if (msg.ID == InfoDataSetup.SpecificIDTo201.Event)
            {
                 var genPreset = msg.Data as SpecificPreset;

                SndPresetPro(genPreset.SpecificCoilID, genPreset.SpecificPos);
                return;
            }
        }
       

        private void SndPresetPro(string coilID, int pos)
        {
            TryFlow(() =>
            {
                var pdi = _coilController.GetPDI(coilID, PDISchema.EntryCoilID);
                var preset = L1MsgFactory.PresetEmpty201Msg(coilID);
                if (pdi != null)
                {
                    //var lkTable = _coilController.GetPreset201LkTableData(pdi.St_No, pdi.Entry_Coil_Thick, pdi.Ts_Stand_Max + pdi.Ts_Stand_Min);
                    var lkTable = _coilController.GetPreset201LkTableData(pdi.St_No, pdi.Entry_Coil_Thick);
                    var defect = _coilController.GetDefect(pdi.Plan_No, coilID);

                    //  計算 SideTrimmerWidth
                    var width = pdi.Out_Coil_Width - pdi.Entry_Coil_Width;
                    lkTable.SideTrimmerWidth = width > 0 ? width : 0f;
                    
                    preset = L1MsgFactory.Preset201Msg(pdi, lkTable, defect, pos);
                }
                _log.I("組合Preset報文", $"{preset.CoilIDNo}Preset報文組合完成");
                MQPoolService.SendToL1(InfoL1.SndPresetMsg.Data(preset));

                // 存取Preset Record
                _coilController.CreatePresetRecord(preset);

                // 存取2.5
                _coilController.CreateL25PresetRecord(preset);
            });
        }

        // 取出Queu資料組Preset
        private void TmrSendElapsed(object sender, ElapsedEventArgs e)
        {
            _queueIndex += 1;

            if (_queueIndex > MAX_COUNTER)
            {
                ClearStatus();
                return;
            }

            string coilID;
            _coilScedules.TryDequeue(out coilID);

            SndPresetPro(coilID, _queueIndex + 10);

            if (coilID == null)
                _tmrSend.Stop();

            //System.Threading.Thread.Sleep(5000);

            //if (coilID != null)
            //{
            //    _tmrSend.Stop();
            //    SndPresetPro(coilID, _queueIndex + 10);
            //    _tmrSend.Start();
            //}
        }

        private void ClearStatus()
        {
            _tmrSend.Close();
            _coilScedules.Clear();
            _queueIndex = 0;
        }

      
    }
}
