using DBService.Base;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.SystemSetting
{
    public class SystemSettingEntity
    {
        public class TBL_SystemSetting : BaseRepositoryModel
        {
            [PrimaryKey]
            public string Parameter_Group { get; set; }
            [PrimaryKey]
            public string Parameter { get; set; }
            public string Value { get; set; }
            public string Remark { get; set; }
        }
    }
}
