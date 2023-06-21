using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinformsMVP.Controls.Forms.Base
{
    public class BaseFormContract
    {

        public interface IView : IBaseFormView<IPresenter>
        {

        }


        public interface IPresenter : IBaseFormPresenter
        {

        }
    }
}
