using DBService.Base;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.LookupTblPaper
{
    public class LkUpTablePaperEntity
    {
        public class TBL_LookupTable_Paper : BaseRepositoryModel
        {
            [PrimaryKey]
            public string Paper_Code { get; set; }
            public int Paper_Base_Weight { get; set; }
            public int Paper_Width { get; set; }
            public int Paper_Thick { get; set; }
            public int Paper_Min_Thick { get; set; }
            public int Paper_Max_Thick { get; set; }
        }
    }
}
