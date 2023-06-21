﻿using Akka.Actor;
using AkkaSysBase;
using Autofac;
using CoilManager.Config;
using CoilManager.View;
using Core.Util;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CoilManager
{
    public class Bootstrapper
    {
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
            akkManager.CreateActor<CoilMgr>();
        }

        private Form CreateMainForm()
        {
            var form = _sysContainer.Resolve<CoilProcessForm>();
            return form;
        }
    }
}
