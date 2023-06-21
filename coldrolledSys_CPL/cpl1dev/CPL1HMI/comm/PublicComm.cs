using Akka.Actor;
using Akka.Event;
using AkkaSysBase;
using Core.Help;
using LabelPrint.Printer;
using LogSender;
using System;
using System.Configuration;

namespace CPL1HMI
{
    class PublicComm
    {
        public static IActorRef Client = null;
        public static IActorRef lprSndEdit = null;
        public static ILoggingAdapter AkkaLog;
        public static ILoggingAdapter ClientLog;
        public static ILoggingAdapter ExceptionLog;
        //public static ILog log;
        public static void Start()
        {
            var akksSysName = ConfigurationManager.AppSettings["AkkaSystemName"];

            //var actorSystem = new Lazy<ActorSystem>(() => ActorSystem.Create(akksSysName));
            //var akkaManager = new SysAkkaManager(actorSystem);

            ActSystem.CreateSystem(akksSysName);
            ActSystem.SetAsClient();
            ActSystem.SetConnectionString(GlobalVariableHandler.Instance.strConn_CPL); 

            AkkaLog = Logging.GetLogger(ActSystem._actSystem, "Akkalog");
            AkkaLog.Info("akkalog start.....");
            
            ClientLog = Logging.GetLogger(ActSystem._actSystem, "HmiLog");
            ClientLog.Info("hmilog start....");
            
            ExceptionLog = Logging.GetLogger(ActSystem._actSystem, "ExceptionLog");
            ExceptionLog.Info("hmilog start....");
 
            Client = ActSystem._actSystem.ActorOf(Props.Create(() => new HMIClient(ClientLog)), "hmiclient");

            string strRemoteIp = GlobalVariableHandler.Printer_IP;// IniSystemHelper.Instance.PrinterRemoteIP;
            int intRemotePort =  GlobalVariableHandler.Printer_Port;//IniSystemHelper.Instance.PrinterRemotePort;
            var zebra = new Zebra(strRemoteIp, intRemotePort);
            lprSndEdit = ActSystem._actSystem.ActorOf(Props.Create(() => new PinterClient(ClientLog, zebra )), "HMIPrinter"); 
        }


        // TODO : Wait實作
        public static void Stop()
        {

        }
    }
}
