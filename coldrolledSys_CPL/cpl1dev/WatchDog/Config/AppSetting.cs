using Core.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace WatchDog.Config
{
    public class AppSetting
    {
        public string SysName { get; private set; } = string.Empty;

        public string LogName { get; private set; } = string.Empty;

        public string[][] ProcItems { get; private set; } = null;

        public AppSetting()
        {
            var iniUtil = new IniUtil(SysParam.IniPath);

            LogName = "WatchDogLog";

            ProcItems = new Func<string[][]>(() => {
                var keys = iniUtil.ReadIni(SysParam.Section_Proc);
                var list = new List<string[]>();

                foreach (var key in keys)
                    list.Add(new string[] { key, iniUtil.ReadIni(SysParam.Section_Proc, key) });
                    
                return list.ToArray();
            })();
        }

        /// <summary>
        ///     系統參數
        /// </summary>
        protected class SysParam
        {
            public const string IniFileName = "Config.ini";         //  設定檔名稱
            public const string Section_Proc = "Proc";              //  Section：執行緒資訊節點

            public static readonly string IniPath = Path.Combine(GetIniPath(), IniFileName);    //  設定檔路徑

            /// <summary>
            ///     取得設定檔路徑
            /// </summary>
            private static string GetIniPath()
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }
    }
}
