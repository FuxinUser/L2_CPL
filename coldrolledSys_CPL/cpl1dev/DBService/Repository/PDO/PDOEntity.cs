using Core.Define;
using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.PDO
{
    public class PDOEntity
    {
        public class TBL_PDO : BaseRepositoryModel
        {
            /// <summary>
            /// 合同號
            /// </summary>
            public string OrderNo { get; set; } 
            /// <summary>
            /// 計畫號
            /// </summary>
            [PrimaryKey]
            public string Plan_No { get; set; }

            /// <summary>
            /// 出口卷號
            /// </summary>
            [PrimaryKey]
            public string Out_Coil_ID { get; set; }

            /// <summary>
            /// 入口卷號
            /// </summary>
            public string In_Coil_ID { get; set; }

            /// <summary>
            /// 原PDI的出口鋼卷號
            /// </summary>
            public string OriPDI_Out_Coil_ID { get; set; }

            /// <summary>
            /// 生產開始時間
            /// </summary>
            public DateTime StartTime { get; set; }

            /// <summary>
            /// 生產結束時間
            /// </summary>
            public DateTime FinishTime { get; set; }

            /// <summary>
            /// 班次
            /// </summary>
            public string Shift { get; set; }

            /// <summary>
            /// 班別
            /// </summary>
            public string Team { get; set; }

            /// <summary>
            /// 出口卷外徑
            /// </summary>
            public float Out_Coil_Outer_Diameter { get; set; }

            /// <summary>
            /// 出口卷內徑
            /// </summary>
            public float Out_Coil_Inner { get; set; }

            /// <summary>
            /// 出口卷理論重
            /// </summary>
            public float Out_Theory_Wt { get; set; }

            /// <summary>
            /// 出口卷淨重
            /// </summary>
            public int Out_Coil_Wt { get; set; }

            /// <summary>
            /// 出口卷毛重
            /// </summary>
            public float Out_Coil_Gross_WT { get; set; }

            /// <summary>
            /// 出口卷厚度
            /// </summary>
            public float Out_Coil_Thick { get; set; }

            /// <summary>
            /// 出口卷寬度
            /// </summary>
            public float Out_Coil_Width { get; set; }

            /// <summary>
            /// 出口卷長度
            /// </summary>
            public float Out_Coil_Length { get; set; }

            /// <summary>
            /// 入口剪裁切鋼捲頭部的長度
            /// </summary>
            public float Header_Cut_Length_Entry { get; set; }

            /// <summary>
            /// 入口剪裁切鋼捲尾部的長度
            /// </summary>
            public float Tail_Cut_Length_Entry { get; set; }

            /// <summary>
            /// 出口剪裁切鋼捲尾部的長度
            /// </summary>
            public float Header_Cut_Length_Exit { get; set; }

            /// <summary>
            /// 出口剪裁切鋼捲尾部的長度
            /// </summary>
            public float Tail_Cut_Length_Exit { get; set; }

            /// <summary>
            /// 出口墊紙種類
            /// </summary>
            public string Paper_Code { get; set; }

            /// <summary>
            /// 出口墊紙方式
            /// </summary>
            public string Paper_Req_Code { get; set; }

            /// <summary>
            /// 出口頭部墊紙長度
            /// </summary>
            public int Out_Head_Paper_Length { get; set; }

            /// <summary>
            /// 出口頭部墊紙寬度
            /// </summary>
            public float Out_Head_Paper_Width { get; set; }

            /// <summary>
            /// 出口尾部墊紙長度
            /// </summary>
            public int Out_Tail_Paper_Length { get; set; }

            /// <summary>
            /// 出口尾部墊紙寬度
            /// </summary>
            public float Out_Tail_Paper_Width { get; set; }

            /// <summary>
            /// 出口套筒內徑
            /// </summary>
            public int Sleeve_Inner_Exit_Diamter { get; set; }

            /// <summary>
            /// 出口套筒類型
            /// </summary>
            public string Sleeve_Type_Exit_Code { get; set; }

            /// <summary>
            /// 頭部打孔位置
            /// </summary>
            public float Head_Hole_Position { get; set; }

            /// <summary>
            /// 頭部導帶長度
            /// </summary>
            public float Head_Leader_Length { get; set; }

            /// <summary>
            /// 頭部導帶寬度
            /// </summary>
            public float Head_Leader_Width { get; set; }

            /// <summary>
            /// 頭部導帶厚度
            /// </summary>
            public float Head_Leader_Thickness { get; set; }

            /// <summary>
            /// 尾部打孔位置
            /// </summary>
            public float Tail_PunchHole_Position { get; set; }

            /// <summary>
            /// 尾部導帶長度
            /// </summary>
            public float Tail_Leader_Length { get; set; }

            /// <summary>
            /// 尾部導帶寬度
            /// </summary>
            public float Tail_Leader_Width { get; set; }

            /// <summary>
            /// 尾部導帶厚度
            /// </summary>
            public float Tail_Leader_Thickness { get; set; }

            /// <summary>
            /// 頭部切廢長度
            /// </summary>
            public int Scraped_Length_Entry { get; set; }

            /// <summary>
            /// 尾部切廢長度
            /// </summary>
            public int Scraped_Length_Exit { get; set; }

            /// <summary>
            /// 頭部導帶鋼種
            /// </summary>
            public string Head_Leader_St_No { get; set; }

            /// <summary>
            /// 尾部導帶鋼種
            /// </summary>
            public string Tail_Leader_St_No { get; set; }


            /// <summary>
            /// 圓盤剪Gap平均值	單位：mm。9.99。
            /// </summary>
            public float Avg_Side_Trimmer_Gap { get; set; }

            /// <summary>
            /// 圓盤剪Lap平均值	單位：mm。-9.99。
            /// </summary>
            public float Avg_Side_Trimmer_Lap { get; set; }

            /// <summary>
            /// 圓盤剪寬度平均值	單位：mm。9999.9。
            /// </summary>
            public float Avg_Side_Trimmer_Width { get; set; }

            /// <summary>
            /// 裁邊量平均值_操作側	單位：mm。99.9。
            /// </summary>
            public float Avg_Trimming_OperateSide { get; set; }

            /// <summary>
            /// 裁邊量平均值_Drive側	單位：mm。99.9。
            /// </summary>
            public float Avg_Trimming_DriveSide { get; set; }


            /// <summary>
            /// 卷曲方向
            /// </summary>
            public string Winding_Direction { get; set; }

            /// <summary>
            /// 好面朝向
            /// </summary>
            public string Base_Surface { get; set; }

            /// <summary>
            /// 封鎖責任者
            /// </summary>
            public string Inspector { get; set; }

            /// <summary>
            /// 封鎖標記
            /// </summary>
            public string Hold_Flag { get; set; }

            /// <summary>
            /// 封鎖原因代碼
            /// </summary>
            public string Hold_Cause_Code { get; set; }

            /// <summary>
            /// 取樣標記
            /// </summary>
            public string Sample_Flag { get; set; }

            /// <summary>
            /// 切邊標記
            /// </summary>
            public string Trim_Flag { get; set; }

            /// <summary>
            /// 分卷標記
            /// </summary>
            public string Fixed_WT_Flag { get; set; }

            /// <summary>
            /// 最終卷標記
            /// </summary>
            public string End_Flag { get; set; }

            /// <summary>
            /// 廢品標記
            /// </summary>
            public string Scrap_Flag { get; set; }

            /// <summary>
            /// 取樣位置
            /// </summary>
            public string Sample_Frqn_Code { get; set; }

            /// <summary>
            /// 未焊引帶代碼
            /// </summary>
            public string No_Leader_Code { get; set; }

            /// <summary>
            /// 表面精度代碼
            /// </summary>
            public string Surface_Accuracy_Code { get; set; }

            /// <summary>
            /// 頭部未軋製區域
            /// </summary>
            public int Head_Off_Gauge { get; set; }

            /// <summary>
            /// 尾部未軋製區域
            /// </summary>
            public int Tail_Off_Gauge { get; set; }

            /// <summary>
            /// 內表面精度代碼
            /// </summary>
            public string Surface_Accu_Code_In { get; set; }

            /// <summary>
            /// 外表面精度代碼
            /// </summary>
            public string Surface_Accu_Code_Out { get; set; }

            /// <summary>
            /// 翻面標記
            /// </summary>
            public string Flip_Tag { get; set; }

            /// <summary>
            /// 工序代碼
            /// </summary>
            public string Process_Code { get; set; }
            public string Decoiler_Direction { get; set; }
            public float Recoiler_Actten_Avg { get; set; }

            /// <summary>
            /// 出口掃描鋼卷號
            /// </summary>
            public string Exit_Scaned_CoilID { get; set; }

            /// <summary>
            /// 出口掃描人員
            /// </summary>
            public string Exit_Scaned_UserID { get; set; }

            /// <summary>
            /// 出口掃描時間
            /// </summary>
            public DateTime Exit_Scaned_Time { get; set; }

            /// <summary>
            /// 出口掃描標記
            /// </summary>
            public string Exit_CoilID_Checked { get; set; }
            public override DateTime CreateTime { get; set; }

            /// <summary>
            /// PDO上傳註記
            /// </summary>
            public string PDO_Uploaded_Flag { get; set; }

            /// <summary>
            /// PDO上傳時間
            /// </summary>
            public DateTime PDO_Uploaded_Time { get; set; }

            /// <summary>
            /// PDO上傳人員
            /// </summary>
            public string PDO_Uploaded_UserID { get; set; }

            /// <summary>
            /// 標籤列印時間
            /// </summary>
            public DateTime LabelPrint_Time { get; set; }

            /// <summary>
            /// 鋼卷秤重時間
            /// </summary>
            public DateTime CoilWeight_Time { get; set; }

            /// <summary>
            /// 鋼卷出口時間
            /// </summary>
            public DateTime Exit_ExportTime { get; set; }

            // 尾段導帶體積
            [IgnoreReflction]
            public float TailStripVolume
            {
                get
                {
                    return Tail_Leader_Length * Tail_Leader_Width / 1000 * Tail_Leader_Thickness / 1000;
                }
            }
            //頭段導帶體積
            [IgnoreReflction]
            public float HeadStripVolume
            {
                get
                {
                    return Head_Leader_Length * Head_Leader_Width / 1000 * Head_Leader_Thickness / 1000;
                }
            }

     
        }
    }
}
