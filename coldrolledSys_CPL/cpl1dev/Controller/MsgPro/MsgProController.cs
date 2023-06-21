using Core.Define;
using Core.Util;
using DataModel.Common;
using DBService.MMSWMSRepository;
using DBService.UnitOfWork;
using LogSender;
using System;
using System.Text;

namespace Controller.MsgPro
{
    public class MsgProController : IMsgProController
    {


        private ILog logger;
        private DapperDBContext _hisDBContext;

        public MsgProController()
        {
            _hisDBContext = new DapperDBContext(DBParaDef.HisDBConn);

        }
   
        public void SetLog(ILog log)
        {
            logger = log;
        }


        #region -- Msg Pro --

        public bool CreateMsgToL1HistoryDB(string tableName, object data)
        {
            try
            {
                var insertNum = _hisDBContext.Create(tableName, data);
                logger.D($"存取至{tableName} : {insertNum > 0}", "收發報文歷史存取至資料庫");
                return insertNum > 0;
            }
            catch (Exception e)
            {
                logger.E($"存取至{tableName} : False", e.Message.CleanInvalidChar());
                throw;
            }

        }

        public bool CreateMMSWMSMsg(string tableName, CommonMsg data)
        {

            var rcvMsg = new MMS_WMS_MsgRecord
            {
                Header = data.Message_Id,
                Length = data.Data.Length.ToString(),
                Data = Encoding.UTF8.GetString(data.Data),
                IsAck = data.IsAck ? "1" : "0",
                CreateTime = DateTime.Now

            };

            try
            {
                var insertNum = _hisDBContext.Create(tableName, rcvMsg);
                return insertNum > 0;
            }
            catch (Exception e)
            {
                logger.E($"存取至{tableName} : False", e.Message.CleanInvalidChar());
                throw;
            }
        }

        #endregion

    }
}
