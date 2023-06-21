using Akka.Actor;
using Akka.Event;
using Autofac;
using Core.Util;
using System;
using Controller.Track;
using Controller.Coil;
using CoilManager.View;
using AkkaSysBase;
using LogSender;
using Controller.Sys;

namespace CoilManager.Config
{
    public static class AutofacConfig
    {
        private static ContainerBuilder _builder = new ContainerBuilder();

        public static IContainer SysInject()
        {
            // 註冊Model
            _builder.RegisterSelfSingletonProp<ITrackingController, TrackingController>();
            _builder.RegisterSelfSingletonProp<ICoilController, CoilController>();
            _builder.RegisterSelfSingletonProp<ISysController, SysController>();

            // 註冊App Setting       
            _builder.RegisterSelfSingleton<AppSetting>();


            // 註冊Form
            _builder.RegisterFrame<CoilProcessContract.IPresenter, CoilProcessPresenter, CoilProcessContract.IView, CoilProcessForm>();


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
                return new CoilMgr(
                      c.Resolve<ISysAkkaManager>(),
                      c.Resolve<ITrackingController>(),
                      c.Resolve<ICoilController>(),
                      c.Resolve<ISysController>(),
                      c.GetLog<CoilMgr>(a => a.MgrLog));
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

            return new Log(LogDef.SysServer,typeof(T).Name, iLogAdapter);
        }
    }
}
