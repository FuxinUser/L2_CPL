using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSender.Format
{
    public interface IFormatStrategy<T>
    {
        T GetData(MsgStruct msgStruct);
        T GetData(string dateTime, string tag, string message, Exception e, StackFrame stackFrame, int threadID);
    }
}
