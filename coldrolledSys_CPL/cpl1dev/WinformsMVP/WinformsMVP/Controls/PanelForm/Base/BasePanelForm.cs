using CoreLibrary.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/**
 * Author: ICSC, 張恩碩
 * Date: 2019/3/4
 * Description: 繼承PanelForm_Base
 * PanelForm_Base
 * Reference: 
 * Modified: 
 */
namespace WinformsMVP
{    
    public class BasePanelForm<P> : PanelForm, IBasePanelFormView<P> where P : IBasePanelFormPresenter
    {
        /// <summary>
        /// 是否於前景顯示
        /// </summary>
        public bool IsFrontView { get; private set; }

        /// <summary>
        /// Presenter的基底
        /// </summary>
        protected P Presenter { get; private set; }

        /// <summary>
        /// 取得PanelForm
        /// </summary>
        public IPanelForm PanelForm { get { return this; } }

        public BasePanelForm() { }

        protected virtual T GetPresenter<T>() where T : IBasePanelFormPresenter
        {
            return (T)(object)Presenter;
        }

        public void SetPresenter(P presenter)
        {
            Presenter = presenter;
        }

        #region --- PanelForm生命週期的覆寫 ---

        public override void OnCreate(IPanelFormContainer panelFormContainer)
        {
            base.OnCreate(panelFormContainer);
            if (Presenter != null) Presenter.OnCreate();
        }

        public override void OnPreStart(object data)
        {
            base.OnPreStart(data);
            if (Presenter != null) Presenter.OnPreStart(data);
        }

        /// <summary>
        /// 選擇性實作，請覆寫此方法加上override，
        /// 畫面已經在前景時。
        /// </summary>
        public override void OnStarted()
        {
            base.OnStarted();
            IsFrontView = true;
            if (Presenter != null) Presenter.OnStarted();
        }

        /// <summary>
        /// 選擇性實作，請覆寫此方法加上override，
        /// 畫面不在前景時，會觸發。
        /// </summary>
        public override void OnStopped()
        {
            base.OnStopped();
            if (Presenter != null) Presenter.OnStopped();
            IsFrontView = false;
        }
        #endregion
    }
}
