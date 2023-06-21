using DBService.Base;

namespace DBService.Repository.SideTrimmerTmp
{
    public class SideTrimmerTmpRepo : BaseRepository<SideTrimmerTmpEntity.TBL_SideTrimmerTmp>
    {

        public SideTrimmerTmpRepo(string connStr) : base(connStr)
        {
        }

        protected override string TableName => nameof(SideTrimmerTmpEntity.TBL_SideTrimmerTmp);

        protected override string[] PKName => new string[] { nameof(SideTrimmerTmpEntity.TBL_SideTrimmerTmp.ReceiveTime)};
    }
}
