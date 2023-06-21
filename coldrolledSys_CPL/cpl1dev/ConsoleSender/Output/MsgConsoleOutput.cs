using ConsoleSender.Format;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinMessageLibrary;

namespace ConsoleSender.Output
{
    public class MsgConsoleOutput : IMsgOutputAdapter
    {
        public event TransportMonitor MsgMonitorHandler;

        private ConcurrentQueue<MsgStruct> MsgQueue { get; set; }

        private WindowsMessage<MsgStruct> WinLogMessage { get; set; }
     
        private IFormatStrategy<MsgStruct> FormatStrategy { get; set; }

        public string ChannelName { get; private set; }

        private bool StartSend { get; set; }

        public bool IsOutputLogEmpty
        {
            get
            {
                return (MsgQueue != null && MsgQueue.Count == 0);
            }
        }

        private Thread LoopThread { get; set; }

        public MsgConsoleOutput(IFormatStrategy<MsgStruct> formatStrategy, string channelName)
        {
            ChannelName = channelName;
            FormatStrategy = formatStrategy;

            StartSend = true;
            MsgQueue = new ConcurrentQueue<MsgStruct>();

            WinLogMessage = new WindowsMessage<MsgStruct>();
            WinLogMessage.RegisterChannel(ChannelName);

            LoopThread = new Thread(InitSendWinLogMessage);
            LoopThread.IsBackground = true;
            LoopThread.Start();
        }


        private void InitSendWinLogMessage()
        {
            while (StartSend)
            {
                if (MsgQueue.Count <= 0)
                {
                    Thread.Sleep(1);
                    continue;
                }

                if (MsgQueue.TryDequeue(out MsgStruct logStruct))
                {
                    var logMsg = FormatStrategy.GetData(logStruct);
                    logMsg.MsgOutputCount = MsgQueue.Count;

                    //WinLogMessage.SendToChannel(ChannelName, logMsg);
                    WinLogMessage.SendMessage(logMsg);

                    MsgMonitor(false, logMsg.ToString());
                }
            }
        }

        private void MsgMonitor(bool isReceived, string message)
        {
            if (MsgMonitorHandler != null) MsgMonitorHandler(isReceived, message);
        }

        public void Msg(string dateTime, string tag, string message, Exception e, StackFrame stackFrame, int threadID)
        {
            var msgStruct = new MsgStruct()
            {
                Datetime = dateTime,
                Tag = tag,
                Message = ConvertControlCharToEmpty(message),
                E = e,
                StackFrame = stackFrame,
                ThreadID = threadID
            };

            if (MsgQueue != null)
            {
                MsgQueue.Enqueue(msgStruct);
                MsgMonitor(true, msgStruct.ToString());
            }
        }

        public void Dispose()
        {
            StartSend = false;
            WinLogMessage.UnRegisterChannel(ChannelName);
            WinLogMessage.Dispose();
            WinLogMessage = null;
        }

        public bool IsOutputMsgEmpty
        {
            get
            {
                return (MsgQueue != null && MsgQueue.Count == 0);
            }
        }

        /// <summary>
        /// 把原本字串過濾掉控制字元
        /// </summary>
        /// <returns></returns>
        private string ConvertControlCharToEmpty(string input)
        {
            string output = "";

            // 查詢換行符號的所有出現的字串位置
            var newLineElements = AllIndexesOf(input, Environment.NewLine);
            for (int i = 0; i < input.Length; i++)
            {
                var asciiInt = Convert.ToInt32(input[i]);
                if (0 <= asciiInt && asciiInt <= 31)
                {
                    // 防止過濾掉段行符號
                    if (newLineElements.Count() != 0 && newLineElements.Contains(i))
                    {
                        // 將換行符號再次加入
                        output += Environment.NewLine;
                    }
                    continue;
                }

                output += input[i];  //就把合法的字元累加到output字串
            }
            return output;
        }

        /// <summary>
        /// 列舉出所有的Index位置，Index的位置是從0開始算起
        /// </summary>
        /// <param name="input">輸入的字串</param>
        /// <param name="searchValue">要查找的字串</param>
        /// <returns>回傳所有查找字串的位置</returns>
        private static IEnumerable<int> AllIndexesOf(string input, string searchValue)
        {
            var indexes = new List<int>();
            if (!string.IsNullOrEmpty(input) && !string.IsNullOrEmpty(searchValue))
            {
                if (input.IndexOf(searchValue) != -1)
                {
                    for (int index = 0; ; index += searchValue.Length)
                    {
                        index = input.IndexOf(searchValue, index);
                        if (index == -1) return indexes;
                        indexes.Add(index);
                    }
                }
            }

            return indexes;
        }
    }
}
