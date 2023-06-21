using DBService.Base;
using System;

namespace DBService.Repository.ProcessDataCT
{
    public class ProcessDataCTRepo : BaseRepository<ProcessDataCTEntity.TBL_ProcessDataCT>
    {
        protected override string TableName => nameof(ProcessDataCTEntity.TBL_ProcessDataCT);

        protected override string[] PKName => throw new NotImplementedException();

        public ProcessDataCTRepo(string connStr) : base(connStr)
        {

        }
    }
}
