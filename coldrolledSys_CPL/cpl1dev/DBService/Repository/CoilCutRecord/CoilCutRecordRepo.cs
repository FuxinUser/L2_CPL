using DBService.Base;
using static DBService.Repository.CoilCutRecord.CoilCutRecordEntity;

namespace DBService.Repository.CoilCutRecord
{
    class CoilCutRecordRepo : BaseRepository<TBL_Coil_CutRecord>
    {
        protected override string TableName => nameof(TBL_Coil_CutRecord);

        protected override string[] PKName => new string[] { nameof(TBL_Coil_CutRecord.Coil_ID), nameof(TBL_Coil_CutRecord.CutTime) };

        public CoilCutRecordRepo(string connStr) : base(connStr)
        {

        }
    }
}
