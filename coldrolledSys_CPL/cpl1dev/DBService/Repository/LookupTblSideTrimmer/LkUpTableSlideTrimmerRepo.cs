using DBService.Base;
using System;

namespace DBService.Repository.LookupTblSideTrimmer
{
    public class LkUpTableSlideTrimmerRepo : BaseRepository<LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer>
    {
        protected override string TableName => nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer);
        protected override string[] PKName => throw new NotImplementedException();

        public LkUpTableSlideTrimmerRepo(string connStr) : base(connStr)
        {

        }
    }
}
