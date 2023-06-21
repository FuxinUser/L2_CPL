using DBService.Base;
using System;
using static DBService.Base.DBAttributes;


namespace DBService.L1Repository
{
    public class L1L2MsgDBModel
    {
        public class L1L2_301
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public int Date { get; set; }
            public int Time { get; set; }
            public string CoilID { get; set; }
            public int CutMode { get; set; }
            public float CutLength { get; set; }
            public System.DateTime DateTime { get; set; }
            [PrimaryKey]
            public System.DateTime CreateTime { get; set; }
        }

        public class L1L2_302
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public string CoilID { get; set; }
            public int Date { get; set; }
            public int Time { get; set; }
            public float WeldVoltageSettingFrontTorch { get; set; }
            public float WeldVoltageSettingRearTorch { get; set; }
            public float WeldWireSpeedFrontTorch { get; set; }
            public float WeldWireSpeedRearTorch { get; set; }
            public float WeldCurrent { get; set; }
            public float TorchCarriageWeldSpeed { get; set; }
            public float OperatorSideFrontWeldGap { get; set; }
            public float DriveSideRearWeldGap { get; set; }
            public float StartPuddleTime { get; set; }
            public float StopPuddleTime { get; set; }
            public int WeldScheduleNumber { get; set; }
            public float AnnealerPowerPercentage { get; set; }
            public float WeldTempActPoint { get; set; }
            public int HeadEndLeaderWeldPointPosition { get; set; }
            public int HeadEndLeaderWeldPointDistanceFromPunchHole { get; set; }
            public int TailEndLeaderWeldPointPosition { get; set; }
            public int TailEndLeaderWeldPointDistanceFromPunchHole { get; set; }
            public DateTime DateTime { get; set; }
            [PrimaryKey]
            public DateTime CreateTime { get; set; }
        }

        public class L1L2_303
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public int Date { get; set; }
            public int Time { get; set; }
            public DateTime DateTime { get; set; }
            [PrimaryKey]
            public DateTime CreateTime { get; set; }
        }

        public class L1L2_305
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public int Date { get; set; }
            public int Time { get; set; }
            public string CoilIDUnCoiler { get; set; }
            public string CoilIDUnSkid1 { get; set; }
            public string CoilIDUnSkid2 { get; set; }
            public string CoilIDUnTop { get; set; }
            public string CoilIDCar { get; set; }
            public int EntryTOPPhotoSensor { get; set; }
            public DateTime DateTime { get; set; }
            [PrimaryKey]
            public DateTime CreateTime { get; set; }
        }

        public class L1L2_306
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public int Date { get; set; }
            public int Time { get; set; }
            public string CoilIDReCoiler { get; set; }
            public string CoilIDRecSkid1 { get; set; }
            public string CoilIDRecSkid2 { get; set; }
            public string CoilIDRecTop { get; set; }
            public string CoilIDCar { get; set; }
            public int ExitTOPPhotoSensor { get; set; }
            public DateTime DateTime { get; set; }
            [PrimaryKey]
            public DateTime CreateTime { get; set; }
        }

        public class L1L2_307
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public int Date { get; set; }
            public int Time { get; set; }
            public double CoilWeight { get; set; }
            public double CoilLength { get; set; }
            public double Diameter { get; set; }
            public double CoiInsideDiam { get; set; }
            public string CoilID { get; set; }
            public string PaperCode { get; set; }
            public double PaperWidth { get; set; }
            public int SleeveCode { get; set; }
            public double SleeveDm { get; set; }
            public DateTime DateTime { get; set; }
            [PrimaryKey]
            public DateTime CreateTime { get; set; }
        }

        public class L1L2_308
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public int Date { get; set; }
            public int Time { get; set; }
            public double CoilWeight { get; set; }
            public string CoilID { get; set; }
            public DateTime DateTime { get; set; }
            [PrimaryKey]
            public DateTime CreateTime { get; set; }
        }

        public class L1L2_309
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public int Date { get; set; }
            public int Time { get; set; }
            public int StatusEn { get; set; }
            public int StatusCPL { get; set; }
            public int StatusEx { get; set; }
            public DateTime DateTime { get; set; }
            [PrimaryKey]
            public DateTime CreateTime { get; set; }
        }

        public class L1L2_310
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public int Date { get; set; }
            public int Time { get; set; }
            public int FaultCode { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public int StopCategory { get; set; }
            public DateTime DateTime { get; set; }
            [PrimaryKey]
            public DateTime CreateTime { get; set; }
        }

        public class L1L2_311
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public int Date { get; set; }
            public int Time { get; set; }
            public string CoilID { get; set; }
            public double CutLength { get; set; }
            public int CutMode { get; set; }
            public double DiamRec { get; set; }
            public double LengthRec { get; set; }
            public double CalculateWeightRec { get; set; }
            public int PUWFlag { get; set; }
            public DateTime DateTime { get; set; }
            [PrimaryKey]
            public DateTime CreateTime { get; set; }
        }

        public class L1L2_312
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public int Date { get; set; }
            public int Time { get; set; }
            public string CoilID { get; set; }
            public DateTime DateTime { get; set; }
            [PrimaryKey]
            public DateTime CreateTime { get; set; }
        }

        public class L1L2_313
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public int Date { get; set; }
            public int Time { get; set; }
            public float ActualSpeed { get; set; }
            public float TenRefUnc { get; set; }
            public float TenActUnc { get; set; }
            public float UncMotorActCurrent { get; set; }
            public float TenRefRec { get; set; }
            public float TenActRec { get; set; }
            public float RecMotorActCurrent { get; set; }
            public float WeldActCurrentFront { get; set; }
            public float WeldActSpd { get; set; }
            public float WeldActCurrentRear { get; set; }
            public float WeldActPlanishRollForce { get; set; }
            public float WeldTemperature { get; set; }
            public int BuildTension { get; set; }
            public DateTime DateTime { get; set; }
            [PrimaryKey]
            public DateTime CreateTime { get; set; }
        }

        public class L1L2_315
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public int Date { get; set; }
            public int Time { get; set; }
            public int Cdc1 { get; set; }
            public int Cdc2 { get; set; }
            public int Cdc3 { get; set; }
            public int Cdc4 { get; set; }
            public int Cdc5 { get; set; }
            public int Cdc6 { get; set; }
            public int Cdc7 { get; set; }
            public int Cdc8 { get; set; }
            public int Cdc9 { get; set; }
            public int Cdc10 { get; set; }
            public int Cdc11 { get; set; }
            public int Cdc12 { get; set; }
            public int Cdc13 { get; set; }
            public int Cdc14 { get; set; }
            public int Cdc15 { get; set; }
            public int Cdc16 { get; set; }
            public int Cdc17 { get; set; }
            public int Cdc18 { get; set; }
            public int Cdc19 { get; set; }
            public int Cdc20 { get; set; }
            public int Cdc21 { get; set; }
            public int Cdc22 { get; set; }
            public int Cdc23 { get; set; }
            public int Cdc24 { get; set; }
            public DateTime DateTime { get; set; }
            [PrimaryKey]
            public DateTime CreateTime { get; set; }
        }

        public class L1L2_316
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public int Date { get; set; }
            public int Time { get; set; }
            public double CompressedAir { get; set; }
            public double IndirectCollingWater { get; set; }
            public DateTime DateTime { get; set; }
            [PrimaryKey]
            public DateTime CreateTime { get; set; }
        }

        public class L1L2_317
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public int Date { get; set; }
            public int Time { get; set; }
            public string CoilID { get; set; }
            public float CoilWeight { get; set; }
            public float CoilLength { get; set; }
            //public int CoilLength { get; set; }
            public float Diameter { get; set; }
            public float CoiInsideDiam { get; set; }
            public float Width { get; set; }
            public DateTime DateTime { get; set; }
            [PrimaryKey]
            public DateTime CreateTime { get; set; }
        }


        public class L1L2_318
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public int Date { get; set; }
            public int Time { get; set; }
      
            public float SideTrimmerGap { get; set; }
            public float SideTrimmerLap { get; set; }
            public float SideTrimmerWidth { get; set; }
            public float Trimming_OperateSide { get; set; }
            public float Trimming_DriveSide { get; set; }

            public DateTime DateTime { get; set; }
            [PrimaryKey]
            public DateTime CreateTime { get; set; }
        }
    }
}
