using DBService.Base;
using System;

namespace DBService.Repository.FaultCode
{
    public class FaultCodeEntity
    {
        public class TBL_FaultCode : BaseRepositoryModel
        {
            public int Sequence_No { get; set; }
            public string Fault_Code { get; set; }
            public string Fault_Description { get; set; }
            public override DateTime CreateTime { get; set; }
        }
    }
}
