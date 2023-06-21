using CoreLibrary.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinformsMVP.Controls.Forms.HelloWorld;

namespace WinformsMVP
{
    public partial class MainFrame : Form
    {
        public MainFrame()
        {
            InitializeComponent();
        }

        private void MainFrame_Load(object sender, EventArgs e)
        {    
            var helloForm = new HelloWorldForm();
            var helloModel = new HelloWorldModel("My first Winform MVP Model");
            var helloPresenter = new HelloWorldPresenter(helloForm, helloModel);
            helloForm.Show();
        }

        private void SetFormToPanel(IPanelForm panelForm, Control panel)
        {
            var form = panelForm.GetForm();
            form.Width = panel.Width;
            form.Height = panel.Height;
            form.TopLevel = false;
            form.ControlBox = false;
            form.AutoScroll = false;
            form.ShowIcon = false;
            form.Text = "";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.WindowState = FormWindowState.Maximized;
            form.StartPosition = FormStartPosition.Manual;
            form.Show();
            panel.Controls.Add(form);
            panelForm.OnStarted();
        }

    }
}
