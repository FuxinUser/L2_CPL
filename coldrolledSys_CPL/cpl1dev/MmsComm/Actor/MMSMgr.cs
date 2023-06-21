using AkkaSysBase;
using AkkaSysBase.Base;
using LogSender;

namespace MMSComm.Actor
{
    public class MMSMgr : BaseActor
    {
        public MMSMgr(ISysAkkaManager akkaManager, ILog log) : base(log)
        {

            log.D("Create Child Actor", "Create RCV App");
            akkaManager.CreateChildActor<MMSRcv>(Context);

            log.D("Create Child Actor", "Create RcvEdit App");
            akkaManager.CreateChildActor<MMSRcvEdit>(Context);

            log.D("Create Child Actor", "Create Snd App");
            akkaManager.CreateChildActor<MMSSnd>(Context);

            log.D("Create Child Actor", "Create SndEdit App");
            akkaManager.CreateChildActor<MMSSndEdit>(Context);
        }


    }
}
