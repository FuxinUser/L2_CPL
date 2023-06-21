using AutoMapper;
using Core.Define;
using Core.Util;
using DBService.Level25Repository.L2L25_CoilPDI;
using static DBService.Repository.CutReocrd.CoilCutRecordTempEntity;
using static DBService.Repository.DefectData.CoilDefectDataEntity;
using static DBService.Repository.LineStatus.ProcessDataEntity;
using static DBService.Repository.PDI.CoilPDIEntity;
using static DBService.Repository.PDO.PDOUploadedReplyEntity;
using static DBService.Repository.Sample.SampleEntity;
using static DBService.Repository.UnmountRecord.UnmountRecordEntity;
using static DBService.Repository.Utility.UtilityEntity;
using static DBService.Repository.WieldRecord.WeldRecordEntity;
using static MsgStruct.L1L2Rcv;
using static MsgStruct.MMSL2Rcv;

namespace MsgConvert.ObjectMapping
{
    /// <summary>
    /// 物件映至:Message Model <-> DBModel
    /// </summary>
    public class MsgMappingDBModel : Profile
    {

        public MsgMappingDBModel()
        {

            MappingL1301EnCoilCutToTblCutRecordTemp();
            MappingL1302CoilWeldToTblWeldRecords();
            MappingL1311ExCoilCutToTblSample();
            MappingL1311ExitCoilCutToTblCutRecordTemp();
            MappingL1313SpdTenToTblProcessData();
            MappingL1316UtilityToTblUtility();
            MappingL1311ExitCoilCutToTblUmountRecord();
            MappingL1311ExitCoilCutToTblSample();

            MappingMsgPDIToTblPDI();
            MappingPDIToTblDefectMap();
            MappingMsgToPdoUploadedReply();

        }

        #region -- L1 -> DB Model --

        private void MappingL1316UtilityToTblUtility()
        {
            CreateMap<Msg_316_Utility_Data, TBL_Utility>();
        }

        private void MappingL1311ExCoilCutToTblSample()
        {
            CreateMap<Msg_311_ExCoilCut, TBL_Sample>();

        }

        private void MappingL1301EnCoilCutToTblCutRecordTemp()
        {
            CreateMap<Msg_301_EnCoilCut, TBL_Coil_CutRecord_Temp>()
                 .ForMember(dest => dest.Coil_ID, opt => opt.MapFrom(src => src.CoilID.ToStr()))
                 .ForMember(dest => dest.In_Coil_ID, opt => opt.MapFrom(src => src.CoilID.ToStr()))
                 .ForMember(dest => dest.CutDevice, opt => opt.MapFrom(src => DeviceParaDef.EntryShear))
                 .ForMember(dest => dest.CutTime, opt => opt.MapFrom(src => src.DateTime));

        }

        private void MappingL1302CoilWeldToTblWeldRecords()
        {
            CreateMap<Msg_302_CoilWeld, TBL_WeldRecords>()
                 .ForMember(dest => dest.Coil_ID, opt => opt.MapFrom(src => src.CoilID.ToStr()));
        }

        private void MappingL1313SpdTenToTblProcessData()
        {
            CreateMap<Msg_313_SpdTen, TBL_ProcessData>()
                   .ForMember(dest => dest.ReceiveTime, opt => opt.MapFrom(src => src.DateTime))
                   .ForMember(dest => dest.LINE_Speed_Actual, opt => opt.MapFrom(src => src.ActualSpeed))

                   .ForMember(dest => dest.POR_Tension_Set, opt => opt.MapFrom(src => src.TenRefUnc))
                   .ForMember(dest => dest.POR_Tension_Actual, opt => opt.MapFrom(src => src.TenActUnc))
                   .ForMember(dest => dest.POR_Current_Actual, opt => opt.MapFrom(src => src.RecMotorActCurrent))

                   .ForMember(dest => dest.TR_Tension_Set, opt => opt.MapFrom(src => src.TenRefRec))
                   .ForMember(dest => dest.TR_Tension_Actual, opt => opt.MapFrom(src => src.TenActRec))
                   .ForMember(dest => dest.TR_Current_Actual, opt => opt.MapFrom(src => src.UncMotorActCurrent))

                   .ForMember(dest => dest.WELD_Current_Actual_Front, opt => opt.MapFrom(src => src.WeldActCurrentFront))
                   .ForMember(dest => dest.WELD_Speed_Actual, opt => opt.MapFrom(src => src.WeldActSpd))
                   .ForMember(dest => dest.WELD_Current_Actual_Rear, opt => opt.MapFrom(src => src.WeldActCurrentRear))
                   .ForMember(dest => dest.WELD_PlanishWheelForce_Actual, opt => opt.MapFrom(src => src.WeldActPlanishRollForce))
                   .ForMember(dest => dest.WELD_Temperture, opt => opt.MapFrom(src => src.WeldTemperature))
                   .ForMember(dest => dest.BuildTension, opt => opt.MapFrom(src => src.BuildTension));

        }

        private void MappingL1311ExitCoilCutToTblCutRecordTemp()
        {
            CreateMap<Msg_311_ExCoilCut, TBL_Coil_CutRecord_Temp>()
                .ForMember(dest => dest.Coil_ID, opt => opt.MapFrom(src => src.CoilID.ToStr()))
                .ForMember(dest => dest.In_Coil_ID, opt => opt.MapFrom(src => src.CoilID.ToStr()))
                .ForMember(dest => dest.CutDevice, opt => opt.MapFrom(src => DeviceParaDef.ExitShear))
                .ForMember(dest => dest.CutLength, opt => opt.MapFrom(src => src.CutLength))
                .ForMember(dest => dest.CutTime, opt => opt.MapFrom(src => src.DateTime))
                .ForMember(dest => dest.Coil_OutDiam, opt => opt.MapFrom(src => src.DiamRec.ToString()))
                .ForMember(dest => dest.Coil_Length, opt => opt.MapFrom(src => src.LengthRec.ToString()))
                .ForMember(dest => dest.Coil_CalcWeight, opt => opt.MapFrom(src => src.CalculateWeightRec))
                .ForMember(dest => dest.Coil_PaperFlag, opt => opt.MapFrom(src => src.PUWFlag.ToString()));

        }

        private void MappingL1311ExitCoilCutToTblUmountRecord()
        {
            // 目前詢問 Width跟內徑 透過311無法知道, Weight則是外部給
            CreateMap<Msg_311_ExCoilCut, TBL_UnmountRecord>()
                 .ForMember(dest => dest.Coil_ID, opt => opt.MapFrom(src => src.CoilID.ToStr()))
                 .ForMember(dest => dest.CoilWeight, opt => opt.MapFrom(src => src.CalculateWeightRec))
                 .ForMember(dest => dest.CoilLength, opt => opt.MapFrom(src => src.CutLength))
                 .ForMember(dest => dest.Diameter, opt => opt.MapFrom(src => src.DiamRec));
        }

        private void MappingL1311ExitCoilCutToTblSample()
        {
            CreateMap<Msg_311_ExCoilCut, TBL_Sample>()
                .ForMember(dest => dest.Sample_Length, opt => opt.MapFrom(src => src.CutLength))
                .ForMember(dest => dest.Calculate_Weight, opt => opt.MapFrom(src => src.CalculateWeightRec))
                .ForMember(dest => dest.Outer_Diameter, opt => opt.MapFrom(src => src.DiamRec))
                .ForMember(dest => dest.Paper_Unwinder, opt => opt.MapFrom(src => src.PUWFlag.ToString()))
                .ForMember(dest => dest.SampleTime, opt => opt.MapFrom(src => src.DateTime));
        }
        #endregion

        #region -- MMS -> DB Model -- 
        // Msg PDI -> L3L2_PDI
        private void MappingMsgPDIToTblPDI()
        {
            CreateMap<Msg_PDI, TBL_PDI>()
                 .ForMember(dest => dest.Plan_No, opt => opt.MapFrom(src => src.Plan_No.ToStr()))
                 .ForMember(dest => dest.Mat_Seq_No, opt => opt.MapFrom(src => src.Mat_Seq_No.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.Plan_Sort, opt => opt.MapFrom(src => src.Plan_Sort.ToStr()))
                 .ForMember(dest => dest.Entry_Coil_ID, opt => opt.MapFrom(src => src.Entry_Coil_No.ToStr()))
                 .ForMember(dest => dest.Entry_Coil_Thick, opt => opt.MapFrom(src => src.Entry_Coil_Thick.ToStr().ToNullable<float>() / 1000 ?? 0))
                 .ForMember(dest => dest.Entry_Coil_Width, opt => opt.MapFrom(src => src.Entry_Coil_Width.ToStr().ToNullable<float>() / 10 ?? 0))
                 .ForMember(dest => dest.Entry_Coil_Weight, opt => opt.MapFrom(src => src.Entry_Coil_Weight.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.Entry_Coil_Length, opt => opt.MapFrom(src => src.Entry_Coil_Length.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.Entry_Coil_Inner, opt => opt.MapFrom(src => src.Entry_Coil_Inner.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.Entry_Coil_Dcos, opt => opt.MapFrom(src => src.Entry_Coil_Dcos.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.Sleeve_Type_Code, opt => opt.MapFrom(src => src.Sleeve_Type_Code.ToStr()))
                 .ForMember(dest => dest.Sleeve_diamter, opt => opt.MapFrom(src => src.Sleeve_diamter.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.Paper_Req_Code, opt => opt.MapFrom(src => src.Paper_Req_Code.ToStr()))
                 .ForMember(dest => dest.Paper_Code, opt => opt.MapFrom(src => src.Paper_Code.ToStr()))
                 .ForMember(dest => dest.Head_Paper_Length, opt => opt.MapFrom(src => src.Head_Paper_Length.ToStr().ToNullable<float>() ?? 0))
                 .ForMember(dest => dest.Head_Paper_Width, opt => opt.MapFrom(src => src.Head_Paper_Width.ToStr().ToNullable<float>() / 10 ?? 0))
                 .ForMember(dest => dest.Tail_Paper_Length, opt => opt.MapFrom(src => src.Tail_Paper_Length.ToStr().ToNullable<float>() ?? 0))
                 .ForMember(dest => dest.Tail_Paper_Width, opt => opt.MapFrom(src => src.Tail_Paper_Width.ToStr().ToNullable<float>() / 10 ?? 0))
                 .ForMember(dest => dest.Ts_Stand_Max, opt => opt.MapFrom(src => src.Ts_Stand_Max.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.Ts_Stand_Min, opt => opt.MapFrom(src => src.Ts_Stand_Min.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.St_No, opt => opt.MapFrom(src => src.St_No.ToStr()))
                 .ForMember(dest => dest.Density, opt => opt.MapFrom(src => src.Density.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.REPAIR_TYPE, opt => opt.MapFrom(src => src.REPAIR_TYPE.ToStr()))
                 .ForMember(dest => dest.Surface_Finishing_Code, opt => opt.MapFrom(src => src.Surface_Finishing_Code.ToStr()))
                 .ForMember(dest => dest.Surface_Accuracy, opt => opt.MapFrom(src => src.Surface_Accuracy.ToStr()))
                 .ForMember(dest => dest.Base_Surface, opt => opt.MapFrom(src => src.Base_Surface.ToStr()))
                 .ForMember(dest => dest.Uncoiler_Direction, opt => opt.MapFrom(src => src.Uncoiler_Direction.ToStr()))
                 .ForMember(dest => dest.Out_Coil_ID, opt => opt.MapFrom(src => src.Out_Mat_No.ToStr()))
                 .ForMember(dest => dest.Out_Paper_Req_Code, opt => opt.MapFrom(src => src.Out_Paper_Req_Code.ToStr()))
                 .ForMember(dest => dest.Out_Paper_Code, opt => opt.MapFrom(src => src.Out_Paper_Code.ToStr()))
                 .ForMember(dest => dest.Out_Sleeve_Diamter, opt => opt.MapFrom(src => src.Out_Sleeve_Diamter.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.Out_Sleeve_Type_Code, opt => opt.MapFrom(src => src.Out_Sleeve_Type_Code.ToStr()))
                 .ForMember(dest => dest.Out_Strap_Num, opt => opt.MapFrom(src => src.Out_Strap_Num.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.Leader_Flag, opt => opt.MapFrom(src => src.Leader_Flag.ToStr()))
                 .ForMember(dest => dest.Sample_Flag, opt => opt.MapFrom(src => src.Samp_Flag.ToStr()))
                 .ForMember(dest => dest.Sample_Frqn_Code, opt => opt.MapFrom(src => src.Sample_Frqn_Code.ToStr()))
                 .ForMember(dest => dest.Sample_Lot_No, opt => opt.MapFrom(src => src.Sample_Lot_No.ToStr()))
                 .ForMember(dest => dest.Coil_Origin, opt => opt.MapFrom(src => src.Coil_Origin.ToStr()))
                 .ForMember(dest => dest.Wholebacklog_Code, opt => opt.MapFrom(src => src.Wholebacklog_Code.ToStr()))
                 .ForMember(dest => dest.Next_Wholebacklog_Code, opt => opt.MapFrom(src => src.Next_Wholebacklog_Code.ToStr()))
                 .ForMember(dest => dest.Trim_Flag, opt => opt.MapFrom(src => src.Trim_Flag.ToStr()))
                 .ForMember(dest => dest.Out_Coil_Width, opt => opt.MapFrom(src => src.Out_Mat_Width.ToStr().ToNullable<float>() / 10 ?? 0))
                 .ForMember(dest => dest.Out_Coil_Width_Max, opt => opt.MapFrom(src => src.Out_Mat_Width_Max.ToStr().ToNullable<float>() / 10 ?? 0))
                 .ForMember(dest => dest.Out_Coil_Width_Min, opt => opt.MapFrom(src => src.Out_Mat_Width_Min.ToStr().ToNullable<float>() / 10 ?? 0))
                 .ForMember(dest => dest.Out_Coil_Thickness, opt => opt.MapFrom(src => src.Out_Mat_Thickness.ToStr().ToNullable<float>() / 1000 ?? 0))
                 .ForMember(dest => dest.Out_Coil_Inner, opt => opt.MapFrom(src => src.Out_Coil_Inner.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.Order_No, opt => opt.MapFrom(src => src.Order_No.ToStr()))
                 .ForMember(dest => dest.Order_Wt_Max, opt => opt.MapFrom(src => src.Order_Wt_Max.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.Order_Wt_Min, opt => opt.MapFrom(src => src.Order_Wt_Min.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.Order_Wt, opt => opt.MapFrom(src => src.Order_Wt.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.Dividing_Flag, opt => opt.MapFrom(src => src.Dividing_Flag.ToStr()))
                 .ForMember(dest => dest.Dividing_Num, opt => opt.MapFrom(src => src.Dividing_Num.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.Orderwt_1, opt => opt.MapFrom(src => src.Orderwt_1.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.Orderwt_2, opt => opt.MapFrom(src => src.Orderwt_2.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.Orderwt_3, opt => opt.MapFrom(src => src.Orderwt_3.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.Orderwt_4, opt => opt.MapFrom(src => src.Orderwt_4.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.Orderwt_5, opt => opt.MapFrom(src => src.Orderwt_5.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.Orderwt_6, opt => opt.MapFrom(src => src.Orderwt_6.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.Order_No_1, opt => opt.MapFrom(src => src.Order_No_1.ToStr()))
                 .ForMember(dest => dest.Order_No_2, opt => opt.MapFrom(src => src.Order_No_2.ToStr()))
                 .ForMember(dest => dest.Order_No_3, opt => opt.MapFrom(src => src.Order_No_3.ToStr()))
                 .ForMember(dest => dest.Order_No_4, opt => opt.MapFrom(src => src.Order_No_4.ToStr()))
                 .ForMember(dest => dest.Order_No_5, opt => opt.MapFrom(src => src.Order_No_5.ToStr()))
                 .ForMember(dest => dest.Order_No_6, opt => opt.MapFrom(src => src.Order_No_6.ToStr()))
                 .ForMember(dest => dest.Test_Plan_No, opt => opt.MapFrom(src => src.Test_Plan_No.ToStr()))
                 .ForMember(dest => dest.Qc_Remark, opt => opt.MapFrom(src => src.Qc_Remark.ToStr()))
                 .ForMember(dest => dest.Head_Off_Gauge, opt => opt.MapFrom(src => src.Head_Off_Gauge.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.Tail_Off_Gauge, opt => opt.MapFrom(src => src.Tail_Off_Gauge.ToStr().ToNullable<int>() ?? 0))
                 .ForMember(dest => dest.Surface_Accu_Code_In, opt => opt.MapFrom(src => src.Surface_Accu_Code_In.ToStr()))
                 .ForMember(dest => dest.Surface_Accu_Code_Out, opt => opt.MapFrom(src => src.Surface_Accu_Code_Out.ToStr()))
                 .ForMember(dest => dest.Sg_Sign, opt => opt.MapFrom(src => src.Sg_Sign.ToStr()))
                 .ForMember(dest => dest.Process_Code, opt => opt.MapFrom(src => src.Process_Code.ToStr()))
                 .ForMember(dest => dest.CustomerCode, opt => opt.MapFrom(src => src.Customer_No.ToStr()))
                 .ForMember(dest => dest.CustomerName_E, opt => opt.MapFrom(src => src.Customer_Name_E.ToStr()))
                 .ForMember(dest => dest.CustomerName_C, opt => opt.MapFrom(src => src.Customer_Name_C.ToStr()))
                 .ForMember(dest => dest.Surface_Acc_Desc, opt => opt.MapFrom(src => src.Surface_Acc_Desc.ToStr()))
                 .ForMember(dest => dest.Surface_Accuracy_Desc, opt => opt.MapFrom(src => src.Surface_Acc_Desc.ToStr()))
                 .ForMember(dest => dest.Surface_Acc_Desc_In, opt => opt.MapFrom(src => src.Surface_Acc_Desc_In.ToStr()))
                 .ForMember(dest => dest.Surface_Acc_Desc_Out, opt => opt.MapFrom(src => src.Surface_Acc_Desc_Out.ToStr()));
        }

        //Msg_PDI -> TBL_Coil_Defect
        private void MappingPDIToTblDefectMap()
        {
            CreateMap<Msg_PDI, TBL_Coil_Defect>()
            .ForMember(dest => dest.Plan_No, opt => opt.MapFrom(src => src.Plan_No.ToStr()))
            .ForMember(dest => dest.Coil_ID, opt => opt.MapFrom(src => src.Out_Mat_No.ToStr()))
            .ForMember(dest => dest.Entry_Coil_ID, opt => opt.MapFrom(src => src.Entry_Coil_No.ToStr()))

            .ForMember(dest => dest.D01_Code, opt => opt.MapFrom(src => src.D01_Code.ToStr()))
            .ForMember(dest => dest.D01_Origin, opt => opt.MapFrom(src => src.D01_Origin.ToStr()))
            .ForMember(dest => dest.D01_Sid, opt => opt.MapFrom(src => src.D01_Sid.ToStr()))
            .ForMember(dest => dest.D01_Pos_W, opt => opt.MapFrom(src => src.D01_Pos_W.ToStr()))
            .ForMember(dest => dest.D01_Pos_L_Start, opt => opt.MapFrom(src => src.D01_Pos_L_Start.ToStr()))
            .ForMember(dest => dest.D01_Pos_L_End, opt => opt.MapFrom(src => src.D01_Pos_L_End.ToStr()))
            .ForMember(dest => dest.D01_Level, opt => opt.MapFrom(src => src.D01_Level.ToStr()))
            .ForMember(dest => dest.D01_Percent, opt => opt.MapFrom(src => src.D01_Percent.ToStr()))
            .ForMember(dest => dest.D01_QGRADE, opt => opt.MapFrom(src => src.D01_QGRADE.ToStr()))

            .ForMember(dest => dest.D02_Code, opt => opt.MapFrom(src => src.D02_Code.ToStr()))
            .ForMember(dest => dest.D02_Origin, opt => opt.MapFrom(src => src.D02_Origin.ToStr()))
            .ForMember(dest => dest.D02_Sid, opt => opt.MapFrom(src => src.D02_Sid.ToStr()))
            .ForMember(dest => dest.D02_Pos_W, opt => opt.MapFrom(src => src.D02_Pos_W.ToStr()))
            .ForMember(dest => dest.D02_Pos_L_Start, opt => opt.MapFrom(src => src.D02_Pos_L_Start.ToStr()))
            .ForMember(dest => dest.D02_Pos_L_End, opt => opt.MapFrom(src => src.D02_Pos_L_End.ToStr()))
            .ForMember(dest => dest.D02_Level, opt => opt.MapFrom(src => src.D02_Level.ToStr()))
            .ForMember(dest => dest.D02_Percent, opt => opt.MapFrom(src => src.D02_Percent.ToStr()))
            .ForMember(dest => dest.D02_QGRADE, opt => opt.MapFrom(src => src.D02_QGRADE.ToStr()))

            .ForMember(dest => dest.D03_Code, opt => opt.MapFrom(src => src.D03_Code.ToStr()))
            .ForMember(dest => dest.D03_Origin, opt => opt.MapFrom(src => src.D03_Origin.ToStr()))
            .ForMember(dest => dest.D03_Sid, opt => opt.MapFrom(src => src.D03_Sid.ToStr()))
            .ForMember(dest => dest.D03_Pos_W, opt => opt.MapFrom(src => src.D03_Pos_W.ToStr()))
            .ForMember(dest => dest.D03_Pos_L_Start, opt => opt.MapFrom(src => src.D03_Pos_L_Start.ToStr()))
            .ForMember(dest => dest.D03_Pos_L_End, opt => opt.MapFrom(src => src.D03_Pos_L_End.ToStr()))
            .ForMember(dest => dest.D03_Level, opt => opt.MapFrom(src => src.D03_Level.ToStr()))
            .ForMember(dest => dest.D03_Percent, opt => opt.MapFrom(src => src.D03_Percent.ToStr()))
            .ForMember(dest => dest.D03_QGRADE, opt => opt.MapFrom(src => src.D03_QGRADE.ToStr()))

            .ForMember(dest => dest.D04_Code, opt => opt.MapFrom(src => src.D04_Code.ToStr()))
            .ForMember(dest => dest.D04_Origin, opt => opt.MapFrom(src => src.D04_Origin.ToStr()))
            .ForMember(dest => dest.D04_Sid, opt => opt.MapFrom(src => src.D04_Sid.ToStr()))
            .ForMember(dest => dest.D04_Pos_W, opt => opt.MapFrom(src => src.D04_Pos_W.ToStr()))
            .ForMember(dest => dest.D04_Pos_L_Start, opt => opt.MapFrom(src => src.D04_Pos_L_Start.ToStr()))
            .ForMember(dest => dest.D04_Pos_L_End, opt => opt.MapFrom(src => src.D04_Pos_L_End.ToStr()))
            .ForMember(dest => dest.D04_Level, opt => opt.MapFrom(src => src.D04_Level.ToStr()))
            .ForMember(dest => dest.D04_Percent, opt => opt.MapFrom(src => src.D04_Percent.ToStr()))
            .ForMember(dest => dest.D04_QGRADE, opt => opt.MapFrom(src => src.D04_QGRADE.ToStr()))

            .ForMember(dest => dest.D05_Code, opt => opt.MapFrom(src => src.D05_Code.ToStr()))
            .ForMember(dest => dest.D05_Origin, opt => opt.MapFrom(src => src.D05_Origin.ToStr()))
            .ForMember(dest => dest.D05_Sid, opt => opt.MapFrom(src => src.D05_Sid.ToStr()))
            .ForMember(dest => dest.D05_Pos_W, opt => opt.MapFrom(src => src.D05_Pos_W.ToStr()))
            .ForMember(dest => dest.D05_Pos_L_Start, opt => opt.MapFrom(src => src.D05_Pos_L_Start.ToStr()))
            .ForMember(dest => dest.D05_Pos_L_End, opt => opt.MapFrom(src => src.D05_Pos_L_End.ToStr()))
            .ForMember(dest => dest.D05_Level, opt => opt.MapFrom(src => src.D05_Level.ToStr()))
            .ForMember(dest => dest.D05_Percent, opt => opt.MapFrom(src => src.D05_Percent.ToStr()))
            .ForMember(dest => dest.D05_QGRADE, opt => opt.MapFrom(src => src.D05_QGRADE.ToStr()))

            .ForMember(dest => dest.D06_Code, opt => opt.MapFrom(src => src.D06_Code.ToStr()))
            .ForMember(dest => dest.D06_Origin, opt => opt.MapFrom(src => src.D06_Origin.ToStr()))
            .ForMember(dest => dest.D06_Sid, opt => opt.MapFrom(src => src.D06_Sid.ToStr()))
            .ForMember(dest => dest.D06_Pos_W, opt => opt.MapFrom(src => src.D06_Pos_W.ToStr()))
            .ForMember(dest => dest.D06_Pos_L_Start, opt => opt.MapFrom(src => src.D06_Pos_L_Start.ToStr()))
            .ForMember(dest => dest.D06_Pos_L_End, opt => opt.MapFrom(src => src.D06_Pos_L_End.ToStr()))
            .ForMember(dest => dest.D06_Level, opt => opt.MapFrom(src => src.D06_Level.ToStr()))
            .ForMember(dest => dest.D06_Percent, opt => opt.MapFrom(src => src.D06_Percent.ToStr()))
            .ForMember(dest => dest.D06_QGRADE, opt => opt.MapFrom(src => src.D06_QGRADE.ToStr()))

            .ForMember(dest => dest.D07_Code, opt => opt.MapFrom(src => src.D07_Code.ToStr()))
            .ForMember(dest => dest.D07_Origin, opt => opt.MapFrom(src => src.D07_Origin.ToStr()))
            .ForMember(dest => dest.D07_Sid, opt => opt.MapFrom(src => src.D07_Sid.ToStr()))
            .ForMember(dest => dest.D07_Pos_W, opt => opt.MapFrom(src => src.D07_Pos_W.ToStr()))
            .ForMember(dest => dest.D07_Pos_L_Start, opt => opt.MapFrom(src => src.D07_Pos_L_Start.ToStr()))
            .ForMember(dest => dest.D07_Pos_L_End, opt => opt.MapFrom(src => src.D07_Pos_L_End.ToStr()))
            .ForMember(dest => dest.D07_Level, opt => opt.MapFrom(src => src.D07_Level.ToStr()))
            .ForMember(dest => dest.D07_Percent, opt => opt.MapFrom(src => src.D07_Percent.ToStr()))
            .ForMember(dest => dest.D07_QGRADE, opt => opt.MapFrom(src => src.D07_QGRADE.ToStr()))

            .ForMember(dest => dest.D08_Code, opt => opt.MapFrom(src => src.D08_Code.ToStr()))
            .ForMember(dest => dest.D08_Origin, opt => opt.MapFrom(src => src.D08_Origin.ToStr()))
            .ForMember(dest => dest.D08_Sid, opt => opt.MapFrom(src => src.D08_Sid.ToStr()))
            .ForMember(dest => dest.D08_Pos_W, opt => opt.MapFrom(src => src.D08_Pos_W.ToStr()))
            .ForMember(dest => dest.D08_Pos_L_Start, opt => opt.MapFrom(src => src.D08_Pos_L_Start.ToStr()))
            .ForMember(dest => dest.D08_Pos_L_End, opt => opt.MapFrom(src => src.D08_Pos_L_End.ToStr()))
            .ForMember(dest => dest.D08_Level, opt => opt.MapFrom(src => src.D08_Level.ToStr()))
            .ForMember(dest => dest.D08_Percent, opt => opt.MapFrom(src => src.D08_Percent.ToStr()))
            .ForMember(dest => dest.D08_QGRADE, opt => opt.MapFrom(src => src.D08_QGRADE.ToStr()))

            .ForMember(dest => dest.D09_Code, opt => opt.MapFrom(src => src.D09_Code.ToStr()))
            .ForMember(dest => dest.D09_Origin, opt => opt.MapFrom(src => src.D09_Origin.ToStr()))
            .ForMember(dest => dest.D09_Sid, opt => opt.MapFrom(src => src.D09_Sid.ToStr()))
            .ForMember(dest => dest.D09_Pos_W, opt => opt.MapFrom(src => src.D09_Pos_W.ToStr()))
            .ForMember(dest => dest.D09_Pos_L_Start, opt => opt.MapFrom(src => src.D09_Pos_L_Start.ToStr()))
            .ForMember(dest => dest.D09_Pos_L_End, opt => opt.MapFrom(src => src.D09_Pos_L_End.ToStr()))
            .ForMember(dest => dest.D09_Level, opt => opt.MapFrom(src => src.D09_Level.ToStr()))
            .ForMember(dest => dest.D09_Percent, opt => opt.MapFrom(src => src.D09_Percent.ToStr()))
            .ForMember(dest => dest.D09_QGRADE, opt => opt.MapFrom(src => src.D09_QGRADE.ToStr()))

            .ForMember(dest => dest.D10_Code, opt => opt.MapFrom(src => src.D10_Code.ToStr()))
            .ForMember(dest => dest.D10_Origin, opt => opt.MapFrom(src => src.D10_Origin.ToStr()))
            .ForMember(dest => dest.D10_Sid, opt => opt.MapFrom(src => src.D10_Sid.ToStr()))
            .ForMember(dest => dest.D10_Pos_W, opt => opt.MapFrom(src => src.D10_Pos_W.ToStr()))
            .ForMember(dest => dest.D10_Pos_L_Start, opt => opt.MapFrom(src => src.D10_Pos_L_Start.ToStr()))
            .ForMember(dest => dest.D10_Pos_L_End, opt => opt.MapFrom(src => src.D10_Pos_L_End.ToStr()))
            .ForMember(dest => dest.D10_Level, opt => opt.MapFrom(src => src.D10_Level.ToStr()))
            .ForMember(dest => dest.D10_Percent, opt => opt.MapFrom(src => src.D10_Percent.ToStr()))
            .ForMember(dest => dest.D10_QGRADE, opt => opt.MapFrom(src => src.D10_QGRADE.ToStr()));
        }

        private void MappingMsgToPdoUploadedReply()
        {
            CreateMap<Msg_Res_For_PDO_Uploaded, TBL_PDOUploadedReply>()
            .ForMember(dest => dest.Plan_No, opt => opt.MapFrom(src => src.Plan_No.ToStr()))
            .ForMember(dest => dest.Out_Coil_ID, opt => opt.MapFrom(src => src.Mat_No.ToStr()))
            .ForMember(dest => dest.Succ_Flag, opt => opt.MapFrom(src => src.Succ_Flag.ToStr()))
            .ForMember(dest => dest.Err_Msg, opt => opt.MapFrom(src => src.Err_Msg.ToStr()));
        }

        #endregion
    }
}
