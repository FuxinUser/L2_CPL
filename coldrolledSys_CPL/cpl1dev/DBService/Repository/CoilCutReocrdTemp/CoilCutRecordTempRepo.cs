using DBService.Base;
using static DBService.Repository.CutReocrd.CoilCutRecordTempEntity;

namespace DBService.Repository.CoilCutReocrd
{
    public class CoilCutRecordTempRepo : BaseRepository<TBL_Coil_CutRecord_Temp>
    {
        protected override string TableName => nameof(TBL_Coil_CutRecord_Temp);

        protected override string[] PKName => new string[] { nameof(TBL_Coil_CutRecord_Temp.Coil_ID), nameof(TBL_Coil_CutRecord_Temp.CutTime) };

        public CoilCutRecordTempRepo(string connStr) : base(connStr)
        {

        }
    }
}
