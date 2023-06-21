﻿using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Core.Util
{
    /// <summary>
    /// Ini Reader
    /// </summary>
    public class IniUtil
    {

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileSection(string lpAppName, byte[] lpszReturnBuffer, int nSize, string lpFileName);

        private string filepath;

        public IniUtil(string filepath)
        {
            this.filepath = filepath;
        }

        public void WriteIni(string section, string key, string val)
        {
            WritePrivateProfileString(section, key, val, filepath);
        }

        public string ReadIni(string section, string key)
        {
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", temp, 255, filepath);
            return temp.ToString();
        }

        public string[] ReadIni(string section)
        {
            var buffer = new byte[2048];

            GetPrivateProfileSection(section, buffer, 2048, filepath);

            var tmp = Encoding.ASCII.GetString(buffer).Trim('\0').Split('\0');

            var result = new List<string>();
            foreach (var entry in tmp)
                result.Add(entry.Substring(0, entry.IndexOf("=")));

            return result.ToArray();
        }
    }
}
