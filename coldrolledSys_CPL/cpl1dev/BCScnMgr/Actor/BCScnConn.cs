using Akka.Actor;
using Akka.IO;
using AkkaSysBase;
using AkkaSysBase.Base;
using BCScnMgr.Service;
using Core.Util;
using LogSender;
using System.Collections.Generic;
using static DataMod.BarCode.Msg.BCSModel;

namespace BCScnMgr.Actor
{
    public class BCScnConn : BaseServerClientActor
    {
        private ISysAkkaManager _akkaManager;
        private IActorRef _rcvEditActor;
        private AggregateService _agService;

        private Dictionary<long, IActorRef> _dicSender = new Dictionary<long, IActorRef>();


        public BCScnConn(ISysAkkaManager akkaManager, AkkaSysIP akkaSysIp, AggregateService agService, ILog log) : base(akkaSysIp, log)
        {
            _agService = agService;
            _akkaManager = akkaManager;
            _rcvEditActor = akkaManager.GetActor(nameof(BCScnRcvEdit));


            Receive<CompareScnResult_SB01>(message => SndResult(message));


            //var t = new BarCodeScnContent("CE00100001", SKPOS.DeliveryTop);
            //InfoWMSScanID(t);

        }

        protected override void TCPConnected(Tcp.Connected message)
        {
            _log.I("TCP連線成功", " Connected. message=" + message.ToString());
            _log.I("TCP連線成功", " LocalAddress=" + message.LocalAddress.ToString());
            _log.I("TCP連線成功", " RemoteAddress=" + message.RemoteAddress.ToString());

            var key = Sender.Path.Uid;

            if (_dicSender.ContainsKey(key))
                _dicSender.Remove(key);

            _dicSender.Add(key, Sender);
            _dicSender[key].Tell(new Tcp.Register(Self));
        }
        protected override void TcpConnectionClosed(Tcp.ConnectionClosed message)
        {
            _log.I("TCP連線關閉", " Tcp.ConnectionClosed. message=" + message.ToString());
            _log.I("TCP連線關閉", " Message.Cause=" + message.Cause);
            _log.I("TCP連線關閉", " Message.IsAborted=" + message.IsAborted.ToString());
            _log.I("TCP連線關閉", " Message.IsConfirmed=" + message.IsConfirmed.ToString());
            _log.I("TCP連線關閉", " Message.IsErrorClosed=" + message.IsErrorClosed.ToString());
            _log.I("TCP連線關閉", " Message.IsPeerClosed=" + message.IsPeerClosed.ToString());

            var key = Sender.Path.Uid;

            if (_dicSender.ContainsKey(key))
                _dicSender.Remove(key);
        }
        protected override void TcpCommandFailed(Tcp.CommandFailed message)
        {
            _log.E("TCP操作失敗", " Tcp.CommandFailed. message=" + message.ToString());
            _log.E("TCP操作失敗", " Tcp.CommandFailed. message=" + message.ToString());
            _log.E("TCP操作失敗", " Cmd=" + message.Cmd.ToString());
            _log.E("TCP操作失敗", " Message=" + message.Cmd.FailureMessage);

            var key = Sender.Path.Uid;

            if (_dicSender.ContainsKey(key))
                _dicSender.Remove(key);
        }




        //private void InfoWMSScanID(BarCodeScnContent msg)
        //{

        //    _log.Info("通知WMS掃描ID", $"掃描位置為{msg.GetPOSStr()}鋼捲ID為{msg.ScanCoilNo}");
        //    var scanResult = new ScanResult(msg.GetScanPosition(), msg.ScanCoilNo);
        //    MQPoolUtil.SendToWMS(InfoWMS.InfoBCSScanID.Data(scanResult));
        //}


        protected override void TcpReceivedData(Tcp.Received msg)
        {
            base.TcpReceivedData(msg);
            //_log.I("TCP接收資料", "Handle_Tcp_Received. message=" + msg.ToString());
            //_log.I("TCP接收資料", "ByteString=" + msg.Data.ToString());
            //_log.I("TCP接收資料", "Count=" + msg.Data.Count.ToString());


            //_rcvEditActor = Context.ActorOf(Context.System.DI().Props<BCScnRcvEdit>());
            //_rcvEditActor.Tell(msg.Data.ToArray());
            _rcvEditActor.Forward(msg.Data.ToArray());


            //var bc = BCSFactory.ScanResult(true, "CH100100001");
            //var bytes = MsgAnalUtil.RawSerialize(bc);
            //_connection.Tell(Tcp.Write.Create(ByteString.FromBytes(bytes)));

        }



        private void SndResult(CompareScnResult_SB01 scanResult)
        {

            if (_connection == null)
            {
                _log.E("TCP發送失敗", "未連結BarCode機,請檢察連線");
                return;
            }
            var bytes = scanResult.RawSerialize();

            _agService.DumpSndRawData(bytes);

            _connection.Tell(Tcp.Write.Create(ByteString.FromBytes(bytes)));
        }
    }
}
