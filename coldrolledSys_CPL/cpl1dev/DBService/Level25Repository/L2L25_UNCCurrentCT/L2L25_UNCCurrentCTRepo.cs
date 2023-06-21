using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_UNCCurrentCT
{
    public class L2L25_UNCCurrentCTRepo : BaseRepository<L2L25_UNCCurrentCT>
    {
        protected override string TableName => nameof(L2L25_UNCCurrentCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_UNCCurrentCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
