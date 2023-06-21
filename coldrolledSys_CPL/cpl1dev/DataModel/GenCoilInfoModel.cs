namespace DataMod.PLC
{

    /**
     Author : ICSC 余士鵬
     Desc : Gen Preset && PDO Msg使用資料模組  
     */
    public class GenCoilInfoModel
    {
        public class GenPreset201LkTableInfo 
        {
            public float FlatenerDepth1 { get; set; } = 0.0f;   // TBL_Lookup_Flattener -> Intermesh_Num1
            public float FlatenerDepth2 { get; set; } = 0.0f;   // TBL_Lookup_Flattener -> Intermesh_Num2
            public float UncoilerTension { get; set; } = 0.0f;  // TBL_Lookup_LineTension -> PORTension 單位張力
            public float SideTrimmerGap { get; set; } = 0.0f;   // TBL_Lookup_SideTrimmer -> KnifeGap 
            public float SideTrimmerLap { get; set; } = 0.0f;   // TBL_Lookup_SideTrimmer -> KnifeLap

            /** PDI TRIM_FLAG 切边标记  0：不切 1：切
                    IN_MAT_WIDTH 入口材料宽度 - OUT_MAT_WIDTH 出口材料宽度*/
            public float SideTrimmerWidth { get; set; } = 0.0f; // TBL_Lookup_SideTrimmer -> 鋼捲目標寬度-鋼捲實際寬度       
            public float Tensionunitdepth { get; set; } = 0.0f; // TBL_Lookup_LineTension -> UnitTension
            public float RecoilerTension { get; set; } = 0.0f;  //TBL_Lookup_LineTension  -> TRTension 單位張力
        }

        public class GenPDODataPara
        {
            // 頭部切廢長度
            public int ScrapedLengthEntry { get; set; } = 0;
            // 尾部切廢長度
            public int ScrapedLengthExit { get; set; } = 0;
            // 是否有焊接
            public string NoLeaderCode { get; set; } = "";
            // 墊紙重量
            public int PaperWt { get; set; } = 0;
            // 套桶重量
            public int SleeveWt { get; set; } = 0;
            // 頭段導帶重量
            public float HeadLeaderWt { get; set; } = 0;
            // 尾段導帶重量
            public float TailLeaderWt { get; set; } = 0;

            // 班次
            public int Shift { get; set; } = 0;
            // 班別
            public string Team { get; set; } = string.Empty;

            // 導帶資訊           
            public string Head_Leader_St_No { get; set; } = string.Empty;
            public float Head_Leader_Length { get; set; } = 0;
            public float Head_Leader_Width { get; set; } = 0;
            public float Head_Leader_Thickness { get; set; } = 0;

            public string Tail_Leader_St_No { get; set; } = string.Empty;
            public float Tail_Leader_Length { get; set; } = 0;
            public float Tail_Leader_Width { get; set; } = 0;
            public float Tail_Leader_Thickness { get; set; } = 0f;

            // 墊紙重量 + 套桶重量 + 頭段導帶重量 + 尾段導帶重量
            public int TotalWt
            {
                get
                {
                    return PaperWt + SleeveWt + (int)HeadLeaderWt + (int)TailLeaderWt;
                }
            }

        }

    }
}
