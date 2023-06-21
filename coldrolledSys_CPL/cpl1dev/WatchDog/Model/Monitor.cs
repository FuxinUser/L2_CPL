using System;
using System.Collections.Generic;
using System.Timers;
using WatchDog.Interface;

namespace WatchDog.Model
{
    public class Monitor : IMonitor
    {
        private readonly IDictionary<string, Timer> _dicTimer = null;

        private Action<IProcWatcher> _actionOnTmer = null;
        private Action<IProcWatcher> _actionApExist = null;
        private Action<IProcWatcher> _actionApNotExist = null;

        public Monitor()
        {
            _dicTimer = new Dictionary<string, Timer>();
            _actionOnTmer = OnTimerDefault;
        }

        public void AddProcItem(params string[][] procItems)
        {
            foreach (var procItem in procItems)
            {
                var name = procItem[0];
                var path = procItem[1];

                SetTimer(name, path);
            }
        }

        public void SetOnTimerEvent(Action<IProcWatcher> action)
        {
            _actionOnTmer = action;
        }

        public void SetApExistEvent(Action<IProcWatcher> action)
        {
            _actionApExist = action;
        }

        public void SetApNotExistEvent(Action<IProcWatcher> action)
        {
            _actionApNotExist = action;
        }

        public void Start()
        {
            foreach (var keyVal in _dicTimer)
                keyVal.Value.Start();
        }

        public void Stop()
        {
            foreach (var keyVal in _dicTimer)
                keyVal.Value.Stop();
        }

        /// <summary>
        ///     設置計時器
        /// </summary>
        private void SetTimer(string name, string path)
        {
            if (!_dicTimer.ContainsKey(name))
            {
                //  設置要監控的執行緒模組
                var watcher = new ProcWatcher(name, path);

                //  建立 timer
                var timer = new Timer();
                timer.Interval = 10000;
                timer.Elapsed += (sender, e) => _actionOnTmer(watcher);

                //  添加到清單中
                _dicTimer.Add(name, timer);
            }
        }

        /// <summary>
        ///     計時器觸發時執行的動作
        /// </summary>
        /// <param name="watcher"> IProcWatcher </param>
        private void OnTimerDefault(IProcWatcher watcher)
        {
            watcher.ChkProcExist();

            var isExist = watcher.IsExist;
            var preIsExist = watcher.PreIsExist;

            //  狀態有變換時執行
            if (isExist != preIsExist)
            {
                if (isExist)
                    //  執行緒存在
                    _actionApExist?.Invoke(watcher);
                else
                    //  執行緒不存在
                    _actionApNotExist?.Invoke(watcher);
            }
        }
    }
}
