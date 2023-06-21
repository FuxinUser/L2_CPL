using DBService.Base;

namespace DBService.L1Repository
{
    public class L1201HisMsgRepo : BaseRepository<L2L1MsgDBModel.L2L1_201>
    {
        protected override string TableName => nameof(L2L1MsgDBModel.L2L1_201);

        protected override string[] PKName => new string[] { nameof(L2L1MsgDBModel.L2L1_201.Date), nameof(L2L1MsgDBModel.L2L1_201.Time) };


        public L1201HisMsgRepo(string connStr) : base(connStr)
        {

        }
    }
}
