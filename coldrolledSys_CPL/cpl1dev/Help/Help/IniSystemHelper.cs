using Core.Define;
using Core.Util;

namespace Core.Help
{
    public class IniSystemHelper
    {
        public IniUtil IniManager { get; set; }

        public static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly IniSystemHelper INSTANCE = new IniSystemHelper() {

                IniManager = new IniUtil(SystemFileDef.iniPath)
            };
        }
        public static IniSystemHelper Instance { get { return SingletonHolder.INSTANCE; } }

        public string DBConn {get=> IniManager.ReadIni(SystemFileDef.DBSection, SystemFileDef.DBKey); }

        public string HisDBConn { get => IniManager.ReadIni(SystemFileDef.DBSection, SystemFileDef.HISDBKey); }

        public string Level2_5_DBConn { get => IniManager.ReadIni(SystemFileDef.DBSection, SystemFileDef.DB25Key); }

        public string Test_DBConn { get => IniManager.ReadIni(SystemFileDef.DBSection, SystemFileDef.DBTESTKey); }

        // 機組號
        public string LineName { get => IniManager.ReadIni(SystemFileDef.LineInfoSection, SystemFileDef.LineName); }
        public string LineNo { get => IniManager.ReadIni(SystemFileDef.LineInfoSection, SystemFileDef.LineNo); }
        // MMS
        public string MMSApp { get => IniManager.ReadIni(SystemFileDef.MMSSection, SystemFileDef.System_Name_Key); }
        public string MMSLocalIP { get => IniManager.ReadIni(SystemFileDef.MMSSection, SystemFileDef.Socket_Local_IP_Key); }
        public int MMSLocalPort { get { return GetIniValue(SystemFileDef.MMSSection, SystemFileDef.Socket_Local_Port_Key); } }
        public string MMSRemoteIP { get => IniManager.ReadIni(SystemFileDef.MMSSection, SystemFileDef.Socket_Remote_IP_Key); }
        public int MMSRemotePort { get { return GetIniValue(SystemFileDef.MMSSection, SystemFileDef.Socket_Remote_Port_Key); } }
        
        public bool MMSDumpRcvMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.MMSSection, SystemFileDef.Rcv_RawData_Key); } }
        public bool MMSDumpSndMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.MMSSection, SystemFileDef.Snd_RawData_Key); } }
        public bool MMSDumpFailMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.MMSSection, SystemFileDef.Fail_RawData_Key); } }
        public bool MMSDumpDebugMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.MMSSection, SystemFileDef.Debug_RawData_Key); } }

        // WMS
        public string WMSApp { get => IniManager.ReadIni(SystemFileDef.WMSSection, SystemFileDef.System_Name_Key); }
        public string WMSLocalIP { get => IniManager.ReadIni(SystemFileDef.WMSSection, SystemFileDef.Socket_Local_IP_Key); }
        public int WMSLocalPort { get { return GetIniValue(SystemFileDef.WMSSection, SystemFileDef.Socket_Local_Port_Key); } }
        public string WMSRemoteIP { get => IniManager.ReadIni(SystemFileDef.WMSSection, SystemFileDef.Socket_Remote_IP_Key); }
        public int WMSRemotePort { get { return GetIniValue(SystemFileDef.WMSSection, SystemFileDef.Socket_Remote_Port_Key); } }

        public bool WMSDumpRcvMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.WMSSection, SystemFileDef.Rcv_RawData_Key); } }
        public bool WMSDumpSndMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.WMSSection, SystemFileDef.Snd_RawData_Key); } }
        public bool WMSDumpFailMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.WMSSection, SystemFileDef.Fail_RawData_Key); } }
        public bool WMSDumpDebugMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.WMSSection, SystemFileDef.Debug_RawData_Key); } }

        // PLC
        public string PLCApp { get => IniManager.ReadIni(SystemFileDef.PLCSection, SystemFileDef.System_Name_Key); }
        public string PLCLocalIP { get => IniManager.ReadIni(SystemFileDef.PLCSection, SystemFileDef.Socket_Local_IP_Key); }
        public int PLCLocalPort { get { return GetIniValue(SystemFileDef.PLCSection, SystemFileDef.Socket_Local_Port_Key); } }
        public string PLCRemoteIP { get => IniManager.ReadIni(SystemFileDef.PLCSection, SystemFileDef.Socket_Remote_IP_Key); }
        public int PLCRemotePort { get { return GetIniValue(SystemFileDef.PLCSection, SystemFileDef.Socket_Remote_Port_Key); } }

        public bool PLCDumpRcvMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.PLCSection, SystemFileDef.Rcv_RawData_Key); } }
        public bool PLCDumpSndMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.PLCSection, SystemFileDef.Snd_RawData_Key); } }
        public bool PLCDumpFailMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.PLCSection, SystemFileDef.Fail_RawData_Key); } }
        public bool PLCDumpDebugMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.PLCSection, SystemFileDef.Debug_RawData_Key); } }


        // Printer
        public string PrinterApp { get => IniManager.ReadIni(SystemFileDef.PrinterSection, SystemFileDef.System_Name_Key); }
        public string PrinterRemoteIP { get => IniManager.ReadIni(SystemFileDef.PrinterSection, SystemFileDef.Socket_Remote_IP_Key); }
        public int PrinterRemotePort { get { return GetIniValue(SystemFileDef.PrinterSection, SystemFileDef.Socket_Remote_Port_Key); } }


        // BarCode
        public string BarCodeApp { get => IniManager.ReadIni(SystemFileDef.BarCodeSection, SystemFileDef.System_Name_Key); }
        public string BarCodeLocalIP { get => IniManager.ReadIni(SystemFileDef.BarCodeSection, SystemFileDef.Socket_Local_IP_Key); }
        public int BarCodeLocalPort { get { return GetIniValue(SystemFileDef.BarCodeSection, SystemFileDef.Socket_Local_Port_Key); } }

        public bool BarCodeDumpRcvMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.BarCodeSection, SystemFileDef.Rcv_RawData_Key); } }
        public bool BarCodeDumpSndMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.BarCodeSection, SystemFileDef.Snd_RawData_Key); } }
        public bool BarCodeDumpFailMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.BarCodeSection, SystemFileDef.Fail_RawData_Key); } }
        public bool BarCodeDumpDebugMsgSwitch { get { return GetConfigBoolVaule(SystemFileDef.BarCodeSection, SystemFileDef.Debug_RawData_Key); } }

        private int GetIniValue(string section, string key)
        {
            return int.Parse(IniManager.ReadIni(section, key));
        }

        private bool GetConfigBoolVaule(string section, string key)
        {
            return bool.Parse(IniManager.ReadIni(section, key));
        }

    }
}
