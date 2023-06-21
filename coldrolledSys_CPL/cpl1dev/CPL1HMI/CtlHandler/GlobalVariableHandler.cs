using System;
using System.Configuration;
using System.Data;
using System.Net;

namespace CPL1HMI
{
    /// <summary>
    /// 自動入料狀態
    /// </summary>
    public enum AutoFeedStatus
    {
        /// <summary>
        /// 手動
        /// </summary>
        Manual,
        /// <summary>
        /// 自動
        /// </summary>
        Auto
    }

    /// <summary>
    /// 排程調整
    /// </summary>
    public enum Seq_No_Change
    {
        Top,
        Up,
        Down,
        Bottom
    }
   
    public class GlobalVariableHandler
    {
        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly GlobalVariableHandler INSTANCE = new GlobalVariableHandler();

        }
        public DataTable dtEventPush = new DataTable();

        public DataRow dr_EventPush = null;

        public static GlobalVariableHandler Instance { get { return SingletonHolder.INSTANCE; } }

        public string strConn_CPL = ConfigurationManager.ConnectionStrings["FUXIN_CPL"].ConnectionString;       
        //public string strConn_CPL_HISTORY = ConfigurationManager.ConnectionStrings["FUXIN_CPL_HISTORY"].ConnectionString;
        public string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        public string getTime = "";
        public string time_CHAR14 = DateTime.Now.ToString("yyyyMMddHHmmss");
        public static string proLine = ConfigurationManager.AppSettings["ProLineName"];

        public static string Printer_IP = ConfigurationManager.AppSettings["Printer_IP"];

        public static int Printer_Port = 0;
        bool bolPrinter_Port = int.TryParse(ConfigurationManager.AppSettings["Printer_Port"], out Printer_Port);
        //public static int Printer_Port = int.Parse(ConfigurationManager.AppSettings["Printer_Port"]);

        public IPAddress getIpAdderss = new IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);

        // Load Excel Setting
        public static int LoadScheduleExcelStarRow = 0;
        bool bolLoadScheduleExcelStarRow = int.TryParse(ConfigurationManager.AppSettings["LoadScheduleExcelStarRow"], out LoadScheduleExcelStarRow);
        //public int LoadScheduleExcelStarRow = int.Parse(ConfigurationManager.AppSettings["LoadScheduleExcelStarRow"]);

        public static int LoadScheduleExcelStarCloumn = 0;
        bool bolLoadScheduleExcelStarCloumn = int.TryParse(ConfigurationManager.AppSettings["LoadScheduleExcelStarCloumn"], out LoadScheduleExcelStarCloumn);
        //public int LoadScheduleExcelStarCloumn = int.Parse(ConfigurationManager.AppSettings["LoadScheduleExcelStarCloumn"]);
    }

}
