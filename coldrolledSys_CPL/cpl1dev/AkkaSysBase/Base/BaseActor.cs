using Akka.Actor;
using System;
using LogSender;

/**
 * Author: ICSC余士鵬
 * Date: 2019/9/19
 * Description: Akka底層(角色生命週期)
 * Reference: 
 * Modified: 
 */
namespace AkkaSysBase.Base
{
    public class BaseActor : ReceiveActor
    {
        protected ILog _log;   
        private string _actorName;

        public BaseActor(ILog log)
        {
            _log = log;
            _actorName = Context.Self.Path.Name;
        }
        protected override void PreStart()
        {
            _log.I("Actor PreStart", _actorName + "PreStart");
            base.PreStart();         
        }
        protected override void PreRestart(Exception reason, object message)
        {
            _log.I("Actor PreRestart", _actorName + " PreRestart");
            _log.I("Actor PreRestart", "Reason:" + reason.Message);
            base.PreRestart(reason, message);
        }
        protected override void PostStop()
        {
            _log.A("Actor PostStop", _actorName + " PostStop");
            base.PostStop();

        }
        protected override void PostRestart(Exception reason)
        {
            _log.E("Actor PostRestart", _actorName + " PostRestart");
            _log.E("Actor PostRestart", "Reason:" + reason.Message);
            base.PostRestart(reason);
        }

        /// <summary>
        ///     Try action of flow
        /// </summary>
        protected virtual void TryFlow(Action action, bool @throw = false)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception ex)
            {
                _log.E("Try Flow Expection", $"ex.Message={ex.Message}");
                _log.E("Try Flow Expection", $"ex.StackTrace={ex.StackTrace}");
                if (@throw) throw;
            }
            finally
            {
            }
        }
    }
}
