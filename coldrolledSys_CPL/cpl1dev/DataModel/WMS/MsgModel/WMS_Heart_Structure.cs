using DataModel.WMS;
using System;
using System.Runtime.InteropServices;

namespace DataMod.WMS.MsgModel
{
    /// <summary>
    /// 心跳電文
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class WMS_Heart_Structure
    {
        [MarshalAs(UnmanagedType.Struct)]
        public WMS_Header_Structure Header;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 43)]
        public byte[] Text;
    }
}
