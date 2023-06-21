using DBService.Repository.EventLog;
using LogRecord.View;

namespace LogRecord
{
    public class ColsoleOut
    {
        public static LogContract.IPresenter _CoilPresenter;

        public static void show(EventLogEntity.TBL_EventLog item)
        {
            _CoilPresenter.InsertLog(item);
        }
    }
}
