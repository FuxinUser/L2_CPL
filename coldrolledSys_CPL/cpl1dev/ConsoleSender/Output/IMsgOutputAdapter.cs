using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSender.Output
{

    /// <summary>
    /// Msg傳輸的監控，用來監聽收、送的訊息
    /// </summary>
    /// <param name="isReceive">true: 收、false: 送</param>
    /// <param name="message">訊息</param>
    public delegate void TransportMonitor(bool isReceive, string message);

    public interface IMsgOutputAdapter : IDisposable
    {
        /// <summary>
        /// Msg傳輸監控的事件，用來監聽收、送的訊息
        /// </summary>
        event TransportMonitor MsgMonitorHandler;

        // 輸出的Msg
        void Msg(string dateTime, string tag, string message, Exception e,StackFrame stackFrame, int threadID);

        /// <summary>
        /// 輸出的Log是否為空
        /// </summary>
        bool IsOutputLogEmpty { get; }

    }
}
