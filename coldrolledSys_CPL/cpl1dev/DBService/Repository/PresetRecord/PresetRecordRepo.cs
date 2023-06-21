using DBService.Base;

namespace DBService.Repository.PresetRecord
{
    public class PresetRecordRepo : BaseRepository<PresetRecordEntity.TBL_PresetRecord>
    {
        protected override string TableName => nameof(PresetRecordEntity.TBL_PresetRecord);

        protected override string[] PKName => new string[] { nameof(PresetRecordEntity.TBL_PresetRecord.Coil_ID) };

        public PresetRecordRepo(string connStr) : base(connStr)
        {

        }
    }
}
