using System;
using DBService.Base;

namespace DBService.Repository.DelayLocation
{
    public class DelayLocationEntity
    {
        public class TBL_DelayLocation_Definition : BaseRepositoryModel
        {
            public int Index_No { get; set; }
            public string Delay_LocationCode { get; set; }
            public string Delay_LocationName { get; set; }
            public string Create_UserID { get; set; }
            public override DateTime CreateTime { get; set; }
        }
    }
}
