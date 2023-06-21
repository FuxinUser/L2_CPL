

using Core.Help;
using System;

namespace Core.Define
{
    public static class DBParaDef
    {

        public static string DBConn = IniSystemHelper.Instance.DBConn;
        public static string HisDBConn = IniSystemHelper.Instance.HisDBConn;
        public static string Level2_5DBConn = IniSystemHelper.Instance.Level2_5_DBConn;
        public static string TestDBConn = IniSystemHelper.Instance.Test_DBConn;

        public static readonly DateTime DefaultTime = Convert.ToDateTime("1970/01/01 00:00:00");
        public static readonly string NowTime = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss:fff");
        public static readonly string DBDateTimeFromat = "yyyy-MM-dd HH:mm:ss.fff";
        public static readonly string NullTime = "0001-01-01 12:00:00.000";
        public static readonly string DB25DateFromat = "yyyyMMdd";
        public static readonly string DB25TimeFromat = "HHmmss";


        public static readonly string HPassSection = "H";

        public static readonly string USE = "1";
        public static readonly string NOTUSE = "0";

        public const  string YES = "Y";
        public const  string NO = "N";

        public const string TRUE = "1";
        public const string FALSE = "0";

        public static readonly string NotCut = "0";
        public static readonly string Cut = "1";

        public static readonly string CutModeStripBreak = "1";
        public static readonly string CutModeSplitCut = "2";
        public static readonly string CutModeScrapCut = "3";
        public static readonly string CutModeSampleCut = "4";
        public static readonly string CutModeReturnCoil = "c";


        public static readonly string CutModeHeadCut = "a";
        public static readonly string CutModeTailCut = "b";

        public enum PDISchema
        {
            EntryCoilID,
            OutCoilID
        }

        public enum CutTempSchema
        {
            In_Coil_ID,
            OriPDI_Out_Coil_ID,
            Coil_ID,
        }

        public class ConnectionSysDef
        {
            public const string Connect = "1";
            public const string UnConnect = "0";
            

            public enum ConnectionType
            {
                L2ConnectToPLC, L2ConnectedByMMS, L2ConnectToMMS, L2ConnectedByWMS, L2ConnectToWMS
            }

            public const string L2 = "LEVEL2";
            public const string MMS = "MMS";
            public const string WMS = "WMS";
            public const string L1 = "LEVEL1";
        }
    }
}
