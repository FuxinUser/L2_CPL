using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.ScheduleDelete_CoilReject_Record_Temp
{
    public class ScheduleDeleteRecordTempEntity
	{
		/// <summary>
		/// 排程跳軋/鋼捲退料暫存記錄
		/// </summary>
		public class TBL_ScheduleDelete_CoilReject_Temp : BaseRepositoryModel
		{
			/// <summary>
			/// 鋼卷編號
			/// </summary>
			[PrimaryKey]
			public string Coil_ID { get; set; }
			/// <summary>
			/// 資料類型; S = 排程跳軋 ; C = 鋼捲退料
			/// </summary>
			public string Record_Type { get; set; }
			/// <summary>
			/// 建立人員職工編號
			/// </summary>
			public string Create_UserID { get; set; }
			/// <summary>
			/// 建立/更新日期時間
			/// </summary>
			public override DateTime CreateTime { get; set; }
			/// <summary>
			/// 排程刪除群組代碼
			/// </summary>
			public string ScheduleDelete_CoilReject_GroupNo { get; set; }
			/// <summary>
			/// 排程刪除代碼
			/// </summary>
			public string ScheduleDelete_CoilReject_Code { get; set; }
			/// <summary>
			/// 入口卷號
			/// </summary>
			public string Entry_Coil_ID { get; set; }
			/// <summary>
			/// 原PDI出口卷
			/// </summary>
			public string OriPDI_Out_Coil_ID { get; set; }
			/// <summary>
			/// 計畫號
			/// </summary>
			public string Plan_No { get; set; }
			/// <summary>
			/// 回退方式 1:Whole Coil Rejected 2:Half Coil Rejected
			/// </summary>
			public string Mode_Of_Reject { get; set; }
			/// <summary>
			/// 回退卷長
			/// </summary>
			public string Length_Of_Rejected_Coil { get; set; }
			/// <summary>
			/// 回退卷重
			/// </summary>
			public string Weight_Of_Rejected_Coil { get; set; }
			/// <summary>
			/// 回退卷內徑
			/// </summary>
			public string Inner_Diameter_Of_RejectedCoil { get; set; }
			/// <summary>
			/// 回退卷外徑
			/// </summary>
			public string Outer_Diameter_Of_RejectedCoil { get; set; }
			/// <summary>
			/// 回退卷寬度
			/// </summary>
			public string Width_Of_RejectedCoil { get; set; }
			/// <summary>
			/// 回退原因代碼 01：设备故障  02：入料错误  03：更改作业排程  04：半卷分切
			/// </summary>
			public string Reason_Of_Reject { get; set; }
			/// <summary>
			/// 回退時間
			/// </summary>
			public string Time_Of_Reject { get; set; }
			/// <summary>
			/// 班次	1/2/3
			/// </summary>
			public string Shift_Of_Reject { get; set; }
			/// <summary>
			/// 班別	A/B/C/D
			/// </summary>
			public string Turn_Of_Reject { get; set; }
			/// <summary>
			/// 墊紙方式	由二級人工輸入
			/// </summary>
			public string Paper_exit_Code { get; set; }
			/// <summary>
			/// 墊紙類型
			/// </summary>
			public string Paper_Type { get; set; }
			/// <summary>
			/// 最后钢卷标记	0：非最终分卷 1：最终卷
			/// </summary>
			public string FINAL_COIL_FLAG { get; set; }
			/// <summary>
			/// 头部垫纸长度
			/// </summary>
			public string HEAD_PAPER_LENGTH { get; set; }
			/// <summary>
			/// 头部垫纸宽度
			/// </summary>
			public string HEAD_PAPER_WIDTH { get; set; }
			/// <summary>
			/// 尾部垫纸长度
			/// </summary>
			public string TAIL_PAPER_LENGTH { get; set; }
			/// <summary>
			/// 尾部垫纸宽度
			/// </summary>
			public string TAIL_PAPER_WIDTH { get; set; }
			/// <summary>
			/// 退料鞍座
			/// </summary>
			public string Reject_Skid { get; set; }
			public string Remarks { get; set; }

            public override DateTime UpdateTime { get; set; }
		}
	}
}
