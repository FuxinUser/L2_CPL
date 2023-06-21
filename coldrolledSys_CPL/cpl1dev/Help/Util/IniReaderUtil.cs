using System.IO;
using System.Runtime.InteropServices;
using System.Text;


/**
 Author : ICSC 余士鵬
 Desc : Ini檔讀取操作Help
 */
namespace Core.Util
{
    public class IniReaderUtil
    {
        public enum States
        {
            Finished,
            FileExists
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        public bool IsExists { get; private set; } = false;
        public string FilePath { get; private set; } = string.Empty;
        public IniReaderUtil(string filePath)
        {
            this.FilePath = filePath;

            CheckFileExists();
        }
        private void CheckFileExists()
        {
            IsExists = File.Exists(FilePath);
        }
        public States CreateFile()
        {
            if (!IsExists)
            {
                File.Create(FilePath);

                return States.Finished;
            }
            else return States.FileExists;
        }
        public void WriteIni(string section, string key, string val)
        {
            WritePrivateProfileString(section, key, val, FilePath);
        }
        public string ReadIni(string section, string key)
        {
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", temp, 255, FilePath);
            return temp.ToString();
        }
    }
}
