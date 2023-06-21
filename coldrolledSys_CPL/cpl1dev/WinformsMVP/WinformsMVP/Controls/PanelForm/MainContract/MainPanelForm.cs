using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinformsMVP
{
    public class MainView : BasePanelForm<MainContract.IPresenter>, MainContract.IView
    {
        public int Addend => throw new NotImplementedException();

        public int Add => throw new NotImplementedException();

        public int Answer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void DisplayMessageBox(string message)
        {
            throw new NotImplementedException();
        }
    }

    public partial class MainPanelForm : MainView
    {
        public MainPanelForm()
        {
            InitializeComponent();
        }

        //public int Addend { get { return int.Parse(Label_Added.Text); } }

        //public int Add { get { return int.Parse(Label_Add.Text); } }

        //public int Answer { get { return int.Parse(Label_Answer.Text); } set { Label_Answer.Text = value.ToString(); } }

        //public void DisplayMessageBox(string message)
        //{
        //    MessageBox.Show(message);
        //}

        private void Button_Cal_Click(object sender, EventArgs e)
        {
            Presenter.Add();
        }
    }
     
}
