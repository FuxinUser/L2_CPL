using DBService.Base;

namespace DBService.Repository.UnmountRecord
{
    public class UmountRecordRepo : BaseRepository<UnmountRecordEntity.TBL_UnmountRecord>
    {

        public UmountRecordRepo(string connStr) : base(connStr)
        {
        }

        protected override string TableName => nameof(UnmountRecordEntity.TBL_UnmountRecord);

        protected override string[] PKName => new string[] { nameof(UnmountRecordEntity.TBL_UnmountRecord.Coil_ID) };
    }
}
