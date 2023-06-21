using ConsoleSender.Output;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace ConsoleSender
{
    public class MsgPrinter : IMsgPrinter
    {
        private static readonly object LOCK = new object();
        private int MsgTag { get; set; }
        private bool EnableMsg { get; set; }
        private List<IMsgOutputAdapter> OutputAdapters { get; set; }

        public MsgPrinter()
        {
            MsgTag = 0;
            EnableMsg = true;
            OutputAdapters = new List<IMsgOutputAdapter>();
        }

        public void Msg(string dateTime, string tag, string message, Exception e, StackFrame stackFrame)
        {
            lock (LOCK)
            {
                if (!EnableMsg) return;

                OutputAdapters.ForEach(logOutput =>
                {
                    logOutput.Msg(dateTime, tag, message, e, stackFrame, Thread.CurrentThread.ManagedThreadId);
                });
            }
        }

        public void AddOutput(IMsgOutputAdapter output)
        {
            OutputAdapters.Add(output);
        }

        public void RemoveOutput(IMsgOutputAdapter output)
        {
            OutputAdapters.Remove(output);
        }

        public bool WaitOutputFinish(int timeoutMs = -1)
        {
            EnableMsg = false;
            return SpinWait.SpinUntil(() =>
            {
                for (; ; )
                {
                    var finish = OutputAdapters.Count;
                    foreach (var logOutput in OutputAdapters)
                    {
                        if (logOutput.IsOutputLogEmpty) --finish;
                    }

                    // 需要釋放資源，否則造成UI Thread Blocking
                    Application.DoEvents();
                    Thread.Sleep(1);
                    if (finish == 0) break;
                }


                return true;
            }, timeoutMs);
        }

        public void SetEnableMsg(bool enableMsg)
        {
            EnableMsg = enableMsg;
        }

        public void SetMsgTag(int tag)
        {
            MsgTag = tag;
        }

        public void Dispose()
        {
            EnableMsg = false;
            foreach (var logOutput in OutputAdapters)
            {
                logOutput.Dispose();
            }

            ClearOutput(); throw new NotImplementedException();
        }

        public void ClearOutput()
        {
            OutputAdapters.Clear();
        }
    }
}
