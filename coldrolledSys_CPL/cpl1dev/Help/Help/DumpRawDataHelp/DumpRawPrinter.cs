using System;
using System.Text;

namespace Core.Help.DumpRawDataHelp
{
    public static class DumpRawPrinter
    {

        public static string PrintRawData(this byte[] bytes, bool toStr=false, bool isTrim = false)
        {
            var strBuild = new StringBuilder();

            if (bytes == null)
                return "Null Bytes";

            #region 轉換 byte[] 為 string
            if (toStr)
            {
                var str = Encoding.UTF8.GetString(bytes);
                str = isTrim ? str.Trim() : str;
                strBuild.Append(str);
                strBuild.AppendLine();
                strBuild.AppendLine();
                strBuild.AppendLine();
            }
          
            #endregion

            #region 印出 byte[] 的內容
            var tmpHexString = "";
            var tmpAsciiString = "";
            var retString = Environment.NewLine + "-----------------------------------------------------------------" + Environment.NewLine;

            for (int idx = 0; idx < bytes.Length; idx++)
            {
                var tmpStr = bytes[idx].ToString("X");

                if (tmpStr.Length == 1)
                    tmpStr = "0" + tmpStr;

                tmpHexString += " " + tmpStr;

                if (bytes[idx] < 20)
                {
                    tmpAsciiString += ".";
                }
                else
                {
                    tmpAsciiString += Encoding.ASCII.GetString(bytes, idx, 1);
                }

                if (idx % 16 == 15)
                {
                    retString += tmpHexString + " " + tmpAsciiString + Environment.NewLine;
                    tmpHexString = "";
                    tmpAsciiString = "";
                }
            }

            if ((bytes.Length - 1) % 16 != 15)
            {
                for (var idx = 0; idx < 15 - ((bytes.Length - 1) % 16); idx++)
                {
                    tmpHexString += "   ";
                    tmpAsciiString += " ";
                }

                retString += tmpHexString + " " + tmpAsciiString + Environment.NewLine;
            }
            retString += "-----------------------------------------------------------------" + Environment.NewLine;
            #endregion

         
            strBuild.Append(retString);

            return strBuild.ToString();
        }
    }
}
