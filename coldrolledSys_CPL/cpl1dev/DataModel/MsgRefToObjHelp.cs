using Core.Define;
using MsgStruct;
using System;

namespace Core.Help
{
    public class MsgRefToObjHelp
    {
        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly MsgRefToObjHelp INSTANCE = new MsgRefToObjHelp();
        }
        public static MsgRefToObjHelp Instance { get { return SingletonHolder.INSTANCE; } }

        public Type GetPlcStructClassType(string messageID)
        {
            Type t = null;

            switch (messageID)
            {
                case PlcSysDef.RcvMsgCode.L1301EnCoilCut:
                    t = typeof(L1L2Rcv.Msg_301_EnCoilCut);
                    break;
                case PlcSysDef.RcvMsgCode.L1302WieldRecord:
                    t = typeof(L1L2Rcv.Msg_302_CoilWeld);
                    break;
                case PlcSysDef.RcvMsgCode.L1303ReqTrackMap:
                    t = typeof(L1L2Rcv.Msg_303_ReqTrackMap);
                    break;
                case PlcSysDef.RcvMsgCode.L1305TrackMapEn:
                    t = typeof(L1L2Rcv.Msg_305_TrackMapEn);
                    break;
                case PlcSysDef.RcvMsgCode.L1306TrackMapEx:
                    t = typeof(L1L2Rcv.Msg_306_TrackMapEx);
                    break;
                case PlcSysDef.RcvMsgCode.L1307CoilDismount:
                    t = typeof(L1L2Rcv.Msg_307_CoilDismount);
                    break;
                case PlcSysDef.RcvMsgCode.L1308CoilWeightScale:
                    t = typeof(L1L2Rcv.Msg_308_CoilWeightScale);
                    break;
                case PlcSysDef.RcvMsgCode.L1309EquipMaint:
                    t = typeof(L1L2Rcv.Msg_309_EquipMaint);
                    break;
                case PlcSysDef.RcvMsgCode.L1310LineFault:
                    t = typeof(L1L2Rcv.Msg_310_LineFault);
                    break;
                case PlcSysDef.RcvMsgCode.L1311ExCoilCut:
                    t = typeof(L1L2Rcv.Msg_311_ExCoilCut);
                    break;
                case PlcSysDef.RcvMsgCode.L1312NewCoilRec:
                    t = typeof(L1L2Rcv.Msg_312_NewCoilRec);
                    break;
                case PlcSysDef.RcvMsgCode.L1313SpdTen:
                    t = typeof(L1L2Rcv.Msg_313_SpdTen);
                    break;
                case PlcSysDef.RcvMsgCode.L1315Cdc:
                    t = typeof(L1L2Rcv.Msg_315_Cdc);
                    break;
                case PlcSysDef.RcvMsgCode.L1316Utility:
                    t = typeof(L1L2Rcv.Msg_316_Utility_Data);
                    break;
                case PlcSysDef.RcvMsgCode.L1317ReturnCoilInfo:
                    t = typeof(L1L2Rcv.Msg_317_ReturnCoilInfo);
                    break;
                case PlcSysDef.RcvMsgCode.L1318SideTrimmerInfo:
                    t = typeof(L1L2Rcv.Msg_318_SideTrimmerInfo);
                    break;
                case PlcSysDef.RcvMsgCode.L1399Alive:
                    t = typeof(L1L2Rcv.Msg_399_L1ALIVE);
                    break;
            }


            return t;
        }



      

    }
}
