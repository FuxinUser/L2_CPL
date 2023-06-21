using DBService.Base;

namespace DBService.Repository.WieldRecord
{
    public class WeldRecordsRepo : BaseRepository<WeldRecordEntity.TBL_WeldRecords>
    {
        protected override string TableName => nameof(WeldRecordEntity.TBL_WeldRecords);

        protected override string[] PKName => new string[] { nameof(WeldRecordEntity.TBL_WeldRecords.Coil_ID) };

        public WeldRecordsRepo(string connStr) : base(connStr)
        {
        }
    }
}
