using DBService.Base;

namespace DBService.Level25Repository.L2L25_DownTime
{
    public class L2L25_DownTime : BaseRepositoryModel
    {
		public string Message_Length { get; set; }
		public string Message_Id { get; set; }
		public string Date { get; set; }
		public string Time { get; set; }
		public string OP_FLAG { get; set; }
		public string UNIT_CODE { get; set; }
		public string PROD_DATE { get; set; }
		public string PROD_SHIFT_NO { get; set; }
		public string PROD_SHIFT_GROUP { get; set; }
		public string STOP_START_TIME { get; set; }
		public string STOP_END_TIME { get; set; }
		public string DELAY_LOCATION { get; set; }
		public string DELAY_LOCATION_DESC { get; set; }
		public string STOP_ELASED_TIME { get; set; }
		public string STOP_REASON { get; set; }
		public string DELAY_REASON_DESC { get; set; }
		public string DELAY_REMARK { get; set; }
		public string RESP_DEPART_DELAY_TIME_M { get; set; }
		public string RESP_DEPART_DELAY_TIME_E { get; set; }
		public string RESP_DEPART_DELAY_TIME_C { get; set; }
		public string RESP_DEPART_DELAY_TIME_P { get; set; }
		public string RESP_DEPART_DELAY_TIME_U { get; set; }
		public string RESP_DEPART_DELAY_TIME_O { get; set; }
		public string RESP_DEPART_DELAY_TIME_R { get; set; }
		public string RESP_DEPART_DELAY_TIME_RS { get; set; }
		public string DECELERATION_CAUSE { get; set; }
		public string DECELERATION_CODE { get; set; }

	}
}
