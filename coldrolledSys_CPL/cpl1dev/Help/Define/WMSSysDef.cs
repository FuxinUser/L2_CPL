using Core.Help;

namespace Core.Define
{
    /// <summary>
    /// 外部系統 WMS 相關定義
    /// </summary>
    public static class WMSSysDef
    {
        public static string CPLSysNumber = IniSystemHelper.Instance.LineNo;
        public const string SysName = "WMS";
        public const string WMS = "WM";

        public class DataCode
        {

            public const string WMSErrorMsgID = "AA00";
            public const string WMSErrorMsgLength = "AA01";
        }

        // L2->WMS
        public class SndMsgCode
        {
            public static string WMSCoilProDone = "WP"+ CPLSysNumber + "1";                // 入料/出料/退料完成訊息
            public static string WMSCoilProResRequest = "WP"+ CPLSysNumber+"3";          // 入料/出料/退料要求回覆
        }

        // WMS->L2
        public class RcvMsgCode
        {
            public const string WMSHeartBeat = "TT01";
            public const string WMSAck = "DL00";
            public const string HeartBeatCode = "TT01";                // 心跳Code
            public static string WMSCoilScheduleInfo = "PW"+ CPLSysNumber+"1";           // 接收排程資訊
            public static string WMSEntryDeliveryTrk = "PW"+ CPLSysNumber+"2";           // 產線入口/出口Tracking
            public static string WMSCoilPDO = "PW"+ CPLSysNumber+"3";                    // 鋼捲產出資訊
            public static string WMSInfoScanID = "PW"+ CPLSysNumber+"6";                 // 掃描ID通知
            public static string WMSProdLineCoilReq = "PW"+ CPLSysNumber+"5";            // 產線入料/出料/退料要求
            public static string WMSProdLineCoilCancel = "PW"+ CPLSysNumber+"4";         // 產線入料/出料/退料取消

        }

        public class Cmd
        {
            public const string WMSInfoNo = "0";
            public const string WMSInfoYes = "1";

            public const string WMSWindingDirectionUp = "0";
            public const string WMSWindingDirectionDown = "1";
        }

        public class SkPos
        {
        
            public const int ESk03 = 2;
            public const int ESk02 = 3;
            public const int ETop = 4;

            public const int DSk03 = 6;
            public const int DSk02 = 7;
            public const int DTop = 8;

            // WMS WG02 Entry Exit Def
            public const string Wx02TrkNoUse = "0";
            public const string Wx02TrkEntry = "1";
            public const string Wx02TrkDelivery = "2";
            public const string Wx02TrkEntryDelivery = "3";
        }
    }
}
