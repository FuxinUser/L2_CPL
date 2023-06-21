using CoreLibrary.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinformsMVP.Controls.Forms.HelloWorld
{
    public partial class HelloWorldForm : BaseForm, HelloWorldContract.IView
    {
        /// <summary>
        /// 使用SynchronizationContext進行UI更新，可以封裝此類別
        /// </summary>
        private SynchronizationContext UiContext { get; set; }
        public HelloWorldContract.IPresenter Presenter { get; set; }

     

        public void SetPresenter(HelloWorldContract.IPresenter presenter)
        {
            Presenter = presenter;
            base.SetPresenter(presenter);
        }

        public HelloWorldForm()
        {
            InitializeComponent();

            // 請在UI Thread時，呼叫SynchronizationContext.Current來取得Context
            UiContext = SynchronizationContext.Current;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Presenter.Click();
        }

        public void ShowMessageBox(string text)
        {
            MessageBox.Show(text);
        }

        /// <summary>
        /// 將傳統更新UI的方式，封裝到ControlUtils內中 (方法一)
        /// 參考方法如下
        /// <see cref="ControlUtils.InvokeIfRequired(Control, MethodInvoker)"/>
        /// </summary>
        public void UpdateDisplayText(string text)
        {
            this.InvokeIfRequired(() => label1.Text = text);
        }

        /// <summary>
        /// MPV架構的更新方法，在外部呼叫View.Invoke(() => View.DisplayText = xxxx); 進行文字更新 (方法二)
        /// 參考方法如下
        /// <see cref="BaseFormPresenter{V, P}.Invoke(Action)"/>
        /// </summary>
        public string DisplayText { get => label1.Text; set => label1.Text = value; }

        /// <summary>
        /// 使用呼叫SynchronizationContext更新UI (方法三)
        /// </summary>        
        public void UpdateDisplayTextByContext(string text)
        {
            UiContext.Send((o) =>
            {
                var data = o as string;
                label1.Text = data;
            }, text);
        }
 
        private void UpdateUiLabel1Clicked(object sender, EventArgs e)
        {
            Presenter.UpdateUiLabel1();
        }

        private void UpdateUiLabel2Clicked(object sender, EventArgs e)
        {
            Presenter.UpdateUiLabel2();
        }

        private void UpdateUiLabel3Clicked(object sender, EventArgs e)
        {
            Presenter.UpdateUiLabel3();
        }

      
    }
}
