using DBService.Repository.WorkSchedule;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CPL1HMI
{
    public partial class Frm_5_4_CrewMaintainance : Form
    {
        DataTable dtGetShift;
        DataTable dtBeforeEdit;
        //string[] strArr_Shift;
        //string[] strArr_Team;
        //語系
        private LanguageHandler LanguageHand;

        public Frm_5_4_CrewMaintainance()
        {
            InitializeComponent();
        }

        private void Frm_5_4_CrewMaintainance_Load(object sender, EventArgs e)
        {
            if (PublicForms.CrewMaintainance == null) PublicForms.CrewMaintainance = this;

            Control[] Frm_5_4_Control = new Control[] {
                Btn_ArrangeCrew
            };

            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_5_4_Control, UserSetupHandler.Instance.Frm_5_4);

            //ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Shift, Cob_Shift);
            //ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Team, Cob_Team);

            Dtp_SearchDate.Value = DateTime.Now;
            Fun_Search();

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Search_Click(object sender, EventArgs e)
        {
            Fun_Search();
        }

        /// <summary>
        /// 汇入Excel班表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Import_Click(object sender, EventArgs e)
        {
            string strEx_YearMonth = "";
            string strEx_Year = "";
            string strEx_Month = "";
            int intDayInMonth = 31;//預設31天

            //匯入排班
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Excel Worksheets|*.xls;*.xlsx"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var fileName = dialog.FileName;

                    if (!PublicForms.IsLoadExcel(fileName))
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"{fileName}正在开启中", "汇入排班",  2);
                        return;
                    }

                    PublicComm.ClientLog.Info($"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()}，汇入排班，汇入来源:{fileName}");
                    EventLogHandler.Instance.LogInfo("5-4", $"汇入排班", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()}，汇入排班，汇入来源:{fileName}。");

                    DataTable dtGetExcel = ShiftHandler.Instance.Fun_ExcelToDataTable(fileName);
                    string strSql = string.Empty;

                    if (dtGetExcel.IsNull())
                    {
                        DialogHandler.Instance.Fun_DialogShowOk("Excel转换结果无资料", "汇入排班",  3);
                        EventLogHandler.Instance.LogInfo("5-4", $"汇入排班", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()}，汇入排班，Excel转换结果无资料。");
                        return;
                    }

                    //List<string> importList = new List<string>();
                    int row = 0;
                    //int col = 0;
                    DataTable dtExTo = dtGetShift.Clone();

                    foreach (DataRow datarow in dtGetExcel.Rows)
                    {
                        row++;
                        if (row == 1)
                        {
                            if (!string.IsNullOrEmpty(datarow[2].ToString()))
                                strEx_Year = datarow[2].ToString();//取得汇入 年
                            if (!string.IsNullOrEmpty(datarow[4].ToString()))
                                strEx_Month = datarow[4].ToString();//取得汇入 月
                            strEx_YearMonth = $"{strEx_Year}{strEx_Month.PadLeft(2, '0')}";

                            if (!string.IsNullOrEmpty(strEx_Year) && !string.IsNullOrEmpty(strEx_Month))
                                intDayInMonth = DateTime.DaysInMonth(Convert.ToInt32(strEx_Year), Convert.ToInt32(strEx_Month));
                        }

                        if (row < 4) continue;//第4行開始取班表
                        if (row >= 4 + intDayInMonth) continue;// 超過 該月份天數不取值

                        // 7 = 晚2早2中2休1
                        for (int i = 1; i <= 7; i++)
                        {
                            DataRow dr = dtExTo.NewRow();
                            dr[nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftDate)] = $"{strEx_YearMonth}{datarow[0].ToString().PadLeft(2, '0')}";//yyyyMMdd

                            string strShift = "0";
                            switch (i)
                            {
                                //晚
                                case 1:
                                case 2: strShift = "1"; break;
                                //早
                                case 3:
                                case 4: strShift = "2"; break;
                                //中
                                case 5:
                                case 6: strShift = "3"; break;
                                //休
                                case 7: strShift = "4"; break;
                            }

                            dr[nameof(WorkScheduleEntity.TBL_WorkSchedule.Shift)] = strShift;
                            dr[nameof(WorkScheduleEntity.TBL_WorkSchedule.Team)] = datarow[1 + i].ToString();//ABCD
                            dr[nameof(WorkScheduleEntity.TBL_WorkSchedule.Mode)] = "1";//欄位暫時無用,先給預設值 1
                            dr[nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftStartTime)] = ShiftHandler.Instance.Fun_GetShiftTimeRange(i)[0];// "00:00"
                            dr[nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftEndTime)] = ShiftHandler.Instance.Fun_GetShiftTimeRange(i)[1];// "04:00"
                            dr[nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftPerson)] = PublicForms.Main.Lbl_LoginUser.Text.Trim();
                            //dr["CreateTime"] = DateTime.Now;  //資料表直接給值 getdate()
                            dtExTo.Rows.Add(dr);
                        }
                    }

                    if (dtExTo.Rows.Count.Equals(0))
                    {
                        DialogHandler.Instance.Fun_DialogShowOk("Excel资料转换异常", "汇入排班", 0);
                        EventLogHandler.Instance.LogInfo("5-4", $"汇入排班", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()}，汇入排班，Excel资料转换异常。");
                        return;
                    }

                    string strMessage = $"{strEx_Year}年{strEx_Month}月排班资料";
                    // 清空匯入的 yyyy 年 MM 月 排班 ---刪除資料庫(TBL_WorkSchedule)                    
                    if (ShiftHandler.Instance.Fun_DeleteShift(strEx_YearMonth))
                    {
                        //成功不跳视窗
                        // DialogHandler.Instance.Fun_DialogShowOk($"清除{strMessage}成功", "汇入排班", null,4);
                        //EventLogHandler.Instance.LogInfo("5-4", $"删除排班", $"清除{strMessage}，成功。");
                    }
                    else
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"清除{strMessage}失败", "汇入排班",  3);
                        EventLogHandler.Instance.LogInfo("5-4", $"汇入排班", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()}，汇入排班，清除{strMessage}失败。");
                    }

                    // 寫入資料庫(TBL_WorkSchedule)
                    if (ShiftHandler.Instance.Fun_InsertShift(dtExTo))
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"汇入 {strMessage}成功", "汇入排班",  4);
                        EventLogHandler.Instance.LogInfo("5-4", $"汇入排班", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()}，汇入{strMessage}，成功。");
                        EventLogHandler.Instance.EventPush_Message($"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()}，汇入 {strMessage}，成功。");
                    }
                    else
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"汇入{strMessage}失败", "汇入排班",  3);
                        EventLogHandler.Instance.LogInfo("5-4", $"汇入排班", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()}，汇入排班，汇入{strMessage}失败。");
                    }
                }
                catch (Exception ex)
                {
                    EventLogHandler.Instance.EventPush_Message($"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()}，汇入排班，失败:{ex}");
                    EventLogHandler.Instance.LogDebug("5-4", "汇入排班", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()}，汇入排班，失败:{ex}");
                    PublicComm.ClientLog.Debug($"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()}，汇入排班，失败:{ex}");
                    PublicComm.ExceptionLog.Debug($"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()}，汇入排班，失败:{ex}");
                    return;
                }
                Dtp_SearchDate.Value = new DateTime(Convert.ToInt32(strEx_Year), Convert.ToInt32(strEx_Month), 1);
                Fun_Search();
            }
        }
        //查询 实做
        private void Fun_Search()
        {
            //查詢條件空白，不執行後續程式
            if (Dtp_SearchDate.Value.ToString().IsEmpty()) 
            {
                DialogHandler.Instance.Fun_DialogShowOk("请选择查询条件","查询班表",0);
                return; 
            }
            string strS_Date = Dtp_SearchDate.Value.ToString("yyyyMM");
            int intYear = Convert.ToInt16(Dtp_SearchDate.Value.ToString("yyyy"));
            int intMonth = Convert.ToInt16(Dtp_SearchDate.Value.ToString("MM"));
            //依條件查詢資料
            string strSql = Frm_5_4_SqlFactory.Frm_5_4_SelectShift(strS_Date);
             dtGetShift = DataAccess.Fun_SelectDate(strSql, "查询班表");

            if (!dtGetShift.IsNull())
            {
                //清除栏位资料
                Fun_DataClear();
                //栏位给值
                ShiftHandler.Instance.Fun_ShiftLableDisplay(dtGetShift);
                #region 目前不使用 Mode 
                //string strMode = ShiftHandler.Instance.Fun_ModeWordChange(dtGetShift.Rows[0][nameof(WorkScheduleEntity.TBL_WorkSchedule.Mode)].ToString()); 
                //Lbl_NowMonth.Text = Dtp_SearchDate.Value.ToString("yyyy 年 MM 月");
                //Lbl_Mode_Show.Text = $"排班原则:{strMode}";
                //ShiftHandler.Instance.Fun_FindMode(strMode);
                //ShiftHandler.Instance.Fun_ShiftTimeDisplay();
                #endregion
                #region 搬到 ShiftHandler.Fun_ShiftLableDisplay(DataTable dt)
                //foreach (DataRow dr in dtGetShift.Rows)
                //{
                //    int intDay = Convert.ToInt32(dr[nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftDate)].ToString().Remove(0, 6));

                //    string strShift = Fun_ChangeShift(dr[nameof(WorkScheduleEntity.TBL_WorkSchedule.Shift)].ToString());

                //    foreach (DataColumn dc in dtGetShift.Columns)
                //    {
                //        if (dc.ColumnName.Equals(nameof(WorkScheduleEntity.TBL_WorkSchedule.Team)))
                //        {
                //            ((Label)(Pnl_Group.Controls.Find("Lbl_" + intDay + "_" + strShift, false)[0])).Text = Fun_TeamChangeWord(dr[dc].ToString());
                //        }
                //    }
                //}
                #endregion
            }
            else
            {
                //清除栏位资料
                Fun_DataClear();
                DialogHandler.Instance.Fun_DialogShowOk($"查询{intYear} / {intMonth}班表:\r\n因尚未建立{intYear} / {intMonth} 资料,无资料显示!", "查询班表", 0);
            }
            Lbl_NowMonth.Text = Dtp_SearchDate.Value.ToString("yyyy / MM ");
            //顯示查詢月份星期
          
            ShiftHandler.Instance.Fun_SettingDayOfWeek(intYear, intMonth);
        }
              
        //清空畫面欄位資料
        private void Fun_DataClear()
        {
            for (int i = 1; i <= 31; i++)
            {
                ((Label)(Pnl_Group.Controls.Find($"Lbl_Day_{i }", false)[0])).Text = "";//日期:1
                ((Label)(Pnl_Group.Controls.Find($"Lbl_Week_{i}", false)[0])).Text = "";//星期
                ((Label)(Pnl_Group.Controls.Find("Lbl_" + i + "_N", false)[0])).Text = string.Empty;
                ((Label)(Pnl_Group.Controls.Find("Lbl_" + i + "_M", false)[0])).Text = string.Empty;
                ((Label)(Pnl_Group.Controls.Find("Lbl_" + i + "_A", false)[0])).Text = string.Empty;
                ((Label)(Pnl_Group.Controls.Find("Lbl_" + i + "_R", false)[0])).Text = string.Empty;
            }

            Lbl_NowMonth.Text = "     /    "; //"　   　年   　月";
            //Lbl_Mode_Show.Text = $"排班原则:";
            //Lbl_Night.Text = Lbl_Night_1.Text = string.Empty;
            //Lbl_Morning.Text = Lbl_Morning_1.Text = string.Empty;
            //Lbl_Afternoon.Text = Lbl_Afternoon_1.Text = string.Empty;
        }

        #region 語系切換,文字調整 Fun_LanguageIsEn_Font
        private void Fun_LanguageIsEn_Font14_12(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)12, fs);
        }
        #endregion Fun_LanguageIsEn_Font End


        #region --- 保留 ---2022-08 停用排班功能 
        private void Btn_ArrangeCrew_Click(object sender, EventArgs e)
        {
            //Fun_DataClear();
            //if (Cob_Mode.Text.IsEmpty() || Cob_Team.Text.IsEmpty() || cobDays.Text.IsEmpty())
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk("请确认排班模式、班组及起始天数是否有正确选填", "排班", 0);
            //    return;
            //}
            //ShiftHandler.Instance.Fun_Shift(Dtp_SatrtDate.Value, Cob_Mode.Text, Cob_Team.SelectedValue.ToString(), cobDays.Text);
        }

        private string Fun_ChangeShift(string strShift)
        {
            string strNewShift;
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

                default: strNewShift = string.Empty; break;
            }
            return strNewShift;
        }

        private string Fun_TeamChangeWord(string team)
        {
            string strChangeWord = string.Empty;
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

        private void Btn_ModeSpare_Click(object sender, EventArgs e)
        {
            if (Pnl_Spare.Visible)
            {
                Pnl_Spare.Visible = false;
                return;
            }
            Pnl_Spare.Visible = true;
            Pnl_Spare.Size = new System.Drawing.Size(1085, 575);
            Pnl_Spare.Location = new System.Drawing.Point(560, 290);

        }

        private void Btn_ClosePanel_Click(object sender, EventArgs e)
        {
            Pnl_Spare.Visible = false;
        }
        //舊排班規則
        private void Fun_SetShiftTeam()
        {

            //DataTable dtSelectOne = new DataTable();
            //dtSelectOne = dtGetShift.Clone();

            //// 排班模式:做六休二 Nud_Work  Nud_Rest
            //int intWork = Convert.ToInt32(Nud_Work.Value);
            //int intRest = Convert.ToInt32(Nud_Rest.Value);

            //// 班次:Shift  1-夜，2-早，3-中，4-休
            ////string[] strArr_Shift = new string[] {"N","M","A","R" };
            //int intShift = Cob_Shift.SelectedIndex;
            //Fun_SetShiftArray(intShift);

            //// 班别:Team   A-甲，B-乙，C-丙，D-丁
            ////string[] strArr_Team = new string[] {"A","B","C","D" };           
            //int intTeam = Cob_Team.SelectedIndex;
            //Fun_SetTeamArray(intTeam);

            //// 排班開始日期:Dtp_SatrtDate
            //DateTime dateTime_S = Dtp_SatrtDate.Value;

            ////取得 年 月 日
            //int intYear = dateTime_S.Year;
            //int intMonth = dateTime_S.Month;
            //int intDays = dateTime_S.Day;

            ////取得 該月有幾天
            //int intDayInMonth = DateTime.DaysInMonth(intYear, intMonth);

            //int intCount_D = 0;
            //int intCount_S = 0;
            //int intCount_T = 0;

            //DateTime dTimeUpdated = DateTime.Now;

            ////夜:lbl_1_N     早:lbl_1_M     中:lbl_1_A    休:lbl_1_R

            //// 每日 ABCD
            //for (int i = intDays; i <= intDayInMonth; i++)
            //{
            //    intCount_D += 1;

            //    for (int r = 0; r < strArr_Shift.Length; r++)
            //    {
            //        if (intCount_S > 3)
            //        {
            //            intCount_S = 0;
            //        }

            //        for (int j = 0; j < strArr_Team.Length; j++)
            //        {
            //            ((Label)(Pnl_Group.Controls.Find("lbl_" + i + "_" + strArr_Shift[r], false)[0])).Text = Fun_TeamChangeWord(strArr_Team[j].ToString());

            //            DataRow drAry = dtSelectOne.NewRow();

            //            //todo 日期
            //            drAry[nameof(WorkScheduleModel.TBL_WorkSchedule.ShiftDate)] = intYear.ToString() + intMonth.ToString().PadLeft(2, '0') + i.ToString().PadLeft(2, '0');
            //            drAry[nameof(WorkScheduleModel.TBL_WorkSchedule.Shift)] = Fun_ChangeShift(strArr_Shift[r].ToString());
            //            drAry[nameof(WorkScheduleModel.TBL_WorkSchedule.Team)] = strArr_Team[j].ToString();
            //            drAry[nameof(WorkScheduleModel.TBL_WorkSchedule.CreateTime)] = GlobalVariableHandler.Instance.getTime;
            //            drAry[nameof(WorkScheduleModel.TBL_WorkSchedule.ShiftPerson)] = PublicForms.Main.lblLoginUser.Text;
            //            dtSelectOne.Rows.Add(drAry);

            //            r += 1;

            //            intCount_T += 1;
            //        }

            //        intCount_S += 1;

            //    }

            //    if ((intCount_D % intRest) == 0)
            //    {

            //        intTeam = intTeam < 3 ? intTeam + 1 : 0;

            //        Fun_SetTeamArray(intTeam);

            //        intCount_D = 0;
            //    }

            //}

            //string strSql = SqlFactory.Frm_5_4_DeleteOldWorkSchedule($"{intYear.ToString() + intMonth.ToString().PadLeft(2, '0') }");

            //if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_CPL, "删除排班", "5-4"))
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk("删除排班失敗", "删除排班", 3);

            //    return;
            //}

            //string strInsElem_New = SqlFactory.Fun_GetInsertSqlFromDataTable(dtSelectOne, nameof(WorkScheduleModel.TBL_WorkSchedule));

            //if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_CPL, "新增排班", "5-4"))
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk("新增排班失敗", "新增排班", 3);

            //    return;
            //}

        }

        //班別
        private void Fun_SetShiftArray(int intShift)
        {
            //switch (intShift)
            //{
                //case 0: strArr_Shift = new string[] { "N", "M", "A", "R" }; break;
                //case 1: strArr_Shift = new string[] { "M", "A", "R", "N" }; break;
                //case 2: strArr_Shift = new string[] { "A", "R", "N", "M" }; break;
                //case 3: strArr_Shift = new string[] { "R", "N", "M", "A" }; break;
            //}
        }

        //班次
        private void Fun_SetTeamArray(int intTeam)
        {
            //switch (intTeam)
            //{
            //    case 0: strArr_Team = new string[] { "A", "B", "C", "D" }; break;
            //    case 1: strArr_Team = new string[] { "B", "C", "D", "A" }; break;
            //    case 2: strArr_Team = new string[] { "C", "D", "A", "B" }; break;
            //    case 3: strArr_Team = new string[] { "D", "A", "B", "C" }; break;
            //}

        }

        #endregion

        #region 搬到ShiftHandler
        public void Fun_SettingDayOfWeek(int Year, int Month)
        {
            int intDayInMonth = DateTime.DaysInMonth(Year, Month);

            for (int days = 1; days <= intDayInMonth; days++)
            {
                DateTime Date = new DateTime(Year, Month, days);
                ((Label)(Pnl_Group.Controls.Find($"Lbl_Week_{days}", false)[0])).Text = Fun_WeekEnToZH(Date.DayOfWeek.ToString());

                ((Label)(Pnl_Group.Controls.Find($"Lbl_Day_{days}", false)[0])).Text = days.ToString();//日期:1

                ////======進階版===============
                //Label lblCtr = (Label)(Pnl_Group.Controls.Find($"lbl_Week_{days}", false)[0]);
                //string strWeek = Date.AddDays(days).DayOfWeek.ToString();
                ////Change Text
                //lblCtr.Text = Fun_WeekEnToZH(strWeek);

                ////Change ForeColor
                //if (strWeek == "Saturday")
                //    lblCtr.ForeColor = Color.Green;
                //else if (strWeek == "Sunday")
                //    lblCtr.ForeColor = Color.Red;
                //else
                //    lblCtr.ForeColor = Color.Black;
                ////======進階版End===============

                Fun_SetLableVisible(days, true);
            }

            //多餘的欄位 => Visible = false 
            for (int intDayLbl = 31; intDayLbl > intDayInMonth; intDayLbl--)
            {
                Fun_SetLableVisible(intDayLbl, false);
            }
        }

        private void Fun_SetLableVisible(int intLbl, bool bolShow)
        {
            ((Label)(Pnl_Group.Controls.Find($"Lbl_Day_{intLbl }", false)[0])).Visible = bolShow;//日期:1
            ((Label)(Pnl_Group.Controls.Find($"Lbl_Week_{intLbl}", false)[0])).Visible = bolShow;//星期
            ((Label)(Pnl_Group.Controls.Find($"Lbl_{intLbl }_N", false)[0])).Visible = bolShow;//夜
            ((Label)(Pnl_Group.Controls.Find($"Lbl_{intLbl }_M", false)[0])).Visible = bolShow;//早
            ((Label)(Pnl_Group.Controls.Find($"Lbl_{intLbl }_A", false)[0])).Visible = bolShow;//中
            ((Label)(Pnl_Group.Controls.Find($"Lbl_{intLbl}_R", false)[0])).Visible = bolShow;//休
        }
        private string Fun_WeekEnToZH(string strWeek)
        {
            switch (strWeek)
            {
                case "Monday": strWeek = "星期一"; break;
                case "Tuesday": strWeek = "星期二"; break;
                case "Wednesday": strWeek = "星期三"; break;
                case "Thursday": strWeek = "星期四"; break;
                case "Friday": strWeek = "星期五"; break;
                case "Saturday": strWeek = "星期六"; break;
                case "Sunday": strWeek = "星期日"; break;

                    //case "Monday": strWeek = userUICulture.Equals(LanguageHandler.English) ? "Mon" : "星期一"; break;
                    //case "Tuesday": strWeek = userUICulture.Equals(LanguageHandler.English) ? "Tues" : "星期二"; break;
                    //case "Wednesday": strWeek = userUICulture.Equals(LanguageHandler.English) ? "Wed" : "星期三"; break;
                    //case "Thursday": strWeek = userUICulture.Equals(LanguageHandler.English) ? "Thur" : "星期四"; break;
                    //case "Friday": strWeek = userUICulture.Equals(LanguageHandler.English) ? "Fri" : "星期五"; break;
                    //case "Saturday": strWeek = userUICulture.Equals(LanguageHandler.English) ? "Sat" : "星期六"; break;
                    //case "Sunday": strWeek = userUICulture.Equals(LanguageHandler.English) ? "Sun" : "星期日"; break;
            }
            return strWeek;
        }
        #endregion

        private void Btn_Search_TextChanged(object sender, EventArgs e)
        {
            //特別處理:中英切換時,重新取得星期X
            Fun_Search();
        }
    }
}
