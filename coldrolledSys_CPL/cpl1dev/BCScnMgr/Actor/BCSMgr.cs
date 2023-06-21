using AkkaSysBase;
using AkkaSysBase.Base;
using BCScnMgr.Actor;
using LogSender;

namespace BCScnMgr
{
    public class BCSMgr : BaseActor
    {
        public BCSMgr(ISysAkkaManager akkaManager, ILog log) : base(log)
        {
            akkaManager.CreateChildActor<BCScnConn>(Context);
            akkaManager.CreateChildActor<BCScnRcvEdit>(Context);
            akkaManager.CreateChildActor<BCScnSndEdit>(Context);
        }
    }
}
