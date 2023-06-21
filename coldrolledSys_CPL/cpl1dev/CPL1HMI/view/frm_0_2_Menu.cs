using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace CPL1HMI
{
    public partial class frm_0_2_Menu : Form
    {
        //語系
        private LanguageHandler LanguageHand;

        public frm_0_2_Menu()
        {
            InitializeComponent();
        }

       
        private void Frm_0_2_Menu_Load(object sender, EventArgs e)
        {
            if (PublicForms.Menu == null) PublicForms.Menu = this;

            UserSetupHandler.Instance.Fun_SetupBanForm_Menu();
            
            Lbl_Version.Text = Fun_GetApplicationCompilerTime();

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }


        /// <summary>
        /// 取得建置時間
        /// </summary>
        /// <returns></returns>
        public static string Fun_GetApplicationCompilerTime()
        {
            string str  = File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location).ToString("yyyy_MMdd_HHmm");


            string strA = Fun_GetApplicationVersion();
            DateTime d = new DateTime(2000, 1, 1);
            string strDays = strA.Remove(8).Remove(0, 4);
            int intDays = Convert.ToInt32(strDays);
            string strSs = strA.Remove(0, 9);
            int intSs = Convert.ToInt32(strSs);
            string strVersion = d.AddDays(intDays).AddSeconds(intSs * 2).ToString("yyyy_MMdd_HHmmss");

            return str;
        }

        /// <summary>
        /// 取得發布版本
        /// </summary>
        /// <returns></returns>
        public static string Fun_GetApplicationVersion()
        {
            string str = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.ToString();
            return str;
        }

        //留下參考 可簡寫成這樣
        //public static string getApplicationVersion() => FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.ToString();
        //public static string getApplicationCompilerTime() => File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location).ToString("yyyy/MM/dd");

        /// <summary>
        ///     ''' 處理按下各畫面按鈕事件，首先關閉已開啟的子畫面，再開啟選擇之畫面!
        ///     ''' </summary>
        ///     ''' <param name="sender"></param>
        ///     ''' <param name="e"></param>
        ///     ''' <remarks></remarks>
        private void MenuClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            frm_0_0_Main FatherForm = Parent.Parent as frm_0_0_Main;
            
            PublicComm.ClientLog.Info($"訊息名稱:功能選單 訊息:使用者<<{CommonDef.LoginUser}>>按下按鈕<{btn.Name}>開啟畫面");

            switch (btn.Name)
            {
                case nameof(tsMenuItem_1_1):
                    {
                        FatherForm.tsMenuItem_1_1.PerformClick();
                        PublicComm.ClientLog.Info($"訊息名稱:功能選單 訊息:開啟1-1入料鋼卷排程資訊");
                        break;
                    }

                case nameof(tsMenuItem_1_2):
                    {
                        FatherForm.tsMenuItem_1_2.PerformClick();
                        PublicComm.ClientLog.Info($"訊息名稱:功能選單 訊息:開啟1-2入料鋼卷詳細資料");
                        break;
                    }
                case nameof(tsMenuItem_1_3):
                    {
                        FatherForm.tsMenuItem_1_3.PerformClick();
                        PublicComm.ClientLog.Info($"訊息名稱:功能選單 訊息:開啟1-3刪除排程記錄");
                        break;
                    }
                case nameof(tsMenuItem_2_1):
                    {
                        FatherForm.tsMenuItem_2_1.PerformClick();
                        PublicComm.ClientLog.Info($"訊息名稱:功能選單 訊息:開啟2-1鋼卷追蹤");
                        break;
                    }

                case nameof(tsMenuItem_2_2):
                    {
                        FatherForm.tsMenuItem_2_2.PerformClick();
                        PublicComm.ClientLog.Info($"訊息名稱:功能選單 訊息:開啟2-2歷史生產設定");
                        break;
                    }

                case nameof(tsMenuItem_3_1):
                    {
                        FatherForm.tsMenuItem_3_1.PerformClick();
                        PublicComm.ClientLog.Info($"訊息名稱:功能選單 訊息:開啟3-1產品鋼卷資訊");
                        break;
                    }

                case nameof(tsMenuItem_3_2):
                    {
                        FatherForm.tsMenuItem_3_2.PerformClick();
                        PublicComm.ClientLog.Info($"訊息名稱:功能選單 訊息:開啟3-2產品鋼卷詳細資料");
                        break;
                    }
                case nameof(tsMenuItem_3_3):
                    {
                        FatherForm.tsMenuItem_3_3.PerformClick();
                        PublicComm.ClientLog.Info($"訊息名稱:功能選單 訊息:開啟3-3趨勢圖");
                        break;
                    }

                case nameof(tsMenuItem_3_4):
                    {
                        FatherForm.tsMenuItem_3_4.PerformClick();
                        PublicComm.ClientLog.Info($"訊息名稱:功能選單 訊息:開啟3-3報表");
                        break;
                    }
                case nameof(tsMenuItem_4_1):
                    {
                        FatherForm.tsMenuItem_4_1.PerformClick();
                        PublicComm.ClientLog.Info($"訊息名稱:功能選單 訊息:開啟4-1停復機記錄");
                        break;
                    }
                case nameof(tsMenuItem_4_2):
                    {
                        FatherForm.tsMenuItem_4_2.PerformClick();
                        PublicComm.ClientLog.Info($"訊息名稱:功能選單 訊息:開啟4-2校磅记录");
                        break;
                    }
                case nameof(tsMenuItem_4_3):
                    {
                        FatherForm.tsMenuItem_4_3.PerformClick();
                        PublicComm.ClientLog.Info($"訊息名稱:功能選單 訊息:開啟4-3設備參數");
                        break;
                    }
                case nameof(tsMenuItem_5_1):
                    {
                        FatherForm.tsMenuItem_5_1.PerformClick();
                        PublicComm.ClientLog.Info($"訊息名稱:功能選單 訊息:開啟5-1事件記錄");
                        break;
                    }
                case nameof(tsMenuItem_5_2):
                    {
                        FatherForm.tsMenuItem_5_2.PerformClick();
                        PublicComm.ClientLog.Info($"訊息名稱:功能選單 訊息:開啟5-2代碼維護");
                        break;
                    }
                case nameof(tsMenuItem_5_3):
                    {
                        FatherForm.tsMenuItem_5_3.PerformClick();
                        PublicComm.ClientLog.Info($"訊息名稱:功能選單 訊息:開啟5-3人員管理");
                        break;
                    }
                case nameof(tsMenuItem_5_4):
                    {
                        FatherForm.tsMenuItem_5_4.PerformClick();
                        PublicComm.ClientLog.Info($"訊息名稱:功能選單 訊息:開啟5-4排班作業");
                        break;
                    }
                case nameof(tsMenuItem_5_5):
                    {
                        FatherForm.tsMenuItem_5_5.PerformClick();
                        PublicComm.ClientLog.Info($"訊息名稱:功能選單 訊息:開啟5-5中英文對照");
                        break;
                    }
                case nameof(tsMenuItem_5_6):
                    {
                        FatherForm.tsMenuItem_5_6.PerformClick();
                        PublicComm.ClientLog.Info($"訊息名稱:功能選單 訊息:開啟5-6連線狀態");
                        break;
                    }

                default:
                    break;
            }
        }
    }
}
