using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSender
{
    public struct MsgStruct
    {
        /// <summary>
        /// 時間(當下時間)
        /// </summary>
        public string Datetime;

        /// <summary>
        /// Tag分類
        /// </summary>
        public string Tag;

        /// <summary>
        /// 訊息
        /// </summary>
        public string Message;

        public Exception E;

        public StackFrame StackFrame;

        public int ThreadID;

        /// <summary>
        /// 發送Log訊息的總數
        /// </summary>
        public int MsgOutputCount;

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("{0}: {1}, ", "DateTime", Datetime);
            stringBuilder.AppendFormat("{0}: {1}, ", "Tag", Tag);
            stringBuilder.AppendFormat("{0}: {1}, ", "Message", Message);         
            return stringBuilder.ToString(); ;
        }

    }
}
