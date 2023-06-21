using DBService.Base;

namespace DBService.Repository.DefectData
{
    public class DefectDataRepo : BaseRepository<CoilDefectDataEntity.TBL_Coil_Defect>
    {
        public DefectDataRepo(string connStr) : base(connStr)
        {
        }

        protected override string TableName => nameof(CoilDefectDataEntity.TBL_Coil_Defect);
        protected override string[] PKName => new string[] { nameof(CoilDefectDataEntity.TBL_Coil_Defect.Plan_No), nameof(CoilDefectDataEntity.TBL_Coil_Defect.Entry_Coil_ID)};
    }
}
