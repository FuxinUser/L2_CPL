using System.IO;
using System.Text;

namespace Core.Define
{
    /// <summary>
    /// 系統檔案相關設置定義
    /// </summary>
    public class SystemFileDef
    {
        #region -- Ini --

        public static string iniPath = Path.Combine(IniPath(), "SystemConfig.ini");

        public static string IniPath()
        {
            var exePath = System.AppDomain.CurrentDomain.BaseDirectory;
            var pathlist = exePath.Split('\\');
            var pathStr = new StringBuilder();
            for (int i = 0; i < pathlist.Length - 4; i++)
                pathStr.Append(pathlist[i]+"\\");

            return pathStr.ToString();
        }

        public static string DBSection = "MsSql-Serve";
        public static string DBKey = "DbConn";
        public static string HISDBKey = "His-DbConn";
        public static string DB25Key = "Level2.5-DbConn";
        public static string DBTESTKey = "TEST-DbConn";

        public static string LineInfoSection = "LineInfo";
        public static string LineName = "LineName";
        public static string LineNo = "LineNo";

        public static string MMSSection = "MMS-App";
        public static string WMSSection = "WMS-App";
        public static string PLCSection = "PLC-App";
        public static string PrinterSection = "Printer-App";
        public static string BarCodeSection = "BarCode-App";

        public static string System_Name_Key = "System-Name";
        public static string Socket_Local_IP_Key = "Socket-Local-IP";       // Socket Loacal IP
        public static string Socket_Local_Port_Key = "Socket-Local-Port";     // Socket Loacal Port
        public static string Socket_Remote_IP_Key = "Socket-Remote-IP";       // Socket Loacal IP
        public static string Socket_Remote_Port_Key = "Socket-Remote-Port";     // Socket Loacal Port

        public static string Rcv_RawData_Key = "Rcv-RawData";
        public static string Snd_RawData_Key = "Snd-RawData";
        public static string Fail_RawData_Key = "Fail-RawData";
        public static string Debug_RawData_Key = "Debug-RawData";

        #endregion
    }
}
