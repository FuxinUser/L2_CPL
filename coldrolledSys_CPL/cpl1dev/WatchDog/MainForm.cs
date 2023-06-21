using NLog;
using System.Windows.Forms;
using WatchDog.Model;

namespace WatchDog
{
    public partial class MainForm : Form
    {
        private MonitorCPL _monitorCpl = null;

        public MainForm()
        {
            InitializeComponent();
        }

        public void SetMonitor(MonitorCPL monitorCapl)
        {
            _monitorCpl = monitorCapl;
            _monitorCpl.Start();
        }
    }
}
