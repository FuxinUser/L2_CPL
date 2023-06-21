using Core.Define;
using Core.Util;
using DataMod.PLC;
using DBService.Repository;
using MsgStruct;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using static DataMod.Response.RespnseModel;

namespace MsgConvert
{
    public class L1MsgFactory
    {
        public static L2L1Snd.Msg_201_Preset Preset201Msg(PDI pdi, GenCoilInfoModel.GenPreset201LkTableInfo lkTableData, DefectData defect, int prrPosId)
        {


            if (defect == null)
                defect = new DefectData();

            var preset201 = new L2L1Snd.Msg_201_Preset()
            {
                #region Header
                MessageId = short.Parse(PlcSysDef.SndMsgCode.L1201Preset),
                MessageLength = (short)Marshal.SizeOf<L2L1Snd.Msg_201_Preset>(),
                Date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd")),
                Time = Int32.Parse(DateTime.Now.ToString("HHmmss")),
                #endregion
                // From PDI
                Coil_ID = pdi.Entry_Coil_ID.ToCByteArray(20).ToL1EndSignByteArray(4),
                SteelGrade = pdi.St_No.ToCByteArray(20).ToL1EndSignByteArray(4),
                Thickness = pdi.Entry_Coil_Thick,
                Width = pdi.Entry_Coil_Width,
                EntryYieldStress = pdi.EntryYieldStress,
                Density = pdi.Density,
                CoilLength = pdi.Entry_Coil_Length,
                CoilWeight = pdi.Entry_Coil_Weight,
                ProcessCode = pdi.Process_Code.ToCByteArray(4).ToL1EndSignByteArray(4),
                InnerDiam = pdi.Entry_Coil_Inner,
                Diameter = pdi.Entry_Coil_Dcos,
                SleeveCodeEntry = pdi.Sleeve_Type_Code.ToNullable<int>() ?? 0,
                SleeveDmEntry = pdi.Sleeve_diamter,
                //PaperWinderFlag = pdi.Paper_Code.ToNullable<int>()??0,
                PaperWinderFlag = pdi.Paper_Req_Code.Equals("0")?0:1,
                SleeveCodeExit = pdi.Out_Sleeve_Type_Code.ToNullable<int>() ?? 0,
                SleeveDmExit = pdi.Out_Sleeve_Diamter,
                PaperTypeExit = pdi.Out_Paper_Req_Code.Equals("0") ? 0 : 1,
                PaperCodeExit = pdi.Out_Paper_Code.ToNullable<int>() ?? 0,
                // From L2 Lookup Table 
                FlatenerDepth1 = lkTableData.FlatenerDepth1,
                FlatenerDepth2 = lkTableData.FlatenerDepth2,

                //KN 總張力=单位张力*入料鋼捲厚度*入料鋼捲寬度
                UncoilerTension = (lkTableData.UncoilerTension * pdi.Entry_Coil_Thick * pdi.Entry_Coil_Width )/ 1000,                                   
                UncoilerTensionMax = DeviceParaDef.UncoilerTensionMax,                 //設備預設值
                UncoilerTensionMin = DeviceParaDef.UncoilerTensionMin,                 //設備預設值

                //// From 操作輸入 : 待確認
                //HeadLeaderStripLength = pdi.Head_Paper_Length,
                //HeadLeaderStripThickness = pdi.Head_Strip_Thickness,
                //HeadLeaderStripWidth = pdi.Head_Strip_Width,
                //HeadLeaderStripSteelGrade = pdi.Head_Strip_St_No.ToNullable<int>() ?? 0,
                //TailLeaderStripLength = pdi.Tail_Strip_Length,
                //TailLeaderStripThickness = pdi.Tail_Strip_Thickness,
                //TailLeaderStripWidth = pdi.Tail_Strip_Width,
                //TailLeaderStripSteelGrade = pdi.Tail_Strip_St_No.ToNullable<int>() ?? 0,

                //// From L2 Lookup Table
                SideTrimmerGap = lkTableData.SideTrimmerGap,
                SideTrimmerLap = lkTableData.SideTrimmerLap,
                SideTrimmerWidth = lkTableData.SideTrimmerWidth,
                TensionUnitDepth = lkTableData.Tensionunitdepth,

                //KN 總張力=单位张力*入料鋼捲厚度*入料鋼捲寬度
                RecoilerTension = (lkTableData.RecoilerTension * pdi.Entry_Coil_Thick * pdi.Entry_Coil_Width) /1000,                         
                RecoilerTensionMax = DeviceParaDef.RecoilerTensionMax,                 //設備預設值
                RecoilerTensionMin = DeviceParaDef.RecoilerTensionMin,                 //設備預設值

                //From PDI 
                PaperUnwinderFlag = pdi.Out_Paper_Req_Code.Equals("0") ? 0 : 1,
                //CoilSplit = pdi.Dividing_Num,
                //Orderwt_1 = pdi.Orderwt_1,
                //Orderwt_2 = pdi.Orderwt_2,
                //Orderwt_3 = pdi.Orderwt_3,
                //Orderwt_4 = pdi.Orderwt_4,
                //Orderwt_5 = pdi.Orderwt_5,
                //Orderwt_6 = pdi.Orderwt_6,

                // L2依需求判斷
                PrrPosId = prrPosId,

                Defect1Code = defect.D01_Code.ToCByteArray(10).ToL1EndSignByteArray(2),
                Defect1StartPosition = defect.D01_Pos_L_Start.ToNullable<int>() ?? 0,
                Defect1EndPosition = defect.D01_Pos_L_End.ToNullable<int>() ?? 0,
                Defect2Code = defect.D02_Code.ToCByteArray(10).ToL1EndSignByteArray(2),
                Defect2StartPosition = defect.D02_Pos_L_Start.ToNullable<int>() ?? 0,
                Defect2EndPosition = defect.D02_Pos_L_End.ToNullable<int>() ?? 0,
                Defect3Code = defect.D03_Code.ToCByteArray(10).ToL1EndSignByteArray(2),
                Defect3StartPosition = defect.D03_Pos_L_Start.ToNullable<int>() ?? 0,
                Defect3EndPosition = defect.D03_Pos_L_End.ToNullable<int>() ?? 0,
                Defect4Code = defect.D04_Code.ToCByteArray(10).ToL1EndSignByteArray(2),
                Defect4StartPosition = defect.D04_Pos_L_Start.ToNullable<int>() ?? 0,
                Defect4EndPosition = defect.D04_Pos_L_End.ToNullable<int>() ?? 0,
                Defect5Code = defect.D05_Code.ToCByteArray(10).ToL1EndSignByteArray(2),
                Defect5StartPosition = defect.D05_Pos_L_Start.ToNullable<int>() ?? 0,
                Defect5EndPosition = defect.D05_Pos_L_End.ToNullable<int>() ?? 0,
                Defect6Code = defect.D06_Code.ToCByteArray(10).ToL1EndSignByteArray(2),
                Defect6StartPosition = defect.D06_Pos_L_Start.ToNullable<int>() ?? 0,
                Defect6EndPosition = defect.D06_Pos_L_End.ToNullable<int>() ?? 0,
                Defect7Code = defect.D07_Code.ToCByteArray(10).ToL1EndSignByteArray(2),
                Defect7StartPosition = defect.D07_Pos_L_Start.ToNullable<int>() ?? 0,
                Defect7EndPosition = defect.D07_Pos_L_End.ToNullable<int>() ?? 0,
                Defect8Code = defect.D08_Code.ToCByteArray(10).ToL1EndSignByteArray(2),
                Defect8StartPosition = defect.D08_Pos_L_Start.ToNullable<int>() ?? 0,
                Defect8EndPosition = defect.D08_Pos_L_End.ToNullable<int>() ?? 0,
                Defect9Code = defect.D09_Code.ToCByteArray(10).ToL1EndSignByteArray(2),
                Defect9StartPosition = defect.D09_Pos_L_Start.ToNullable<int>() ?? 0,
                Defect9EndPosition = defect.D09_Pos_L_End.ToNullable<int>() ?? 0,
                Defect10Code = defect.D10_Code.ToCByteArray(10).ToL1EndSignByteArray(2),
                Defect10StartPosition = defect.D10_Pos_L_Start.ToNullable<int>() ?? 0,
                Defect10EndPosition = defect.D10_Pos_L_End.ToNullable<int>() ?? 0,
                Spare1 = 3,
                Spare2 = 0,
                Spare3 = 0,
                Spare4 = 0,
                Spare5 = 0,
            };


            // 根據工序代碼給值
            preset201 = ProcessCodePro(pdi, lkTableData, preset201);


            return preset201;
        }

        private static L2L1Snd.Msg_201_Preset ProcessCodePro(PDI pdi, GenCoilInfoModel.GenPreset201LkTableInfo lkTableData, L2L1Snd.Msg_201_Preset preset)
        {
            // Leader
            if (pdi.Process_Code.Equals(CoilDef.ProcessCode_Leader) 
                || pdi.Process_Code.Equals(CoilDef.ProcessCode_LeaderTrimming))
            {
                preset.HeadLeaderStripLength = pdi.Head_Paper_Length;
                preset.HeadLeaderStripThickness = pdi.Head_Strip_Thickness;
                preset.HeadLeaderStripWidth = pdi.Head_Strip_Width;
                preset.HeadLeaderStripSteelGrade = pdi.Head_Strip_St_No.ToNullable<int>() ?? 0;
                preset.TailLeaderStripLength = pdi.Tail_Strip_Length;
                preset.TailLeaderStripThickness = pdi.Tail_Strip_Thickness;
                preset.TailLeaderStripWidth = pdi.Tail_Strip_Width;
                preset.TailLeaderStripSteelGrade = pdi.Tail_Strip_St_No.ToNullable<int>() ?? 0;
            }

            // Trimming
            if (pdi.Process_Code.Equals(CoilDef.ProcessCode_Trimming) 
                || pdi.Process_Code.Equals(CoilDef.ProcessCode_LeaderTrimming)
                || pdi.Process_Code.Equals(CoilDef.ProcessCode_CutTrimming))
            {
                preset.SideTrimmerGap = lkTableData.SideTrimmerGap;
                preset.SideTrimmerLap = lkTableData.SideTrimmerLap;
                preset.SideTrimmerWidth = lkTableData.SideTrimmerWidth;
            }


            // Cut
            if (pdi.Process_Code.Equals(CoilDef.ProcessCode_Cut) 
                || pdi.Process_Code.Equals(CoilDef.ProcessCode_CutTrimming)
                && pdi.Dividing_Flag.Equals(DBParaDef.TRUE))
            {
                preset.CoilSplit = pdi.Dividing_Num;
                preset.Orderwt_1 = pdi.Orderwt_1;
                preset.Orderwt_2 = pdi.Orderwt_2;
                preset.Orderwt_3 = pdi.Orderwt_3;
                preset.Orderwt_4 = pdi.Orderwt_4;
                preset.Orderwt_5 = pdi.Orderwt_5;
                preset.Orderwt_6 = pdi.Orderwt_6;
            }

            return preset;
        } 



        public static L2L1Snd.Msg_201_Preset PresetEmpty201Msg(string coilID)
        {
            
            var empty201 = new L2L1Snd.Msg_201_Preset()
            {
                #region Header
                MessageId = short.Parse(PlcSysDef.SndMsgCode.L1201Preset),
                MessageLength = (short)Marshal.SizeOf<L2L1Snd.Msg_201_Preset>(),
                Date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd")),
                Time = Int32.Parse(DateTime.Now.ToString("HHmmsshh")),
                #endregion
            };

          
            foreach(FieldInfo fi in empty201.GetType().GetFields())
            {
                if (fi.FieldType == typeof(char[]))
                {
                    var ma = fi.GetCustomAttribute<MarshalAsAttribute>();
                    fi.SetValue(empty201, "".PadRight(ma.SizeConst).ToCharArray());
                }

                if (fi.FieldType == typeof(byte[]))
                {
                    var ma = fi.GetCustomAttribute<MarshalAsAttribute>();
                    fi.SetValue(empty201, "".ToCByteArray(ma.SizeConst));
                }


            }

            empty201.Coil_ID = coilID.ToCByteArray(24);

            return empty201;
        }
        public static L2L1Snd.Msg_202_TrackMapL2 L2TrackMap202Msg(CoilMapEntity.TBL_CoilMap currMap){
        
            var L2TrackMap = new L2L1Snd.Msg_202_TrackMapL2
            {

                #region Header
                MessageId = short.Parse(PlcSysDef.SndMsgCode.L1202TrackMapL2),
                MessageLength = (short)Marshal.SizeOf<L2L1Snd.Msg_202_TrackMapL2>(),
                Date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd")),
                Time = Int32.Parse(DateTime.Now.ToString("HHmmsshh")),
                #endregion

                CoilIDUnc = currMap.POR.ToCByteArray(20).ToL1EndSignByteArray(4),
                CoilIDUncSk1 = currMap.Entry_SK01.ToCByteArray(20).ToL1EndSignByteArray(4),
                CoilIDUncSk2 = currMap.Entry_SK02.ToCByteArray(20).ToL1EndSignByteArray(4),
                CoilIDUncTop = currMap.Entry_TOP.ToCByteArray(20).ToL1EndSignByteArray(4),
                CoilIDRec = currMap.TR.ToCByteArray(20).ToL1EndSignByteArray(4),
                CoilIDRecSk1 = currMap.Delivery_SK01.ToCByteArray(20).ToL1EndSignByteArray(4),
                CoilIDRecSk2 = currMap.Delivery_SK02.ToCByteArray(20).ToL1EndSignByteArray(4),
                CoilIDRecTop = currMap.Delivery_TOP.ToCByteArray(20).ToL1EndSignByteArray(4),
                PrrPosId = 0,   //0: All
            };
            return L2TrackMap;
        }
        public static L2L1Snd.Msg_203_SplitId SplitID203Msg(string coilID)
        {
            var msg = new L2L1Snd.Msg_203_SplitId()
            {
                #region Header
                MessageId = short.Parse(PlcSysDef.SndMsgCode.L1203SplitID),
                MessageLength = (short)Marshal.SizeOf<L2L1Snd.Msg_203_SplitId>(),
                Date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd")),
                Time = Int32.Parse(DateTime.Now.ToString("HHmmsshh")),
                #endregion
                CoilID = coilID.ToCByteArray(20).ToL1EndSignByteArray(4)
            };

            return msg;
        }
        public static L2L1Snd.Msg_204_DelSkid DelSkid204Msg(string delPosiD){

            var msg = new L2L1Snd.Msg_204_DelSkid()
            {
                #region Header
                MessageId = short.Parse(PlcSysDef.SndMsgCode.L1204DelSkID),
                MessageLength = (short)Marshal.SizeOf<L2L1Snd.Msg_204_DelSkid>(),
                Date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd")),
                Time = Int32.Parse(DateTime.Now.ToString("HHmmsshh")),
                #endregion
                DelPosId = short.Parse(delPosiD),
            };

            return msg;
        }

        public static L2L1Snd.Msg_205_NewPORId NewPORMsg(string coilID)
        {
            var msg = new L2L1Snd.Msg_205_NewPORId()
            {
                #region Header
                MessageId = short.Parse(PlcSysDef.SndMsgCode.L1205NewPOR),
                MessageLength = (short)Marshal.SizeOf<L2L1Snd.Msg_205_NewPORId>(),
                Date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd")),
                Time = Int32.Parse(DateTime.Now.ToString("HHmmsshh")),
                #endregion
                CoilID = coilID.ToCByteArray(20).ToL1EndSignByteArray(4)
            };

            return msg;
        }

        public static L2L1Snd.Msg_299_L2ALIVE L2Alive()
        {
            var msg = new L2L1Snd.Msg_299_L2ALIVE()
            {
                MessageId = short.Parse(PlcSysDef.SndMsgCode.L2299Alive),
                MessageLength = (short)Marshal.SizeOf<L2L1Snd.Msg_299_L2ALIVE>(),
                Date = Int32.Parse(DateTime.Now.ToString("yyyyMMdd")),
                Time = Int32.Parse(DateTime.Now.ToString("HHmmsshh")),
            };
            return msg;
        }  
       
    }
}
