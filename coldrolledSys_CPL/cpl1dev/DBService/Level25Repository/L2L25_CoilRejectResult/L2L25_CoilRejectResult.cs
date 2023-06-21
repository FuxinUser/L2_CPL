using DBService.Base;

namespace DBService.Level25Repository.L2L25_CoilRejectResult
{
    public class L2L25_CoilRejectResult : BaseRepositoryModel
    {
        public string Message_Length { get; set; }
        public string Message_Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string OUT_MAT_NO { get; set; }
        public string IN_MAT_NO { get; set; }
        public string MAT_RETURN_MODE { get; set; }
        public string RETURN_MAT_LEN { get; set; }
        public string RETURN_MAT_WT { get; set; }
        public string RETURN_MAT_OUTER_DIA { get; set; }
        public string OUT_MAT_INNER_DIA { get; set; }
        public string MAT_RETURN_CAUSE_CODE { get; set; }
        public string RETURN_TIME { get; set; }
        public string PROD_SHIFT_NO { get; set; }
        public string PROD_SHIFT_GROUP { get; set; }
    }
}
