using Core.Define;
using System;

/*
* Author:ICSC SPYUA
* Desc: Clinet發送至Server命令定義
* Date:2019/12/30
*/


namespace DataModel.HMIServerCom.Msg
{
    public class SCCommMsg
    {
        /// 狀態顏色 (0:Information ,1:Question ,2:Warning ,3:Error;4:CheckOK) 
        public static class DialogType
        {
            public static int Information = 0;
            public static int Question = 1;
            public static int Warning = 2;
            public static int Error = 3;
            public static int CheckOK = 4;
        }

        /// <summary>
        /// Client Alive
        /// </summary>
        public class ClientAliveMsg
        {
            public string Client_IP_Port { get; set; }

            public string data { get; set; } = string.Empty;
        }


        public class ServerAckClientAliveMsg
        {
            public string data { get; set; } = string.Empty;
        }

        #region PDI

        [Serializable]
        public class CS20_InfoPDIModify{
            public string CoilID { get; set; } = string.Empty;
            public string PlanNo { get; set; } = string.Empty;

            public CS20_InfoPDIModify(string planNo , string coilID)
            {
                CoilID = coilID;
                PlanNo = planNo;
            }
        }

        #endregion
        
        #region 生產排程管理

        /// <summary>
        /// 重新要求排程,通知Server發送電文給MMS要求下發最新排程
        /// </summary>
        [Serializable]
        public class CS01_AckSchedule
        {
            /// <summary>
            /// Source = 產線_HMI
            /// </summary>
            public string Source = string.Empty;
            /// <summary>
            /// ID = AckSchedule
            /// </summary>
            public string ID = string.Empty;

            public string CoilID = string.Empty;
        }

        /// <summary>
        /// 通知Server發送電文給MMS要求指定鋼捲PDI資料
        /// 一次只會跟MMS發送一顆鋼卷的PDI請求
        /// </summary>       
        [Serializable]
        public class CS02_AckPDI
        {
            /// <summary>
            /// Source = 產線_HMI
            /// </summary>
            public string Source = string.Empty;
            /// <summary>
            /// ID = AckPDI
            /// </summary>
            public string ID = string.Empty;
            /// <summary>
            /// 入口鋼卷號
            /// </summary>
            public string Coil_ID = string.Empty;
        }

        #region 調整/刪除排程
        /// <summary>
        /// ADJUST = 0 調整排程  DELETE = 1 刪除排程
        /// </summary>
        [Serializable]
        public enum ScheduleStatus
        {
            ADJUST, DELETE
        }

        /// <summary>
        /// 通知Server更新排程給MMS及WMS
        /// 不論刪除或調整Schedule順序，皆告知MMS同步資料
        /// </summary>    
        [Serializable]
        public class CS03_ScheduleChange
        {
            /// <summary>
            /// Source = 產線_HMI
            /// </summary>
            public string Source;
            /// <summary>
            /// ADJUST = 0 調整排程  DELETE = 1 刪除排程
            /// </summary>
            public ScheduleStatus SchStatus;
            /// <summary>
            /// 入口鋼卷號
            /// </summary>
            public string EntryCoilID;
            /// <summary>
            /// 刪除責任者
            /// </summary>
            public string OperatorID;
            /// <summary>
            /// 刪除原因代碼
            /// </summary>
            public string ReasonCode;


            public CS03_ScheduleChange() {

            }

            public CS03_ScheduleChange(string source, ScheduleStatus schStatus, string entryCoilID, string operatorID, string reasonCode)
            {
                Source = source;
                SchStatus = schStatus;
                EntryCoilID = entryCoilID;
                OperatorID = operatorID;
                ReasonCode = reasonCode;
            }
        }
        /// <summary>
        /// 通知HMI排程已更新
        /// </summary>
        [Serializable]
        public class SC04_ScheduleChangeNotice
        {
            public string Source { get; set; }
            /// <summary>
            /// ID = ScheduleChangEnotice
            /// </summary>
            public string ID { get; set; }

            public SC04_ScheduleChangeNotice(string source, string id)
            {
                this.Source = source;
                this.ID = id;
            }
        }
        #endregion

        /// <summary>
        /// HMI 完成匯入排程，通知Server發送Preset40筆
        /// </summary>
        [Serializable]
        public class CS16_FinishLoadSchedule
        {



        }

        /// <summary>
        /// HMI 完成匯入PDI，通知Server發送Preset40筆
        /// </summary>
        [Serializable]
        public class CS17_FinishLoadPDI
        {



        }

        #endregion


        #region 入料段作業

        /// <summary>
        /// 自動入料
        /// 自動入料模式變更，通知Server進行模式確認
        /// Select [TBL_SystemSetting]
        /// </summary>
        [Serializable]
        public class CS10_Coil_AutoFeedModeChange
        {
            /// <summary>
            /// Source = 產線_HMI
            /// </summary>
            public string Source = string.Empty;
            /// <summary>
            /// ID = AutoFeedModeChange
            /// </summary>
            public string ID = string.Empty;
        }


        /// <summary>
        /// 手動入料
        /// 手動操作通知Server可以入料
        /// </summary>
        [Serializable]
        public class CS11_Coil_ManualFeed
        {
            /// <summary>
            /// Source = 產線_Server
            /// </summary>
            public string Source = string.Empty;
            /// <summary>
            /// ID = ManualFeed
            /// </summary>
            public string CoilID = string.Empty;
        }


        /// <summary>
        /// 鞍座入料
        /// 在鞍座上手動操作入料並通知Server
        /// </summary>
        [Serializable]
        public class CS12_Coil_SkidFeed
        {
            /// <summary>
            /// Source = 產線_Server
            /// </summary>
            public string Source = string.Empty;
            /// <summary>
            /// ID = SkidFeed
            /// </summary>
            public string ID = string.Empty;
            /// <summary>
            /// 鞍座
            /// </summary>
            public int Skid;
            /// <summary>
            /// 入料鋼卷號
            /// </summary>
            public string Coil_ID = string.Empty;



        }


        /// <summary>
        /// 入口段掃碼結果
        /// 1. SERVER接收Barcode掃碼結果
        /// 2. SERVER比對CoilMap與掃碼結果不符
        /// 3. 通知CLIENT掃碼結果不符
        /// </summary>
        [Serializable]
        public class SC01_ScnBarcodeID
        {
            public BCScanResult ScanResult;

            public string CoilNo = string.Empty;

            public string CoilNoOnMap = string.Empty;

            /// <summary>
            ///  // 1: Uncoiler 2: UncSK1 3: UncSK2 4: UncTOP 11~50: reserved coil 1~40
            /// </summary>
            public int Postion;

            public bool ParsingSuccess = true;

            public SC01_ScnBarcodeID(BCScanResult scanResult, string coilNo, string coilNoOnMap, int position, bool parsingSuccess = true)
            {
                ScanResult = scanResult;
                CoilNo = coilNo;
                CoilNoOnMap = coilNoOnMap;
                Postion = position;
                ParsingSuccess = parsingSuccess;
            }
        }


        /// <summary>
        /// 出口段掃碼結果
        /// 1. SERVER接收Barcode掃碼結果
        /// 2. SERVER比對CoilMap與掃碼結果不符
        /// 3. 通知CLIENT掃碼結果不符
        /// </summary>
        [Serializable]
        public class SC05_DeliveryBarcodeID
        {
            public BCScanResult ScanResult;

            public string CoilNo = string.Empty;

            public string CoilNoOnMap = string.Empty;

            public SC05_DeliveryBarcodeID(BCScanResult scanResult, string coilNo, string coilNoOnMap)
            {
                ScanResult = scanResult;
                CoilNo = coilNo;
                CoilNoOnMap = coilNoOnMap;
            }
        }


        [Serializable]
        public enum BCScanResult
        {
            Sucess, Error
        }


        /// <summary>
        /// 掃描Barcode
        /// 鋼捲ID更正
        /// 1. CLIENT收到SERVER通知掃描結果鋼捲ID不符
        /// 2. CLIENT提示畫面要求操作確認
        /// 3. CLIENT通知SERVER操作確認結果
        /// </summary>
        [Serializable]
        public class CS04_RenameCoil
        {
            /// <summary>
            /// Source = 產線_HMI
            /// </summary>
            public string Source = string.Empty;
            /// <summary>
            /// ID = RenameCoil
            /// </summary>
            public string ID = string.Empty;
            /// <summary>
            /// 根據Postion去判斷Select [L3L2_PDI] or [L2L3_PDO]
            /// </summary>
            public string Coil_ID = string.Empty;
            /// <summary>
            ///  // 1: Uncoiler 2: UncSK1 3: UncSK2 4: UncTOP 11~50: reserved coil 1~40
            /// </summary>
            public int Postion;
        }


        /// <summary>
        /// 1. CLIENT紀錄退料實績相關資料到資料庫
        /// 2. 通知SERVER發送退料實績（MMS）及退料要求（WMS）
        /// 詳細資料至DB撈
        /// </summary>
        [Serializable]
        public class CS05_RejectCoil
        {
            /// <summary>
            /// Source = 產線_HMI
            /// </summary>
            public string Source = string.Empty;
            /// <summary>
            /// ID = RejectCoil
            /// </summary>
            public string ID = string.Empty;
            /// <summary>
            /// 入口鋼卷號
            /// 入口段才有退料，故為入口剛卷號
            /// </summary>
            public string CoilID = string.Empty;
            /// <summary>
            /// 鞍座位置 (ESK01,ESK02,ETOP
            /// </summary>
            public string Saddle = string.Empty;

            public string WMSPos { get {

                    var pos = string.Empty;

                    switch (Saddle)
                    {
                        case "ESK01":
                            pos = WMSSysDef.SkPos.ESk03.ToString();
                            break;
                        case "ESK02":
                            pos = WMSSysDef.SkPos.ESk02.ToString();
                            break;

                        default:
                            pos = WMSSysDef.SkPos.ETop.ToString();
                            break;
                    }

                    return pos;
                } }

        }


        /// <summary>
        /// SERVER通知CLIENT鋼捲到達位置
        /// </summary>
        [Serializable]
        public class SC02_CoilEntry
        {
            public CoilSkPosition CoilPosition;

            public SC02_CoilEntry(int presetSKNo)
            {
                if (presetSKNo == PlcSysDef.Pos.Preset201ETOP)
                {
                    CoilPosition = CoilSkPosition.ETOP;
                    return;
                }


                if (presetSKNo == PlcSysDef.Pos.Preset201SK1)
                {
                    CoilPosition = CoilSkPosition.ETOP;
                    return;
                }


                if (presetSKNo == PlcSysDef.Pos.Preset201SK2)
                {
                    CoilPosition = CoilSkPosition.ESK02;
                    return;
                }

            }
        }

        public enum CoilSkPosition
        {
            POR,ESK01, ESK02, ETOP, TR, DSK01, DSK02, DTOP
        }


        /// <summary>
        /// SERVER通知天車入料給予天車入料ID
        /// </summary>
        [Serializable]
        public class SC06_CraneEntryCoil
        {
            public string CoilID = string.Empty;
            public CoilSkPosition CoilPosition;

            public SC06_CraneEntryCoil(string CoilNo, CoilSkPosition pos)
            {
                CoilID = CoilNo;
                CoilPosition = pos;
            }
        }

        /// <summary>
        /// 刷新停復機畫面
        /// </summary>
        [Serializable]
        public class SC07_RefreshLineDefault
        {
            public SC07_RefreshLineDefault() { }
        }


        /// <summary>
        /// HMI通知Server當天車入料時選擇此ID
        /// </summary>
        [Serializable]
        public class CS18_CarneEntryCoilSelect
        {
            public string coilID = string.Empty;

            public CoilSkPosition SKPos;

            public int SKNo
            {
                get
                {
                    // SK3
                    if (SKPos == CoilSkPosition.ETOP)
                        return PlcSysDef.Pos.Preset201ETOP;
                    if (SKPos == CoilSkPosition.ESK02)
                        return PlcSysDef.Pos.Preset201SK2;
                    if (SKPos == CoilSkPosition.ESK01)
                        return PlcSysDef.Pos.Preset201SK1;

                    return PlcSysDef.Pos.Preset201POR;
                }
            }

        }


        /// <summary>
        /// POR斷帶處理，HMI通知Server發送Preset 201給L1
        /// </summary>
        [Serializable]
        public class CS19_POR_StripBreak
        {

            /// <summary>
            /// POR子捲號
            /// </summary>
            public string Coil_ID = string.Empty;

        }

        /// <summary>
        /// 在POR端因為鋼捲斷帶，要指定子捲號，HMI通知Server修改子捲號，Server需下拋205NewPORId通知L1修改POR捲號。
        /// </summary>
        [Serializable]
        public class CS21_POR_StripBreakModify
        {

            /// <summary>
            /// POR子捲號
            /// </summary>
            public string Coil_ID = string.Empty;

        }
        public class CS22_POR_PresetL1
        {

            /// <summary>
            /// POR钢卷号
            /// </summary>
            public string Coil_ID = string.Empty;

            /// <summary>
            /// POR钢卷之计划号
            /// </summary>
            public string Plan_No = string.Empty;
        }
        #endregion


        #region 出料段作業

        /// <summary>
        /// 操作確認上傳鋼捲PDO
        /// 1. 操作於CLIENT HMI確認PDO資料無誤並要求上傳PDO
        /// 2. CLIENT通知SERVER發送PDO資料
        /// </summary>    
        [Serializable]
        public class CS06_SendMMSPDO
        {
            /// <summary>
            /// Source = 產線_HMI
            /// </summary>
            public string Source = string.Empty;
            /// <summary>
            /// ID = SendMMSPDO
            /// </summary>
            public string ID = string.Empty;
            /// <summary>
            /// 出口鋼卷號，Select [L2L3_PDO]將資料上傳給MMS
            /// </summary>
            public string Coil_ID = string.Empty;

            public string In_Coil_ID = string.Empty;

            public string Plan_No = string.Empty;                      

            public string FinishTime = string.Empty;

            /// <summary>
            /// 上傳UserID
            /// </summary>
            public string OperatorID;
        }

        /// <summary>
        /// 操作要求手動列印標籤, CLIENT通知SERVER列印標籤
        /// </summary>    
        [Serializable]
        public class CS07_PrintLabel
        {
            /// <summary>
            /// Source = 產線_HMI
            /// </summary>
            public string Source = string.Empty;
            /// <summary>
            /// ID = PrintLabel
            /// </summary>
            public string ID = string.Empty;
            /// <summary>
            /// 出口鋼卷號，這邊會給指定鞍座上的鋼卷號
            /// </summary>
            public string CoilID = string.Empty;
        }

        /// <summary>
        /// 操作手動輸入秤重資料, CLIENT通知SERVER更新鋼捲秤重資料（毛重）
        /// </summary>
        [Serializable]
        public class CS08_WeightInput
        {
            /// <summary>
            /// Source = 產線_HMI
            /// </summary>
            public string Source = string.Empty;
            /// <summary>
            /// ID = WeightInput
            /// </summary>
            public string ID = string.Empty;
            /// <summary>
            /// 出口鋼卷號
            /// </summary>
            public string OutCoilID = string.Empty;
            /// <summary>
            /// 重量(毛重)
            /// </summary>
            public string WeightInput = string.Empty;

            // 重量(Str轉float)
            public float CoilWt {
                get {
                    try {
                        var wt = float.Parse(WeightInput);
                        return wt;
                    }
                    catch
                    {
                        return -1.0f;
                    }
                }
            }
        }
        /// <summary>
        /// 出口段出料
        /// </summary>
        [Serializable]
        public class CS14_DeliveryCoilOut
        {
            public string CoilID { get; set; } = string.Empty;
            public CoilSkPosition CoilPosition;
            public int Pos { get {

                    var pos = 0;

                    switch (CoilPosition)
                    {
                        case CoilSkPosition.ETOP:
                            pos = 4;// 1;
                            break;
                        case CoilSkPosition.ESK02:
                            pos = 3;// 2;
                            break;

                        case CoilSkPosition.DSK01:
                            pos = 6;// 4;
                            break;
                        case CoilSkPosition.DSK02:
                            pos = 7;// 5;
                            break;
                        case CoilSkPosition.DTOP:
                            pos = 8;// 6;
                            break;
                        default:
                            //pos = 3;  //TOP
                            break;
                    }

                    return pos;
                } }

            public string PosStr { get
                {

                    var posstr = string.Empty;

                    switch (CoilPosition)
                    {
                        case CoilSkPosition.ETOP:
                            posstr = "ESK01";
                            break;
                        case CoilSkPosition.ESK02:
                            posstr = "ESK02";
                            break;

                        case CoilSkPosition.DSK01:
                            posstr = "DSK01";
                            break;
                        case CoilSkPosition.DSK02:
                            posstr = "DSK02";
                            break;
                        case CoilSkPosition.DTOP:
                            posstr = "DTOP";
                            break;
                        default:
                            posstr = "ETOP";  //TOP
                            break;
                    }

                    return posstr;


                } }
        }
        #endregion


        #region 刪除鞍座鋼卷號
        /// <summary>
        /// 刪除鞍座上鋼卷號
        /// </summary>
        [Serializable]
        public class CS13_DeleteSidCoil
        {
            public short DelPos;
            public string Coil_ID { get; set; } = string.Empty;
        }

        #endregion


        #region 能源耗用
        /// <summary>
        /// 能源耗用
        /// </summary>
        [Serializable]
        public class CS15_Utility
        {
            // 生產氣體量 單位(?)
            public string TotalCompressedAir = string.Empty;
            // 生產水 單位(?)
            public string TotalCoolingWater = string.Empty;
            // 日期yyyymmdd
            public string Shift_Date = string.Empty;
            // 班次 1-夜，2-早，3-中
            public string Shift_No = string.Empty;
            // 班组 A-甲，B-乙，C-丙，D-丁
            public string Group_No = string.Empty;
            // 机组代码 
            public string Unit_code = string.Empty;

            // 顯示用
            public string ShiftName { get {

                    if (Shift_No.Equals("1"))
                        return "夜班";
                    if (Shift_No.Equals("2"))
                        return "早班";
                    if (Shift_No.Equals("3"))
                        return "中班";
                    return string.Empty;
                } }

            public string GroupName
            {
                get
                {

                    if (Shift_No.Equals("A"))
                        return "甲";
                    if (Shift_No.Equals("B"))
                        return "乙";
                    if (Shift_No.Equals("C"))
                        return "丙";
                    if (Shift_No.Equals("D"))
                        return "丁";
                    return string.Empty;
                }
            }

            // Server用
            public float TAir
            {
                get
                {
                    return float.Parse(TotalCompressedAir);
                }
            }
            public float TWater
            {
                get
                {
                    return float.Parse(TotalCoolingWater);
                }
            }


        }
        #endregion


        #region 系統狀態

        /// <summary>
        /// 系統狀態
        /// 1. 操作於CLIENT HMI編輯停復機資料，存檔時通知SERVER
        /// 2. SERVER發送停復機資料給MMS
        /// Table : [TBL_LineFaultRecords]
        /// </summary>
        [Serializable]
        public class CS09_LineFaultData
        {
            /// <summary>
            /// Source = 產線_HMI
            /// </summary>
            public string Source = string.Empty;
            /// <summary>
            /// ID = WeightInput
            /// </summary>
            public string ID = string.Empty;
            /// <summary>
            /// 班次
            /// </summary>
            public string prod_Shift_no = string.Empty;
            /// <summary>
            /// 停機開始時間
            /// </summary>
            public string stop_start_time = string.Empty;
            /// <summary>
            /// 停機結束時間
            /// </summary>
            public string stop_end_time = string.Empty;

            /// <summary>
            /// 日期
            /// </summary>
            public string prod_time = string.Empty;
        }

        /// <summary>
        /// 事件訊息推播
        /// Server To Client的訊息
        /// </summary>
        [Serializable]
        public class SC03_EventPush
        {
            /// <summary>
            /// 訊息來源
            /// </summary>
            public string Source { get; set; } = string.Empty;
            /// <summary>
            /// 訊息名稱
            /// </summary>
            public string EventName { get; set; } = string.Empty;
            /// <summary>
            /// 訊息內容
            /// </summary>
            public string EventMsg { get; set; } = string.Empty;

            public DateTime Time { get; set; }

            public SC03_EventPush(string eventName, string eventMsg = "", string source = "")
            {
                this.Source = source;
                this.EventName = eventName;
                this.EventMsg = eventMsg;
                this.Time = DateTime.Now;
            }

        }

        /// <summary>
        /// 事件訊息推播(Dialog Show)
        /// Server To Client的訊息
        /// </summary>
        [Serializable]
        public class SC03_EventPushShowDialog{

            /// <summary>
            /// 標題
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// 內容
            /// </summary>
            public string Message { get; set; }
            /// <summary>
            /// 狀態顏色 (0:Information ,1:Question ,2:Warning ,3:Error;4:CheckOK) 
            /// </summary>
            public int Type { get; set; } 

            public SC03_EventPushShowDialog(string title, string msg, int type)
            {
                Title = title;
                Message = msg;
                Type = type;                
            }

        }

        [Serializable]
        public class SC03_APStatus
        {
            /// <summary>
            /// AP 名稱
            /// </summary>
            public string ApName { get; set; }
            /// <summary>
            /// 狀態
            /// </summary>
            public string Status { get; set; }

            public SC03_APStatus(string apName, string status)
            {
                ApName = apName;
                Status = status;
            }
        }

        /// <summary>
        /// 上傳 PDO 後的回覆
        /// </summary>
        [Serializable]
        public class SC08_PdoUploadedReply
        {
            /// <summary>
            /// 訊息
            /// </summary>
            public string Message { get; set; }

            public SC08_PdoUploadedReply(string msg)
            {
                Message = msg;
            }
        }

        #endregion


    }
}
