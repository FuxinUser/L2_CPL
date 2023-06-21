using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_RECTensionCT
{
    public class L2L25_RECTensionCTRepo : BaseRepository<L2L25_RECTensionCT>
    {
        protected override string TableName => nameof(L2L25_RECTensionCT);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_RECTensionCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
