﻿
using Core.Define;
using Core.Help.DumpRawDataHelp;
using DataMod.Common;
using PLCComm.Config;
using System;
using System.Runtime.InteropServices;
using static MsgStruct.L2L1Snd;
/**
* Author: ICSC余士鵬
* Date: 2019/9/19
* Description: 聚合服務器(提供DI管理Prop資料)
* Reference: 
* Modified: 
*/
namespace PLCComm.Service
{
    public class AggregateService
    {

        public PlcMsgTypeAndLengthDic MsgLengthAndTypeDic { get; set; }

        public Msg_299_L2ALIVE AliveMsg { get; set; }
        public AppSetting appSetting { get; set; }

        public IDumpRawData DumpRawData { get; set; }

        public AggregateService() { }

        public void InitAliveMsg()
        {
            AliveMsg.MessageId = short.Parse(PlcSysDef.SndMsgCode.L2299Alive);
            AliveMsg.MessageLength = (short)Marshal.SizeOf<Msg_299_L2ALIVE>();
            AliveMsg.Date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
            AliveMsg.Time = Int32.Parse(DateTime.Now.ToString("HHmmss"));
        }

        public Msg_299_L2ALIVE GetNowAliveMsg()
        {
            AliveMsg.Date = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
            AliveMsg.Time = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
            return AliveMsg;
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
