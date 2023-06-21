using DBService.Base;

namespace DBService.Repository.SystemSetting
{
    public class SystemParameterRepo : BaseRepository<SystemParameterEntity.TBL_SystemParameter>
    {

        public SystemParameterRepo(string connStr) : base(connStr)
        {
        }

        protected override string TableName => nameof(SystemParameterEntity.TBL_SystemParameter);

        protected override string[] PKName => new string[] { nameof(SystemParameterEntity.TBL_SystemParameter.Name) };
    }
}
