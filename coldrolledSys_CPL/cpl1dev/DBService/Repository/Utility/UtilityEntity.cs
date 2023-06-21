using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.Utility
{
    public class UtilityEntity
    {

        public class TBL_Utility : BaseRepositoryModel
        {
            [PrimaryKey]
            public DateTime Receive_Time { get; set; }
            
            public string Shift { get; set; }

            public string Team { get; set; }
            public float CompressedAir { get; set; }
            public float IndirectCollingWater { get; set; }
        }

    }
}
