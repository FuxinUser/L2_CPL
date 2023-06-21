using CoreLibrary.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinformsMVP
{
    public abstract class BasePanelFormPresenter<V, P>: IBasePanelFormPresenter
        where V : IBasePanelFormView<P>
        where P : IBasePanelFormPresenter
    { 
        /// <summary>
        /// 是否於前景顯示
        /// </summary>
        public bool IsFrontView { get { return View.IsFrontView; } }
         
        /// <summary>
        /// Form控制項的名子
        /// </summary>
        protected string FormName { get { return View.PanelForm.GetForm().Name; } }
 
        protected V View { get; private set; }

        public BasePanelFormPresenter(V view)
        {
            View = view;
            View.SetPresenter((P)(object)this);            
        }

        protected void Invoke(Action action)
        {
            View.Invoke(action);
        }

        #region --- PanelForm的生命週期 ---

        public virtual void OnCreate()
        {
            // 選擇性實作
        }

        public virtual void OnPreStart(object data)
        {
            // 選擇性實作
        }

        public virtual void OnStarted()
        {
            // 選擇性實作
        }

        public virtual void OnStopped()
        {
            // 選擇性實作
        }
        #endregion
    }
}
