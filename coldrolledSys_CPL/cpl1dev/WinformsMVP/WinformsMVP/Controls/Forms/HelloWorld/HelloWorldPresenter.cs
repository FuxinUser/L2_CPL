using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformsMVP.Controls.Forms.HelloWorld
{
    /// <summary>
    /// 使用完整的MVP架構
    /// </summary>
    public class HelloWorldPresenter :
        BaseFormPresenter<HelloWorldContract.IModel,
                          HelloWorldContract.IView,
                          HelloWorldContract.IPresenter>,
        HelloWorldContract.IPresenter
    {
        public HelloWorldPresenter(HelloWorldContract.IView view, HelloWorldContract.IModel model) : base(view, model)
        {
        }

        public void Click()
        {
            View.ShowMessageBox(Model.ShowText);
        }

        public void UpdateUiLabel1()
        {
            Task.Factory.StartNew(() =>
            {
                View.UpdateDisplayText("Thread更新UI的第一種方式");
            });
        }

        public void UpdateUiLabel2()
        {
            Task.Factory.StartNew(() =>
            {
                Invoke(() => View.DisplayText = "Thread更新UI的第二種方式");
            });
        }

        public void UpdateUiLabel3()
        {
            Task.Factory.StartNew(() =>
            {
                View.UpdateDisplayTextByContext("Thread更新UI的第三種方式");
            });
        }

        protected override void View_Load(object sender, EventArgs e)
        {
            // 開啟視窗
        }
    }
}
