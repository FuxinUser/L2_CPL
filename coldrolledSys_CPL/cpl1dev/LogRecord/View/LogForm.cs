using Core.Define;
using DBService.Repository.EventLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinformsMVP.Controls.Forms;

namespace LogRecord.View
{
    public partial class LogForm : BaseForm, LogContract.IView
    {
        private bool isAutoClearConsole = true;
        public LogContract.IPresenter Presenter { get; set; }
        public void SetPresenter(LogContract.IPresenter presenter)
        {
            ColsoleOut._CoilPresenter = presenter;
            Presenter = presenter;
            base.SetPresenter(presenter);
        }
        public LogForm()
        {
            InitializeComponent();
        }

        private void LogForm_Shown(object sender, EventArgs e)
        {
        }

        public void DisplayLog(EventLogEntity.TBL_EventLog item)
        {
  
            if (isAutoClearConsole && rtb.Lines.Length > 10)
                rtb.Clear();

            RtbAppend(rtb, $"{item.CreateTime.ToString("MM/dd HH:mm:ss.fff")}", Color.White);
            RtbAppend(rtb, $"   {(item.System_ID == "1" ? "Server" : "HMI")}", Color.Gray);
            eventMapping.TryGetValue(item.Event_Type, out string ans);
            if (item.Event_Type == "1")
            {
                RtbAppend(rtb, $"   {ans}", Color.DarkRed);
            }
            else if (item.Event_Type == "4")
            {
                RtbAppend(rtb, $"   {ans}", Color.Gray);
            }
            else
            {
                RtbAppend(rtb, $"   {ans}", Color.Blue);
            }

            RtbAppend(rtb, $"{item.Function_Block}\t", Color.DarkOrange);
            RtbAppend(rtb, $"{item.Event_Description}\t", Color.LightCyan);
            RtbAppend(rtb, $"{item.Command}\t\r\n", Color.LightGreen);
        }

        public void DisplayHMILog(EventLogEntity.TBL_EventLog item)
        {
            RtbAppend(rtbHMI, $"{item.CreateTime.ToString("MM/dd HH:mm:ss.fff")}", Color.White);
            RtbAppend(rtbHMI, $"   {(item.System_ID == "1" ? "Server" : "HMI")}", Color.Gray);
            eventMapping.TryGetValue(item.Event_Type, out string ans);
            if (item.Event_Type == "1")
            {
                RtbAppend(rtbHMI, $"   {ans}", Color.DarkRed);
            }
            else if (item.Event_Type == "4")
            {
                RtbAppend(rtbHMI, $"   {ans}", Color.Gray);
            }
            else
            {
                RtbAppend(rtbHMI, $"   {ans}", Color.Blue);
            }

            RtbAppend(rtbHMI, $"   Block: {item.Function_Block}", Color.DarkOrange);
            RtbAppend(rtbHMI, $"   Command: {item.Command}", Color.DarkGreen);
            RtbAppend(rtbHMI, $"   Descript: {item.Event_Description} \r\n", Color.DarkCyan);
        }

        private void RtbAppend(RichTextBox box, string str, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(str);
            box.SelectionColor = box.ForeColor;

        }

        private Dictionary<string, string> eventMapping = new Dictionary<string, string>()
        {
            {"1","Error" },
            {"2","Alarm" },
            {"3","Info" },
            {"4","Debug" }
        };

        private void btnClearConsole_Click(object sender, EventArgs e)
        {
            rtb.Clear();
            rtbHMI.Clear();
        }


        private void checkBoxDisDebugInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDisDebugInfo.Checked)
                Presenter.MgrActorTell(EventDef.CMDSET.DEBUG_LOG_OPEN);
            else
                Presenter.MgrActorTell(EventDef.CMDSET.DEBUG_LOG_CLOSE);           
        }

        private void checkBoxIsClearConsole_CheckedChanged(object sender, EventArgs e)
        {
            isAutoClearConsole = checkBoxIsClearConsole.Checked;
        }

    }
}
