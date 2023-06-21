using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Resources;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CPL1HMI
{
    public partial class frm_5_5_Language : Form
    {
        //語系
        private LanguageHandler LanguageHand;
        public frm_5_5_Language()
        {
            InitializeComponent();
        }

        private void Frm_5_5_Language_Load(object sender, EventArgs e)
        {           
            if (PublicForms.Language == null) PublicForms.Language = this;

            Control[] Frm_5_5_Control = new Control[] {
                Btn_Edit
            };

            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_5_5_Control, UserSetupHandler.Instance.Frm_5_5);

            Fun_Select_ChineseEnglishTitle();

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }


        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Search_Click(object sender, EventArgs e)
        {

            Fun_Select_ChineseEnglishTitle(true);
           
        }


        /// <summary>
        /// 搜尋中英文標題資料表
        /// </summary>
        private void Fun_Select_ChineseEnglishTitle(bool bolSearch = false)
        { 
            string strSql = Frm_5_5_SqlFactory.SQL_Select_LangSwitch_Nav(bolSearch);

            DataTable dtGetTitle = DataAccess.Fun_SelectDate(strSql, "中英文标题查询");

            //DGV設定
            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_Language, dtGetTitle);
            Frm_5_5_ColumnsHandler.Instance.Frm_5_5_LanguageTitle(Dgv_Language);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_Language);

        }
   

        /// <summary>
        /// DataGridView CellClick Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_Language_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                //中文標題
                txtKeywordChinese.Text = Dgv_Language.CurrentRow.Cells[0].Value.ToString();

                //英文標題
                txtKeywordEnglish.Text = Dgv_Language.CurrentRow.Cells[1].Value.ToString();

            }
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Edit_Click(object sender, EventArgs e)
        {

            string strSql = Frm_5_5_SqlFactory.SQL_Update_LangSwitch_Nav();

            if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, "修改中英文对照表"))
            {
                DialogHandler.Instance.Fun_DialogShowOk("中英文对照表修改失敗", "中英文对照表修改", 3);

                return;
            }

            //修改完的結果更新各個已開啟的畫面
            LanguageHandler.Instance.Fun_TitleLangSwitch();
        }

    }
}
