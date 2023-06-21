using Autofac;
using Controller.Sys;
using Core.Util;
using WatchDog.Interface;
using WatchDog.Model;

namespace WatchDog.Config
{
    public static class AutofacConfig
    {
        private static readonly ContainerBuilder _builder = new ContainerBuilder();

        public static IContainer Inject()
        {
            RegisterSetting();
            RegisterLog();
            RegisterModel();

            return _builder.Build();
        }

        /// <summary>
        ///     註冊設定檔
        /// </summary>
        private static void RegisterSetting()
        {
            _builder.RegisterSelfSingleton<AppSetting>();
        }

        /// <summary>
        ///     註冊 log
        /// </summary>
        private static void RegisterLog()
        {
            _builder.Register(c => new SysLog(
                    c.Resolve<AppSetting>().LogName)).As<ISysLog>().RegisterSelfSingleton();
        }

        /// <summary>
        ///     註冊模組
        /// </summary>
        private static void RegisterModel()
        {
            _builder.Register(c => new Monitor()).As<IMonitor>().RegisterSelfSingleton();
            _builder.Register(c => new SysController()).As<ISysController>().RegisterSelfSingleton();
            _builder.Register(c => new MonitorCPL(
                    c.Resolve<AppSetting>().ProcItems,
                    c.Resolve<IMonitor>(),
                    c.Resolve<ISysController>(),
                    c.Resolve<ISysLog>())).RegisterSelfSingleton();
        }
    }
}
