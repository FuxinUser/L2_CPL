using NLog;
using WatchDog.Interface;

namespace WatchDog.Model
{
    public class SysLog : ISysLog
    {
        private string _logName = string.Empty;
        private Logger _nLog = null;

        public SysLog(string logName)
        {
            _logName = logName;
        }

        public void Build()
        {
            _nLog = LogManager.GetLogger(_logName);

            D($"nLog 已就緒");
        }

        public void I(string msg) => _nLog.Info(msg);

        public void D(string msg) => _nLog.Debug(msg);

        public void W(string msg) => _nLog.Warn(msg);

        public void E(string msg) => _nLog.Error(msg);
    }
}
