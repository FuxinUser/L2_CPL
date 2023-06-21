using Core.Define;
using System;

namespace DataMod.Response
{
    public class RespnseModel
    {
		/// <summary>
		/// 排程刪除資訊暫存資料
		/// </summary>
		[Serializable]
		public class ScheduleDeleteCoilRejectTempInfo
        {
			public string Coil_ID { get; set; }
			// 資料類型; S = 排程跳軋 ; C = 鋼捲退料
			public string Record_Type { get; set; }
			// 建立人員職工編號
			public string Create_UserID { get; set; }			
			// 排程刪除群組代碼
			public string ScheduleDelete_CoilReject_GroupNo { get; set; }
			// 排程刪除代碼
			public string ScheduleDelete_CoilReject_Code { get; set; }
			// 入口卷號
			public string Entry_Coil_ID { get; set; }
            // 原PDI出口卷
            public string OriPDI_Out_Coil_ID { get; set; }
            // 計畫號
            public string Plan_No { get; set; }
			// 回退方式 1:Whole Coil Rejected 2:Half Coil Rejected
			public string Mode_Of_Reject { get; set; }
			// 回退卷長
			public string Length_Of_Rejected_Coil { get; set; }
			// 回退卷重
			public string Weight_Of_Rejected_Coil { get; set; }
			// 回退卷內徑
			public string Inner_Diameter_Of_RejectedCoil { get; set; }
			// 回退卷外徑
			public string Outer_Diameter_Of_RejectedCoil { get; set; }
			// 回退卷寬度
			public string Width_Of_RejectedCoil { get; set; }
			// 回退原因代碼 01：设备故障  02：入料错误  03：更改作业排程  04：半卷分切
			public string Reason_Of_Reject { get; set; }
			// 回退時間
			public string Time_Of_Reject { get; set; }
			// 班次	1/2/3
			public string Shift_Of_Reject { get; set; }
			// 班別	A/B/C/D
			public string Turn_Of_Reject { get; set; }
			// 墊紙方式	由二級人工輸入
			public string Paper_exit_Code { get; set; }
			// 墊紙類型
			public string Paper_Type { get; set; }
			// 最后钢卷标记	0：非最终分卷 1：最终卷
			public string FINAL_COIL_FLAG { get; set; }
			// 头部垫纸长度
			public string HEAD_PAPER_LENGTH { get; set; }
			// 头部垫纸宽度
			public string HEAD_PAPER_WIDTH { get; set; }
			// 尾部垫纸长度
			public string TAIL_PAPER_LENGTH { get; set; }
			// 尾部垫纸宽度
			public string TAIL_PAPER_WIDTH { get; set; }
            // 退料鞍座
            public string Reject_Skid { get; set; }
            public string Remarks { get; set; }

		}

		/// <summary>
		/// 回退鋼捲資訊
		/// </summary>
		[Serializable]
		public class ReturnCoilInfo
        {
            //鋼卷編號
            public string Coil_ID { get; set; }
			//資料類型 S=排程跳軋 C=鋼捲退料
			public string Record_Type { get; set; }
			//建立人員職工編號
			public string Create_UserID { get; set; }
            //排程刪除群組代碼
            public string ScheduleDelete_CoilReject_GroupNo { get; set; }
            //排程刪除代碼
            public string ScheduleDelete_CoilReject_Code { get; set; }
            //入口卷號
            public string Entry_Coil_ID { get; set; }
            // 原PDI出口卷
            public string OriPDI_Out_Coil_ID { get; set; }
            //計畫號
            public string Plan_No { get; set; }
            //回退方式
            public string Mode_Of_Reject { get; set; }
            //回退卷長
            public string Length_Of_Rejected_Coil { get; set; }
            //回退卷重
            public string Weight_Of_Rejected_Coil { get; set; }
            //回退卷內徑
            public string Inner_Diameter_Of_RejectedCoil { get; set; }
            //回退卷外徑
            public string Outer_Diameter_Of_RejectedCoil { get; set; }
            //回退卷寬度
            public string Width_Of_RejectedCoil { get; set; }
            //回退原因代碼
            public string Reason_Of_Reject { get; set; }
            //回退時間 
            public string Time_Of_Reject { get; set; }
            //班次
            public string Shift_Of_Reject { get; set; }
            //班別
            public string Turn_Of_Reject { get; set; }
            //墊紙方式
            public string Paper_exit_Code { get; set; }
            //墊紙類型
            public string Paper_Type { get; set; }
            //最后钢卷标记
            public string FINAL_COIL_FLAG { get; set; }
            //头部垫纸长度
            public string HEAD_PAPER_LENGTH { get; set; }
            //头部垫纸宽度
            public string HEAD_PAPER_WIDTH { get; set; }
            //尾部垫纸长度
            public string TAIL_PAPER_LENGTH { get; set; }
            //尾部垫纸宽度
            public string TAIL_PAPER_WIDTH { get; set; }

            public string Reject_Skid { get; set; }
            //備註
            public string Remarks { get; set; }
        }

		/// <summary>
		/// Defect Data
		/// </summary>
		[Serializable]
		public class DefectData
        {
            public string Plan_No { get; set; } = string.Empty;
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
        }

        /// <summary>
        /// 鋼捲整體回退資訊(回退資訊+Defect資料)
        /// </summary>
		[Serializable]
		public class CoilRejectInfo
        {
			public ReturnCoilInfo RetrunCoilInfo { get; private set; }

			public DefectData DefectInfo { get; private set; }

			public string FinalFalg { get; private set; }

		    public CoilRejectInfo(ReturnCoilInfo returnInfo, DefectData defect, string finalFlag)
            {
				RetrunCoilInfo = returnInfo;
				DefectInfo = defect;
				FinalFalg = finalFlag;
			}
		}

        /// <summary>
        /// PDI
        /// </summary>
		[Serializable]
		public class PDI {

            // 作業計畫號
            public string Plan_No { get; set; }
            // 計畫內順序號
            public int Mat_Seq_No { get; set; }
            // 作業計畫種類
            public string Plan_Sort { get; set; }
            // 入口卷號
            public string Entry_Coil_ID { get; set; }
            // 入口鋼卷厚度
            public float Entry_Coil_Thick { get; set; }
            // 入口鋼卷寬度
            public float Entry_Coil_Width { get; set; }
            // 入口鋼卷重量
            public int Entry_Coil_Weight { get; set; }
            // 入口鋼卷長度
            public int Entry_Coil_Length { get; set; }
            // 入口鋼卷內徑
            public int Entry_Coil_Inner { get; set; }
            // 入口鋼卷外徑
            public int Entry_Coil_Dcos { get; set; }
            // 入口套筒類型
            public string Sleeve_Type_Code { get; set; }
            // 入口套筒內徑
            public int Sleeve_diamter { get; set; }
            // 入口墊紙方式
            public string Paper_Req_Code { get; set; }
            // 入口墊紙類型
            public string Paper_Code { get; set; }
            // 入口頭部墊紙長度
            public int Head_Paper_Length { get; set; }
            // 入口頭部墊紙寬度
            public float Head_Paper_Width { get; set; }
            // 入口尾部墊紙長度
            public int Tail_Paper_Length { get; set; }
            // 入口尾部墊紙寬度
            public float Tail_Paper_Width { get; set; }
            // 抗拉最大值
            public int Ts_Stand_Max { get; set; }
            // 抗拉最小值
            public int Ts_Stand_Min { get; set; }
            // 內部鋼種
            public string St_No { get; set; }
            // 密度(工藝卡)
            public int Density { get; set; }                    //Kg/m³
            // 返修類型
            public string REPAIR_TYPE { get; set; }
            // 訂單表面精度代碼
            public string Surface_Finishing_Code { get; set; }
            // 實際表面精度代碼
            public string Surface_Accuracy { get; set; }
            // 好面朝向
            public string Base_Surface { get; set; }
            // 開卷方向
            public string Uncoiler_Direction { get; set; }
            // 出口鋼卷號
            public string Out_Coil_ID { get; set; }
            // 出口墊紙方式
            public string Out_Paper_Req_Code { get; set; }
            // 出口墊紙種類
            public string Out_Paper_Code { get; set; }
            // 出口套筒內徑
            public int Out_Sleeve_Diamter { get; set; }
            // 出口套筒類型
            public string Out_Sleeve_Type_Code { get; set; }
            // 出口綁帶方式 
            public int Out_Strap_Num { get; set; }
            // 導帶使用方式
            public string Leader_Flag { get; set; }
            // 取樣要求
            public string Sample_Flag { get; set; }
            // 取樣位置
            public string Sample_Frqn_Code { get; set; }
            // 試批號
            public string Sample_Lot_No { get; set; }
            // 鋼卷來源
            public string Coil_Origin { get; set; }
            // 上游工序
            public string Wholebacklog_Code { get; set; }
            // 下游工序
            public string Next_Wholebacklog_Code { get; set; }
            // 切邊要求
            public string Trim_Flag { get; set; }
            // 目標寬度
            public float Out_Coil_Width { get; set; }
            // 目標寬度最大值
            public float Out_Coil_Width_Max { get; set; }
            // 目標寬度最小值
            public float Out_Coil_Width_Min { get; set; }
            // 目標出口厚度
            public float Out_Coil_Thickness { get; set; }
            // 出口鋼卷內徑
            public int Out_Coil_Inner { get; set; }
            // 合同號
            public string Order_No { get; set; }
            // 訂貨單重上限
            public int Order_Wt_Max { get; set; }
            // 訂貨單重下限
            public int Order_Wt_Min { get; set; }
            // 目標訂貨單重
            public int Order_Wt { get; set; }
            // 分卷標記
            public string Dividing_Flag { get; set; }
            // 分卷數量
            public int Dividing_Num { get; set; }
            // 合同訂貨重量1
            public int Orderwt_1 { get; set; }
            // 合同訂貨重量2
            public int Orderwt_2 { get; set; }
            // 合同訂貨重量3
            public int Orderwt_3 { get; set; }
            // 合同訂貨重量4
            public int Orderwt_4 { get; set; }
            // 合同訂貨重量5
            public int Orderwt_5 { get; set; }
            // 合同訂貨重量6
            public int Orderwt_6 { get; set; }
            // 合同號1
            public string Order_No_1 { get; set; }
            // 合同號2
            public string Order_No_2 { get; set; }
            // 合同號3
            public string Order_No_3 { get; set; }
            // 合同號4
            public string Order_No_4 { get; set; }
            // 合同號5
            public string Order_No_5 { get; set; }
            // 合同號6
            public string Order_No_6 { get; set; }
            // 測試計畫號
            public string Test_Plan_No { get; set; }
            // QC備註
            public string Qc_Remark { get; set; }
            // 頭部未軋製區域
            public int Head_Off_Gauge { get; set; }
            // 尾部未軋製區域
            public int Tail_Off_Gauge { get; set; }
            // 內表面精度代碼
            public string Surface_Accu_Code_In { get; set; }
            // 外表面精度代碼
            public string Surface_Accu_Code_Out { get; set; }
            // 牌號
            public string Sg_Sign { get; set; }
            // 工序代碼
            public string Process_Code { get; set; }
            // 訂貨用戶代碼
            public string CustomerCode { get; set; }
            // 訂貨用戶英文名稱
            public string CustomerName_E { get; set; }
            // 訂貨用戶中文名稱
            public string CustomerName_C { get; set; }
            // 訂單表面精度描述
            public string Surface_Acc_Desc { get; set; }
            // 來料表面精度描述
            public string Surface_Accuracy_Desc { get; set; }
            // 内表面精度描述
            public string Surface_Acc_Desc_In { get; set; }
            // 外表面精度描述
            public string Surface_Acc_Desc_Out { get; set; }
            // 頭段導帶鋼種 - 待刪除

            public string Head_Strip_St_No { get; set; }
            // 頭段導帶長度 - 待刪除

            public int Head_Strip_Length { get; set; }
            // 頭段導帶寬度 - 待刪除
            public int Head_Strip_Width { get; set; }
            // 頭段導帶厚度 - 待刪除
            public float Head_Strip_Thickness { get; set; }
            // 尾段導帶鋼種 - 待刪除
            public string Tail_Strip_St_No { get; set; }
            // 尾段導帶長度 - 待刪除
            public int Tail_Strip_Length { get; set; }
            // 尾段導帶寬度 - 待刪除
            public int Tail_Strip_Width { get; set; }
            // 尾段導帶厚度 - 待刪除
            public float Tail_Strip_Thickness { get; set; }
            // 入口段掃描鋼卷號
            public string Entry_Scaned_Coil_ID { get; set; }
            // 入口段掃描日期
            public DateTime Entry_Scaned_Time { get; set; }
            // 掃描確認
            public string Entry_Coil_ID_Checked { get; set; }
            // 刪除註記
            public string Is_Delete { get; set; }
            // 刪除日期
            public DateTime Delete_DateTime { get; set; }
            // 刪除人員
            public string Delete_UserID { get; set; }
            // 建立人員
            public string Create_UserID { get; set; }
            // 入口段到達時間
            public DateTime Entry_Arrive_Time { get; set; }
            // 上POR開始生產時間
            public DateTime Start_Time { get; set; }
            // 回退時間
            public DateTime Coil_Reject_Time { get; set; }
            // 回退人員
            public string Coil_Reject_UserID { get; set; }
            // 鋼捲完成生產的時間 
            public DateTime Finish_Time { get; set; }

            public int EntryYieldStress
            {
                get
                {
                    return (Ts_Stand_Max + Ts_Stand_Min) / 2;
                }
            }
            // 尾段導帶體積
            public float TailStripVolume
            {
                get
                {
                    return Tail_Strip_Length * Tail_Strip_Width * Tail_Strip_Thickness;
                }
            }
            //頭段導帶體積
            public float HeadStripVolume
            {
                get
                {
                    return Head_Strip_Length * Head_Strip_Width * Head_Strip_Thickness;
                }
            }

            // 是否切割
            public bool IsDivid
            {
                get
                {
                    return Dividing_Flag.Equals(DBParaDef.Cut);
                }
            }

            // 取樣位置數量
            public int SampleCnt
            {
                get
                {
                    var cnt = 0;

                    if (Sample_Frqn_Code[0] == '1')
                        cnt++;

                    if (Sample_Frqn_Code[1] == '1')
                        cnt++;

                    if (Sample_Frqn_Code[2] == '1')
                        cnt++;

                    return cnt;
                }
            }
            // 取樣位置
            public string SamplePosStr
            {
                get
                {

                    if (Sample_Frqn_Code.Equals("100"))
                        return "H";

                    if (Sample_Frqn_Code.Equals("010"))
                        return "M";

                    return "T";
                }
            }

        }
        
        /// <summary>
        /// 鋼捲排程
        /// </summary>
        [Serializable]
        public class CoilSchedule{
            public string Coil_ID { get; set; }
            public short Seq_No { get; set; }
            // 排程狀態
            // N = 新鋼捲 ; R = 要求入料 ; F = 已入料 ; I = 身分確認成功 ; P = 生產中 ; D = 已產出 ; 
            public string Schedule_Status { get; set; }
            public string Update_Source { get; set; }
        }

        /// <summary>
        /// 鋼捲PDO
        /// </summary>
        [Serializable]
        public class PDO
        {
            public string OrderNo { get; set; }
            public string Plan_No { get; set; }
            public string Out_Coil_ID { get; set; }
            //[PrimaryKey]
            public string In_Coil_ID { get; set; }
            public string OriPDI_Out_Coil_ID { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime FinishTime { get; set; }
            public string Shift { get; set; }
            public string Team { get; set; }
            public float Out_Coil_Outer_Diameter { get; set; }
            public float Out_Coil_Inner { get; set; }
            public float Out_Theory_Wt { get; set; }
            public int Out_Coil_Wt { get; set; }
            public float Out_Coil_Gross_WT { get; set; }
            public float Out_Coil_Thick { get; set; }
            public float Out_Coil_Width { get; set; }
            public float Out_Coil_Length { get; set; }
            public string Paper_Code { get; set; }
            public string Paper_Req_Code { get; set; }
            public int Out_Head_Paper_Length { get; set; }
            public float Out_Head_Paper_Width { get; set; }
            public int Out_Tail_Paper_Length { get; set; }
            public float Out_Tail_Paper_Width { get; set; }
            public int Sleeve_Inner_Exit_Diamter { get; set; }
            public string Sleeve_Type_Exit_Code { get; set; }
            public float Head_Hole_Position { get; set; }
            public int Head_Leader_Length { get; set; }
            public float Head_Leader_Width { get; set; }
            public float Head_Leader_Thickness { get; set; }
            public float Tail_PunchHole_Position { get; set; }
            public int Tail_Leader_Length { get; set; }
            public float Tail_Leader_Width { get; set; }
            public float Tail_Leader_Thickness { get; set; }
            public int Scraped_Length_Entry { get; set; }
            public int Scraped_Length_Exit { get; set; }
            public string Head_Leader_St_No { get; set; }
            public string Tail_Leader_St_No { get; set; }
            public float Avg_Side_Trimmer_Gap { get; set; }
            public float Avg_Side_Trimmer_Lap { get; set; }
            public float Avg_Side_Trimmer_Width { get; set; }
            public float Avg_Trimming_OperateSide { get; set; }
            public float Avg_Trimming_DriveSide { get; set; }
            public string Winding_Direction { get; set; }
            public string Base_Surface { get; set; }
            public string Inspector { get; set; }
            public string Hold_Flag { get; set; }
            public string Hold_Cause_Code { get; set; }
            public string Sample_Flag { get; set; }
            public string Trim_Flag { get; set; }
            public string Fixed_WT_Flag { get; set; }
            public string End_Flag { get; set; }
            public string Scrap_Flag { get; set; }
            public string Sample_Frqn_Code { get; set; }
            public string No_Leader_Code { get; set; }
            public string Surface_Accuracy_Code { get; set; }
            public int Head_Off_Gauge { get; set; }
            public int Tail_Off_Gauge { get; set; }
            public string Surface_Accu_Code_In { get; set; }
            public string Surface_Accu_Code_Out { get; set; }
            public string Flip_Tag { get; set; }
            public string Process_Code { get; set; }
            public string Decoiler_Direction { get; set; }
            public float Recoiler_Actten_Avg { get; set; }
            public string Exit_Scaned_CoilID { get; set; }
            public string Exit_Scaned_UserID { get; set; }
            public DateTime Exit_Scaned_Time { get; set; }
            public string Exit_CoilID_Checked { get; set; }
            public string PDO_Uploaded_Flag { get; set; }
            public DateTime PDO_Uploaded_Time { get; set; }
            public string PDO_Uploaded_UserID { get; set; }
            public DateTime LabelPrint_Time { get; set; }
            public DateTime CoilWeight_Time { get; set; }
            public DateTime Exit_ExportTime { get; set; }

            // 尾段導帶體積
            public float TailStripVolume
            {
                get
                {
                    return Tail_Leader_Length * Tail_Leader_Width / 1000 * Tail_Leader_Thickness;
                }
            }
            //頭段導帶體積
            public float HeadStripVolume
            {
                get
                {
                    return Head_Leader_Length * Head_Leader_Width / 1000 * Head_Leader_Thickness;
                }
            }
        }

        /// <summary>
        /// 取樣鋼捲資料
        /// </summary>
        [Serializable]
        public class SampleCoil
        {
            // 作業計畫號
            public string Plan_No { get; set; }

            // 計畫內順序號
            public int Mat_Seq_No { get; set; }

            // 作業計畫種類
            public string Plan_Sort { get; set; }

            // 樣品號碼
            public string Sample_ID { get; set; }

            // 入口捲號
            public string Entry_Coil_ID { get; set; }

            // 合同號
            public string Order_No { get; set; }

            // 試批號
            public string Sample_Lot_No { get; set; }

            // 切邊標記
            public string Trim_Flag { get; set; }

            // 取樣位置
            // H = 頭部取樣 ; M = 中部取樣 ; T = 尾部取樣 
            public string Sample_Position { get; set; }

            // 內部鋼種
            public string St_No { get; set; }

            // 取樣長度
            public float Sample_Length { get; set; }

            // 寬度
            // L1沒給，要注意有經過圓盤剪，寬度會改變。
            public float Width { get; set; }

            // 厚度
            public float Thickness { get; set; }

            // 計算重量
            public float Calculate_Weight { get; set; }

            // 外徑
            public int Outer_Diameter { get; set; }

            // 0 = No used ; 1 = Used
            public string Paper_Unwinder { get; set; }

            // 班次
            // 1 - 夜 ; 2 - 早 ; 3 - 中 ;
            public string Shift { get; set; }

            // 班別
            // A - 甲 ; B - 乙 ; C - 丙 ; D - 丁 ;
            public string Team { get; set; }
        }

        /// <summary>
        /// 分切暫存紀錄
        /// </summary>
        [Serializable]
        public class CoilCutRecordTemp
        {
            /// <summary>
            /// 鋼卷號
            /// </summary>
            public string Coil_ID { get; set; } = "";

            /// <summary>
            /// 入口卷號
            /// </summary>
            public string In_Coil_ID { get; set; } = "";

            /// <summary>
            /// 原PDI給定出口捲
            /// </summary>
            public string OriPDI_Out_Coil_ID { get; set; } = "";

            /// <summary>
            /// 1-入口剪刀 2-出口剪刀
            /// </summary>
            public string CutDevice { get; set; } = "";

            /// <summary>
            /// 分切模式 1-出口剪WeldCut 2-出口剪SplitCut 3-出口剪ScrapCut 4-出口剪SampleCut 5-入口剪裁切鋼捲頭部 6-入口剪裁切鋼捲尾部
            /// </summary>
            public string CutMode { get; set; }

            /// <summary>
            /// 切割長度
            /// </summary>
            public float CutLength { get; set; } = 0f;

            /// <summary>
            /// 分切時間
            /// </summary>
            public DateTime CutTime { get; set; }

            /// <summary>
            /// 分切鋼卷外徑
            /// </summary>
            public string Coil_OutDiam { get; set; } = "";

            /// <summary>
            /// 分切鋼捲長度
            /// </summary>
            public string Coil_Length { get; set; } = "";

            /// <summary>
            /// 分切鋼卷理論重
            /// </summary>
            public float Coil_CalcWeight { get; set; } = 0;

            /// <summary>
            /// 拆紙機標記
            /// </summary>
            public string Coil_PaperFlag { get; set; } = "";

        }


        /// <summary>
        /// 排班表
        /// </summary>
        [Serializable]
        public class WorkSchedule
        {
            // 班次
            public int Shift { get; set; }
            // 班別
            public string Team { get; set; }
            // 班次日期
            public string ShiftDate { get; set; }
            // 排班模式
            public int Mode { get; set; }
            // 班次開始時間
            public string ShiftStartTime { get; set; }
            // 班次結束時間
            public string ShiftEndTime { get; set; }
            // 排班人
            public string ShiftPerson { get; set; }
        }


        /// <summary>
        /// 導帶相關資訊 
        /// </summary>
        [Serializable]
        public class LedaerData
        {
            public string Coil_ID { get; set; }
            public string OriPDI_Out_Coil_ID { get; set; }
            public string Head_Leader_St_No { get; set; }
            public int Head_Leader_Length { get; set; }
            public float Head_Leader_Width { get; set; }
            public float Head_Leader_Thickness { get; set; }
            public string Tail_Leader_St_No { get; set; }
            public int Tail_Leader_Length { get; set; }
            public float Tail_Leader_Width { get; set; }
            public float Tail_Leader_Thickness { get; set; }
            public string Create_UserID { get; set; }
        }

        [Serializable]
        public class LineFaultRecord
        {
            /// <summary>
            /// 機組號
            /// </summary>
            public string unit_code { get; set; }

            /// <summary>
            /// 日期
            /// </summary>
            public DateTime prod_time { get; set; }

            /// <summary>
            /// 停機開始時間
            /// </summary>
            public DateTime stop_start_time { get; set; }

            /// <summary>
            /// 停機結束時間
            /// </summary>
            public DateTime stop_end_time { get; set; }


            /// <summary>
            /// 班次
            /// </summary>
            public string prod_shift_no { get; set; }

            /// <summary>
            /// 班別
            /// </summary>
            public string prod_shift_group { get; set; }

            /// <summary>
            /// 停機位置
            /// </summary>
            public string delay_location { get; set; }

            /// <summary>
            /// 停機位置描述
            /// </summary>
            public string delay_location_desc { get; set; }

            /// <summary>
            /// 停機持續時間
            /// </summary>
            public string stop_elased_timey { get; set; }

            /// <summary>
            /// 停機類別
            /// </summary>
            public int stop_category { get; set; }

            /// <summary>
            /// 停機原因代碼
            /// </summary>
            public string delay_reason_code { get; set; }

            /// <summary>
            /// 停機原因描述
            /// </summary>
            public string delay_reason_desc { get; set; }

            /// <summary>
            /// 停機原因備註
            /// </summary>
            public string delay_remark { get; set; }

            /// <summary>
            /// 機械部門原因停機時間
            /// </summary>
            public string resp_depart_delay_time_m { get; set; }

            /// <summary>
            /// 電器部門原因停機時間
            /// </summary>
            public string resp_depart_delay_time_e { get; set; }

            /// <summary>
            /// L3原因停機時間
            /// </summary>
            public string resp_depart_delay_time_c { get; set; }

            /// <summary>
            /// 生產部門原因停機時間
            /// </summary>
            public string resp_depart_delay_time_p { get; set; }

            /// <summary>
            /// 正常停機時間
            /// </summary>
            public string resp_depart_delay_time_u { get; set; }

            /// <summary>
            /// 其他部門原因停機時間
            /// </summary>
            public string resp_depart_delay_time_o { get; set; }

            /// <summary>
            /// 換輥原因停機時間
            /// </summary>
            public string resp_depart_delay_time_r { get; set; }

            /// <summary>
            /// 磨輥間原因停機時間
            /// </summary>
            public string resp_depart_delay_time_rs { get; set; }

            /// <summary>
            /// 降速原因
            /// </summary>
            public string deceleration_cause { get; set; }

            /// <summary>
            /// 降速代码
            /// </summary>
            public string deceleration_code { get; set; }

            public string UploadMMS { get; set; }
        }
    }
}
