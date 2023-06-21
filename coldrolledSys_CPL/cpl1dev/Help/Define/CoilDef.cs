namespace Core.Define
{
    /// <summary>
    /// 鋼捲相關邏輯定義
    /// </summary>
    public class CoilDef
    {
        public const int ColdRolledCoilLength = 14;
        public const int HotRolledCoilLength = 12;

        # region MMS 
        
        // MMS:報文每筆鋼捲長度定義
        public const int UnitCoilIDMsgCharLen = 20;
        // MMS:鋼捲生產命令-插入所有入口鋼捲至CoilScheduleTable)
        public const string InsertAllCoil = "0";
        // MMS: 鋼捲排程Source
        public const string UpdateSourceMMS = "0";
        public const string UpdateSourceL2 = "1";

        // MMS: Process Code 工序代碼
        public const string ProcessCode_Leader = "CP01";
        public const string ProcessCode_Trimming =  "CP02";
        public const string ProcessCode_LeaderTrimming = "CP03";
        public const string ProcessCode_Recoiling = "CP04";
        public const string ProcessCode_ReInspection = "CP05";
        public const string ProcessCode_Cut = "CP06";
        public const string ProcessCode_CutTrimming = "CP07";

        #endregion

        #region WMS

        // WMS:WPX1 入料/出料/退料完成旗標
        public const string EntryCoil = "1";      //入料
        public const string DeliveryCoil = "2";   //出料
        public const string RejectCoil = "3";     //退料
        // WMS: WPX5  要求入料/出料/退料 入料/出料/退料
        public const string ReqWMSEntryCoil = "1";           //入料
        public const string ReqWMSDeliveryCoil = "2";       //出料
        public const string ReqWMSRejectCoil = "3";         //退料

        #endregion

        #region L1

        /* L1 : Cut Mode 
               Bit 0 斷帶 (判斷成捲不成捲做處理)
               Bit 1 Split cut (not use)
               Bit 2 Sample cut (not use)
               Bit 3 Head cut (not use)
               Bit 4 Tail cut (not use)
               Bit 5 Weld cut 
               Bit 6 Scrap cut
               Bit 7 虛擬切割                    
                   00000001 : 1    斷帶
                   00000010 : 2    Split Cut
                   00000100 : 4    Sample Cut
                   10000000 : 128  虛擬切割
           */
        public const int CutModeStripBrake = 1;     //斷帶 Bit0
        public const int CutModeSplitCut = 2;       //分切 Bit1
        public const int CutSampleCut = 4;          //取樣 Bit2
        public const int CutHeadScrapCut = 72;       //頭廢切
        public const int CutTailScrapCut = 80;       //尾廢切
        public const int CutModeVirtualCut = 128;   //虛擬切割 Bit7
        public const string CutModeReturnCoil = "C"; 





        #endregion


        // 鋼捲參數定義 : Leader Density
        //public const float LeaderDensity = 0.34f;
        //public const float LeaderDensity = 0.34f;
        //public const float LeaderDensity = 7930;    // 編號SUS304 單位 kg/m^3  
        public const float LeaderDensity = 7.930f;    // 編號SUS304 單位 kg/m^3  

        // 鋼卷成卷重量定義
        public const float ProductCoilWeight = 500f;


        // 鋼卷狀態  N-新鋼捲  R-要求入料  F-已入料  I-身分確認成功  P-生產中  D-已產出 C-回退
        public const string NewCoil_Statuts = "N";
        public const string RequestEntryCoil_Statuts = "R";
        public const string EntryCoilDone_Statuts = "F";
        public const string IdentifyOK_Statuts = "I";
        public const string Producing_Statuts = "P";
        public const string ReturnCoil_Statuts = "C";
        public const string ProduceDone_Statuts = "D";
        public const short ScheduleDoneSeqDef = -1;

        // 鋼捲回退與刪除資料類型
        public const string ScheduleDelete = "S";
        public const string ScheduleReject = "C";

        // 預取排程總數
        public const int DefaultGetScheduleCnts = 40;



        // 最終捲
        public const string FinalCoil = "1";
        public const string NotFinalCoil = "0";

        // 墊紙
        public class Paper
        {
            public const string NoPaper = "0";       // 不使用
            public const string PaperFullPad = "1";  // 整卷墊
            public const string HeadTail50Pad = "2"; // 頭尾端各50米墊
            public const string HeadTail30Pad = "3"; // 頭尾端各30米墊
            public const string Tail30Pad = "4";     // 尾端各80米墊
        }

    }
}
