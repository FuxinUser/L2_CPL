using Autofac;
using System.Diagnostics;
using System.Windows.Forms;
using WatchDog.Config;
using WatchDog.Interface;
using WatchDog.Model;

namespace WatchDog
{
    public class Bootstrapper
    {
        private readonly IContainer _container = AutofacConfig.Inject();

        public Bootstrapper() { }

        public void Run() => StartForm();

        public void StartForm()
        {
            //  建立 MainForm
            var form = new MainForm();
            form.Load += (o, e) => SetFormParameters(form);
            Application.Run(form);
        }

        private void SetFormParameters(MainForm form)
        {
            //  取得應用程式參數
            var monitorCpl = _container.Resolve<MonitorCPL>();

            //  建置 log
            _container.Resolve<ISysLog>().Build();

            //  設置 form 參數
            form.SetMonitor(monitorCpl);
            form.Text = Process.GetCurrentProcess().MainModule.ModuleName.Replace(".exe", "");
        }
    }
}
