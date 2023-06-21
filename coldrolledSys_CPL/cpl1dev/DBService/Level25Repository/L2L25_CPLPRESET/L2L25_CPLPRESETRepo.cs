using DBService.Base;
using System;

namespace DBService.Level25Repository.L2L25_CPL1PRESET
{
    public class L2L25_CPLPRESETRepo : BaseRepository<L2L25_CPLPRESET>
    {
        protected override string TableName => nameof(L2L25_CPLPRESET);

        protected override string[] PKName => throw new NotImplementedException();


        public L2L25_CPLPRESETRepo(string connStr) : base(connStr)
        {

        }
    }
}
