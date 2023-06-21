using System;
using System.Runtime.InteropServices;
using Core.Util;


/**
* Author: ICSC余士鵬
* Date: 2019/9/19
* Description: L1發送報文接收Struct
* Reference: 
* Modified: 
*/
namespace MsgStruct
{
    public class L1L2Rcv
    {

        private static DateTime IntConvertDateTime(int Date, int Time)
        {

            try
            {
                var date = CalanderUtil.ConvertDateFormatStr(Date.ToString(), "yyyyMMdd", "yyyy-MM-dd");
                //var time = CalanderUtil.ConvertTimeFormatStr(Time.ToString().PadLeft(6, '0'), "HHmmss", "HH:mm:ss");
                var time = CalanderUtil.ConvertTimeFormatStr(Time.ToString().PadLeft(8, '0'), "HHmmssff", "HH:mm:ss.ff");  
                var datetime = string.Format("{0} {1}", date, time);
                //return DateTime.Parse(datetime) != null ? DateTime.Parse(datetime) : DBDefine.DefaultTime;
                return DateTime.Parse(datetime);
            }
            catch
            {
                //return DBDefine.DefaultTime;
                throw;
            }
           
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_301_EnCoilCut
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
            public byte[] CoilID = new byte[24];
            [MarshalAs(UnmanagedType.I4)]
            public int CutMode;
            [MarshalAs(UnmanagedType.R4)]
            public float CutLength;
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

            public string CoilNo { get => CoilID.ToStr(); }
            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }
            
        }



        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_302_CoilWeld
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
            public byte[] CoilID = new byte[24];
            [MarshalAs(UnmanagedType.R4)]
            public float WeldVoltageSettingFrontTorch;
            [MarshalAs(UnmanagedType.R4)]
            public float WeldVoltageSettingRearTorch;
            [MarshalAs(UnmanagedType.R4)]
            public float WeldWireSpeedFrontTorch;
            [MarshalAs(UnmanagedType.R4)]
            public float WeldWireSpeedRearTorch;
            [MarshalAs(UnmanagedType.R4)]
            public float WeldCurrent;
            [MarshalAs(UnmanagedType.R4)]
            public float TorchCarriageWeldSpeed;
            [MarshalAs(UnmanagedType.R4)]
            public float OperatorSideFrontWeldGap;
            [MarshalAs(UnmanagedType.R4)]
            public float DriveSideRearWeldGap;
            [MarshalAs(UnmanagedType.R4)]
            public float StartPuddleTime;
            [MarshalAs(UnmanagedType.R4)]
            public float StopPuddleTime;
            [MarshalAs(UnmanagedType.I4)]
            public int WeldScheduleNumber;
            [MarshalAs(UnmanagedType.R4)]
            public float AnnealerPowerPercentage;
            [MarshalAs(UnmanagedType.R4)]
            public float WeldTempActPoint;
            [MarshalAs(UnmanagedType.I4)]
            public int HeadEndLeaderWeldPointPosition;
            [MarshalAs(UnmanagedType.I4)]
            public int HeadEndLeaderWeldPointDistanceFromPunchHole;
            [MarshalAs(UnmanagedType.I4)]
            public int TailEndLeaderWeldPointPosition;
            [MarshalAs(UnmanagedType.I4)]
            public int TailEndLeaderWeldPointDistanceFromPunchHole;
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

            public string CoilNoID { get => CoilID.ToStr(); }
            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_303_ReqTrackMap
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
            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_305_TrackMapEn
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
            public byte[] CoilIDUnCoiler = new byte[24];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilIDUnSkid1 = new byte[24];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilIDUnSkid2 = new byte[24];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilIDUnTop = new byte[24];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilIDCar = new byte[24];
            [MarshalAs(UnmanagedType.I4)]
            public int EntryTOPPhotoSensor;
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

            public string EntryCar { get => CoilIDCar.ToStr(); }
            public string EntryTOP { get => CoilIDUnTop.ToStr(); }
            public string POR { get => CoilIDUnCoiler.ToStr(); }
            public string EntrySK01 { get => CoilIDUnSkid1.ToStr(); }
            public string EntrySK02 { get => CoilIDUnSkid2.ToStr(); }

            public bool isEntryTopEmpty { get => EntryTOP.Equals(string.Empty); }
            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }
        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_306_TrackMapEx
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
            public byte[] CoilIDReCoiler = new byte[24];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilIDRecSkid1 = new byte[24];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilIDRecSkid2 = new byte[24];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilIDRecTop = new byte[24];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilIDCar = new byte[24];
            [MarshalAs(UnmanagedType.I4)]
            public int ExitTOPPhotoSensor;
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

            public string DeliveryCar { get => CoilIDCar.ToStr(); }
            public string TR { get => CoilIDReCoiler.ToStr(); }
            public string DeliverySK01 { get => CoilIDRecSkid1.ToStr(); }
            public string DeliverySK02 { get => CoilIDRecSkid2.ToStr(); }
            public string DeliveryTOP { get => CoilIDRecTop.ToStr(); }
            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_307_CoilDismount
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.R4)]
            public float CoilWeight;
            [MarshalAs(UnmanagedType.R4)]
            public float CoilLength;
            [MarshalAs(UnmanagedType.R4)]
            public float Diameter;
            [MarshalAs(UnmanagedType.R4)]
            public float CoiInsideDiam;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] CoilID = new byte[24];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] PaperCode = new byte[4];
            [MarshalAs(UnmanagedType.R4)]
            public float PaperWidth;
            [MarshalAs(UnmanagedType.I4)]
            public int SleeveCode;
            [MarshalAs(UnmanagedType.R4)]
            public float SleeveDm;
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

            public string CoilIDNo { get => CoilID.ToStr(); }
            public int DividFlag
            {
                get
                {
                    return CoilIDNo.EndsWith("0000") ? 0 : 1;
                }
            }
            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_308_CoilWeightScale
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.R4)]
            public float CoilWeight;
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

            public string CoilIDNo { get => CoilID.ToStr(); }
            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_309_EquipMaint
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
            public int StatusEn;
            [MarshalAs(UnmanagedType.I4)]
            public int StatusCPL;
            [MarshalAs(UnmanagedType.I4)]
            public int StatusEx;
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
        public class Msg_310_LineFault
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
            public int FaultCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] StartTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] EndTime;
            [MarshalAs(UnmanagedType.I4)]
            public int StopCategory;
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
        public class Msg_311_ExCoilCut
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
            [MarshalAs(UnmanagedType.R4)]
            public float CutLength;
            [MarshalAs(UnmanagedType.I4)]
            public int CutMode;
            [MarshalAs(UnmanagedType.R4)]
            public float DiamRec;
            [MarshalAs(UnmanagedType.R4)]
            public float LengthRec;
            [MarshalAs(UnmanagedType.R4)]
            public float CalculateWeightRec;
            [MarshalAs(UnmanagedType.I4)]
            public int PUWFlag;
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

            public string ParentCoilID { get => CoilID.ToStr(); }
            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

       

            /* L1 : Cut Mode 
                Bit 0 Strip break
                Bit 1 Split cut 
                Bit 2 Sample cut 
                Bit 3 Head cut 
                Bit 4 Tail cut 
                Bit 5 Weld cut 
                Bit 6 Scrap cut
                Bit 7 虛擬切割                    
                    00000001 : 1    Strip break
                    00000010 : 2    Split Cut
                    00000100 : 4    Sample Cut
                    10000000 : 128  Virtual cut
            */

            public string CutModeStr
            {
                get
                {
                    var str = string.Empty;

                    switch (CutMode)
                    {
                        case 1:
                            str = "Strip break";
                            break;
                        case 2:
                            str = "Split cut";
                            break;
                        case 4:
                            str = "Sample cut";
                            break;
                        case 128:
                            str = "Virtual cut";
                            break;
                    }
                    return str;
                }           
            }
        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_312_NewCoilRec
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
        public class Msg_313_SpdTen
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.R4)]
            public float ActualSpeed;
            [MarshalAs(UnmanagedType.R4)]
            public float TenRefUnc;
            [MarshalAs(UnmanagedType.R4)]
            public float TenActUnc;
            [MarshalAs(UnmanagedType.R4)]
            public float UncMotorActCurrent;
            [MarshalAs(UnmanagedType.R4)]
            public float TenRefRec;
            [MarshalAs(UnmanagedType.R4)]
            public float TenActRec;
            [MarshalAs(UnmanagedType.R4)]
            public float RecMotorActCurrent;
            [MarshalAs(UnmanagedType.R4)]
            public float WeldActCurrentFront;
            [MarshalAs(UnmanagedType.R4)]
            public float WeldActSpd;
            [MarshalAs(UnmanagedType.R4)]
            public float WeldActCurrentRear;
            [MarshalAs(UnmanagedType.R4)]
            public float WeldActPlanishRollForce;
            [MarshalAs(UnmanagedType.R4)]
            public float WeldTemperature;
            [MarshalAs(UnmanagedType.I4)]
            public int BuildTension;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare1;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare2;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare3;
            [MarshalAs(UnmanagedType.I4)]
            public int Spare4;
            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_315_Cdc
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
            public int Cdc1;
            [MarshalAs(UnmanagedType.I4)]
            public int Cdc2;
            [MarshalAs(UnmanagedType.I4)]
            public int Cdc3;
            [MarshalAs(UnmanagedType.I4)]
            public int Cdc4;
            [MarshalAs(UnmanagedType.I4)]
            public int Cdc5;
            [MarshalAs(UnmanagedType.I4)]
            public int Cdc6;
            [MarshalAs(UnmanagedType.I4)]
            public int Cdc7;
            [MarshalAs(UnmanagedType.I4)]
            public int Cdc8;
            [MarshalAs(UnmanagedType.I4)]
            public int Cdc9;
            [MarshalAs(UnmanagedType.I4)]
            public int Cdc10;
            [MarshalAs(UnmanagedType.I4)]
            public int Cdc11;
            [MarshalAs(UnmanagedType.I4)]
            public int Cdc12;
            [MarshalAs(UnmanagedType.I4)]
            public int Cdc13;
            [MarshalAs(UnmanagedType.I4)]
            public int Cdc14;
            [MarshalAs(UnmanagedType.I4)]
            public int Cdc15;
            [MarshalAs(UnmanagedType.I4)]
            public int Cdc16;
            [MarshalAs(UnmanagedType.I4)]
            public int Cdc17;
            [MarshalAs(UnmanagedType.I4)]
            public int Cdc18;
            [MarshalAs(UnmanagedType.I4)]
            public int Cdc19;
            [MarshalAs(UnmanagedType.I4)]
            public int Cdc20;
            [MarshalAs(UnmanagedType.I4)]
            public int Cdc21;
            [MarshalAs(UnmanagedType.I4)]
            public int Cdc22;
            [MarshalAs(UnmanagedType.I4)]
            public int Cdc23;
            [MarshalAs(UnmanagedType.I4)]
            public int Cdc24;
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
        public class Msg_316_Utility_Data
        {
            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;
            [MarshalAs(UnmanagedType.R4)]
            public float CompressedAir;
            [MarshalAs(UnmanagedType.R4)]
            public float IndirectCollingWater;
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
        public class Msg_317_ReturnCoilInfo
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
            public byte[] CoilID = new byte[24];
            [MarshalAs(UnmanagedType.R4)]
            public float CoilWeight;
            //[MarshalAs(UnmanagedType.I4)]
            //public int CoilLength;
            [MarshalAs(UnmanagedType.R4)]
            public float CoilLength;
            [MarshalAs(UnmanagedType.R4)]
            public float Diameter;
            [MarshalAs(UnmanagedType.R4)]
            public float CoiInsideDiam;
            [MarshalAs(UnmanagedType.R4)]
            public float Width;
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
        public class Msg_318_SideTrimmerInfo
        {

            [MarshalAs(UnmanagedType.I2)]
            public short MessageLength;
            [MarshalAs(UnmanagedType.I2)]
            public short MessageId;
            [MarshalAs(UnmanagedType.I4)]
            public int Date;
            [MarshalAs(UnmanagedType.I4)]
            public int Time;

            [MarshalAs(UnmanagedType.R4)]
            public float SideTrimmerGap;
            [MarshalAs(UnmanagedType.R4)]
            public float SideTrimmerLap;
            [MarshalAs(UnmanagedType.R4)]
            public float SideTrimmerWidth;
            [MarshalAs(UnmanagedType.R4)]
            public float Trimming_OperateSide;
            [MarshalAs(UnmanagedType.R4)]
            public float Trimming_DriveSide;       

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
        public class Msg_399_L1ALIVE
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
            public DateTime DateTime { get => IntConvertDateTime(Date, Time); }

        }

    }
}