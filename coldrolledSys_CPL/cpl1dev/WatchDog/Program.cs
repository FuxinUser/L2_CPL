using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace WatchDog
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            /**
             * 加入 Global\ 前綴字 :
             * 表示跨 session 唯一，即使用多個帳號透過Terminal Service登入系統，整台機器也只能執行一份
             */
            using (var mutex = new Mutex(false, $"Global\\{GetGuid()}"))
            {
                //  檢查是否同名 Mutex 已存在(表示另一份程式正在執行)
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show($"{Application.ProductName} 已開啟");
                    return;
                }

                /**
                 * Windows Form、Application.Run() 要包在 using Mutex 範圍內，
                 * 確保 Windows Form 執行期間 Mutex 一直存在
                 */

                //  啟動
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                new Bootstrapper().Run();
            }
        }

        /// <summary>
        ///     取得程式的 guid
        /// </summary>
        static string GetGuid()
        {
            var attr = typeof(Program).Assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0] as GuidAttribute;

            return attr.Value;
        }
    }
}
