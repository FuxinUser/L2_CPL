using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_WeldCT
{
    public class L2L25_WeldCTRepo : BaseRepository<L2L25_WeldCT>
    {
        protected override string TableName => nameof(L2L25_WeldCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_WeldCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
