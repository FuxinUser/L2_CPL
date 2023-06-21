using Akka.Actor;
using Akka.Event;
using Core.Help;
using Core.Util;
using DBService.Repository.EventLog;
using LogSender;
using MSMQ;
using System;


/**
 * Author: ICSC SPYUA
 * Date:2019/12/30
 * Desc: Log Thread (純記DB Log)
 **/
namespace LogRecord.Actor
{
    public class LogMgr : ReceiveActor
    {
        private IActorRef _self;
        private string _connectionStr;      
        private EventLogRepo _eventLogRepo;
   

        public LogMgr(ILoggingAdapter log) 
        {           
            _self = ActSystem.GetDicCtrl(typeof(LogMgr).Name);

            _connectionStr = IniSystemHelper.Instance.DBConn;
            _eventLogRepo = new EventLogRepo(_connectionStr);

            MQPool.GetMQ($"{nameof(LogMgr)}").Receive(x =>
            {
                var msg = (x as MQPool.MQMessage).Data;
                _self.Tell(msg);
            });

            Receive<EventLogEntity.TBL_EventLog>(message => {

                if (message.Event_Type.Equals(LogDef.DEBUG))
                    return;

                SaveLogToDB(message);

            });
         
        }


      
        public void SaveLogToDB(EventLogEntity.TBL_EventLog log)
        {
            try
            {
                var insertNum = _eventLogRepo.Insert(log);
                if (insertNum > 0)
                    ColsoleOut.show(log);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.CleanInvalidChar());
            }
           
        }

    }
}
