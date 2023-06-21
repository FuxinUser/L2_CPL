using Core.Help;
using System.Configuration;

namespace BCScnMgr.Config
{
    public class AppSetting
    {
        public static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly AppSetting INSTANCE = new AppSetting();
        }
        public static AppSetting Instance { get { return SingletonHolder.INSTANCE; } }

        // Akka System Setting
        public string AkkaSysName { get; private set; }
        public string LocalIp { get; private set; }
        public int LocalPort { get; private set; }
        public string RemoteIp { get; private set; }
        public int RemotePort { get; private set; }

        // Akka System Log Setting
        public string AkkaSysLog { get; private set; }
        public string MgrLog { get; private set; }
        public string ConnLog { get; private set; }
        public string RcvEditLog { get; private set; }
        //public string SndLog { get; private set; }
        public string SndEditLog { get; private set; }

        // Dump Msg File
        public string RcvMsgFilePath { get; private set; }
        public string SndMsgFilePath { get; private set; }
        public string FailMsgFilePath { get; private set; }
        public string DebugFilePath { get; private set; }

        // Crash Log Path
        public string CrashLogPath { get; private set; }

        // Dump Switch
        public bool DumpRcvMsgSwitchOn { get; private set; }
        public bool DumpSndMsgSwitchOn { get; private set; }
        public bool DumpDebugMsgSwitchOn { get; private set; }
        public bool DumpFailMsgSwitchOn { get; private set; }

        public AppSetting()
        {
            // Akka Setting
            AkkaSysName = IniSystemHelper.Instance.BarCodeApp;
            LocalIp = IniSystemHelper.Instance.BarCodeLocalIP;
            LocalPort = IniSystemHelper.Instance.BarCodeLocalPort;
            //RemoteIp = GetAppConfigValue("RemoteIP");
            //RemotePort = GetAppConfigIntVaule("RemotePort");

            // Dump Setting
            RcvMsgFilePath = GetConfigValue("RcvMsgFilePath");
            SndMsgFilePath = GetConfigValue("SndMsgFilePath");
            FailMsgFilePath = GetConfigValue("FailMsgFilePath");
            DebugFilePath = GetConfigValue("DebugMsgFilePath");
            CrashLogPath = GetConfigValue("CrashLogFilePath");

            DumpRcvMsgSwitchOn = IniSystemHelper.Instance.BarCodeDumpRcvMsgSwitch;
            DumpSndMsgSwitchOn = IniSystemHelper.Instance.BarCodeDumpSndMsgSwitch;
            DumpFailMsgSwitchOn = IniSystemHelper.Instance.BarCodeDumpFailMsgSwitch;
            DumpDebugMsgSwitchOn = IniSystemHelper.Instance.BarCodeDumpDebugMsgSwitch;

            // Akka Log Setting
            AkkaSysLog = "AkkaLog";
            MgrLog = "BCScnMgrLog";
            ConnLog = "BCScnConnLog";
            RcvEditLog = "BCScnRcvEditLog";
            //SndLog = "BCScnSndLog";
            SndEditLog = "BCScnSndEditLog";

        }
        private string GetConfigValue(string value)
        {
            return ConfigurationManager.AppSettings[value];
        }
        private int GetConfigIntVaule(string value)
        {
            return int.Parse(ConfigurationManager.AppSettings[value]);
        }
    }
}
