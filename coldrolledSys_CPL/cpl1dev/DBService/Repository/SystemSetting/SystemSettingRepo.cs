using DBService.Base;

namespace DBService.Repository.SystemSetting
{
    public class SystemSettingRepo : BaseRepository<SystemSettingEntity.TBL_SystemSetting>
    {

        public SystemSettingRepo(string connStr) : base(connStr)
        {
        }

        protected override string TableName => nameof(SystemSettingEntity.TBL_SystemSetting);

        protected override string[] PKName => new string[] { nameof(SystemSettingEntity.TBL_SystemSetting.Parameter_Group), nameof(SystemSettingEntity.TBL_SystemSetting.Parameter) };
    }
}
