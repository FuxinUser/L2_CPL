using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common.StTool
{
    public partial class CtrNumTextBox : TextBox
    {
        private object objBindCurrentValue;
        private int intIntegerDigit;
        private int intDecimalDigit;

        public CtrNumTextBox()
        {
            InitializeComponent();
            intIntegerDigit = 15;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;

        }

        //public CtrNumTextBox(IContainer container)
        //{
        //    container.Add(this);

        //    InitializeComponent();
        //}

        // 進入控制項時全選內容.
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            this.SelectAll();
            //this.BeginInvoke((MethodInvoker)delegate() { this.SelectAll(); });
        }

        // 預設 Text 值改變時同時更新 BingCurrentValue 的值.
        protected override void OnValidated(EventArgs e)
        {
            base.OnValidated(e);

            BindCurrentValue = this.Text;
        }

        // 提供各子物件處理自身的 Text 顯示格式.
        public virtual void Fun_UpdateTextByCurrentValue()
        {
            this.Text = objBindCurrentValue == null ? "" : objBindCurrentValue.ToString();
        }

        //-----------------------------------------------------------------------------------------

        #region Validating Functions & Events.

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            //數字                                     //小數點(僅能輸入一個且不能為第一個字元)  
            if (!(
                (e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == '.' && (this.Text.IndexOf(e.KeyChar) < 0) && (this.Text.Length > 0)) ||
                (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Enter)))//&& (e.KeyChar != (char)Keys.Space)
            {               //backspace                    //Enter
                e.Handled = true;
            }

            //if (e.KeyChar == (char)Keys.Space)
            //    e.Handled = false;
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);
            if (string.IsNullOrEmpty(this.Text)) { return; }
            decimal dcText = Convert.ToDecimal(this.Text);
            //intIntegerDigit 整數位數 5
            //intDecimalDigit 小數位數 3

            //實際可輸入的整數位數
            decimal dcReIdg = intIntegerDigit - intDecimalDigit; //2

            string strOne = "1";
            string strDdg = strOne.PadRight(intDecimalDigit + 1, '0');//1000
            string strIdg = strOne.PadRight(intIntegerDigit + 1, '0');//10000

            decimal dcDdg = Convert.ToDecimal(strDdg);
            decimal dcIdg = Convert.ToDecimal(strIdg);
            decimal dcTol = dcText * dcDdg;
            if (intDecimalDigit != 0)
            {
                if (dcTol > dcIdg)
                {
                    MessageBox.Show("整数限" + dcReIdg + "位，\r\n小数限" + intDecimalDigit + "位！");
                   //OpenFunction.Fun_DialogShow("整数限" + dcReIdg + "位，\r\n小数限" + intDecimalDigit + "位！", "讯息提示");
                    this.Focus();
                    return;
                }

                if ((dcTol % 1) != 0)
                {
                    MessageBox.Show("小数位数限" + intDecimalDigit + "位！");
                    //OpenFunction.Fun_DialogShow("小数位数限" + intDecimalDigit + "位！", "讯息提示");
                    this.Focus();
                    return;
                }
            }
            else
            {
                if (this.Text.Contains("."))
                {
                    MessageBox.Show("该字段无小数位数！");
                    //OpenFunction.Fun_DialogShow("该字段无小数位数！", "讯息提示");
                    this.Focus();
                    return;
                }
            }



        }

        #endregion

        //-----------------------------------------------------------------------------------------               

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


        [Category("StTool"), Description("整數位數")]
        public int IntegerDigit
        {
            get { return intIntegerDigit; }
            set
            {
                intIntegerDigit = value;

                if (intIntegerDigit > 15 || intIntegerDigit < 0)
                    intIntegerDigit = 15;


                this.MaxLength = (intDecimalDigit == 0) ? intIntegerDigit : intIntegerDigit + 1;

            }

        }

        [Category("StTool"), Description("小數位數")]
        public int DecimalDigit
        {
            get { return intDecimalDigit; }
            set
            {
                intDecimalDigit = value;

                if (intDecimalDigit > 9 || intDecimalDigit < 0)
                    intDecimalDigit = 9;

                this.MaxLength = (intDecimalDigit == 0) ? intIntegerDigit : intIntegerDigit + 1;
            }
        }


    }
}
