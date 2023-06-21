using System;
using System.Configuration;
using WinformsMVP.Controls.Forms;
using Core.Define;
using WinformsMVP.Controls.Forms.Base;
using System.Windows.Forms;
using DBService;
using static DBService.L1Repository.L2L1MsgDBModel;
using DBService.L1Repository;
using System.Linq;
using AkkaSysBase;
using PLCComm.Actor;
using Core.Util;
using DataModel.Common;
using Akka.Actor;
using MsgConvert.EntityFactory;
using Core.Help;
using MsgStruct;
using Controller;
using MSMQ.Core.MSMQ;
/**
* Author: ICSC 余士鵬
* Date: 2019/09/19
* Description: PLC UI
* Reference: 
* Modified: 
*/
namespace PLCComm.View
{
    public partial class PlcCommForm : BaseForm, PlcCommContract.IView
    {
        private ISysAkkaManager _akkaManager;
        public PlcCommContract.IPresenter Presenter { get; set; }
        public void SetPresenter(PlcCommContract.IPresenter presenter)
        {
            InitializeComponent();
            Presenter = presenter;
            Presenter.SetView(this);
        }

        public PlcCommForm(ISysAkkaManager akkaManager)
        {
            _akkaManager = akkaManager;
            InitializeComponent();
        }


        private void PlcCommForm_Shown(object sender, EventArgs e)
        {
            InitUI();
        }

        private void InitUI()
        {
            //tbLocalIp.Text = ConfigurationManager.AppSettings["LocalIP"];
            //tbLocalPort.Text = ConfigurationManager.AppSettings["LocalPort"];
            //tbRemoteIp.Text = ConfigurationManager.AppSettings["RemoteIP"];
            //tbRemotePort.Text = ConfigurationManager.AppSettings["RemotePort"];

            tbLocalIp.Text = IniSystemHelper.Instance.PLCLocalIP;
            tbLocalPort.Text = IniSystemHelper.Instance.PLCLocalPort.ToString();
            tbRemoteIp.Text = IniSystemHelper.Instance.PLCRemoteIP;
            tbRemotePort.Text = IniSystemHelper.Instance.PLCRemotePort.ToString();

        }

        


        private void SndTell(object msg, string msgID)
        {
            var sndActor = _akkaManager.GetActor(nameof(PlcCom));
            var bytes = msg.RawSerialize(true);
            var sndMsg = new CommonMsg(bytes.Length.ToString(), msgID, bytes);
            sndActor.Tell(sndMsg);
        }

        [Obsolete]
        public void SetPresenter(BaseFormContract.IPresenter presenter)
        {
            throw new NotImplementedException();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            var tableName = BoxMsgID.Text;

            if (BoxMsgID.Text.Equals(string.Empty))
            {
                MessageBox.Show("請選擇報文");
                return;
            }


            var sqlStr = $"SELECT * FROM {tableName}";


            try
            {
                var msgSet = DataAccess.GetInstance().Select(sqlStr, DBParaDef.HisDBConn);

                Dgv_History.ColumnHeadersVisible = false;
                Dgv_History.RowHeadersVisible = false;
                if (Dgv_History.Columns.Count > 0)
                {
                    Dgv_History.Columns.Clear();
                }
                Dgv_History.DataSource = msgSet;

                //if (BoxMsgID.Text.Equals("L2L1_204") || BoxMsgID.Text.Equals("L2L1_205"))
                //{
                for (int i = 10; i < Dgv_History.Columns.Count; i++)
                    Dgv_History.Columns[i].Visible = false;
                //}

                Dgv_History.ColumnHeadersVisible = true;

            }
            catch (Exception expection)
            {
                MessageBox.Show(expection.Message);
            }
        }

        private void btnHisSnd_Click(object sender, EventArgs e)
        {
            if (BoxMsgID.Text.Equals(string.Empty))
            {
                MessageBox.Show("請選擇報文");
                return;
            }
            if (Dgv_History == null || Dgv_History.Rows.Count == 0)
            {
                MessageBox.Show("無資料可選");
                return;
            }
            TryFlow(() => SndHisData(BoxMsgID.Text));
        }

        private void SndHisData(string tableName)
        {

            var createTime = (DateTime)Dgv_History.SelectedRows[0].Cells[5].Value;

            switch (tableName)
            {
                case nameof(L2L1_201):
                    var repo201 = new L1201HisMsgRepo(DBParaDef.HisDBConn);
                    var data201 = repo201.GetAll().Where(x => x.CreateTime.CompareTo(createTime) == 0).FirstOrDefault();
                    var L1201 = data201.ConvertL1MsgModel("201");
                    SndTell(L1201, "201");
                    break;
                case nameof(L2L1_202):
                    var repo202 = new L1202HisMsgRepo(DBParaDef.HisDBConn);
                    var data202 = repo202.GetAll().Where(x => x.CreateTime.CompareTo(createTime) == 0).FirstOrDefault();
                    var L1202 = data202.ConvertL1MsgModel("202");
                    SndTell(L1202, "202");
                    break;
                case nameof(L2L1_203):
                    var repo203 = new L1203HisMsgRepo(DBParaDef.HisDBConn);
                    var data203 = repo203.GetAll().Where(x => x.CreateTime.CompareTo(createTime) == 0).FirstOrDefault();
                    var L1203 = data203.ConvertL1MsgModel("203");
                    SndTell(L1203, "203");
                    break;
                case nameof(L2L1_204):
                    var repo204 = new L1204HisMsgRepo(DBParaDef.HisDBConn);
                    var data204 = repo204.GetAll().Where(x => x.CreateTime.CompareTo(createTime) == 0).FirstOrDefault();
                    var L1204 = data204.ConvertL1MsgModel("204");
                    SndTell(L1204, "204");
                    break;
                case nameof(L2L1_205):
                    var repo205 = new L1204HisMsgRepo(DBParaDef.HisDBConn);
                    var data205 = repo205.GetAll().Where(x => x.CreateTime.CompareTo(createTime) == 0).FirstOrDefault();
                    var L1205 = data205.ConvertL1MsgModel("205");
                    SndTell(L1205, "205");
                    break;

            }
        }


        protected virtual void TryFlow(Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selText = BoxMsgID.Text;
            if (selText == string.Empty)
                return;

            var msgId = selText.Substring(selText.Length - 3, 3);

            object msg = null;

            switch (msgId)
            {
                case "201":
                    msg = Get201();
                    break;
                case "202":
                    msg = Get202();
                    break;
                case "203":
                    msg = Get203();
                    break;
                case "204":
                    break;
            }

            if (msg == null)
                return;

            MQPoolService.SendToL1(InfoL1.SndPresetMsg.Data(msg));
        }

        private L2L1Snd.Msg_201_Preset Get201()
        {
            var msg201 = new L2L1Snd.Msg_201_Preset();

            msg201.MessageLength = 464;
            msg201.MessageId = 201;
            msg201.Date = 20210525;
            msg201.Time = 10303033;
            msg201.Coil_ID = "HE20123".ToCByteArray(20).ToL1EndSignByteArray(4);
            msg201.SteelGrade = "5678".ToCByteArray(20).ToL1EndSignByteArray(4);
            msg201.Thickness = 1f;
            msg201.Width = 2f;
            msg201.EntryYieldStress = 3f;
            msg201.Density = 4f;
            msg201.CoilLength = 5f;
            msg201.CoilWeight = 6f;
            msg201.ProcessCode = "CP0".ToCByteArray(7).ToL1EndSignByteArray(1);
            msg201.InnerDiam = 1f;
            msg201.Diameter = 2f;
            msg201.SleeveCodeEntry = 3;
            msg201.SleeveDmEntry = 4f;
            msg201.PaperWinderFlag = 5;
            msg201.SleeveCodeExit = 6;
            msg201.SleeveDmExit = 7f;
            msg201.PaperTypeExit = 8;
            msg201.PaperCodeExit = 9;
            msg201.FlatenerDepth1 = 1f;
            msg201.FlatenerDepth2 = 2f;
            msg201.UncoilerTension = 3f;
            msg201.UncoilerTensionMax = 4f;
            msg201.UncoilerTensionMin = 5f;
            msg201.HeadLeaderStripLength = 6f;
            msg201.HeadLeaderStripThickness = 7f;
            msg201.HeadLeaderStripWidth = 8f;
            msg201.HeadLeaderStripSteelGrade = 9;
            msg201.TailLeaderStripLength = 1f;
            msg201.TailLeaderStripThickness = 2f;
            msg201.TailLeaderStripWidth = 3f;
            msg201.TailLeaderStripSteelGrade = 4;
            msg201.SideTrimmerGap = 5f;
            msg201.SideTrimmerLap = 6f;
            msg201.SideTrimmerWidth = 7f;
            msg201.TensionUnitDepth = 8f;
            msg201.RecoilerTension = 9f;
            msg201.RecoilerTensionMax = 1f;
            msg201.RecoilerTensionMin = 2f;
            msg201.PaperUnwinderFlag = 3;
            msg201.CoilSplit = 4;
            msg201.Orderwt_1 = 5f;
            msg201.Orderwt_2 = 6f;
            msg201.Orderwt_3 = 7f;
            msg201.Orderwt_4 = 8f;
            msg201.Orderwt_5 = 9f;
            msg201.Orderwt_6 = 1f;
            msg201.PrrPosId = 50;
            msg201.Defect1Code = "3".ToCByteArray(10).ToL1EndSignByteArray(2);
            msg201.Defect1StartPosition = 4f;
            msg201.Defect1EndPosition = 5f;
            msg201.Defect2Code = "6".ToCByteArray(10).ToL1EndSignByteArray(2);
            msg201.Defect2StartPosition = 7f;
            msg201.Defect2EndPosition = 8f;
            msg201.Defect3Code = "9".ToCByteArray(10).ToL1EndSignByteArray(2);
            msg201.Defect3StartPosition = 1f;
            msg201.Defect3EndPosition = 2f;
            msg201.Defect4Code = "3".ToCByteArray(10).ToL1EndSignByteArray(2);
            msg201.Defect4StartPosition = 4f;
            msg201.Defect4EndPosition = 5f;
            msg201.Defect5Code = "6".ToCByteArray(10).ToL1EndSignByteArray(2);
            msg201.Defect5StartPosition = 7f;
            msg201.Defect5EndPosition = 8f;
            msg201.Defect6Code = "9".ToCByteArray(10).ToL1EndSignByteArray(2);
            msg201.Defect6StartPosition = 1f;
            msg201.Defect6EndPosition = 2f;
            msg201.Defect7Code = "3".ToCByteArray(10).ToL1EndSignByteArray(2);
            msg201.Defect7StartPosition = 4f;
            msg201.Defect7EndPosition = 5f;
            msg201.Defect8Code = "6".ToCByteArray(10).ToL1EndSignByteArray(2);
            msg201.Defect8StartPosition = 7f;
            msg201.Defect8EndPosition = 8f;
            msg201.Defect9Code = "9".ToCByteArray(10).ToL1EndSignByteArray(2);
            msg201.Defect9StartPosition = 1f;
            msg201.Defect9EndPosition = 2f;
            msg201.Defect10Code = "3".ToCByteArray(10).ToL1EndSignByteArray(2);
            msg201.Defect10StartPosition = 4f;
            msg201.Defect10EndPosition = 5f;
            msg201.Spare1 = 3;
            msg201.Spare2 = 0;
            msg201.Spare3 = 0;
            msg201.Spare4 = 0;
            msg201.Spare5 = 0;

            return msg201;
        }

        private L2L1Snd.Msg_202_TrackMapL2 Get202()
        {
            var msg202 = new L2L1Snd.Msg_202_TrackMapL2();
            msg202.MessageLength = 228;
            msg202.MessageId = 202;
            msg202.Date = 20210525;
            msg202.Time = 10303033;
            msg202.CoilIDUnc = "1".ToCByteArray(20).ToL1EndSignByteArray(4);
            msg202.CoilIDUncSk1 = "2".ToCByteArray(20).ToL1EndSignByteArray(4);
            msg202.CoilIDUncSk2 = "3".ToCByteArray(20).ToL1EndSignByteArray(4);
            msg202.CoilIDUncTop = "4".ToCByteArray(20).ToL1EndSignByteArray(4);
            msg202.CoilIDRec = "5".ToCByteArray(20).ToL1EndSignByteArray(4);
            msg202.CoilIDRecSk1 = "6".ToCByteArray(20).ToL1EndSignByteArray(4);
            msg202.CoilIDRecSk2 = "7".ToCByteArray(20).ToL1EndSignByteArray(4);
            msg202.CoilIDRecTop = "8".ToCByteArray(20).ToL1EndSignByteArray(4);
            msg202.PrrPosId = 1;
            msg202.Spare1 = 0;
            msg202.Spare2 = 0;
            msg202.Spare3 = 0;
            msg202.Spare4 = 0;
            msg202.Spare5 = 0;

            return msg202;
        }

        private L2L1Snd.Msg_203_SplitId Get203()
        {
            var msg203 = new L2L1Snd.Msg_203_SplitId();
            msg203.MessageLength = 52;
            msg203.MessageId = 203;
            msg203.Date = 20210525;
            msg203.Time = 10303033;
            msg203.CoilID = "1234".ToCByteArray(20).ToL1EndSignByteArray(4);
            msg203.Spare1 = 0;
            msg203.Spare2 = 0;
            msg203.Spare3 = 0;
            msg203.Spare4 = 0;
            msg203.Spare5 = 0;

            return msg203;
        }
    }
}
