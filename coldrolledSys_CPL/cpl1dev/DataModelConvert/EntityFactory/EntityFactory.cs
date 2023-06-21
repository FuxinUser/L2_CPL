using Core.Define;
using Core.Util;
using DataMod.Common;
using DBService.Level25Repository.L2L25_CoilMap;
using DBService.Level25Repository.L2L25_CoilPDI;
using DBService.Level25Repository.L2L25_CoilPDO;
using DBService.Level25Repository.L2L25_CoilRejectResult;
using DBService.Level25Repository.L2L25_CPL1PRESET;
using DBService.Level25Repository.L2L25_DownTime;
using DBService.Level25Repository.L2L25_ENGC;
using DBService.Level25Repository.L2L25_L1L2DisConnection;
using DBService.Level25Repository.L2L25_RECCurrentCT;
using DBService.Level25Repository.L2L25_RECTensionCT;
using DBService.Level25Repository.L2L25_SpeedCT;
using DBService.Level25Repository.L2L25_UNCCurrentCT;
using DBService.Level25Repository.L2L25_UNCTensionCT;
using DBService.Level25Repository.L2L25_WeldCT;
using DBService.Repository;
using MsgStruct;
using System;
using static Core.Define.DBParaDef.ConnectionSysDef;
using static Core.Define.L25SysDef;
using static DataMod.Response.RespnseModel;
using static DBService.Repository.CoilMapEntity;
using static DBService.Repository.CoilScheduleDelete.CoilScheduleDeleteEntity;
using static DBService.Repository.ConnectionStatus.ConnectionStatusEntity;
using static DBService.Repository.DefectData.CoilDefectDataEntity;
using static DBService.Repository.LineFaultRecords.LineFaultRecordsEntity;
using static DBService.Repository.LookupTblPaper.LkUpTablePaperEntity;
using static DBService.Repository.LookupTbSleeve.LkUpTableSleeveEntity;
using static DBService.Repository.PDI.CoilPDIEntity;
using static DBService.Repository.PDO.PDOEntity;
using static DBService.Repository.PresetRecord.PresetRecordEntity;
using static DBService.Repository.ProcessDataCT.ProcessDataCTEntity;
using static DBService.Repository.ScheduleDelete_CoilReject_Record_Temp.ScheduleDeleteRecordTempEntity;
using static DBService.Repository.Utility.UtilityEntity;
using static MsgStruct.L1L2Rcv;
using static MsgStruct.L2L1Snd;
using static MsgStruct.MMSL2Rcv;

namespace MsgConvert.EntityFactory
{
    public static class EntityFactory
    {
        //MMS Sleeve Sync -> TBL_LookupTable_Sleeve
        public static TBL_LookupTable_Sleeve ToTblLKSleeveEntity(this MMSL2Rcv.Msg_Sleeve_Value_Synchronize msg)
        {
            var dbModel = new TBL_LookupTable_Sleeve()
            {
                Sleeve_Code = msg.SleeveCode.ToStr(),
                Out_Coil_Inner_Dia = msg.Out_Mat_Inner_Dia.ToStr().ToNullable<int>() ?? 0,
                Sleeve_Thick = msg.Sleeve_Thick.ToStr().ToNullable<int>() ?? 0,
                Sleeve_Width = msg.Sleeve_Thick.ToStr().ToNullable<int>() ?? 0,
                Sleeve_Weight = msg.Sleeve_Wt.ToStr().ToNullable<float>() / 1000 ?? 0,
                Sleeve_Material = msg.Sleeve_Type.ToStr(),
                Out_Coil_Width_Min = msg.Out_Mat_Width_Min.ToStr().ToNullable<float>() / 10 ?? 0,
                Out_Coil_Width_Max = msg.Out_Mat_Width_Max.ToStr().ToNullable<float>() / 10 ?? 0,
            };

            return dbModel;
        }

        //MMS Paper Sync -> TBL_LookupTable_Paper
        public static TBL_LookupTable_Paper ToTblLKPaperEntity(this MMSL2Rcv.Msg_Paper_Value_Synchronize msg)
        {
            var dbModel = new TBL_LookupTable_Paper()
            {
                Paper_Code = msg.PaperCode.ToStr(),
                Paper_Base_Weight = msg.Paper_Wt.ToStr().ToNullable<int>() ?? 0,
                Paper_Width = msg.Paper_Width.ToStr().ToNullable<int>() ?? 0,
                Paper_Min_Thick = msg.Paper_Min_Thick.ToStr().ToNullable<int>() ?? 0,
                Paper_Max_Thick = msg.Paper_Max_Thick.ToStr().ToNullable<int>() ?? 0,
                Paper_Thick = msg.Paper_Thick.ToStr().ToNullable<int>() ?? 0,
            };

            return dbModel;
        }

        //317 -> TBL_ScheduleDelete_CoilReject_Temp
        public static TBL_ScheduleDelete_CoilReject_Temp ToTblScheduleDeleteCoilRejectTempEntity(this Msg_317_ReturnCoilInfo msg, string entryCoilID)
        {
            var record = new TBL_ScheduleDelete_CoilReject_Temp();

            record.Coil_ID = msg.CoilID.ToStr();
            record.Weight_Of_Rejected_Coil = msg.CoilWeight.ToString();
            record.Length_Of_Rejected_Coil = msg.CoilLength.ToString();
            record.Outer_Diameter_Of_RejectedCoil = msg.Diameter.ToString();
            record.Inner_Diameter_Of_RejectedCoil = msg.CoiInsideDiam.ToString();
            record.Entry_Coil_ID = entryCoilID;
            record.Width_Of_RejectedCoil = msg.Width.ToString();
            return record;
        }

        //310 -> TBL_LineFaultRecords
        public static TBL_LineFaultRecords TblLineFaultRecordsEntity(this Msg_310_LineFault msg, string nowTeam, int nowShift)
        {
            var lineFault = new TBL_LineFaultRecords();



            lineFault.unit_code = L2SystemDef.SystemIDCode;     // 機組代碼
            lineFault.prod_time = msg.DateTime.Date;            // 生產日期.收到當下日期                                                             
            lineFault.delay_reason_code = msg.FaultCode.ToString();
            lineFault.stop_category = msg.StopCategory;

            if (!msg.StartTime.ToStr().Equals(string.Empty))
            {
                lineFault.stop_start_time = msg.StartTime.ConverDateTime();

            }
            else
            {
                lineFault.stop_end_time = msg.EndTime.ConverDateTime();
            }


            lineFault.prod_shift_no = nowShift <= 0 ? "" : nowShift.ToString();     // 目前生產班次 1-夜(24:00-8:00)，2-早(8:00-16:00)，3-中(16:00-24:00)
            lineFault.prod_shift_group = nowTeam;                                   // 當日生產組別 A-甲，B-乙，C-丙，D-丁
            lineFault.UploadMMS = DBParaDef.NO;

            return lineFault;
        }

        //306 -> TBL_CoilMap
        public static CoilMapEntity.TBL_CoilMap ToTBExitCoilMapEntity(this Msg_306_TrackMapEx msg)
        {
            var coilMap = new CoilMapEntity.TBL_CoilMap()
            {
                UpdateTime = DateTime.Now,
                TR = msg.TR,
                Delivery_SK01 = msg.DeliverySK01,
                Delivery_SK02 = msg.DeliverySK02,
                Delivery_TOP = msg.DeliveryTOP,
                Delivery_Car = msg.DeliveryCar,
            };

            return coilMap;
        }

        // 305 -> TBL_CoilMap
        public static CoilMapEntity.TBL_CoilMap ToTBEntryCoilMapEntity(this Msg_305_TrackMapEn msg)
        {
            var coilMap = new CoilMapEntity.TBL_CoilMap()
            {
                UpdateTime = DateTime.Now,
                POR = msg.POR,
                Entry_SK01 = msg.EntrySK01,
                Entry_SK02 = msg.EntrySK02,
                Entry_TOP = msg.EntryTOP,
                Entry_Car = msg.EntryCar,
            };

            return coilMap;
        }

        
        // 316 -> TBL_Utility
        public static TBL_Utility ToTblUtilityEntity(this Msg_316_Utility_Data msg, string shift, string team)
        {
            var utility = new TBL_Utility()
            {
                Receive_Time = DateTime.Now,
                CompressedAir = msg.CompressedAir,
                IndirectCollingWater = msg.IndirectCollingWater,
                Shift = shift,
                Team = team
            };

            return utility;
        }

        // 201 -> TBL_PresetRecord
        public static TBL_PresetRecord ToTblPresetRecordEntity(this Msg_201_Preset msg)
        {
            var tb = new TBL_PresetRecord();
            tb.Coil_ID = msg.CoilIDNo;
            tb.OriPDI_Out_Coil_ID = msg.CoilIDNo;
            tb.SteelGrade = msg.SteelGrade.ToStr();
            tb.Thickness = msg.Thickness;
            tb.Width = msg.Width;
            tb.EntryYieldStress = msg.EntryYieldStress;
            tb.Density = msg.Density;
            tb.CoilLength = msg.CoilLength;
            tb.CoilWeight = msg.CoilWeight;
            tb.ProcessCode = msg.ProcessCode.ToStr();
            tb.InnerDiam = msg.InnerDiam;
            tb.Diameter = msg.Diameter;
            tb.SleeveCodeEntry = msg.SleeveCodeEntry;
            tb.SleeveDmEntry = msg.SleeveDmEntry;
            tb.PaperWinderFlag = msg.PaperWinderFlag;
            tb.SleeveCodeExit = msg.SleeveCodeExit;
            tb.SleeveDmExit = msg.SleeveDmExit;
            tb.PaperTypeExit = msg.PaperTypeExit;
            tb.PaperCodeExit = msg.PaperCodeExit;
            tb.FlatenerDepth1 = msg.FlatenerDepth1;
            tb.FlatenerDepth2 = msg.FlatenerDepth2;
            tb.UncoilerTension = msg.UncoilerTension;
            tb.UncoilerTensionMax = msg.UncoilerTensionMax;
            tb.UncoilerTensionMin = msg.UncoilerTensionMin;
            tb.HeadLeaderStripLength = msg.HeadLeaderStripLength;
            tb.HeadLeaderStripThickness = msg.HeadLeaderStripThickness;
            tb.HeadLeaderStripWidth = msg.HeadLeaderStripWidth;
            tb.HeadLeaderStripSteelGrade = msg.HeadLeaderStripSteelGrade;
            tb.TailLeaderStripLength = msg.TailLeaderStripLength;
            tb.TailLeaderStripThickness = msg.TailLeaderStripThickness;
            tb.TailLeaderStripWidth = msg.TailLeaderStripWidth;
            tb.TailLeaderStripSteelGrade = msg.TailLeaderStripSteelGrade;
            tb.SideTrimmerGap = msg.SideTrimmerGap;
            tb.SideTrimmerLap = msg.SideTrimmerLap;
            tb.SideTrimmerWidth = msg.SideTrimmerWidth;
            tb.TensionUnitDepth = msg.TensionUnitDepth;
            tb.RecoilerTension = msg.RecoilerTension;
            tb.RecoilerTensionMax = msg.RecoilerTensionMax;
            tb.RecoilerTensionMin = msg.RecoilerTensionMin;
            tb.PaperUnwinderFlag = msg.PaperUnwinderFlag;
            tb.CoilSplit = msg.CoilSplit;
            tb.Orderwt_1 = msg.Orderwt_1;
            tb.Orderwt_2 = msg.Orderwt_2;
            tb.Orderwt_3 = msg.Orderwt_3;
            tb.Orderwt_4 = msg.Orderwt_4;
            tb.Orderwt_5 = msg.Orderwt_5;
            tb.Orderwt_6 = msg.Orderwt_6;
            tb.PrrPosId = msg.PrrPosId;
            tb.PresetTime = msg.DateTime;

            return tb;

        }


        // To TBL_CoilScheduleDelete
        public static TBL_CoilScheduleDelete ToTblCoilScheduleDeleteEntity(this string coilID, string operatorId = "", string reasonCode = "", string remarks = "")
        {
            var delSchedule = new TBL_CoilScheduleDelete
            {
                Coil_ID = coilID,
                OperatorId = operatorId,
                ReasonCode = reasonCode,
                Remarks = remarks
            };

            return delSchedule;
        }

        // To TBL_ConnectionStatus
        public static TBL_ConnectionStatus ToTblConnectionStatusEntity(ConnectionType connType, string ip, string port, string connectionStatut)
        {
            var tb = new TBL_ConnectionStatus();

            switch (connType)
            {
                //L2->PLC
                case ConnectionType.L2ConnectToPLC:
                    tb.Connection_From = L2;
                    tb.Connection_To = L1;
                    break;
                //L2<- MMS
                case ConnectionType.L2ConnectedByMMS:
                    tb.Connection_From = MMS;
                    tb.Connection_To = L2;
                    break;
                //L2-> MMS
                case ConnectionType.L2ConnectToMMS:
                    tb.Connection_From = L2;
                    tb.Connection_To = MMS;
                    break;
                //L2 <-WMS
                case ConnectionType.L2ConnectedByWMS:
                    tb.Connection_From = WMS;
                    tb.Connection_To = L2;
                    break;
                //L2 -> WMS
                case ConnectionType.L2ConnectToWMS:
                    tb.Connection_From = L2;
                    tb.Connection_To = WMS;
                    break;
            }

            tb.Connection_IP = ip;
            tb.Connection_Port = port;
            tb.Connection_Status = connectionStatut;
            tb.Create_DateTime = DateTime.Now;

            return tb;
        }

        // To L2L25_L1L2DisConnection
        public static L2L25_L1L2DisConnection ToL2L25L1L2DisConnection(this ConnectionType connType, string connectionStatuts)
        {
            var entity = new L2L25_L1L2DisConnection();
            entity.Message_Id = MsgCode.Msg112L1L2DisConnection;
            entity.Message_Length = MsgLength.Msg112L1L2DisConnection;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");
            entity.Time = DateTime.Now.ToString("HHmmss");


            switch (connType)
            {
                //L2->PLC
                case ConnectionType.L2ConnectToPLC:
                    entity.SystemID_3 =  connectionStatuts;
                    break;
                //L2<- MMS
                case ConnectionType.L2ConnectedByMMS:
                    entity.SystemID_1 = connectionStatuts;
                    break;
                //L2-> MMS
                case ConnectionType.L2ConnectToMMS:
                    entity.SystemID_1 =  connectionStatuts;
                    break;
                //L2 <-WMS
                case ConnectionType.L2ConnectedByWMS:
                    entity.SystemID_4 = connectionStatuts;
                    break;
                //L2 -> WMS
                case ConnectionType.L2ConnectToWMS:
                    entity.SystemID_4 = connectionStatuts;
                    break;
            }


            return entity;
        }


        public static TBL_ProcessDataCT ToProcessDataCTEntity(this PDO pdo, ProcessCTModel data, L25CTData dataClassify)
        {

            var entity = new TBL_ProcessDataCT();
            entity.Out_Coil_No = pdo.Out_Coil_ID;
            entity.In_Coil_ID = pdo.In_Coil_ID;
            entity.OriPDI_Out_Coil_ID = pdo.OriPDI_Out_Coil_ID;
            entity.BeginDate = pdo.StartTime.ToString(DBParaDef.DB25DateFromat);
            entity.BeginTime = pdo.StartTime.ToString(DBParaDef.DB25TimeFromat);
            entity.StopDate = pdo.FinishTime.ToString(DBParaDef.DB25DateFromat);
            entity.StopTime = pdo.FinishTime.ToString(DBParaDef.DB25TimeFromat);
            entity.DataCount = data.DataCnt;

            switch (dataClassify)
            {
                case L25CTData.Speed:
                    entity.SeqUnit = CTData.SeqUnit.Speed;
                    entity.DataCode = CTData.Code.Speed;
                    entity.DataDesc = CTData.Desc.Speed;
                    entity.DataString = data.SpeedStr;
                    break;
                case L25CTData.UNCTensionActCT:
                    entity.SeqUnit = CTData.SeqUnit.UNCTension;
                    entity.DataCode = CTData.Code.UNCTensionAct;
                    entity.DataDesc = CTData.Desc.Tension;
                    entity.DataString = data.PorTensionActStr;
                    break;
                case L25CTData.UNCTensionRefCT:
                    entity.SeqUnit = CTData.SeqUnit.UNCTension;
                    entity.DataCode = CTData.Code.UNCTensionRef;
                    entity.DataDesc = CTData.Desc.Tension;
                    entity.DataString = data.PorTensionRefStr;
                    break;
                case L25CTData.UNCCurrentCT:
                    entity.SeqUnit = CTData.SeqUnit.UNCCurrent;
                    entity.DataCode = CTData.Code.UNCCurrent;
                    entity.DataDesc = CTData.Desc.Current;
                    entity.DataString = data.PorCurrentStr;
                    break;
                case L25CTData.RECTensionActCT:
                    entity.SeqUnit = CTData.SeqUnit.RECTension;
                    entity.DataCode = CTData.Code.RECTensionAct;
                    entity.DataDesc = CTData.Desc.Tension;
                    entity.DataString = data.TrTensionActStr;
                    break;
                case L25CTData.RECTensionRefCT:
                    entity.SeqUnit = CTData.SeqUnit.RECTension;
                    entity.DataCode = CTData.Code.RECTensionRef;
                    entity.DataDesc = CTData.Desc.Tension;
                    entity.DataString = data.TrTensionRefStr;
                    break;
                case L25CTData.RECCurrentCT:
                    entity.SeqUnit = CTData.SeqUnit.RECCurrent;
                    entity.DataCode = CTData.Code.RECCurrent;
                    entity.DataDesc = CTData.Desc.Current;
                    entity.DataString = data.TrCurrentStr;
                    break;
            }
           
            return entity;
        }

        public static TBL_ProcessDataCT ToProcessDataCTWeldEntity(this PDO pdo, ProcessCTWeldModel data, L25CTData dataClassify)
        {

            var entity = new TBL_ProcessDataCT();
            entity.Out_Coil_No = pdo.Out_Coil_ID;
            entity.In_Coil_ID = pdo.In_Coil_ID;
            entity.OriPDI_Out_Coil_ID = pdo.OriPDI_Out_Coil_ID;
            entity.BeginDate = pdo.StartTime.ToString(DBParaDef.DB25DateFromat);
            entity.BeginTime = pdo.StartTime.ToString(DBParaDef.DB25TimeFromat);
            entity.StopDate = pdo.FinishTime.ToString(DBParaDef.DB25DateFromat);
            entity.StopTime = pdo.FinishTime.ToString(DBParaDef.DB25TimeFromat);
            entity.DataCount = data.DataCnt;

            switch (dataClassify)
            {
                case L25CTData.WELD_Speed_Actual:
                    entity.SeqUnit = CTData.SeqUnit.WELD_Speed_Actual;
                    entity.DataCode = CTData.Code.WELD_Speed_Actual;
                    entity.DataDesc = CTData.Desc.Weld;
                    entity.DataString = data.WeldSpeedActStr;
                    break;
                case L25CTData.WELD_Current_Actual_Front:
                    entity.SeqUnit = CTData.SeqUnit.WELD_Current_Actual_Front;
                    entity.DataCode = CTData.Code.WELD_Current_Actual_Front;
                    entity.DataDesc = CTData.Desc.Weld;
                    entity.DataString = data.WeldCurrentActFStr;
                    break;
                case L25CTData.WELD_Current_Actual_Rear:
                    entity.SeqUnit = CTData.SeqUnit.WELD_Current_Actual_Rear;
                    entity.DataCode = CTData.Code.WELD_Current_Actual_Rear;
                    entity.DataDesc = CTData.Desc.Weld;
                    entity.DataString = data.WeldCurrentActRStr;
                    break;
                case L25CTData.WELD_PlanishWheelForce_Actual:
                    entity.SeqUnit = CTData.SeqUnit.WELD_PlanishWheelForce_Actual;
                    entity.DataCode = CTData.Code.WELD_PlanishWheelForce_Actual;
                    entity.DataDesc = CTData.Desc.Weld;
                    entity.DataString = data.WeldPlanishWFActStr;
                    break;
                case L25CTData.WELD_Temperture:
                    entity.SeqUnit = CTData.SeqUnit.WELD_Temperture;
                    entity.DataCode = CTData.Code.WELD_Temperture;
                    entity.DataDesc = CTData.Desc.Weld;
                    entity.DataString = data.WeldTempertureStr;
                    break;
            }


            return entity;
        }


        // MMS PDI -> L2L25_CoilPDI

        public static L2L25_CoilPDI ToL25PDIEntity(this Msg_PDI msg)
        {
            var tb = new L2L25_CoilPDI();
            tb.Message_Id = L25SysDef.MsgCode.Msg101PDI;
            tb.Message_Length = msg.Length.ToStr();
            tb.Date = msg.Date.ToStr();
            tb.Time = msg.Time.ToStr();

            tb.PLAN_NO = msg.Plan_No.ToStr();
            tb.MAT_PLAN_SEQ_NO = msg.Mat_Seq_No.ToStr();
            tb.PLAN_TYPE = msg.Plan_Sort.ToStr();
            tb.IN_MAT_NO = msg.Entry_Coil_No.ToStr();
            tb.IN_MAT_THICK = msg.Entry_Coil_Thick.ToStr();
            tb.IN_MAT_WIDTH = msg.Entry_Coil_Width.ToStr();
            tb.IN_MAT_WT = msg.Entry_Coil_Weight.ToStr();
            tb.IN_MAT_LEN = msg.Entry_Coil_Length.ToStr();
            tb.IN_MAT_INNER_DIA = msg.Entry_Coil_Inner.ToStr();
            tb.IN_MAT_OUTER_DIA = msg.Entry_Coil_Dcos.ToStr();
            tb.SLEEVE_TYPE_CODE = msg.Sleeve_Type_Code.ToStr();
            tb.SLEEVE_DIAMTER = msg.Sleeve_diamter.ToStr();
            tb.PAPER_REQ_CODE = msg.Paper_Req_Code.ToStr();
            tb.PAPER_CODE = msg.Paper_Code.ToStr();
            tb.HEAD_PAPER_LENGTH = msg.Head_Paper_Length.ToStr();
            tb.HEAD_PAPER_WIDTH = msg.Head_Paper_Width.ToStr();
            tb.TAIL_PAPER_LENGTH = msg.Tail_Paper_Length.ToStr();
            tb.TAIL_PAPER_WIDTH = msg.Tail_Paper_Width.ToStr();
            tb.TS_MAX = msg.Ts_Stand_Max.ToStr();
            tb.TS_MIN = msg.Ts_Stand_Min.ToStr();
            tb.ST_NO = msg.St_No.ToStr();
            tb.DENSITY = msg.Density.ToStr();
            tb.REPAIR_TYPE = msg.REPAIR_TYPE.ToStr();
            tb.SURFACE_ACCU_CODE = msg.Surface_Finishing_Code.ToStr();
            tb.SURFACE_ACCURACY = msg.Surface_Accuracy.ToStr();
            tb.BETTER_SURF_WARD_CODE = msg.Base_Surface.ToStr();
            tb.UNCOIL_DIRECTION = msg.Uncoiler_Direction.ToStr();
            tb.OUT_MAT_NO = msg.Out_Mat_No.ToStr();
            tb.OUT_PAPER_REQ_CODE = msg.Out_Paper_Req_Code.ToStr();
            tb.OUT_PAPER_CODE = msg.Out_Paper_Code.ToStr();
            tb.OUT_SLEEVE_DIAMTER = msg.Out_Sleeve_Diamter.ToStr();
            tb.OUT_SLEEVE_TYPE_CODE = msg.Out_Sleeve_Type_Code.ToStr();
            tb.PACK_MODE = msg.Out_Strap_Num.ToStr();
            tb.LEADER_FLAG = msg.Leader_Flag.ToStr();
            tb.SAMPLE_FLAG = msg.Samp_Flag.ToStr();
            tb.SAMPLE_FRQN_CODE = msg.Sample_Frqn_Code.ToStr();
            tb.SAMPLE_LOT_NO = msg.Sample_Lot_No.ToStr();
            tb.ORIGIN_CODE = msg.Coil_Origin.ToStr();
            tb.PREV_WHOLE_BACKLOG_CODE = msg.Wholebacklog_Code.ToStr();
            tb.NEXT_WHOLE_BACKLOG_CODE = msg.Next_Wholebacklog_Code.ToStr();
            tb.TRIM_FLAG = msg.Trim_Flag.ToStr();
            tb.OUT_MAT_WIDTH = msg.Out_Mat_Width.ToStr();
            tb.OUT_MAT_MAX_WIDTH = msg.Out_Mat_Width_Max.ToStr();
            tb.OUT_MAT_MIN_WIDTH = msg.Out_Mat_Width_Min.ToStr();
            tb.OUT_MAT_THICK = msg.Out_Mat_Thickness.ToStr();
            tb.OUT_MAT_INNER_DIA = msg.Out_Coil_Inner.ToStr();
            tb.ORDER_NO = msg.Order_No.ToStr();
            tb.ORDER_UNIT_MAX_WT = msg.Order_Wt_Max.ToStr();
            tb.ORDER_UNIT_MIN_WT = msg.Order_Wt_Min.ToStr();
            tb.ORDER_UNIT_AIM_WT = msg.Order_Wt.ToStr();
            tb.FIXED_WT_FLAG = msg.Dividing_Flag.ToStr();
            tb.DIVIDE_NUM = msg.Dividing_Num.ToStr();
            tb.ORDER_WT1 = msg.Orderwt_1.ToStr();
            tb.ORDER_WT2 = msg.Orderwt_2.ToStr();
            tb.ORDER_WT3 = msg.Orderwt_3.ToStr();
            tb.ORDER_WT4 = msg.Orderwt_4.ToStr();
            tb.ORDER_WT5 = msg.Orderwt_5.ToStr();
            tb.ORDER_WT6 = msg.Orderwt_6.ToStr();
            tb.ORDER_NO1 = msg.Order_No_1.ToStr();
            tb.ORDER_NO2 = msg.Order_No_2.ToStr();
            tb.ORDER_NO3 = msg.Order_No_3.ToStr();
            tb.ORDER_NO4 = msg.Order_No_4.ToStr();
            tb.ORDER_NO5 = msg.Order_No_5.ToStr();
            tb.ORDER_NO6 = msg.Order_No_6.ToStr();

            tb.TEST_PLAN_NO = msg.Test_Plan_No.ToStr();
            tb.QC_REMARK = msg.Qc_Remark.ToStr();
            tb.HEAD_OFF_GAUGE = msg.Head_Off_Gauge.ToStr();
            tb.TAIL_OFF_GAUGE = msg.Tail_Off_Gauge.ToStr();
            tb.SURFACE_ACCU_CODE_IN = msg.Surface_Accu_Code_In.ToStr();
            tb.SURFACE_ACCU_CODE_OUT = msg.Surface_Accu_Code_Out.ToStr();
            tb.SG_SIGN = msg.Sg_Sign.ToStr();
            tb.PROCESS_CODE = msg.Process_Code.ToStr();
            tb.ORDER_CUST_CODE = msg.Customer_No.ToStr();
            tb.ORDER_CUST_ENAME = msg.Customer_Name_E.ToStr();
            tb.ORDER_CUST_CNAME = msg.Customer_Name_C.ToStr();
            tb.SURFACE_ACCU_DESC = msg.Surface_Acc_Desc.ToStr();
            tb.SURFACE_ACCURACY_DESC = msg.Surface_Acc_Desc.ToStr();
            tb.SURFACE_ACCU_DESC_IN = msg.Surface_Acc_Desc_In.ToStr();
            tb.SURFACE_ACCU_DESC_OUT = msg.Surface_Acc_Desc_Out.ToStr();

            tb.DEFECT_CODE1 = msg.D01_Code.ToStr();
            tb.DEFECT_FROM1 = msg.D01_Origin.ToStr();
            tb.DEFECT_SURF1 = msg.D01_Sid.ToStr();
            tb.DEFECT_POSITION_WIDTH1 = msg.D01_Pos_W.ToStr();
            tb.DEFECT_LEN_START1 = msg.D01_Pos_L_Start.ToStr();
            tb.DEFECT_LEN_END1 = msg.D01_Pos_L_End.ToStr();
            tb.DEFECT_CLASS1 = msg.D01_Level.ToStr();
            tb.DEFECT_PERCENT1 = msg.D01_Percent.ToStr();
            tb.DEFECT_QUALITY_GRADE1 = msg.D01_QGRADE.ToStr();

            tb.DEFECT_CODE2 = msg.D02_Code.ToStr();
            tb.DEFECT_FROM2 = msg.D02_Origin.ToStr();
            tb.DEFECT_SURF2 = msg.D02_Sid.ToStr();
            tb.DEFECT_POSITION_WIDTH2 = msg.D02_Pos_W.ToStr();
            tb.DEFECT_LEN_START2 = msg.D02_Pos_L_Start.ToStr();
            tb.DEFECT_LEN_END2 = msg.D02_Pos_L_End.ToStr();
            tb.DEFECT_CLASS2 = msg.D02_Level.ToStr();
            tb.DEFECT_PERCENT2 = msg.D02_Percent.ToStr();
            tb.DEFECT_QUALITY_GRADE2 = msg.D02_QGRADE.ToStr();


            tb.DEFECT_CODE3 = msg.D03_Code.ToStr();
            tb.DEFECT_FROM3 = msg.D03_Origin.ToStr();
            tb.DEFECT_SURF3 = msg.D03_Sid.ToStr();
            tb.DEFECT_POSITION_WIDTH3 = msg.D03_Pos_W.ToStr();
            tb.DEFECT_LEN_START3 = msg.D03_Pos_L_Start.ToStr();
            tb.DEFECT_LEN_END3 = msg.D03_Pos_L_End.ToStr();
            tb.DEFECT_CLASS3 = msg.D03_Level.ToStr();
            tb.DEFECT_PERCENT3 = msg.D03_Percent.ToStr();
            tb.DEFECT_QUALITY_GRADE3 = msg.D03_QGRADE.ToStr();

            tb.DEFECT_CODE4 = msg.D04_Code.ToStr();
            tb.DEFECT_FROM4 = msg.D04_Origin.ToStr();
            tb.DEFECT_SURF4 = msg.D04_Sid.ToStr();
            tb.DEFECT_POSITION_WIDTH4 = msg.D04_Pos_W.ToStr();
            tb.DEFECT_LEN_START4 = msg.D04_Pos_L_Start.ToStr();
            tb.DEFECT_LEN_END4 = msg.D04_Pos_L_End.ToStr();
            tb.DEFECT_CLASS4 = msg.D04_Level.ToStr();
            tb.DEFECT_PERCENT4 = msg.D04_Percent.ToStr();
            tb.DEFECT_QUALITY_GRADE4 = msg.D04_QGRADE.ToStr();

            tb.DEFECT_CODE5 = msg.D05_Code.ToStr();
            tb.DEFECT_FROM5 = msg.D05_Origin.ToStr();
            tb.DEFECT_SURF5 = msg.D05_Sid.ToStr();
            tb.DEFECT_POSITION_WIDTH5 = msg.D05_Pos_W.ToStr();
            tb.DEFECT_LEN_START5 = msg.D05_Pos_L_Start.ToStr();
            tb.DEFECT_LEN_END5 = msg.D05_Pos_L_End.ToStr();
            tb.DEFECT_CLASS5 = msg.D05_Level.ToStr();
            tb.DEFECT_PERCENT5 = msg.D05_Percent.ToStr();
            tb.DEFECT_QUALITY_GRADE5 = msg.D05_QGRADE.ToStr();

            tb.DEFECT_CODE6 = msg.D06_Code.ToStr();
            tb.DEFECT_FROM6 = msg.D06_Origin.ToStr();
            tb.DEFECT_SURF6 = msg.D06_Sid.ToStr();
            tb.DEFECT_POSITION_WIDTH6 = msg.D06_Pos_W.ToStr();
            tb.DEFECT_LEN_START6 = msg.D06_Pos_L_Start.ToStr();
            tb.DEFECT_LEN_END6 = msg.D06_Pos_L_End.ToStr();
            tb.DEFECT_CLASS6 = msg.D06_Level.ToStr();
            tb.DEFECT_PERCENT6 = msg.D06_Percent.ToStr();
            tb.DEFECT_QUALITY_GRADE6 = msg.D06_QGRADE.ToStr();

            tb.DEFECT_CODE7 = msg.D07_Code.ToStr();
            tb.DEFECT_FROM7 = msg.D07_Origin.ToStr();
            tb.DEFECT_SURF7 = msg.D07_Sid.ToStr();
            tb.DEFECT_POSITION_WIDTH7 = msg.D07_Pos_W.ToStr();
            tb.DEFECT_LEN_START7 = msg.D07_Pos_L_Start.ToStr();
            tb.DEFECT_LEN_END7 = msg.D07_Pos_L_End.ToStr();
            tb.DEFECT_CLASS7 = msg.D07_Level.ToStr();
            tb.DEFECT_PERCENT7 = msg.D07_Percent.ToStr();
            tb.DEFECT_QUALITY_GRADE7 = msg.D07_QGRADE.ToStr();

            tb.DEFECT_CODE8 = msg.D08_Code.ToStr();
            tb.DEFECT_FROM8 = msg.D08_Origin.ToStr();
            tb.DEFECT_SURF8 = msg.D08_Sid.ToStr();
            tb.DEFECT_POSITION_WIDTH8 = msg.D08_Pos_W.ToStr();
            tb.DEFECT_LEN_START8 = msg.D08_Pos_L_Start.ToStr();
            tb.DEFECT_LEN_END8 = msg.D08_Pos_L_End.ToStr();
            tb.DEFECT_CLASS8 = msg.D08_Level.ToStr();
            tb.DEFECT_PERCENT8 = msg.D08_Percent.ToStr();
            tb.DEFECT_QUALITY_GRADE8 = msg.D08_QGRADE.ToStr();

            tb.DEFECT_CODE9 = msg.D09_Code.ToStr();
            tb.DEFECT_FROM9 = msg.D09_Origin.ToStr();
            tb.DEFECT_SURF9 = msg.D09_Sid.ToStr();
            tb.DEFECT_POSITION_WIDTH9 = msg.D09_Pos_W.ToStr();
            tb.DEFECT_LEN_START9 = msg.D09_Pos_L_Start.ToStr();
            tb.DEFECT_LEN_END9 = msg.D09_Pos_L_End.ToStr();
            tb.DEFECT_CLASS9 = msg.D09_Level.ToStr();
            tb.DEFECT_PERCENT9 = msg.D09_Percent.ToStr();
            tb.DEFECT_QUALITY_GRADE9 = msg.D09_QGRADE.ToStr();

            tb.DEFECT_CODE10 = msg.D10_Code.ToStr();
            tb.DEFECT_FROM10 = msg.D10_Origin.ToStr();
            tb.DEFECT_SURF10 = msg.D10_Sid.ToStr();
            tb.DEFECT_POSITION_WIDTH10 = msg.D10_Pos_W.ToStr();
            tb.DEFECT_LEN_START10 = msg.D10_Pos_L_Start.ToStr();
            tb.DEFECT_LEN_END10 = msg.D10_Pos_L_End.ToStr();
            tb.DEFECT_CLASS10 = msg.D10_Level.ToStr();
            tb.DEFECT_PERCENT10 = msg.D10_Percent.ToStr();
            tb.DEFECT_QUALITY_GRADE10 = msg.D10_QGRADE.ToStr();

            return tb;
        }


        // TBL_PDI TBL_Coil_Defect -> L2L25_CoilPDI

        public static L2L25_CoilPDI ToL25PDIEntity(this TBL_PDI dao, TBL_Coil_Defect defect)
        {
            var tb = new L2L25_CoilPDI();
            if (defect == null)
                defect = new TBL_Coil_Defect();


            tb.Message_Id = L25SysDef.MsgCode.Msg101PDI;
            tb.Message_Length = L25SysDef.MsgLength.Msg101PDI;
            tb.Date = DateTime.Now.ToString("yyyyMMdd");
            tb.Time = DateTime.Now.ToString("HHmmss");

            tb.PLAN_NO = dao.Plan_No;
            tb.MAT_PLAN_SEQ_NO = dao.Mat_Seq_No.ToString();
            tb.PLAN_TYPE = dao.Plan_Sort;
            tb.IN_MAT_NO = dao.Entry_Coil_ID;
            tb.IN_MAT_THICK = dao.Entry_Coil_Thick.ToString();
            tb.IN_MAT_WIDTH = dao.Entry_Coil_Width.ToString();
            tb.IN_MAT_WT = dao.Entry_Coil_Weight.ToString();
            tb.IN_MAT_LEN = dao.Entry_Coil_Length.ToString();
            tb.IN_MAT_INNER_DIA = dao.Entry_Coil_Inner.ToString();
            tb.IN_MAT_OUTER_DIA = dao.Entry_Coil_Dcos.ToString();
            tb.SLEEVE_TYPE_CODE = dao.Sleeve_Type_Code.ToString();
            tb.SLEEVE_DIAMTER = dao.Sleeve_diamter.ToString();
            tb.PAPER_REQ_CODE = dao.Paper_Req_Code.ToString();
            tb.PAPER_CODE = dao.Paper_Code.ToString();
            tb.HEAD_PAPER_LENGTH = dao.Head_Paper_Length.ToString();
            tb.HEAD_PAPER_WIDTH = dao.Head_Paper_Width.ToString();
            tb.TAIL_PAPER_LENGTH = dao.Tail_Paper_Length.ToString();
            tb.TAIL_PAPER_WIDTH = dao.Tail_Paper_Width.ToString();
            tb.TS_MAX = dao.Ts_Stand_Max.ToString();
            tb.TS_MIN = dao.Ts_Stand_Min.ToString();
            tb.ST_NO = dao.St_No.ToString();
            tb.DENSITY = dao.Density.ToString();
            tb.REPAIR_TYPE = dao.REPAIR_TYPE.ToString();
            tb.SURFACE_ACCU_CODE = dao.Surface_Finishing_Code.ToString();
            tb.SURFACE_ACCURACY = dao.Surface_Accuracy.ToString();
            tb.BETTER_SURF_WARD_CODE = dao.Base_Surface.ToString();
            tb.UNCOIL_DIRECTION = dao.Uncoiler_Direction.ToString();
            tb.OUT_MAT_NO = dao.Out_Coil_ID;
            tb.OUT_PAPER_REQ_CODE = dao.Out_Paper_Req_Code.ToString();
            tb.OUT_PAPER_CODE = dao.Out_Paper_Code.ToString();
            tb.OUT_SLEEVE_DIAMTER = dao.Out_Sleeve_Diamter.ToString();
            tb.OUT_SLEEVE_TYPE_CODE = dao.Out_Sleeve_Type_Code.ToString();
            tb.PACK_MODE = dao.Out_Strap_Num.ToString();
            tb.LEADER_FLAG = dao.Leader_Flag.ToString();
            tb.SAMPLE_FLAG = dao.Sample_Flag;
            tb.SAMPLE_FRQN_CODE = dao.Sample_Frqn_Code.ToString();
            tb.SAMPLE_LOT_NO = dao.Sample_Lot_No.ToString();
            tb.ORIGIN_CODE = dao.Coil_Origin.ToString();
            tb.PREV_WHOLE_BACKLOG_CODE = dao.Wholebacklog_Code.ToString();
            tb.NEXT_WHOLE_BACKLOG_CODE = dao.Next_Wholebacklog_Code.ToString();
            tb.TRIM_FLAG = dao.Trim_Flag.ToString();
            tb.OUT_MAT_WIDTH = dao.Out_Coil_Width.ToString();
            tb.OUT_MAT_MAX_WIDTH = dao.Out_Coil_Width_Max.ToString();
            tb.OUT_MAT_MIN_WIDTH = dao.Out_Coil_Width_Min.ToString();
            tb.OUT_MAT_THICK = dao.Out_Coil_Thickness.ToString();
            tb.OUT_MAT_INNER_DIA = dao.Out_Coil_Inner.ToString();
            tb.ORDER_NO = dao.Order_No.ToString();
            tb.ORDER_UNIT_MAX_WT = dao.Order_Wt_Max.ToString();
            tb.ORDER_UNIT_MIN_WT = dao.Order_Wt_Min.ToString();
            tb.ORDER_UNIT_AIM_WT = dao.Order_Wt.ToString();
            tb.FIXED_WT_FLAG = dao.Dividing_Flag.ToString();
            tb.DIVIDE_NUM = dao.Dividing_Num.ToString();
            tb.ORDER_WT1 = dao.Orderwt_1.ToString();
            tb.ORDER_WT2 = dao.Orderwt_2.ToString();
            tb.ORDER_WT3 = dao.Orderwt_3.ToString();
            tb.ORDER_WT4 = dao.Orderwt_4.ToString();
            tb.ORDER_WT5 = dao.Orderwt_5.ToString();
            tb.ORDER_WT6 = dao.Orderwt_6.ToString();
            tb.ORDER_NO1 = dao.Order_No_1.ToString();
            tb.ORDER_NO2 = dao.Order_No_2.ToString();
            tb.ORDER_NO3 = dao.Order_No_3.ToString();
            tb.ORDER_NO4 = dao.Order_No_4.ToString();
            tb.ORDER_NO5 = dao.Order_No_5.ToString();
            tb.ORDER_NO6 = dao.Order_No_6.ToString();

            tb.TEST_PLAN_NO = dao.Test_Plan_No.ToString();
            tb.QC_REMARK = dao.Qc_Remark.ToString();
            tb.HEAD_OFF_GAUGE = dao.Head_Off_Gauge.ToString();
            tb.TAIL_OFF_GAUGE = dao.Tail_Off_Gauge.ToString();
            tb.SURFACE_ACCU_CODE_IN = dao.Surface_Accu_Code_In.ToString();
            tb.SURFACE_ACCU_CODE_OUT = dao.Surface_Accu_Code_Out.ToString();
            tb.SG_SIGN = dao.Sg_Sign.ToString();
            tb.PROCESS_CODE = dao.Process_Code.ToString();
            tb.ORDER_CUST_CODE = dao.CustomerCode.ToString();
            tb.ORDER_CUST_ENAME = dao.CustomerName_E.ToString();
            tb.ORDER_CUST_CNAME = dao.CustomerName_C.ToString();
            tb.SURFACE_ACCU_DESC = dao.Surface_Acc_Desc.ToString();
            tb.SURFACE_ACCURACY_DESC = dao.Surface_Acc_Desc.ToString();
            tb.SURFACE_ACCU_DESC_IN = dao.Surface_Acc_Desc_In.ToString();
            tb.SURFACE_ACCU_DESC_OUT = dao.Surface_Acc_Desc_Out.ToString();

            tb.DEFECT_CODE1 = defect.D01_Code;
            tb.DEFECT_FROM1 = defect.D01_Origin;
            tb.DEFECT_SURF1 = defect.D01_Sid;
            tb.DEFECT_POSITION_WIDTH1 = defect.D01_Pos_W;
            tb.DEFECT_LEN_START1 = defect.D01_Pos_L_Start;
            tb.DEFECT_LEN_END1 = defect.D01_Pos_L_End;
            tb.DEFECT_CLASS1 = defect.D01_Level;
            tb.DEFECT_PERCENT1 = defect.D01_Percent;
            tb.DEFECT_QUALITY_GRADE1 = defect.D01_QGRADE;

            tb.DEFECT_CODE2 = defect.D02_Code;
            tb.DEFECT_FROM2 = defect.D02_Origin;
            tb.DEFECT_SURF2 = defect.D02_Sid;
            tb.DEFECT_POSITION_WIDTH2 = defect.D02_Pos_W;
            tb.DEFECT_LEN_START2 = defect.D02_Pos_L_Start;
            tb.DEFECT_LEN_END2 = defect.D02_Pos_L_End;
            tb.DEFECT_CLASS2 = defect.D02_Level;
            tb.DEFECT_PERCENT2 = defect.D02_Percent;
            tb.DEFECT_QUALITY_GRADE2 = defect.D02_QGRADE;


            tb.DEFECT_CODE3 = defect.D03_Code;
            tb.DEFECT_FROM3 = defect.D03_Origin;
            tb.DEFECT_SURF3 = defect.D03_Sid;
            tb.DEFECT_POSITION_WIDTH3 = defect.D03_Pos_W;
            tb.DEFECT_LEN_START3 = defect.D03_Pos_L_Start;
            tb.DEFECT_LEN_END3 = defect.D03_Pos_L_End;
            tb.DEFECT_CLASS3 = defect.D03_Level;
            tb.DEFECT_PERCENT3 = defect.D03_Percent;
            tb.DEFECT_QUALITY_GRADE3 = defect.D03_QGRADE;

            tb.DEFECT_CODE4 = defect.D04_Code;
            tb.DEFECT_FROM4 = defect.D04_Origin;
            tb.DEFECT_SURF4 = defect.D04_Sid;
            tb.DEFECT_POSITION_WIDTH4 = defect.D04_Pos_W;
            tb.DEFECT_LEN_START4 = defect.D04_Pos_L_Start;
            tb.DEFECT_LEN_END4 = defect.D04_Pos_L_End;
            tb.DEFECT_CLASS4 = defect.D04_Level;
            tb.DEFECT_PERCENT4 = defect.D04_Percent;
            tb.DEFECT_QUALITY_GRADE4 = defect.D04_QGRADE;

            tb.DEFECT_CODE5 = defect.D05_Code;
            tb.DEFECT_FROM5 = defect.D05_Origin;
            tb.DEFECT_SURF5 = defect.D05_Sid;
            tb.DEFECT_POSITION_WIDTH5 = defect.D05_Pos_W;
            tb.DEFECT_LEN_START5 = defect.D05_Pos_L_Start;
            tb.DEFECT_LEN_END5 = defect.D05_Pos_L_End;
            tb.DEFECT_CLASS5 = defect.D05_Level;
            tb.DEFECT_PERCENT5 = defect.D05_Percent;
            tb.DEFECT_QUALITY_GRADE5 = defect.D05_QGRADE;

            tb.DEFECT_CODE6 = defect.D06_Code;
            tb.DEFECT_FROM6 = defect.D06_Origin;
            tb.DEFECT_SURF6 = defect.D06_Sid;
            tb.DEFECT_POSITION_WIDTH6 = defect.D06_Pos_W;
            tb.DEFECT_LEN_START6 = defect.D06_Pos_L_Start;
            tb.DEFECT_LEN_END6 = defect.D06_Pos_L_End;
            tb.DEFECT_CLASS6 = defect.D06_Level;
            tb.DEFECT_PERCENT6 = defect.D06_Percent;
            tb.DEFECT_QUALITY_GRADE6 = defect.D06_QGRADE;

            tb.DEFECT_CODE7 = defect.D07_Code;
            tb.DEFECT_FROM7 = defect.D07_Origin;
            tb.DEFECT_SURF7 = defect.D07_Sid;
            tb.DEFECT_POSITION_WIDTH7 = defect.D07_Pos_W;
            tb.DEFECT_LEN_START7 = defect.D07_Pos_L_Start;
            tb.DEFECT_LEN_END7 = defect.D07_Pos_L_End;
            tb.DEFECT_CLASS7 = defect.D07_Level;
            tb.DEFECT_PERCENT7 = defect.D07_Percent;
            tb.DEFECT_QUALITY_GRADE7 = defect.D07_QGRADE;

            tb.DEFECT_CODE8 = defect.D08_Code;
            tb.DEFECT_FROM8 = defect.D08_Origin;
            tb.DEFECT_SURF8 = defect.D08_Sid;
            tb.DEFECT_POSITION_WIDTH8 = defect.D08_Pos_W;
            tb.DEFECT_LEN_START8 = defect.D08_Pos_L_Start;
            tb.DEFECT_LEN_END8 = defect.D08_Pos_L_End;
            tb.DEFECT_CLASS8 = defect.D08_Level;
            tb.DEFECT_PERCENT8 = defect.D08_Percent;
            tb.DEFECT_QUALITY_GRADE8 = defect.D08_QGRADE;

            tb.DEFECT_CODE9 = defect.D09_Code;
            tb.DEFECT_FROM9 = defect.D09_Origin;
            tb.DEFECT_SURF9 = defect.D09_Sid;
            tb.DEFECT_POSITION_WIDTH9 = defect.D09_Pos_W;
            tb.DEFECT_LEN_START9 = defect.D09_Pos_L_Start;
            tb.DEFECT_LEN_END9 = defect.D09_Pos_L_End;
            tb.DEFECT_CLASS9 = defect.D09_Level;
            tb.DEFECT_PERCENT9 = defect.D09_Percent;
            tb.DEFECT_QUALITY_GRADE9 = defect.D09_QGRADE;

            tb.DEFECT_CODE10 = defect.D10_Code;
            tb.DEFECT_FROM10 = defect.D10_Origin;
            tb.DEFECT_SURF10 = defect.D10_Sid;
            tb.DEFECT_POSITION_WIDTH10 = defect.D10_Pos_W;
            tb.DEFECT_LEN_START10 = defect.D10_Pos_L_Start;
            tb.DEFECT_LEN_END10 = defect.D10_Pos_L_End;
            tb.DEFECT_CLASS10 = defect.D10_Level;
            tb.DEFECT_PERCENT10 = defect.D10_Percent;
            tb.DEFECT_QUALITY_GRADE10 = defect.D10_QGRADE;

            return tb;
        }

        // TBL_PDO -> L2L25_CoilPDO
        public static L2L25_CoilPDO ToL25PDOEntity(this TBL_PDO dao, TBL_Coil_Defect defect, TBL_LookupTable_Paper paper, TBL_LookupTable_Sleeve sleeve)
        {
            var tb = new L2L25_CoilPDO();
            if (defect == null)
                defect = new TBL_Coil_Defect();

            tb.Message_Id = L25SysDef.MsgCode.Msg102PDO;
            tb.Message_Length = L25SysDef.MsgLength.Msg102PDO;
            tb.Date = DateTime.Now.ToString("yyyyMMdd");
            tb.Time = DateTime.Now.ToString("HHmmss");

            tb.ORDER_NO = dao.OrderNo;
            tb.PLAN_NO = dao.Plan_No;
            tb.OUT_MAT_NO = dao.Out_Coil_ID;
            tb.IN_MAT_NO = dao.In_Coil_ID;
            tb.START_PROD_TIME = dao.StartTime.ToString("yyyyMMddHHmmss");
            tb.END_PROD_TIME = dao.FinishTime.ToString("yyyyMMddHHmmss");
            tb.PROD_SHIFT_NO = dao.Shift;
            tb.PROD_SHIFT_GROUP = dao.Team;
            tb.OUT_MAT_ACT_OUTER_DIA = dao.Out_Coil_Outer_Diameter.ToString();
            tb.OUT_MAT_ACT_INNER_DIA = dao.Out_Coil_Inner.ToString();
            tb.OUT_MAT_ACT_WT = dao.Out_Coil_Wt.ToString();
            tb.OUT_MAT_GROSS_WT = dao.Out_Coil_Gross_WT.ToString();
            tb.OUT_MAT_ACT_THICK = dao.Out_Coil_Thick.ToString();
            tb.OUT_MAT_ACT_WIDTH = dao.Out_Coil_Width.ToString();
            tb.OUT_MAT_ACT_LEN = dao.Out_Coil_Length.ToString();
            tb.PAPER_CODE = dao.Paper_Code;
            tb.PAPER_REQ_CODE = dao.Paper_Req_Code;
            tb.HEAD_PAPER_LENGTH = dao.Out_Head_Paper_Length.ToString();
            tb.HEAD_PAPER_WIDTH = dao.Out_Head_Paper_Width.ToString();
            tb.TAIL_PAPER_LENGTH = dao.Out_Tail_Paper_Length.ToString();
            tb.TAIL_PAPER_WIDTH = dao.Out_Tail_Paper_Width.ToString();
            tb.SLEEVE_DIAMTER = dao.Sleeve_Inner_Exit_Diamter.ToString();
            tb.SLEEVE_TYPE_CODE = dao.Sleeve_Type_Exit_Code;
            tb.HEAD_HOLE_POSITION = dao.Head_Hole_Position.ToString();
            tb.HEAD_LEADER_LENGTH = dao.Head_Leader_Length.ToString();
            tb.HEAD_LEADER_WIDTH = dao.Head_Leader_Width.ToString();
            tb.HEAD_LEADER_THICK = dao.Head_Leader_Thickness.ToString();
            tb.TAIL_HOLE_POSITION = dao.Tail_PunchHole_Position.ToString();
            tb.TAIL_LEADER_LENGTH = dao.Tail_Leader_Length.ToString();
            tb.TAIL_LEADER_WIDTH = dao.Tail_Leader_Width.ToString();
            tb.TAIL_LEADER_THICK = dao.Tail_Leader_Thickness.ToString();
            tb.SCRAP_LEN_HEAD = dao.Scraped_Length_Entry.ToString();
            tb.SCRAP_LEN_TAIL = dao.Scraped_Length_Exit.ToString();
            tb.HEAD_LEADER_ST_NO = dao.Head_Leader_St_No;
            tb.TAIL_LEADER_ST_NO = dao.Tail_Leader_St_No;
            tb.WINDING_DIRE = dao.Winding_Direction;
            tb.BETTER_SURF_WARD_CODE = dao.Base_Surface;
            tb.HOLD_MAKER = dao.Inspector;
            tb.HOLD_FLAG = dao.Hold_Flag;
            tb.HOLD_CAUSE_CODE = dao.Hold_Cause_Code;
            tb.SAMPLE_FLAG = dao.Sample_Flag;
            tb.TRIM_FLAG = dao.Trim_Flag;
            tb.FIXED_WT_FLAG = dao.Fixed_WT_Flag;
            tb.FINAL_COIL_FLAG = dao.End_Flag;
            tb.SCRAP_FLAG = dao.Scrap_Flag;
            tb.SAMPLE_POS_CODE = dao.Sample_Frqn_Code;
            tb.NO_LEADER_CODE = dao.No_Leader_Code;
            tb.SURFACE_ACCU_CODE = dao.Surface_Accuracy_Code;
            tb.OFF_ROLL_LEN_HEAD = dao.Head_Off_Gauge.ToString();
            tb.OFF_ROLL_LEN_TAIL = dao.Tail_Off_Gauge.ToString();
            tb.SURFACE_ACCU_CODE_IN = dao.Surface_Accu_Code_In;
            tb.SURFACE_ACCU_CODE_OUT = dao.Surface_Accu_Code_Out;
            tb.FLIP_TAG = dao.Flip_Tag;
            tb.PROCESS_CODE = dao.Process_Code;
            tb.UNCOIL_DIRECTION = dao.Decoiler_Direction;
            tb.RECOILER_ACTTEN_AVG = dao.Recoiler_Actten_Avg.ToString();


            tb.DEFECT_CODE_1 = defect.D01_Code;
            tb.DEFECT_FROM_1 = defect.D01_Origin;
            tb.DEFECT_SURF_1 = defect.D01_Sid;
            tb.DEFECT_POSITION_WIDTH_1 = defect.D01_Pos_W;
            tb.DEFECT_LEN_START_1 = defect.D01_Pos_L_Start;
            tb.DEFECT_LEN_END_1 = defect.D01_Pos_L_End;
            tb.DEFECT_CLASS_1 = defect.D01_Level;
            tb.DEFECT_PERCENT_1 = defect.D01_Percent;
            tb.DEFECT_QUALITY_GRADE_1 = defect.D01_QGRADE;

            tb.DEFECT_CODE_2 = defect.D02_Code;
            tb.DEFECT_FROM_2 = defect.D02_Origin;
            tb.DEFECT_SURF_2 = defect.D02_Sid;
            tb.DEFECT_POSITION_WIDTH_2 = defect.D02_Pos_W;
            tb.DEFECT_LEN_START_2 = defect.D02_Pos_L_Start;
            tb.DEFECT_LEN_END_2 = defect.D02_Pos_L_End;
            tb.DEFECT_CLASS_2 = defect.D02_Level;
            tb.DEFECT_PERCENT_2 = defect.D02_Percent;
            tb.DEFECT_QUALITY_GRADE_2 = defect.D02_QGRADE;


            tb.DEFECT_CODE_3 = defect.D03_Code;
            tb.DEFECT_FROM_3 = defect.D03_Origin;
            tb.DEFECT_SURF_3 = defect.D03_Sid;
            tb.DEFECT_POSITION_WIDTH_3 = defect.D03_Pos_W;
            tb.DEFECT_LEN_START_3 = defect.D03_Pos_L_Start;
            tb.DEFECT_LEN_END_3 = defect.D03_Pos_L_End;
            tb.DEFECT_CLASS_3 = defect.D03_Level;
            tb.DEFECT_PERCENT_3 = defect.D03_Percent;
            tb.DEFECT_QUALITY_GRADE_3 = defect.D03_QGRADE;

            tb.DEFECT_CODE_4 = defect.D04_Code;
            tb.DEFECT_FROM_4 = defect.D04_Origin;
            tb.DEFECT_SURF_4 = defect.D04_Sid;
            tb.DEFECT_POSITION_WIDTH_4 = defect.D04_Pos_W;
            tb.DEFECT_LEN_START_4 = defect.D04_Pos_L_Start;
            tb.DEFECT_LEN_END_4 = defect.D04_Pos_L_End;
            tb.DEFECT_CLASS_4 = defect.D04_Level;
            tb.DEFECT_PERCENT_4 = defect.D04_Percent;
            tb.DEFECT_QUALITY_GRADE_4 = defect.D04_QGRADE;

            tb.DEFECT_CODE_5 = defect.D05_Code;
            tb.DEFECT_FROM_5 = defect.D05_Origin;
            tb.DEFECT_SURF_5 = defect.D05_Sid;
            tb.DEFECT_POSITION_WIDTH_5 = defect.D05_Pos_W;
            tb.DEFECT_LEN_START_5 = defect.D05_Pos_L_Start;
            tb.DEFECT_LEN_END_5 = defect.D05_Pos_L_End;
            tb.DEFECT_CLASS_5 = defect.D05_Level;
            tb.DEFECT_PERCENT_5 = defect.D05_Percent;
            tb.DEFECT_QUALITY_GRADE_5 = defect.D05_QGRADE;

            tb.DEFECT_CODE_6 = defect.D06_Code;
            tb.DEFECT_FROM_6 = defect.D06_Origin;
            tb.DEFECT_SURF_6 = defect.D06_Sid;
            tb.DEFECT_POSITION_WIDTH_6 = defect.D06_Pos_W;
            tb.DEFECT_LEN_START_6 = defect.D06_Pos_L_Start;
            tb.DEFECT_LEN_END_6 = defect.D06_Pos_L_End;
            tb.DEFECT_CLASS_6 = defect.D06_Level;
            tb.DEFECT_PERCENT_6 = defect.D06_Percent;
            tb.DEFECT_QUALITY_GRADE_6 = defect.D06_QGRADE;

            tb.DEFECT_CODE_7 = defect.D07_Code;
            tb.DEFECT_FROM_7 = defect.D07_Origin;
            tb.DEFECT_SURF_7 = defect.D07_Sid;
            tb.DEFECT_POSITION_WIDTH_7 = defect.D07_Pos_W;
            tb.DEFECT_LEN_START_7 = defect.D07_Pos_L_Start;
            tb.DEFECT_LEN_END_7 = defect.D07_Pos_L_End;
            tb.DEFECT_CLASS_7 = defect.D07_Level;
            tb.DEFECT_PERCENT_7 = defect.D07_Percent;
            tb.DEFECT_QUALITY_GRADE_7 = defect.D07_QGRADE;

            tb.DEFECT_CODE_8 = defect.D08_Code;
            tb.DEFECT_FROM_8 = defect.D08_Origin;
            tb.DEFECT_SURF_8 = defect.D08_Sid;
            tb.DEFECT_POSITION_WIDTH_8 = defect.D08_Pos_W;
            tb.DEFECT_LEN_START_8 = defect.D08_Pos_L_Start;
            tb.DEFECT_LEN_END_8 = defect.D08_Pos_L_End;
            tb.DEFECT_CLASS_8 = defect.D08_Level;
            tb.DEFECT_PERCENT_8 = defect.D08_Percent;
            tb.DEFECT_QUALITY_GRADE_8 = defect.D08_QGRADE;

            tb.DEFECT_CODE_9 = defect.D09_Code;
            tb.DEFECT_FROM_9 = defect.D09_Origin;
            tb.DEFECT_SURF_9 = defect.D09_Sid;
            tb.DEFECT_POSITION_WIDTH_9 = defect.D09_Pos_W;
            tb.DEFECT_LEN_START_9 = defect.D09_Pos_L_Start;
            tb.DEFECT_LEN_END_9 = defect.D09_Pos_L_End;
            tb.DEFECT_CLASS_9 = defect.D09_Level;
            tb.DEFECT_PERCENT_9 = defect.D09_Percent;
            tb.DEFECT_QUALITY_GRADE_9 = defect.D09_QGRADE;

            tb.DEFECT_CODE_A = defect.D10_Code;
            tb.DEFECT_FROM_A = defect.D10_Origin;
            tb.DEFECT_SURF_A = defect.D10_Sid;
            tb.DEFECT_POSITION_WIDTH_A = defect.D10_Pos_W;
            tb.DEFECT_LEN_START_A = defect.D10_Pos_L_Start;
            tb.DEFECT_LEN_END_A = defect.D10_Pos_L_End;
            tb.DEFECT_CLASS_A = defect.D10_Level;
            tb.DEFECT_PERCENT_A = defect.D10_Percent;
            tb.DEFECT_QUALITY_GRADE_A = defect.D10_QGRADE;
            tb.PAPER_DENSITY = paper == null ? "" : paper?.Paper_Base_Weight.ToString();
            tb.SLEEVE_WEIGHT = sleeve == null ? "" : sleeve?.Sleeve_Weight.ToString();

            return tb;

        }

        // 201-> L2L25_CPLPRESET
        public static L2L25_CPLPRESET ToL25PresetRecordEntity(this Msg_201_Preset msg)
        {
            var tb = new L2L25_CPLPRESET();

            tb.Message_Id = L25SysDef.MsgCode.Msg110CPLPRESET;
            tb.Message_Length = L25SysDef.MsgLength.Msg110CPLPRESET;
            tb.Date = DateTime.Now.ToString("yyyyMMdd");
            tb.Time = DateTime.Now.ToString("HHmmss");

            tb.CoilID = msg.CoilIDNo;
            tb.Steel_Grade = msg.SteelGrade.ToStr();
            tb.Thickness = msg.Thickness.ToString();
            tb.Width = msg.Width.ToString();
            tb.Entry_Yield_Stress = msg.EntryYieldStress.ToString();
            tb.Density = msg.Density.ToString();
            tb.CoilLength = msg.CoilLength.ToString();
            tb.CoilWeight = msg.CoilWeight.ToString();
            tb.ProcessCode = msg.ProcessCode.ToStr();
            tb.InnerDiam = msg.InnerDiam.ToString();
            tb.Diameter = msg.Diameter.ToString();
            tb.SleeveCodeEntry = msg.SleeveCodeEntry.ToString();
            tb.SleeveDmEntry = msg.SleeveDmEntry.ToString();
            tb.PaperWinder_Flag = msg.PaperWinderFlag.ToString();
            tb.SleeveCodeExit = msg.SleeveCodeExit.ToString();
            tb.SleeveDmExit = msg.SleeveDmExit.ToString();
            tb.PaperTypeExit = msg.PaperTypeExit.ToString();
            tb.PaperCodeExit = msg.PaperCodeExit.ToString();
            tb.Flatener_Depth_1 = msg.FlatenerDepth1.ToString();
            tb.Flatener_Depth_2 = msg.FlatenerDepth2.ToString();
            tb.Uncoiler_Tension = msg.UncoilerTension.ToString();
            tb.Uncoiler_Tension_Max = msg.UncoilerTensionMax.ToString();
            tb.Uncoiler_Tension_Min = msg.UncoilerTensionMin.ToString();
            tb.head_leader_strip_Length = msg.HeadLeaderStripLength.ToString();
            tb.head_leader_strip_thickness = msg.HeadLeaderStripThickness.ToString();
            tb.head_leader_strip_width = msg.HeadLeaderStripWidth.ToString();
            tb.head_leader_strip_steel_grade = msg.HeadLeaderStripSteelGrade.ToString();
            tb.tail_leader_strip_Length = msg.TailLeaderStripLength.ToString();
            tb.tail_leader_strip_thickness = msg.TailLeaderStripThickness.ToString();
            tb.tail_leader_strip_width = msg.TailLeaderStripWidth.ToString();
            tb.tail_leader_strip_steel_grade = msg.TailLeaderStripSteelGrade.ToString();
            tb.Side_Trimmer_Gap = msg.SideTrimmerGap.ToString();
            tb.Side_Trimmer_Lap = msg.SideTrimmerLap.ToString();
            tb.Side_Trimmer_Width = msg.SideTrimmerWidth.ToString();
            tb.tension_unit_depth = msg.TensionUnitDepth.ToString();
            tb.Recoiler_Tension = msg.RecoilerTension.ToString();
            tb.Recoiler_Tension_Max = msg.RecoilerTensionMax.ToString();
            tb.Recoiler_Tension_Min = msg.RecoilerTensionMin.ToString();
            tb.PaperUnwinder_Flag = msg.PaperUnwinderFlag.ToString();
            tb.CoilSplit = msg.CoilSplit.ToString();
            tb.Split_Weight_1 = msg.Orderwt_1.ToString();
            tb.Split_Weight_2 = msg.Orderwt_2.ToString();
            tb.Split_Weight_3 = msg.Orderwt_3.ToString();
            tb.Split_Weight_4 = msg.Orderwt_4.ToString();
            tb.Split_Weight_5 = msg.Orderwt_5.ToString();
            tb.Split_Weight_6 = msg.Orderwt_6.ToString();
            tb.PrrPosId = msg.PrrPosId.ToString();

            tb.Defect_1_code = msg.Defect1Code.ToStr();
            tb.Defect_1_start_position = msg.Defect1StartPosition.ToString();
            tb.Defect_1_end_position = msg.Defect1EndPosition.ToString();
            tb.Defect_2_code = msg.Defect2Code.ToStr();
            tb.Defect_2_start_position = msg.Defect2StartPosition.ToString();
            tb.Defect_2_end_position = msg.Defect2EndPosition.ToString();
            tb.Defect_3_code = msg.Defect3Code.ToStr();
            tb.Defect_3_start_position = msg.Defect3StartPosition.ToString();
            tb.Defect_3_end_position = msg.Defect3EndPosition.ToString();
            tb.Defect_4_code = msg.Defect4Code.ToStr();
            tb.Defect_4_start_position = msg.Defect4StartPosition.ToString();
            tb.Defect_4_end_position = msg.Defect4EndPosition.ToString();
            tb.Defect_5_code = msg.Defect5Code.ToStr();
            tb.Defect_5_start_position = msg.Defect5StartPosition.ToString();
            tb.Defect_5_end_position = msg.Defect5EndPosition.ToString();
            tb.Defect_6_code = msg.Defect6Code.ToStr();
            tb.Defect_6_start_position = msg.Defect6StartPosition.ToString();
            tb.Defect_6_end_position = msg.Defect6EndPosition.ToString();
            tb.Defect_7_code = msg.Defect7Code.ToStr();
            tb.Defect_7_start_position = msg.Defect7StartPosition.ToString();
            tb.Defect_7_end_position = msg.Defect7EndPosition.ToString();
            tb.Defect_8_code = msg.Defect8Code.ToStr();
            tb.Defect_8_start_position = msg.Defect8StartPosition.ToString();
            tb.Defect_8_end_position = msg.Defect8EndPosition.ToString();
            tb.Defect_9_code = msg.Defect9Code.ToStr();
            tb.Defect_9_start_position = msg.Defect9StartPosition.ToString();
            tb.Defect_9_end_position = msg.Defect9EndPosition.ToString();
            tb.Defect_10_code = msg.Defect10Code.ToStr();
            tb.Defect_10_start_position = msg.Defect10StartPosition.ToString();
            tb.Defect_10_end_position = msg.Defect10EndPosition.ToString();

            return tb;
        }

        // TBL_CoilMap -> L2L25_CoilMap
        public static L2L25_CoilMap ToL25CoilMapEntity(this TBL_CoilMap dao)
        {
            var entity = new L2L25_CoilMap();
            entity.Message_Id = L25SysDef.MsgCode.Msg111CoilMap;
            entity.Message_Length = L25SysDef.MsgLength.Msg111CoilMap;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");
            entity.Time = DateTime.Now.ToString("HHmmss");

            entity.CoilIDReCoiler = dao.TR;
            entity.CoilIDRecSkid1 = dao.Delivery_SK01;
            entity.CoilIDRecSkid2 = dao.Delivery_SK02;
            entity.CoilIDRecTop = dao.Delivery_TOP;
            entity.CoilIDCarExit = dao.Delivery_Car;

            entity.EntryTOPPhotoSensor = string.Empty;

            entity.CoilIDUnCoiler = dao.POR;
            entity.CoilIDUnSkid1 = dao.Entry_SK01;
            entity.CoilIDUnSkid2 = dao.Entry_SK02;
            entity.CoilIDUnTop = dao.Entry_TOP;
            entity.CoilIDCarEntry = dao.Entry_Car;

            entity.ExitTOPPhotoSensor = string.Empty;

            return entity;
        }


        // 316 -> L2L25_ENGC
        public static L2L25_ENGC ToL25EngcEntity(this Msg_316_Utility_Data msg)
        {
            var entity = new L2L25_ENGC();

            entity.Message_Id = L25SysDef.MsgCode.Msg104ENGC;
            entity.Message_Length = L25SysDef.MsgLength.Msg104ENGC;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");
            entity.Time = DateTime.Now.ToString("HHmmss");
            entity.CompressedAir = msg.CompressedAir.ToString();
            entity.IndirectCollingWater = msg.IndirectCollingWater.ToString();
            return entity;

        }

        // LineFaultRecord -> L2L25_DownTime
        public static L2L25_DownTime ToL25DownTimeEntity(this LineFaultRecord dao)
        {
            var entity = new L2L25_DownTime();

            entity.Message_Id = L25SysDef.MsgCode.Msg103DownTime;
            entity.Message_Length = L25SysDef.MsgLength.Msg103DownTime;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");
            entity.Time = DateTime.Now.ToString("HHmmss");


            entity.OP_FLAG = "1";   //1:暫時只做新增
            entity.UNIT_CODE = dao.unit_code;
            entity.PROD_DATE = dao.prod_time.ToString("yyyyMMdd");
            entity.PROD_SHIFT_NO = dao.prod_shift_no;
            entity.PROD_SHIFT_GROUP = dao.prod_shift_group;
            entity.STOP_START_TIME = dao.stop_start_time.ToString("yyyyMMddHHmmss");
            entity.STOP_END_TIME = dao.stop_end_time.ToString("yyyyMMddHHmmss");
            entity.DELAY_LOCATION = dao.delay_location;
            entity.DELAY_LOCATION_DESC = dao.delay_location_desc;
            entity.STOP_ELASED_TIME = dao.stop_elased_timey;
            entity.STOP_REASON = dao.delay_reason_code;
            entity.DELAY_REASON_DESC = dao.delay_reason_desc;
            entity.DELAY_REMARK = dao.delay_remark;
            entity.RESP_DEPART_DELAY_TIME_M = dao.resp_depart_delay_time_m;
            entity.RESP_DEPART_DELAY_TIME_E = dao.resp_depart_delay_time_e;
            entity.RESP_DEPART_DELAY_TIME_C = dao.resp_depart_delay_time_c;
            entity.RESP_DEPART_DELAY_TIME_P = dao.resp_depart_delay_time_p;
            entity.RESP_DEPART_DELAY_TIME_U = dao.resp_depart_delay_time_u;
            entity.RESP_DEPART_DELAY_TIME_O = dao.resp_depart_delay_time_o;
            entity.RESP_DEPART_DELAY_TIME_R = dao.resp_depart_delay_time_r;
            entity.RESP_DEPART_DELAY_TIME_RS = dao.resp_depart_delay_time_rs;
            entity.DECELERATION_CAUSE = dao.deceleration_cause;
            entity.DECELERATION_CODE = dao.deceleration_code;

            return entity;

    }


        // PDO, ProcessCTModel -> L2L25_SpeedCT
        public static L2L25_SpeedCT ToL25SpeedCTEntity(this PDO pdo, ProcessCTModel data)
        {
            var entity = new L2L25_SpeedCT();
        
            entity.Message_Id = L25SysDef.MsgCode.Msg105SpeedCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg105SpeedCT;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");
            entity.Time = DateTime.Now.ToString("HHmmss");

            entity.out_mat_no = pdo.Out_Coil_ID;
            entity.lineCode = L2SystemDef.CPLGroup;
            entity.seqUnit = CTData.SeqUnit.Speed;
            entity.dataCode = CTData.Code.Speed;
            entity.dataDesc = CTData.Desc.Speed;
            entity.resultUnit = CTData.ResultUnit.Speed;
            entity.genBeginDate = pdo.StartTime.ToString(DBParaDef.DB25DateFromat);
            entity.genBeginTime = pdo.StartTime.ToString(DBParaDef.DB25TimeFromat);
            entity.genStopDate = pdo.FinishTime.ToString(DBParaDef.DB25DateFromat);
            entity.genStopTime = pdo.FinishTime.ToString(DBParaDef.DB25TimeFromat);
            entity.frequency = CTData.Frenquency.FSecond;
            entity.DataCount = data.DataCnt.ToString();
            entity.DataString = data.SpeedStr;
            entity.plan_no = pdo.Plan_No;

            return entity;
        }

        // PDO, ProcessCTModel -> L2L25_UNCTensionCT (Act)
        public static L2L25_UNCTensionCT ToL25UNCTensionActCTEntity(this PDO pdo, ProcessCTModel data)
        {
            var entity = new L2L25_UNCTensionCT();

            entity.Message_Id = L25SysDef.MsgCode.Msg106UNCTensionCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg106UNCTensionCT;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");
            entity.Time = DateTime.Now.ToString("HHmmss");

            entity.out_mat_no = pdo.Out_Coil_ID;
            entity.lineCode = L2SystemDef.CPLGroup;
            entity.seqUnit = CTData.SeqUnit.UNCTension;
            entity.dataCode = CTData.Code.UNCTensionAct;
            entity.dataDesc = CTData.Desc.Tension;
            entity.resultUnit = CTData.ResultUnit.UNCTension;
            entity.genBeginDate = pdo.StartTime.ToString(DBParaDef.DB25DateFromat);
            entity.genBeginTime = pdo.StartTime.ToString(DBParaDef.DB25TimeFromat);
            entity.genStopDate = pdo.FinishTime.ToString(DBParaDef.DB25DateFromat);
            entity.genStopTime = pdo.FinishTime.ToString(DBParaDef.DB25TimeFromat);
            entity.frequency = CTData.Frenquency.FSecond;
            entity.DataCount = data.DataCnt.ToString();
            entity.DataString = data.PorTensionActStr;
            entity.plan_no = pdo.Plan_No;

            return entity;
        }

        // PDO, ProcessCTModel -> L2L25_UNCTensionCT (Ref)
        public static L2L25_UNCTensionCT ToL25UNCTensionRefCTEntity(this PDO pdo, ProcessCTModel data)
        {
            var entity = new L2L25_UNCTensionCT();

            entity.Message_Id = L25SysDef.MsgCode.Msg106UNCTensionCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg106UNCTensionCT;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");
            entity.Time = DateTime.Now.ToString("HHmmss");

            entity.out_mat_no = pdo.Out_Coil_ID;
            entity.lineCode = L2SystemDef.CPLGroup;
            entity.seqUnit = CTData.SeqUnit.UNCTension;
            entity.dataCode = CTData.Code.UNCTensionRef;
            entity.dataDesc = CTData.Desc.Tension;
            entity.resultUnit = CTData.ResultUnit.UNCTension;
            entity.genBeginDate = pdo.StartTime.ToString(DBParaDef.DB25DateFromat);
            entity.genBeginTime = pdo.StartTime.ToString(DBParaDef.DB25TimeFromat);
            entity.genStopDate = pdo.FinishTime.ToString(DBParaDef.DB25DateFromat);
            entity.genStopTime = pdo.FinishTime.ToString(DBParaDef.DB25TimeFromat);
            entity.frequency = CTData.Frenquency.FSecond;
            entity.DataCount = data.DataCnt.ToString();
            entity.DataString = data.PorTensionRefStr;
            entity.plan_no = pdo.Plan_No;

            return entity;
        }

        // PDO, ProcessCTModel -> L2L25_UNCCurrentCT
        public static L2L25_UNCCurrentCT ToL25UNCCurrentCTEntity(this PDO pdo, ProcessCTModel data)
        {
            var entity = new L2L25_UNCCurrentCT();

            entity.Message_Id = MsgCode.Msg107UNCCurrentCT;
            entity.Message_Length = MsgLength.Msg107UNCCurrentCT;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");
            entity.Time = DateTime.Now.ToString("HHmmss");

            entity.out_mat_no = pdo.Out_Coil_ID;
            entity.lineCode = L2SystemDef.CPLGroup;
            entity.seqUnit = CTData.SeqUnit.UNCCurrent;
            entity.dataCode = CTData.Code.UNCCurrent;
            entity.dataDesc = CTData.Desc.Tension;
            entity.resultUnit = CTData.ResultUnit.UNCCurrent;
            entity.genBeginDate = pdo.StartTime.ToString(DBParaDef.DB25DateFromat);
            entity.genBeginTime = pdo.StartTime.ToString(DBParaDef.DB25TimeFromat);
            entity.genStopDate = pdo.FinishTime.ToString(DBParaDef.DB25DateFromat);
            entity.genStopTime = pdo.FinishTime.ToString(DBParaDef.DB25TimeFromat);
            entity.frequency = CTData.Frenquency.FSecond;
            entity.DataCount = data.DataCnt.ToString();
            entity.DataString = data.PorCurrentStr;
            entity.plan_no = pdo.Plan_No;

            return entity;
        }

        // PDO, ProcessCTModel -> L2L25_RECTensionCT (Act)
        public static L2L25_RECTensionCT ToL25RECTensionActCTEntity(this PDO pdo, ProcessCTModel data)
        {
            var entity = new L2L25_RECTensionCT();

            entity.Message_Id = L25SysDef.MsgCode.Msg108RECTensionCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg108RECTensionCT;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");
            entity.Time = DateTime.Now.ToString("HHmmss");

            entity.out_mat_no = pdo.Out_Coil_ID;
            entity.lineCode = L2SystemDef.CPLGroup;
            entity.seqUnit = CTData.SeqUnit.RECTension;
            entity.dataCode = CTData.Code.RECTensionAct;
            entity.dataDesc = CTData.Desc.Tension;
            entity.resultUnit = CTData.ResultUnit.RECTension;
            entity.genBeginDate = pdo.StartTime.ToString(DBParaDef.DB25DateFromat);
            entity.genBeginTime = pdo.StartTime.ToString(DBParaDef.DB25TimeFromat);
            entity.genStopDate = pdo.FinishTime.ToString(DBParaDef.DB25DateFromat);
            entity.genStopTime = pdo.FinishTime.ToString(DBParaDef.DB25TimeFromat);
            entity.frequency = CTData.Frenquency.FSecond;
            entity.DataCount = data.DataCnt.ToString();
            entity.DataString = data.TrTensionActStr;
            entity.plan_no = pdo.Plan_No;

            return entity;
        }

        // PDO, ProcessCTModel -> L2L25_RECTensionCT (Ref)
        public static L2L25_RECTensionCT ToL25RECTensionRefCTEntity(this PDO pdo, ProcessCTModel data)
        {
            var entity = new L2L25_RECTensionCT();

            entity.Message_Id = L25SysDef.MsgCode.Msg108RECTensionCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg108RECTensionCT;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");
            entity.Time = DateTime.Now.ToString("HHmmss");

            entity.out_mat_no = pdo.Out_Coil_ID;
            entity.lineCode = L2SystemDef.CPLGroup;
            entity.seqUnit = CTData.SeqUnit.RECTension;
            entity.dataCode = CTData.Code.RECTensionRef;
            entity.dataDesc = CTData.Desc.Tension;
            entity.resultUnit = CTData.ResultUnit.RECTension;
            entity.genBeginDate = pdo.StartTime.ToString(DBParaDef.DB25DateFromat);
            entity.genBeginTime = pdo.StartTime.ToString(DBParaDef.DB25TimeFromat);
            entity.genStopDate = pdo.FinishTime.ToString(DBParaDef.DB25DateFromat);
            entity.genStopTime = pdo.FinishTime.ToString(DBParaDef.DB25TimeFromat);
            entity.frequency = CTData.Frenquency.FSecond;
            entity.DataCount = data.DataCnt.ToString();
            entity.DataString = data.TrTensionRefStr;
            entity.plan_no = pdo.Plan_No;

            return entity;
        }

        // PDO, ProcessCTModel -> L2L25_RECCurrentCT
        public static L2L25_RECCurrentCT ToL25RECCurrentCTEntity(this PDO pdo, ProcessCTModel data)
        {
            var entity = new L2L25_RECCurrentCT();

            entity.Message_Id = L25SysDef.MsgCode.Msg109RECCurrentCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg109RECCurrentCT;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");
            entity.Time = DateTime.Now.ToString("HHmmss");

            entity.out_mat_no = pdo.Out_Coil_ID;
            entity.lineCode = L2SystemDef.CPLGroup;
            entity.seqUnit = CTData.SeqUnit.RECCurrent;
            entity.dataCode = CTData.Code.RECCurrent;
            entity.dataDesc = CTData.Desc.Current;
            entity.resultUnit = CTData.ResultUnit.RECCurrent;
            entity.genBeginDate = pdo.StartTime.ToString(DBParaDef.DB25DateFromat);
            entity.genBeginTime = pdo.StartTime.ToString(DBParaDef.DB25TimeFromat);
            entity.genStopDate = pdo.FinishTime.ToString(DBParaDef.DB25DateFromat);
            entity.genStopTime = pdo.FinishTime.ToString(DBParaDef.DB25TimeFromat);
            entity.frequency = CTData.Frenquency.FSecond;
            entity.DataCount = data.DataCnt.ToString();
            entity.DataString = data.TrCurrentStr;
            entity.plan_no = pdo.Plan_No;

            return entity;
        }

        

        // PDO, ProcessCTWeldModel -> L2L25_WeldCT (WELD_Current_Actual_Front)
        public static L2L25_WeldCT ToL25WeldCTEntity_WeldCurrActF(this PDO pdo, ProcessCTWeldModel data)
        {
            var entity = new L2L25_WeldCT();

            entity.Message_Id = L25SysDef.MsgCode.Msg116WeldCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg116WeldCT;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");
            entity.Time = DateTime.Now.ToString("HHmmss");

            entity.out_mat_no = pdo.Out_Coil_ID;
            entity.lineCode = L2SystemDef.CPLGroup;
            entity.seqUnit = CTData.SeqUnit.WELD_Current_Actual_Front;
            entity.dataCode = CTData.Code.WELD_Current_Actual_Front;
            entity.dataDesc = CTData.Desc.Weld;
            entity.resultUnit = CTData.ResultUnit.WELD_Current_Actual_Front;
            entity.genBeginDate = pdo.StartTime.ToString(DBParaDef.DB25DateFromat);
            entity.genBeginTime = pdo.StartTime.ToString(DBParaDef.DB25TimeFromat);
            entity.genStopDate = pdo.FinishTime.ToString(DBParaDef.DB25DateFromat);
            entity.genStopTime = pdo.FinishTime.ToString(DBParaDef.DB25TimeFromat);
            entity.frequency = CTData.Frenquency.FSecond;
            entity.DataCount = data.DataCnt.ToString();
            entity.DataString = data.WeldCurrentActFStr;
            entity.plan_no = pdo.Plan_No;
            entity.pass_no = data.PassNo.ToString();

            return entity;
        }

        // PDO, ProcessCTWeldModel -> L2L25_WeldCT (WELD_Current_Actual_Rear)
        public static L2L25_WeldCT ToL25WeldCTEntity_WeldCurrActR(this PDO pdo, ProcessCTWeldModel data)
        {
            var entity = new L2L25_WeldCT();

            entity.Message_Id = L25SysDef.MsgCode.Msg116WeldCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg116WeldCT;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");
            entity.Time = DateTime.Now.ToString("HHmmss");

            entity.out_mat_no = pdo.Out_Coil_ID;
            entity.lineCode = L2SystemDef.CPLGroup;
            entity.seqUnit = CTData.SeqUnit.WELD_Current_Actual_Rear;
            entity.dataCode = CTData.Code.WELD_Current_Actual_Rear;
            entity.dataDesc = CTData.Desc.Weld;
            entity.resultUnit = CTData.ResultUnit.WELD_Current_Actual_Rear;
            entity.genBeginDate = pdo.StartTime.ToString(DBParaDef.DB25DateFromat);
            entity.genBeginTime = pdo.StartTime.ToString(DBParaDef.DB25TimeFromat);
            entity.genStopDate = pdo.FinishTime.ToString(DBParaDef.DB25DateFromat);
            entity.genStopTime = pdo.FinishTime.ToString(DBParaDef.DB25TimeFromat);
            entity.frequency = CTData.Frenquency.FSecond;
            entity.DataCount = data.DataCnt.ToString();
            entity.DataString = data.WeldCurrentActRStr;
            entity.plan_no = pdo.Plan_No;
            entity.pass_no = data.PassNo.ToString();

            return entity;
        }

        // PDO, ProcessCTWeldModel -> L2L25_WeldCT (WELD_Speed_Actual)
        public static L2L25_WeldCT ToL25WeldCTEntity_WeldSpeedAct(this PDO pdo, ProcessCTWeldModel data)
        {
            var entity = new L2L25_WeldCT();

            entity.Message_Id = L25SysDef.MsgCode.Msg116WeldCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg116WeldCT;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");
            entity.Time = DateTime.Now.ToString("HHmmss");

            entity.out_mat_no = pdo.Out_Coil_ID;
            entity.lineCode = L2SystemDef.CPLGroup;
            entity.seqUnit = CTData.SeqUnit.WELD_Speed_Actual;
            entity.dataCode = CTData.Code.WELD_Speed_Actual;
            entity.dataDesc = CTData.Desc.Weld;
            entity.resultUnit = CTData.ResultUnit.WELD_Speed_Actual;
            entity.genBeginDate = pdo.StartTime.ToString(DBParaDef.DB25DateFromat);
            entity.genBeginTime = pdo.StartTime.ToString(DBParaDef.DB25TimeFromat);
            entity.genStopDate = pdo.FinishTime.ToString(DBParaDef.DB25DateFromat);
            entity.genStopTime = pdo.FinishTime.ToString(DBParaDef.DB25TimeFromat);
            entity.frequency = CTData.Frenquency.FSecond;
            entity.DataCount = data.DataCnt.ToString();
            entity.DataString = data.WeldSpeedActStr;
            entity.plan_no = pdo.Plan_No;
            entity.pass_no = data.PassNo.ToString();

            return entity;
        }

        // PDO, ProcessCTWeldModel -> L2L25_WeldCT (WELD_PlanishWheelForce_Actual)
        public static L2L25_WeldCT ToL25WeldCTEntity_WeldPlanushWFAct(this PDO pdo, ProcessCTWeldModel data)
        {
            var entity = new L2L25_WeldCT();

            entity.Message_Id = L25SysDef.MsgCode.Msg116WeldCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg116WeldCT;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");
            entity.Time = DateTime.Now.ToString("HHmmss");

            entity.out_mat_no = pdo.Out_Coil_ID;
            entity.lineCode = L2SystemDef.CPLGroup;
            entity.seqUnit = CTData.SeqUnit.WELD_PlanishWheelForce_Actual;
            entity.dataCode = CTData.Code.WELD_PlanishWheelForce_Actual;
            entity.dataDesc = CTData.Desc.Weld;
            entity.resultUnit = CTData.ResultUnit.WELD_PlanishWheelForce_Actual;
            entity.genBeginDate = pdo.StartTime.ToString(DBParaDef.DB25DateFromat);
            entity.genBeginTime = pdo.StartTime.ToString(DBParaDef.DB25TimeFromat);
            entity.genStopDate = pdo.FinishTime.ToString(DBParaDef.DB25DateFromat);
            entity.genStopTime = pdo.FinishTime.ToString(DBParaDef.DB25TimeFromat);
            entity.frequency = CTData.Frenquency.FSecond;
            entity.DataCount = data.DataCnt.ToString();
            entity.DataString = data.WeldPlanishWFActStr;
            entity.plan_no = pdo.Plan_No;
            entity.pass_no = data.PassNo.ToString();

            return entity;
        }

        // PDO, ProcessCTWeldModel -> L2L25_WeldCT (WELD_Temperture)
        public static L2L25_WeldCT ToL25WeldCTEntity_WeldTempertureAct(this PDO pdo, ProcessCTWeldModel data)
        {
            var entity = new L2L25_WeldCT();

            entity.Message_Id = L25SysDef.MsgCode.Msg116WeldCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg116WeldCT;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");
            entity.Time = DateTime.Now.ToString("HHmmss");

            entity.out_mat_no = pdo.Out_Coil_ID;
            entity.lineCode = L2SystemDef.CPLGroup;
            entity.seqUnit = CTData.SeqUnit.WELD_Temperture;
            entity.dataCode = CTData.Code.WELD_Temperture;
            entity.dataDesc = CTData.Desc.Weld;
            entity.resultUnit = CTData.ResultUnit.WELD_Temperture;
            entity.genBeginDate = pdo.StartTime.ToString(DBParaDef.DB25DateFromat);
            entity.genBeginTime = pdo.StartTime.ToString(DBParaDef.DB25TimeFromat);
            entity.genStopDate = pdo.FinishTime.ToString(DBParaDef.DB25DateFromat);
            entity.genStopTime = pdo.FinishTime.ToString(DBParaDef.DB25TimeFromat);
            entity.frequency = CTData.Frenquency.FSecond;
            entity.DataCount = data.DataCnt.ToString();
            entity.DataString = data.WeldTempertureStr;
            entity.plan_no = pdo.Plan_No;
            entity.pass_no = data.PassNo.ToString();

            return entity;
        }

        public static L2L25_CoilRejectResult ToL25CoilRejectResult(this ReturnCoilInfo returnCoilInfo)
        {
            var entity = new L2L25_CoilRejectResult();

            entity.Message_Id = L25SysDef.MsgCode.Msg115CoilRejectResult;
            entity.Message_Length = L25SysDef.MsgLength.Msg115CoilRejectResult;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");
            entity.Time = DateTime.Now.ToString("HHmmss");

            entity.OUT_MAT_NO = returnCoilInfo.Coil_ID;

            entity.IN_MAT_NO = returnCoilInfo.Entry_Coil_ID;
            entity.MAT_RETURN_MODE = returnCoilInfo.Mode_Of_Reject;
            entity.RETURN_MAT_LEN = returnCoilInfo.Length_Of_Rejected_Coil;
            entity.RETURN_MAT_WT = returnCoilInfo.Weight_Of_Rejected_Coil;
            entity.RETURN_MAT_OUTER_DIA = returnCoilInfo.Outer_Diameter_Of_RejectedCoil;
            entity.OUT_MAT_INNER_DIA = returnCoilInfo.Inner_Diameter_Of_RejectedCoil;
            entity.MAT_RETURN_CAUSE_CODE = returnCoilInfo.Reason_Of_Reject;

            entity.RETURN_TIME = returnCoilInfo.Time_Of_Reject;
            entity.PROD_SHIFT_NO = returnCoilInfo.Shift_Of_Reject;
            entity.PROD_SHIFT_GROUP = returnCoilInfo.Turn_Of_Reject;

            return entity;
        }
    }
}
