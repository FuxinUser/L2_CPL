using Akka.Actor;
using Akka.Event;
using Akka.IO;
using AkkaSysBase;
using Autofac;
using Controller.MsgPro;
using Controller.Sys;
using Core.Help.DumpRawDataHelp;
using Core.Util;
using DataMod.Common;
using DataModel.Common;
using LogSender;
using PLCComm.Actor;
using PLCComm.Service;
using PLCComm.View;
using System;
using System.Net;
using static MsgStruct.L2L1Snd;


/**
 * Author: ICSC余士鵬
 * Date: 2019/9/19
 * Description: DI管理器(物件記憶體管理)
 * Reference: 
 * Modified: 
 */
namespace PLCComm.Config
{
    public static class AutofacConfig
    {

        private static ContainerBuilder _builder = new ContainerBuilder();

        public static IContainer SysInject()
        {
            //// 註冊程式使用相關Model
            _builder.RegisterSelfSingletonProp<PlcMsgTypeAndLengthDic>();
            _builder.RegisterSelfSingletonProp<Msg_299_L2ALIVE>();
            _builder.RegisterSelfSingletonProp<SendQueue<ByteString>>();

            _builder.RegisterPerLifetimeProp<ISysController, SysController>();
            _builder.RegisterPerLifetimeProp<IMsgProController, MsgProController>();
            _builder.RegisterSelfSingletonProp<IDumpRawData, DumpPlcMsg>();
            _builder.RegisterSelfSingletonProp<AggregateService>();

            // 註冊App Setting       
            _builder.RegisterSelfSingleton<AppSetting>();

            // 註冊SysIP
            _builder.Register(c =>
            {
                var appSetting = c.Resolve<AppSetting>();
                var akkaIpModel = new AkkaSysIP()
                {
                    LocalIpEndPoint = new IPEndPoint(IPAddress.Parse(appSetting.LocalIp), appSetting.LocalPort),
                    RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(appSetting.RemoteIp), appSetting.RemotePort),
                };
                return akkaIpModel;
            }).SingleInstance();
            // 註冊Form
            _builder.RegisterFrame<PlcCommContract.IPresenter, PlcCommPresenter, PlcCommContract.IView, PlcCommForm>();


            // 註冊Actor
            _builder.Register(c =>
            {
                return ActorSystem.Create(c.Resolve<AppSetting>().AkkaSysName);
            }).SingleInstance();

            //註冊ActorManager
            _builder.RegisterSelfSingleton<ISysAkkaManager, SysAkkaManager>();

            //註冊MgrActor
            _builder.Register(c =>
            {
                return new PlcMgr(c.Resolve<ISysAkkaManager>(), c.GetLog<PlcMgr>(a => a.MgrLog));
            }).PropertiesAutowired()
                .AsSelf()
                .InstancePerLifetimeScope();

            //註冊RcvActor
            _builder.Register(c =>
            {
                var d = c.Resolve<AggregateService>();
                return new PlcCom(
                    c.Resolve<ISysAkkaManager>(),
                    c.Resolve<ISysController>(),
                    c.Resolve<AkkaSysIP>(),
                    c.GetLog<PlcCom>(a => a.ComLog),
                    c.Resolve<AggregateService>()
                    );

            }).PropertiesAutowired()
                .AsSelf()
                 .InstancePerLifetimeScope();

            //註冊RcvEditActor
            _builder.Register(c =>
            {
                return new PlcRcvEdit(
                    c.Resolve<ISysController>(),
                    c.Resolve<IMsgProController>(),
                    c.GetLog<PlcRcvEdit>(a => a.RcvEditLog)
                    );

            }).PropertiesAutowired()
                .AsSelf()
                  .InstancePerLifetimeScope();


            //註冊SndEditActor
            _builder.Register(c =>
            {
                return new PlcSndEdit(
                    c.Resolve<ISysAkkaManager>(),
                    c.Resolve<IMsgProController>(),
                    c.GetLog<PlcSndEdit>(a => a.SndEditLog),
                    c.Resolve<AggregateService>()
                    );

            }).PropertiesAutowired()
               .AsSelf()
                 .InstancePerLifetimeScope();


            var container = _builder.Build();
            return container;
        }


        private static ILog GetLog<T>(this IComponentContext context, Func<AppSetting, string> func)
        {
            var akkySys = context.Resolve<ActorSystem>();
            var logFileName = func?.Invoke(context.Resolve<AppSetting>());
            var iLogAdapter = akkySys.GetLogger(logFileName);

            return new Log(LogDef.SysServer, typeof(T).Name, iLogAdapter);
        }
    }
}
