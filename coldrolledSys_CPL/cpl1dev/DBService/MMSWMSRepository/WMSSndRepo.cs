using DBService.Base;

namespace DBService.MMSWMSRepository
{
    public class WMSSndRepo : BaseRepository<MMS_WMS_MsgRecord>
    {
        protected override string TableName => "TBL_WMS_SendRecord";

        protected override string[] PKName => new string[] { nameof(MMS_WMS_MsgRecord.CreateTime) };


        public WMSSndRepo(string connStr) : base(connStr)
        {

        }
    }
}
