using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinformsMVP.Controls.Forms
{
    public class BaseForm : Form, IBaseFormView<IBaseFormPresenter>
    {
        private IBaseFormPresenter Presenter {  get; set; }

        public BaseForm()
        {
        }

        public void SetPresenter(IBaseFormPresenter presenter)
        {
            Presenter = presenter;             
        }

      
    }
}
