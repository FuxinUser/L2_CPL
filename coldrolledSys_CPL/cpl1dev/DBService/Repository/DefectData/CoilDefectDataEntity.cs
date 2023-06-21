using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.DefectData
{
    public class CoilDefectDataEntity
    {
        [Serializable]
        public class TBL_Coil_Defect : BaseRepositoryModel
        {
			[PrimaryKey]
			public string Plan_No { get; set; } = string.Empty;
			[PrimaryKey]
			public string Coil_ID { get; set; } = string.Empty; 
			public string Entry_Coil_ID { get; set; } = string.Empty;
			public string D01_Code { get; set; } = string.Empty;
			public string D01_Origin { get; set; } = string.Empty;
			public string D01_Sid { get; set; } = string.Empty;
			public string D01_Pos_W { get; set; } = string.Empty;
			public string D01_Pos_L_Start { get; set; } = string.Empty;
			public string D01_Pos_L_End { get; set; } = string.Empty;
			public string D01_Level { get; set; } = string.Empty;
			public string D01_Percent { get; set; } = string.Empty;
			public string D01_QGRADE { get; set; } = string.Empty;
			public string D02_Code { get; set; } = string.Empty;
			public string D02_Origin { get; set; } = string.Empty;
			public string D02_Sid { get; set; } = string.Empty;
			public string D02_Pos_W { get; set; } = string.Empty;
			public string D02_Pos_L_Start { get; set; } = string.Empty;
			public string D02_Pos_L_End { get; set; } = string.Empty;
			public string D02_Level { get; set; } = string.Empty;
			public string D02_Percent { get; set; } = string.Empty;
			public string D02_QGRADE { get; set; } = string.Empty;
			public string D03_Code { get; set; } = string.Empty;
			public string D03_Origin { get; set; } = string.Empty;
			public string D03_Sid { get; set; } = string.Empty;
			public string D03_Pos_W { get; set; } = string.Empty;
			public string D03_Pos_L_Start { get; set; } = string.Empty;
			public string D03_Pos_L_End { get; set; } = string.Empty;
			public string D03_Level { get; set; } = string.Empty;
			public string D03_Percent { get; set; } = string.Empty;
			public string D03_QGRADE { get; set; } = string.Empty;
			public string D04_Code { get; set; } = string.Empty;
			public string D04_Origin { get; set; } = string.Empty;
			public string D04_Sid { get; set; } = string.Empty;
			public string D04_Pos_W { get; set; } = string.Empty;
			public string D04_Pos_L_Start { get; set; } = string.Empty;
			public string D04_Pos_L_End { get; set; } = string.Empty;
			public string D04_Level { get; set; } = string.Empty;
			public string D04_Percent { get; set; } = string.Empty;
			public string D04_QGRADE { get; set; } = string.Empty;
			public string D05_Code { get; set; } = string.Empty;
			public string D05_Origin { get; set; } = string.Empty;
			public string D05_Sid { get; set; } = string.Empty;
			public string D05_Pos_W { get; set; } = string.Empty;
			public string D05_Pos_L_Start { get; set; } = string.Empty;
			public string D05_Pos_L_End { get; set; } = string.Empty;
			public string D05_Level { get; set; } = string.Empty;
			public string D05_Percent { get; set; } = string.Empty;
			public string D05_QGRADE { get; set; } = string.Empty;
			public string D06_Code { get; set; } = string.Empty;
			public string D06_Origin { get; set; } = string.Empty;
			public string D06_Sid { get; set; } = string.Empty;
			public string D06_Pos_W { get; set; } = string.Empty;
			public string D06_Pos_L_Start { get; set; } = string.Empty;
			public string D06_Pos_L_End { get; set; } = string.Empty;
			public string D06_Level { get; set; } = string.Empty;
			public string D06_Percent { get; set; } = string.Empty;
			public string D06_QGRADE { get; set; } = string.Empty;
			public string D07_Code { get; set; } = string.Empty;
			public string D07_Origin { get; set; } = string.Empty;
			public string D07_Sid { get; set; } = string.Empty;
			public string D07_Pos_W { get; set; } = string.Empty;
			public string D07_Pos_L_Start { get; set; } = string.Empty;
			public string D07_Pos_L_End { get; set; } = string.Empty;
			public string D07_Level { get; set; } = string.Empty;
			public string D07_Percent { get; set; } = string.Empty;
			public string D07_QGRADE { get; set; } = string.Empty;
			public string D08_Code { get; set; } = string.Empty;
			public string D08_Origin { get; set; } = string.Empty;
			public string D08_Sid { get; set; } = string.Empty;
			public string D08_Pos_W { get; set; } = string.Empty;
			public string D08_Pos_L_Start { get; set; } = string.Empty;
			public string D08_Pos_L_End { get; set; } = string.Empty;
			public string D08_Level { get; set; } = string.Empty;
			public string D08_Percent { get; set; } = string.Empty;
			public string D08_QGRADE { get; set; } = string.Empty;
			public string D09_Code { get; set; } = string.Empty;
			public string D09_Origin { get; set; } = string.Empty;
			public string D09_Sid { get; set; } = string.Empty;
			public string D09_Pos_W { get; set; } = string.Empty;
			public string D09_Pos_L_Start { get; set; } = string.Empty;
			public string D09_Pos_L_End { get; set; } = string.Empty;
			public string D09_Level { get; set; } = string.Empty;
			public string D09_Percent { get; set; } = string.Empty;
			public string D09_QGRADE { get; set; } = string.Empty;
			public string D10_Code { get; set; } = string.Empty;
			public string D10_Origin { get; set; } = string.Empty;
			public string D10_Sid { get; set; } = string.Empty;
			public string D10_Pos_W { get; set; } = string.Empty;
			public string D10_Pos_L_Start { get; set; } = string.Empty;
			public string D10_Pos_L_End { get; set; } = string.Empty;
			public string D10_Level { get; set; } = string.Empty;
			public string D10_Percent { get; set; } = string.Empty;
			public string D10_QGRADE { get; set; } = string.Empty;
			public string Modify_UserID { get; set; } = string.Empty;
			public DateTime ModifyTime { get; set; }
		}
    }
}
