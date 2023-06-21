using Akka.IO;
using Core.Define;
using Core.Help.DumpRawDataHelp;
using Core.Util;
using DataMod.Common;
using DataMod.WMS.MsgModel;
using DataModel.Common;
using DataModel.WMS;
using System;
using System.Runtime.InteropServices;
using WMSComm.Config;

namespace WMSComm.Service
{
    public class AggregateService
    {
        public WMS_ACK_Structure WmsAck { get; set; }

        public WMS_Heart_Structure WMSHeartBeatMsg { get; set; }

        public WMSMsgTypeAndLengthDic MsgLengthAndTypeDic { get; set; }

        public SendQueue<ByteString> SndQueue { get; set; }
        public AppSetting appSetting { get; set; }

        public IDumpRawData DumpRawData { get; set; }

        public AggregateService()
        {

        }

        public void InitMMSHeartBeatMsg()
        {
            WMSHeartBeatMsg = new WMS_Heart_Structure()
            {
                Header = new WMS_Header_Structure()
                {
                    Length = Marshal.SizeOf<WMS_Heart_Structure>().ToString("0000").ToCByteArray(4),
                    Message_ID = "TT01".ToCByteArray(6),
                    ID_Of_Source_Computer = L2SystemDef.L2.ToCByteArray(2),
                    ID_Of_Destination_Computer = WMSSysDef.WMS.ToCByteArray(2),                
                }
            };
        }


        public void InitWMSAckMsg()
        {
            WmsAck.Header = new WMS_Header_Structure()
            {
                Message_ID = WMSSysDef.RcvMsgCode.WMSAck.ToCByteArray(4),
                ID_Of_Source_Computer = L2SystemDef.SystemIDCode.ToCByteArray(2),
                ID_Of_Destination_Computer = WMSSysDef.WMS.ToCByteArray(2),
                Length = Marshal.SizeOf<WMS_ACK_Structure>().ToString("0000").ToCByteArray(4),
            };

            WmsAck.Blank = string.Empty.ToCChar(1);
            WmsAck.Blank2 = string.Empty.ToCChar(2);
        }

        public WMS_Heart_Structure GetNowHeartBeatMsg()
        {
            WMSHeartBeatMsg.Header.Process_Date_Time = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(14);          
            WMSHeartBeatMsg.Text = "THE QUICK BROWN FOX JUMPS OVER THE LAZY DOG".ToCByteArray(43);

            return WMSHeartBeatMsg;
        }

        public WMS_ACK_Structure GetNowTimeAckMsg(string msgID, string isAccept, string errorCode = "", string errorMsg = "")
        {

            WmsAck.Header.Process_Date_Time = DateTime.Now.ToString("yyyyMMddHHmmss").ToCByteArray(14);
            WmsAck.AcceptedOrNot = isAccept.ToCChar(1);
            WmsAck.ReceivedMsgID = msgID.ToCChar(4);
            WmsAck.ErrorCode = errorCode.ToCChar(4);
            WmsAck.ErrorMsg = errorMsg.ToCChar(31);
            return WmsAck;
        }


        public void DumpRcvRawData(byte[] msg)
        {
            try
            {
                if (appSetting.DumpRcvMsgSwitchOn)
                    DumpRawData.DumpMsg(msg, appSetting.RcvMsgFilePath);
            }
            catch
            {
                throw;
            }
        }

        public void DumpSndRawData(byte[] msg)
        {
            try
            {
                if (appSetting.DumpSndMsgSwitchOn)
                    DumpRawData.DumpMsg(msg, appSetting.SndMsgFilePath);
            }
            catch
            {
                throw;
            }
        }

        public void DumpFailRawData(byte[] msg)
        {
            try
            {
                if (appSetting.DumpFailMsgSwitchOn)
                    DumpRawData.DumpMsg(msg, appSetting.FailMsgFilePath);
            }
            catch
            {
                throw;
            }
        }

        public void DumpDebugRawData(byte[] msg)
        {
            try
            {
                if (appSetting.DumpDebugMsgSwitchOn)
                    DumpRawData.DumpMsg(msg, appSetting.DebugFilePath);
            }
            catch
            {
                throw;
            }
        }


    }
}
