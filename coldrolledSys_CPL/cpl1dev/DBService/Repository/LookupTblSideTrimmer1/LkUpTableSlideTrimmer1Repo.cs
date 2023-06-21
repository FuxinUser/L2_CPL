using DBService.Base;
using System;

namespace DBService.Repository.LookupTblSideTrimmer1
{
    public class LkUpTableSlideTrimmer1Repo : BaseRepository<LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1>
    {
        protected override string TableName => nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1);
        protected override string[] PKName => throw new NotImplementedException();

        public LkUpTableSlideTrimmer1Repo(string connStr) : base(connStr)
        {

        }
    }
}
