using DBService.Base;
using static DBService.Repository.LookupTblTensionUnitDepth.LkUpTableTensionUnitDepthEntity;

namespace DBService.Repository.LookupTblTensionUnitDepth
{
    public class LkUpTableTensionUnitDepthRepo : BaseRepository<TBL_LookupTable_TensionUnitDepth>
    {
        protected override string TableName => nameof(TBL_LookupTable_TensionUnitDepth);
        protected override string[] PKName => new string[] { nameof(TBL_LookupTable_TensionUnitDepth.Material_Grade), 
                                                             nameof(TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Min),
                                                             nameof(TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Max)};

        public LkUpTableTensionUnitDepthRepo(string connStr) : base(connStr)
        {

        }
    }
}
