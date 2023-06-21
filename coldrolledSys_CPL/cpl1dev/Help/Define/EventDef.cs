namespace Core.Define
{
    /// <summary>
    ///  L2系統處理事件定義
    /// </summary>
    public static class EventDef
    {        
        public enum TCPCMDSET
        {
            CLIENT_TRY_CONNECTION
        }

        public enum CMDSET
        {
            ACK_TIMEOUT,
            SND_HEARTBEAT,
            CHECK_ACK,
            DETECT_L1_ALIVE,
            DEBUG_LOG_CLOSE,
            DEBUG_LOG_OPEN,
            HEART_BEAT_OPEN,
            HEART_BEAT_CLOSE,
            ACK_OPEN,
            ACK_CLOSE,
            TRKSCAN, 
            TRKINFO,
            DETECT_SND_QUEU,
            SERVER_ALIVE,
            L25Alive,
        }      
        public enum LOGSW
        {
            DEBUG_LOG_CLOSE,
            DEBUG_LOG_OPEN,
        }

        public const string ProOK = "0";
        public const string ProError = "1";
        public const string ProSchedFail = "Process coil schedule fail";
        public const string NoSchedule = "No shcedule ID";

        // 掃描 : 鋼捲ID Check Uncheck
        public const string CheckCoilNo = "1";
        public const string UnCheckedCoilNo = "0";


        // Server Event 
        public const string ReceiveMMSPDI = "接收三級PDI資料";
        public const string ReceiveWMSCancelMsg = "接收WMS回復";
        public const string InfoWMSCancelCoilMsg = "發送取消入料給WMS";
        public const string InfoWMSReqInCoilMsg = "發送要求入料給WMS";
        public const string InfoWMSReqOutCoilMsg = "發送要求出料給WMS";
        public const string InfoWMSReqRejCoilMsg = "發送要求退料給WMS";
        public const string InfoWMSSndProductCoilMsg = "發送產出資訊給WMS";
        public const string SystemAutoFitChange = "更改入料狀態 OK";
        public const string ReceiveMMSCoilSchedule = "收到鋼卷生產命令";
        public const string MMSNoPDI = "MMS通知無鋼捲PDI回應";
        public const string MMSNoCoil = "MMS通知無鋼捲生產回應";
        public const string DeliveryCoilDone = "出料完成";
        public const string EntryCoilDone = "入料完成";
        public const string RejectCoilDone = "退料完成";
        public const string CoilRejectFail = "失敗";
        public const string CoilRejectSuccess = "成功";
        public const string L1DisConn = "L1斷線";
    }
}
