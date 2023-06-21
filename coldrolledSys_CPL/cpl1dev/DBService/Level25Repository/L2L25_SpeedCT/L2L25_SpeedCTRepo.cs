using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_SpeedCT
{
    public class L2L25_SpeedCTRepo : BaseRepository<L2L25_SpeedCT>
    {
        protected override string TableName => nameof(L2L25_SpeedCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_SpeedCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
