using Core.Util;
using LogSender;
using MSMQ;
using MSMQ.Core.MSMQ;
using System;
using static DataModel.HMIServerCom.Msg.SCCommMsg;

namespace Controller
{
    public static class MQPoolService
    {
        private static ILog log = new Log();
        
        public static void PushHMI(string eventMsg, string content = "")
        {
            var message = InfoHMI.EventPush.Data(new SC03_EventPush(eventMsg, content));

            try
            {
                SendToPCCom(message);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message.CleanInvalidChar());
            }
        }

        public static void SendToWMS(MQPool.MQMessage message)
        {
            try
            {
                MQPool.SendToWMS(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message.CleanInvalidChar());
            }
            
        }
        public static void SendToTrk(MQPool.MQMessage message)
        {
            try
            {
                MQPool.SendToTrk(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message.CleanInvalidChar());
            }           
        }
        public static void SendToL1(MQPool.MQMessage message)
        {
            try
            {
                MQPool.SendToL1(message.ID, message.Data);
            }catch(Exception e)
            {
                log.E("MSMQ", message.ID + " "+ e.Message.CleanInvalidChar());
            }
            
        }
        public static void SendToMMS(MQPool.MQMessage message)
        {
            try
            {
                MQPool.SendToMMS(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message.CleanInvalidChar());
            }           
        }
        public static void SendToCoil(MQPool.MQMessage message)
        {

            try
            {
                MQPool.SendToCoil(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message.CleanInvalidChar());
            }            
        }
        public static void SendToPCCom(MQPool.MQMessage message)
        {
            try
            {
                 MQPool.SendToPCCom(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message.CleanInvalidChar());
            }       
        }

       
        public static void SendToDtGtr(MQPool.MQMessage message)
        {
            try
            {
                MQPool.SendToDtGtr(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message.CleanInvalidChar());
            }
            
        }
        public static void SendToDtStp(MQPool.MQMessage message)
        {
            try
            {
                MQPool.SendToDtStp(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message.CleanInvalidChar());
            }
            
        }

        public static void SendToDtProGtr(MQPool.MQMessage message)
        {
            try
            {
                MQPool.SendToDtProGtr(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message.CleanInvalidChar());
            }

        }

        public static void SendToLog(MQPool.MQMessage message)
        {
            try
            {
                MQPool.SendToLog(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message.CleanInvalidChar());
            }
            
        }
        public static void SendToBCsn(MQPool.MQMessage message)
        {
            try
            {
                MQPool.SendToBCsn(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message.CleanInvalidChar());
            }
            
        }
        public static void SendToLpr(MQPool.MQMessage message)
        {
            try
            {
                MQPool.SendToLpr(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message.CleanInvalidChar());
            }
            
        }


        public static void SendToBCScnRcvEdit(MQPool.MQMessage message)
        {
            try
            {
                MQPool.SendToBCScnRcvEdit(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message.CleanInvalidChar());
            }
            
        }
    }
}
