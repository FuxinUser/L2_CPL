using Core.Help;

namespace Core.Define
{
    /// <summary>
    /// 外部系統 MMS 相關定義
    /// </summary>
    public static class MMSSysDef
    {
        public static string CPLSysNumber = IniSystemHelper.Instance.LineNo;
        public const string SysName = "MMS";
        public const string MMS = "MM";

        public const byte MMSEndTag = 0x0a;     // 結尾符
        public const int EndTagLength = 1;      // 結尾符長度 1byte
        public const int MsgLenLength = 4;      // "報文長度" 此欄位大小 4byte

        // MMS 定義Data Code
        public class DataCode
        {
            public const string DataMsg = "D";  // 表資料報文
            public const string HeartMsg = "C"; // 表心跳報文

            public const string Accept = "A";    // Ack OK
            public const string NotAccept = "B"; // Ack Fail
         }

        // MMS->L2
        public class RcvMsgCode
        {
            public static string HeartBeatCode = "999999";                            // 心跳Code
            public static string CoilSchedule = "MMP"+ CPLSysNumber+"01";             // 鋼捲生產時機
            public static string CoilPDI = "MMP"+ CPLSysNumber+"02";                  // PDI
            public static string ReqDeletePlanNo = "MMP"+ CPLSysNumber+"03";          // 作業計畫刪除請求
            public static string ReqProResult = "MMP"+ CPLSysNumber+"06";             // 生产实绩请求
            public static string CoilRejectResult = "MMP"+ CPLSysNumber+"07";         // 钢卷删除/回退实绩应答         
            public static string ResForNoCoil = "MMP"+ CPLSysNumber+"08";             // 無鋼捲生產命令回覆
            public static string ResForNoCoilPDI = "MMP"+ CPLSysNumber+"09";          // 無鋼捲PDI回覆
            public static string PdoUploadedReply = "MMP" + CPLSysNumber + "10";      // 上傳PDO的回覆
            public static string SleeveValueSyn = "MMP"+ CPLSysNumber+"15";           // 套筒静态数据同步
            public static string PaperValueSync = "MMP"+ CPLSysNumber+"16";           // 垫纸静态数据同步
        }

        // L2->MMS
        public class SndMsgCode
        {
           
          
            public static string ReqForCoilSched = "P"+ CPLSysNumber + "MM01";             // 钢卷生产命令请求
            public static string ReqForPDI = "P"+ CPLSysNumber + "MM02";                   // 鋼捲PDI請求
            public static string ResForCoilSched = "P"+ CPLSysNumber + "MM03";             // 钢卷生产命令应答
            public static string ResForCoilPDI = "P"+ CPLSysNumber + "MM04";               // 钢卷PDI应答
            public static string CoilRejectData = "P"+ CPLSysNumber + "MM05";              // 鋼捲回退實績
            public static string CoilLoadedSkid = "P"+ CPLSysNumber + "MM06";              // 鋼捲上安座通知
            public static string CoilPDO = "P"+ CPLSysNumber + "MM07";                     // PDO上傳
            public static string EqDownResultCode = "P"+ CPLSysNumber + "MM08";            // 停机实绩
            public static string MMSEnergyConsumptionInfo = "P"+ CPLSysNumber + "MM09";    // 能源消耗訊息  
            public static string CoilScheduleChanged = "P"+ CPLSysNumber + "MM10";         // 鋼捲生產時機

            public static string MMSCoilScheduleDelete = "P"+ CPLSysNumber + "MM18";       // 鋼捲生產命令刪除
            public static string MMSResDeletePlanNoResult = "P"+ CPLSysNumber + "MM25";    // 回覆整計畫刪除電文          

        }

        public class SndMsg
        {
            public static string CoilScheduleChanged = "鋼捲排程調整通知";
            public static string CoilLoadedSkid = "傳送鋼捲上鞍做通知";
            public static string CoilRejectData = "鋼捲回退實績通知";
            public static string CoilPDO = "MMS PDO上傳";
            public static string ResForCoilSched = "回覆接收排程電文";
            public static string ResForCoilPDI = "回覆PDI處理結果";
            public static string ReqForCoilSched = "要求生產排程";
            public static string ReqForPDI = "鋼捲PDI請求";
            public static string MMSResDeletePlanNoResult = "回覆整計畫刪除電文";
            public static string MMSEnergyConsumptionInfo = "上傳能源消耗訊息";
            public static string MMSCoilScheduleDelete = "發送鋼捲生產命令刪除通知";
            public static string EqDownResultCode = "上傳休止实绩";
        }

        public class Cmd
        {
            // Defect 定義
            public const string DefectClassL = "L";     // A,B
            public const string DefectClassM = "M";     // C
            public const string DefectClassH = "H";     // D
            public const string DefectClassS = "S";     // E

            public const string ProOK = "0";
            public const string ProNG = "1";

            // 套筒墊紙同步
            public const string SyncValueInsert = "I";                 // 同步-新增
            public const string SyncValueUpdate = "U";                 // 同步-修改
            public const string SyncValueDelete = "D";                 // 同步-刪除

            // 
            public const string DelScheduleByPlanNo = "整计划删除";
            public const string DelScheduleByHmiReq = "单卷删除";
            public const string DelScheduleByReject = "回退记录";
            public const string DelSchedulePlanNoReject = "部份計畫鋼捲已要求入料或已上鋼捲";

            public const string NoCoilSchedule = "0";
                
        }
    }
}
