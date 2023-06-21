using DBService.Base;
using System;

namespace DBService.Repository.PDO
{
    public class CoilPDORepo : BaseRepository<PDOEntity.TBL_PDO>
    {
        protected override string TableName => nameof(PDOEntity.TBL_PDO);

        protected override string[] PKName => new string[] { nameof(PDOEntity.TBL_PDO.Plan_No), nameof(PDOEntity.TBL_PDO.Out_Coil_ID) };

        public CoilPDORepo(string connStr) : base(connStr)
        {
        }

        public int UpdateExitCoilIDChecked(string exitCoilNo, string entryCoilIDChecked)
        {
            var dbObj = new
            {
                Exit_CoilID_Checked = entryCoilIDChecked,
            }; 
            
            return DBContext.Update(TableName, dbObj, $"{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)} = '{exitCoilNo}'");
        }

        // 更新PDO毛重, 淨重
        public int UpdatePDOCoilWt(string exitCoilNo, float coilPureWt, float coilGrossWt)
        {
            var dbObj = new
            {
                Out_Coil_Gross_WT = coilGrossWt,
                Out_Coil_Wt = coilPureWt

            };
            return DBContext.Update(TableName, dbObj, $"{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)} = '{exitCoilNo}'");
        }

        public int UpdateUploadPDOUserID(string planNo, string coilID, string uploadUserID)
        {
            var dbObj = new
            {
                PDO_Uploaded_UserID = uploadUserID,
                PDO_Uploaded_Time = DateTime.Now
            };

            var where = $"{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)} = '{coilID}'" + " AND " + $"{nameof(PDOEntity.TBL_PDO.Plan_No)} = '{planNo}'";

            return DBContext.Update(TableName, dbObj, where);
        }


        public int UpdateUploadPDOCheck(string planNo, string coilID, string uploadFlag)
        {
            var dbObj = new
            {
                PDO_Uploaded_Flag = uploadFlag,
                PDO_Uploaded_Time = DateTime.Now
            };

            var where = $"{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)} = '{coilID}'" + " AND " + $"{nameof(PDOEntity.TBL_PDO.Plan_No)} = '{planNo}'";

            return DBContext.Update(TableName, dbObj, where);
        }
    }
}
