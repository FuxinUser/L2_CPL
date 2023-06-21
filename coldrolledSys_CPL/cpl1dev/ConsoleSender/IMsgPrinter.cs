using ConsoleSender.Output;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSender
{
    public interface IMsgPrinter : IDisposable
    {
        /// <summary>
        /// 設定要輸出的Msg對象
        /// </summary>
        void AddOutput(IMsgOutputAdapter output);

        /// <summary>
        /// 移除要輸出的Msg對象
        /// </summary>
        void RemoveOutput(IMsgOutputAdapter output);

        /// <summary>
        /// 等待Msg Output內的資料都完成，時間單位為毫秒
        /// </summary>
        bool WaitOutputFinish(int timeoutMs = -1);

        /// <summary>
        /// 開啟或關閉Msg Print
        /// </summary>
        void SetEnableMsg(bool enableMsg);

        /// <summary>
        /// 設定要輸出的Msg Tag
        /// </summary>
        void SetMsgTag(int tag);

        /// <summary>
        /// 清除要輸出的Msg對象
        /// </summary>
        void ClearOutput();

        /// <summary>
        /// Msg輸出
        /// </summary>
        void Msg(string dateTime, string tag, string message, Exception e, StackFrame stackFrame);
    }
}
