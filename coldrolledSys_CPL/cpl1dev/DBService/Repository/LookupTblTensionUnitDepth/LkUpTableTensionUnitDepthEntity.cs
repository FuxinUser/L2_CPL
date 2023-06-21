using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.LookupTblTensionUnitDepth
{
    public class LkUpTableTensionUnitDepthEntity
    {
        public class TBL_LookupTable_TensionUnitDepth : BaseRepositoryModel
        {
            [PrimaryKey]
            public string Material_Grade { get; set; } = "";
            [PrimaryKey]
            public float Coil_Thickness_Min { get; set; } = 0.0f;
            [PrimaryKey]
            public float Coil_Thickness_Max { get; set; } = 0.0f;
            public int TensionUnitDepth { get; set; } = 0;
            public override DateTime UpdateTime { get; set; }
        }
    }
}
