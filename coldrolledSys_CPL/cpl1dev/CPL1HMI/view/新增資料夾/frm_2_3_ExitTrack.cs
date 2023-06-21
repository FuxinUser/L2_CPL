using System;
using System.Windows.Forms;

namespace APLHMI
{
    public partial class frm_2_3_ExitTrack : Form
    {
        public frm_2_3_ExitTrack()
        {
            InitializeComponent();
        }

        private void frm_2_3_ExitTrack_Shown(object sender, EventArgs e)
        {
            this.Initial();
        }

        /// <summary>
        ///     ''' 依[登入使用者]設定各畫面權限
        ///     ''' </summary>
        ///     ''' <remarks></remarks>
        public void Initial()
        {
        }
    }
}
