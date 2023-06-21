using DBService.Base;
using System;

namespace DBService.Repository.SystemSetting
{
    public class SystemParameterEntity
    {
        public class TBL_SystemParameter : BaseRepositoryModel
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public DateTime ValueDate { get; set; }
            public string Remark { get; set; }
        }
    }
}
