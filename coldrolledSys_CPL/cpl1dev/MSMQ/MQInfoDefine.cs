using System;


/**
 * Author: 余士鵬
 * Data: 2019/12/19
 * Desc: MQ傳輸類別事件定義
 *          Type
 *          101-150 InfoCoil
 *          151-200 InfoTrk
 *          201-250 InfoHMI
 *          251-300 InfoWMS
 *          301-400 InfoL1
 *          401-500 InfoMMS
 *          501-510 InfoDataSetup
 *          510-550 InfoDtProGtr
 *          550-650 InfoLog
 *          651-700 InfoDtgr
 *          701-750 InfoBCScn
 *          751-800 InfoPrinter
 */
namespace MSMQ
{
    namespace Core.MSMQ
    {
        [Serializable]
        public class MQInfoType
        {
            /// <summary>
            /// 類別碼
            /// </summary>
            public int Event { get; private set; }
            /// <summary>
            /// 描述行為
            /// </summary>
            public string Description { get; private set; }

            public MQInfoType(int type, string description)
            {
                Event = type;
                Description = description;
            }

            public override string ToString()
            {
                return string.Format("Type Code: {0}, Description: {1}", Event, Description);
            }

            //轉Message (不更動原本架構)
            public MQPool.MQMessage Data(object data)
            {
                return new MQPool.MQMessage
                {
                    ID = Event,
                    Data = data
                };
            }

        }

        public class InfoCoil : MQInfoType
        {
            /// <summary>
            /// 鋼捲生產排程調整:鋼捲調整
            /// </summary>
            public static readonly InfoCoil UpdateCoilSchedule = new InfoCoil(0x101, "Info Coil Mgr Update Coil Schedule");
            /// <summary>
            /// 鋼捲生產排程調整:鋼捲刪除
            /// </summary>
            public static readonly InfoCoil DeleteCoilSchedule = new InfoCoil(0x102, "Info Coil Mgr Delete Coil Schedule");
            /// <summary>
            /// 鋼捲生產PDI
            /// </summary>
            public static readonly InfoCoil SaveCoilPDI = new InfoCoil(0x103, "Info Coil Mgr Save MMS PDI Data");
            /// <summary>
            /// 要求PDO上傳
            /// </summary>
            public static readonly InfoCoil AskSndPDO = new InfoCoil(0x104, "Info Coil Mgr Snd PDO");
            /// <summary>
            /// 檢查Exit Cut Model 
            /// </summary>
            public static readonly InfoCoil DetExCoilCut = new InfoCoil(0x105, "Info Coil Mgr det exit coil cut mode");
            /// <summary>
            /// 存取Schedule 
            /// </summary>
            public static readonly InfoCoil SaveSchedule = new InfoCoil(0x106, "Info Coil Mgr save coil schedule");
            /// <summary>
            /// 告知回退實績並須做判斷
            /// </summary>
            public static readonly InfoCoil CoilRejectResult = new InfoCoil(0x107, "Info Coil Mgr det coil reject result");
            /// <summary>
            /// 生产实绩请求(要PDO)
            /// </summary>
            public static readonly InfoCoil ReqPDO = new InfoCoil(0x108, "Info Coil Mgr info mms snd pdo");
            /// <summary>
            /// 要求MMS發送排程
            /// </summary>
            public static readonly InfoCoil AskCoilSchedule = new InfoCoil(0x109, "Info Coil Mgr ask MMS Snd Coil Schedule");
            /// <summary>
            /// 向MMS要求PDI
            /// </summary>
            public static readonly InfoCoil AskPDI = new InfoCoil(0x110, "Info Coil Mgr ask MMS Snd PDI");
            /// <summary>
            /// 三級要求整計畫刪除
            /// </summary>
            public static readonly InfoCoil DeleteShcedPlanNo = new InfoCoil(0x111, "Info Coil Mgr delete schedule by planNo");
            /// <summary>
            /// 發送能源消耗訊息 
            /// </summary>
            public static readonly InfoCoil SndEnergyConsumpInfo = new InfoCoil(0x112, "Info Coil Mgr snd enerfy consumption info");
            /// <summary>
            /// 結算PDO
            /// </summary>
            public static readonly InfoCoil AccountPDO = new InfoCoil(0x113, "Info Coil Mgr account PDO result");
            /// <summary>
            /// 更新PDO預存資料（PDO_TEMP) No_Leader_Code=0
            /// </summary>
            public static readonly InfoCoil SetPDOTempLeaderCode = new InfoCoil(0x114, "Info Coil Mgr set pdo temp no leader code flag");
            /// <summary>
            /// 更新PDO淨重
            /// </summary>
            public static readonly InfoCoil UpdateOutMatPureWT = new InfoCoil(0x115, "Info Coil Mgr update gross wt");
            /// <summary>
            /// 套筒静态数据同步
            /// </summary>
            public static readonly InfoCoil SyncSleeveValue = new InfoCoil(0x116, "Info coil sync sleeve value");
            /// <summary>
            /// 垫纸静态数据同步
            /// </summary>
            public static readonly InfoCoil SyncPaperValue = new InfoCoil(0x117, "Info coil sync paper value");
            /// <summary>
            /// 無鋼捲PDI回覆
            /// </summary>
            public static readonly InfoCoil ResNoPDI = new InfoCoil(0x118, "Info coil no pdi");
            /// <summary>
            /// 無鋼捲生產回覆
            /// </summary>
            public static readonly InfoCoil ResNoCoil = new InfoCoil(0x119, "Info coil no coil");
            /// <summary>
            /// 通知Coil發送Preset通知給DataSetup
            /// </summary>
            public static readonly InfoCoil SndPresetInfo = new InfoCoil(0x120, "Info coil send Preset");

            /// <summary>
            /// 入口切紀錄
            /// </summary>
            public static readonly InfoCoil RecordEnCoilCut = new InfoCoil(0x121, "Save Entry Coil Cut Record");

            /// <summary>
            /// 新鋼捲訊息
            /// </summary>
            public static readonly InfoCoil NewCoilRec = new InfoCoil(0x122, "Info new coil rec");

            /// <summary>
            /// 操作通知修正PDI
            /// </summary>
            public static readonly InfoCoil OpModifyPDI = new InfoCoil(0x123, "Info op modify PDI");


            /// <summary>
            /// 操作通知修改POR子捲號
            /// </summary>
            public static readonly InfoCoil ModifyPORCoilID = new InfoCoil(0x124, "Info modify por coilID");

            /// 上傳PDO的回覆
            /// </summary>
            public static readonly InfoCoil PdoUploadedReply = new InfoCoil(0x125, "Info Pdo Uploaded Reply");

            public InfoCoil(int type, string description) : base(type, description)
            {

            }


        }

        public class InfoTrk : MQInfoType
        {

            /// <summary>
            /// 開始/停止進料確認通知
            /// </summary>
            public static readonly InfoTrk CheckCoilEnterInfo = new InfoTrk(0x151, "Info Trk Enter Info");
            /// <summary>
            /// 確認鋼捲開始供料
            /// </summary>
            public static readonly InfoTrk CheckCoilEnterStar = new InfoTrk(0x152, "Info Trk Check Coil Enter Star");
            /// <summary>
            /// 確認鋼捲停止供料
            /// </summary>
            public static readonly InfoTrk CheckCoilEnterEnd = new InfoTrk(0x153, "Info Trk Check Coil Enter End");
            /// <summary>
            /// 鋼捲生產退料
            /// </summary>
            public static readonly InfoTrk ReturnCoil = new InfoTrk(0x154, "Info Trk Return Coil");
            /// <summary>
            /// 出口位置鋼捲號
            /// </summary>
            public static readonly InfoTrk TrackMapExCoilNo = new InfoTrk(0x155, "Info Trk Exit tracking No, send from L1");
            /// <summary>
            /// 入口位置鋼捲號
            /// </summary>
            public static readonly InfoTrk TrackMapEnCoilNo = new InfoTrk(0x156, "Info Trk Entry tracking No, send from L1");
            /// <summary>
            /// 入料/出料/退料完成訊息
            /// </summary>
            public static readonly InfoTrk WMSActionFinish = new InfoTrk(0x157, "Info Trk WMS Action Finish");
            /// <summary>
            /// 入口段鋼捲ID更正
            /// </summary>
            public static readonly InfoTrk ScnRenameCoil = new InfoTrk(0x158, "Info Trk rename coil ID");
            /// <summary>
            /// 要求發送目前Track Map
            /// </summary>
            public static readonly InfoTrk SndCurCoilMap = new InfoTrk(0x159, "Info Trk snd current coil map");
            /// <summary>
            ///  直接發入料要求
            /// </summary>
            public static readonly InfoTrk SndEntryCoilReqMsg = new InfoTrk(0x160, "Info Trk snd current coil map");
            /// <summary>
            /// 手動入料 - 操作於指定鞍座上操作入料指示
            /// </summary>
            public static readonly InfoTrk SndSkidFeedMsg = new InfoTrk(0x161, "Info Trk snd skid feed");
          
            /// <summary>
            /// 入料/出料/退料要求回覆
            /// </summary>
            public static readonly InfoTrk WMSCoilProResRequest = new InfoTrk(0x163, "Info Trk WMS Res Request ");

            /// <summary>
            /// 天車入料時選擇鋼捲ID
            /// </summary>
            public static readonly InfoTrk CarneEntryCoilSelect = new InfoTrk(0x164, "Info Trk del coil on sk");

            /// <summary>
            /// 天車入料時選擇鋼捲ID
            /// </summary>
            public static readonly InfoTrk DeliveryCoilOut = new InfoTrk(0x165, "Info Trk delvery coil out");

            /// <summary>
            /// 操作刪除鞍作上鋼捲
            /// </summary>
            public static readonly InfoTrk DelSkidCoil = new InfoTrk(0x166, "Info Trk snd delete coil skid");


            public InfoTrk(int type, string description) : base(type, description)
            {

            }
        }

        public class InfoHMI : MQInfoType
        {
            /// <summary>
            /// 入料完成
            /// </summary>
            public static readonly InfoHMI EntryCoilDone = new InfoHMI(0x201, "Info HMI material entry done");
            /// <summary>
            /// 出料完成
            /// </summary>
            public static readonly InfoHMI DeliveryCoilDone = new InfoHMI(0x202, "Info HMI material entry done");
            /// <summary>
            /// 鋼捲刪除,調整處理後通知HMI
            /// </summary>
            public static readonly InfoHMI UpdateCoilSchedView = new InfoHMI(0x203, "Info HMI Update Coil Shedule View");
            public static readonly InfoHMI BarcodeScanResult = new InfoHMI(0x204, "Info HMI Barcode Compare Result");
            public static readonly InfoHMI EventPush = new InfoHMI(0x205, "Info HMI Event");
            public static readonly InfoHMI ScheduleChangeNotice = new InfoHMI(0x206, "Info HMI Schedule Change");
            public static readonly InfoHMI CraneEntryCoil = new InfoHMI(0x207, "Info HMI Crane Entry Coil");
            public static readonly InfoHMI EventPushDialogShow = new InfoHMI(0x208, "Info HMI Event dialog show");
            public static readonly InfoHMI RefreshLineFault = new InfoHMI(0x209, "Info HMI Refresh LineFault");
            public static readonly InfoHMI PdoUploadedReply = new InfoHMI(0x210, "Info HMI Update Pdo Uploaded Reply");

            public InfoHMI(int type, string description) : base(type, description)
            {

            }
        }

        public class InfoWMS : MQInfoType
        {
            /// <summary>
            /// 發送入料要求 (PW15)
            /// </summary>
            public static readonly InfoWMS InfoCoilEntryOrDeliveryReq = new InfoWMS(0x251, "通知WM要求入料/出料");

          
            /// <summary>
            /// 發送退料要求 
            /// </summary>
            public static readonly InfoWMS RejectCoilReqMsg = new InfoWMS(0x253, "通知WMS要求退料");

            /// <summary>
            /// 鋼捲排程編號資訊
            /// </summary>
            public static readonly InfoWMS CoilScheduleInfoMsg = new InfoWMS(0x254, "通知WMS鋼捲排程編號資料");

            /// <summary>
            /// 通知產線入口/出口 Tracking
            /// </summary>
            public static readonly InfoWMS InfoTrackMap = new InfoWMS(0x255, "通知WMS Tracking Map");

            /// <summary>
            /// 鋼卷產出資訊
            /// </summary>
            public static readonly InfoWMS InfoiCoilPDOMsg = new InfoWMS(0x256, "通知WMS鋼卷產出資訊");

            /// <summary>
            /// 通知掃描ID
            /// </summary>
            public static readonly InfoWMS InfoBCSScanID = new InfoWMS(0x257, "通知WMS Scan Coil ID");

            public InfoWMS(int type, string description) : base(type, description)
            {

            }
        }

        public class InfoL1 : MQInfoType
        {
            /// <summary>
            /// 發送Preset (報文201)
            /// </summary>
            public static readonly InfoL1 SndPresetMsg = new InfoL1(0x301, "Ask L1 Snd 201 Preset Msg");
            /// <summary>
            /// 發送TrackMap (報文202)
            /// </summary>
            public static readonly InfoL1 SndTrackMap = new InfoL1(0x302, "Ask L1 Snd 202");
            /// <summary>
            /// 發送SplitId (報文203)
            /// </summary>
            public static readonly InfoL1 SndSplitId = new InfoL1(0x303, "Ask L1 Snd 203");
            /// <summary>
            /// 發送DelSkid (Entry) (報文204)
            /// </summary>
            public static readonly InfoL1 SndDelSkEntryID = new InfoL1(0x304, "Ask L1 Delete coil data on entry coil skid");

            /// <summary>
            /// 發送POR ID (Entry) (報文205)
            /// </summary>
            public static readonly InfoL1 SndNewPORId = new InfoL1(0x305, "Send new por id for L1");

            public InfoL1(int type, string description) : base(type, description)
            {

            }
        }

        public class InfoMMS : MQInfoType
        {

            /// <summary>
            /// 發送鋼捲上鞍座訊號通知
            /// </summary>
            public static readonly InfoMMS CoilLoadedOnSk = new InfoMMS(0x401, "Info MMS the coil load on sk");
            public static readonly InfoMMS CoilSceduleChanged = new InfoMMS(0x402, "Info MMS the coil schedule changed");
            public static readonly InfoMMS CoilSceduleDeleted = new InfoMMS(0x403, "Info MMS the coil schedule deleted");
            public static readonly InfoMMS CoilRejectResult = new InfoMMS(0x404, "Info MMS the coil reject data");
            public static readonly InfoMMS SndEquipDownResult = new InfoMMS(0x405, "Info MMS Snd Equipment Down Result ");
            public static readonly InfoMMS ClientInfoSndPDO = new InfoMMS(0x406, "Info MMS Client ask snd PDO to MMS  ");
            public static readonly InfoMMS ResCoilSchedResult = new InfoMMS(0x407, "Info MMS Client response coil schedule result ");
            public static readonly InfoMMS SndPDIProResult = new InfoMMS(0x408, "Info MMS Client send pdi pro result ");
            public static readonly InfoMMS MMSInfoSndkPDO = new InfoMMS(0x409, "Info MMS send PDO");
            public static readonly InfoMMS AskCoilSchedule = new InfoMMS(0x410, "Info MMS ask coil schedule");
            public static readonly InfoMMS AskPDI = new InfoMMS(0x411, "Info MMS ask pdi");
            public static readonly InfoMMS ResPlanNoShedDelResult = new InfoMMS(0x412, "Info MMS Client response coil schedule delete result by planNo ");
            public static readonly InfoMMS SndEnergyConsumptionInfo = new InfoMMS(0x413,"Info MMS Snd energy consumption info");
            public static readonly InfoMMS UploadEnergyConsumptionInfo = new InfoMMS(0x414, "Info MMS Upload energy consumption info");
            public static readonly InfoMMS UploadLineFaultRecord = new InfoMMS(0x415, "Info MMS Upload line fault infomation");


            public InfoMMS
                (int type, string description) : base(type, description)
            {

            }
        }

        public class InfoDataSetup : MQInfoType
        {


            public static readonly InfoDataSetup ScheduleIDsTo201 = new InfoDataSetup(0x501, "Info DataSetup get pdi from shedule ids convert to 201 Preset Data");

            public static readonly InfoDataSetup SpecificIDTo201 = new InfoDataSetup(0x502, "Info DataSetup get pdi from shedule ids convert to 201 Preset Data");
            public InfoDataSetup
                (int type, string description) : base(type, description)
            {

            }
        }


        public class InfoDtProGtr : MQInfoType
        {
     
            public static readonly InfoDtProGtr ProProcessData = new InfoDtProGtr(0x511, "Info DtProGtr process the level1 data to l25");
            public static readonly InfoDtProGtr CalculateProcessAvgData = new InfoDtProGtr(0x512, "Info DtProGtr calculate tr avg tension");
            public static readonly InfoDtProGtr DeleteAllSiderTrimmer = new InfoDtProGtr(0x513, "Info DtProGtr delete all sider trimmer");

            public InfoDtProGtr
              (int type, string description) : base(type, description)
            {

            }
        }

        public class InfoLog : MQInfoType
        {
            public static readonly InfoLog SaveLogMsg = new InfoLog(0x551, "Info LogMg Save Log Msg");

            public InfoLog
               (int type, string description) : base(type, description)
            {

            }
        }

        public class InfoDtGtr : MQInfoType
        {
            
            public static readonly InfoDtGtr SaveL1302CoilWeldMsg = new InfoDtGtr(0x652, "Info Data Garthering Save 302");
            public static readonly InfoDtGtr SaveL1313SpdTen = new InfoDtGtr(0x653, "Info Data Garthering Save 313");
            public static readonly InfoDtGtr SaveL1309EquipMaint = new InfoDtGtr(0x654, "Info Data Garthering Save 309");
            public static readonly InfoDtGtr SaveL1308CoilWeightScale = new InfoDtGtr(0x655, "Info Data Garthering Save 308");
            public static readonly InfoDtGtr SaveL1310LineFault = new InfoDtGtr(0x656, "Info Data Garthering Save 310");        
            public static readonly InfoDtGtr SaveL1316Utility = new InfoDtGtr(0x658, "Info Data Garthering Save 316");
            public static readonly InfoDtGtr SaveL1317ReturnCoilInfo = new InfoDtGtr(0x658, "Info Data Garthering Save 317");
            public static readonly InfoDtGtr UploadLineFault = new InfoDtGtr(0x659, "Info Data Garthering upload line fault data");
            public static readonly InfoDtGtr SaveL1318SideTrimmerInfo = new InfoDtGtr(0x659, "Info Data Garthering save sider trimmer info");

            public InfoDtGtr
               (int type, string description) : base(type, description)
            {

            }
        }

        public class InfoBCScn : MQInfoType
        {
            public static readonly InfoBCScn ScanEntryCoilNo = new InfoBCScn(0x701, "");
            public static readonly InfoBCScn ScanDeliveryCoilNo = new InfoBCScn(0x702, "");

            public InfoBCScn
               (int type, string description) : base(type, description)
            {

            }
        }

        public class InfoLpr : MQInfoType
        {
            public static readonly InfoLpr ManualPrint = new InfoLpr(0x751, "Info Printer do manual print");

            public static readonly InfoLpr CoilInExitSK2 = new InfoLpr(0x752, "Info Printer coil in exit sk2 to print");

            public static readonly InfoLpr SampleCut = new InfoLpr(0x752, "Info Printer sample coil to print");


            public InfoLpr
               (int type, string description) : base(type, description)
            {

            }
        }
    }
}
