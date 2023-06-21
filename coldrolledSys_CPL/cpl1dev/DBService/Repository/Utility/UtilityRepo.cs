using DBService.Base;

namespace DBService.Repository.Utility
{
    public class UtilityRepo : BaseRepository<UtilityEntity.TBL_Utility>
    {
        protected override string TableName => nameof(UtilityEntity.TBL_Utility);

        protected override string[] PKName => new string[] { nameof(UtilityEntity.TBL_Utility.Receive_Time)};

        public UtilityRepo(string connStr) : base(connStr)
        {
        }
    }
}
