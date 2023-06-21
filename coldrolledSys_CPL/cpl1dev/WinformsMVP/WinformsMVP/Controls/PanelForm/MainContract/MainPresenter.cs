using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinformsMVP
{
    public class MainPresenter : BasePanelFormPresenter<MainContract.IView, MainContract.IPresenter>, MainContract.IPresenter
    {
        public MainPresenter(MainContract.IView view) : base(view)
        {
        }

        public void Add()
        {
            View.Answer = View.Addend + View.Add;
        }
    }
}
