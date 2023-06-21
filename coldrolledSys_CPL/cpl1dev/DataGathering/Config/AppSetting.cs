using System.Configuration;

namespace DataGathering.Config
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

        // Crash Log Path
        public string CrashLogPath { get; private set; }

        // Akka System Log Setting
        public string AkkaSysLog { get; private set; }
        public string MgrLog { get; private set; }

        public string DtProGtrLog { get; private set; }

        public AppSetting()
        {
            // Akka Setting
            AkkaSysName = GetConfigValue("ActorSystemName");

            CrashLogPath = GetConfigValue("CrashLogFilePath");

            // Akka Log Setting
            AkkaSysLog = "AkkaLog";
            MgrLog = "DtGtrMgrLog";
            DtProGtrLog = "DtProGtrLog";
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
