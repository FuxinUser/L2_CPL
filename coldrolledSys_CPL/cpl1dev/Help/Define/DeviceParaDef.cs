namespace Core.Define
{
    /// <summary>
    /// 設備與產線參數相關定義
    /// </summary>
    public class DeviceParaDef
    {
        // 出入口捲設備
        public const string EntryShear = "1";   // 入口剪設備
        public const string ExitShear = "2";    // 出口剪設備
        public const int HeaderCut = 1;
        public const int TailCut = 2;

        // 開捲機張力 Max Min定義
        public const float UncoilerTensionMax = 1.0f;
        public const float UncoilerTensionMin = 1.0f;

        // 收卷機張力 Max Min定義
        public const float RecoilerTensionMax = 1.0f;
        public const float RecoilerTensionMin = 1.0f;

        // CPL: 600mm GPL : 600mm
        public const float HeadHolePosition = 600f;
        public const float TailPunchHolePosition = 600f;

        // 下開
        public const string WindingDirectionDown = "L";
        // 上開
        public const string WindingDirectionUp = "U";

        // BarCode機
        public const string BCSScanCoil = "BS01";           // BarCode機 傳送鋼捲身分識別要求
        public const string CompareScanResultId = "SB01";
        public const string ScanResultLength = "37";        // BCSScnResult Model最大長度

        // BarCode機 位置定義 :  pos 1:ESK01 2:ESK02 3:ETOP 4:DSK01 5:DSK02 6:DTOP
        public const string BCSDefPOS_ESK01 = "1";
        public const string BCSDefPOS_ESK02 = "2";
        public const string BCSDefPOS_ETOP = "3";
        public const string BCSDefPOS_DSK01 = "4";
        public const string BCSDefPOS_DSK02 = "5";
        public const string BCSDefPOS_DTOP = "6";
    }
}
