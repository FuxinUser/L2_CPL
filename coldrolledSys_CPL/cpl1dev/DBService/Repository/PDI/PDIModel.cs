using Core.Define;
using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.PDI
{
    public class CoilPDIEntity
    {
        public class TBL_PDI : BaseRepositoryModel
        {
            // 作業計畫號
            [PrimaryKey]
            public string Plan_No { get; set; }
            // 計畫內順序號
            public int Mat_Seq_No { get; set; }
            // 作業計畫種類
            public string Plan_Sort { get; set; }
            // 入口卷號
            [PrimaryKey]
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
            public float Head_Paper_Length { get; set; }
            // 入口頭部墊紙寬度
            public float Head_Paper_Width { get; set; }
            // 入口尾部墊紙長度
            public float Tail_Paper_Length { get; set; }
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
            // 導帶使用方式 	0:不使用  1：使用
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
            [IgnoreReflction]
            public string Head_Strip_St_No { get; set; }
            // 頭段導帶長度 - 待刪除
            [IgnoreReflction]
            public int Head_Strip_Length { get; set; }
            // 頭段導帶寬度 - 待刪除
            [IgnoreReflction]
            public int Head_Strip_Width { get; set; }
            // 頭段導帶厚度 - 待刪除
            [IgnoreReflction]
            public float Head_Strip_Thickness { get; set; }
            // 尾段導帶鋼種 - 待刪除
            [IgnoreReflction]
            public string Tail_Strip_St_No { get; set; }
            // 尾段導帶長度 - 待刪除
            [IgnoreReflction]
            public int Tail_Strip_Length { get; set; }
            // 尾段導帶寬度 - 待刪除
            [IgnoreReflction]
            public int Tail_Strip_Width { get; set; }
            // 尾段導帶厚度 - 待刪除
            [IgnoreReflction]
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
            // 建立時間
            public override DateTime CreateTime { get; set; }

            [IgnoreReflction]
            public int EntryYieldStress
            {
                get
                {
                    return (Ts_Stand_Max + Ts_Stand_Min) / 2;
                }
            }
            // 尾段導帶體積
            [IgnoreReflction]
            public float TailStripVolume
            {
                get
                {
                    return Tail_Strip_Length * Tail_Strip_Width * Tail_Strip_Thickness;
                }
            }
            //頭段導帶體積
            [IgnoreReflction]
            public float HeadStripVolume
            {
                get
                {
                    return Head_Strip_Length * Head_Strip_Width * Head_Strip_Thickness;
                }
            }

            // 是否切割
            [IgnoreReflction]
            public bool IsDivid
            {
                get
                {
                    return Dividing_Flag.Equals(DBParaDef.Cut);
                }
            }

            // 取樣位置數量
            [IgnoreReflction]
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
            [IgnoreReflction]
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

    }

}
