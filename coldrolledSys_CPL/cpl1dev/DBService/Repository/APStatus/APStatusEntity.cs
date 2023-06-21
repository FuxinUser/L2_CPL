using DBService.Base;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.APStatus
{
    public class APStatusEntity
    {
        public class TBL_APStatus : BaseRepositoryModel
        {
   
            [PrimaryKey]
            public string GroupName { get; set; }
            [PrimaryKey]
            public string Name { get; set; }

            public string Status { get; set; }

            public string Description { get; set; }

            public string Updater { get; set; }
        }

    }
}
