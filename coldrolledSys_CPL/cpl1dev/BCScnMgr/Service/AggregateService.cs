using BCScnMgr.Config;
using Core.Help.DumpRawDataHelp;

namespace BCScnMgr.Service
{
    public class AggregateService
    {
        public IDumpRawData DumpRawData { get; set; }

        public AppSetting appSetting { get; set; }
        
        public AggregateService()
        {

        }

        public void DumpRcvRawData(byte[] msg)
        {
            try
            {
                if (appSetting.DumpRcvMsgSwitchOn)
                    DumpRawData.DumpMsg(msg, appSetting.RcvMsgFilePath);
            }
            catch
            {
                throw;
            }
        }

        public void DumpSndRawData(byte[] msg)
        {
            try
            {
                if (appSetting.DumpSndMsgSwitchOn)
                    DumpRawData.DumpMsg(msg, appSetting.SndMsgFilePath);
            }
            catch
            {
                throw;
            }
        }

        public void DumpFailRawData(byte[] msg)
        {
            try
            {
                if (appSetting.DumpFailMsgSwitchOn)
                    DumpRawData.DumpMsg(msg, appSetting.FailMsgFilePath);
            }
            catch
            {
                throw;
            }
        }

        public void DumpDebugRawData(byte[] msg)
        {
            try
            {
                if (appSetting.DumpDebugMsgSwitchOn)
                    DumpRawData.DumpMsg(msg, appSetting.DebugFilePath);
            }
            catch
            {
                throw;
            }
        }

    }
}
