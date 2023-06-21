using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.LookupTbl
{
    public class LkUpTableLineTensionEntity
    {
        public class TBL_LookupTable_LineTension : BaseRepositoryModel
        {
            [PrimaryKey]
            public string Material_Grade { get; set; } = "";
            public int Coil_Width { get; set; } = 0;
            [PrimaryKey]
            public float Coil_Thickness_Max { get; set; } = 0.0f;
            [PrimaryKey]
            public float Coil_Thickness_Min { get; set; } = 0.0f;
            public float TS_STAND_MAX { get; set; } = 0.0f;
            public float TS_STAND_MIN { get; set; } = 0.0f;
            public float PORTension { get; set; } = 0;
            public float TRTension { get; set; } = 0;
            //public int UnitTension { get; set; } = 0;
            public override DateTime UpdateTime { get; set; }
        }
    }
}
