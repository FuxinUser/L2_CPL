using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common.StTool
{
    public partial class CtrTextBox : TextBox
    {
        private object objBindCurrentValue;

        public CtrTextBox()
        {
            InitializeComponent();
            this.ImeMode = System.Windows.Forms.ImeMode.OnHalf;
        }

        //public CtrTextBox(IContainer container)
        //{
        //    container.Add(this);

        //    InitializeComponent();
        //}

        protected override void OnTextChanged(EventArgs e)
        {
            //base.OnTextChanged(e);
            //this.BackColor = Color.LightBlue;


        }


        // 預設 Text 值改變時同時更新 BingCurrentValue 的值.
        protected override void OnValidated(EventArgs e)
        {
            base.OnValidated(e);
            BindCurrentValue = this.Text;
        }

        // 進入控制項時全選內容.
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            this.SelectAll();
            this.ImeMode = System.Windows.Forms.ImeMode.OnHalf;
        }

        // 提供各子物件處理自身的 Text 顯示格式.
        public virtual void Fun_UpdateTextByCurrentValue()
        {
            this.Text = objBindCurrentValue == null ? "" : objBindCurrentValue.ToString();
        }

        [Category("StTool"), Description("綁定的資料欄位名稱")]
        public string BindColumnName { get; set; }

        [Browsable(false)]
        [Category("StTool"), Description("綁定的資料欄位現值")]
        public virtual object BindCurrentValue
        {
            get
            {
                return objBindCurrentValue;
            }
            set
            {
                objBindCurrentValue = value;
                Fun_UpdateTextByCurrentValue();
            }
        }




    }
}
