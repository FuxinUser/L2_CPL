using System;
using System.Diagnostics;
using System.Linq;
using WatchDog.Interface;

namespace WatchDog.Model
{
    public class ProcWatcher : IProcWatcher
    {
        private bool _isNew = true;

        public string Name { get; private set; } = string.Empty;

        public string Path { get; private set; } = string.Empty;

        public Process Proc { get; private set; } = null;

        public bool IsExist { get; private set; } = false;

        public bool PreIsExist { get; private set; } = false;

        public ProcWatcher(string name, string path)
            => (Name, Path) = (name, path);

        public void Run()
        {
            Kill();

            try
            {
                Process.Start($"{Path}\\{Name}.exe");

                ChkProcExist();
            }
            catch
            {
                throw;
            }
        }

        public void Kill()
        {
            try
            {
                ChkProcExist();

                if (IsExist)
                    Proc?.Kill();
            }
            catch
            {
                throw;
            }
            finally
            {
                Proc = null;
            }
        }

        public void ChkProcExist()
        {
            Proc = GetProcess();

            var isExsit = Proc != null;

            if (_isNew)
            {
                IsExist = PreIsExist = isExsit;

                _isNew = false;
            }
            else
            {
                PreIsExist = IsExist;
                IsExist = isExsit;
            }
        }

        private Process GetProcess()
        {
            var procs = Process.GetProcessesByName(Name);

            if (procs == null || procs.Length <= 0)
                return null;
            
            if (procs.Length > 1)
                return procs.Where(x => x.MainModule.FileName.Contains(Path)).FirstOrDefault();

            return Proc = procs[0];
        }
    }
}
