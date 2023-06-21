using MsgStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// L1 報文長度字典集合
/// </summary>
namespace DataMod.Common
{
    public class PlcMsgTypeAndLengthDic
    {
        public readonly Dictionary<short, int> PlcMsgLen = new Dictionary<short, int> {
            { 301, Marshal.SizeOf<L1L2Rcv.Msg_301_EnCoilCut>() },
            { 302, Marshal.SizeOf<L1L2Rcv.Msg_302_CoilWeld>() },
            { 303, Marshal.SizeOf<L1L2Rcv.Msg_303_ReqTrackMap>() },
            { 305, Marshal.SizeOf<L1L2Rcv.Msg_305_TrackMapEn>() },
            { 306, Marshal.SizeOf<L1L2Rcv.Msg_306_TrackMapEx>() },
            { 307, Marshal.SizeOf<L1L2Rcv.Msg_307_CoilDismount>() },
            { 308, Marshal.SizeOf<L1L2Rcv.Msg_308_CoilWeightScale>() },
            { 309, Marshal.SizeOf<L1L2Rcv.Msg_309_EquipMaint>() },
            { 310, Marshal.SizeOf<L1L2Rcv.Msg_310_LineFault>() },
            { 311, Marshal.SizeOf<L1L2Rcv.Msg_311_ExCoilCut>() },
            { 312, Marshal.SizeOf<L1L2Rcv.Msg_312_NewCoilRec>() },
            { 313, Marshal.SizeOf<L1L2Rcv.Msg_313_SpdTen>() },
            { 315, Marshal.SizeOf<L1L2Rcv.Msg_315_Cdc>() },
            { 316, Marshal.SizeOf<L1L2Rcv.Msg_316_Utility_Data>() },
            { 317, Marshal.SizeOf<L1L2Rcv.Msg_317_ReturnCoilInfo>() },
            { 318, Marshal.SizeOf<L1L2Rcv.Msg_318_SideTrimmerInfo>() },
            { 399, Marshal.SizeOf<L1L2Rcv.Msg_399_L1ALIVE>() },
        };
    }
}
