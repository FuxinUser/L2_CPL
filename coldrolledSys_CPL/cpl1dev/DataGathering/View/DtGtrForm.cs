using Core.Define;
using Core.Help;
using System;
using WinformsMVP.Controls.Forms;
using WinformsMVP.Controls.Forms.Base;

namespace DataGathering.View
{
    public partial class DtGtrForm : BaseForm, DtGtrContract.IView
    {
        public DtGtrContract.IPresenter Presenter { get; set; }
        public void SetPresenter(DtGtrContract.IPresenter presenter)
        {
            InitializeComponent();
            Presenter = presenter;
            Presenter.SetView(this);
        }
        public DtGtrForm()
        {
            InitializeComponent();
        }



        private void checkBoxDebugLogOpen_Click(object sender, EventArgs e)
        {
       
        }

        private void DtGtrForm_Shown(object sender, EventArgs e)
        {
        }


        [Obsolete]
        public void SetPresenter(BaseFormContract.IPresenter presenter)
        {
            throw new NotImplementedException();
        }
    }
}
