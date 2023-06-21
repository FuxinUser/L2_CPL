using DBService.Base;
using static DBService.Repository.Leader.LeaderTempEntity;

namespace DBService.Repository.Leader
{
    public class LeaderTempRepo : BaseRepository<TBL_Leader_Temp>
    {
        protected override string TableName => nameof(TBL_Leader_Temp);

        protected override string[] PKName => new string[] { nameof(TBL_Leader_Temp.Coil_ID) };

        public LeaderTempRepo(string connStr) : base(connStr)
        {

        }


    }
}
