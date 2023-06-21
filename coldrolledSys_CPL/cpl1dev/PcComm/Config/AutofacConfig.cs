using Akka.Actor;
using Akka.Event;
using AkkaSysBase;
using Autofac;
using Controller.Sys;
using Core.Util;
using LogSender;
using PcComm.View;
using System;

namespace PcComm.Config
{
    public static class AutofacConfig
    {
        private static ContainerBuilder _builder = new ContainerBuilder();

        public static IContainer SysInject()
        {
            _builder.RegisterSelfSingletonProp<ISysController, SysController>();

            // 註冊App Setting       
            _builder.RegisterSelfSingleton<AppSetting>();


            // 註冊Form
            _builder.RegisterFrame<ClientCoordinatorContract.IPresenter, ClientCoordinatorPresenter, ClientCoordinatorContract.IView, ClientCoordinatorForm1>();


            // 註冊ActorSys
            _builder.Register(c =>
            {
                return ActorSystem.Create(c.Resolve<AppSetting>().AkkaSysName);
            }).SingleInstance();

            //註冊ActorManager
            _builder.RegisterSelfSingleton<ISysAkkaManager, SysAkkaManager>();

            //註冊MgrActor
            _builder.Register(c =>
            {
                return new PCcom(
                      c.Resolve<ISysAkkaManager>(),
                      c.Resolve<ISysController>(),
                      c.GetLog<PCcom>(a => a.MgrLog));
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
