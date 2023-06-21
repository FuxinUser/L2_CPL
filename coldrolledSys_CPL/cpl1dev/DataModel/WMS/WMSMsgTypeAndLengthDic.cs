using Core.Define;
using DataMod.WMS.MsgModel;
using MsgStruct;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DataMod.Common
{
    public class WMSMsgTypeAndLengthDic
    {

        public readonly Dictionary<string, int> WMSMsgLen = new Dictionary<string, int> {
              { WMSSysDef.RcvMsgCode.WMSCoilScheduleInfo, Marshal.SizeOf<L2_WMS_Snd.PWx1_ScheduleList>() },
            { WMSSysDef.RcvMsgCode.WMSEntryDeliveryTrk, Marshal.SizeOf<L2_WMS_Snd.PWx2_TrackingMap>() },
            { WMSSysDef.RcvMsgCode.WMSCoilPDO, Marshal.SizeOf<L2_WMS_Snd.PWx3_CoilInfo>() },
            { WMSSysDef.RcvMsgCode.WMSProdLineCoilReq, Marshal.SizeOf<L2_WMS_Snd.PWx5_FeedingRequest_EntryExitReturn>() },
            { WMSSysDef.SndMsgCode.WMSCoilProDone, Marshal.SizeOf<WMS_L2_Rcv.WPx1_CompleteOfFeeding>() },
            { WMSSysDef.RcvMsgCode.HeartBeatCode, Marshal.SizeOf<WMS_Heart_Structure>() },
            { WMSSysDef.SndMsgCode.WMSCoilProResRequest, Marshal.SizeOf<WMS_L2_Rcv.WPx3_RequestResponse>() },
            };

        public readonly Dictionary<string, Type> WMSMsgType = new Dictionary<string, Type>
        {
            //for test
            { WMSSysDef.RcvMsgCode.WMSCoilScheduleInfo, typeof(L2_WMS_Snd.PWx1_ScheduleList) },
            { WMSSysDef.RcvMsgCode.WMSEntryDeliveryTrk, typeof(L2_WMS_Snd.PWx2_TrackingMap) },
             { WMSSysDef.RcvMsgCode.HeartBeatCode, typeof(WMS_Heart_Structure) },
            { WMSSysDef.RcvMsgCode.WMSCoilPDO, typeof(L2_WMS_Snd.PWx3_CoilInfo) },         
            { WMSSysDef.RcvMsgCode.WMSProdLineCoilReq, typeof(L2_WMS_Snd.PWx5_FeedingRequest_EntryExitReturn) },
            { WMSSysDef.SndMsgCode.WMSCoilProDone, typeof(WMS_L2_Rcv.WPx1_CompleteOfFeeding) },
            { WMSSysDef.SndMsgCode.WMSCoilProResRequest, typeof(WMS_L2_Rcv.WPx3_RequestResponse) },
               { WMSSysDef.RcvMsgCode.WMSInfoScanID, typeof(L2_WMS_Snd.PWx6_ScanCoil) },
            
        };

    }
}
