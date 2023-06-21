using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinformsMVP
{
    public class MainContract
    {
        public interface IView : IBasePanelFormView<IPresenter>
        {
            /// <summary>
            /// 被加數
            /// </summary>
            int Addend { get; }

            /// <summary>
            /// 加數
            /// </summary>
            int Add { get; }

            /// <summary>DD
            /// 結果
            /// </summary>
            int Answer { get; set; }

            void DisplayMessageBox(string message);
        }


        public interface IPresenter : IBasePanelFormPresenter
        {
            /// <summary>
            /// 執行加法
            /// </summary>
            void Add();             
        }
    }
}
