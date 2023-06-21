using Akka.Actor;
using AkkaSysBase;
using BCScnMgr.Actor;
using Controller;
using Core.Help;
using MSMQ.Core.MSMQ;
using System;
using System.Configuration;
using System.Windows.Forms;
using WinformsMVP.Controls.Forms;
using WinformsMVP.Controls.Forms.Base;
using static Core.Define.L2SystemDef;
using static DataMod.BarCode.BCSDataModel;

namespace BCScnMgr.View
{
    public partial class BCSScnForm : BaseForm, BCScnContract.IView
    {
        public BCScnContract.IPresenter Presenter { get; set; }
        private IActorRef _rcvEdit;
        private ISysAkkaManager _akkaManager;
        public BCSScnForm(ISysAkkaManager akkaManager)
        {
            _akkaManager = akkaManager;
            InitializeComponent();
        }

        private void BCSScnForm_Load(object sender, EventArgs e)
        {
            EntryComoBox.SelectedIndex = 0;
            DeliveryComboBox.SelectedIndex = 0;

            
        }

        private void BCSScnForm_Shown(object sender, EventArgs e)
        {
            //網路環境設定(顯示):
            tbLocalIp.Text = IniSystemHelper.Instance.BarCodeLocalIP;
            tbLocalPort.Text = IniSystemHelper.Instance.BarCodeLocalPort.ToString();
            //tbRemoteIp.Text = ConfigurationManager.AppSettings["RemoteIP"];
            //tbRemotePort.Text = ConfigurationManager.AppSettings["RemotePort"];

            _rcvEdit = _akkaManager.GetActor(nameof(BCScnRcvEdit));
        }

        private void btnEntryScan_Click(object sender, EventArgs e)
        {
            if (txtEntryCoilNo.Text.Equals(""))
            {
                MessageBox.Show("請輸入鋼捲編號");
                return;
            }

            var bcs = new BarCodeScnContent
            {
                ScanCoilNo = txtEntryCoilNo.Text,
                ScanPosition = DetPos(EntryComoBox.Text),
            };

            _rcvEdit.Tell(bcs);
            //MQPoolService.SendToBCScnRcvEdit(InfoBCScn.ScanEntryCoilNo.Data(bcs));
        }

        private void btnDeliveryScn_Click(object sender, EventArgs e)
        {
            if (txtDeliveryCoilNo.Text.Equals(""))
            {
                MessageBox.Show("請輸入鋼捲編號");
                return;
            }

            var bcs = new BarCodeScnContent
            {
                ScanCoilNo = txtDeliveryCoilNo.Text,
                ScanPosition = DetPos(DeliveryComboBox.Text),
            };

            _rcvEdit.Tell(bcs);
            //MQPoolService.SendToBCScnRcvEdit(InfoBCScn.ScanDeliveryCoilNo.Data(bcs));
        }

        private SKPOS DetPos(string pos)
        {
            SKPOS POS = SKPOS.EntryTOP;

            switch (pos)
            {
                case "ESK1":
                    POS = SKPOS.Entry_SK01;
                    break;
                case "ESK2":
                    POS = SKPOS.Entry_SK02;
                    break;
                case "ETOP":
                    POS = SKPOS.EntryTOP;
                    break;
                case "DSK1":
                    POS = SKPOS.Delivery_SK01;
                    break;
                case "DSK2":
                    POS = SKPOS.Delivery_SK02;
                    break;
                case "DTOP":
                    POS = SKPOS.DeliveryTop;
                    break;
            }

            return POS;
        }

        [Obsolete]
        public void SetPresenter(BaseFormContract.IPresenter presenter)
        {
            throw new NotImplementedException();
        }
    }
}
