using DBService.Base;
using System;

namespace DBService.Repository.ProcessDataCT
{
    public class ProcessDataCTEntity
    {
        public class TBL_ProcessDataCT : BaseRepositoryModel
        {
            public string Out_Coil_No { get; set; }
            public string In_Coil_ID { get; set; }
            public string OriPDI_Out_Coil_ID { get; set; }
            public string BeginDate { get; set; }
            public string BeginTime { get; set; }
            public string StopDate { get; set; }
            public string StopTime { get; set; }
            public string SeqUnit { get; set; }
            public string DataCode { get; set; }
            public string DataDesc { get; set; }
            public int DataCount { get; set; }
            public string DataString { get; set; }

            public override DateTime CreateTime { get; set; }
        }

    }
}
