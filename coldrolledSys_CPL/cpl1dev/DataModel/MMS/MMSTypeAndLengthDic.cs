using Core.Define;
using MsgStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataMod.MMS
{
    public class MMSTypeAndLengthDic
    {

        public readonly Dictionary<string, int> MMSMsgLen = new Dictionary<string, int> {
            { MMSSysDef.RcvMsgCode.CoilSchedule, Marshal.SizeOf<MMSL2Rcv.Msg_Coil_Schedule>()+1 },
            { MMSSysDef.RcvMsgCode.CoilPDI,  Marshal.SizeOf<MMSL2Rcv.Msg_PDI>()+1 },
            { MMSSysDef.RcvMsgCode.ReqProResult,  Marshal.SizeOf<MMSL2Rcv.Msg_Product_Result_Request>() +1},
            { MMSSysDef.RcvMsgCode.CoilRejectResult,  Marshal.SizeOf<MMSL2Rcv.Msg_Res_For_Coil_Reject_Result>()+1 },
            { MMSSysDef.RcvMsgCode.ReqDeletePlanNo, Marshal.SizeOf<MMSL2Rcv.Msg_Req_Delete_Schedule_Plan>() +1},
            { MMSSysDef.RcvMsgCode.ResForNoCoil, Marshal.SizeOf<MMSL2Rcv.Msg_Res_For_No_Coil_Schedule>() +1},
            { MMSSysDef.RcvMsgCode.ResForNoCoilPDI, Marshal.SizeOf<MMSL2Rcv.Msg_Res_For_No_Coil_PDI>()+1 },
            { MMSSysDef.RcvMsgCode.PdoUploadedReply, Marshal.SizeOf<MMSL2Rcv.Msg_Res_For_PDO_Uploaded>()+1 },
            { MMSSysDef.RcvMsgCode.SleeveValueSyn, Marshal.SizeOf<MMSL2Rcv.Msg_Sleeve_Value_Synchronize>()+1 },
            { MMSSysDef.RcvMsgCode.PaperValueSync, Marshal.SizeOf<MMSL2Rcv.Msg_Paper_Value_Synchronize>()+1 },
            { MMSSysDef.RcvMsgCode.HeartBeatCode, Marshal.SizeOf<MMSL2Rcv.Msg_HeartBeat>()+1 },
        };


        public readonly Dictionary<string, Type> MMSMsgType = new Dictionary<string, Type> {

            // MM->L2
          { MMSSysDef.RcvMsgCode.CoilSchedule, typeof(MMSL2Rcv.Msg_Coil_Schedule) },
            { MMSSysDef.RcvMsgCode.CoilPDI,  typeof(MMSL2Rcv.Msg_PDI) },
            { MMSSysDef.RcvMsgCode.ReqProResult,  typeof(MMSL2Rcv.Msg_Product_Result_Request)},
            { MMSSysDef.RcvMsgCode.CoilRejectResult,  typeof(MMSL2Rcv.Msg_Res_For_Coil_Reject_Result) },
            { MMSSysDef.RcvMsgCode.ReqDeletePlanNo, typeof(MMSL2Rcv.Msg_Req_Delete_Schedule_Plan)},
            { MMSSysDef.RcvMsgCode.ResForNoCoil, typeof(MMSL2Rcv.Msg_Res_For_No_Coil_Schedule)},
            { MMSSysDef.RcvMsgCode.ResForNoCoilPDI, typeof(MMSL2Rcv.Msg_Res_For_No_Coil_PDI) },
            { MMSSysDef.RcvMsgCode.PdoUploadedReply, typeof(MMSL2Rcv.Msg_Res_For_PDO_Uploaded) },
            { MMSSysDef.RcvMsgCode.SleeveValueSyn, typeof(MMSL2Rcv.Msg_Sleeve_Value_Synchronize) },
            { MMSSysDef.RcvMsgCode.PaperValueSync, typeof(MMSL2Rcv.Msg_Paper_Value_Synchronize) },
            { MMSSysDef.RcvMsgCode.HeartBeatCode, typeof(MMSL2Rcv.Msg_HeartBeat) },

            //L2->MM
            { MMSSysDef.SndMsgCode.ReqForCoilSched, typeof(L2MMSSnd.Msg_Req_Coil_Schedule) },
            { MMSSysDef.SndMsgCode.ReqForPDI, typeof(L2MMSSnd.Msg_Request_Coil_PDI) },
            { MMSSysDef.SndMsgCode.ResForCoilSched, typeof(L2MMSSnd.Msg_Res_For_Coil_Schedule) },
            { MMSSysDef.SndMsgCode.ResForCoilPDI, typeof(L2MMSSnd.Msg_Res_For_Coil_PDI) },
            { MMSSysDef.SndMsgCode.CoilRejectData, typeof(L2MMSSnd.Msg_Coil_Reject_Result) },
            { MMSSysDef.SndMsgCode.CoilLoadedSkid, typeof(L2MMSSnd.Msg_Coil_Loaded_Skid) },
            { MMSSysDef.SndMsgCode.CoilPDO, typeof(L2MMSSnd.Msg_PDO) },
            { MMSSysDef.SndMsgCode.EqDownResultCode, typeof(L2MMSSnd.Msg_Equipment_Down_Result_Msg) },
            //{ MMSSysDef.RcvMsgCode.EqDownResultCode, typeof(L2MMSSnd.Msg_Equipment_Down_Result_Msg) },
            { MMSSysDef.SndMsgCode.MMSEnergyConsumptionInfo, typeof(L2MMSSnd.Msg_Energy_Consumption_Info) },
            { MMSSysDef.SndMsgCode.CoilScheduleChanged, typeof(L2MMSSnd.Msg_Coil_Schedule_Changed) },
            { MMSSysDef.SndMsgCode.MMSCoilScheduleDelete, typeof(L2MMSSnd.Msg_Coil_Schedule_Delete) },
            { MMSSysDef.SndMsgCode.MMSResDeletePlanNoResult, typeof(L2MMSSnd.Msg_Res_For_PlanNo_Delete) },

        };
    }
}
