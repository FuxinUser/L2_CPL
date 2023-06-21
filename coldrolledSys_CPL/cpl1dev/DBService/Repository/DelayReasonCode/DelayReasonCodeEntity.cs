using System;
using DBService.Base;

namespace DBService.Repository.DelayReasonCode
{
    public class DelayReasonCodeEntity
    {
        public class TBL_DelayReasonCode_Definition : BaseRepositoryModel
        {
            public int Index_No { get; set; }
            public string Delay_ReasonCode { get; set; }
            public string Delay_ReasonName { get; set; }
            public string Delay_GroupCode { get; set; }
            public string Delay_GroupName { get; set; }
            public string Responsible_Department { get; set; }
            public string Create_UserID { get; set; }
            public override DateTime CreateTime { get; set; }
        }
    }
}
