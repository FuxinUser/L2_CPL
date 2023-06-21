/**
 * Author: ICSC, 張恩碩
 * Date: 2019/6/28
 * Description: 
 * Reference: 
 * Modified: 
 */
using CoreLibrary.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinformsMVP
{
    public interface IBasePanelFormView<in P> where P : IBasePanelFormPresenter
    {
        /// <summary>
        /// 是否於前景顯示
        /// </summary>
        bool IsFrontView { get; }

        /// <summary>
        /// 取得PanelForm
        /// </summary>
        IPanelForm PanelForm { get; }
 
        /// <summary>
        /// 取得非同步的方式來更新UI
        /// </summary>
        IAsyncResult BeginInvoke(Delegate method);

        /// <summary>
        /// 同步的方式來更新UI
        /// </summary>        
        object Invoke(Delegate method);

        /// <summary>
        /// 設定Presenter
        /// </summary>
        /// <param name="presenter"></param>
        void SetPresenter(P presenter);        
    }
}
