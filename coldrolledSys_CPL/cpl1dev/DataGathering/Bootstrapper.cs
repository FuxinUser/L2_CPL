using Akka.Actor;
using AkkaSysBase;
using Autofac;
using Core.Util;
using DataGathering.Actor;
using DataGathering.Config;
using DataGathering.View;
using DataSetup.Actor;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace DataGathering
{
    public class Bootstrapper
    {
        private readonly IContainer _sysContainer = AutofacConfig.SysInject();

        private Timer Timer { get; set; }

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


        // 啟動程式
        public void Run()
        {
            StarFormApp();
        }

        private void StarFormApp()
        {
            // Create Form
            var form = CreateMainForm();
            form.FormClosing += (o, e) =>
            {
                ProcessUtils.KillProcessAndChildren(Process.GetCurrentProcess().Id);
            };
            form.Load += (o, e) => StarAkkaSys();
            Application.Run(form);
        }

        private void StarAkkaSys()
        {
            // Create Actor Sys
            var akkaSys = _sysContainer.Resolve<ActorSystem>();
            akkaSys.UseAutofac(_sysContainer);
            // Create Actor Manager
            var akkManager = _sysContainer.Resolve<ISysAkkaManager>();
            akkManager.CreateActor<DtGtrMgr>();
            akkManager.CreateActor<DtProGtr>();
        }

        private Form CreateMainForm()
        {
            var form = _sysContainer.Resolve<DtGtrForm>();
            return form;
        }
    }
}
