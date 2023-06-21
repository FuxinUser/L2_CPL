using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.WieldRecord
{
    public class WeldRecordEntity
    {
        public class TBL_WeldRecords : BaseRepositoryModel
        {
            [PrimaryKey]
            public string Coil_ID { get; set; }
            [PrimaryKey]
            public DateTime ReceiveTime { get; set; }
            public string OriPDI_Out_Coil_ID { get; set; }
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
        }


    }
}
