using DataModel.Common;
using LogSender;

namespace Controller.MsgPro
{
    public interface IMsgProController
    {
        bool CreateMsgToL1HistoryDB(string tableName, object data);

        void SetLog(ILog log);
        bool CreateMMSWMSMsg(string tableName, CommonMsg data);
    }
}
