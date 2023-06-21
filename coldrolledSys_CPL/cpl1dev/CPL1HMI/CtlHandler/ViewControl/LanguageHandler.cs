﻿using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using static CPL1HMI.DataBaseTableFactory;
using System.Reflection;
using System;

namespace CPL1HMI
{
    public class LanguageHandler
    {
        public string UILanguage;
        public const string English = "en-US";
        public const string Chinese = "zh-CN";
        public const string China = "zh-CN";
        public const string Taiwan = "zh-TW";

        /// <summary>
        ///true = 中文; False = 英文
        /// </summary>
        public bool DefaultLanguage = true;

        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly LanguageHandler INSTANCE = new LanguageHandler();
        }

        public static LanguageHandler Instance { get { return SingletonHolder.INSTANCE; } }

        public void Fun_CurrentLanguage(string FormName = "")
        {
            #region Old_2021_1124
            //UILanguage = DefaultLanguage ? Chinese : English;
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(UILanguage);
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(UILanguage);

            ////Fun_LanguageChange();

            //Fun_TitleLangSwitch();

            //Fun_ControlLangSwitch(FormName);
            #endregion
           
        }
        public void ChangeLang()
        {           
            Dictionary<string, string> navLang;
            Dictionary<string, string> ctrLang;

            string userUICulture = Thread.CurrentThread.CurrentUICulture.Name;
            if (userUICulture == Taiwan || userUICulture == China)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                navLang = GetNavLangSwitch("EN");
                ctrLang = GetCtrLangSwitch("EN");
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
                navLang = GetNavLangSwitch("ZH");
                ctrLang = GetCtrLangSwitch("ZH");
            }

            if (navLang?.Values.Count == 0 || ctrLang?.Values.Count == 0) return;

            //navMenu
            Fun_SettingMainTitleArea(navLang);

            //forms ctr
            foreach (Form form in PublicForms.Main.Pnl_Main.Controls.OfType<Form>())
            {

                foreach (Control ctrl in form.Controls)
                {
                    Fun_SelectControl(form, ctrl, ctrLang);
                }
            }
        }
        private Dictionary<string, string> GetNavLangSwitch(string lang)
        {
            Dictionary<string, string> rtn = new Dictionary<string, string>();
            //using (TLLDBEntities entities = new TLLDBEntities())
            //{
            string strSql = $"SELECT * FROM   {nameof(TBL_LangSwitch_Nav)}";// 
            var ls = DataAccess.Fun_SelectDate(strSql, $"取得{nameof(TBL_LangSwitch_Nav)}资料");// PublicComm.portal.DbHand.Fun_DataTable(strSql, PublicSystem.ConnectionString); //entities.TBL_LangSwitch_Nav.AsNoTracking().ToList();
            if (lang.ToUpper() == nameof(TBL_LangSwitch_Nav.EN))
            {
                foreach (DataRow row in ls.Rows)
                {
                    rtn[row[nameof(TBL_LangSwitch_Nav.PKey)].ToString().ToUpper()] = row[nameof(TBL_LangSwitch_Nav.EN)].ToString();
                }
                //foreach (var row in ls)
                //{
                //    rtn[row.PKey] = row.EN;
                //}
            }
            else
            {
                foreach (DataRow row in ls.Rows)
                {
                    rtn[row[nameof(TBL_LangSwitch_Nav.PKey)].ToString().ToUpper()] = row[nameof(TBL_LangSwitch_Nav.ZH)].ToString();
                }
                //foreach (var row in ls)
                //{
                //    rtn[row.PKey] = row.ZH;
                //}
            }
            //}
            return rtn;
        }

        private Dictionary<string, string> GetCtrLangSwitch(string lang)
        {
            Dictionary<string, string> rtn = new Dictionary<string, string>();
            //using (TLLDBEntities entities = new TLLDBEntities())
            //{
            string strSql = $"SELECT * FROM   {nameof(TBL_LangSwitch_Ctr)}";// 
            var ls = DataAccess.Fun_SelectDate(strSql, $"取得{nameof(TBL_LangSwitch_Ctr)}资料");//PublicComm.portal.DbHand.Fun_DataTable(strSql, PublicSystem.ConnectionString); //entities.TBL_LangSwitch_Ctr.AsNoTracking().ToList();
            if (lang.ToUpper() == nameof(TBL_LangSwitch_Ctr.EN))
            {
                foreach (DataRow row in ls.Rows)
                {
                    rtn[row[nameof(TBL_LangSwitch_Ctr.FormName)].ToString().ToUpper() + row[nameof(TBL_LangSwitch_Ctr.CtrName)].ToString().ToUpper()] = row[nameof(TBL_LangSwitch_Ctr.EN)].ToString();
                }
                //foreach (var row in ls)
                //{
                //    rtn[row.FormName + row.ComName] = row.EN;
                //}
            }
            else
            {
                foreach (DataRow row in ls.Rows)
                {
                    rtn[row[nameof(TBL_LangSwitch_Ctr.FormName)].ToString().ToUpper() + row[nameof(TBL_LangSwitch_Ctr.CtrName)].ToString().ToUpper()] = row[nameof(TBL_LangSwitch_Ctr.ZH)].ToString();
                }
                //foreach (var row in ls)
                //{
                //    rtn[row.FormName + row.ComName] = row.ZH;
                //}
            }
            //}
            return rtn;
        }

        /// <summary>
        /// 分類各容器元件，如不是容器元件直接設定名稱
        /// </summary>
        /// <param name="Ctr"></param>
        /// <param name="lang"></param>
        private void Fun_SelectControl(Form frm, Control Ctr, Dictionary<string, string> lang)
        {
            if (Ctr is Panel)
            {
                Fun_FindControlInPanel(frm, Ctr, lang);
            }
            else if (Ctr is GroupBox)
            {
                Fun_FindControlInGroupBox(frm, Ctr, lang);
            }
            else if (Ctr is TabControl)
            {
                Fun_FindControlInTabControl(frm, Ctr, lang);
            }
            else
            {
                Fun_SettingControlText(frm, Ctr, lang);
            }
        }

        /// <summary>
        /// 找尋Panel內的元件並設定名稱
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="lang"></param>
        private void Fun_FindControlInPanel(Form frm, Control ctrl, Dictionary<string, string> lang)
        {
            foreach (Control Ctr in ctrl.Controls.OfType<Control>())
            {
                Fun_SelectControl(frm, Ctr, lang);
            }
        }

        /// <summary>
        /// 尋找GroupBox內的元件並設定名稱
        /// </summary>
        /// <param name="Ctr"></param>
        /// <param name="lang"></param>
        private void Fun_FindControlInGroupBox(Form frm, Control Ctr, Dictionary<string, string> lang)
        {
            Fun_SettingControlText(frm, Ctr, lang);

            foreach (Control control in Ctr.Controls.OfType<Control>())
            {
                Fun_SelectControl(frm, control, lang);
            }
        }

        /// <summary>
        /// 找尋所有TabPage內的元件並設定名稱
        /// </summary>
        /// <param name="Ctr"></param>
        /// <param name="lang"></param>
        private void Fun_FindControlInTabControl(Form frm, Control Ctr, Dictionary<string, string> lang)
        {
            TabControl tab = (TabControl)Ctr;

            foreach (TabPage tp in tab.TabPages)
            {

                Fun_SettingControlText(frm, tp, lang);

                foreach (Control control in tp.Controls.OfType<Control>())
                {
                    Fun_SelectControl(frm, control, lang);
                }
            }
        }      

        /// <summary>
        /// 設定元件名稱
        /// </summary>
        /// <param name="Ctr"></param>
        /// <param name="lang"></param>
        private void Fun_SettingControlText(Form frm, Control Ctr, Dictionary<string, string> lang)
        {
            string key = frm.Name + Ctr.Name;
            if (lang.ContainsKey(key.ToUpper()))
            {
                Ctr.Text = lang[key.ToUpper()].Replace("\\r\\n", "\r\n");
            }
        }

        private void Fun_SettingMainTitleArea(Dictionary<string, string> lang)
        {
            #region [Iten Text]
            string str_0 = lang["tsMenuItem_0".ToUpper()];
            string str_1 = lang["tsMenuItem_1".ToUpper()];
            string str_2 = lang["tsMenuItem_2".ToUpper()];
            string str_3 = lang["tsMenuItem_3".ToUpper()];
            string str_4 = lang["tsMenuItem_4".ToUpper()];
            string str_5 = lang["tsMenuItem_5".ToUpper()];

            string str_1_1 = lang["tsMenuItem_1_1".ToUpper()];
            string str_1_2 = lang["tsMenuItem_1_2".ToUpper()];
            string str_1_3 = lang["tsMenuItem_1_3".ToUpper()];

            string str_2_1 = lang["tsMenuItem_2_1".ToUpper()];
            string str_2_2 = lang["tsMenuItem_2_2".ToUpper()];

            string str_3_1 = lang["tsMenuItem_3_1".ToUpper()];
            string str_3_2 = lang["tsMenuItem_3_2".ToUpper()];
            string str_3_3 = lang["tsMenuItem_3_3".ToUpper()];
            string str_3_4 = lang["tsMenuItem_3_4".ToUpper()];

            string str_4_1 = lang["tsMenuItem_4_1".ToUpper()];
            string str_4_2 = lang["tsMenuItem_4_2".ToUpper()];
            string str_4_3 = lang["tsMenuItem_4_3".ToUpper()];
      
            string str_5_1 = lang["tsMenuItem_5_1".ToUpper()];
            string str_5_2 = lang["tsMenuItem_5_2".ToUpper()];
            string str_5_3 = lang["tsMenuItem_5_3".ToUpper()];
            string str_5_4 = lang["tsMenuItem_5_4".ToUpper()];
            string str_5_5 = lang["tsMenuItem_5_5".ToUpper()];
            string str_5_6 = lang["tsMenuItem_5_6".ToUpper()];
            #endregion

            #region [Form_Main msTitleArea]
            if (PublicForms.Main != null)
            {
                //PublicForms.Main.men.Text = lang["tsMenuItem_0"];
                PublicForms.Main.Lbl_LoginUser_header.Text = lang["Lbl_LoginUser_header".ToUpper()];
                PublicForms.Main.Btn_Language.Text = lang["Btn_Language".ToUpper()];
                PublicForms.Main.Lbl_EvnMsg.Text = lang["Lbl_EvnMsg".ToUpper()];
                #region [Form_Main msTitleArea]
                PublicForms.Main.tsMenuItem_0.Text = str_0;
                PublicForms.Main.tsMenuItem_1.Text = str_1;
                PublicForms.Main.tsMenuItem_2.Text = str_2;
                PublicForms.Main.tsMenuItem_3.Text = str_3;
                PublicForms.Main.tsMenuItem_4.Text = str_4;
                PublicForms.Main.tsMenuItem_5.Text = str_5;

                PublicForms.Main.tsMenuItem_1_1.Text = str_1_1;
                PublicForms.Main.tsMenuItem_1_2.Text = str_1_2;
                PublicForms.Main.tsMenuItem_1_3.Text = str_1_3;

                PublicForms.Main.tsMenuItem_2_1.Text = str_2_1;
                PublicForms.Main.tsMenuItem_2_2.Text = str_2_2;

                PublicForms.Main.tsMenuItem_3_1.Text = str_3_1;
                PublicForms.Main.tsMenuItem_3_2.Text = str_3_2;
                PublicForms.Main.tsMenuItem_3_3.Text = str_3_3;
                PublicForms.Main.tsMenuItem_3_4.Text = str_3_4;

                PublicForms.Main.tsMenuItem_4_1.Text = str_4_1;
                PublicForms.Main.tsMenuItem_4_2.Text = str_4_2;
                PublicForms.Main.tsMenuItem_4_3.Text = str_4_3;

                PublicForms.Main.tsMenuItem_5_1.Text = str_5_1;
                PublicForms.Main.tsMenuItem_5_2.Text = str_5_2;
                PublicForms.Main.tsMenuItem_5_3.Text = str_5_3;
                PublicForms.Main.tsMenuItem_5_4.Text = str_5_4;
                PublicForms.Main.tsMenuItem_5_5.Text = str_5_5;
                PublicForms.Main.tsMenuItem_5_6.Text = str_5_6;

                #endregion
            }

            //PublicForms.Main.tsMenuItem_1.Text = lang["tsMenuItem_1"];
            //PublicForms.Main.tsMenuItem_1_1.Text = lang["tsMenuItem_1_1"];
            //PublicForms.Main.tsMenuItem_1_2.Text = lang["tsMenuItem_1_2"];
            //PublicForms.Main.tsMenuItem_1_3.Text = lang["tsMenuItem_1_3"];
            //PublicForms.Main.tsMenuItem_2.Text = lang["tsMenuItem_2"];
            //PublicForms.Main.tsMenuItem_2_1.Text = lang["tsMenuItem_2_1"];
            //PublicForms.Main.tsMenuItem_2_2.Text = lang["tsMenuItem_2_2"];
            //PublicForms.Main.tsMenuItem_3.Text = lang["tsMenuItem_3"];
            //PublicForms.Main.tsMenuItem_3_1.Text = lang["tsMenuItem_3_1"];
            //PublicForms.Main.tsMenuItem_3_2.Text = lang["tsMenuItem_3_2"];
            //PublicForms.Main.tsMenuItem_3_3.Text = lang["tsMenuItem_3_3"];
            //PublicForms.Main.tsMenuItem_3_4.Text = lang["tsMenuItem_3_4"];
            //PublicForms.Main.tsMenuItem_4.Text = lang["tsMenuItem_4"];
            //PublicForms.Main.tsMenuItem_4_1.Text = lang["tsMenuItem_4_1"];
            //PublicForms.Main.tsMenuItem_4_2.Text = lang["tsMenuItem_4_2"];
            //PublicForms.Main.tsMenuItem_4_3.Text = lang["tsMenuItem_4_3"];
            //PublicForms.Main.tsMenuItem_5.Text = lang["tsMenuItem_5"];
            //PublicForms.Main.tsMenuItem_5_1.Text = lang["tsMenuItem_5_1"];
            //PublicForms.Main.tsMenuItem_5_2.Text = lang["tsMenuItem_5_2"];
            //PublicForms.Main.tsMenuItem_5_3.Text = lang["tsMenuItem_5_3"];
            //PublicForms.Main.tsMenuItem_5_4.Text = lang["tsMenuItem_5_4"];
            //PublicForms.Main.tsMenuItem_5_5.Text = lang["tsMenuItem_5_5"];
            //PublicForms.Main.tsMenuItem_5_6.Text = lang["tsMenuItem_5_6"];
            //PublicForms.Main.tsMenuItem_0.Text = lang["tsMenuItem_0"];

            ////PublicForms.Main.lblLoginUser_header.Text = lang["lblLoginUser_header"];
            ////PublicForms.Main.lblEvnMsg.Text = lang["lblEvnMsg"];
            #endregion

            if (PublicForms.Menu != null)
            {
                #region [Form_Menu Button]
                PublicForms.Menu.tsMenuItem_0.Text = str_0;
                PublicForms.Menu.tsMenuItem_1.Text = str_1;
                PublicForms.Menu.tsMenuItem_2.Text = str_2;
                PublicForms.Menu.tsMenuItem_3.Text = str_3;
                PublicForms.Menu.tsMenuItem_4.Text = str_4;
                PublicForms.Menu.tsMenuItem_5.Text = str_5;

                PublicForms.Menu.tsMenuItem_1_1.Text = str_1_1;
                PublicForms.Menu.tsMenuItem_1_2.Text = str_1_2;
                PublicForms.Menu.tsMenuItem_1_3.Text = str_1_3;

                PublicForms.Menu.tsMenuItem_2_1.Text = str_2_1;
                PublicForms.Menu.tsMenuItem_2_2.Text = str_2_2;

                PublicForms.Menu.tsMenuItem_3_1.Text = str_3_1;
                PublicForms.Menu.tsMenuItem_3_2.Text = str_3_2;
                PublicForms.Menu.tsMenuItem_3_3.Text = str_3_3;
                PublicForms.Menu.tsMenuItem_3_4.Text = str_3_4;

                PublicForms.Menu.tsMenuItem_4_1.Text = str_4_1;
                PublicForms.Menu.tsMenuItem_4_2.Text = str_4_2;
                PublicForms.Menu.tsMenuItem_4_3.Text = str_4_3;

                PublicForms.Menu.tsMenuItem_5_1.Text = str_5_1;
                PublicForms.Menu.tsMenuItem_5_2.Text = str_5_2;
                PublicForms.Menu.tsMenuItem_5_3.Text = str_5_3;
                PublicForms.Menu.tsMenuItem_5_4.Text = str_5_4;
                PublicForms.Menu.tsMenuItem_5_5.Text = str_5_5;
                PublicForms.Menu.tsMenuItem_5_6.Text = str_5_6;

                #endregion
            }

            if (PublicForms.UserSetup != null)
            {
                #region [Form_UserSetup Lable]
                PublicForms.UserSetup.Lbl_1_1.Text = str_1_1;
                PublicForms.UserSetup.Lbl_1_2.Text = str_1_2;
                PublicForms.UserSetup.Lbl_1_3.Text = str_1_3;

                PublicForms.UserSetup.Lbl_2_1.Text = str_2_1;
                PublicForms.UserSetup.Lbl_2_2.Text = str_2_2;

                PublicForms.UserSetup.Lbl_3_1.Text = str_3_1;
                PublicForms.UserSetup.Lbl_3_2.Text = str_3_2;
                PublicForms.UserSetup.Lbl_3_3.Text = str_3_3;
                PublicForms.UserSetup.Lbl_3_4.Text = str_3_4;

                PublicForms.UserSetup.Lbl_4_1.Text = str_4_1;
                PublicForms.UserSetup.Lbl_4_2.Text = str_4_2;
                PublicForms.UserSetup.Lbl_4_3.Text = str_4_3;

                PublicForms.UserSetup.Lbl_5_1.Text = str_5_1;
                PublicForms.UserSetup.Lbl_5_2.Text = str_5_2;
                PublicForms.UserSetup.Lbl_5_3.Text = str_5_3;
                PublicForms.UserSetup.Lbl_5_4.Text = str_5_4;
                PublicForms.UserSetup.Lbl_5_5.Text = str_5_5;
                PublicForms.UserSetup.Lbl_5_6.Text = str_5_6;

                #endregion
            }

            #region [All_Form_Title]           
            //一 PDI
            if (PublicForms.PDISchl != null) { PublicForms.PDISchl.tsMenuItem_1_1.Text = str_1_1; }         //PDI 排程&清單
            if (PublicForms.PDIDetail != null) { PublicForms.PDIDetail.tsMenuItem_1_2.Text = str_1_2; }           //PDI 詳細
            if (PublicForms.DeleteScheduleRecord != null) { PublicForms.DeleteScheduleRecord.Lbl_DelSchdlTitle.Text = str_1_3; }   //排程刪除資訊

            //二 钢卷追踪
            if (PublicForms.Tracking != null) { PublicForms.Tracking.Lbl_TrackingTitle.Text = str_2_1; }             //钢卷追踪
            if (PublicForms.HistoryProductionSetup != null) { PublicForms.HistoryProductionSetup.Lbl_ProdSettingTitle.Text = str_2_2; } //历史生产设定

            //三 PDO
            if (PublicForms.PDOCoilList != null) { PublicForms.PDOCoilList.Lbl_MainTitle.Text = str_3_1; }    //PDO 清單
            if (PublicForms.PDODetail != null) { PublicForms.PDODetail.Lbl_MainTitle.Text = str_3_2; }            //PDO 詳細 
            if (PublicForms.TrendChart != null) { PublicForms.TrendChart.Lbl_FrmTitle.Text = str_3_3; }                      //趨勢圖
            if (PublicForms.Report != null) { PublicForms.Report.lblMainTitle.Text = str_3_4; }                      //報表

            //四 設備資訊
            if (PublicForms.LineDelayRecord != null) { PublicForms.LineDelayRecord.Lbl_MainTitle.Text = str_4_1; }              //停復機紀錄
            //if (PublicForms.Utility != null) { PublicForms.Utility.Lbl_MainTitle.Text = str_4_2; }                  //能源耗用
            if (PublicForms.Weighing != null) { PublicForms.Weighing.Lbl_MainTitle.Text = str_4_2; }                  //校磅紀錄
            if (PublicForms.PresetTable != null) { PublicForms.PresetTable.Lbl_MainTitle.Text = str_4_3; }         //設備參數


            //五 系統維護
            if (PublicForms.EventLog != null) { PublicForms.EventLog.Lbl_MainTitle.Text = str_5_1; }      //警報與事件
            if (PublicForms.CodeMaintain != null) { PublicForms.CodeMaintain.Lbl_MainTitle.Text = str_5_2; }   //代碼设定維護
            if (PublicForms.UserSetup != null) { PublicForms.UserSetup.Lbl_MainTitle.Text = str_5_3; }               //人員設定
            if (PublicForms.CrewMaintainance != null) { PublicForms.CrewMaintainance.lblMainTitle.Text = str_5_4; }               //排班作業
            if (PublicForms.Language != null) { PublicForms.Language.lblMainTitle.Text = str_5_5; }              //中英對照
            if (PublicForms.NetworkStatus != null) { PublicForms.NetworkStatus.Lbl_MainTitle.Text = str_5_6; }       //連線狀態
            #endregion

        }
        /// <summary>
        /// 取得現在語系做顯示 (this.Name)
        /// </summary>
        /// <param name="strFormName">指定FormName</param>
        public void Fun_GetNowLangShow(string strFormName)
        {
            Dictionary<string, string> navLang;
            Dictionary<string, string> ctrLang;

            string userUICulture = Thread.CurrentThread.CurrentUICulture.Name;
            if (userUICulture == Taiwan || userUICulture == China)
            {
                //Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
                navLang = GetNavLangSwitch("ZH");
                ctrLang = GetCtrLangSwitch("ZH");
            }
            else
            {

                //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                navLang = GetNavLangSwitch("EN");
                ctrLang = GetCtrLangSwitch("EN");
            }

            if (navLang?.Values.Count == 0 || ctrLang?.Values.Count == 0) return;

            //navMenu
            Fun_SettingMainTitleArea(navLang);

            //forms ctr
            foreach (Form form in PublicForms.Main.Pnl_Main.Controls.OfType<Form>())
            {
                if (form.Name == strFormName)
                {
                    foreach (Control ctrl in form.Controls)
                    {
                        Fun_SelectControl(form, ctrl, ctrLang);
                    }
                }

            }
        }
        /////////////////////////////////////////////////

        /// <summary>
        /// 标题中英切换 
        /// </summary>
        public void Fun_TitleLangSwitch()
        {
            string userUICulture = Thread.CurrentThread.CurrentUICulture.Name;

            Dictionary<string, string> LangTitle;

            LangTitle = userUICulture.Equals(Chinese) || userUICulture.Equals(Taiwan) ? Fun_GetLangSwitch_Nav("ZH") : Fun_GetLangSwitch_Nav("EN");

            if (LangTitle.Values.Count.Equals(0)) return;

            //navMenu
            Fun_SettingMainTitleArea(LangTitle);

            //subforms
            foreach (Form form in PublicForms.Main.Pnl_Main.Controls.OfType<Form>())
            {

                foreach (Control ctrl in form.Controls)
                {
                    Fun_SelectControl(ctrl, LangTitle);
                }
            }
        }

        /// <summary>
        /// 元件中英切换
        /// </summary>
        public void Fun_ControlLangSwitch(string FormName)
        {
            string userUICulture = Thread.CurrentThread.CurrentUICulture.Name;

            Dictionary<string, string> LangControl;

            LangControl = userUICulture.Equals(Chinese) || userUICulture.Equals(Taiwan) ? Fun_GetLangSwitch_Ctr("ZH", FormName) : Fun_GetLangSwitch_Ctr("EN", FormName);

            if (LangControl.Values.Count.Equals(0)) return;

            //subforms
            foreach (Form form in PublicForms.Main.Pnl_Main.Controls.OfType<Form>())
            {

                foreach (Control ctrl in form.Controls)
                {
                    Fun_SelectControl(ctrl, LangControl);
                }
            }


            Dictionary<string, string> LangControl_Main;

            LangControl_Main = userUICulture.Equals(Chinese) || userUICulture.Equals(Taiwan) ? Fun_GetLangSwitch_Ctr("ZH", "") : Fun_GetLangSwitch_Ctr("EN", "");

            if (LangControl_Main.Values.Count.Equals(0)) return;

            Fun_SettingMainLabel(LangControl_Main);
        }

        #region '語系切換新版'

        /// <summary>
        /// pnl Equals Form_Main Panel
        /// </summary>
        /// <param name="pnl"></param>
        public void Fun_LanguageChange()
        {
            //string userUICulture = Thread.CurrentThread.CurrentUICulture.Name;

            //Dictionary<string, string> lang;

            //lang = userUICulture.Equals(Chinese) || userUICulture.Equals(Taiwan) ? Fun_GetLangSwitch("ZH") : Fun_GetLangSwitch("EN");

            //if (lang.Values.Count.Equals(0)) return;


            ////navMenu
            //Fun_SettingMainTitleArea(lang);

            ////subforms
            //foreach (Form form in PublicForms.Main.pnl_Main.Controls.OfType<Form>())
            //{

            //    foreach (Control ctrl in form.Controls)
            //    {
            //        Fun_SelectControl(ctrl, lang);
            //    }
            //}
        }

        /// <summary>
        /// 找尋Panel內的元件並設定名稱
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="lang"></param>
        private void Fun_FindControlInPanel(Control ctrl, Dictionary<string, string> lang)
        {
            foreach (Control Ctr in ctrl.Controls.OfType<Control>())
            {
                Fun_SelectControl(Ctr, lang);
            }
        }

        /// <summary>
        /// 找尋所有TabPage內的元件並設定名稱
        /// </summary>
        /// <param name="Ctr"></param>
        /// <param name="lang"></param>
        private void Fun_FindControlInTabControl(Control Ctr, Dictionary<string, string> lang)
        {
            TabControl tab = (TabControl)Ctr;

            foreach (TabPage tp in tab.TabPages)
            {

                Fun_SettingControlText(tp, lang);

                foreach (Control control in tp.Controls.OfType<Control>())
                {
                    Fun_SelectControl(control, lang);
                }
            }
        }

        /// <summary>
        /// 尋找GroupBox內的元件並設定名稱
        /// </summary>
        /// <param name="Ctr"></param>
        /// <param name="lang"></param>
        private void Fun_FindControlInGroupBox(Control Ctr, Dictionary<string, string> lang)
        {
            Fun_SettingControlText(Ctr, lang);

            foreach (Control control in Ctr.Controls.OfType<Control>())
            {
                Fun_SelectControl(control, lang);
            }
        }

        /// <summary>
        /// 分類各容器元件，如不是容器元件直接設定名稱
        /// </summary>
        /// <param name="Ctr"></param>
        /// <param name="lang"></param>
        private void Fun_SelectControl(Control Ctr, Dictionary<string, string> lang)
        {
            if (Ctr is Panel)
            {
                Fun_FindControlInPanel(Ctr, lang);
            }
            else if (Ctr is GroupBox)
            {
                Fun_FindControlInGroupBox(Ctr, lang);
            }
            else if (Ctr is TabControl)
            {
                Fun_FindControlInTabControl(Ctr, lang);
            }
            else
            {
                Fun_SettingControlText(Ctr, lang);
            }
        }

        /// <summary>
        /// 設定元件名稱
        /// </summary>
        /// <param name="Ctr"></param>
        /// <param name="lang"></param>
        private void Fun_SettingControlText(Control Ctr, Dictionary<string, string> lang)
        {
            var Name = Ctr.Name;

            if (lang.ContainsKey(Name))
            {
                Ctr.Text = lang[Name];
            }
        }

       

       
        public Dictionary<string, string> Fun_GetLangSwitch_Nav(string lang,string FormName = "")
        {
            Dictionary<string, string> rtn = new Dictionary<string, string>();

            string strSql = Frm_5_5_SqlFactory.SQL_Select_LangSwitch_NavChange();

            DataTable dt = DataAccess.Fun_SelectDate(strSql, "中英文切换");
            var ls = DatatTableToList<TBL_LangSwitch_Nav>(dt);

            if (ls == null) return rtn;

            if (lang.ToUpper() == "EN")
            {
                foreach (var row in ls)
                {
                    rtn[row.PKey] = row.EN;
                }
            }
            else
            {
                foreach (var row in ls)
                {
                    rtn[row.PKey] = row.ZH;
                }
            }

            return rtn;
        }

        public Dictionary<string, string> Fun_GetLangSwitch_Ctr(string lang, string FormName = "")
        {
            Dictionary<string, string> rtn = new Dictionary<string, string>();

            string strSql = Frm_5_5_SqlFactory.SQL_Select_LangSwitch_CtrChange(FormName);

            DataTable dt = DataAccess.Fun_SelectDate(strSql, "中英文切换");

            var ls = DatatTableToList<TBL_LangSwitch_Ctr>(dt);

            if (ls == null) return rtn;

            if (lang.ToUpper() == "EN")
            {
                foreach (var row in ls)
                {
                    rtn[row.CtrName] = row.EN;
                }
            }
            else
            {
                foreach (var row in ls)
                {
                    rtn[row.CtrName] = row.ZH;
                }
            }

            return rtn;
        }

        #endregion

        private void Fun_SettingMainLabel(Dictionary<string, string> lang)
        {
            PublicForms.Main.Lbl_LoginUser_header.Text = lang["lblLoginUser_header"];
            PublicForms.Main.Lbl_EvnMsg.Text = lang["lblEvnMsg"];
        }



        /// <summary>
        /// DataTable轉List
        /// </summary>
        /// <typeparam name="T">list中的類型</typeparam>
        /// <param name="dt">要轉換的DataTable</param>
        /// <returns></returns>
        public static List<T> DatatTableToList<T>(DataTable dt) where T : class, new()
        {

            List<T> list = new List<T>();
            T t = new T();

            PropertyInfo[] prop = t.GetType().GetProperties();

            if (dt.IsNull()) return null;

            //遍歷所有DataTable的行
            foreach (DataRow dr in dt.Rows)
            {
                t = new T();
                //通過反射獲取T類型的所有成員
                foreach (PropertyInfo pi in prop)
                {
                    //DataTable列名=屬性名
                    if (dt.Columns.Contains(pi.Name))
                    {
                        //屬性值不為空
                        if (dr[pi.Name] != DBNull.Value)
                        {
                            object value = Convert.ChangeType(dr[pi.Name], pi.PropertyType);
                            //給T類型字段賦值
                            pi.SetValue(t, value, null);
                        }
                    }
                }
                //將T類型添加到集合list
                list.Add(t);
            }

            return list;

        }


        #region '各畫面語系切換[old]'

        public void Fun_ChangeLanguage()
        {
            Frm_MainLanguage();

            List<Control> listCtl = new List<Control>();
            listCtl.AddRange(PublicForms.Main.Pnl_Main.Controls.OfType<Form>());

            for (int i = 0; i < listCtl.Count; i++)
            {
                foreach (Form fx in PublicForms.Main.Pnl_Main.Controls.OfType<Form>())
                {
                    switch (fx.Name)
                    {
                        case nameof(frm_0_2_Menu):
                            Frm_MenuLanguage();
                            break;
                        default:
                            break;
                    }

                }
            }

        }

        /// <summary>
        /// Form_Main
        /// </summary>
        private void Frm_MainLanguage()
        {
            var Frm_Main = new System.Resources.ResourceManager(typeof(frm_0_0_Main));

            PublicForms.Main.tsMenuItem_0.Text = Frm_Main.GetString("tsMenuItem_0.Text");
            PublicForms.Main.tsMenuItem_1.Text = Frm_Main.GetString("tsMenuItem_1.Text");
            PublicForms.Main.tsMenuItem_1_1.Text = Frm_Main.GetString("tsMenuItem_1_1.Text");
            PublicForms.Main.tsMenuItem_1_2.Text = Frm_Main.GetString("tsMenuItem_1_2.Text");
            PublicForms.Main.tsMenuItem_1_3.Text = Frm_Main.GetString("tsMenuItem_1_3.Text");
            PublicForms.Main.tsMenuItem_2.Text = Frm_Main.GetString("tsMenuItem_2.Text");
            PublicForms.Main.tsMenuItem_2_1.Text = Frm_Main.GetString("tsMenuItem_2_1.Text");
            PublicForms.Main.tsMenuItem_2_2.Text = Frm_Main.GetString("tsMenuItem_2_2.Text");
            PublicForms.Main.tsMenuItem_3.Text = Frm_Main.GetString("tsMenuItem_3.Text");
            PublicForms.Main.tsMenuItem_3_1.Text = Frm_Main.GetString("tsMenuItem_3_1.Text");
            PublicForms.Main.tsMenuItem_3_2.Text = Frm_Main.GetString("tsMenuItem_3_2.Text");
            PublicForms.Main.tsMenuItem_3_3.Text = Frm_Main.GetString("tsMenuItem_3_3.Text");
            PublicForms.Main.tsMenuItem_3_4.Text = Frm_Main.GetString("tsMenuItem_3_4.Text");
            PublicForms.Main.tsMenuItem_4.Text = Frm_Main.GetString("tsMenuItem_4.Text");
            PublicForms.Main.tsMenuItem_4_1.Text = Frm_Main.GetString("tsMenuItem_4_1.Text");
            PublicForms.Main.tsMenuItem_4_2.Text = Frm_Main.GetString("tsMenuItem_4_2.Text");
            PublicForms.Main.tsMenuItem_4_3.Text = Frm_Main.GetString("tsMenuItem_4_3.Text");
            PublicForms.Main.tsMenuItem_5.Text = Frm_Main.GetString("tsMenuItem_5.Text");
            PublicForms.Main.tsMenuItem_5_1.Text = Frm_Main.GetString("tsMenuItem_5_1.Text");
            PublicForms.Main.tsMenuItem_5_2.Text = Frm_Main.GetString("tsMenuItem_5_2.Text");
            PublicForms.Main.tsMenuItem_5_3.Text = Frm_Main.GetString("tsMenuItem_5_3.Text");
            PublicForms.Main.tsMenuItem_5_4.Text = Frm_Main.GetString("tsMenuItem_5_4.Text");
            PublicForms.Main.tsMenuItem_5_5.Text = Frm_Main.GetString("tsMenuItem_5_5.Text");
            PublicForms.Main.tsMenuItem_5_6.Text = Frm_Main.GetString("tsMenuItem_5_6.Text");
            PublicForms.Main.Lbl_EvnMsg.Text = Frm_Main.GetString("lblEvnMsg.Text");
            PublicForms.Main.Lbl_LoginUser_header.Text = Frm_Main.GetString("lblLoginUser_header.Text");
            PublicForms.Main.Btn_Language.Text = Frm_Main.GetString("btnLanguage.Text");

            PublicForms.Main.msTitleArea.Font = Frm_Main.GetObject("msTitleArea.Font") as Font;
            PublicForms.Main.Lbl_EvnMsg.Font = Frm_Main.GetObject("lblEvnMsg.Font") as Font;
            PublicForms.Main.Lbl_LoginUser_header.Font = Frm_Main.GetObject("lblLoginUser_header.Font") as Font;
        }

        /// <summary>
        /// 功能選單
        /// </summary>
        private void Frm_MenuLanguage()
        {
            var Frm_Menu = new System.Resources.ResourceManager(typeof(frm_0_2_Menu));

            PublicForms.Menu.tsMenuItem_1_1.Text = Frm_Menu.GetString("btn_1_1.Text");
            PublicForms.Menu.tsMenuItem_1_2.Text = Frm_Menu.GetString("btn_1_2.Text");
            PublicForms.Menu.tsMenuItem_1_3.Text = Frm_Menu.GetString("btn_1_3.Text");
            PublicForms.Menu.tsMenuItem_2_1.Text = Frm_Menu.GetString("btn_2_1.Text");
            PublicForms.Menu.tsMenuItem_2_2.Text = Frm_Menu.GetString("btn_2_2.Text");
            PublicForms.Menu.tsMenuItem_3_1.Text = Frm_Menu.GetString("btn_3_1.Text");
            PublicForms.Menu.tsMenuItem_3_2.Text = Frm_Menu.GetString("btn_3_2.Text");
            PublicForms.Menu.tsMenuItem_3_3.Text = Frm_Menu.GetString("btn_3_3.Text");
            PublicForms.Menu.tsMenuItem_3_4.Text = Frm_Menu.GetString("btn_3_4.Text");
            PublicForms.Menu.tsMenuItem_4_1.Text = Frm_Menu.GetString("btn_4_1.Text");
            PublicForms.Menu.tsMenuItem_4_2.Text = Frm_Menu.GetString("btn_4_2.Text");
            PublicForms.Menu.tsMenuItem_4_3.Text = Frm_Menu.GetString("btn_4_3.Text");
            PublicForms.Menu.tsMenuItem_5_1.Text = Frm_Menu.GetString("btn_5_1.Text");
            PublicForms.Menu.tsMenuItem_5_2.Text = Frm_Menu.GetString("btn_5_2.Text");
            PublicForms.Menu.tsMenuItem_5_3.Text = Frm_Menu.GetString("btn_5_3.Text");
            PublicForms.Menu.tsMenuItem_5_4.Text = Frm_Menu.GetString("btn_5_4.Text");
            PublicForms.Menu.tsMenuItem_5_5.Text = Frm_Menu.GetString("btn_5_5.Text");
            PublicForms.Menu.tsMenuItem_5_6.Text = Frm_Menu.GetString("btn_5_6.Text");

            PublicForms.Menu.tsMenuItem_0.Text = Frm_Menu.GetString("lblMainTitle.Text");
            PublicForms.Menu.tsMenuItem_1.Text = Frm_Menu.GetString("lblTitle_1.Text");
            PublicForms.Menu.tsMenuItem_2.Text = Frm_Menu.GetString("lblTitle_2.Text");
            PublicForms.Menu.tsMenuItem_3.Text = Frm_Menu.GetString("lblTitle_3.Text");
            PublicForms.Menu.tsMenuItem_4.Text = Frm_Menu.GetString("lblTitle_4.Text");
            PublicForms.Menu.tsMenuItem_5.Text = Frm_Menu.GetString("lblTitle_5.Text");
        }

        #endregion
    }
}
