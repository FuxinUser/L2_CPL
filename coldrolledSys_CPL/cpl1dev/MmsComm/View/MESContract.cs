﻿using WinformsMVP.Controls.Forms.Base;

/**
 * Author: ICSC余士鵬
 * Date: 2019/10/09
 * Description:  MES View 與 Presenter的溝通管道
 * Reference: 
 * Modified: 
 */

namespace MMSComm.View
{
    public class MESContract
    {
        public interface IView : BaseFormContract.IView
        {
        
        }

        public interface IPresenter : BaseFormContract.IPresenter
        {
          
        }

    }
}
