using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_RECCurrentCT
{
    public class L2L25_RECCurrentCTRepo : BaseRepository<L2L25_RECCurrentCT>
    {
        protected override string TableName => nameof(L2L25_RECCurrentCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_RECCurrentCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
