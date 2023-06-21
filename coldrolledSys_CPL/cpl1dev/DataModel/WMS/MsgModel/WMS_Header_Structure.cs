using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DataModel.WMS
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class WMS_Header_Structure
    {
        /// <summary>
        /// 訊息代碼 ,4
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Message_ID;

        /// <summary>
        /// 處理日期時間 ,14
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
        public byte[] Process_Date_Time;

        /// <summary>
        /// 來源電腦代號 ,2
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] ID_Of_Source_Computer;

        /// <summary>
        /// 目的電腦代號 ,2
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] ID_Of_Destination_Computer;

        /// <summary>
        /// 長度 ,4
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Length;
    }
}
