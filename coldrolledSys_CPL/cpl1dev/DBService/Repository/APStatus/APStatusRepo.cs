using DBService.Base;
using static DBService.Repository.APStatus.APStatusEntity;

namespace DBService.Repository.APStatus
{
    public class APStatusRepo : BaseRepository<TBL_APStatus>
    {
        protected override string TableName => nameof(TBL_APStatus);

        protected override string[] PKName => new string[] { nameof(TBL_APStatus.GroupName), nameof(TBL_APStatus.GroupName) };

        public APStatusRepo(string connStr) : base(connStr)
        {

        }
    }
}
