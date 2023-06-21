using Core.Define;
using Core.Util;
using System;
using System.Runtime.InteropServices;



/**
* Author: ICSC余士鵬
* Date: 2019/9/19
* Description: L2µo°e³ø¤åStruct
* Reference: 
* Modified: 
*/
namespace MsgStruct
{
    public class L2L1Snd
    {

        private static DateTime IntConvertDateTime(int Date, int Time)
        {

            try
            {
                //var date = CalanderUtil.ConvertDateFormatStr(Date.ToString(), "yyyyMMdd", "yyyy-MM-dd");
                //var time = CalanderUtil.ConvertTimeFormatStr(Time.ToString().PadLeft(8, '0'), "HHmmssff", "HH:mm:ss.ff");
                //var datetime = string.Format("{0} {1}", date, time);
                ////return DateTime.Parse(datetime) != null ? DateTime.Parse(datetime) : DBParaDef.DefaultTime;
                //return DateTime.Parse(datetime);

                var date = CalanderUtil.ConvertDateFormatStr(Date.ToString(), "yyyyMMdd", "yyyy-MM-dd");
                //var time = CalanderUtil.ConvertTimeFormatStr(Time.ToString().PadLeft(6, '0'), "HHmmss", "HH:mm:ss");
                var time = CalanderUtil.ConvertTimeFormatStr(Time.ToString().PadLeft(8, '0'), "HHmmssff", "HH:mm:ss.ff");   //  20210521»¨¾^­×§ï, L1°eªºtime®æ¦¡¬OHHmmssff
                var datetime = string.Format("{0} {1}", date, time);
                //return DateTime.Parse(datetime) != null ? DateTime.Parse(datetime) : DBDefine.DefaultTime;
                return DateTime.Parse(datetime);
            }
            catch
            {
                throw;
            }

        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_201_Preset
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] Coil_ID = new byte[24];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] SteelGrade = new byte[24];
            [MarshalAs(UnmanagedType.R4)]
            public float Thickness;
            [MarshalAs(UnmanagedType.R4)]
            public float Width;
            [MarshalAs(UnmanagedType.R4)]
            public float EntryYieldStress;
            [MarshalAs(UnmanagedType.R4)]
            public float Density;
            [MarshalAs(UnmanagedType.R4)]
            public float CoilLength;
            [MarshalAs(UnmanagedType.R4)]
            public float CoilWeight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] ProcessCode = new byte[8];
            [MarshalAs(UnmanagedType.R4)]
            public float InnerDiam;
            [MarshalAs(UnmanagedType.R4)]
            public float Diameter;
            [MarshalAs(UnmanagedType.I4)]
            public int SleeveCodeEntry;
            [MarshalAs(UnmanagedType.R4)]
            public float SleeveDmEntry;
            [MarshalAs(UnmanagedType.I4)]
            public int PaperWinderFlag;
            [MarshalAs(UnmanagedType.I4)]
            public int SleeveCodeExit;
            [MarshalAs(UnmanagedType.R4)]
            public float SleeveDmExit;
            [MarshalAs(UnmanagedType.I4)]
            public int PaperTypeExit;
            [MarshalAs(UnmanagedType.I4)]
            public int PaperCodeExit;
            [MarshalAs(UnmanagedType.R4)]
            public float FlatenerDepth1;
            [MarshalAs(UnmanagedType.R4)]
            public float FlatenerDepth2;
            [MarshalAs(UnmanagedType.R4)]
            public float UncoilerTension;
            [MarshalAs(UnmanagedType.R4)]
            public float UncoilerTensionMax;
            [MarshalAs(UnmanagedType.R4)]
            public float UncoilerTensionMin;
            [MarshalAs(UnmanagedType.R4)]
            public float HeadLeaderStripLength;
            [MarshalAs(UnmanagedType.R4)]
            public float HeadLeaderStripThickness;
            [MarshalAs(UnmanagedType.R4)]
            public float HeadLeaderStripWidth;
            [MarshalAs(UnmanagedType.I4)]
            public int HeadLeaderStripSteelGrade;
            [MarshalAs(UnmanagedType.R4)]
            public float TailLeaderStripLength;
            [MarshalAs(UnmanagedType.R4)]
            public float TailLeaderStripThickness;
            [MarshalAs(UnmanagedType.R4)]
            public float TailLeaderStripWidth;
            [MarshalAs(UnmanagedType.I4)]
            public int TailLeaderStripSteelGrade;
            [MarshalAs(UnmanagedType.R4)]
            public float SideTrimmerGap;
            [MarshalAs(UnmanagedType.R4)]
            public float SideTrimmerLap;
            [MarshalAs(UnmanagedType.R4)]
            public float SideTrimmerWidth;
            [MarshalAs(UnmanagedType.R4)]
            public float TensionUnitDepth;
            [MarshalAs(UnmanagedType.R4)]
            public float RecoilerTension;
            [MarshalAs(UnmanagedType.R4)]
            public float RecoilerTensionMax;
            [MarshalAs(UnmanagedType.R4)]
            public float RecoilerTensionMin;
            [MarshalAs(UnmanagedType.I4)]
            public int PaperUnwinderFlag;
            [MarshalAs(UnmanagedType.I4)]
            public int CoilSplit;
            [MarshalAs(UnmanagedType.R4)]
            public float Orderwt_1;
            [MarshalAs(UnmanagedType.R4)]
            public float Orderwt_2;
            [MarshalAs(UnmanagedType.R4)]
            public float Orderwt_3;
            [MarshalAs(UnmanagedType.R4)]
            public float Orderwt_4;
            [MarshalAs(UnmanagedType.R4)]
            public float Orderwt_5;
            [MarshalAs(UnmanagedType.R4)]
            public float Orderwt_6;
            [MarshalAs(UnmanagedType.I4)]
            public int PrrPosId;


            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] Defect1Code;
            [MarshalAs(UnmanagedType.R4)]
            public float Defect1StartPosition;
            [MarshalAs(UnmanagedType.R4)]
            public float Defect1EndPosition;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] Defect2Code;
            [MarshalAs(UnmanagedType.R4)]
            public float Defect2StartPosition;
            [MarshalAs(UnmanagedType.R4)]
            public float Defect2EndPosition;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] Defect3Code;
            [MarshalAs(UnmanagedType.R4)]
            public float Defect3StartPosition;
            [MarshalAs(UnmanagedType.R4)]
            public float Defect3EndPosition;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] Defect4Code;
            [MarshalAs(UnmanagedType.R4)]
            public float Defect4StartPosition;
            [MarshalAs(UnmanagedType.R4)]
            public float Defect4EndPosition;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] Defect5Code;
            [MarshalAs(UnmanagedType.R4)]
            public float Defect5StartPosition;
            [MarshalAs(UnmanagedType.R4)]
            public float Defect5EndPosition;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] Defect6Code;
            [MarshalAs(UnmanagedType.R4)]
            public float Defect6StartPosition;
            [MarshalAs(UnmanagedType.R4)]
            public float Defect6EndPosition;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] Defect7Code;
            [MarshalAs(UnmanagedType.R4)]
            public float Defect7StartPosition;
            [MarshalAs(UnmanagedType.R4)]
            public float Defect7EndPosition;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] Defect8Code;
            [MarshalAs(UnmanagedType.R4)]
            public float Defect8StartPosition;
            [MarshalAs(UnmanagedType.R4)]
            public float Defect8EndPosition;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] Defect9Code;
            [MarshalAs(UnmanagedType.R4)]
            public float Defect9StartPosition;
            [MarshalAs(UnmanagedType.R4)]
            public float Defect9EndPosition;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] Defect10Code;
            [MarshalAs(UnmanagedType.R4)]
            public float Defect10StartPosition;
            [MarshalAs(UnmanagedType.R4)]
            public float Defect10EndPosition;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare1;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare2;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare3;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare4;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare5;

            public string CoilIDNo { get => Coil_ID.ToStr(); }
            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_202_TrackMapL2
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilIDUnc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilIDUncSk1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilIDUncSk2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilIDUncTop;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilIDRec;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilIDRecSk1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilIDRecSk2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilIDRecTop;
            [MarshalAs(UnmanagedType.I4)]
            public int PrrPosId;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare1;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare2;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare3;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare4;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare5;
            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_203_SplitId
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilID;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare1;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare2;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare3;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare4;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare5;
            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_204_DelSkid
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.I4)]
            public int DelPosId;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare1;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare2;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare3;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare4;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare5;
            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }


        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_205_NewPORId
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilID;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare1;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare2;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare3;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare4;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare5;

            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_299_L2ALIVE
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare1;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare2;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare3;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare4;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare5;

        }


       

        [Serializable]
        public class SpecificPreset
        {
            public string SpecificCoilID = "";         
            // 1: Uncoiler 2: UncSK1 3: UncSK2 4: UncTOP 11~50: reserved coil 1~40
            public int SpecificPos = -1;

            public SpecificPreset(string coilID, int pos)
            {
                SpecificCoilID = coilID;
                SpecificPos = pos;
            }
        }
    }
}