using static WinformsMVP.Controls.Forms.Base.BaseFormContract;

namespace WinformsMVP.Controls.Forms
{
    public interface IBaseFormPresenter
    {
        /// <summary>
        /// 設定View
        /// </summary>
        void SetView(IView view);
    }
}
