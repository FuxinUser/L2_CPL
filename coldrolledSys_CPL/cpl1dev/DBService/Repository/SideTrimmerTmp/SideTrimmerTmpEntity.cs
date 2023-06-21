using DBService.Base;
using System;

namespace DBService.Repository.SideTrimmerTmp
{
    public class SideTrimmerTmpEntity
    {
        public class TBL_SideTrimmerTmp : BaseRepositoryModel
        {
            public float Side_Trimmer_Gap { get; set; }

            public float Side_Trimmer_Lap { get; set; }

            public float Side_Trimmer_Width { get; set; }

            public float Trimming_OperateSide { get; set; }

            public float Trimming_DriveSide { get; set; }

            public DateTime ReceiveTime { get; set; }

        }

    }
}
