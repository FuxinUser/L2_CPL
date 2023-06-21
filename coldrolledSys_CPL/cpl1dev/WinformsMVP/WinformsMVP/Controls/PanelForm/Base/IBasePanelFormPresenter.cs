
using CoreLibrary.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/**
 * Author: ICSC, 張恩碩
 * Date: 2019/6/4
 * Description: 
 * Reference: 
 * Modified: 
 */
namespace WinformsMVP
{
    public interface IBasePanelFormPresenter 
    {
        #region --- PanelForm的生命週期 ---

        /// <summary>
        /// 註冊以及PanelForm，此事件只有在註冊時會觸發
        /// </summary>
        void OnCreate();

        /// <summary>
        /// 畫面即將在前景時
        /// 並接收傳遞的資料
        /// </summary>
        void OnPreStart(object data);

        /// <summary>
        /// 畫面已經在前景時
        /// 將資料和UI那些都呈現在畫面上
        /// </summary>
        void OnStarted();

        /// <summary>
        /// 畫面不在前景時，會觸發
        /// </summary>
        void OnStopped();
        #endregion
    }
}
