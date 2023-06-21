using Akka.Actor;
using CPL1HMI.comm;
using DataModel.HMIServerCom.Msg;
using DBService.Repository.DelayLocation;
using DBService.Repository.DelayReasonCode;
using DBService.Repository.LineFaultRecords;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CPL1HMI
{
    public partial class frm_4_1_LineDelayRecord : Form
    {
        #region 變數
        DataTable dtGetRecord;        
        DataTable dtSelectOne;
        DataTable dtBeforEdit;

        bool bolEditStatus = false;
        enum TurnDelayFlag
        {
            Insert = 1,
            Update = 2,
            Delete = 3
        }

        //語系
        private LanguageHandler LanguageHand;
        #endregion

        public frm_4_1_LineDelayRecord()
        {
            InitializeComponent();
        }
        private void Frm_4_1_LineDelayRecord_Load(object sender, EventArgs e)
        {
           // LanguageHandler.Instance.Fun_CurrentLanguage(this.Name);
            if (PublicForms.LineDelayRecord == null) PublicForms.LineDelayRecord = this;

            Control[] Frm_4_1_Control = new Control[] {
                Btn_New,
                Btn_Save,
                Btn_Edit,
                Btn_Delete
            };

            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_4_1_Control, UserSetupHandler.Instance.Frm_4_1);
            Nud_Stop_Time.Value = 3;
            Fun_Control(Grb_DataCol, false);

            Fun_GetDownTime();

            Fun_InitialComboBox();

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }

        private void Fun_InitialComboBox()
        {
            //停機位置
            //Fun_SettingComboBox(Cob_delay_location, Fun_SelectStopLocation(), nameof(DelayLocationModel.TBL_DelayLocation_Definition.Delay_LocationCode), nameof(DelayLocationModel.TBL_DelayLocation_Definition.Delay_LocationName));
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Fun_SelectStopLocation(), Cob_delay_location);
            //停機代碼
            //Fun_SettingComboBox(Cob_delay_reason_code, Fun_SelectReasonCode(), nameof(DelayReasonCodeModel.TBL_DelayReasonCode_Definition.Delay_ReasonCode), nameof(DelayReasonCodeModel.TBL_DelayReasonCode_Definition.Delay_ReasonName));
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Fun_SelectReasonCode(), Cob_delay_reason_code);

            //降速代碼
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Deceleration, Cob_deceleration_code);

            //故障代碼
            //Fun_SettingComboBox(Cob_FaultCode, Fun_SelectFaultCode(), nameof(FaultCodeModel.TBL_FaultCode.Fault_Code), nameof(FaultCodeModel.TBL_FaultCode.Fault_Description));

            //班次
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Shift, Cob_shift_no);
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Shift, Cob_prod_shift_no);

            //班別
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Team, Cob_shift_group);
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Team, Cob_prod_shift_group);
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {           
            Fun_GetDownTime();

            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_DataCol, true);
        }

        /// <summary>
        /// 搜尋所有停復機紀錄
        /// </summary>
        public void Fun_GetDownTime()
        {
            if (Dtp_Start_Time.DateTimeRangeIsFail(Dtp_Finish_Time.Value))
            {
                EventLogHandler.Instance.EventPush_Message($"日期区间起讫时间不正确，请重新确认");
                return;
            }
            try
            {
                string strSql = Frm_4_1_SqlFactory.SQL_Select_LineFaultRecord();

                dtGetRecord = DataAccess.Fun_SelectDate(strSql, "停复机记录");
                dtSelectOne = dtGetRecord.Clone();
                dtBeforEdit = dtGetRecord.Clone();
            }
            catch (Exception ex)
            {

            }

            Fun_Dt_UploadMMS_ChangeShow(dtGetRecord, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS), 1);

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_Info, dtGetRecord);
            Frm_4_1_ColumnsHandler.Instance.Frm_4_1_LineDelayRecord(Dgv_Info);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_Info);

            if (dtGetRecord.IsNull())
            {
                //DialogHandler.Instance.Fun_DialogShowOk("查无停复机记录", "查询停复机记录", 0);
                Btn_Edit.Enabled = false;
            }
            else
            {
                string GetEndTime = string.Empty;

                for (int Index = 0; Index < dtGetRecord.Rows.Count; Index++)
                {
                    GetEndTime = Dgv_Info.Rows[Index].Cells[5].Value.ToString();

                    if (GetEndTime.Equals("1970/1/1") || GetEndTime.Equals("1970/1/1 上午 12:00:00"))
                    {
                        Dgv_Info.Rows[Index].Cells[5].Value = string.Empty;
                        //Dgv_Info.Rows[Index].Cells[5].Style.BackColor = Color.Red;
                    }
                }
            }
            //檢查 是否有 正確的停機結束時間,如果沒有則不開放編輯
            if(Dgv_Info.CurrentRow != null)
            {
                string strStop_End_Time = Dgv_Info.CurrentRow.Cells[5].Value.ToString();
                Btn_Edit.Enabled = Fun_CheckStop_End_Time_Null(strStop_End_Time);
            }
            else
            {
                Btn_Edit.Enabled = false;
            }
            
        }

        /// <summary>
        /// 停機位置選項
        /// </summary>
        private DataTable Fun_SelectStopLocation()
        {
            string strSql = Frm_4_1_SqlFactory.SQL_Select_DelayLocation();

            DataTable dtGetLocation = DataAccess.Fun_SelectDate(strSql, "停机位置");

            return dtGetLocation;
        }

        /// <summary>
        /// 停機原因代碼選項
        /// </summary>
        /// <returns></returns>
        private DataTable Fun_SelectReasonCode()
        {
            string strSql = Frm_4_1_SqlFactory.SQL_Select_DelayReasonCode();

            DataTable dtGetReason = DataAccess.Fun_SelectDate(strSql, "停机原因代码");

            return dtGetReason;
        }

        /// <summary>
        /// 故障代碼選項
        /// </summary>
        /// <returns></returns>
        private DataTable Fun_SelectFaultCode()
        {
            string strSql = Frm_4_1_SqlFactory.SQL_Select_FaultCode();

            DataTable dtGetReason = DataAccess.Fun_SelectDate(strSql, "故障代码");

            return dtGetReason;
        }

        /// <summary>
        /// 設定ComboBox
        /// </summary>
        /// <param name="cbo"></param>
        /// <param name="dt"></param>
        /// <param name="strValue"></param>
        /// <param name="strDisplay"></param>
        private void Fun_SettingComboBox(ComboBox cbo, DataTable dt, string strValue, string strDisplay)
        {
            if (dt.IsNull()) return;

            foreach (DataRow Row in dt.Rows)
            {
                cbo.Items.Add($"[{Row[strValue]}]{Row[strDisplay]}");
            }
        }

        private void Dgv_Info_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0) { return; }//列标题不响应CellClick事件
            if (Dgv_Info.Rows.Count.Equals(0)) { return; }
            if (Dgv_Info.CurrentRow == null) { return; }
            if (bolEditStatus) { return; }

            //DataRow drGetCurrentRow = dtGetRecord.Rows[Dgv_Info.CurrentRow.Index];
            DataRow dr = Fun_GetDataRowFromCurrentRow(Dgv_Info, dtGetRecord);
            DataTable dt = dtGetRecord.Clone();
            try
            {
                dt.LoadDataRow(dr.ItemArray, false);
            }
            catch { return; }

            ////HT
            //var dr = (Dgv_Info.CurrentRow.DataBoundItem as DataRowView).Row;
            //var dt = dr.Table.Clone();
            //dt.ImportRow(dr);

            Fun_SetSelectOne( dt);

            //檢查 是否有 正確的停機結束時間,如果沒有則不開放編輯
            string strStop_End_Time = Dgv_Info.CurrentRow.Cells[5].Value.ToString();
            Btn_Edit.Enabled = Fun_CheckStop_End_Time_Null(strStop_End_Time);

        }

        /// <summary>
        /// 將選中的 DataGridViewRow 轉成 DataRow.
        /// </summary>
        /// <param name="dgv">來源DataGridView</param>
        /// <param name="dt">來源DataTable</param>
        /// <returns></returns>
        public DataRow Fun_GetDataRowFromCurrentRow(DataGridView dgv, DataTable dt)
        {
            if (dgv.CurrentRow == null) { return null; }
            if (dgv.SelectedRows.Count <= 0) { return null; }
            DataRowView drv = dgv.SelectedRows[0].DataBoundItem as DataRowView;
            int index = dt.Rows.IndexOf(drv.Row);
            DataRow dr = dt.Rows[index];

            return dr;
        }

        //檢查 是否有 正確的停機結束時間,如果沒有則不開放編輯
        private bool Fun_CheckStop_End_Time_Null(string strStop_End_Time)
        {
            if (string.IsNullOrEmpty(strStop_End_Time) || strStop_End_Time.StartsWith("1970") || strStop_End_Time.StartsWith("0001") || strStop_End_Time.StartsWith("999"))
            {
                return false;
            }
            else
            {
                return true;
            }     
        }


        private void Fun_SetSelectOne(DataTable dt)
        {
            if (dt != null && dt.Rows.Count != 0) 
            {
                dtSelectOne = dt.Copy();

                //機組號
                Txt_UnitCode.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.unit_code));//dtSelectOne.Rows[0][nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.unit_code)].ToString();
                //日期
                Dtp_prod_time.Value = DateTime.Parse(Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time)));
                //班次
                Cob_prod_shift_no.SelectedValue = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_no));//dtSelectOne.Rows[0][nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_no)].ToString().Trim();
                //班別
                Cob_prod_shift_group.SelectedValue = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_group));//dtSelectOne.Rows[0][nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_group)].ToString().Trim();

                //停機位置
                //Cob_delay_location.SelectedIndex = Cob_delay_location.FindString(drGetCurrentRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location)].ToString());
                Cob_delay_location.SelectedValue = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location));//dtSelectOne.Rows[0][nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location)].ToString().Trim();
                //停機代碼
                // Cob_delay_reason_code.SelectedIndex = Cob_delay_reason_code.FindString(drGetCurrentRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_code)].ToString());
                Cob_delay_reason_code.SelectedValue = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_code));//dtSelectOne.Rows[0][nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_code)].ToString().Trim();


                //降速代碼
                Cob_deceleration_code.SelectedValue = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_code));// dtSelectOne.Rows[0][nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_code)].ToString().Trim();
               
                //故障代碼
                //Cob_FaultCode.SelectedIndex = Cob_FaultCode.FindString(drGetCurrentRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.Fault_Code)].ToString());

                //停機備註
                Txt_Remark.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_remark));//dtSelectOne.Rows[0][nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_remark)].ToString();

                //停機開始時間
                if (!Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)).IsEmpty() ||
                    !Dgv_Info.CurrentRow.Cells[4].Value.ToString().IsEmpty())
                {
                    Dtp_stop_start_time.Value = (DateTime)dtSelectOne.Rows[0][nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)];
                    //Dtp_stop_start_time.Value = DateTime.Parse(Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)));
                }

                //停機結束時間
                if (!Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)).IsEmpty() &&
                    !Dgv_Info.CurrentRow.Cells[5].Value.ToString().IsEmpty())
                {
                    string GetEndTime = Dgv_Info.CurrentRow.Cells[5].Value.ToString();

                    if (GetEndTime.Equals("0001/1/1") || GetEndTime.Equals("0001/1/1 上午 12:00:00"))
                    {
                        Dtp_stop_end_time.Value = DateTimePicker.MaximumDateTime;//DateTime.Parse("1970/1/1 上午 12:00:00"); 
                    }
                    else
                    {
                        Dtp_stop_end_time.Value = (DateTime)dtSelectOne.Rows[0][nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)];
                        //Dtp_stop_end_time.Value = DateTime.Parse(Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)));
                    }
                }
                else
                {
                    Dtp_stop_end_time.Value = DateTimePicker.MaximumDateTime;//DateTime.Parse("1970/1/1 上午 12:00:00"); 
                }

                //停機持續時間(min)
                Txt_Stop_Elased_Time.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey));// dtSelectOne.Rows[0][].ToString();
                //停機位置描述
                Txt_delay_location_desc.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location_desc));// dtSelectOne.Rows[0][].ToString();
                //停機原因描述
                Txt_delay_reason_desc.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_desc));// dtSelectOne.Rows[0][].ToString();
                //機械部門原因停機時間(min)
                Txt_Resp_Depart_Delay_Time_m.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_m));// dtSelectOne.Rows[0][].ToString();
                //電氣部門原因停機時間(min)         
                Txt_Resp_Depart_Delay_Time_e.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_e));// dtSelectOne.Rows[0][].ToString();
                //L3原因停機時間(min)                                                                              
                Txt_Resp_Depart_Delay_Time_c.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_c));// dtSelectOne.Rows[0][].ToString();
                //生產部門原因停機時間(min)                                                                          
                Txt_Resp_Depart_Delay_Time_p.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_p));// dtSelectOne.Rows[0][].ToString();
                //正常停機時間(min)                                                                                  
                Txt_Resp_Depart_Delay_Time_u.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_u));// dtSelectOne.Rows[0][].ToString();
                //其它部門原因停機時間(min)                                                                          
                Txt_Resp_Depart_Delay_Time_o.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_o));// dtSelectOne.Rows[0][].ToString();
                //換輥原因停機時間(min)                                                                              
                Txt_Resp_Depart_Delay_Time_r.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_r));// dtSelectOne.Rows[0][].ToString();
                //磨輥原因停機時間(min)            
                Txt_Resp_Depart_Delay_Time_rs.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_rs));// dtSelectOne.Rows[0][].ToString();

            }
            else
            {              
                Fun_ControlClear();
            }

        }

        private string Fun_TryDtGetString(DataTable dt , string strCol)
        {
            string strGet = "";
            if (dtSelectOne != null && dtSelectOne.Rows.Count > 0)
            {
                try
                {
                    strGet = dt.Rows[0][strCol].ToString().Trim();
                }
                catch (ArgumentException ae)
                {
                    DialogHandler.Instance.Fun_DialogShowOk(ae.Message, "错误资讯", 3);
                    strGet = "";
                }
            }
               
            return strGet;
        }

        private string Fun_TryDgvGetString(DataGridView dgv,int intRows ,string strCol)
        {
            string strGet = "";
            try
            {
                strGet = dgv.Rows[intRows].Cells[strCol].Value.ToString().Trim();
                //Dgv_Info.Rows[e.RowIndex].Cells[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS)].Value.ToString().Trim();
            }
            catch (ArgumentException ae)
            {
                DialogHandler.Instance.Fun_DialogShowOk(ae.Message, "错误资讯", 3);
                strGet = "";
            }
            return strGet;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Edit_Click(object sender, EventArgs e)
        {
            //檢查 是否已是編輯狀態 
            if (bolEditStatus) { return; }

            #region 编辑前检查目前栏位上显示的資訊與资料库内是否相符

            if (dtSelectOne != null && dtSelectOne.Rows.Count > 0)
            {

                //檢查 是否有 正確的停機結束時間,如果沒有則不開放編輯
                string strStop_End_Time = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)); //dtSelectOne.Rows[0]["stop_end_time"].ToString();
                if (!Fun_CheckStop_End_Time_Null(strStop_End_Time))
                {
                    DialogHandler.Instance.Fun_DialogShowOk("无停机结束时间，不可编辑！", "提示资讯", 0);
                    return;
                }


                //編輯前先檢查是否有這筆資料
                string strProd_time = Dtp_prod_time.Value.ToString("yyyy-MM-dd");//日期
                string strStart_time = Dtp_stop_start_time.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");//停機開始時間
                string strEnd_time = Dtp_stop_end_time.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");//停機結束時間          
                string strShift_no = Cob_prod_shift_no.SelectedValue == null ? Cob_prod_shift_no.Text : Cob_prod_shift_no.SelectedValue.ToString();//班次
                string strShift_group = Cob_prod_shift_group.SelectedValue == null ? Cob_prod_shift_group.Text : Cob_prod_shift_group.SelectedValue.ToString();//班組

                StringBuilder sbFindSql = new StringBuilder();
                sbFindSql.AppendLine(" SELECT * ");
                sbFindSql.AppendLine(" FROM   TBL_LineFaultRecords ");
                sbFindSql.AppendLine($" WHERE prod_time = N'{strProd_time}' ");
                sbFindSql.AppendLine($" AND prod_shift_no = N'{strShift_no}' ");
                sbFindSql.AppendLine($" AND prod_shift_group = N'{strShift_group}' ");
                sbFindSql.AppendLine($" AND stop_start_time = N'{strStart_time}' ");
                sbFindSql.AppendLine($" AND stop_end_time = N'{strEnd_time}' ");
               

                string strFindSql = sbFindSql.ToString();
                DataTable dtCheckData = DataAccess.Fun_SelectDate(strFindSql, "停机资料确认");

                if (dtCheckData == null || dtCheckData.Rows.Count == 0) 
                {
                    DialogHandler.Instance.Fun_DialogShowOk("请重新选取要编辑的资料！", "提示资讯", 0);
                    return;
                }
                else
                {
                    //Except()差集
                    var tempExcept = dtSelectOne.AsEnumerable().Except(dtCheckData.AsEnumerable(), DataRowComparer.Default);

                    //如果沒有差異,就繼續執行
                    if (tempExcept.Count() == 0) 
                    { 
                       // return;
                    }
                    else
                    {
                        dtSelectOne = dtCheckData.Copy();
                        Fun_SetSelectOne(dtSelectOne);
                    }                   
                }
            }
            else
            {
                //MessageBox.Show("尚未選取資料！");
                DialogHandler.Instance.Fun_DialogShowOk("尚未选取资料！", "提示资讯", 0);
                return;
            }

            #endregion



            if (dtSelectOne != null && dtSelectOne.Rows.Count >0)
            {
                //先備份之前顯示的資料
                dtBeforEdit = dtSelectOne.Copy();

                //Edit Sataus
                bolEditStatus = true;

                //编辑
                Fun_SetBottonEnabled(Btn_Edit, true);
                //儲存
                Btn_Save.Visible = true;
                //取消
                Btn_Cancel.Visible = true;
                //Control Enabled =  true
                Fun_Control(Grb_DataCol, true);

                //新增 Visible = false
                Fun_SetBottonEnabled(Btn_New, false);
                //刪除 Visible = false
                Fun_SetBottonEnabled(Btn_Delete, false);
            }
            else
            {
                //MessageBox.Show("尚未選取資料！");
                DialogHandler.Instance.Fun_DialogShowOk("尚未选取资料！", "提示资讯", 0); 
                return;
            }
           

        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_New_Click(object sender, EventArgs e)
        {
            //Edit Sataus
            bolEditStatus = true;

            //修改
            Fun_SetBottonEnabled(Btn_Edit, false);

            //刪除
            Fun_SetBottonEnabled(Btn_Delete, false);

            //儲存
            Btn_Save.Visible = true;

            //取消
            Btn_Cancel.Visible = true;

            Fun_Control(Grb_DataCol, true);
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            //修改
            Fun_SetBottonEnabled(Btn_Edit, false);

            //新增
            Fun_SetBottonEnabled(Btn_New, false);

            DialogResult actionResult = MessageBox.Show($"確定要刪除停復機紀錄日期 [{Dtp_prod_time.Value:yyyy/MM/dd}] 的資料？", "刪除停復機紀錄",
               MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (actionResult == DialogResult.No) return;

            string strSql = Frm_4_1_SqlFactory.SQL_Delete_LineFaultRecord();

            if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, "删除停复机记录"))
            {
                DialogHandler.Instance.Fun_DialogShowOk("删除停复机记录失败", "删除停复机记录", 0);
                return;
            }

            DialogHandler.Instance.Fun_DialogShowOk($"删除日期 [{Dtp_prod_time.Value:yyyy/MM/dd}]停复机记录成功!", "删除停复机记录", 4);

            EventLogHandler.Instance.LogInfo("3-4", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text }道次操作", $"删除日期 [{Dtp_prod_time.Value:yyyy/MM/dd}]停复机记录成功!");

            PublicComm.ClientLog.Info($"刪除日期[{Dtp_prod_time.Value:yyyy/MM/dd}]停復機記錄成功");

            Fun_TurnDelayMessage(TurnDelayFlag.Delete);

            //修改
            Fun_SetBottonEnabled(Btn_Edit, true);

            //新增
            Fun_SetBottonEnabled(Btn_New, true);
        }

        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            //必填欄位            
            if (!Fun_IsColumnsEmpty()) { return; }
            

            string strSql;

            if (Btn_New.Enabled)
            {
                strSql = Frm_4_1_SqlFactory.SQL_Insert_LineFaultRecord();

                if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, "新增停复机记录"))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"新增停复机记录失败", "新增停复机记录", 0);

                    return;
                }


                Fun_TurnDelayMessage(TurnDelayFlag.Insert);

            }
            else if (Btn_Edit.Enabled)
            {
                DataRow drGetEditRow = dtSelectOne.Rows[0];// dtGetRecord.Rows[Dgv_Info.CurrentRow.Index];

                strSql = Frm_4_1_SqlFactory.SQL_Update_LineFaultRecord(drGetEditRow);

                if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, "修改停复机记录"))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"修改停复机记录失败", "修改停复机记录", 0);

                    return;
                }

                // 日期、班次、班別、開始時間、結束時間任一有異動，則傳送舊資料給Server
                if (!drGetEditRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time)].Equals(Dtp_prod_time.Value.ToString("yyyy-MM-dd")) ||
                    !drGetEditRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_no)].Equals(Cob_prod_shift_no.SelectedValue) ||
                    !drGetEditRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_group)].Equals(Cob_prod_shift_group.SelectedValue) ||
                    !drGetEditRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)].Equals($"{Dtp_stop_start_time.Value:yyyy-MM-dd HH:mm:ss.fff}") ||
                    !drGetEditRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)].Equals($"{Dtp_stop_end_time.Value:yyyy-MM-dd HH:mm:ss.fff}"))
                {
                    Fun_TurnDelayMessage(TurnDelayFlag.Update);
                }
            }

            //Edit Sataus
            bolEditStatus = false;
            //修改
            Fun_SetBottonEnabled(Btn_Edit, true);           
            //儲存
            Btn_Save.Visible = false;
            //取消
            Btn_Cancel.Visible = false;

            //新增 Visible = false
            Fun_SetBottonEnabled(Btn_New, false);
            //刪除 Visible = false
            Fun_SetBottonEnabled(Btn_Delete, false);

            //Control Enabled =  false
            Fun_Control(Grb_DataCol, false);

            //儲存後,重新查詢資料
            Fun_GetDownTime();
            //查詢後,找到儲存的該筆資料
            Fun_SelectedBackRow(Dgv_Info);
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            if (dtBeforEdit != null && dtBeforEdit.Rows.Count != 0)
            {
                Fun_SetSelectOne(dtBeforEdit);
                //修改
                Fun_SetBottonEnabled(Btn_Edit, true);

            }
            else
            {
                Fun_ControlClear();
                //修改
                Fun_SetBottonEnabled(Btn_Edit, false);
            }
            //紀錄是否編輯,狀態
            bolEditStatus = false;
            //儲存
            Btn_Save.Visible = false;
            //取消
            Btn_Cancel.Visible = false;
            //Control Enabled =  false
            Fun_Control(Grb_DataCol, false);

            //新增 Visible = false
            Fun_SetBottonEnabled(Btn_New, false);
            //刪除 Visible = false
            Fun_SetBottonEnabled(Btn_Delete, false);
           
        }

        /// <summary>
        /// 計算持續時間
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fun_DtpTimeCompute(object sender, EventArgs e)
        {
            if (bolEditStatus)
            {
                double GetMinTest = (DateTime.Parse(Dtp_stop_end_time.Value.ToString("yyyy-MM-dd HH:mm")) - DateTime.Parse(Dtp_stop_start_time.Value.ToString("yyyy-MM-dd HH:mm"))).TotalMinutes;

                Txt_Stop_Elased_Time.Text = GetMinTest.ToString();
            }
        }

        private void Fun_SelectedBackRow(DataGridView dgv)
        {
            string[] strFindKey = new string[] { nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time) ,
                                                 nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_no) ,
                                                 nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_group) ,
                                                 nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time) ,
                                                 nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)
                                               };

            int intIndex = -1;
                          
                //foreach (DataGridViewRow dgvr in dgv.Rows)
                //{
                //    if ( dgvr.Cells[strFindKey[0]].Value.ToString().Equals(drArr[0][strFindKey[0]].ToString())&
                //         dgvr.Cells[strFindKey[1]].Value.ToString().Equals(drArr[0][strFindKey[1]].ToString())&
                //         dgvr.Cells[strFindKey[2]].Value.ToString().Equals(drArr[0][strFindKey[2]].ToString())&
                //         dgvr.Cells[strFindKey[3]].Value.ToString().Equals(drArr[0][strFindKey[3]].ToString())&
                //         dgvr.Cells[strFindKey[4]].Value.ToString().Equals(drArr[0][strFindKey[4]].ToString()))
                //        intIndex = dgvr.Index;
                //}

                try
                {
                    DataGridViewRow row = dgv.Rows.Cast<DataGridViewRow>().Where(r =>
                              Convert.ToDateTime(r.Cells[strFindKey[0]].Value.ToString()).ToString("yyyy-MM-dd").Equals(Dtp_prod_time.Value.ToString("yyyy-MM-dd"))
                            & r.Cells[strFindKey[1]].Value.ToString().Equals(Cob_prod_shift_no.SelectedValue)
                            & r.Cells[strFindKey[2]].Value.ToString().Equals(Cob_prod_shift_group.SelectedValue)
                            & Convert.ToDateTime(r.Cells[strFindKey[3]].Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss").Equals(Dtp_stop_start_time.Value.ToString("yyyy-MM-dd HH:mm:ss"))
                            & Convert.ToDateTime(r.Cells[strFindKey[4]].Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss").Equals(Dtp_stop_end_time.Value.ToString("yyyy-MM-dd HH:mm:ss"))
                            ).First();
                    intIndex = row.Index;
                }
                catch (Exception ee)
                {

                }

            if (intIndex > -1)
            {
                dgv.Rows[intIndex].Selected = true;
                dgv.Rows[intIndex].Cells[0].Selected = true;
            }
            else
                dgv.ClearSelection();

        }


        //設定按鈕啟用狀態並改顏色
        public void Fun_SetBottonEnabled(Button btn, bool bolE)
        {
            btn.Enabled = bolE;

            //Color colorBack;
            btn.BackColor = bolE ? Color.CornflowerBlue : Color.LightGray;
        }

        /// <summary>
        /// 通知Server停復機紀錄異動
        /// </summary>
        /// <param name="flag"></param>
        private void Fun_TurnDelayMessage(TurnDelayFlag flag)
        {
            //SCCommMsg.CS09_LineFaultData Msg = new SCCommMsg.CS09_LineFaultData
            //{
            //    prod_Shift_no = Cob_prod_shift_no.SelectedValue.ToString(),
            //    stop_start_time = Dtp_stop_start_time.Value.ToString("yyyy-MM-dd HH:mm:ss"),
            //    stop_end_time = Dtp_stop_end_time.Value.ToString("yyyy-MM-dd HH:mm:ss"),
            //    prod_time = Dtp_prod_time.Value.ToString("yyyy-MM-dd")
            //};
            //PublicComm.Client.Tell(Msg);

            string str = (int)flag == (int)TurnDelayFlag.Insert ? "新增" : (int)flag == (int)TurnDelayFlag.Update ? "修改" : "刪除";

            EventLogHandler.Instance.LogInfo("4-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text}{str}停复机记录", $"{str}[{Dtp_prod_time.Value:yyyy-MM-dd}]停复机记录");

            //DialogHandler.Instance.Fun_DialogShowOk($"已通知Server{str}[{Dtp_prod_time.Value:yyyy-MM-dd}]记录", "停复机记录异动", 4);

            //PublicComm.ClientLog.Info($"已通知Server{str}[{Dtp_prod_time.Value:yyyy/MM/dd}]停復機記錄");
            //PublicComm.AkkaLog.Info($"已通知Server{str}[{Dtp_prod_time.Value:yyyy/MM/dd}]停復機記錄");
        }

        /// <summary>
        /// 控制編輯區塊
        /// </summary>
        /// <param name="Group"></param>
        /// <param name="bolControl"></param>
        private void Fun_Control(GroupBox Group, bool bolControl)
        {
            Control[] ctrContainer = { Group };
            foreach (Control container in ctrContainer)
            {
                Fun_FindControlReadOnly(container, bolControl);
                foreach (Control ctrl in container.Controls)
                {
                    Fun_FindControlReadOnly(ctrl, bolControl);
                    foreach (Control ctrl2 in ctrl.Controls)
                    {
                        Fun_FindControlReadOnly(ctrl2, bolControl);
                    }
                }
            }
        }
        private void Fun_FindControlReadOnly(Control container, bool bolControl)
        {
            if (container.Controls.Count.Equals(0)) return;


            foreach (Control Ctl1 in container.Controls)
            {
                if (Ctl1 is TextBox)
                {
                    TextBox txtBox = Ctl1 as TextBox;

                    if (txtBox.Name.Equals(Txt_Stop_Elased_Time.Name))
                    {
                        txtBox.Enabled = false;
                    }
                    else
                    {
                        txtBox.Enabled = bolControl;
                    }
                }
                else if (Ctl1 is DateTimePicker)
                {
                    DateTimePicker dtPicker = Ctl1 as DateTimePicker;

                    if (dtPicker.Name.Equals(Dtp_stop_start_time.Name) ||
                        dtPicker.Name.Equals(Dtp_stop_end_time.Name) ||
                        dtPicker.Name.Equals(Dtp_prod_time.Name)
                        )
                    {
                        dtPicker.Enabled = false;
                    }
                    else
                    {
                        dtPicker.Enabled = bolControl;
                    }
                }
                else if (Ctl1 is ComboBox)
                {
                    ComboBox cbo = Ctl1 as ComboBox;

                    if (cbo.Name.Equals(Cob_prod_shift_no.Name) ||
                        cbo.Name.Equals(Cob_prod_shift_group.Name)
                        )
                    {
                        cbo.Enabled = false;
                    }
                    else
                    {
                        cbo.Enabled = bolControl;
                    }
                }
            }

        }

        private void Fun_ControlClear()
        {
            Cob_prod_shift_no.SelectedIndex = -1;//班次
            Cob_prod_shift_group.SelectedIndex = -1;//班組
            Cob_delay_location.SelectedIndex = -1;//停機位置
            Cob_delay_reason_code.SelectedIndex = -1;//停機原因
            Cob_deceleration_code.SelectedIndex = -1;//降速原因          
            //Cob_unit_code.SelectedIndex = -1;//機組號 基本上是固定不變
          
            //DateTimePicker 
            Dtp_prod_time.Value = DateTime.Now;
            Dtp_stop_start_time.Value = DateTime.Now;          
            Dtp_stop_end_time.Value = DateTime.Now;

            string strClear = string.Empty;
            //停機持續時間(min)
            Txt_Stop_Elased_Time.Text = strClear;
            //停機位置描述
            Txt_delay_location_desc.Text = strClear;
            //停機原因描述
            Txt_delay_reason_desc.Text = strClear;
            //機械部門原因停機時間(min)
            Txt_Resp_Depart_Delay_Time_m.Text = strClear;
            //電氣部門原因停機時間(min)         
            Txt_Resp_Depart_Delay_Time_e.Text = strClear;
            //L3原因停機時間(min)                                                                              
            Txt_Resp_Depart_Delay_Time_c.Text = strClear;
            //生產部門原因停機時間(min)                                                                          
            Txt_Resp_Depart_Delay_Time_p.Text = strClear;
            //正常停機時間(min)                                                                                  
            Txt_Resp_Depart_Delay_Time_u.Text = strClear;
            //其它部門原因停機時間(min)                                                                          
            Txt_Resp_Depart_Delay_Time_o.Text = strClear;
            //換輥原因停機時間(min)                                                                              
            Txt_Resp_Depart_Delay_Time_r.Text = strClear;
            //磨輥原因停機時間(min)            
            Txt_Resp_Depart_Delay_Time_rs.Text = strClear;
        }

        //檢查必填欄位是否空白
        private bool Fun_IsColumnsEmpty()
        {          
            //if (Cob_prod_shift_no.Text.IsEmpty() )
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_prod_shift_no_Title.Text} 栏位请勿空白，请再次确认", "提示资讯", 0);
            //    Cob_prod_shift_no.Focus();
            //    return false;
            //}
            //if (Cob_prod_shift_group.Text.IsEmpty() )
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_prod_shift_group_Title.Text} 栏位请勿空白，请再次确认", "提示资讯", 0);
            //    Cob_prod_shift_group.Focus();
            //    return false;
            //}
            if ( Cob_delay_location.Text.IsEmpty() )
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_delay_location_Title.Text} 栏位请勿空白，请再次确认", "提示资讯", 0);
                Cob_delay_location.Focus();
                return false;
            }
            if ( Cob_delay_reason_code.Text.IsEmpty())
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_delay_reason_code_Title.Text} 栏位请勿空白，请再次确认", "提示资讯", 0);
                Cob_delay_reason_code.Focus();
                return false;
            }
            //if (Cob_deceleration_code.Text.IsEmpty())
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_deceleration_code_Title.Text} 栏位请勿空白，请再次确认", "提示资讯", 0);
            //    Cob_delay_reason_code.Focus();
            //    return false;
            //}
            return true;
        }

        /// <summary>
        /// UploadMMS文字转换显示
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="strColumns">UploadMMS</param>
        /// <param name="intFormat">1:str = "Y"; 2:str = "1";</param>
        /// <returns></returns>
        private DataTable Fun_Dt_UploadMMS_ChangeShow(DataTable dt, string strColumns, int intFormat)
        {
            string strOldShow = "";
            string strNewShow = "";
            switch (intFormat)
            {
                case 1:
                    strOldShow = "Y";
                    strNewShow = "已上传";
                    break;
                case 2:
                    strOldShow = "1";
                    strNewShow = "已上传";
                    break;

                default:
                    break;
            }

            foreach (DataRow dr in dt.Rows)
                foreach (DataColumn dc in dt.Columns)
                    if (dc.ToString() == strColumns)
                    {
                        if (dr[dc].ToString() == strOldShow)
                        {
                            try
                            {
                                dr[dc] = strNewShow;
                            }
                            catch (Exception ee)
                            {
                                dr[dc] = dr[dc].ToString();
                            }
                        }
                        else
                        {
                            dr[dc] = "未上传";
                        }
                    }

            return dt;
        }

        private void Dgv_Info_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (dtGetRecord.IsNull())
                return;
            if (Dgv_Info.DgvIsNull())
                return;

            if (Dgv_Info.Rows.Count != 0)
            {    
                string strUploadMMS = Fun_TryDgvGetString(Dgv_Info, e.RowIndex, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS));
              
                if (strUploadMMS != "已上传")
                {                    
                    Dgv_Info.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 255, 128);
                }

            }
            
        }

        private void Btn_MMS_Click(object sender, EventArgs e)
        {           
            if (bolEditStatus == true)
            {
                DialogHandler.Instance.Fun_DialogShowOk("请结束编辑状态后再上传", "上传MMS", 0);
                return;
            }
            if (Dgv_Info.Rows.Count.Equals(0)) 
            {
                DialogHandler.Instance.Fun_DialogShowOk("无可上传的停复机记录", "上传MMS", 0);
                return;
            }
            if (Dgv_Info.CurrentRow == null) 
            {
                DialogHandler.Instance.Fun_DialogShowOk("请选取要上传的停复机记录", "上传MMS", 0);
                return; 
            }
            if (Dgv_Info.SelectedRows == null || Dgv_Info.SelectedRows.Count <= 0)
            {
                DialogHandler.Instance.Fun_DialogShowOk("请选取要上传的停复机记录", "上传MMS", 0);
                return;
            }
            string str = "上传";

            StringBuilder sbShow = new StringBuilder();
            sbShow.AppendLine($"请确定是否要上传停复机记录?");
            sbShow.AppendLine($"{Lbl_prod_time_Title.Text}:{Dtp_prod_time.Value.ToString("yyyy-MM-dd")}");
            sbShow.AppendLine($"{Lbl_stop_start_time_Title.Text}:{Dtp_stop_start_time.Value.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
            sbShow.AppendLine($"{Lbl_stop_end_time_Title.Text}:{Dtp_stop_end_time.Value.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
            string strMessage = sbShow.ToString();//PDO只能上传一次，
            DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel(strMessage, $"{str}停复机记录", Properties.Resources.dialogQuestion, 1);

            if (dialogR.Equals(DialogResult.OK))
            {
                if (dtSelectOne != null && dtSelectOne.Rows.Count > 0)
                {
                    SCCommMsg.CS09_LineFaultData Msg = new SCCommMsg.CS09_LineFaultData
                    {
                        prod_Shift_no = Cob_prod_shift_no.SelectedValue?.ToString(),
                        stop_start_time = Dtp_stop_start_time.Value.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                        stop_end_time = Dtp_stop_end_time.Value.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                        prod_time = Dtp_prod_time.Value.ToString("yyyy-MM-dd")
                    };
                    PublicComm.Client.Tell(Msg);                    

                    EventLogHandler.Instance.LogInfo("4-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text}{str}停复机记录", $"{str}[{Dtp_prod_time.Value:yyyy-MM-dd}]停复机记录");

                    DialogHandler.Instance.Fun_DialogShowOk($"已通知Server{str}[{Dtp_prod_time.Value:yyyy-MM-dd}]记录", $"{str}停复机记录", 4);

                    PublicComm.ClientLog.Info($"已通知Server{str}[{Dtp_prod_time.Value:yyyy/MM/dd}]停復機記錄");
                    PublicComm.AkkaLog.Info($"已通知Server{str}[{Dtp_prod_time.Value:yyyy/MM/dd}]停復機記錄");
                }
            }

                
               
        }

        #region 配合語系切換調整字體大小

        private void Fun_LanguageIsEn_Font14_12(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            System.Drawing.FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)12, fs);
        }

        private void Fun_LanguageIsEn_Font14_10(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            System.Drawing.FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)10, fs);
        }
        #endregion

        private void Chk_SendMMS_CheckedChanged(object sender, EventArgs e)
        {
            bool Check = Chk_SendMMS.Checked;

            Rdb_SendMMS.Enabled = Rdb_UnSendMMS.Enabled = Check;
        }

        public void Handle_SC07_RefreshLineDefault()
        {
            Fun_GetDownTime();
        }

        private void Btn_Export_Click(object sender, EventArgs e)
        {
            //  檢查是否為編輯狀態
            if (!ChkFlowByEditStatus())
                return;

            var strSql = Frm_4_1_SqlFactory.SQL_Select_LineFaultRecord();
            var dt = DataAccess.Fun_SelectDate(strSql, "停复机记录");
            var strFileName = $"停机纪录";

            try
            {
                var dialog = new FolderBrowserDialog() { Description = "请选择路径" };

                if (dialog.ShowDialog() == DialogResult.Cancel)
                    return;
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                    return;

                ChangeColName(ref dt);

                var samplePath = $@"{Application.StartupPath}\Excel\停复机记录范本.xlsx";
                ExportFile.WriteToExcel_xlsx(SetExportFile, dt, samplePath, dialog.SelectedPath, strFileName);

                DialogHandler.Instance.Fun_DialogShowOk($"User:{PublicForms.Main.Lbl_LoginUser.Text}\r\n汇出Execl：成功。共{dt.Rows.Count}筆。", $"汇出Execl", 4);
                PublicComm.ClientLog.Info($"User:{PublicForms.Main.Lbl_LoginUser.Text} {strFileName} 汇出Execl：成功。共{dt.Rows.Count}筆。");
                EventLogHandler.Instance.LogInfo("4-1", $"User:{PublicForms.Main.Lbl_LoginUser.Text} {strFileName} 汇出Execl：成功。", $"共{dt.Rows.Count}筆。");
            }
            catch (Exception ex)
            {
                DialogHandler.Instance.Fun_DialogShowOk(ex.Message, "错误资讯", 3);
            }
        }

        private IWorkbook SetExportFile(IWorkbook wb, object obj)
        {
            var dt = obj as DataTable;
            var ws = wb.GetSheet("Sheet1");
            var headerStyle = ExportFile.CreateCellStyle(wb, HSSFColor.SkyBlue.Index);

            //  建置標頭
            ws.CreateRow(0);
            for (var i = 0; i < dt.Columns.Count; i++)
            {
                var cell = ws.GetRow(0).CreateCell(i);

                cell.SetCellValue($"{dt.Columns[i]}");
                cell.CellStyle = headerStyle;
            }

            //  建置內容
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var rowIdx = i + 1;

                ws.CreateRow(rowIdx);
                for (var j = 0; j < dt.Columns.Count; j++)
                {
                    ws.GetRow(rowIdx).CreateCell(j).SetCellValue($"{dt.Rows[i][$"{dt.Columns[j]}"]}");
                }
            }

            return wb;
        }

        private void ChangeColName(ref DataTable dt)
        {
            dt.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS)].ColumnName = "上传MMS";
            dt.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.unit_code)].ColumnName = "机组代码";
            dt.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time)].ColumnName = "生产日期";
            dt.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_no)].ColumnName = "生产班次";
            dt.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_group)].ColumnName = "生产班组";
            dt.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)].ColumnName = "停机开始时间";
            dt.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)].ColumnName = "停机结束时间";
            dt.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location)].ColumnName = "停机位置";
            dt.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location_desc)].ColumnName = "停机位置描述";
            dt.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey)].ColumnName = "停机持续时间(Min)";
            dt.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_code)].ColumnName = "停机原因代码";
            dt.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_desc)].ColumnName = "停机原因描述";
            dt.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_remark)].ColumnName = "停机原因备注";
            dt.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_m)].ColumnName = "机械部门原因停机时间(Min)";
            dt.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_e)].ColumnName = "电气部门原因停机时间(Min)";
            dt.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_c)].ColumnName = "L3原因停机时间(Min)";
            dt.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_p)].ColumnName = "生产部门原因停机时间(Min)";
            dt.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_u)].ColumnName = "正常停机时间(Min)";
            dt.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_o)].ColumnName = "其他部门原因停机时间(Min)";
            dt.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_r)].ColumnName = "换辊原因停机时间(Min)";
            dt.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_rs)].ColumnName = "磨辊间原因停机时间(Min)";
            dt.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_code)].ColumnName = "降速代码";
            dt.Columns[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_cause)].ColumnName = "降速原因";
        }

        private bool ChkFlowByEditStatus()
        {
            //  編輯狀態下提示要先取消才允許執行功能
            if (bolEditStatus)
                DialogHandler.Instance.Fun_DialogShowOk("请先取消编辑状态！", "提示", 0);

            return !bolEditStatus;
        }
    }
}
