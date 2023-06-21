namespace Core.Define
{
    /// <summary>
    /// 報文識別ID定義
    /// </summary>
    public static class MsgIDDef
    {
        #region -- MMS --
        public const string L2 = "L2";
        public const string MMS = "MM";

        public const string MMSDataMsg = "D";
        public const string MMSHeartMsg = "C";

        // MMS 下發命令編號
        public const string MMSCoilSchedule = "MMP101";             // 鋼捲生產時機
        public const string MMSCoilPDI = "MMP102";                  // PDI
        public const string MMSReqProResult = "MMP106";             // 生产实绩请求
        public const string MMSCoilRejectResult = "MMP107";         // 钢卷删除/回退实绩应答
        public const string MMSReqDeletePlanNo = "MMP103";          // 作業計畫刪除請求


        // MMS 接收命令編號
        public const string MMSCoilScheduleChanged = "P1MM10";      // 鋼捲生產時機
        public const string MMSCoilRejectData = "P1MM05";           // 鋼捲回退實績
        public const string MMSCoilLoadedSkid = "P1MM06";           // 鋼捲上安座通知
        public const string MMSCoilPDO = "P1MM07";                  // 
        public const string MMSResForCoilSched = "P1MM03";          // 钢卷生产命令应答
        public const string MMSResForCoilPDI = "P1MM04";            // 钢卷PDI应答
        public const string MMSReqForCoilSched = "P1MM01";          // 钢卷生产命令请求
        public const string MMSResCoilRejectResult = "P1MM07";      // 鋼捲生產刪除回應
        public const string MMSReqForPDI = "P1MM02";                // 鋼捲PDI請求

        public const string MMSResDeletePlanNoResult = "P1MM25";    // 回覆整計畫刪除電文
        public const string MMSEnergyConsumptionInfo = "P1MM09";    // 能源消耗訊息    
        public const string MMSCoilScheduleDelete = "P1MM18";       // 鋼捲生產命令刪除

        public const string MMSProOK = "0";
        public const string MMSProNG = "1";
        #endregion
        #region -- WMS --
        // WMS 命令編號 產線->WMS
        public const string WMSCoilScheduleInfo = "PW11";           // 接收排程資訊

        public const string WMSEntryDeliveryTrk = "PW12";           // 產線入口/出口Tracking
        public const string WMSCoilData = "PW13";                   // 鋼捲產出資訊

        public const string WMSProdLineCoilReq = "PW15";            // 產線入料/出料/退料要求
        public const string WMSProdLineCoilCancel = "PW14";         // 產線入料/出料/退料取消
        // WMS 命令編號 WMS->產線
        public const string WMSCoilProDone = "WP11";                // 入料/出料/退料完成訊息
        #endregion
        #region -- L1 --
        // Preset position 1: Uncoiler 2: UncSK1 3: UncSK2 4: UncTOP
        public const int TOP = 4;
        public const int SK2 = 3;
        public const int SK1 = 2;
        public const int POR  = 1;


        // L1命令編號 產線->L1
        public const string L1201Preset = "201";
        public const string L1202TrackMapL2 = "202";
        public const string L1203SplitID = "203";
        public const string L1204DelSkID = "204";
        public const string L2299Alive = "299";

        // L1命令編號 L1->產線
        public const string L1301EnCoilCut = "301";                 // Length of coil be cut on Uncoiler
        public const string L1302WieldRecord = "302";               // Send Welder related data and Model calculation data to L2, When strip has been welded into coil.
        public const string L1303ReqTrackMap = "303";               // Used to inform that the L1 has started up and would like to get a complete tracking map from L2.
        public const string L1305TrackMapEn = "305";                // Entry tracking information。Cyclic -5sec 入口段通知
        public const string L1306TrackMapEx = "306";                // Exit tracking information, send from L1. Cyclic -5sec 出口段通知
        public const string L1307CoilDismount = "307";              // Calculate Data(length,weight,diameter) sent by level 1 at coil dismount from recoiler
        public const string L1308CoilWeightScale = "308";           // Actual measured weight by weighing scale
        public const string L1309EquipMaint = "309";                // Equipment data
        public const string L1310LineFault = "310";                 // Line Fault message
        public const string L1311ExCoilCut = "311";                 // Exit cut and length of cut away material from the produced coil.
        public const string L1312NewCoilRec = "312";                // Indicates a new coil on recoiler is loaded. 
        public const string L1313SpdTen = "313";                    // Tension and speed data
        public const string L1315Cdc = "315";                       // Coil display codes for L2 to display similar L1 display weld tracking
        public const string L1316Utility = "316";                   // Utility Data (水電氣資料)
        public const string L1399Alive = "399";                     // Check the connection status in L2. 


        #endregion
    }
}
