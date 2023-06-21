using APL;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CPL1HMI
{
    public partial class Frm_5_4_ShiftTeam : Form
    {
        //權限設定 功能Btn
        Control[] CtlAuthority;

        DataTable dtShiftTeam;      
        DataTable dtBeforeEdit;

        string[] strArr_Shift;
        string[] strArr_Team;

        public Frm_5_4_ShiftTeam()
        {
            InitializeComponent();
            
            if (PublicForms.ShiftTeam == null)
                PublicForms.ShiftTeam = this;
            else { }
        }


        private void Frm_5_4_ShiftTeam_Load(object sender, EventArgs e)
        {
            Fun_InitialAuthority();
            Fun_InitialComboBox();
            Fun_Search();
            PublicComm.portal.UIHand.Fun_SetControlAuthority(CtlAuthority, PublicForms.LoginUser, PublicConst.FrameNo_5_4);//"5-4");
        }

        private void Frm_5_4_ShiftTeam_Shown(object sender, EventArgs e)
        {
            Dtp_SearchDate.Value = DateTime.Now;
            Lbl_NowMonth.Text = Dtp_SearchDate.Value.ToString("yyyy 年 MM 月");
        }

        private void Fun_InitialAuthority()
        {
            //控制项 权限
            CtlAuthority = new Control[] {
                                            Dtp_SatrtDate,Cob_Shift,Cob_Team,Btn_SetShiftTeam                                            
                                         };
        }

        // ComboBox 
        private void Fun_InitialComboBox()
        {
            PublicComm.portal.UIHand.Fun_ComboBoxSet("ToAll", "Cob_Shift", Cob_Shift, 0);//班次 1234
            PublicComm.portal.UIHand.Fun_ComboBoxSet("ToAll", "Cob_Team", Cob_Team, 0);//班別  甲乙丙丁
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            Fun_Search();
        }

        private void Fun_Search()
        {
            //查詢條件空白，不執行後續程式
            if (string.IsNullOrEmpty(Dtp_SearchDate.Value.ToString())) { return; }

            string strS_Date = Dtp_SearchDate.Value.ToString("yyyy-MM");

            //SUBSTRING(Finish_Time, 1,10)  BETWEEN '" + strDateTimeStart + "' AND '" + strDateTimeEnd + "' 
            //依條件查詢資料

            string strSql = $"SELECT * FROM APLA_ShiftTeam WHERE   day Like '{ strS_Date }%' ";//name, password, description, Updated, UpdatedProc 
            dtShiftTeam = PublicComm.portal.DbHand.Fun_DataTable(strSql, PublicSystem.StrConn);
           
            if (!PublicComm.portal.UIHand.Fun_IsDataTableNull(dtShiftTeam))
            {
                //清除栏位资料
                Fun_DataClear();

                Lbl_NowMonth.Text = Dtp_SearchDate.Value.ToString("yyyy 年 MM 月");
                Lbl_NowShift.Text = "";
                Lbl_NowTeam.Text = "";

              
                foreach (DataRow dr in dtShiftTeam.Rows)
                {
                    int intDay = Convert.ToInt32(dr[nameof(APL.TableClass.APLA_ShiftTeam.day)].ToString().Remove(0,6));
                    string strShift = Fun_ChangeShift(dr[nameof(APL.TableClass.APLA_ShiftTeam.shift)].ToString());

                    foreach (DataColumn dc in dtShiftTeam.Columns)
                    {
                        if (dc.ColumnName == "team")
                        {
                            ((Label)(Pnl_Group.Controls.Find("lbl_" + intDay + "_" + strShift, false)[0])).Text = Fun_TeamChangeWord(dr[dc].ToString());
                        }
                            
                    }
                }           

            }
            else
            {
                //清除栏位资料
                Fun_DataClear();
            }

        }

      
        private void Btn_SetShiftTeam_Click(object sender, EventArgs e)
        {
            Fun_DataClear();

            //if (PublicComm.portal.UIHand.Fun_IsDataTableNull(dtShiftTeam)) { return; }

            //修改前先備份資料
            if (!PublicComm.portal.UIHand.Fun_IsDataTableNull(dtShiftTeam))
            {
                //先備份之前顯示的資料
                dtBeforeEdit = dtShiftTeam.Copy();
            }
            else
            {
                //MessageBox.Show("尚未選取資料！");
                //return;
            }
            

            Fun_SetShiftTeam();

           

        }


        private void Button1_Click(object sender, EventArgs e)
        {
            Fun_DataClear();
        }

        private void Fun_DataClear()
        {
            for (int i = 1; i <= 31; i++)
            {
                ((Label)(Pnl_Group.Controls.Find("lbl_" + i + "_N", false)[0])).Text = "";
                ((Label)(Pnl_Group.Controls.Find("lbl_" + i + "_M", false)[0])).Text = "";
                ((Label)(Pnl_Group.Controls.Find("lbl_" + i + "_A", false)[0])).Text = "";
                ((Label)(Pnl_Group.Controls.Find("lbl_" + i + "_R", false)[0])).Text = "";
            }

            Lbl_NowMonth.Text = "　   　年   　月";
            Lbl_NowShift.Text = "";
            Lbl_NowTeam.Text = "";

        }

      
        private void Fun_SetShiftTeam()
        {

            DataTable dtSelectOne = new DataTable();
            dtSelectOne = dtShiftTeam.Clone();

            // 排班模式:做六休二 Nud_Work  Nud_Rest
            int intWork = Convert.ToInt32(Nud_Work.Value);
            int intRest = Convert.ToInt32(Nud_Rest.Value);

            // 班次:Shift  1-夜，2-早，3-中，4-休
            //string[] strArr_Shift = new string[] {"N","M","A","R" };
            int intShift = Cob_Shift.SelectedIndex;
            Fun_SetShiftArray(intShift);

            // 班别:Team   A-甲，B-乙，C-丙，D-丁
            //string[] strArr_Team = new string[] {"A","B","C","D" };           
            int intTeam = Cob_Team.SelectedIndex;
            Fun_SetTeamArray(intTeam);

            // 排班開始日期:Dtp_SatrtDate
            DateTime dateTime_S = Dtp_SatrtDate.Value;

            //取得 年 月 日
            int intYear = dateTime_S.Year;
            int intMonth = dateTime_S.Month; 
            int intDays = dateTime_S.Day;
            //取得 該月有幾天
            int intDayInMonth = DateTime.DaysInMonth(intYear, intMonth);

            int intCount_D = 0;
            int intCount_S = 0;
            int intCount_T = 0;

            DateTime dTimeUpdated = DateTime.Now;

            //夜:lbl_1_N     早:lbl_1_M     中:lbl_1_A    休:lbl_1_R

            // 每日 ABCD
            for (int i = intDays; i <= intDayInMonth; i++)
            {
                intCount_D += 1;               

                for (int r = 0; r < strArr_Shift.Length; r++)
                {
                    if (intCount_S > 3) { intCount_S = 0; }
                    for (int j = 0; j < strArr_Team.Length; j++)
                    {
                        //if (intCount_T > 3) { intCount_T = 0; }
                        ((Label)(Pnl_Group.Controls.Find("lbl_" + i + "_" + strArr_Shift[r] , false)[0])).Text = Fun_TeamChangeWord(strArr_Team[j].ToString()) ;

                        DataRow drAry = dtSelectOne.NewRow();
                        drAry[nameof(APL.TableClass.APLA_ShiftTeam.day)] = intYear.ToString() + intMonth.ToString().PadLeft(2, '0') + i.ToString().PadLeft(2,'0') ;
                        drAry[nameof(APL.TableClass.APLA_ShiftTeam.shift)] = Fun_ChangeShift(strArr_Shift[r].ToString()) ;
                        drAry[nameof(APL.TableClass.APLA_ShiftTeam.team)] = strArr_Team[j].ToString();
                        drAry[nameof(APL.TableClass.APLA_ShiftTeam.Updated)] = dTimeUpdated;
                        drAry[nameof(APL.TableClass.APLA_ShiftTeam.UpdatedProc)] = "HMI";
                        dtSelectOne.Rows.Add(drAry);

                        r += 1;
                        intCount_T += 1;
                    }
                    intCount_S += 1;
                }

                if ((intCount_D % intRest) == 0)
                {
                    if (intTeam < 3)
                    {
                        intTeam += 1;
                        Fun_SetTeamArray(intTeam);
                    }
                    else
                    {
                        intTeam = 0;
                        Fun_SetTeamArray(intTeam);
                    }
                    intCount_D = 0;
                }

            }

            //PRIMARY KEY
            string[] strArr = { nameof(APL.TableClass.APLA_ShiftTeam.day), nameof(APL.TableClass.APLA_ShiftTeam.shift) };

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"DELETE FROM {nameof(APL.TableClass.APLA_ShiftTeam)}");
                sb.AppendLine($"WHERE {nameof(APL.TableClass.APLA_ShiftTeam.day)} Like N'{intYear.ToString() + intMonth.ToString().PadLeft(2, '0') }%' ");
                sb.AppendLine("");
             

                string strDelElem = sb.ToString();
                PublicComm.portal.DbHand.Fun_GetObject(strDelElem, PublicSystem.StrConn);

                string strInsElem_New = PublicComm.portal.DbHand.Fun_GetInsertSqlFromDataTable(dtSelectOne, nameof(APL.TableClass.APLA_ShiftTeam));
                PublicComm.portal.DbHand.Fun_InsertData(strInsElem_New, PublicSystem.StrConn);
            }
            catch
            {

            }






            #region Old Loop

            //for (int i = intDays; i <= intDayInMonth; i++)
            //{
            //    intCount_D += 1;
            //    foreach (string r in strArr_Shift)
            //    {
            //        intCount_S += 1;
            //        foreach (string j in strArr_Team)
            //        {
            //            intCount_T += 1;
            //            strT = j;
            //            ((Label)(Pnl_Group.Controls.Find("lbl_" + i + "_" + r + "", false)[0])).Text = strT;
            //            //break;
            //        }
            //    }
            //}

            //for (int i = intDays; i <= intDayInMonth; i++)
            //{
            //    for (int r = 0; r < strArr_Shift.Length; r++)
            //    {
            //        if (intCount_S > 3) { intCount_S = 0; }
            //        for (int j = 0; j < strArr_Team.Length; j++)
            //        {
            //            if (intCount_T > 3) { intCount_T = 0; }
            //            ((Label)(Pnl_Group.Controls.Find("lbl_" + i + "_" + strArr_Shift[intCount_S] + "", false)[0])).Text = strArr_Team[intCount_T].ToString();

            //            r += 1;

            //            intCount_T += 1;
            //        }
            //        intCount_S += 1;
            //    }
            //    intCount_D += 1;
            //}

            //// [第一天 ABCD 第二天 空白 ]重複
            //for (int i = intDays; i <= intDayInMonth; i++)
            //{
            //    if (intCount_D % 2 == 0) { intCount_S = 0; intCount_T = 0; }
            //    for (int r = 0; r < strArr_Shift.Length; r++)
            //    {

            //        for (int j = intCount_T; j < strArr_Team.Length; j++)
            //        {

            //            //if (intCount_T > 3) { intCount_T = 0; }
            //            ((Label)(Pnl_Group.Controls.Find("lbl_" + i + "_" + strArr_Shift[r], false)[0])).Text = strArr_Team[j].ToString();
            //            r += 1;
            //            intCount_T += 1;
            //            //if (intCount_S > 3) { intCount_S = 0; }
            //        }
            //        intCount_S += 1;

            //    }
            //    intCount_D += 1;
            //}


            //for (int i = intDays; i <= intDayInMonth; i++)
            //{
            //    intCount_D += 1;

            //    for (int r = 0; r < strArr_Shift.Length; r++)
            //    {
            //        intCount_S += 1;

            //        for (int j = 0; j < strArr_Team.Length; j++)
            //        {
            //            if (intCount_D == 2)
            //            {
            //                //r++;
            //                intCount_D = 1;
            //                continue;
            //            }
            //            ((Label)(Pnl_Group.Controls.Find("lbl_" + i + "_" + strArr_Shift[r] + "", false)[0])).Text = strArr_Team[j].ToString();

            //            r += 1;

            //        }

            //    }

            //}

            //// 每日 ABCD
            //for (int i = intDays; i <= intDayInMonth; i++)
            //{
            //    for (int r = 0; r < strArr_Shift.Length; r++)
            //    {
            //        for (int j = 0; j < strArr_Team.Length; j++)
            //        {
            //            ((Label)(Pnl_Group.Controls.Find("lbl_" + i + "_" + strArr_Shift[r] + "", false)[0])).Text = strArr_Team[j].ToString();
            //            r += 1;
            //        }
            //        intCount_S += 1;
            //    }
            //}

            #endregion

        }

        //班別
        private void Fun_SetShiftArray(int intShift)
        {          
            switch (intShift)
            {
                case 0: strArr_Shift = new string[] { "N", "M", "A", "R" }; break;
                case 1: strArr_Shift = new string[] { "M", "A", "R", "N" }; break;
                case 2: strArr_Shift = new string[] { "A", "R", "N", "M" }; break;
                case 3: strArr_Shift = new string[] { "R", "N", "M", "A" }; break;
            }            
        }
        
        //班次
        private void Fun_SetTeamArray(int intTeam)
        {           
            switch (intTeam)
            {
                case 0:strArr_Team = new string[] { "A", "B", "C", "D" };break;
                case 1:strArr_Team = new string[] { "B", "C", "D", "A" };break;
                case 2:strArr_Team = new string[] { "C", "D", "A", "B" };break;
                case 3:strArr_Team = new string[] { "D", "A", "B", "C" };break;
            }

        }

        //
        private string Fun_ChangeShift(string strShift)
        {
            string strNewShift = "R";           
            switch (strShift)
            {
                case "1": strNewShift = "N"; break;
                case "2": strNewShift = "M"; break;
                case "3": strNewShift = "A"; break;
                case "4": strNewShift = "R"; break;

                case "N": strNewShift = "1"; break;
                case "M": strNewShift = "2"; break;
                case "A": strNewShift = "3"; break;
                case "R": strNewShift = "4"; break;

                default: strNewShift = ""; break;
            }
            return strNewShift;
        }

        private string Fun_TeamChangeWord(string team)
        {
            string strChangeWord = "";

            switch (team)
            {
                case "A":
                    strChangeWord = "甲";
                    break;
                case "B":
                    strChangeWord = "乙";
                    break;
                case "C":
                    strChangeWord = "丙";
                    break;
                case "D":
                    strChangeWord = "丁";
                    break;
            }

            return strChangeWord;
        }

    }
}
