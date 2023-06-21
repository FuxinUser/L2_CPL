using DBService.Base;

namespace DBService.Repository.LineStatus
{
    public class ProcessDataRepo : BaseRepository<ProcessDataEntity.TBL_ProcessData>
    {
        protected override string TableName => nameof(ProcessDataEntity.TBL_ProcessData);

        protected override string[] PKName => new string[] { nameof(ProcessDataEntity.TBL_ProcessData.ReceiveTime) };

        public ProcessDataRepo(string connStr) : base(connStr)
        {

        }
    }
}
