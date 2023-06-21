using Controller.Sys;
using LogSender;
using NLog;
using System;
using WatchDog.Interface;

namespace WatchDog.Model
{
    public class MonitorCPL
    {
        private readonly ISysLog _log = null;
        private readonly ISysController _sysController;

        private IMonitor _monitor = null;

        public MonitorCPL(
            string[][] procItems,
            IMonitor monitor,
            ISysController sysController,
            ISysLog log = null)
        {
            _log = log;

            _sysController = sysController;

            _monitor = monitor;
            _monitor.AddProcItem(procItems);
            _monitor.SetApExistEvent(OnProcExist);
            _monitor.SetApNotExistEvent(OnProcNotExist);
        }

        public void Start() => _monitor.Start();

        public void Stop() => _monitor.Stop();

        private void OnProcExist(IProcWatcher watcher)
        {
            _log.D($"執行緒存在, Name={watcher.Name}, IsExist={watcher.IsExist}, PreIsExixt={watcher.PreIsExist}");

            try
            {
                _sysController.SaveAPStatusToL25(watcher.Name, "1");
            }
            catch (Exception e)
            {
                _log.E(e.Message);
                _log.E(e.StackTrace);
            }
        }

        private void OnProcNotExist(IProcWatcher watcher)
        {
            _log.D($"執行緒不存在, Name={watcher.Name}, IsExist={watcher.IsExist}, PreIsExixt={watcher.PreIsExist}");

            try
            {
                _sysController.SaveAPStatusToL25(watcher.Name, "0");
            }
            catch (Exception e)
            {
                _log.E(e.Message);
                _log.E(e.StackTrace);
            }
        }
    }
}
