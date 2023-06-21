using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.CoilCutRecord
{
    public class CoilCutRecordEntity
    {
        public class TBL_Coil_CutRecord : BaseRepositoryModel
        {
   
            [PrimaryKey]
            public DateTime CutTime { get; set; }
            [PrimaryKey]
            public string Coil_ID { get; set; }

            public string In_Coil_ID { get; set; }

            public string Cut_Type { get; set; }

            public float CutLength { get; set; }

        }

    }
}
