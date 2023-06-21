using DBService.Base;
using System;

namespace DBService.Repository.PDI
{
    public class CoilPDIRepo : BaseRepository<CoilPDIEntity.TBL_PDI>
    {
        public CoilPDIRepo(string connStr) : base(connStr)
        {
        }

        protected override string TableName => nameof(CoilPDIEntity.TBL_PDI);

        protected override string[] PKName => new string[] { nameof(CoilPDIEntity.TBL_PDI.Plan_No), nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)};

        public string GetPlanNoByEntryCoilID(string entryCoilID)
        {
            var sql = $"select {nameof(CoilPDIEntity.TBL_PDI.Plan_No)} From {TableName} where {nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)} = '{entryCoilID}'";
            return DBContext.QuerySingleOrDefault<string>(sql);
        }

        public int UpdateEntryScanCoilInfo(string scanCoilID, string entryCoilIDChecked)
        {
            var dbObj = new
            {
                Entry_Scaned_Coil_ID = scanCoilID,
                Entry_Coil_ID_Checked = entryCoilIDChecked,
                Entry_Scaned_Time = DateTime.Now,

            };

            return DBContext.Update(TableName, dbObj, $"{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)} = '{scanCoilID}'");
        }

        

        public string GetSpeicDataByEntryCoilID(string columnName, string coilID)
        {
            var sql = $"select TOP 1 {columnName} From {TableName} where {nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)} = '{coilID}' order by {nameof(CoilPDIEntity.TBL_PDI.CreateTime)} desc";
            return DBContext.QuerySingleOrDefault<string>(sql);
        }

       public int UpdateField(string coilID, object dbObj)
        {
            return DBContext.Update(TableName, dbObj, $"{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)} = '{coilID}'");
        }
        
        
        public int UpdateStarTime(string coilID)
        {
            var dbObj = new
            {
                Start_Time = DateTime.Now,
            };
            return DBContext.Update(TableName, dbObj, $"{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)} = '{coilID}'");
        }

        public int UpdateFinishTime(string coilID)
        {
            var dbObj = new
            {
                Finish_Time = DateTime.Now,
            };
            return DBContext.Update(TableName, dbObj, $"{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_ID)} = '{coilID}'");
        }
      
        public int UpdateEntryTime(string coilID)
        {
            var dbObj = new
            {
                Entry_Arrive_Time = DateTime.Now,
            };
            return DBContext.Update(TableName, dbObj, $"{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)} = '{coilID}'");
        }



        public int UpdateEntryScanTime(string coilID)
        {
            var dbObj = new
            {
                Entry_Arrive_Time = DateTime.Now,
            };
            return DBContext.Update(TableName, dbObj, $"{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)} = '{coilID}'");
        }

  

        public int UpdateSampleFlag(string sample, string coilID)
        {
            var dbObj = new
            {
                Sample_Flag = sample,
            };
            return DBContext.Update(TableName, dbObj, $"{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)} = '{coilID}'");
        }
    }
}
