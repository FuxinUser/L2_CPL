using DBService.Base;
using System;

namespace DBService.Repository.LookupTblSideTrimmer
{
    public class LkUpTableSideTrimmerEntity
    {
        public class TBL_LookupTable_SideTrimmer : BaseRepositoryModel
        {
            public string Material_Grade { get; set; } = "";
            public float Coil_Thickness_Min { get; set; } = 0.0f;
            public float Coil_Thickness_Max { get; set; } = 0.0f;
            //public float TS_STAND_MIN { get; set; } = 0.0f;
            //public float TS_STAND_MAX { get; set; } = 0.0f;
            public float KnifeGap { get; set; } = 0.0f;
            public float KnifeLap { get; set; } = 0.0f;
            public override DateTime UpdateTime { get; set; }
        }

    }
}
