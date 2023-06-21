using WinformsMVP.Controls.Forms.Base;

/**
 * Author: ICSC余士鵬
 * Date: 2019/10/25
 * Description:  Presenter的溝通管道
 * Reference: 
 * Modified: 
 */

namespace LabelPrint.View
{
    public class LprContract
    {
        public interface IView : BaseFormContract.IView
        {
            void ShowNumber(int number);
        }
        public interface IPresenter : BaseFormContract.IPresenter
        {
            void ContNumber(int cnt);
        }

    }
}
