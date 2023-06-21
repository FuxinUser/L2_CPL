using DBService.Base;
using static DBService.Repository.ScheduleDelete_CoilReject_Record_Temp.ScheduleDeleteRecordTempEntity;


namespace DBService.Repository.ScheduleDelete_CoilReject_Record_Temp
{
    public class SchDelCoilRejectRecordTempRepo : BaseRepository<TBL_ScheduleDelete_CoilReject_Temp>
    {
        protected override string TableName => nameof(TBL_ScheduleDelete_CoilReject_Temp);

        protected override string[] PKName => new string[] { nameof(TBL_ScheduleDelete_CoilReject_Temp.Coil_ID) };

        public SchDelCoilRejectRecordTempRepo(string connStr) : base(connStr)
        {



        }
    }
}
