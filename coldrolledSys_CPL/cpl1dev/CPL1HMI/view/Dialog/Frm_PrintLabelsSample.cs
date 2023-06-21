using Akka.Actor;
using Common.StTool;
using LabelPrint.Printer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LabelPrint.Model.ZebraModel;

namespace CPL1HMI
{
    public partial class Frm_PrintLabelsSample : Form
    {
        public string Str_Coil_No;//钢卷号
        public string Str_Steel_Grade_Sign;//钢种(牌号)
        public string Str_Coil_Thick;//钢卷厚度
        public string Str_Sample_Lot_No;//试批号
        public string Str_Sample_Position;//取样位置
        public string Str_Unit_code;//产线 CAPL
        public Frm_PrintLabelsSample()
        {
            InitializeComponent();
        }

        private void Frm_PrintLabelsSample_Load(object sender, EventArgs e)
        {
            //取樣位置 
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.SAMPLE_FRQN_CODE, Cob_Sample_Position);

            Txt_Entry_Coil_No.Text =    Str_Coil_No;//钢卷号
            Txt_Steel_Grade_Sign.Text = Str_Steel_Grade_Sign;//钢种(牌号)
            Txt_Coil_Thick.Text = Str_Coil_Thick;//钢卷厚度
            Txt_Sample_Lot_No.Text = Str_Sample_Lot_No;//试批号

            //Cob_Sample_Position.Text = Str_Sample_Position;//取样位置
            Cob_Sample_Position.SelectedValue = Str_Sample_Position;

        }
        private string GetSampleCode(string sampleCode)
        {
            switch (sampleCode)
            {
                case "100": return "H";     //  頭端
                case "010": return "M";     //  中端
                case "001": return "T";     //  尾端
                default: return "";         //  找不到
            }
        }
        private void Btn_OK_Click(object sender, EventArgs e)
        {
            var coilID = Txt_Entry_Coil_No.Text;
            var stNo = Txt_Steel_Grade_Sign.Text;
            var thick = Txt_Coil_Thick.Text ;
            var sampleNo = Txt_Sample_Lot_No.Text;
            var sampleCode = GetSampleCode( Cob_Sample_Position.Text.Split(']')[0].Replace("[", "")); 

            ZebraCommand msg = new ZebraCommand();
            msg.ZPL = coilID.zplQRCmd(stNo,thick, sampleNo, sampleCode);

            PublicComm.lprSndEdit.Tell(msg);

            EventLogHandler.Instance.LogInfo("1-2", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()} 打印标签作业", $"打印{Txt_Entry_Coil_No.Text.Trim()}标签");
            EventLogHandler.Instance.EventPush_Message($"列印[{Txt_Entry_Coil_No.Text.Trim()}]标签");
            PublicComm.ClientLog.Info($"通知Server列印鋼卷號:[{Txt_Entry_Coil_No.Text.Trim()}]標籤");


            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

       
    }
}
