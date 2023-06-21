using ConsoleSender.Output;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSender
{
    public class Msg
    {
        private static readonly IMsgPrinter MSG_PRINTER = new MsgPrinter();

        private const int STACK_INDEX = 2;

        public readonly static int VERBOSE = 1;
        public readonly static int DEBUG = 2;
        public readonly static int WARM = 3;
        public readonly static int ERROR = 4;
        public readonly static int NOTHING = 5;

        public static void SetMsgTag(int tag)
        {
            MSG_PRINTER.SetMsgTag(tag);
        }

        public static void AddOutput(IMsgOutputAdapter output)
        {
            MSG_PRINTER.AddOutput(output);
        }

        public static void RemoveOutput(IMsgOutputAdapter output)
        {
            MSG_PRINTER.RemoveOutput(output);
        }

        public static void Dispose()
        {
            MSG_PRINTER.Dispose();
        }

        public static void WaitOutputFinish(int timeoutMs = -1)
        {
            MSG_PRINTER.WaitOutputFinish(timeoutMs);
        }

        public static void Print(string tag, string message, params object[] args)
        {
            MSG_PRINTER.Msg("2019", tag, CheckStringFormat(message, args), null, null);
        }
    
        internal static string CheckStringFormat(string message, params object[] args)
        {
            return (args == null || args.Length == 0) ? message : string.Format(message, args);
        }

        internal static StackFrame GetStackFrame(int skipFrame = STACK_INDEX)
        {
            var stackTrace = new StackTrace(skipFrame, true);
            return stackTrace.GetFrame(0);
        }



    }
}
