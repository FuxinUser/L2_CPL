﻿using System;
using System.IO;

namespace Core.Help.DumpRawDataHelp
{
    public class DumpPlcMsg : IDumpRawData
    {
        
        public void DumpMsg(byte[] data, string filePath)
        {
            try
            {
                var msgID = GetMsgID(data);
                var sortPath = $"{filePath}{msgID}";

                if (!Directory.Exists(sortPath))
                    Directory.CreateDirectory(sortPath);

                var fileName = $"{sortPath}\\{DateTime.Now.ToString("yyyyMMdd_HHmmss_fff")}_MsgID_{msgID}_{data.Length}.blob";  //  20210521豪霆, 檔案多顯示長度
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(data);
                bw.Flush();
                bw.Close();
                fs.Close();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 取得電文的Msg ID
        /// </summary>
        /// <param name="data">電文資料</param>
        /// <returns></returns>
        public string GetMsgID(byte[] data)
        {
            string strResult = "";
            byte[] tmp = { 0x00, 0x00 };

            if (data.Length >= 4)
            {
                Buffer.BlockCopy(data, 2, tmp, 0, 2);
                Array.Reverse(tmp);
                strResult = BitConverter.ToInt16(tmp, 0).ToString("000");
            }

            return strResult;
        }
    }
}
