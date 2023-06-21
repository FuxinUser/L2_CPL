using System;
using System.Windows.Forms;

namespace CPL1HMI
{
    public enum System_ID
    {
        System =1,
        Client =2
    }
    public enum Event_Type
    {
        Error = 1,
        Alarm = 2,
        Info = 3,
        Debug = 4
    }
    public class EventLogHandler
    {
        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly EventLogHandler INSTANCE = new EventLogHandler();
        }
        public static EventLogHandler Instance { get { return SingletonHolder.INSTANCE; } }

        public void LogDebug(string FrameNO, string Event_Description, string Command)
        {
            Log(FrameNO, Event_Type.Debug, Event_Description, Command);
        }

        public void LogInfo(string FrameNO, string Event_Description, string Command)
        {
            Log( FrameNO, Event_Type.Info, Event_Description, Command);
        }

        /// <summary>
        /// LOG字串
        /// </summary>
        /// <param name="System_ID"></param>
        /// <param name="FrameGroup_NO"></param>
        /// <param name="Frame_NO"></param>
        /// <param name="Event_Type"></param>
        public void Log(string FrameNO, Event_Type EventType,string Event_Description, string Command)
        {
            string strSql = Frm_5_1_SqlFactory.SQL_Insert_EventLog_Client(FrameNO, EventType, Event_Description, Command);

            //DataAccess.GetInstance().ExecuteNonQuery(strSql, GlobalVariableHandler.Instance.strConn_CPL);

            if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, Event_Description))
            {
                DialogHandler.Instance.Fun_DialogShowOk(Command, Event_Description, 3);
                
                return;
            }

        }
       
        public void EventPush_Message(string msg)
        {
            msg = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {msg}";

            if (PublicForms.Main.Cob_EvnMsg.Items.Count >= 15)
            { 
                PublicForms.Main.Cob_EvnMsg.Items.Remove(0);
            }

            PublicForms.Main.Cob_EvnMsg.Items.Add(msg);
            PublicForms.Main.Cob_EvnMsg.SelectedIndex = PublicForms.Main.Cob_EvnMsg.Items.Count - 1;

            PublicForms.Main.Cob_EvnMsg.DropDownStyle = ComboBoxStyle.DropDownList;
            PublicForms.Main.Cob_EvnMsg.AutoCompleteMode = AutoCompleteMode.None;// SuggestAppend;
        }
        
    }
}
