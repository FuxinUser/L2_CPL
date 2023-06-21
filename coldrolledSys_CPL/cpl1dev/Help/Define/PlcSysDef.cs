namespace Core.Define
{
    /// <summary>
    /// 外部系統 PLC 相關定義
    /// </summary>
    public static class PlcSysDef
    {
        public static string SysName = "Level1";

        // L2->L1
        public class SndMsgCode {

            // L1命令編號 產線->L1
            public const string L1201Preset = "201";
            public const string L1202TrackMapL2 = "202";
            public const string L1203SplitID = "203";
            public const string L1204DelSkID = "204";
            public const string L1205NewPOR = "205";
            public const string L2299Alive = "299";
        }

        // L1->L2
        public class RcvMsgCode {

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
            public const string L1317ReturnCoilInfo = "317";            // Return Coil Infomation
            public const string L1318SideTrimmerInfo = "318";           // If Coil need to side-trim，Using this msg. When coil on POR，start to send msg every 5 second. When end coil dismount from TR，stop to send msg.
            public const string L1399Alive = "399";                     // Check the connection status in L2. 

        }

        public class ExitMsgCutMode
        {
            public const int StripBreak = 1;
            public const int SplitCut = 2;
            public const int ScrapCut = 72;
            public const int VirtualCut = 128;
        }

        public class Cmd {

            // L1 Defect Level Def
            public const string DefectGradeA = "A";
            public const string DefectGradeB = "B";
            public const string DefectGradeC = "C";
            public const string DefectGradeD = "D";
            public const string DefectGradeE = "E";

            // Use, Not Use Int Def
            public const int Use = 1;
            public const int NotUse = 0;

            // Use , Not Use Str Def
            public const string UseStr = "Y";
            public const string NotUseStr = "N";
         
        }

        public class Pos
        {
            /*
                Preset position
                1: Uncoiler 
                2: UncSK3
                3: UncSK2
                4: UncTOP
                11~50: reserved coil 1~40
            */
            public const int Preset201POR = 1;
            public const int Preset201SK1 = 2;
            public const int Preset201SK2 = 3;
            public const int Preset201ETOP = 4;         
        }
    }
}
