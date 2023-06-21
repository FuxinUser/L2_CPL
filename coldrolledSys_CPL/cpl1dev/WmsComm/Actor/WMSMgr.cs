using AkkaSysBase;
using AkkaSysBase.Base;
using LogSender;

namespace WMSComm.Actor
{
    public class WMSMgr : BaseActor
    {
        public WMSMgr(ISysAkkaManager akkaManager, ILog log) : base(log)
        {
            log.D("創建AThread", "Create WMSRCV App");
            akkaManager.CreateChildActor<WMSRcv>(Context);

            log.D("創建AThread", "Create WMSRcvEdit App");
            akkaManager.CreateChildActor<WMSRcvEdit>(Context);

            log.D("創建AThread", "Create WMSSnd App");
            akkaManager.CreateChildActor<WMSSnd>(Context);

            log.D("創建AThread", "Create WMSSndEdit App");
            akkaManager.CreateChildActor<WMSSndEdit>(Context);
        }

    }
}
