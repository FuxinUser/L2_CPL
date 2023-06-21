using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSender.Format
{
    public class MsgConsoleStarategy : IFormatStrategy<MsgStruct>
    {

        private MsgStruct Msg;

        public MsgStruct GetData(string dateTime, string tag, string message, Exception e, StackFrame stackFrame,  int threadID)
        {
            //

            return Msg;
        }

        public MsgStruct GetData(MsgStruct msgStruct)
        {
            return GetData(msgStruct.Datetime, msgStruct.Tag, msgStruct.Message, msgStruct.E, msgStruct.StackFrame, msgStruct.ThreadID);
        }
    }
}
