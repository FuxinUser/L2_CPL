using System;

namespace WatchDog.Interface
{
    public interface IMonitor
    {
        /// <summary>
        ///     添加監控項目
        /// </summary>
        /// <param name="procItems"> 執行緒名稱 </param>
        void AddProcItem(params string[][] procItems);

        /// <summary>
        ///     設置 OnTimer 事件
        /// </summary>
        /// <param name="action"></param>
        void SetOnTimerEvent(Action<IProcWatcher> action);

        /// <summary>
        ///     設置執行緒存在時要觸發的事件
        /// </summary>
        /// <param name="action"> 委派事件 </param>
        void SetApExistEvent(Action<IProcWatcher> action);

        /// <summary>
        ///     設置執行緒不存在時要觸發的事件
        /// </summary>
        /// <param name="action"> 委派事件 </param>
        void SetApNotExistEvent(Action<IProcWatcher> action);

        /// <summary>
        ///     監控開始
        /// </summary>
        void Start();

        /// <summary>
        ///     監控停止
        /// </summary>
        void Stop();
    }
}
