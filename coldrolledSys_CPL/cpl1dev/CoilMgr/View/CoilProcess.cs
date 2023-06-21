using Core.Define;
using Core.Help;
using System;
using WinformsMVP.Controls.Forms;
using WinformsMVP.Controls.Forms.Base;

/**
* Author: ICSC 余士鵬
* Date: 2019/10/04
* Description: COIL UI
* Reference: 
* Modified: 
*/
namespace CoilManager.View
{
    public partial class CoilProcessForm : BaseForm, CoilProcessContract.IView
    {
        public CoilProcessContract.IPresenter Presenter { get; set; }
        public void SetPresenter(CoilProcessContract.IPresenter presenter)
        {
            InitializeComponent();
            Presenter = presenter;
            Presenter.SetView(this);
        }


        public CoilProcessForm()
        {
            InitializeComponent();
        }

        private void CoilProcess_Shown(object sender, EventArgs e)
        {
           
        }

      
     
        // 開啟Debug Log
        private void checkBoxDebugLogOpen_Click(object sender, EventArgs e)
        {
           
        }

        public void SetPresenter(BaseFormContract.IPresenter presenter)
        {
            throw new NotImplementedException();
        }
    }
}
