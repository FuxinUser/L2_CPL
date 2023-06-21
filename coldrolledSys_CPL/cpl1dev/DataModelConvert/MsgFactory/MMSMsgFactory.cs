using Core.Define;
using MsgStruct;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using static DataMod.Common.MMSMsgProResultModel;
using Core.Util;
using static MsgStruct.L2MMSSnd;
using static DBService.Repository.DefectData.CoilDefectDataEntity;
using static DataMod.Response.RespnseModel;
using static DataModel.HMIServerCom.Msg.SCCommMsg;
using static DataMod.PLC.GenCoilInfoModel;

namespace MsgConvert
{
    public static class MMSMsgFactory
    {
        public static Msg_Coil_Schedule_Changed CoilScheduleChangedMsg(List<string> coilsOfTable)
        {
            var coilIDStr = string.Join("", coilsOfTable.ToArray());
            var CoilSchChange = new Msg_Coil_Schedule_Changed()
            {
                Code = MMSSysDef.SndMsgCode.CoilScheduleChanged.ToCByteArray(6),
                FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1),
                SendWho = L2SystemDef.L2.ToCByteArray(2),
                RcvWho = MMSSysDef.MMS.ToCByteArray(2),
                Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8),
                Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6),
                Length = CalculateLength<Msg_Coil_Schedule_Changed>(),

                Number_Of_Coils = coilsOfTable.Count.ToNByteArray(3),
                Entry_Coil_No = coilIDStr.ToCByteArray(6000)
            };
            return CoilSchChange;
        }

        public static Msg_Coil_Loaded_Skid CoilLoadSkidMsg(string planNo, string entryCoilNo)
        {
            var coilLoadSkid = new Msg_Coil_Loaded_Skid()
            {
                Code = MMSSysDef.SndMsgCode.CoilLoadedSkid.ToCByteArray(6),
                FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1),
                SendWho = L2SystemDef.L2.ToCByteArray(2),
                RcvWho = MMSSysDef.MMS.ToCByteArray(2),
                Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8),
                Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6),
                Length = CalculateLength<L2MMSSnd.Msg_Coil_Loaded_Skid>(),

                Plan_No = planNo.ToCByteArray(10),
                Entry_Coil_No = entryCoilNo.ToCByteArray(20),
                Loaded_Time = DateTime.Now.ToString("yyyyMMddHHmmss").ToCByteArray(14),
                Unit_Code = L2SystemDef.SystemIDCode.ToCByteArray(4),


            };

            return coilLoadSkid;


        }

        public static Msg_Coil_Reject_Result CoilRejectResult(CoilRejectInfo rejectInfo)
        {
            var coilRejectResult = new Msg_Coil_Reject_Result();

            //Header
            coilRejectResult.Code = MMSSysDef.SndMsgCode.CoilRejectData.ToCByteArray(6);
            coilRejectResult.FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1);
            coilRejectResult.SendWho = L2SystemDef.L2.ToCByteArray(2);
            coilRejectResult.RcvWho = MMSSysDef.MMS.ToCByteArray(2);
            coilRejectResult.Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8);
            coilRejectResult.Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6);
            coilRejectResult.Length = CalculateLength<Msg_Coil_Reject_Result>();
            //Data
            coilRejectResult.Reject_Coil_No = rejectInfo.RetrunCoilInfo.Coil_ID.ToCByteArray(20);
            coilRejectResult.Entry_CoilNo = rejectInfo.RetrunCoilInfo.Entry_Coil_ID.ToCByteArray(20);
            coilRejectResult.Plan_No = rejectInfo.RetrunCoilInfo.Plan_No.ToCByteArray(10);
            coilRejectResult.Mode_Of_Reject = rejectInfo.RetrunCoilInfo.Mode_Of_Reject.ToCByteArray(1);

            coilRejectResult.Length_Of_Rejected_Coil = rejectInfo.RetrunCoilInfo.Length_Of_Rejected_Coil.ToNByteArray(5);
            coilRejectResult.Weight_Of_Rejected_Coil = rejectInfo.RetrunCoilInfo.Weight_Of_Rejected_Coil.ToNByteArray(5);
            coilRejectResult.Inner_Diameter_Of_RejectedCoil = rejectInfo.RetrunCoilInfo.Inner_Diameter_Of_RejectedCoil.ToNByteArray(4);
            coilRejectResult.Outer_Diameter_Of_RejectedCoil = rejectInfo.RetrunCoilInfo.Outer_Diameter_Of_RejectedCoil.ToNByteArray(4);
            
            coilRejectResult.Reason_Of_Reject = rejectInfo.RetrunCoilInfo.Reason_Of_Reject.ToCByteArray(4);
            coilRejectResult.Time_Of_Reject = rejectInfo.RetrunCoilInfo.Time_Of_Reject.ToCByteArray(14);
            coilRejectResult.Shift_Of_Reject = rejectInfo.RetrunCoilInfo.Shift_Of_Reject.ToCByteArray(1);
            coilRejectResult.Turn_Of_Reject = rejectInfo.RetrunCoilInfo.Turn_Of_Reject.ToCByteArray(1);
            coilRejectResult.Paper_exit_Code = rejectInfo.RetrunCoilInfo.Paper_exit_Code.ToCByteArray(1);
            coilRejectResult.Paper_Type = rejectInfo.RetrunCoilInfo.Paper_Type.ToCByteArray(4);
            coilRejectResult.Finally_Tag = "0".ToCByteArray(1);

            float temp = 12;

            coilRejectResult.Head_Paper_Length = temp.ToNByteArray(5);
            coilRejectResult.Tail_Paper_Length = temp.ToNByteArray(5);
            coilRejectResult.Head_Paper_Width = temp.ToNByteArray(5,10);
            coilRejectResult.Tail_Paper_Width = temp.ToNByteArray(5,10);


            coilRejectResult.D1_Code = rejectInfo.DefectInfo.D01_Code.ToString().ToCByteArray(3);
            coilRejectResult.D1_Origin = rejectInfo.DefectInfo.D01_Origin.ToCByteArray(3);
            coilRejectResult.D1_Sid = rejectInfo.DefectInfo.D01_Sid.ToCByteArray(1);
            coilRejectResult.D1_Pos_W = rejectInfo.DefectInfo.D01_Pos_W.ToCByteArray(1);
            coilRejectResult.D1_Pos_L_Start = rejectInfo.DefectInfo.D01_Pos_L_Start.ToString().ToCByteArray(4);
            coilRejectResult.D1_Pos_L_End = rejectInfo.DefectInfo.D01_Pos_L_End.ToCByteArray(4);
            coilRejectResult.D1_Level = rejectInfo.DefectInfo.D01_Level.ToCByteArray(1);
            coilRejectResult.D1_Percent = rejectInfo.DefectInfo.D01_Percent.ToCByteArray(3);
            coilRejectResult.D1_QGrade = rejectInfo.DefectInfo.D01_QGRADE.ToCByteArray(1);

            coilRejectResult.D2_Code = rejectInfo.DefectInfo.D02_Code.ToString().ToCByteArray(3);
            coilRejectResult.D2_Origin = rejectInfo.DefectInfo.D02_Origin.ToCByteArray(3);
            coilRejectResult.D2_Sid = rejectInfo.DefectInfo.D02_Sid.ToCByteArray(1);
            coilRejectResult.D2_Pos_W = rejectInfo.DefectInfo.D02_Pos_W.ToCByteArray(1);
            coilRejectResult.D2_Pos_L_Start = rejectInfo.DefectInfo.D02_Pos_L_Start.ToString().ToCByteArray(4);
            coilRejectResult.D2_Pos_L_End = rejectInfo.DefectInfo.D02_Pos_L_End.ToCByteArray(4);
            coilRejectResult.D2_Level = rejectInfo.DefectInfo.D02_Level.ToCByteArray(1);
            coilRejectResult.D2_Percent = rejectInfo.DefectInfo.D02_Percent.ToCByteArray(3);
            coilRejectResult.D2_QGrade = rejectInfo.DefectInfo.D02_QGRADE.ToCByteArray(1);

            coilRejectResult.D3_Code = rejectInfo.DefectInfo.D03_Code.ToString().ToCByteArray(3);
            coilRejectResult.D3_Origin = rejectInfo.DefectInfo.D03_Origin.ToCByteArray(3);
            coilRejectResult.D3_Sid = rejectInfo.DefectInfo.D03_Sid.ToCByteArray(1);
            coilRejectResult.D3_Pos_W = rejectInfo.DefectInfo.D03_Pos_W.ToCByteArray(1);
            coilRejectResult.D3_Pos_L_Start = rejectInfo.DefectInfo.D03_Pos_L_Start.ToString().ToCByteArray(4);
            coilRejectResult.D3_Pos_L_End = rejectInfo.DefectInfo.D03_Pos_L_End.ToCByteArray(4);
            coilRejectResult.D3_Level = rejectInfo.DefectInfo.D03_Level.ToCByteArray(1);
            coilRejectResult.D3_Percent = rejectInfo.DefectInfo.D03_Percent.ToCByteArray(3);
            coilRejectResult.D3_QGrade = rejectInfo.DefectInfo.D03_QGRADE.ToCByteArray(1);

            coilRejectResult.D4_Code = rejectInfo.DefectInfo.D04_Code.ToString().ToCByteArray(3);
            coilRejectResult.D4_Origin = rejectInfo.DefectInfo.D04_Origin.ToCByteArray(3);
            coilRejectResult.D4_Sid = rejectInfo.DefectInfo.D04_Sid.ToCByteArray(1);
            coilRejectResult.D4_Pos_W = rejectInfo.DefectInfo.D04_Pos_W.ToCByteArray(1);
            coilRejectResult.D4_Pos_L_Start = rejectInfo.DefectInfo.D04_Pos_L_Start.ToString().ToCByteArray(4);
            coilRejectResult.D4_Pos_L_End = rejectInfo.DefectInfo.D04_Pos_L_End.ToCByteArray(4);
            coilRejectResult.D4_Level = rejectInfo.DefectInfo.D04_Level.ToCByteArray(1);
            coilRejectResult.D4_Percent = rejectInfo.DefectInfo.D04_Percent.ToCByteArray(3);
            coilRejectResult.D4_QGrade = rejectInfo.DefectInfo.D04_QGRADE.ToCByteArray(1);

            coilRejectResult.D5_Code = rejectInfo.DefectInfo.D05_Code.ToString().ToCByteArray(3);
            coilRejectResult.D5_Origin = rejectInfo.DefectInfo.D05_Origin.ToCByteArray(3);
            coilRejectResult.D5_Sid = rejectInfo.DefectInfo.D05_Sid.ToCByteArray(1);
            coilRejectResult.D5_Pos_W = rejectInfo.DefectInfo.D05_Pos_W.ToCByteArray(1);
            coilRejectResult.D5_Pos_L_Start = rejectInfo.DefectInfo.D05_Pos_L_Start.ToString().ToCByteArray(4);
            coilRejectResult.D5_Pos_L_End = rejectInfo.DefectInfo.D05_Pos_L_End.ToCByteArray(4);
            coilRejectResult.D5_Level = rejectInfo.DefectInfo.D05_Level.ToCByteArray(1);
            coilRejectResult.D5_Percent = rejectInfo.DefectInfo.D05_Percent.ToCByteArray(3);
            coilRejectResult.D5_QGrade = rejectInfo.DefectInfo.D05_QGRADE.ToCByteArray(1);

            coilRejectResult.D6_Code = rejectInfo.DefectInfo.D06_Code.ToString().ToCByteArray(3);
            coilRejectResult.D6_Origin = rejectInfo.DefectInfo.D06_Origin.ToCByteArray(3);
            coilRejectResult.D6_Sid = rejectInfo.DefectInfo.D06_Sid.ToCByteArray(1);
            coilRejectResult.D6_Pos_W = rejectInfo.DefectInfo.D06_Pos_W.ToCByteArray(1);
            coilRejectResult.D6_Pos_L_Start = rejectInfo.DefectInfo.D06_Pos_L_Start.ToString().ToCByteArray(4);
            coilRejectResult.D6_Pos_L_End = rejectInfo.DefectInfo.D06_Pos_L_End.ToCByteArray(4);
            coilRejectResult.D6_Level = rejectInfo.DefectInfo.D06_Level.ToCByteArray(1);
            coilRejectResult.D6_Percent = rejectInfo.DefectInfo.D06_Percent.ToCByteArray(3);
            coilRejectResult.D6_QGrade = rejectInfo.DefectInfo.D06_QGRADE.ToCByteArray(1);

            coilRejectResult.D7_Code = rejectInfo.DefectInfo.D07_Code.ToString().ToCByteArray(3);
            coilRejectResult.D7_Origin = rejectInfo.DefectInfo.D07_Origin.ToCByteArray(3);
            coilRejectResult.D7_Sid = rejectInfo.DefectInfo.D07_Sid.ToCByteArray(1);
            coilRejectResult.D7_Pos_W = rejectInfo.DefectInfo.D07_Pos_W.ToCByteArray(1);
            coilRejectResult.D7_Pos_L_Start = rejectInfo.DefectInfo.D07_Pos_L_Start.ToString().ToCByteArray(4);
            coilRejectResult.D7_Pos_L_End = rejectInfo.DefectInfo.D07_Pos_L_End.ToCByteArray(4);
            coilRejectResult.D7_Level = rejectInfo.DefectInfo.D07_Level.ToCByteArray(1);
            coilRejectResult.D7_Percent = rejectInfo.DefectInfo.D07_Percent.ToCByteArray(3);
            coilRejectResult.D7_QGrade = rejectInfo.DefectInfo.D07_QGRADE.ToCByteArray(1);

            coilRejectResult.D8_Code = rejectInfo.DefectInfo.D08_Code.ToString().ToCByteArray(3);
            coilRejectResult.D8_Origin = rejectInfo.DefectInfo.D08_Origin.ToCByteArray(3);
            coilRejectResult.D8_Sid = rejectInfo.DefectInfo.D08_Sid.ToCByteArray(1);
            coilRejectResult.D8_Pos_W = rejectInfo.DefectInfo.D08_Pos_W.ToCByteArray(1);
            coilRejectResult.D8_Pos_L_Start = rejectInfo.DefectInfo.D08_Pos_L_Start.ToString().ToCByteArray(4);
            coilRejectResult.D8_Pos_L_End = rejectInfo.DefectInfo.D08_Pos_L_End.ToCByteArray(4);
            coilRejectResult.D8_Level = rejectInfo.DefectInfo.D08_Level.ToCByteArray(1);
            coilRejectResult.D8_Percent = rejectInfo.DefectInfo.D08_Percent.ToCByteArray(3);
            coilRejectResult.D8_QGrade = rejectInfo.DefectInfo.D08_QGRADE.ToCByteArray(1);

            coilRejectResult.D9_Code = rejectInfo.DefectInfo.D09_Code.ToString().ToCByteArray(3);
            coilRejectResult.D9_Origin = rejectInfo.DefectInfo.D09_Origin.ToCByteArray(3);
            coilRejectResult.D9_Sid = rejectInfo.DefectInfo.D09_Sid.ToCByteArray(1);
            coilRejectResult.D9_Pos_W = rejectInfo.DefectInfo.D09_Pos_W.ToCByteArray(1);
            coilRejectResult.D9_Pos_L_Start = rejectInfo.DefectInfo.D09_Pos_L_Start.ToString().ToCByteArray(4);
            coilRejectResult.D9_Pos_L_End = rejectInfo.DefectInfo.D09_Pos_L_End.ToCByteArray(4);
            coilRejectResult.D9_Level = rejectInfo.DefectInfo.D09_Level.ToCByteArray(1);
            coilRejectResult.D9_Percent = rejectInfo.DefectInfo.D09_Percent.ToCByteArray(3);
            coilRejectResult.D9_QGrade = rejectInfo.DefectInfo.D09_QGRADE.ToCByteArray(1);

            coilRejectResult.D10_Code = rejectInfo.DefectInfo.D10_Code.ToString().ToCByteArray(3);
            coilRejectResult.D10_Origin = rejectInfo.DefectInfo.D10_Origin.ToCByteArray(3);
            coilRejectResult.D10_Sid = rejectInfo.DefectInfo.D10_Sid.ToCByteArray(1);
            coilRejectResult.D10_Pos_W = rejectInfo.DefectInfo.D10_Pos_W.ToCByteArray(1);
            coilRejectResult.D10_Pos_L_Start = rejectInfo.DefectInfo.D10_Pos_L_Start.ToString().ToCByteArray(4);
            coilRejectResult.D10_Pos_L_End = rejectInfo.DefectInfo.D10_Pos_L_End.ToCByteArray(4);
            coilRejectResult.D10_Level = rejectInfo.DefectInfo.D10_Level.ToCByteArray(1);
            coilRejectResult.D10_Percent = rejectInfo.DefectInfo.D10_Percent.ToCByteArray(3);
            coilRejectResult.D10_QGrade = rejectInfo.DefectInfo.D10_QGRADE.ToCByteArray(1);

            return coilRejectResult;
        }

        //public static Msg_PDO CoilPDO(PDO tblPDO, DefectData defect)
        public static Msg_PDO CoilPDO(PDO tblPDO, DefectData defect)
        {


            var msg  = new Msg_PDO();

            //Header
            msg.Code = MMSSysDef.SndMsgCode.CoilPDO.ToCByteArray(6);
                msg.FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1);
            msg.SendWho = L2SystemDef.L2.ToCByteArray(2);
            msg.RcvWho = MMSSysDef.MMS.ToCByteArray(2);
            msg.Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8);
            msg.Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6);
            msg.Length = CalculateLength<Msg_PDO>();
            // Data             
            msg.Order_No = tblPDO.OrderNo.ToCByteArray(10);
            msg.Plan_No = tblPDO.Plan_No.ToCByteArray(10);
            msg.Out_mat_No = tblPDO.Out_Coil_ID.ToCByteArray(20);
            msg.In_mat_No = tblPDO.In_Coil_ID.ToCByteArray(20);
            msg.StartTime = tblPDO.StartTime.ToString("yyyyMMddHHmmss").ToCByteArray(14);
            msg.FinishTime = tblPDO.FinishTime.ToString("yyyyMMddHHmmss").ToCByteArray(14);
            msg.Shift = tblPDO.Shift.ToCByteArray(1);
            msg.Team = tblPDO.Team.ToCByteArray(1);
            msg.Out_Mat_Outer_Diameter = tblPDO.Out_Coil_Outer_Diameter.ToNByteArray(4);
            msg.Out_Mat_Inner = tblPDO.Out_Coil_Inner.ToNByteArray(4);
            msg.Out_Mat_Wt = tblPDO.Out_Coil_Wt.ToNByteArray(5);
            msg.Out_Mat_Gross_WT = tblPDO.Out_Coil_Gross_WT.ToNByteArray(5);
            msg.Out_Mat_Thick = tblPDO.Out_Coil_Thick.ToNByteArray(5,1000);
            msg.Out_Mat_Width = tblPDO.Out_Coil_Width.ToNByteArray(5,10);
            msg.Out_Mat_Length = tblPDO.Out_Coil_Length.ToNByteArray(5);
            msg.Paper_Code = tblPDO.Paper_Code.ToCByteArray(3);
            msg.Paper_Req_Code = tblPDO.Paper_Req_Code.ToCByteArray(1);
            msg.Out_Head_Paper_Length = tblPDO.Out_Head_Paper_Length.ToNByteArray(5);
            msg.Out_Head_Paper_Width = tblPDO.Out_Head_Paper_Width.ToNByteArray(5,10);
            msg.Out_Tail_Paper_Length = tblPDO.Out_Tail_Paper_Length.ToNByteArray(5);
            msg.Out_Tail_Paper_Width = tblPDO.Out_Tail_Paper_Width.ToNByteArray(5,10);
            msg.Sleeve_Inner_Exit_Diamter = tblPDO.Sleeve_Inner_Exit_Diamter.ToNByteArray(4);
            msg.Sleeve_Type_Exit_Code = tblPDO.Sleeve_Type_Exit_Code.ToCByteArray(3);
            msg.Head_Hole_Position = tblPDO.Head_Hole_Position.ToNByteArray(7,1000);
            msg.Head_Leader_Length = tblPDO.Head_Leader_Length.ToNByteArray(5);
            msg.Head_Leader_Width = tblPDO.Head_Leader_Width.ToNByteArray(5,10);
            msg.Head_Leader_Thickness = tblPDO.Head_Leader_Thickness.ToNByteArray(5,1000);
            msg.Tail_PunchHole_Position = tblPDO.Tail_PunchHole_Position.ToNByteArray(7,1000);
            msg.Tail_Leader_Length = tblPDO.Tail_Leader_Length.ToNByteArray(5);
            msg.Tail_Leader_Width = tblPDO.Tail_Leader_Width.ToNByteArray(5,10);
            msg.Tail_Leader_Thickness = tblPDO.Tail_Leader_Thickness.ToNByteArray(5,1000);
            msg.Scraped_Length_Entry = tblPDO.Scraped_Length_Entry.ToNByteArray(4);
            msg.Scraped_Length_Exit = tblPDO.Scraped_Length_Exit.ToNByteArray(4);
            msg.Head_Leader_St_No = tblPDO.Head_Leader_St_No.ToCByteArray(8);
            msg.Tail_Leader_St_No = tblPDO.Tail_Leader_St_No.ToCByteArray(8);

            msg.D01_Code = defect.D01_Code.ToCByteArray(3);
            msg.D01_Origin = defect.D01_Origin.ToCByteArray(3);
            msg.D01_Sid = defect.D01_Sid.ToCByteArray(1);
            msg.D01_Pos_W = defect.D01_Pos_W.ToCByteArray(1);
            msg.D01_Pos_L_Start = defect.D01_Pos_L_Start.ToCByteArray(4);
            msg.D01_Pos_L_End = defect.D01_Pos_L_End.ToCByteArray(4);
            msg.D01_Level = defect.D01_Level.ToCByteArray(1);
            msg.D01_Percent = defect.D01_Percent.ToCByteArray(3);
            msg.D01_QGRADE = defect.D01_QGRADE.ToCByteArray(1);

            msg.D02_Code = defect.D02_Code.ToCByteArray(3);
            msg.D02_Origin = defect.D02_Origin.ToCByteArray(3);
            msg.D02_Sid = defect.D02_Sid.ToCByteArray(1);
            msg.D02_Pos_W = defect.D02_Pos_W.ToCByteArray(1);
            msg.D02_Pos_L_Start = defect.D02_Pos_L_Start.ToCByteArray(4);
            msg.D02_Pos_L_End = defect.D02_Pos_L_End.ToCByteArray(4);
            msg.D02_Level = defect.D02_Level.ToCByteArray(1);
            msg.D02_Percent = defect.D02_Percent.ToCByteArray(3);
            msg.D02_QGRADE = defect.D02_QGRADE.ToCByteArray(1);


            msg.D03_Code = defect.D03_Code.ToCByteArray(3);
            msg.D03_Origin = defect.D03_Origin.ToCByteArray(3);
            msg.D03_Sid = defect.D03_Sid.ToCByteArray(1);
            msg.D03_Pos_W = defect.D03_Pos_W.ToCByteArray(1);
            msg.D03_Pos_L_Start = defect.D03_Pos_L_Start.ToCByteArray(4);
            msg.D03_Pos_L_End = defect.D03_Pos_L_End.ToCByteArray(4);
            msg.D03_Level = defect.D03_Level.ToCByteArray(1);
            msg.D03_Percent = defect.D03_Percent.ToCByteArray(3);
            msg.D03_QGRADE = defect.D03_QGRADE.ToCByteArray(1);

            msg.D04_Code = defect.D04_Code.ToCByteArray(3);
            msg.D04_Origin = defect.D04_Origin.ToCByteArray(3);
            msg.D04_Sid = defect.D04_Sid.ToCByteArray(1);
            msg.D04_Pos_W = defect.D04_Pos_W.ToCByteArray(1);
            msg.D04_Pos_L_Start = defect.D04_Pos_L_Start.ToCByteArray(4);
            msg.D04_Pos_L_End = defect.D04_Pos_L_End.ToCByteArray(4);
            msg.D04_Level = defect.D04_Level.ToCByteArray(1);
            msg.D04_Percent = defect.D04_Percent.ToCByteArray(3);
            msg.D04_QGRADE = defect.D04_QGRADE.ToCByteArray(1);

            msg.D05_Code = defect.D05_Code.ToCByteArray(3);
            msg.D05_Origin = defect.D05_Origin.ToCByteArray(3);
            msg.D05_Sid = defect.D05_Sid.ToCByteArray(1);
            msg.D05_Pos_W = defect.D05_Pos_W.ToCByteArray(1);
            msg.D05_Pos_L_Start = defect.D05_Pos_L_Start.ToCByteArray(4);
            msg.D05_Pos_L_End = defect.D05_Pos_L_End.ToCByteArray(4);
            msg.D05_Level = defect.D05_Level.ToCByteArray(1);
            msg.D05_Percent = defect.D05_Percent.ToCByteArray(3);
            msg.D05_QGRADE = defect.D05_QGRADE.ToCByteArray(1);

            msg.D06_Code = defect.D06_Code.ToCByteArray(3);
            msg.D06_Origin = defect.D06_Origin.ToCByteArray(3);
            msg.D06_Sid = defect.D06_Sid.ToCByteArray(1);
            msg.D06_Pos_W = defect.D06_Pos_W.ToCByteArray(1);
            msg.D06_Pos_L_Start = defect.D06_Pos_L_Start.ToCByteArray(4);
            msg.D06_Pos_L_End = defect.D06_Pos_L_End.ToCByteArray(4);
            msg.D06_Level = defect.D06_Level.ToCByteArray(1);
            msg.D06_Percent = defect.D06_Percent.ToCByteArray(3);
            msg.D06_QGRADE = defect.D06_QGRADE.ToCByteArray(1);

            msg.D07_Code = defect.D07_Code.ToCByteArray(3);
            msg.D07_Origin = defect.D07_Origin.ToCByteArray(3);
            msg.D07_Sid = defect.D07_Sid.ToCByteArray(1);
            msg.D07_Pos_W = defect.D07_Pos_W.ToCByteArray(1);
            msg.D07_Pos_L_Start = defect.D07_Pos_L_Start.ToCByteArray(4);
            msg.D07_Pos_L_End = defect.D07_Pos_L_End.ToCByteArray(4);
            msg.D07_Level = defect.D07_Level.ToCByteArray(1);
            msg.D07_Percent = defect.D07_Percent.ToCByteArray(3);
            msg.D07_QGRADE = defect.D07_QGRADE.ToCByteArray(1);

            msg.D08_Code = defect.D08_Code.ToCByteArray(3);
            msg.D08_Origin = defect.D08_Origin.ToCByteArray(3);
            msg.D08_Sid = defect.D08_Sid.ToCByteArray(1);
            msg.D08_Pos_W = defect.D08_Pos_W.ToCByteArray(1);
            msg.D08_Pos_L_Start = defect.D08_Pos_L_Start.ToCByteArray(4);
            msg.D08_Pos_L_End = defect.D08_Pos_L_End.ToCByteArray(4);
            msg.D08_Level = defect.D08_Level.ToCByteArray(1);
            msg.D08_Percent = defect.D08_Percent.ToCByteArray(3);
            msg.D08_QGRADE = defect.D08_QGRADE.ToCByteArray(1);

            msg.D09_Code = defect.D09_Code.ToCByteArray(3);
            msg.D09_Origin = defect.D09_Origin.ToCByteArray(3);
            msg.D09_Sid = defect.D09_Sid.ToCByteArray(1);
            msg.D09_Pos_W = defect.D09_Pos_W.ToCByteArray(1);
            msg.D09_Pos_L_Start = defect.D09_Pos_L_Start.ToCByteArray(4);
            msg.D09_Pos_L_End = defect.D09_Pos_L_End.ToCByteArray(4);
            msg.D09_Level = defect.D09_Level.ToCByteArray(1);
            msg.D09_Percent = defect.D09_Percent.ToCByteArray(3);
            msg.D09_QGRADE = defect.D09_QGRADE.ToCByteArray(1);

            msg.D10_Code = defect.D10_Code.ToCByteArray(3);
            msg.D10_Origin = defect.D10_Origin.ToCByteArray(3);
            msg.D10_Sid = defect.D10_Sid.ToCByteArray(1);
            msg.D10_Pos_W = defect.D10_Pos_W.ToCByteArray(1);
            msg.D10_Pos_L_Start = defect.D10_Pos_L_Start.ToCByteArray(4);
            msg.D10_Pos_L_End = defect.D10_Pos_L_End.ToCByteArray(4);
            msg.D10_Level = defect.D10_Level.ToCByteArray(1);
            msg.D10_Percent = defect.D10_Percent.ToCByteArray(3);
            msg.D10_QGRADE = defect.D10_QGRADE.ToCByteArray(1);

            msg.Winding_Direction = tblPDO.Winding_Direction.ToCByteArray(1);
            msg.Base_Surface = tblPDO.Base_Surface.ToCByteArray(1);
            msg.Inspector = tblPDO.Inspector.ToCByteArray(10);
            msg.Hold_Flag = tblPDO.Hold_Flag.ToCByteArray(1);
            msg.Hold_Cause_Code = tblPDO.Hold_Cause_Code.ToCByteArray(4);
            msg.Sample_Flag = tblPDO.Sample_Flag.ToCByteArray(1);
            msg.Trim_Flag = tblPDO.Trim_Flag.ToCByteArray(1);
            msg.Fixed_WT_Flag = tblPDO.Fixed_WT_Flag.ToCByteArray(1);
            msg.End_Flag = tblPDO.End_Flag.ToCByteArray(1);
            msg.Scrap_Flag = tblPDO.Scrap_Flag.ToCByteArray(1);
            msg.Sample_Frqn_Code = tblPDO.Sample_Frqn_Code.ToCByteArray(3);
            msg.No_Leader_Code = tblPDO.No_Leader_Code.ToCByteArray(1);
            msg.Surface_Accuracy_Code = tblPDO.Surface_Accuracy_Code.ToCByteArray(2);
            msg.Head_Off_Gauge = tblPDO.Head_Off_Gauge.ToNByteArray(5);
            msg.Tail_Off_Gauge = tblPDO.Tail_Off_Gauge.ToNByteArray(5);
            msg.Surface_Accu_Code_In = tblPDO.Surface_Accu_Code_In.ToCByteArray(2);
            msg.Surface_Accu_Code_Out = tblPDO.Surface_Accu_Code_Out.ToCByteArray(2);
            msg.Flip_Tag = tblPDO.Flip_Tag.ToCByteArray(1);
            msg.Process_Code = tblPDO.Process_Code.ToCByteArray(6);
            msg.Decoiler_Direction = tblPDO.Decoiler_Direction.ToCByteArray(1);
            msg.Recoiler_Actten_Avg = tblPDO.Recoiler_Actten_Avg.ToString().ToNByteArray(7);


            // 圓盤剪
            msg.Side_Trimmer_Gap = tblPDO.Avg_Side_Trimmer_Gap.GetPoint(2).ToNByteArray(3, 100);
            msg.Side_Trimmer_Lap = tblPDO.Avg_Side_Trimmer_Lap.GetPoint(2).ToNByteArray(4, 100);
            msg.Side_Trimmer_Width = tblPDO.Avg_Side_Trimmer_Width.GetPoint(1).ToNByteArray(5,10);
            msg.Trimming_OperateSide = tblPDO.Avg_Trimming_OperateSide.GetPoint(1).ToNByteArray(3,10);
            msg.Trimming_DriveSide = tblPDO.Avg_Trimming_DriveSide.GetPoint(1).ToNByteArray(3,10);

            return msg;


        }

        public static Msg_Res_For_Coil_Schedule ToCoilSchedRes(this ProResult proResult)
        {
            var coilSchedRes = new Msg_Res_For_Coil_Schedule()
            {
                Code = MMSSysDef.SndMsgCode.ResForCoilSched.ToCByteArray(6),
                FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1),
                SendWho = L2SystemDef.L2.ToCByteArray(2),
                RcvWho = MMSSysDef.MMS.ToCByteArray(2),
                Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8),
                Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6),
                Length = CalculateLength<L2MMSSnd.Msg_Res_For_Coil_Schedule>(),

                Requested_Coil_No = proResult.No.ToCByteArray(20),
                Process_Result = proResult.Result.ToCByteArray(1),
                Reject_Cause = proResult.RejectCause.ToCByteArray(80),

            };

            return coilSchedRes;


        }

        public static Msg_Res_For_Coil_PDI CoilPDIProRes(ProResult proResult)
        {
            var coilPDIRes = new Msg_Res_For_Coil_PDI()
            {
                Code = MMSSysDef.SndMsgCode.ResForCoilPDI.ToCByteArray(6),
                FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1),
                SendWho = L2SystemDef.L2.ToCByteArray(2),
                RcvWho = MMSSysDef.MMS.ToCByteArray(2),
                Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8),
                Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6),
                Length = CalculateLength<L2MMSSnd.Msg_Res_For_Coil_PDI>(),

                RequestedCoilNo = proResult.No.ToCByteArray(20),
                ProcessResult = proResult.Result.ToCByteArray(1),
                RejectCause = proResult.RejectCause.ToCByteArray(80),
            };

            return coilPDIRes;
        }

        public static Msg_Req_Coil_Schedule ReqCoilSchedule(string coilID)
        {

            var askSchedule = new Msg_Req_Coil_Schedule()
            {

                Code = MMSSysDef.SndMsgCode.ReqForCoilSched.ToCByteArray(6),
                FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1),
                SendWho = L2SystemDef.L2.ToCByteArray(2),
                RcvWho = MMSSysDef.MMS.ToCByteArray(2),
                Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8),
                Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6),
                Length = CalculateLength<L2MMSSnd.Msg_Req_Coil_Schedule>(),

                Requested_Coil_No = coilID.ToCByteArray(20)
            };

            return askSchedule;
        }

        public static Msg_Request_Coil_PDI ReqCoilPDI(string coilID)
        {
            var askCoilPDI = new Msg_Request_Coil_PDI
            {
                Code = MMSSysDef.SndMsgCode.ReqForPDI.ToCByteArray(6),
                FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1),
                SendWho = L2SystemDef.L2.ToCByteArray(2),
                RcvWho = MMSSysDef.MMS.ToCByteArray(2),
                Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8),
                Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6),
                Length = CalculateLength<L2MMSSnd.Msg_Request_Coil_PDI>(),

                Requested_Coil_No = coilID.ToCByteArray(20)
            };

            return askCoilPDI;
        }

        public static Msg_Res_For_PlanNo_Delete ResPlanNoDelete(ProResult proResult)
        {
            var resPlanNoDeleteResult = new Msg_Res_For_PlanNo_Delete
            {
                Code = MMSSysDef.SndMsgCode.MMSResDeletePlanNoResult.ToCByteArray(6),
                FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1),
                SendWho = L2SystemDef.L2.ToCByteArray(2),
                RcvWho = MMSSysDef.MMS.ToCByteArray(2),
                Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8),
                Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6),
                Length = CalculateLength<L2MMSSnd.Msg_Res_For_PlanNo_Delete>(),

                Plan_No = proResult.No.ToCByteArray(10),
                ProcessResult = proResult.Result.ToCByteArray(1),
                RejectCause = proResult.RejectCause.ToCByteArray(80)
            };

            return resPlanNoDeleteResult;
        }

        public static Msg_Equipment_Down_Result_Msg ToEquipmentDownResult(this LineFaultRecord value, string action)
        {
            var msg = new Msg_Equipment_Down_Result_Msg();

            msg.Code = MMSSysDef.SndMsgCode.EqDownResultCode.ToCByteArray(6);
            msg.FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1);
            msg.SendWho = L2SystemDef.L2.ToCByteArray(2);
            msg.RcvWho = MMSSysDef.MMS.ToCByteArray(2);
            msg.Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8);
            msg.Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6);
            msg.Length = CalculateLength<Msg_Equipment_Down_Result_Msg>();

            // 暫時只作新增 
            msg.Op_Flag = action.ToCByteArray(1);
            msg.Unit_Code = value.unit_code.ToCByteArray(4);
            // YYYYMMDD
            msg.Prod_Time = value.prod_time.ToString("yyyyMMdd").ToCByteArray(8);
            msg.Prod_Shift_No = value.prod_shift_no.ToCByteArray(1);
            msg.Prod_Shift_Group = value.prod_shift_group.ToCByteArray(1);
            // yyyyMMddHHmmss
            msg.Stop_Start_Time = value.stop_start_time.ToString("yyyyMMddHHmmss").ToCByteArray(14);
            // yyyyMMddHHmmss
            msg.Stop_End_Time = value.stop_end_time.ToString("yyyyMMddHHmmss").ToCByteArray(14);
            msg.Delay_Location = value.delay_location.ToCByteArray(6);
            msg.Delay_Location_Desc = value.delay_location_desc.ToCByteArray(30);
            msg.Stop_Elased_Time = value.stop_elased_timey.ToCByteArray(7);
            msg.Delay_Reason_Code = value.delay_reason_code.ToCByteArray(2);
            msg.Delay_Reason_Desc = value.delay_reason_desc.ToCByteArray(50);
            msg.Delay_Remark = value.delay_remark.ToCByteArray(200);
            msg.Resp_Depart_Delay_Time_m = value.resp_depart_delay_time_m.ToCByteArray(7);
            msg.Resp_Depart_Delay_Time_e = value.resp_depart_delay_time_e.ToCByteArray(7);
            msg.Resp_Depart_Delay_Time_c = value.resp_depart_delay_time_c.ToCByteArray(7);
            msg.Resp_Depart_Delay_Time_p = value.resp_depart_delay_time_p.ToCByteArray(7);
            msg.Resp_Depart_Delay_Time_u = value.resp_depart_delay_time_u.ToCByteArray(7);
            msg.Resp_Depart_Delay_Time_o = value.resp_depart_delay_time_o.ToCByteArray(7);
            msg.Resp_Depart_Delay_Time_r = value.resp_depart_delay_time_r.ToCByteArray(7);
            msg.Resp_Depart_Delay_Time_rs = value.resp_depart_delay_time_rs.ToCByteArray(7);
            msg.Deceleration_Cauese = value.deceleration_cause.ToCByteArray(200);
            msg.Deceleration_Code = value.deceleration_code.ToCByteArray(10);
            return msg;
        }

        // 上傳能源消耗訊息
        public static Msg_Energy_Consumption_Info UploadEnergyConsumptionInfo(CS15_Utility value)
        {
            var sndMsg = new Msg_Energy_Consumption_Info();
            foreach (FieldInfo fi in sndMsg.GetType().GetFields())
            {
                if (fi.FieldType == typeof(byte[]))
                {
                    var ma = fi.GetCustomAttribute<MarshalAsAttribute>();
                    fi.SetValue(sndMsg, "".ToNByteArray(ma.SizeConst));
                    
                }
            }
            sndMsg.Code = MMSSysDef.SndMsgCode.MMSEnergyConsumptionInfo.ToCByteArray(6);
            sndMsg.FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1);
            sndMsg.SendWho = L2SystemDef.L2.ToCByteArray(2);
            sndMsg.RcvWho = MMSSysDef.MMS.ToCByteArray(2);
            sndMsg.Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8);
            sndMsg.Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6);
            sndMsg.Length = CalculateLength<L2MMSSnd.Msg_Energy_Consumption_Info>();

            sndMsg.Shift_Date = value.Shift_Date.ToCByteArray(8);
            sndMsg.Shift_No = value.Shift_No.ToCByteArray(1);
            sndMsg.Group_No = value.Group_No.ToCByteArray(1);
            sndMsg.Unit_code = value.Unit_code.ToCByteArray(4);

            sndMsg.IDCW_1 = value.TWater.ToNByteArray(20, 1000000);
            sndMsg.CA_1 = value.TAir.ToNByteArray(20, 1000000);
        
            return sndMsg;
        }

        //
        public static Msg_Energy_Consumption_Info EnergyConsumptionInfo(L1L2Rcv.Msg_316_Utility_Data msg)
        {
            var enerfyConInfo = new L2MMSSnd.Msg_Energy_Consumption_Info();
            foreach (FieldInfo fi in enerfyConInfo.GetType().GetFields())
            {
                if (fi.FieldType == typeof(byte[]))
                {
                    var ma = fi.GetCustomAttribute<MarshalAsAttribute>();
                    fi.SetValue(enerfyConInfo, "".PadRight(ma.SizeConst).ToNByteArray(26));
               }
            }
            enerfyConInfo.Code = MMSSysDef.SndMsgCode.MMSEnergyConsumptionInfo.ToCByteArray(6);
            enerfyConInfo.FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1);
            enerfyConInfo.SendWho = L2SystemDef.L2.ToCByteArray(2);
            enerfyConInfo.RcvWho = MMSSysDef.MMS.ToCByteArray(2);
            enerfyConInfo.Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8);
            enerfyConInfo.Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6);
            enerfyConInfo.Length = CalculateLength<L2MMSSnd.Msg_Energy_Consumption_Info>();

            enerfyConInfo.IDCW_1 = msg.IndirectCollingWater.ToNByteArray(20,1000000);
            enerfyConInfo.CA_1 = msg.CompressedAir.ToNByteArray(20, 1000000);
            return enerfyConInfo;
        }

        public static Msg_Coil_Schedule_Delete CoilSchDelMsg(CS03_ScheduleChange message)
        {
            var coilSchDelete = new Msg_Coil_Schedule_Delete()
            {
                //Header
                Code = MMSSysDef.SndMsgCode.MMSCoilScheduleDelete.ToCByteArray(6),
                FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1),
                SendWho = L2SystemDef.L2.ToCByteArray(2),
                RcvWho = MMSSysDef.MMS.ToCByteArray(2),
                Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8),
                Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6),
                Length = CalculateLength<L2MMSSnd.Msg_Coil_Schedule_Delete>(),
                //Data
                EntryCoilNo = message.EntryCoilID.ToCByteArray(20),
                OperatorId = message.OperatorID.ToCByteArray(10),
                ReasonCode = message.ReasonCode.ToCByteArray(4)
            };
            return coilSchDelete;


        }


        public static void LoadDefectData(this Msg_Coil_Reject_Result coilRejectResut, IEnumerable<TBL_Coil_Defect> defectDatas)
        {
            //int cnt = 1;

            //foreach (TBL_Coil_Defect defectData in defectDatas)
            //{
            //    switch (cnt)
            //    {
            //        case 1:
            //            coilRejectResut.D1_Code = defectData.Defect_Code.ToString().ToCByteArray(3);
            //            coilRejectResut.D1_Origin = defectData.Defect_Code.ToCByteArray(3);
            //            coilRejectResut.D1_Sid = defectData.Defect_Side.ToCByteArray(1);
            //            coilRejectResut.D1_Pos_W = defectData.Defect_Position_WidthDirection.ToCByteArray(1);
            //            coilRejectResut.D1_Pos_L_Start = defectData.Defect_Position_LengthStartDirection.ToString().ToCByteArray(4);
            //            coilRejectResut.D1_Pos_L_End = defectData.Defect_Position_LengthEndDirection.ToString().ToCByteArray(4);
            //            coilRejectResut.D1_Level = DefectClass(defectData.Defect_Level).ToCByteArray(1);
            //            coilRejectResut.D1_Percent = defectData.Defect_Percent.ToCByteArray(3);
            //            coilRejectResut.D1_QGrade = string.Empty.ToCByteArray(1);
            //            break;

            //        case 2:
            //            coilRejectResut.D2_Code = defectData.Defect_Code.ToString().ToCByteArray(3);
            //            coilRejectResut.D2_Origin = defectData.Defect_Code.ToCByteArray(3);
            //            coilRejectResut.D2_Sid = defectData.Defect_Side.ToCByteArray(1);
            //            coilRejectResut.D2_Pos_W = defectData.Defect_Position_WidthDirection.ToCByteArray(1);
            //            coilRejectResut.D2_Pos_L_Start = defectData.Defect_Position_LengthStartDirection.ToString().ToCByteArray(4);
            //            coilRejectResut.D2_Pos_L_End = defectData.Defect_Position_LengthEndDirection.ToString().ToCByteArray(4);
            //            coilRejectResut.D2_Level = DefectClass(defectData.Defect_Level).ToCByteArray(1);
            //            coilRejectResut.D2_Percent = defectData.Defect_Percent.ToCByteArray(3);
            //            coilRejectResut.D2_QGrade = string.Empty.ToCByteArray(1);
            //            break;

            //        case 3:
            //            coilRejectResut.D3_Code = defectData.Defect_Code.ToString().ToCByteArray(3);
            //            coilRejectResut.D3_Origin = defectData.Defect_Code.ToCByteArray(3);
            //            coilRejectResut.D3_Sid = defectData.Defect_Side.ToCByteArray(1);
            //            coilRejectResut.D3_Pos_W = defectData.Defect_Position_WidthDirection.ToCByteArray(1);
            //            coilRejectResut.D3_Pos_L_Start = defectData.Defect_Position_LengthStartDirection.ToString().ToCByteArray(4);
            //            coilRejectResut.D3_Pos_L_End = defectData.Defect_Position_LengthEndDirection.ToString().ToCByteArray(4);
            //            coilRejectResut.D3_Level = DefectClass(defectData.Defect_Level).ToCByteArray(1);
            //            coilRejectResut.D3_Percent = defectData.Defect_Percent.ToCByteArray(3);
            //            coilRejectResut.D3_QGrade = string.Empty.ToCByteArray(1);
            //            break;

            //        case 4:
            //            coilRejectResut.D4_Code = defectData.Defect_Code.ToString().ToCByteArray(3);
            //            coilRejectResut.D4_Origin = defectData.Defect_Code.ToCByteArray(3);
            //            coilRejectResut.D4_Sid = defectData.Defect_Side.ToCByteArray(1);
            //            coilRejectResut.D4_Pos_W = defectData.Defect_Position_WidthDirection.ToCByteArray(1);
            //            coilRejectResut.D4_Pos_L_Start = defectData.Defect_Position_LengthStartDirection.ToString().ToCByteArray(4);
            //            coilRejectResut.D4_Pos_L_End = defectData.Defect_Position_LengthEndDirection.ToString().ToCByteArray(4);
            //            coilRejectResut.D4_Level = DefectClass(defectData.Defect_Level).ToCByteArray(1);
            //            coilRejectResut.D4_Percent = defectData.Defect_Percent.ToCByteArray(3);
            //            coilRejectResut.D4_QGrade = string.Empty.ToCByteArray(1);
            //            break;

            //        case 5:
            //            coilRejectResut.D5_Code = defectData.Defect_Code.ToString().ToCByteArray(3);
            //            coilRejectResut.D5_Origin = defectData.Defect_Code.ToCByteArray(3);
            //            coilRejectResut.D5_Sid = defectData.Defect_Side.ToCByteArray(1);
            //            coilRejectResut.D5_Pos_W = defectData.Defect_Position_WidthDirection.ToCByteArray(1);
            //            coilRejectResut.D5_Pos_L_Start = defectData.Defect_Position_LengthStartDirection.ToString().ToCByteArray(4);
            //            coilRejectResut.D5_Pos_L_End = defectData.Defect_Position_LengthEndDirection.ToString().ToCByteArray(4);
            //            coilRejectResut.D5_Level = DefectClass(defectData.Defect_Level).ToCByteArray(1);
            //            coilRejectResut.D5_Percent = defectData.Defect_Percent.ToCByteArray(3);
            //            coilRejectResut.D5_QGrade = string.Empty.ToCByteArray(1);


            //            break;

            //        case 6:
            //            coilRejectResut.D6_Code = defectData.Defect_Code.ToString().ToCByteArray(3);
            //            coilRejectResut.D6_Origin = defectData.Defect_Code.ToCByteArray(3);
            //            coilRejectResut.D6_Sid = defectData.Defect_Side.ToCByteArray(1);
            //            coilRejectResut.D6_Pos_W = defectData.Defect_Position_WidthDirection.ToCByteArray(1);
            //            coilRejectResut.D6_Pos_L_Start = defectData.Defect_Position_LengthStartDirection.ToString().ToCByteArray(4);                       
            //            coilRejectResut.D6_Pos_L_End = defectData.Defect_Position_LengthEndDirection.ToString().ToCByteArray(4);
            //            coilRejectResut.D6_Level = DefectClass(defectData.Defect_Level).ToCByteArray(1);
            //            coilRejectResut.D6_Percent = defectData.Defect_Percent.ToCByteArray(3);
            //            coilRejectResut.D6_QGrade = string.Empty.ToCByteArray(1);

            //            break;

            //        case 7:
            //            coilRejectResut.D7_Code = defectData.Defect_Code.ToString().ToCByteArray(3);
            //            coilRejectResut.D7_Origin = defectData.Defect_Code.ToCByteArray(3);
            //            coilRejectResut.D7_Sid = defectData.Defect_Side.ToCByteArray(1);
            //            coilRejectResut.D7_Pos_W = defectData.Defect_Position_WidthDirection.ToCByteArray(1);
            //            coilRejectResut.D7_Pos_L_Start = defectData.Defect_Position_LengthStartDirection.ToString().ToCByteArray(4);
            //            coilRejectResut.D7_Pos_L_End = defectData.Defect_Position_LengthEndDirection.ToString().ToCByteArray(4);
            //            coilRejectResut.D7_Level = DefectClass(defectData.Defect_Level).ToCByteArray(1);
            //            coilRejectResut.D7_Percent = defectData.Defect_Percent.ToCByteArray(3);
            //            coilRejectResut.D7_QGrade = string.Empty.ToCByteArray(1);


            //            break;

            //        case 8:

            //            coilRejectResut.D8_Code = defectData.Defect_Code.ToString().ToCByteArray(3);
            //            coilRejectResut.D8_Origin = defectData.Defect_Code.ToCByteArray(3);
            //            coilRejectResut.D8_Sid = defectData.Defect_Side.ToCByteArray(1);
            //            coilRejectResut.D8_Pos_W = defectData.Defect_Position_WidthDirection.ToCByteArray(1);
            //            coilRejectResut.D8_Pos_L_Start = defectData.Defect_Position_LengthStartDirection.ToString().ToCByteArray(4);
            //            coilRejectResut.D8_Pos_L_End = defectData.Defect_Position_LengthEndDirection.ToString().ToCByteArray(4);
            //            coilRejectResut.D8_Level = DefectClass(defectData.Defect_Level).ToCByteArray(1);
            //            coilRejectResut.D8_Percent = defectData.Defect_Percent.ToCByteArray(3);
            //            coilRejectResut.D8_QGrade = string.Empty.ToCByteArray(1);

            //            break;

            //        case 9:

            //            coilRejectResut.D9_Code = defectData.Defect_Code.ToString().ToCByteArray(3);
            //            coilRejectResut.D9_Origin = defectData.Defect_Code.ToCByteArray(3);
            //            coilRejectResut.D9_Sid = defectData.Defect_Side.ToCByteArray(1);
            //            coilRejectResut.D9_Pos_W = defectData.Defect_Position_WidthDirection.ToCByteArray(1);
            //            coilRejectResut.D9_Pos_L_Start = defectData.Defect_Position_LengthStartDirection.ToString().ToCByteArray(4);
            //            coilRejectResut.D9_Pos_L_End = defectData.Defect_Position_LengthEndDirection.ToString().ToCByteArray(4);
            //            coilRejectResut.D9_Level = DefectClass(defectData.Defect_Level).ToCByteArray(1);
            //            coilRejectResut.D9_Percent = defectData.Defect_Percent.ToCByteArray(3);
            //            coilRejectResut.D9_QGrade = string.Empty.ToCByteArray(1);

            //            break;

            //        case 10:

            //            coilRejectResut.D10_Code = defectData.Defect_Code.ToString().ToCByteArray(3);
            //            coilRejectResut.D10_Origin = defectData.Defect_Code.ToCByteArray(3);
            //            coilRejectResut.D10_Sid = defectData.Defect_Side.ToCByteArray(1);
            //            coilRejectResut.D10_Pos_W = defectData.Defect_Position_WidthDirection.ToCByteArray(1);
            //            coilRejectResut.D10_Pos_L_Start = defectData.Defect_Position_LengthStartDirection.ToString().ToCByteArray(4);
            //            coilRejectResut.D10_Pos_L_End = defectData.Defect_Position_LengthEndDirection.ToString().ToCByteArray(4);
            //            coilRejectResut.D10_Level = DefectClass(defectData.Defect_Level).ToCByteArray(1);
            //            coilRejectResut.D10_Percent = defectData.Defect_Percent.ToCByteArray(3);
            //            coilRejectResut.D10_QGrade = string.Empty.ToCByteArray(1);

            //            break;
            //    }



            //    cnt++;
            //}
        }

        public static void LoadEmptyDefectData(this Msg_Coil_Reject_Result coilRejectResut)
        {
            coilRejectResut.D1_Code = string.Empty.ToCByteArray(3);
            coilRejectResut.D1_Origin = string.Empty.ToCByteArray(3);
            coilRejectResut.D1_Sid = string.Empty.ToCByteArray(1);
            coilRejectResut.D1_Pos_W = string.Empty.ToCByteArray(1);
            coilRejectResut.D1_Pos_L_Start = string.Empty.ToCByteArray(4);
            coilRejectResut.D1_Pos_L_End = string.Empty.ToCByteArray(4);
            coilRejectResut.D1_Level = string.Empty.ToCByteArray(1);
            coilRejectResut.D1_Percent = string.Empty.ToCByteArray(3);
            coilRejectResut.D1_QGrade = string.Empty.ToCByteArray(1);

            coilRejectResut.D2_Code = string.Empty.ToCByteArray(3);
            coilRejectResut.D2_Origin = string.Empty.ToCByteArray(3);
            coilRejectResut.D2_Sid = string.Empty.ToCByteArray(1);
            coilRejectResut.D2_Pos_W = string.Empty.ToCByteArray(1);
            coilRejectResut.D2_Pos_L_Start = string.Empty.ToCByteArray(4);
            coilRejectResut.D2_Pos_L_End = string.Empty.ToCByteArray(4);
            coilRejectResut.D2_Level = string.Empty.ToCByteArray(1);
            coilRejectResut.D2_Percent = string.Empty.ToCByteArray(3);
            coilRejectResut.D2_QGrade = string.Empty.ToCByteArray(1);

            coilRejectResut.D3_Code = string.Empty.ToCByteArray(3);
            coilRejectResut.D3_Origin = string.Empty.ToCByteArray(3);
            coilRejectResut.D3_Sid = string.Empty.ToCByteArray(1);
            coilRejectResut.D3_Pos_W = string.Empty.ToCByteArray(1);
            coilRejectResut.D3_Pos_L_Start = string.Empty.ToCByteArray(4);
            coilRejectResut.D3_Pos_L_End = string.Empty.ToCByteArray(4);
            coilRejectResut.D3_Level = string.Empty.ToCByteArray(1);
            coilRejectResut.D3_Percent = string.Empty.ToCByteArray(3);
            coilRejectResut.D3_QGrade = string.Empty.ToCByteArray(1);

            coilRejectResut.D4_Code = string.Empty.ToCByteArray(3);
            coilRejectResut.D4_Origin = string.Empty.ToCByteArray(3);
            coilRejectResut.D4_Sid = string.Empty.ToCByteArray(1);
            coilRejectResut.D4_Pos_W = string.Empty.ToCByteArray(1);
            coilRejectResut.D4_Pos_L_Start = string.Empty.ToCByteArray(4);
            coilRejectResut.D4_Pos_L_End = string.Empty.ToCByteArray(4);
            coilRejectResut.D4_Level = string.Empty.ToCByteArray(1);
            coilRejectResut.D4_Percent = string.Empty.ToCByteArray(3);
            coilRejectResut.D4_QGrade = string.Empty.ToCByteArray(1);

            coilRejectResut.D5_Code = string.Empty.ToCByteArray(3);
            coilRejectResut.D5_Origin = string.Empty.ToCByteArray(3);
            coilRejectResut.D5_Sid = string.Empty.ToCByteArray(1);
            coilRejectResut.D5_Pos_W = string.Empty.ToCByteArray(1);
            coilRejectResut.D5_Pos_L_Start = string.Empty.ToCByteArray(4);
            coilRejectResut.D5_Pos_L_End = string.Empty.ToCByteArray(4);
            coilRejectResut.D5_Level = string.Empty.ToCByteArray(1);
            coilRejectResut.D5_Percent = string.Empty.ToCByteArray(3);
            coilRejectResut.D5_QGrade = string.Empty.ToCByteArray(1);

            coilRejectResut.D6_Code = string.Empty.ToCByteArray(3);
            coilRejectResut.D6_Origin = string.Empty.ToCByteArray(3);
            coilRejectResut.D6_Sid = string.Empty.ToCByteArray(1);
            coilRejectResut.D6_Pos_W = string.Empty.ToCByteArray(1);
            coilRejectResut.D6_Pos_L_Start = string.Empty.ToCByteArray(4);
            coilRejectResut.D6_Pos_L_End = string.Empty.ToCByteArray(4);
            coilRejectResut.D6_Level = string.Empty.ToCByteArray(1);
            coilRejectResut.D6_Percent = string.Empty.ToCByteArray(3);
            coilRejectResut.D6_QGrade = string.Empty.ToCByteArray(1);

            coilRejectResut.D7_Code = string.Empty.ToCByteArray(3);
            coilRejectResut.D7_Origin = string.Empty.ToCByteArray(3);
            coilRejectResut.D7_Sid = string.Empty.ToCByteArray(1);
            coilRejectResut.D7_Pos_W = string.Empty.ToCByteArray(1);
            coilRejectResut.D7_Pos_L_Start = string.Empty.ToCByteArray(4);
            coilRejectResut.D7_Pos_L_End = string.Empty.ToCByteArray(4);
            coilRejectResut.D7_Level = string.Empty.ToCByteArray(1);
            coilRejectResut.D7_Percent = string.Empty.ToCByteArray(3);
            coilRejectResut.D7_QGrade = string.Empty.ToCByteArray(1);

            coilRejectResut.D8_Code = string.Empty.ToCByteArray(3);
            coilRejectResut.D8_Origin = string.Empty.ToCByteArray(3);
            coilRejectResut.D8_Sid = string.Empty.ToCByteArray(1);
            coilRejectResut.D8_Pos_W = string.Empty.ToCByteArray(1);
            coilRejectResut.D8_Pos_L_Start = string.Empty.ToCByteArray(4);
            coilRejectResut.D8_Pos_L_End = string.Empty.ToCByteArray(4);
            coilRejectResut.D8_Level = string.Empty.ToCByteArray(1);
            coilRejectResut.D8_Percent = string.Empty.ToCByteArray(3);
            coilRejectResut.D8_QGrade = string.Empty.ToCByteArray(1);

            coilRejectResut.D9_Code = string.Empty.ToCByteArray(3);
            coilRejectResut.D9_Origin = string.Empty.ToCByteArray(3);
            coilRejectResut.D9_Sid = string.Empty.ToCByteArray(1);
            coilRejectResut.D9_Pos_W = string.Empty.ToCByteArray(1);
            coilRejectResut.D9_Pos_L_Start = string.Empty.ToCByteArray(4);
            coilRejectResut.D9_Pos_L_End = string.Empty.ToCByteArray(4);
            coilRejectResut.D9_Level = string.Empty.ToCByteArray(1);
            coilRejectResut.D9_Percent = string.Empty.ToCByteArray(3);
            coilRejectResut.D9_QGrade = string.Empty.ToCByteArray(1);

            coilRejectResut.D10_Code = string.Empty.ToCByteArray(3);
            coilRejectResut.D10_Origin = string.Empty.ToCByteArray(3);
            coilRejectResut.D10_Sid = string.Empty.ToCByteArray(1);
            coilRejectResut.D10_Pos_W = string.Empty.ToCByteArray(1);
            coilRejectResut.D10_Pos_L_Start = string.Empty.ToCByteArray(4);
            coilRejectResut.D10_Pos_L_End = string.Empty.ToCByteArray(4);
            coilRejectResut.D10_Level = string.Empty.ToCByteArray(1);
            coilRejectResut.D10_Percent = string.Empty.ToCByteArray(3);
            coilRejectResut.D10_QGrade = string.Empty.ToCByteArray(1);

            

        }


        private static string DefectClass(string defectLevel)
        {
            if (defectLevel.Equals(PlcSysDef.Cmd.DefectGradeA) || defectLevel.Equals(PlcSysDef.Cmd.DefectGradeB))
                return MMSSysDef.Cmd.DefectClassL;

            if (defectLevel.Equals(PlcSysDef.Cmd.DefectGradeC))
                return MMSSysDef.Cmd.DefectClassM;

            if (defectLevel.Equals(PlcSysDef.Cmd.DefectGradeD))
                return MMSSysDef.Cmd.DefectClassH;

            if (defectLevel.Equals(PlcSysDef.Cmd.DefectGradeE))
                return MMSSysDef.Cmd.DefectClassS;

            return string.Empty;
        }


        private static byte[] CalculateLength<T>()
        {
            // +1 為結尾符號長度
            return (Marshal.SizeOf<T>() + MMSSysDef.EndTagLength).ToString("0000").ToNByteArray(MMSSysDef.MsgLenLength);
        }
    }
}
