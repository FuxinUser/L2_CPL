using Akka.Actor;
using AkkaSysBase;
using AkkaSysBase.Base;
using Controller;
using Controller.Sys;
using Core.Define;
using Core.Util;
using LogSender;
using MSMQ;
using MSMQ.Core.MSMQ;
using System.Collections.Concurrent;
using static DataModel.HMIServerCom.Msg.SCCommMsg;
using static MsgStruct.L2L1Snd;

/**
 * Author: ICSC余士鵬
 * Date: 2019/10/8
 * Description: PC Com
 * Reference: 
 * Modified: 
 */
namespace PcComm
{
    public class PCcom : BaseActor {

        private IActorRef _selfActor;
        private ConcurrentDictionary<string, IActorRef> _clients;

        private ISysController _sysController;


        public PCcom(ISysAkkaManager akkaManager, ISysController sysController, ILog log) : base(log)
        {
            _selfActor = akkaManager.GetActor(GetType().Name);
            _clients = new ConcurrentDictionary<string, IActorRef>();

            _sysController = sysController;
            _sysController.SetLog(log);

            //_sysController.SaveAPStatusToL25("SystemAP_13", "1");

            MQPool.ReceiveFromPCCom(x => _selfActor.Tell(x));

            Receive<MQPool.MQMessage>(x => Handle_MQ_Message(x));
           
            Receive<ClientAliveMsg>(ClientAliveMsg);
            
            // 重新要求排程,通知Server發送電文給MMS要求下發最新排程
            Receive<CS01_AckSchedule>(CS01_AckSchedulePro);
            // 通知Server發送電文給MMS要求指定鋼捲PDI資料
            Receive<CS02_AckPDI>(CS02_AckPDIPro);
            // 通知Server更新排程給MMS及WMS
            Receive<CS03_ScheduleChange>(CS03_ScheduleChangePro);
            // 入口段鋼捲ID更正
            Receive<CS04_RenameCoil>(CS04_RenameCoilPro);
            // CLIENT紀錄退料實績相關資料到資料庫, 通知SERVER發送退料實績（MMS）及退料要求（WMS）
            Receive<CS05_RejectCoil>(CS05_RejectCoilPro);
            // 操作確認上傳鋼捲PDO
            Receive<CS06_SendMMSPDO>(CS06_SendMMSPDOPro);
            // 操作要求手動列印標籤, CLIENT通知SERVER列印標籤
            Receive<CS07_PrintLabel>(CS07_PrintLabelPro);
            // 操作手動輸入秤重資料, CLIENT通知SERVER更新鋼捲秤重資料（毛重）
            Receive<CS08_WeightInput>(CS08_WeightInputPro);
            // 系統狀態
            Receive<CS09_LineFaultData>(CS09_LineFaultDataPro);
            // 產線開始/停止供料確認
            Receive<CS10_Coil_AutoFeedModeChange>(CS10_Coil_AutoFeedModeChange);
            // 手動入料: 直接發入料要求
            Receive<CS11_Coil_ManualFeed>(CS11_Coil_ManualFeed);
            // 操作於指定鞍座上操作入料指示
            Receive<CS12_Coil_SkidFeed>(CS12_Coil_SkidFeed);
            // 手動刪除鋼捲Tracking
            Receive<CS13_DeleteSidCoil>(CS13_DeleteSidCoil);
            // 出口段出料
            Receive<CS14_DeliveryCoilOut>(CS14_DeliveryCoilOut);
            // 上傳能源消耗
            Receive<CS15_Utility>(CS15_Utility);

            // 操作端完成匯入排程，通知Server發送Preset40筆
            Receive<CS16_FinishLoadSchedule>(CS16_FinishLoadSchedule);
            // 操作端完成匯入PDI，通知Server發送Preset40筆
            Receive<CS17_FinishLoadPDI>(CS17_FinishLoadPDI);
            // 天車入料時選擇此ID
            Receive<CS18_CarneEntryCoilSelect>(CS18_CarneEntryCoilSelect);
            // 操作通知HMI修改
            Receive<CS20_InfoPDIModify>(CS20_InfoPDIModify);

            //HMI通知Server修改子捲號
            Receive<CS21_POR_StripBreakModify>(CS21_POR_StripBreakModify);

            //HMI通知Server發POR Preset
            Receive<CS22_POR_PresetL1>(CS22_POR_PresetL1);
        
        
            ReceiveAny(message => RcvObject(message));
        
        }

        private void BroadCast(object msg)
        {
            foreach(var client in _clients)
                client.Value.Tell(msg);
        }

        // 通知HMI
        private void Handle_MQ_Message(MQPool.MQMessage msg)
        {
                        // 入料完成
            if (msg.ID == InfoHMI.EntryCoilDone.Event)
            {
                _log.I("入料完成","發送通知HMI更新頁面");
                //TODO 通知HMI                
                //_hmiClient.Tell(msg.Data);
                BroadCast(msg.Data);
                return;
            }          
            // 入口段掃碼結果
            if( msg.ID == InfoHMI.BarcodeScanResult.Event)
            {

                _log.D("Server通知HMI", "掃碼結果");
                _log.D("Server通知HMI", $"{JsonUtil.ToJson(msg)}");

                _log.I("通知HMI", "掃碼結果");
                _log.D("通知HMI", $"{JsonUtil.ToJson(msg)}");
                //TODO 通知HMI
                //_hmiClient.Tell(msg.Data);
                BroadCast(msg.Data);
                return;
            }
            // Event Push
            if( msg.ID == InfoHMI.EventPush.Event)
            {
                _log.I("通知HMI", "事件通知");
                _log.D("通知HMI", $"{JsonUtil.ToJson(msg.Data)}");
                //_hmiClient.Tell(msg.Data);
                BroadCast(msg.Data);
                return;
            }
            // 排程資料庫已變更
            if(msg.ID == InfoHMI.ScheduleChangeNotice.Event)
            {
                _log.I("通知HMI", "排程資料已更新");
                _log.D("通知HMI", $"{JsonUtil.ToJson(msg.Data)}");
                //_hmiClient.Tell(msg.Data);
                BroadCast(msg.Data);
                return;
            }
            // 通知HMI 通知天車入料給予天車入料ID
            if (msg.ID == InfoHMI.CraneEntryCoil.Event)
            {
                _log.I("通知HMI", $"通知天車入料給予天車入料ID");
                _log.D("通知HMI", $"{JsonUtil.ToJson(msg.Data)}");
                //_hmiClient.Tell(msg.Data);
                BroadCast(msg.Data);
                return;
            }
            if(msg.ID == InfoHMI.EventPushDialogShow.Event)
            {
                _log.I("通知HMI", "事件通知(Dialog顯示)");
                _log.D("通知HMI", $"{JsonUtil.ToJson(msg.Data)}");
                //_hmiClient.Tell(msg.Data);
                BroadCast(msg.Data);
                return;
            }
            if (msg.ID == InfoHMI.RefreshLineFault.Event)
            {
                _log.I("通知HMI", "刷新停復機畫面");
                _log.D("通知HMI", $"{JsonUtil.ToJson(msg.Data)}");
                //_hmiClient.Tell(msg.Data);
                BroadCast(msg.Data);
                return;
            }
            if (msg.ID == InfoHMI.PdoUploadedReply.Event)
            {
                _log.I("通知HMI", "顯示上傳PDO的回覆資訊");
                _log.D("通知HMI", $"{JsonUtil.ToJson(msg.Data)}");
                BroadCast(msg.Data);
                return;
            }
        }

        private void ClientAliveMsg(ClientAliveMsg msg)
        {
            var id = msg.Client_IP_Port;

            if (_clients.ContainsKey(id))
            {
                IActorRef refActor;

                if (_clients.TryRemove(id, out refActor))
                {
                    if (!_clients.TryAdd(id, Sender))
                    {
                        _log.E("加入HMI Group失敗", $"加入到HMI{msg.Client_IP_Port} Alive Msg");
                        return;
                    }

                    _log.D("加入HMI Group成功", $"加入到HMI{msg.Client_IP_Port} Alive Msg");
                    _clients[id].Tell(new ServerAckClientAliveMsg());
                }
                return;
            }

            if (!_clients.TryAdd(id, Sender))
            {
                _log.E("加入HMI Group失敗", $"加入到HMI{msg.Client_IP_Port} Alive Msg");
                return;
            }

            _log.D("加入HMI Group成功", $"加入到HMI{msg.Client_IP_Port} Alive Msg");
            _clients[id].Tell(new ServerAckClientAliveMsg());
        }

        private void CS01_AckSchedulePro(CS01_AckSchedule msg)
        {
            _log.I("通知三級下發鋼捲排程", $"通知MMS下發鋼捲{msg.CoilID}最新排程");
            MQPoolService.SendToCoil(InfoCoil.AskCoilSchedule.Data(msg));
        }
        private void CS02_AckPDIPro(CS02_AckPDI msg)
        {
            _log.I("通知三級下發PDI", $"通知MMS下發鋼捲{msg.Coil_ID}PDI");
            MQPoolService.SendToCoil(InfoCoil.AskPDI.Data(msg));
        }
        private void CS03_ScheduleChangePro(CS03_ScheduleChange msg)
        {
            _log.I("通知三級鋼捲變更", $"鋼捲號{msg.EntryCoilID}排程更新訊息給MMS及WMS");
            MQPoolService.SendToCoil(InfoCoil.UpdateCoilSchedule.Data(msg));
        }
        private void CS04_RenameCoilPro(CS04_RenameCoil msg)
        {
            _log.I("通知入口鋼捲更正", "入口段鋼捲ID更正");
            MQPoolService.SendToTrk(InfoTrk.ScnRenameCoil.Data(msg));
        }
        private void CS05_RejectCoilPro(CS05_RejectCoil msg)
        {
            _log.I("回退鋼捲通知", "CLIENT紀錄退料實績相關資料到資料庫");
            MQPoolService.SendToTrk(InfoTrk.ReturnCoil.Data(msg));
        }
        private void CS06_SendMMSPDOPro(CS06_SendMMSPDO msg)
        {
            _log.I("上傳PDO", "操作確認，上傳鋼捲PDO");
            MQPoolService.SendToCoil(InfoCoil.AskSndPDO.Data(msg));
            //MQPoolService.SendToDtProGtr(InfoDtProGtr.ProProcessData.Data(msg));
        }
        private void CS07_PrintLabelPro(CS07_PrintLabel msg)
        {
            _log.I("HMI通知", $"操作要求手動列印標籤, 鋼捲{msg.CoilID}列印標籤");
            MQPoolService.SendToLpr(InfoLpr.ManualPrint.Data(msg));
        }
        private void CS08_WeightInputPro(CS08_WeightInput msg)
        {
            _log.I("手動輸入秤重資料", "操作手動輸入秤重資料, CLIENT通知SERVER更新鋼捲秤重資料（毛重）");
            MQPoolService.SendToCoil(InfoCoil.UpdateOutMatPureWT.Data(msg));
        }
        private void CS09_LineFaultDataPro(CS09_LineFaultData msg)
        {
            _log.I("停復機通知", "系統狀態");
            MQPoolService.SendToDtGtr(InfoDtGtr.UploadLineFault.Data(msg));
            
        }
        private void CS10_Coil_AutoFeedModeChange(CS10_Coil_AutoFeedModeChange msg)
        {
            _log.I("自動入料模式變更", "收到自動入料模式變更，通知Server進行模式確認");
            _log.D("自動入料模式變更", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToTrk(InfoTrk.CheckCoilEnterInfo.Data(msg));
        }
        private void CS11_Coil_ManualFeed(CS11_Coil_ManualFeed msg)
        {
            _log.I("手動操作通知Server可以入料", "收到手動操作通知Server可以入料通知");
            _log.D("手動操作通知Server可以入料", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToTrk(InfoTrk.SndEntryCoilReqMsg.Data(msg));
        }
        private void CS12_Coil_SkidFeed(CS12_Coil_SkidFeed msg)
        {
            _log.I("鞍座上手動操作入料", "鞍座上手動操作入料通知");
            _log.D("鞍座上手動操作入料", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToTrk(InfoTrk.SndSkidFeedMsg.Data(msg));
        }

        private void CS13_DeleteSidCoil(CS13_DeleteSidCoil msg)
        {
            _log.I("鞍座上手動移除鞍作上鋼捲", "鞍座上手動移除鞍作上鋼捲通知");
            _log.D("鞍座上手動移除鞍作上鋼捲", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToTrk(InfoTrk.DelSkidCoil.Data(msg));
        }

        private void CS14_DeliveryCoilOut(CS14_DeliveryCoilOut msg)
        {
            _log.I("出口段出料", $"出口鋼捲{msg.CoilID}段出料通知 ");
            _log.D("出口段出料", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToTrk(InfoTrk.DeliveryCoilOut.Data(msg));
        }

        private void CS15_Utility(CS15_Utility msg)
        {
            _log.I("能源消耗上傳", $"能源消耗上傳 班次:{msg.ShiftName} 班別:{msg.GroupName} 消耗水:{msg.TotalCoolingWater} 消耗氣體:{msg.TotalCompressedAir}");
            _log.D("能源消耗上傳", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToMMS(InfoMMS.UploadEnergyConsumptionInfo.Data(msg));
        }

        private void CS16_FinishLoadSchedule(CS16_FinishLoadSchedule msg)
        {
            _log.I("完成匯入排程", "完成匯入排程通知");
            _log.D("完成匯入排程", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToCoil(InfoCoil.SndPresetInfo.Data(msg));
        }

        private void CS17_FinishLoadPDI(CS17_FinishLoadPDI msg)
        {
            _log.I("完成匯入PDI", "完成匯入PDI通知");
            _log.D("完成匯入PDI", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToCoil(InfoCoil.SndPresetInfo.Data(msg));
        }

        private void CS18_CarneEntryCoilSelect(CS18_CarneEntryCoilSelect msg)
        {
            _log.I("天車入料選擇", $"天車入料選擇鋼捲{msg.coilID}");
            _log.D("天車入料選擇", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToTrk(InfoTrk.CarneEntryCoilSelect.Data(msg));
        }

        private void CS20_InfoPDIModify(CS20_InfoPDIModify msg)
        {
            _log.I("操作通知修改PDI", $"操作通知修改PDI{msg.CoilID}");
            _log.D("操作通知修改PDI", $"{JsonUtil.ToJson(msg)}");
            MQPoolService.SendToCoil(InfoCoil.OpModifyPDI.Data(msg));
        }

        private void CS21_POR_StripBreakModify(CS21_POR_StripBreakModify msg)
        {
            _log.I("操作通知修改POR子捲號", $"操作通知修改POR子捲號{msg.Coil_ID}");
            _log.D("操作通知修改POR子捲號", $"{JsonUtil.ToJson(msg)}");

            MQPoolService.SendToCoil(InfoCoil.ModifyPORCoilID.Data(msg));
        }

        private void CS22_POR_PresetL1(CS22_POR_PresetL1 msg)
        {
            _log.I("操作通知發送POR Preset", $"操作通知發送POR Preset{msg.Coil_ID}");
            _log.D("操作通知發送POR Preset", $"{JsonUtil.ToJson(msg)}");

            // 通知L1 Preset
            var specificPreset = new SpecificPreset(msg.Coil_ID, PlcSysDef.Pos.Preset201POR);
            MQPoolService.SendToDtStp(InfoDataSetup.SpecificIDTo201.Data(specificPreset));
        }


        private void RcvObject(object msg)
        {
            _log.E("AThread接收資料-RcvObject", $"無法解析資料! Type:{msg.GetType()} From Sender:{Sender.Path}");
        }



    }
}
