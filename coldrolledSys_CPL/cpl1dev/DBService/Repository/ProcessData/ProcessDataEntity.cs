using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.LineStatus
{
    public class ProcessDataEntity 
    {
        public class TBL_ProcessData : BaseRepositoryModel
        {
            [PrimaryKey]
            public DateTime ReceiveTime { get; set; }
            public float LINE_Speed_Actual { get; set; }
            public float POR_Tension_Set { get; set; }
            public float POR_Tension_Actual { get; set; }
            public float POR_Current_Actual { get; set; }
            public float TR_Tension_Set { get; set; }
            public float TR_Tension_Actual { get; set; }
            public float TR_Current_Actual { get; set; }
            public float WELD_Current_Actual_Front { get; set; }
            public float WELD_Speed_Actual { get; set; }
            public float WELD_Current_Actual_Rear { get; set; }
            public float WELD_PlanishWheelForce_Actual { get; set; }
            public float WELD_Temperture { get; set; }
            public int BuildTension { get; set; }
        }
    }
}
