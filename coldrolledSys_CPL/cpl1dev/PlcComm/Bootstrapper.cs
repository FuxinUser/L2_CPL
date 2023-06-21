using Akka.Actor;
using AkkaSysBase;
using Autofac;
using Core.Util;
using PLCComm.Actor;
using PLCComm.Config;
using PLCComm.View;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PLCComm
{
    public class Bootstrapper
    {
        // DI 管理器
        private readonly IContainer _sysContainer = AutofacConfig.SysInject();

        public Bootstrapper()
        {
            ConfigureServices();
        }

        private void ConfigureServices()
        {
            // 初始化應用程式的錯誤服務訊息
            var applicationException = new ApplicationExceptionUtil(AppSetting.Instance.AkkaSysName, AppSetting.Instance.CrashLogPath);
            Application.ThreadException += applicationException.Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += applicationException.CurrentDomain_UnhandledException;
        }

        /// <summary>
        /// 啟動程式
        /// </summary>
        public void Run()
        {
            StarFormApp();
        }

        /// <summary>
        /// 啟用WindwForm
        /// </summary>
        private void StarFormApp()
        {

            var form = CreateMainForm();
            form.FormClosing += (o, e) =>
            {
                ProcessUtils.KillProcessAndChildren(Process.GetCurrentProcess().Id);
            };
            form.Load += (o, e) => StarAkkaSys();
            Application.Run(form);
        }

        /// <summary>
        /// 建立與外部連結系統並啟動
        /// </summary>
        private void StarAkkaSys()
        {
            // 建立與外部連結系統
            var akkaSys = _sysContainer.Resolve<ActorSystem>();

            // 註冊物件
            akkaSys.UseAutofac(_sysContainer);

            // 建立Manager系統
            var akkManager = _sysContainer.Resolve<ISysAkkaManager>();
            akkManager.CreateActor<PlcMgr>();
        }

        private Form CreateMainForm()
        {
            var form = _sysContainer.Resolve<PlcCommForm>();
            return form;
        }
    }
}
