using DBService.Repository.PDI;
using DBService.Repository.PDO;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CPL1HMI
{
    public partial class frm_3_1_PDOCoilList : Form
    {
        //語系
        private LanguageHandler LanguageHand;

        public frm_3_1_PDOCoilList()
        {
            InitializeComponent();
        }
        private void Frm_3_1_PDOCoilList_Load(object sender, EventArgs e)
        {
           // LanguageHandler.Instance.Fun_CurrentLanguage(this.Name);

            if (PublicForms.PDOCoilList == null) PublicForms.PDOCoilList = this;

            //班別
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Team, Cob_Team);

            Fun_ComboBoxItems();
            Chk_Finish_Time.Checked = true;

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }

        private void Frm_3_1_PDOCoilList_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                Fun_SelectOut_Mat_No();
                Fun_SelectIn_Mat_No();
                Fun_SelectSteelGrade();
                Fun_SelectSg_Sign();
                Fun_SelectSteelGrade();

            }

        }
        #region ComboBox

        /// <summary>
        /// 出口鋼卷編號/入口鋼卷編號
        /// </summary>
        private void Fun_ComboBoxItems()
        {
            Fun_SelectOut_Mat_No();
            Fun_SelectIn_Mat_No();
            Fun_SelectSteelGrade();
            Fun_SelectCustomer();
            Fun_SelectSg_Sign();

            //Process Code
            Cob_ProcessCode.Items.Clear();
            Cob_ProcessCode.Items.Add("CP01");
            Cob_ProcessCode.Items.Add("CP02");
            Cob_ProcessCode.Items.Add("CP03");
            Cob_ProcessCode.Items.Add("CP04");
            Cob_ProcessCode.Items.Add("CP05");
            Cob_ProcessCode.Items.Add("CP06");
            Cob_ProcessCode.Items.Add("CP07");
        }


        /// <summary>
        /// 出口卷號清單
        /// </summary>
        private void Fun_SelectOut_Mat_No()
        {
            string strSql = Frm_3_1_SqlFactory.SQL_Select_ComboBoxItems(nameof(PDOEntity.TBL_PDO.Out_Coil_ID));
            DataTable dtOut_Mat_No = DataAccess.Fun_SelectDate(strSql,"出口卷号清单");

            if (dtOut_Mat_No.IsNull())
            {
                Cob_Exit_Coil_No.DataSource = null;
                return;
            } 

            Cob_Exit_Coil_No.DisplayMember = nameof(PDOEntity.TBL_PDO.Out_Coil_ID);
            Cob_Exit_Coil_No.ValueMember = nameof(PDOEntity.TBL_PDO.Out_Coil_ID);
            Cob_Exit_Coil_No.DataSource = dtOut_Mat_No;
        }
        /// <summary>
        /// 入口卷號清單
        /// </summary>
        private void Fun_SelectIn_Mat_No()
        {
            string strSql = Frm_3_1_SqlFactory.SQL_Select_ComboBoxItems(nameof(PDOEntity.TBL_PDO.In_Coil_ID));
            DataTable dtIn_Mat_No = DataAccess.Fun_SelectDate(strSql, "入口卷号清单");

            if (dtIn_Mat_No.IsNull())
            {
                Cob_Entry_Coil_No.DataSource = null;
                return;
            }
            

            Cob_Entry_Coil_No.DisplayMember = nameof(PDOEntity.TBL_PDO.In_Coil_ID);
            Cob_Entry_Coil_No.ValueMember = nameof(PDOEntity.TBL_PDO.In_Coil_ID);
            Cob_Entry_Coil_No.DataSource = dtIn_Mat_No;
        }
        /// <summary>
        /// 鋼種清單
        /// </summary>
        private void Fun_SelectSteelGrade()
        {
            string strSql = Frm_3_1_SqlFactory.SQL_Select_ComboBoxItems(nameof(CoilPDIEntity.TBL_PDI.St_No),true);
            DataTable dtSt_No = DataAccess.Fun_SelectDate(strSql, "钢种清单");

            if (dtSt_No.IsNull())
            {
                Cob_SteelGrade.DataSource = null;
                return;
            } 

            Cob_SteelGrade.DisplayMember = nameof(CoilPDIEntity.TBL_PDI.St_No);
            Cob_SteelGrade.ValueMember = nameof(CoilPDIEntity.TBL_PDI.St_No);
            Cob_SteelGrade.DataSource = dtSt_No;
        }
        /// <summary>
        /// 客戶清單
        /// </summary>
        private void Fun_SelectCustomer()
        {
            string strSql = Frm_3_1_SqlFactory.SQL_Select_ComboBoxItems(nameof(CoilPDIEntity.TBL_PDI.CustomerCode), true);
            DataTable dtCustmer = DataAccess.Fun_SelectDate(strSql, "客户清单");

            if (dtCustmer.IsNull())
            {
                Cob_Customer.DataSource = null;
                return;
            }

            Cob_Customer.DisplayMember = nameof(CoilPDIEntity.TBL_PDI.CustomerCode);
            Cob_Customer.ValueMember = nameof(CoilPDIEntity.TBL_PDI.CustomerCode);
            Cob_Customer.DataSource = dtCustmer;
        }

        /// <summary>
        /// 牌號清單
        /// </summary>
        private void Fun_SelectSg_Sign()
        {
            string strSql = Frm_3_1_SqlFactory.SQL_Select_ComboBoxItems(nameof(CoilPDIEntity.TBL_PDI.Sg_Sign), true);
            DataTable dtSg_Sign = DataAccess.Fun_SelectDate(strSql, "牌号清单");

            if (dtSg_Sign.IsNull())
            {
                Cob_Sg.DataSource = null;
                return;
            }

            Cob_Sg.DisplayMember = nameof(CoilPDIEntity.TBL_PDI.Sg_Sign);
            Cob_Sg.ValueMember = nameof(CoilPDIEntity.TBL_PDI.Sg_Sign);
            Cob_Sg.DataSource = dtSg_Sign;
        }

        #endregion

        #region DataGridView
        /// <summary>
        /// PDO DataGridView
        /// </summary>
        public void Fun_DataGridViewCoilList()
        {
            //string strSql = Frm_3_1_SqlFactory.SQL_Select_PDOList();
            //DataTable dt = DataAccess.Fun_SelectDate(strSql, "产品钢卷清单");

            //DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_Info, dt);
            //Frm_3_1_ColumnsHandler.Instance.Frm_3_1PDOColumns(Dgv_Info);
            //DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_Info);


            //if (Dgv_Info.Rows.Count.Equals(0))
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk("查询不到PDO清单", "PDO清单", 0);
            //}
            //else
            //{
            //    Fun_ChangeUpdateFlagColor(dt);
            //}
            
            

            ////計數
            //Lbl_CoilCount.Text = Dgv_Info.Rows.Count.ToString();

        }

        private void Fun_ChangeUpdateFlagColor(DataTable dt)
        {
            if (dt.IsNull()) return;

            string PDO_Upload;

            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                PDO_Upload = dt.Rows[RowIndex][nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)].ToString();

                if (PDO_Upload.Equals("0"))
                {
                    Dgv_Info.Rows[RowIndex].Cells[0].Value = "未上传";
                    Dgv_Info.Rows[RowIndex].DefaultCellStyle.BackColor = Color.Gold;
                }
                else if (PDO_Upload.Equals("1"))
                {
                    Dgv_Info.Rows[RowIndex].Cells[0].Value = "已上传";
                    Dgv_Info.Rows[RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
                else
                {
                    Dgv_Info.Rows[RowIndex].Cells[0].Value = "Null";
                    Dgv_Info.Rows[RowIndex].DefaultCellStyle.BackColor = Color.Gold;
                }
                Dgv_Info.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }
        #endregion

        #region 查詢

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Search_Click(object sender, EventArgs e)
        {
            //全沒選跳掉
            if (!Chk_Finish_Time.Checked && !Chk_Exit_Coil_No.Checked && !Chk_Entry_Coil_No.Checked && !Chk_Team.Checked &&
                !Chk_Out_Width.Checked && !Chk_SteelGrade.Checked && !Chk_Out_Thick.Checked && !Chk_Customer.Checked &&
                !Chk_Sg.Checked && !Chk_ProcessCode.Checked && !Chk_SendMMS.Checked)
            {
                DialogHandler.Instance.Fun_DialogShowOk("请选择查询条件","查询",0);

                return;
            }


            if (Chk_Finish_Time.Checked)
            {
                if (Dtp_Start_Time.DateTimeRangeIsFail(Dtp_Finish_Time.Value))
                {
                    DialogHandler.Instance.Fun_DialogShowOk("日期区间起讫时间不正确，请重新确认", "查询", 0);

                    return;
                }
            }

            if (Chk_Out_Width.Checked)
            {
                if (string.IsNullOrEmpty(Txt_Out_Width_Min.Text) && string.IsNullOrEmpty(Txt_Out_Width_Max.Text))
                {
                    DialogHandler.Instance.Fun_DialogShowOk("產出宽度請输入查询区间!", "查询", 0);

                    return;
                }
            }

            if (Chk_Out_Thick.Checked)
            {
                if (string.IsNullOrEmpty(Txt_Out_Thick_Min.Text) && string.IsNullOrEmpty(Txt_Out_Thick_Max.Text))
                {
                    DialogHandler.Instance.Fun_DialogShowOk("產出厚度請输入查询区间!", "查询", 0);

                    return;
                }
            }

            string strSql = Fun_ConditionalSentence();

            DataTable dt = DataAccess.Fun_SelectDate(strSql,"产品钢卷资讯条件");

            Fun_Dt_UploadMMS_ChangeShow(dt, nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag), 2);

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_Info, dt);
            Frm_3_1_ColumnsHandler.Instance.Frm_3_1PDOColumns(Dgv_Info);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_Info);

            EventLogHandler.Instance.EventPush_Message($"产品钢卷资讯查询动作");
            PublicComm.ClientLog.Info($"產品鋼卷資訊查詢動作");

            if (dt.IsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("查无资料", "查询", 0);
            }
            else
            { 
                //Fun_ChangeUpdateFlagColor(dt);
            }

           

            Lbl_CoilCount.Text = Dgv_Info.Rows.Count.ToString();

        }

        /// <summary>
        /// 查詢條件
        /// </summary>
        private string Fun_ConditionalSentence()
        {
            string strSql = Frm_3_1_SqlFactory.SQL_Select_PDOList();

            #region -- 時間範圍 --

            if (Chk_Finish_Time.Checked)
            {
                strSql += $" and pdo.[{nameof(PDOEntity.TBL_PDO.FinishTime)}] between '{Dtp_Start_Time.Value:yyyy-MM-dd HH}:00:00' and '{Dtp_Finish_Time.Value:yyyy-MM-dd HH}:59:59'";
            }

            #endregion

            #region -- 出口卷號 --

            if (Chk_Exit_Coil_No.Checked)
            {
                strSql += $" and pdo.[{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] ='{Cob_Exit_Coil_No.Text}'";
            }

            #endregion

            #region -- 入口卷號 --
            if (Chk_Entry_Coil_No.Checked)
            {
                strSql += $" and pdo.[{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}] = '{Cob_Entry_Coil_No.Text}'";
            }
            #endregion

            #region -- 班別 --
            if (Chk_Team.Checked)
            {
                strSql += $" and pdo.[{nameof(PDOEntity.TBL_PDO.Team)}] = '{Cob_Team.SelectedValue}'";
            }
            #endregion

            #region -- 出口寬度 --
            if (Chk_Out_Width.Checked)
            {
                if (!string.IsNullOrEmpty(Txt_Out_Width_Min.Text) && string.IsNullOrEmpty(Txt_Out_Width_Max.Text))
                {
                    strSql += $" and pdo.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Width)}] = '{Txt_Out_Width_Min.Text}'  ";
                }
                else if (string.IsNullOrEmpty(Txt_Out_Width_Min.Text) && !string.IsNullOrEmpty(Txt_Out_Width_Max.Text))
                {
                    strSql += $" and pdo.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Width)}] = '{Txt_Out_Width_Max.Text}'  ";
                }
                else if (!string.IsNullOrEmpty(Txt_Out_Width_Min.Text) && !string.IsNullOrEmpty(Txt_Out_Width_Max.Text))
                {
                    strSql += $" and pdo.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Width)}] between '{Txt_Out_Width_Min.Text}' and '{Txt_Out_Width_Max.Text}' ";
                }
                else
                {
                    //if (string.IsNullOrEmpty(TxT_Out_Width_Min.Text) && string.IsNullOrEmpty(TxT_Out_Width_Max.Text))
                }
            }
            #endregion

            #region -- 鋼種 --
            if (Chk_SteelGrade.Checked)
            {
                //strSql += $" and pdi.[{nameof(CoilPDIEntity.TBL_PDI.St_No)}] = '{Cob_SteelGrade.SelectedValue}'";
            }
            #endregion

            #region -- 出口厚度 --
            if (Chk_Out_Thick.Checked)
            {
                if (!string.IsNullOrEmpty(Txt_Out_Thick_Min.Text) && string.IsNullOrEmpty(Txt_Out_Thick_Max.Text))
                {
                    strSql += $" and pdo.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Thick)}] = '{Txt_Out_Thick_Min.Text}'  ";
                }
                else if (string.IsNullOrEmpty(Txt_Out_Thick_Min.Text) && !string.IsNullOrEmpty(Txt_Out_Thick_Max.Text))
                {
                    strSql += $" and pdo.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Thick)}] = '{Txt_Out_Thick_Max.Text}'  ";
                }
                else if (!string.IsNullOrEmpty(Txt_Out_Thick_Min.Text) && !string.IsNullOrEmpty(Txt_Out_Thick_Max.Text))
                {
                    strSql += $" and pdo.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Thick)}] between '{Txt_Out_Thick_Min.Text}' and '{Txt_Out_Thick_Max.Text}' ";
                }
                else
                {
                    // if (string.IsNullOrEmpty(TxT_Out_Thick_Min.Text) && string.IsNullOrEmpty(TxT_Out_Thick_Max.Text))
                }
            }
            #endregion

            #region -- 客戶代碼 --
            if (Chk_Customer.Checked)
            {
                //strSql += $" and pdi.[{nameof(CoilPDIEntity.TBL_PDI.CustomerCode)}]  = '{Cob_Customer.SelectedValue}'";
            }
            #endregion

            #region -- 牌號 --
            if (Chk_Sg.Checked)
            {
                //strSql += $" and pdi.[{nameof(CoilPDIEntity.TBL_PDI.Sg_Sign)}] = '{Cob_Sg.SelectedValue}'";
            }
            #endregion

            #region -- ProcessCode --
            if (Chk_ProcessCode.Checked)
            {
                //strSql += $" and pdi.[{nameof(CoilPDIEntity.TBL_PDI.Process_Code)}] = '{Cob_ProcessCode.SelectedText}'";
            }
            #endregion

            #region -- 上傳MMS --
            if (Chk_SendMMS.Checked)
            {
                if (Rdb_SendMMS.Checked)
                {
                    strSql += $@" and pdo.[{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)}] = '1' 
                                  and pdo.[{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)}] is not null";

                }
                else if (Rdb_UnSendMMS.Checked)
                {
                    strSql += $" and pdo.[{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)}] <> '1'";
                }
            }
            #endregion

            strSql += $@"  ORDER BY pdo.[{nameof(PDOEntity.TBL_PDO.FinishTime)}]";

            return strSql;
        }

        #endregion

        /// <summary>
        /// 開啟詳細資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_PdiDet_Click(object sender, EventArgs e)
        {
            if (Dgv_Info.DgvIsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("无钢卷清单可开启","开启详细资料",0);
                return;
            } 


            if (Dgv_Info.CurrentIsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("请选取要显示的钢卷", "开启详细资料", 0);
                return;
            }
            
            Frm_3_2_ProdDetailOpen(Dgv_Info.CurrentRow);
        }

        private void Frm_3_2_ProdDetailOpen(DataGridViewRow dgvrData)
        {
            if(dgvrData != null)
            {                
                string strOut_Coil_ID = dgvrData.Cells["Out_Coil_ID"].Value.ToString();//1
                //DateTime dTime = DateTime.Parse(dgvrData.Cells["FinishTime"].Value.ToString());
                string strFinishTime = dgvrData.Cells["FinishTime"].FormattedValue.ToString();
                //string strFinishTime = dTime.ToString("yyyy-MM-dd HH:mm:ss");//2
                string strIn_Coil_ID = dgvrData.Cells["In_Coil_ID"].Value.ToString();//3
                string strPlan_No = dgvrData.Cells["Plan_No"].Value.ToString();//4

                PublicForms.Main.tsMenuItem_3_2.PerformClick();
                //PublicForms.PDODetail.Cob_Search_Out_Coil_ID.Text = strOut_Coil_ID;
                PublicForms.PDODetail.Txt_Search_Out_Coil_ID.Text = strOut_Coil_ID;
                PublicForms.PDODetail.Fun_SelectCoilPDO(strOut_Coil_ID, strFinishTime, strIn_Coil_ID, strPlan_No);

                EventLogHandler.Instance.LogInfo("3-1", $"产品钢卷资讯", $"产品钢卷资讯 信息:开启[{strOut_Coil_ID.Trim()}]PDO详细资料並跳转至3-2产品钢卷详细资料");
                PublicComm.ClientLog.Info($"開啟[{strOut_Coil_ID.Trim()}]PDO詳細資料並跳轉至3-2產品鋼卷詳細資料");
            }


          
        }

        private void Chk_SendMMS_CheckedChanged(object sender, EventArgs e)
        {
            bool Check = Chk_SendMMS.Checked;

            Rdb_SendMMS.Enabled = Rdb_UnSendMMS.Enabled = Check;
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
            if (Dgv_Info.DgvIsNull())
                return;

            if (Dgv_Info.Rows.Count != 0)
            {
                string strUploadMMS = Fun_TryDgvGetString(Dgv_Info, e.RowIndex, nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag));
                //Fun_TryDtGetString(dtGetRecord, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS));
                //Dgv_Info.Rows[e.RowIndex].Cells[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS)].Value.ToString().Trim();

                if (strUploadMMS != "已上传")
                {
                    //Dgv_Info.Rows[e.RowIndex].Cells[0].Value = "未上传";
                    Dgv_Info.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gold;
                }

            }
           
        }

        private string Fun_TryDgvGetString(DataGridView dgv, int intRows, string strCol)
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
    }
}
