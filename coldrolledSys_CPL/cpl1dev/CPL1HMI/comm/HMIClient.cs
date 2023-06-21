using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using Akka.Actor;
using Akka.Event;
using DataModel.HMIServerCom.Msg;

namespace CPL1HMI
{
    class HMIClient : ReceiveActor
    {
        //Save the loaded of nLog 
        private readonly ILoggingAdapter _log;

        // Save the ActorSelection of connected server
        private ActorSelection _pccom;
        
        // Save the ICancelable element
        private ICancelable _keepaliveCancel;

        private string _client_IP_Port;

        #region Initialize
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="log"> NLog element </param>
        public HMIClient(ILoggingAdapter log)
        {

            _log = log != null ? log : Logging.GetLogger(Context);
            _client_IP_Port = ConfigurationManager.AppSettings["ClientIPPort"];

            var serverAkkaSysName = ConfigurationManager.AppSettings["ServerAkkaSystem"];
            var serverAkkaIPPort = ConfigurationManager.AppSettings["ServerAkkaSystemIP"];          
            _pccom = Context.ActorSelection($"akka.tcp://{serverAkkaSysName}@{serverAkkaIPPort}/user/PCcom");

            Receive<SCCommMsg.ClientAliveMsg>(message => HandleSendClientAlive(message));

            #region C to S
            //要求更新排程
            Receive<SCCommMsg.CS01_AckSchedule>(message => Handle_CS01_AckSchedule(message));
            //要求PDI
            Receive<SCCommMsg.CS02_AckPDI>(message => Handle_CS02_AckPDI(message));
            //排程调整/删除
            Receive<SCCommMsg.CS03_ScheduleChange>(message => Handle_CS03_ScheduleChange(message));
            //入口段鋼卷ID更正
            Receive<SCCommMsg.CS04_RenameCoil>(message => Handle_CS04_RenameCoil(message));
            //退料
            Receive<SCCommMsg.CS05_RejectCoil>(message => Handle_CS05_RejectCoil(message));
            //上传PDO
            Receive<SCCommMsg.CS06_SendMMSPDO>(message => Handle_CS06_SendMMSPDO(message));
            //列印标签
            Receive<SCCommMsg.CS07_PrintLabel>(message => Handle_CS07_PrintLabel(message));
            //更新毛重
            Receive<SCCommMsg.CS08_WeightInput>(message => Handle_CS08_WeightInput(message));
            //停復機紀錄
            Receive<SCCommMsg.CS09_LineFaultData>(message => _pccom.Tell(message));
            //自动入料
            Receive<SCCommMsg.CS10_Coil_AutoFeedModeChange>(message => Handle_CS10_AutoEntryCoil(message));
            //手动入料
            Receive<SCCommMsg.CS11_Coil_ManualFeed>(message => Handle_CS11_ManualEntry(message));
            //鞍座入料
            Receive<SCCommMsg.CS12_Coil_SkidFeed>(message => Handle_CS12_SkidFeed(message));
            //刪除鞍座
            Receive<SCCommMsg.CS13_DeleteSidCoil>(message => _pccom.Tell(message));
            //出料
            Receive<SCCommMsg.CS14_DeliveryCoilOut>(message => _pccom.Tell(message));
            //上傳能源耗用
            Receive<SCCommMsg.CS15_Utility>(message => _pccom.Tell(message));
            //排程匯入
            Receive<SCCommMsg.CS16_FinishLoadSchedule>(message => _pccom.Tell(message));
            //PDI匯入
            Receive<SCCommMsg.CS17_FinishLoadPDI>(message => Handle_CS17_FinishLoadPDI(message));
            //天車入料WMS鋼卷號與掃描鋼卷號不一致，操作選擇後回傳給Server
            Receive<SCCommMsg.CS18_CarneEntryCoilSelect>(message => _pccom.Tell(message));
            //通知PDI修正
            Receive<SCCommMsg.CS20_InfoPDIModify>(message => _pccom.Tell(message));
            //HMI通知Server修改子捲號
            Receive<SCCommMsg.CS21_POR_StripBreakModify>(message => _pccom.Tell(message));
            //HMI通知Server下拋POR鋼捲生產參數給L1
            Receive<SCCommMsg.CS22_POR_PresetL1>(message => _pccom.Tell(message));
           
            #endregion

            #region S to C
            //扫描结果不一致
            Receive<SCCommMsg.SC01_ScnBarcodeID>(message => Handle_SC01_EntryBarcodeID(message));
            //通知排程已刷新
            Receive<SCCommMsg.SC04_ScheduleChangeNotice>(message => Handle_SC04_ScheduleChangeNotice(message));
            //訊息推播
            Receive<SCCommMsg.SC03_EventPush>(message => Handle_SC03_EventPush(message));
            //訊息推播_ShowDialog
            Receive<SCCommMsg.SC03_EventPushShowDialog>(message => Handle_SC03_EventPushShowDialog(message));
            //天車入料WMS鋼卷號與掃描鋼卷號不一致 ( WMS鋼卷號[Empty] / 掃描鋼卷號[有值])
            Receive<SCCommMsg.SC06_CraneEntryCoil>(message => Handle_SC06_CraneEntryCoil(message));

            Receive<SCCommMsg.SC07_RefreshLineDefault>(message => Handle_SC07_RefreshLineDefault(message));

            Receive<SCCommMsg.SC08_PdoUploadedReply>(message => Handle_SC08_PdoUploadedReply(message));

            //Server心跳接收
            Receive<SCCommMsg.ServerAckClientAliveMsg>(message => Handle_Server_Alive(message));


            #endregion

            ReceiveAny(message => Handle_Any(message));
        }

        #region HMI To Server

        /// <summary>
        /// 向Server發送要求排程訊息給MMS
        /// </summary>
        /// <param name="msg"></param>
        private void Handle_CS01_AckSchedule(SCCommMsg.CS01_AckSchedule msg)
        {
            _pccom.Tell(msg);

            PublicComm.AkkaLog.Debug($"HMI发送[CS01_AckSchedule]信息给Server  要求排程 钢卷号 : [{msg.CoilID}]。");
        }

        /// <summary>
        /// 要求PDI
        /// </summary>
        /// <param name="msg"></param>
        private void Handle_CS02_AckPDI(SCCommMsg.CS02_AckPDI msg)
        {
            _pccom.Tell(msg);

            PublicComm.AkkaLog.Debug($"HMI发送[CS02_AckPDI]信息给Server  要求钢卷号 : [{msg.Coil_ID}] PDI。");

        }

        /// <summary>
        /// 排程調整/刪除
        /// </summary>
        /// <param name="msg"></param>
        private void Handle_CS03_ScheduleChange(SCCommMsg.CS03_ScheduleChange msg)
        {
            _pccom.Tell(msg);

            string Action = msg.SchStatus.Equals(0) ? "调整排程" : "删除排程";

            PublicComm.AkkaLog.Debug($"HMI发送[CS03_ScheduleChange]信息给Server  ({Action})钢卷号 : [{msg.EntryCoilID}]。");

        }

        /// <summary>
        /// 入口段鋼卷ID更正
        /// </summary>
        /// <param name="msg"></param>
        private void Handle_CS04_RenameCoil(SCCommMsg.CS04_RenameCoil msg)
        {
            _pccom.Tell(msg);

            PublicComm.AkkaLog.Debug($"HMI发送[CS04_RenameCoil]信息给Server  入口端钢卷更名 钢卷号 : [{msg.Coil_ID}]。");

        }

        /// <summary>
        /// 自动入料
        /// </summary>
        /// <param name="msg"></param>
        private void Handle_CS10_AutoEntryCoil(SCCommMsg.CS10_Coil_AutoFeedModeChange msg)
        {
            _pccom.Tell(msg);

            PublicComm.AkkaLog.Debug($"HMI发送[CS10_Coil_AutoFeedModeChange]信息给Server，更换进料模式。");

        }

        /// <summary>
        /// 手动入料
        /// </summary>
        /// <param name="msg"></param>
        private void Handle_CS11_ManualEntry(SCCommMsg.CS11_Coil_ManualFeed msg)
        {
            _pccom.Tell(msg);

            PublicComm.AkkaLog.Debug($"HMI发送[CS11_Coil_ManualFeed]信息给Server，手动入料 钢卷号 : [{msg.CoilID}]。");

        }

        /// <summary>
        /// 入料作业
        /// </summary>
        /// <param name="msg"></param>
        private void Handle_CS12_SkidFeed(SCCommMsg.CS12_Coil_SkidFeed msg)
        {
            _pccom.Tell(msg);

            PublicComm.AkkaLog.Debug($"HMI发送[CS12_Coil_SkidFeed]信息给Server，鞍座入料 鞍座: [{msg.Skid}]  钢卷号 : [{msg.Coil_ID}]。");

        }

        /// <summary>
        /// 退料
        /// </summary>
        /// <param name="msg"></param>
        private void Handle_CS05_RejectCoil(SCCommMsg.CS05_RejectCoil msg)
        {
            _pccom.Tell(msg);

            PublicComm.AkkaLog.Debug($"HMI发送[CS05_RejectCoil]信息给Server，退料 鞍座: [{msg.Saddle}]  钢卷号 : [{msg.CoilID}]。");

        }

        /// <summary>
        /// 上传PDO
        /// </summary>
        /// <param name="msg"></param>
        private void Handle_CS06_SendMMSPDO(SCCommMsg.CS06_SendMMSPDO msg)
        {
            _pccom.Tell(msg);

            PublicComm.AkkaLog.Debug($"HMI发送[CS06_SendMMSPDO]信息给Server，PDO上传  钢卷号 : [{msg.Coil_ID}]。");

        }

        /// <summary>
        /// 列印标签
        /// </summary>
        /// <param name="msg"></param>
        private void Handle_CS07_PrintLabel(SCCommMsg.CS07_PrintLabel msg)
        {
            _pccom.Tell(msg);

            PublicComm.AkkaLog.Debug($"HMI发送[CS07_PrintLabel]信息给Server，列印标签  钢卷号 : [{msg.CoilID}]。");

        }

        /// <summary>
        /// 更新毛重
        /// </summary>
        /// <param name="msg"></param>
        private void Handle_CS08_WeightInput(SCCommMsg.CS08_WeightInput msg)
        {
            _pccom.Tell(msg);

            PublicComm.AkkaLog.Debug($"HMI发送[CS08_WeightInput]信息给Server，更新毛重  钢卷号 : [{msg.OutCoilID}] 重量 : [{msg.WeightInput}]。");

        }

        /// <summary>
        /// 汇入PDI
        /// </summary>
        /// <param name="msg"></param>
        private void Handle_CS17_FinishLoadPDI(SCCommMsg.CS17_FinishLoadPDI msg)
        {
            _pccom.Tell(msg);

            PublicComm.AkkaLog.Debug($"HMI发送[CS17_FinishLoadPDI]信息给Server，汇入PDI。");

        }

        #endregion

        #region Server To HMI
        private void Handle_Server_Alive(SCCommMsg.ServerAckClientAliveMsg message)
        {
            _log.Debug("接收Server Alive訊息");

        }

        private void HandleSendClientAlive(SCCommMsg.ClientAliveMsg message)
        {
            _log.Debug("Send keep alive message to server as IP Port:" + message.Client_IP_Port);
            _pccom.Tell(message);
        }
        /// <summary>
        /// 掃描結果不符，收到通知讓Tracking畫面跳ID選擇畫面給操作選擇
        /// </summary>
        /// <param name="msg"></param>
        public void Handle_SC01_EntryBarcodeID(SCCommMsg.SC01_ScnBarcodeID msg)
        {
            EventLogHandler.Instance.LogInfo("2-1", "扫描结果不相符", "入口段扫描Barcode钢卷编号与TrackingMap鞍座钢卷编号不符");
        }
        /// <summary>
        /// 信息推播
        /// </summary>
        /// <param name="msg"></param>
        public void Handle_SC03_EventPush(SCCommMsg.SC03_EventPush msg)
        {
            try
            {
                PublicForms.Main.Invoke(new Action(() =>
                { PublicForms.Main.Handle_SC03_EventPush(msg); }));
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"信息推播有错误");
                PublicComm.ExceptionLog.Debug($"信息推播有错误:{ex}");
                PublicComm.ClientLog.Debug($"信息推播有错误:[{ex}]");
                PublicComm.AkkaLog.Debug($"信息推播有错误:[{ex}]");
            }
        }

        /// <summary>
        /// 信息推播_ShowDialog
        /// </summary>
        /// <param name="msg"></param>
        public void Handle_SC03_EventPushShowDialog(SCCommMsg.SC03_EventPushShowDialog msg)
        {
            try
            {
                PublicForms.Main.Invoke(new Action(() =>
                { PublicForms.Main.Handle_SC03_EventPushShowDialog(msg); }));
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"信息視窗有错误");
                PublicComm.ExceptionLog.Debug($"信息視窗有错误:{ex}");
                PublicComm.ClientLog.Debug($"信息視窗有错误:[{ex}]");
                PublicComm.AkkaLog.Debug($"信息視窗有错误:[{ex}]");
            }
        }

        public void Handle_SC04_ScheduleChangeNotice(SCCommMsg.SC04_ScheduleChangeNotice msg)
        {
            try
            {
                PublicForms.Main.Invoke(new Action(() =>
                {
                    //是否开启画面
                    bool openflag = false;
                    frm_1_1_PDISchl form = new frm_1_1_PDISchl();
                    foreach (Form fx in PublicForms.Main.Pnl_Main.Controls.Cast<Control>().Where(x => x is Form))
                    {
                        if (fx.Name.Equals(form.Name))
                        {
                            form = fx as frm_1_1_PDISchl;
                            PublicForms.PDISchl.Handle_SC04_ScheduleChangeNotice(msg);
                            openflag = true;
                        }
                    }
                    if (openflag == false)
                    {
                        EventLogHandler.Instance.EventPush_Message($"已接收到新的生产排程");

                        //frm_0_0_Main Main = new frm_0_0_Main
                        //{
                        //    MdiParent = PublicForms.Main,
                        //    Parent = PublicForms.Main.pnl_Main,
                        //    StartPosition = FormStartPosition.Manual,
                        //    Location = new Point(0, 55),
                        //    Width = 1920,
                        //    Height = 980

                        //};

                        //Main.Show();
                        //Main.Focus();

                        //Handle_SC04_ScheduleChangeNotice(msg);
                    }
                }));
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"排程异动通知有错误");
                PublicComm.ExceptionLog.Debug($"排程异动通知有错误:{ex}");
                PublicComm.ClientLog.Debug($"排程异动通知有错误:[{ex}]");
                PublicComm.AkkaLog.Debug($"排程异动通知有错误:[{ex}]");
            }
        }

        /// <summary>
        /// 天車入料Server通知，
        /// </summary>
        /// <param name="Message"></param>
        public void Handle_SC06_CraneEntryCoil(SCCommMsg.SC06_CraneEntryCoil Message)
        {
            try
            {
                PublicForms.Main.Invoke(new Action(() =>
                {
                    //是否开启画面
                    bool openflag = false;
                    Frm_2_1_Tracking form = new Frm_2_1_Tracking();
                    foreach (Form fx in PublicForms.Main.Pnl_Main.Controls.Cast<Control>().Where(x => x is Form))
                    {
                        if (fx.Name.Equals(form.Name))
                        {
                            form = fx as Frm_2_1_Tracking;
                            PublicForms.Tracking.Handle_SC06_CraneEntryCoil(Message);
                            openflag = true;
                        }
                    }
                    if (openflag == false)
                    {
                        PublicForms.Main.tsMenuItem_2_1.PerformClick();

                        //frm_0_0_Main Main = new frm_0_0_Main
                        //{
                        //    MdiParent = PublicForms.Main,
                        //    Parent = PublicForms.Main.pnl_Main,
                        //    StartPosition = FormStartPosition.Manual,
                        //    Location = new Point(0, 55),
                        //    Width = 1920,
                        //    Height = 980
                        //};
                        //Main = Main.Parent.Parent as frm_0_0_Main;
                        //Main.tsMenuItem_1_1.PerformClick();

                        //Main.Show();
                        //Main.Focus();

                        Handle_SC06_CraneEntryCoil(Message);
                    }
                }));
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"天车入料通知有错误");
                PublicComm.ExceptionLog.Debug($"天车入料通知有错误:{ex}");
                PublicComm.ClientLog.Debug($"天车入料通知有错误:[{ex}]");
                PublicComm.AkkaLog.Debug($"天车入料通知有错误:[{ex}]");
            }
        }

        /// <summary>
        /// 刷新停復機畫面
        /// </summary>
        /// <param name="message"></param>
        public void Handle_SC07_RefreshLineDefault(SCCommMsg.SC07_RefreshLineDefault msg)
        {
            try
            {
                PublicForms.Main.Invoke(new Action(() =>
                {
                    //是否开启画面
                    var openflag = false;
                    var form = new frm_4_1_LineDelayRecord();
                    foreach (Form fx in PublicForms.Main.Pnl_Main.Controls.Cast<Control>().Where(x => x is Form))
                    {
                        if (fx.Name.Equals(form.Name))
                        {
                            form = fx as frm_4_1_LineDelayRecord;
                            PublicForms.LineDelayRecord.Handle_SC07_RefreshLineDefault();
                            openflag = true;
                        }
                    }
                    if (openflag == false)
                    {
                        EventLogHandler.Instance.EventPush_Message($"已接收到刷新停复机");
                    }
                }));
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"刷新停复机通知有错误");
                PublicComm.ExceptionLog.Debug($"刷新停复机通知有错误:{ex}");
                PublicComm.ClientLog.Debug($"刷新停复机通知有错误:[{ex}]");
                PublicComm.AkkaLog.Debug($"刷新停复机通知有错误:[{ex}]");
            }
        }

        public void Handle_SC08_PdoUploadedReply(SCCommMsg.SC08_PdoUploadedReply msg)
        {
            try
            {
                PublicForms.Main.Invoke(new Action(() => PublicForms.PDODetail?.Handle_SC08_PdoUploadedReply(msg.Message)));
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"顯示上傳PDO");
                PublicComm.ExceptionLog.Debug($"刷新停复机通知有错误:{ex}");
                PublicComm.ClientLog.Debug($"刷新停复机通知有错误:[{ex}]");
                PublicComm.AkkaLog.Debug($"刷新停复机通知有错误:[{ex}]");
            }
        }

        #endregion

        #endregion

        #region Handle events that when receiving request messages from forms (send to Pccomm)

        /// <summary>
        ///     Handle when receiving an undefined message
        /// </summary>
        /// <param name="message"> Information of undefined type </param>
        private void Handle_Any(object message)
        {
            _log.Error($"Received an unhandled message!!! type:{message.GetType()} from Sender:{Sender.Path}");
        }

        #endregion

        #region Override events
        protected override void PreStart()
        {
            _log.Debug("PreStart. ");

            var alive = new SCCommMsg.ClientAliveMsg()
            {
                Client_IP_Port = _client_IP_Port
            };

            _keepaliveCancel = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(3), Self, alive, Self);
           
            base.PreStart();
        }


        protected override void PreRestart(Exception reason, object message)
        {
            _log.Debug($"PreRestart. exception message:[{reason.Message}] happened when received, message:[{message.GetType().Name}]");
            if (reason.StackTrace != null) _log.Debug($"PreRestart. exception stackTrace:{reason.StackTrace}");

            base.PreRestart(reason, message);
        }


        protected override void PostStop()
        {
            _log.Debug($"PostStop. {Self.Path.Name}");

            base.PostStop();
        }


        protected override void PostRestart(Exception reason)
        {
            _log.Debug($"PostRestart. exception message:[{reason.Message}] happened when received");

            base.PostRestart(reason);
        }
        #endregion
    }
}
