using System.Diagnostics;

namespace WatchDog.Interface
{
    public interface IProcWatcher
    {
        /// <summary>
        ///     程式名稱
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     程式路徑
        /// </summary>
        string Path { get; }

        /// <summary>
        ///     System Process 資訊
        /// </summary>
        Process Proc { get; }

        /// <summary>
        ///     是否存在
        /// </summary>
        bool IsExist { get; }
        
        /// <summary>
        ///     前一次存在的狀態
        /// </summary>
        bool PreIsExist { get; }

        /// <summary>
        ///     啟動
        /// </summary>
        void Run();

        /// <summary>
        ///     強制結束執行續
        /// </summary>
        void Kill();

        /// <summary>
        ///     檢查程式是否存在
        /// </summary>
        void ChkProcExist();
    }
}
