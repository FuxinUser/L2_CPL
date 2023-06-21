using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.ScheduleDelete_CoilReject_Temp
{
    public class ScheduleDelete_CoilReject_Temp_Entity
    {
        public class TBL_ScheduleDelete_CoilReject_Temp : BaseRepositoryModel
        { 
            public string Coil_ID { get; set; } = string.Empty;
            public string Record_Type { get; set; } = string.Empty;
            public string Create_UserID { get; set; } = string.Empty;
            public DateTime CreateTime { get; set; } = DateTime.Now;
            public string ScheduleDelete_CoilReject_GroupNo { get; set; } = string.Empty;
            public string ScheduleDelete_CoilReject_Code { get; set; } = string.Empty;
            public string Entry_CoilNo { get; set; } = string.Empty;
            public string Plan_No { get; set; } = string.Empty;
            public string Mode_Of_Reject { get; set; } = string.Empty;
            public string Length_Of_Rejected_Coil { get; set; } = string.Empty;
            public string Weight_Of_Rejected_Coil { get; set; } = string.Empty;
            public string Inner_Diameter_Of_RejectedCoil { get; set; } = string.Empty;
            public string Outer_Diameter_Of_RejectedCoil { get; set; } = string.Empty;
            public string Width_Of_RejectedCoil { get; set; } = string.Empty;
            public string Reason_Of_Reject { get; set; } = string.Empty;
            public string Time_Of_Reject { get; set; } = string.Empty;
            public string Shift_Of_Reject { get; set; } = string.Empty;
            public string Turn_Of_Reject { get; set; } = string.Empty;
            public string Paper_exit_Code { get; set; } = string.Empty;
            public string Paper_Type { get; set; } = string.Empty;
            public string FINAL_COIL_FLAG { get; set; } = string.Empty;
            public int HEAD_PAPER_LENGTH { get; set; } = 0;
            public float HEAD_PAPER_WIDTH { get; set; } = 0;
            public int TAIL_PAPER_LENGTH { get; set; } = 0;
            public float TAIL_PAPER_WIDTH { get; set; } = 0;
            public string Remarks { get; set; } = string.Empty;
           

        }
    }
}
