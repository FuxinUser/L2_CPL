using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CPL1HMI.Tool
{
    public partial class toolGroupBox : GroupBox
    {
        private Color _BorderColor = Color.Red;
        [Description("設定或取得外框顏色")]
        public Color BorderColor
        {
            get { return _BorderColor; }
            set { _BorderColor = value; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //取得text字型大小
            Size FontSize = TextRenderer.MeasureText(this.Text,
                                                     this.Font);
            //畫框線
            Rectangle rec = new Rectangle(e.ClipRectangle.Y,
                                          this.Font.Height / 2,
                                          e.ClipRectangle.Width - 1,
                                          e.ClipRectangle.Height - 1 -
                                          this.Font.Height / 2);

            e.Graphics.DrawRectangle(new Pen(BorderColor), rec);

            //填滿text的背景
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor),
                new Rectangle(6, 0, FontSize.Width, FontSize.Height));

            //text
            e.Graphics.DrawString(this.Text, this.Font,
                new Pen(this.ForeColor).Brush, 6, 0);
        }
        public toolGroupBox()
        {
            InitializeComponent();
        }

        public toolGroupBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
