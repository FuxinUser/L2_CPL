using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_UNCTensionCT
{
    public class L2L25_UNCTensionCTRepo : BaseRepository<L2L25_UNCTensionCT>
    {
        protected override string TableName => nameof(L2L25_UNCTensionCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_UNCTensionCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
