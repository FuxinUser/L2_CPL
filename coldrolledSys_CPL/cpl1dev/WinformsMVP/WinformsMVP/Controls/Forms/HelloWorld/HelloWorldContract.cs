using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinformsMVP.Controls.Forms.HelloWorld
{
    public class HelloWorldContract
    {
        public interface IView : IBaseFormView<IPresenter>
        {
            void ShowMessageBox(string text);

            /// <summary>
            /// 更新Label (傳統更新)  (方法一)
            /// </summary>
            /// <param name="text"></param>
            void UpdateDisplayText(string text);

            /// <summary>
            /// 顯示文字，使用MVP架構進行更新 (方法二)
            /// </summary>
            string DisplayText { get; set; }

            /// <summary>
            /// 更新Label (使用SynchronizationContext更新)  (方法三)
            /// </summary>
            /// <param name="text"></param>
            void UpdateDisplayTextByContext(string text);
        }

        public interface IPresenter : IBaseFormPresenter
        {
            void Click();

            /// <summary>
            /// 執行緒更新UI的方法 (傳統更新)
            /// </summary>
            void UpdateUiLabel1();

            /// <summary>
            /// 執行緒更新UI的方法 (MVP架構更新)
            /// </summary>
            void UpdateUiLabel2();

            /// <summary>
            /// 執行緒更新UI的方法 (SynchronizationContext更新法)
            /// </summary>
            void UpdateUiLabel3();
        }

        public interface IModel : IBaseModel
        {
            string ShowText { get; }
        }
    }
}
