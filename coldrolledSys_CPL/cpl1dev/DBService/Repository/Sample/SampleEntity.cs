using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.Sample
{
    public class SampleEntity
    {
        public class TBL_Sample : BaseRepositoryModel
        {
            // 作業計畫號
            [PrimaryKey]
            public string Plan_No { get; set; }

            // 計畫內順序號
            [PrimaryKey]
            public int Mat_Seq_No { get; set; }

            // 作業計畫種類
            [PrimaryKey]
            public string Plan_Sort { get; set; }

            // 樣品號碼
            [PrimaryKey]
            public string Sample_ID { get; set; }

            // 入口捲號
            public string Entry_Coil_ID { get; set; }

            // 原出口卷號
            public string OriPDI_Out_Coil_ID { get; set; }

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
            public float Outer_Diameter { get; set; }

            // 0 = No used ; 1 = Used
            public string Paper_Unwinder { get; set; }

            // 班次
            // 1 - 夜 ; 2 - 早 ; 3 - 中 ;
            public string Shift { get; set; }

            // 班別
            // A - 甲 ; B - 乙 ; C - 丙 ; D - 丁 ;
            public string Team { get; set; }

            // 取樣時間
            public DateTime SampleTime { get; set; } = DateTime.Now;
        }

    }
}
