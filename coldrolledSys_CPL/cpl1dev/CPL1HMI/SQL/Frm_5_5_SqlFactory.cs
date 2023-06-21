using static CPL1HMI.DataBaseTableFactory;

namespace CPL1HMI
{
    public class Frm_5_5_SqlFactory
    {

        #region --- Display ---

        /// <summary>
        /// 搜尋中英文標題
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_LangSwitch_Nav(bool bolSearch = false)
        {

            string strSql = $"Select [{nameof(TBL_LangSwitch_Nav.ZH)}],[{nameof(TBL_LangSwitch_Nav.EN)}] From [{nameof(TBL_LangSwitch_Nav)}] ";

            if (bolSearch)
            {

                if (PublicForms.Language.Rdo_KeywordCn.Checked)
                {

                    strSql += $"Where [{nameof(TBL_LangSwitch_Nav.ZH)}] like '%{PublicForms.Language.txtKeywordChinese.Text.Trim()}%'";

                }
                else
                {

                    strSql += $"Where [{nameof(TBL_LangSwitch_Nav.EN)}] like '%{PublicForms.Language.txtKeywordEnglish.Text.Trim()}%'";

                }

            }

            return strSql;

        }


        /// <summary>
        /// 簡英切換-標題搜尋
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_LangSwitch_NavChange()
        {
            string strSql = $@"Select * From [{nameof(TBL_LangSwitch_Nav)}] ";

            return strSql;
        }


        /// <summary>
        /// 簡英切換-元件搜尋
        /// </summary>
        /// <param name="FormName"></param>
        /// <returns></returns>
        public static string SQL_Select_LangSwitch_CtrChange(string FormName)
        {
            string strSql = $@"Select [{nameof(TBL_LangSwitch_Ctr.CtrName)}],
                                      [{nameof(TBL_LangSwitch_Ctr.ZH)}],
                                      [{nameof(TBL_LangSwitch_Ctr.EN)}]
                                 From [{nameof(TBL_LangSwitch_Ctr)}]";

            if (!FormName.Equals(""))
            {
                strSql += $"Where [{nameof(TBL_LangSwitch_Ctr.FormName)}] = '{FormName}'";
            }

            return strSql;
        }



        #endregion


        #region --- Funtion ---

        public static string SQL_Update_LangSwitch_Nav()
        {
            string strSql = $@"Update  [{nameof(TBL_LangSwitch_Nav)}] Set ";


            if (PublicForms.Language.Rdo_KeywordCn.Checked)
            {
                strSql += $@"   [{nameof(TBL_LangSwitch_Nav.ZH)}] = '{PublicForms.Language.txtKeywordChinese.Text.Trim()}'";
            }
            else
            {
                strSql += $@"   [{nameof(TBL_LangSwitch_Nav.EN)}] = '{PublicForms.Language.txtKeywordEnglish.Text.Trim()}'";
            }


            strSql += $@" Where [{nameof(TBL_LangSwitch_Nav.ZH)}] = '{PublicForms.Language.Dgv_Language.CurrentRow.Cells[0].Value}'
                            And [{nameof(TBL_LangSwitch_Nav.EN)}] = '{PublicForms.Language.Dgv_Language.CurrentRow.Cells[1].Value}'";

             return strSql;
        }


        #endregion

    }
}
