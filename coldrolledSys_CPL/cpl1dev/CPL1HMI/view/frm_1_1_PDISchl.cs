using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Akka.Actor;
using DataModel.HMIServerCom.Msg;
using ExcelDataReader;
using DBService.Repository.PDI;
using DBService.Repository;
using System.Reflection;
using System.Collections;

namespace CPL1HMI
{
    public partial class frm_1_1_PDISchl : Form
    {
        
        #region 變數
        public DataTable dt;
        DataTable dt_ForSearch;
        DataTable dt_ScheduleSeq;
        DataTable dtUpdateTime;

        int intTopCanNotMove = 3;
        public DataTable dtSelected = new DataTable();
        public DataTable dtCopy = new DataTable();

        //語系
        private LanguageHandler LanguageHand;

        #endregion

        //TODO 180秒操作倒數+180秒重置按鈕
        public frm_1_1_PDISchl()
        {
            InitializeComponent();
        }

        private void Frm_1_1_PDISchl_Load(object sender, EventArgs e)
        {
            //LanguageHandler.Instance.Fun_CurrentLanguage(this.Name);
                      
            if (PublicForms.PDISchl == null) PublicForms.PDISchl = this;

            Control[] Frm_1_1_Control = new Control[] {
            Btn_First,//第一位
            Btn_Up,//上一位
            Btn_Last,//最後一位
            Btn_Down,//下一位
            Btn_MovePdi,//確定排程
            Btn_OutSchedule,//刪除排程
            Btn_ImportPDI,//匯入PDI
            Btn_ImportSchedule,//匯入排程
            Btn_RequestPDI,//要求PDI
            Btn_NewSchedule//要求排程刷新
            };

            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_1_1_Control, UserSetupHandler.Instance.Frm_1_1);

            Btn_MovePdi.Enabled = false;

            //鋼卷排程資訊
            try
            {
                //
                Fun_DisplayDataGridView();
            }
            catch (Exception ex)
            {
                DialogHandler.Instance.Fun_DialogShowOk($"查询资料库失败:{ex}", "钢卷排程资讯", 3);

                EventLogHandler.Instance.LogDebug("1-1", $"钢卷排程资讯", $"查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"鋼卷排程資訊查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"鋼卷排程資訊查詢資料庫失敗:{ex}");
            }

            //ComboBox选项
            try
            {
                Fun_ComboBoxItem();
            }
            catch (Exception ex)
            {
                DialogHandler.Instance.Fun_DialogShowOk($"查询资料库失败:{ex}", "ComboBox选项", 3);

                EventLogHandler.Instance.LogDebug("1-1", $"ComboBox选项", $"查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"ComboBox選項查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"ComboBox選項查詢資料庫失敗:{ex}");
            }

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);

        }

        private void Frm_1_1_PDISchl_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
                Fun_ReLoad();
        }

        #region DGV資料

        #region For Dgv_Info
        /// <summary>
        /// DGV資料
        /// </summary>
        private void Fun_DisplayDataGridView()
        {
            if (!Fun_TopScheduleLock())
                intTopCanNotMove = 0;
            else
                intTopCanNotMove = 3;

            string strSql;
           
            //鋼卷排程資訊SQL
            strSql = Frm_1_1_SqlFactory.SQL_Select_Production_Schedule();
            dt = DataAccess.Fun_SelectDate(strSql, "钢卷排程");

            //鋼卷數量統計
            Txt_Count_ForSchedule.Text = dt!= null? dt.Rows.Count.ToString():"0";

            //紀錄原有Seq_No
            strSql = Frm_1_1_SqlFactory.SQL_Select_KeepScheduleSeqNo();
            dt_ScheduleSeq = DataAccess.Fun_SelectDate(strSql, "记录顺序号");

            //记录更新时间
            strSql = Frm_1_1_SqlFactory.SQL_Select_KeepScheduleUpdateTime();
            dtUpdateTime = DataAccess.Fun_SelectDate(strSql, "记录钢卷排程时间");

            //DGV設定
            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_ScheduleInfo, dt);
            Frm_1_1_ColumnsHandler.Instance.Frm_1_1_ScheduleColumns(Dgv_ScheduleInfo);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_ScheduleInfo);

            Dgv_ScheduleInfo.ClearSelection();

            PublicComm.ClientLog.Info($"DataGridView排程清單");

            if (dt_ScheduleSeq.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无排程");
                return;
            }

            if (dtUpdateTime.IsNull())
                return;
           
        }

        private void Dgv_ScheduleInfo_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (dt.IsNull())
                return;
            if (Dgv_ScheduleInfo.DgvIsNull())
                return;

            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                if (RowIndex <= intTopCanNotMove-1)
                {
                    Dgv_ScheduleInfo.Rows[RowIndex].DefaultCellStyle.BackColor = Color.DimGray;
                    Dgv_ScheduleInfo.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    Dgv_ScheduleInfo.Rows[RowIndex].DefaultCellStyle.BackColor = Dgv_ScheduleInfo.Rows[RowIndex].Cells[nameof(CoilPDIEntity.TBL_PDI.Plan_No)].Value.ToString().IsEmpty() ? Color.FromArgb(255, 90, 82) : Color.White;
                    Dgv_ScheduleInfo.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }
        #endregion

        #region For Dgv_Search
        private void Fun_SearchDataGridView()
        {
            string strMsg = "附加搜寻条件:";

            strMsg += Chk_Status.Checked ? " 生产完成 " : Chk_Online.Checked ? " 已上线 " : Chk_NotOnline.Checked ? " 未上线 " : " 无 ";


            EventLogHandler.Instance.LogInfo( "1-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text}執行查詢", strMsg);

            string strSql = Frm_1_1_SqlFactory.SQL_Select_PageCoilInfo_CoilData();

            dt_ForSearch = DataAccess.Fun_SelectDate(strSql, "钢卷资讯查询");

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_PDISearch, dt_ForSearch);
            Frm_1_1_ColumnsHandler.Instance.Frm_1_1_PDI_ListColumns(Dgv_PDISearch);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_PDISearch);

            strMsg = dt_ForSearch.IsNull() ? "查询无资料" :"查询完成";

            if (Dgv_PDISearch.Rows.Count.Equals(0))
            {
                DialogHandler.Instance.Fun_DialogShowOk(strMsg, "钢卷资讯查询", 0);
            }
            else
            { 
                EventLogHandler.Instance.EventPush_Message(strMsg);
                PublicComm.ClientLog.Info($"DataGridView入料鋼卷清單");
            }

            //鋼卷數量統計
            Txt_Count_ForSearch.Text = dt_ForSearch.IsNull()? "0" : dt_ForSearch.Rows.Count.ToString();

        }

        #region CheckBox 單選
        /// <summary>
        /// 未生產中狀態
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chk_NotOnline_CheckedChanged(object sender, EventArgs e)
        {

            if (Chk_NotOnline.Checked)
            {
                Chk_Status.Checked = false;
                Chk_Online.Checked = false;
            }

        }

        /// <summary>
        /// 生產中狀態
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chk_Online_CheckedChanged(object sender, EventArgs e)
        {

            if (Chk_Online.Checked)
            {
                Chk_Status.Checked = false;
                Chk_NotOnline.Checked = false;
            }

        }

        /// <summary>
        /// 已生產狀態
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chk_Status_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_Status.Checked)
            {
                Chk_Online.Checked = false;
                Chk_NotOnline.Checked = false;
            }
        }

        #endregion

        #endregion

        #endregion

        #region ComboBox

        /// <summary>
        /// 鋼卷資訊查詢ComboBox內容
        /// </summary>
        private void Fun_ComboBoxItem()
        {
            #region 計畫號
            string strSql = Frm_1_1_SqlFactory.SQL_Select_ComboBoxItems($" distinct [{nameof(CoilPDIEntity.TBL_PDI.Plan_No)}] ");
            DataTable dtGetPlanList = DataAccess.Fun_SelectDate(strSql, "1-1钢卷资讯计划号清单");

            if (dtGetPlanList.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无计划号清单");
                return;
            } 

            Cob_Plan_No.DisplayMember = nameof(CoilPDIEntity.TBL_PDI.Plan_No);
            Cob_Plan_No.ValueMember = nameof(CoilPDIEntity.TBL_PDI.Plan_No);
            Cob_Plan_No.DataSource = dtGetPlanList;

            PublicComm.ClientLog.Info($"計畫號選項清單查詢");
            #endregion

            #region 鋼卷編號
            strSql = Frm_1_1_SqlFactory.SQL_Select_ComboBoxItems($" [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] ");
            DataTable dtGetCoilList = DataAccess.Fun_SelectDate(strSql, "1-1钢卷资讯钢卷号清单");

            if (dtGetCoilList.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无钢卷号清单");
                return;
            } 
               
            Cob_Entry_Coil_No.DisplayMember = nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID);
            Cob_Entry_Coil_No.ValueMember = nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID);
            Cob_Entry_Coil_No.DataSource = dtGetCoilList;

            PublicComm.ClientLog.Info($"鋼卷號選項清單查詢");
            #endregion

            #region 刪除代碼
            //ComboBoxIndexHandler.Instance.ComboBox_DeleteCode(cboDelReasonCode);
            #endregion

        }
        #endregion

        #region 排程異動

        #region 排序

        //排序-移至第一筆
        private void Btn_First_Click(object sender, EventArgs e)
        {
            if (Dgv_ScheduleInfo.DgvIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无排程");
                return;
            }

            if (Dgv_ScheduleInfo.CurrentIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无选取排程");
                return;
            }

            Fun_ScheduleMove((int)Seq_No_Change.Top);
            
        }

        //排序-移至上一筆
        private void Btn_Up_Click(object sender, EventArgs e)
        {
            if (Dgv_ScheduleInfo.DgvIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无排程");
                return;
            }

            if (Dgv_ScheduleInfo.CurrentIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无选取排程");
                return;
            }
            Fun_ScheduleMove((int)Seq_No_Change.Up);
        }

        //排序-移至下一筆
        private void Btn_Down_Click(object sender, EventArgs e)
        {
            if (Dgv_ScheduleInfo.DgvIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无排程");
                return;
            }

            if (Dgv_ScheduleInfo.CurrentIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无选取排程");
                return;
            }

            Fun_ScheduleMove((int)Seq_No_Change.Down);
        }

        //排序-移至最後一筆
        private void Btn_Last_Click(object sender, EventArgs e)
        {
            if (Dgv_ScheduleInfo.DgvIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无排程");
                return;
            }

            if (Dgv_ScheduleInfo.CurrentIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无选取排程");
                return;
            }

            Fun_ScheduleMove((int)Seq_No_Change.Bottom);
        }

        /// <summary>
        /// 檢查是否可以調整前3顆排程
        /// </summary>
        /// <returns>false = 0:未鎖 ; true = 1:鎖定  </returns>
        private bool Fun_TopScheduleLock()
        {
           bool bolLock = true;
           string strSql = Frm_1_1_SqlFactory.SQL_Select_SystemSetting_TopScheduleLock();
           DataTable dtTopLock = DataAccess.Fun_SelectDate(strSql, "TopScheduleLock");
           if( dtTopLock != null && dtTopLock.Rows.Count > 0)
            {
                bolLock = dtTopLock.Rows[0]["Value"].ToString().Trim() == "0" ? false : true ;
            }
            return bolLock ;
        }
               
        private void Fun_ScheduleMove(int move)
        {
            if (!Fun_TopScheduleLock())
                intTopCanNotMove = 0;
            else
                intTopCanNotMove = 3;

            if (Dgv_ScheduleInfo.CurrentRow.Index > intTopCanNotMove -1)
            {
                //Fun_RecordSelected(Dgv_ScheduleInfo);
                Fun_SortingAlgorithm(move);                
            }
            else if (Dgv_ScheduleInfo.CurrentRow.Index <= intTopCanNotMove - 1)
            {
                EventLogHandler.Instance.EventPush_Message($"钢卷排程前三笔无法更动!");
                PublicComm.ClientLog.Info($"排程調整，鋼卷號:{Dgv_ScheduleInfo.CurrentRow.Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value}為排程前三筆，禁止異動!");
                return;
            }
        }
  
        private void Btn_CancelMove_Click(object sender, EventArgs e)
        {
            PublicComm.ClientLog.Info($"取消調整動作");
            Btn_MovePdi.Enabled = false;
            Btn_CancelMove.Visible = false;
            Btn_OutSchedule.Enabled = true;
            Btn_ImportSchedule.Enabled = true;
            Btn_ImportPDI.Enabled = true;
            Btn_ReLoad.Enabled = true;
            Fun_DisplayDataGridView();
            EventLogHandler.Instance.EventPush_Message($"已恢復成原排序");
            PublicComm.ClientLog.Info($"取消調整並恢復成原排序");
        }
        
        
        /// <summary>
        /// 紀錄選取行
        /// </summary>
        public void Fun_RecordSelected(DataGridView dgv)
        {
            if (dt.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无排程");
                return;
            }

            if (dgv.CurrentIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无选取排程");
                return;
            }

            dtSelected = dt.Clone();
            for (int i = 0; i < dgv.RowCount; i++)
            {
                if (dgv.Rows[i].Selected) dtSelected.ImportRow(dt.Rows[i]);
            }
        }
        /// <summary>
        /// 0:移至第一筆 / 1:往上一筆 / 2:往下一筆 / 3:移至最後一筆
        /// </summary>
        /// <param name="Number"></param>
        /// <param name="Dgv_ScheduleInfo"></param>
        /// <param name="dt"></param>
        public void Fun_SortingAlgorithm(int Number)
        {
            //int Rowsindex = 0;

            if (Dgv_ScheduleInfo.CurrentIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无选取排程");
                return;
            }

            dtCopy = dt.Clone();

            switch (Number)
            {
                //移至最上面
                case 0:
                    if (!Dgv_ScheduleInfo.CurrentIsNull())
                    {
                        Fun_ScheduleFirst();
                        #region Old

                        //if (dtSelected.Rows.Count.Equals(1))
                        //{
                        //    Rowsindex = Dgv_ScheduleInfo.CurrentRow.Index;
                        //    if (Rowsindex <= 2) { return; }//前三筆不能異動

                        //    object[] _rowData = dt.Rows[Rowsindex].ItemArray;//先把要置頂的列 存起來
                        //    for (int intN = Rowsindex; intN >= 3; intN--)
                        //        dt.Rows[intN].ItemArray = dt.Rows[intN - 1].ItemArray;
                        //    dt.Rows[3].ItemArray = _rowData;
                        //    Dgv_ScheduleInfo.CurrentCell = Dgv_ScheduleInfo[Dgv_ScheduleInfo.CurrentCell.ColumnIndex, 3];
                        //    EventLogHandler.Instance.EventPush_Message($"钢卷号[{Dgv_ScheduleInfo.CurrentRow.Cells[nameof(CoilScheduleModel.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}]移动至第四位");
                        //    PublicComm.ClientLog.Info($"排程调整，钢卷号:{Dgv_ScheduleInfo.CurrentRow.Cells[nameof(CoilScheduleModel.TBL_Production_Schedule.Coil_ID)].Value}移动至第四位");
                        //}
                        //else
                        //{
                        //    //如果群組內包含第一位~第三位就跳出
                        //    if (Dgv_ScheduleInfo.Rows[0].Selected) return;
                        //    else if (Dgv_ScheduleInfo.Rows[1].Selected) return;
                        //    else if (Dgv_ScheduleInfo.Rows[2].Selected) return;
                        //    dtCopy.ImportRow(dt.Rows[0]);
                        //    dtCopy.ImportRow(dt.Rows[1]);
                        //    dtCopy.ImportRow(dt.Rows[2]);
                        //    for (int i = 0; i < dt.Rows.Count; i++)
                        //    {
                        //        if (Dgv_ScheduleInfo.Rows[i].Selected)
                        //        {
                        //            dtCopy.ImportRow(dt.Rows[i]);
                        //        }
                        //    }
                        //    for (int i = 3; i < dt.Rows.Count; i++)
                        //    {
                        //        if (Dgv_ScheduleInfo.Rows[i].Selected.Equals(false)) dtCopy.ImportRow(dt.Rows[i]);
                        //    }

                        //    Dgv_ScheduleInfo.DataSource = dtCopy;
                        //}
                        #endregion
                    }
                    break;
                //往上移一位
                case 1:                  
                    if (!Dgv_ScheduleInfo.CurrentIsNull())
                    {
                        Fun_ScheduleIndexUp();
                        #region Old

                        //if (dtSelected.Rows.Count == 1)
                        //{
                        //    Rowsindex = Dgv_ScheduleInfo.CurrentRow.Index;
                        //    if (Rowsindex <= 2) { return; }//前三筆不能異動

                        //    object[] _rowData = dt.Rows[Rowsindex].ItemArray;
                        //    if (Rowsindex - 1 <= 2)//防止非前三筆的資料行可上移
                        //    {
                        //        EventLogHandler.Instance.EventPush_Message($"排程前三笔禁止异动");
                        //        return;
                        //    }
                        //    dt.Rows[Rowsindex].ItemArray = dt.Rows[Rowsindex - 1].ItemArray;
                        //    dt.Rows[Rowsindex - 1].ItemArray = _rowData;
                        //    Dgv_ScheduleInfo.CurrentCell = Dgv_ScheduleInfo[Dgv_ScheduleInfo.CurrentCell.ColumnIndex, Dgv_ScheduleInfo.CurrentCell.RowIndex - 1];
                        //    EventLogHandler.Instance.EventPush_Message($"钢卷号[{Dgv_ScheduleInfo.CurrentRow.Cells[nameof(CoilScheduleModel.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}]往上移动一位");
                        //    PublicComm.ClientLog.Info($"排程调整，钢卷号:{Dgv_ScheduleInfo.CurrentRow.Cells[nameof(CoilScheduleModel.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}往上移动一位");
                        //}
                        //else
                        //{
                        //    //如果群組內包含第一位~第三位就跳出
                        //    if (Dgv_ScheduleInfo.Rows[0].Selected) return;
                        //    else if (Dgv_ScheduleInfo.Rows[1].Selected) return;
                        //    else if (Dgv_ScheduleInfo.Rows[2].Selected) return;
                        //    for (int i = 0; i < dt.Rows.Count; i++)
                        //    {
                        //        if (Dgv_ScheduleInfo.Rows[i].Selected)
                        //        {
                        //            Rowsindex = Dgv_ScheduleInfo.Rows[i].Index;
                        //            break;
                        //        }
                        //    }
                        //    for (int i = 0; i < dt.Rows.Count; i++)
                        //    {
                        //        if (Dgv_ScheduleInfo.Rows[i].Index == Rowsindex)
                        //        {
                        //            for (int j = 0; j < dt.Rows.Count; j++)
                        //            {
                        //                if (Dgv_ScheduleInfo.Rows[j].Selected) dtCopy.ImportRow(dt.Rows[j]);
                        //            }
                        //            if (Rowsindex - 1 <= 2)//防止非前三筆的資料行可上移
                        //            {
                        //                EventLogHandler.Instance.EventPush_Message($"排程前三笔禁止异动");
                        //                return;
                        //            }
                        //            dtCopy.ImportRow(dt.Rows[Rowsindex - 1]);
                        //        }
                        //        else if (Dgv_ScheduleInfo.Rows[i].Selected.Equals(false) && !Dgv_ScheduleInfo.Rows[i].Index.Equals(Rowsindex - 1)) dtCopy.ImportRow(dt.Rows[i]);
                        //    }

                        //    Dgv_ScheduleInfo.DataSource = dtCopy;
                        //}
                        #endregion
                    }

                    break;
                //往下移一位
                case 2:    
                    if (!Dgv_ScheduleInfo.CurrentIsNull())
                    {
                        Fun_ScheduleIndexDown();
                        #region Old
                        //if (dtSelected.Rows.Count == 1)
                        //{
                        //    Rowsindex = Dgv_ScheduleInfo.CurrentRow.Index;
                        //    if (Rowsindex.Equals(dt.Rows.Count - 1)) { return; }
                        //    if (Rowsindex <= 2)//前三筆不能異動
                        //    {
                        //        return;
                        //    }
                        //    object[] _rowData = dt.Rows[Rowsindex].ItemArray;
                        //    dt.Rows[Rowsindex].ItemArray = dt.Rows[Rowsindex + 1].ItemArray;
                        //    dt.Rows[Rowsindex + 1].ItemArray = _rowData;
                        //    Dgv_ScheduleInfo.CurrentCell = Dgv_ScheduleInfo[Dgv_ScheduleInfo.CurrentCell.ColumnIndex, Dgv_ScheduleInfo.CurrentCell.RowIndex + 1];
                        //    EventLogHandler.Instance.EventPush_Message($"钢卷号[{Dgv_ScheduleInfo.CurrentRow.Cells[nameof(CoilScheduleModel.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}]往下移动一位");
                        //    PublicComm.ClientLog.Info($"排程调整，钢卷号:{Dgv_ScheduleInfo.CurrentRow.Cells[nameof(CoilScheduleModel.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}往下移动一位");
                        //}
                        //else
                        //{
                        //    //如果群組包含最後一位就跳出
                        //    if (Dgv_ScheduleInfo.Rows[dt.Rows.Count - 1].Selected) return;
                        //    //如果群組內包含第一位~第三位就跳出
                        //    if (Dgv_ScheduleInfo.Rows[0].Selected) return;
                        //    else if (Dgv_ScheduleInfo.Rows[1].Selected) return;
                        //    else if (Dgv_ScheduleInfo.Rows[2].Selected) return;

                        //    for (int i = 0; i < dt.Rows.Count; i++)
                        //    {
                        //        if (Dgv_ScheduleInfo.Rows[i].Selected)
                        //        {
                        //            Rowsindex = Dgv_ScheduleInfo.Rows[i].Index;
                        //            //break;
                        //        }
                        //    }
                        //    for (int i = 0; i < dt.Rows.Count; i++)
                        //    {
                        //        if (Dgv_ScheduleInfo.Rows[i].Index.Equals(Rowsindex + 1))
                        //        {
                        //            dtCopy.ImportRow(dt.Rows[Rowsindex + 1]);
                        //            for (int j = 0; j < dt.Rows.Count; j++)
                        //            {
                        //                if (Dgv_ScheduleInfo.Rows[j].Selected) dtCopy.ImportRow(dt.Rows[j]);
                        //            }
                        //        }
                        //        else if (Dgv_ScheduleInfo.Rows[i].Selected.Equals(false) && !Dgv_ScheduleInfo.Rows[i].Index.Equals(Rowsindex + 1)) dtCopy.ImportRow(dt.Rows[i]);
                        //    }

                        //    Dgv_ScheduleInfo.DataSource = dtCopy;
                        //}
                        #endregion
                    }

                    break;
                //移至最下面
                case 3:    
                    if (!Dgv_ScheduleInfo.CurrentIsNull())
                    {
                        Fun_ScheduleIndexLast();
                        #region Old
                        //if (dtSelected.Rows.Count.Equals(1))
                        //{
                        //    Int32 intLast = Dgv_ScheduleInfo.Rows.Count - 1;
                        //    Rowsindex = Dgv_ScheduleInfo.CurrentRow.Index;
                        //    if (Rowsindex.Equals(dt.Rows.Count - 1)) { return; }
                        //    if (Rowsindex <= 2) { return; }//前三筆不能異動
                        //    object[] _rowData = dt.Rows[Rowsindex].ItemArray;//先把要置底的列 存起來
                        //    for (int intN = Rowsindex; intN < intLast; intN++)
                        //        dt.Rows[intN].ItemArray = dt.Rows[intN + 1].ItemArray;

                        //    dt.Rows[intLast].ItemArray = _rowData;
                        //    Dgv_ScheduleInfo.CurrentCell = Dgv_ScheduleInfo[Dgv_ScheduleInfo.CurrentCell.ColumnIndex, intLast];
                        //    EventLogHandler.Instance.EventPush_Message($"钢卷号[{Dgv_ScheduleInfo.CurrentRow.Cells[nameof(CoilScheduleModel.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}]移动至最后一位");
                        //    PublicComm.ClientLog.Info($"排程调整，钢卷号:{Dgv_ScheduleInfo.CurrentRow.Cells[nameof(CoilScheduleModel.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}移动至最后一位");
                        //}
                        //else
                        //{
                        //    if (Dgv_ScheduleInfo.Rows[0].Selected) return;
                        //    else if (Dgv_ScheduleInfo.Rows[1].Selected) return;
                        //    else if (Dgv_ScheduleInfo.Rows[2].Selected) return;
                        //    for (int i = 0; i < dt.Rows.Count; i++)
                        //    {
                        //        if (Dgv_ScheduleInfo.Rows[i].Selected.Equals(false)) dtCopy.ImportRow(dt.Rows[i]);
                        //    }
                        //    for (int i = 0; i < dt.Rows.Count; i++)
                        //    {
                        //        if (Dgv_ScheduleInfo.Rows[i].Selected) dtCopy.ImportRow(dt.Rows[i]);
                        //    }
                        //    Dgv_ScheduleInfo.DataSource = dtCopy;
                        //}
                        #endregion
                    }

                    break;
                default:
                    return;
            }

            //Btn_ReLoad.Enabled = false;
            Btn_OutSchedule.Enabled = false;
            Btn_ImportSchedule.Enabled = false;
            Btn_ImportPDI.Enabled = false;
            Btn_MovePdi.Enabled = true;
            Btn_CancelMove.Visible = true;

            if (dtCopy.Rows.Count != 0)
                dt = dtCopy;
        }

        /// <summary>
        /// 移至第一位
        /// </summary>
        private void Fun_ScheduleFirst()
        {
            List<int> SelectedCoilIndexList = new List<int>();
            DataRow drGetRow;

            try
            {
                //---  從上到下掃描鋼捲號碼，找出有選取的鋼捲號碼  ---//
                for (int RowIndex = 0; RowIndex < Dgv_ScheduleInfo.Rows.Count; RowIndex++)
                {
                    if (Dgv_ScheduleInfo.Rows[RowIndex].Selected)
                    {
                        // 前三筆不能異動
                        if (RowIndex <= intTopCanNotMove -1)
                        {
                            // 補訊息提示
                            break;
                        }
                        SelectedCoilIndexList.Add(RowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                // 補上錯誤訊息Dialog
            }

            try
            {
                //---  將選取的鋼捲號碼移至最前面  ---//
                for (int nCount = 0; nCount < SelectedCoilIndexList.Count ; nCount++)
                {
                    drGetRow = dt.NewRow();
                    int nSripIndex = SelectedCoilIndexList[nCount];
                    drGetRow.ItemArray = dt.Rows[nSripIndex].ItemArray;
                    dt.Rows.RemoveAt(nSripIndex);
                    dt.Rows.InsertAt(drGetRow, intTopCanNotMove + nCount);
                    drGetRow = null;
                }
            }
            catch (Exception ex)
            {
                // 補上錯誤訊息Dialog
            }

            Dgv_ScheduleInfo.ClearSelection();
          
            //---  上移的鋼捲號碼改為選取狀態  ---//
            for (int nCount = 0; nCount < SelectedCoilIndexList.Count; nCount++)
            {
                Dgv_ScheduleInfo.Rows[intTopCanNotMove + nCount].Selected = true;
            }

            //---  選取資料即將超出畫面，則移動捲軸  ---//
            if (Dgv_ScheduleInfo.SelectedRows.Count > 0)
            {
                int nSelRowIndex = Dgv_ScheduleInfo.SelectedRows[0].Index;
               
                Dgv_ScheduleInfo.FirstDisplayedScrollingRowIndex = 0;
                
            }

        }

        /// <summary>
        /// 往上移一位
        /// </summary>
        private void Fun_ScheduleIndexUp()
        {
            List<int> SelectedCoilIndexList = new List<int>();
            DataRow drGetRow;

            try
            {
                //---  從上到下掃描鋼捲號碼，找出有選取的鋼捲號碼  ---//
                for (int RowIndex = 0; RowIndex < Dgv_ScheduleInfo.Rows.Count; RowIndex++)
                {
                    if (Dgv_ScheduleInfo.Rows[RowIndex].Selected)
                    {
                        // 前三筆不能異動
                        if (RowIndex <= intTopCanNotMove-1)
                        {
                            // 補訊息提示
                            break;
                        }
                        SelectedCoilIndexList.Add(RowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                // 補上錯誤訊息Dialog
            }

            try
            {
                //---  將選取的鋼捲號碼往上移一格  ---//
                for (int nCount = 0; nCount < SelectedCoilIndexList.Count ; nCount++)
                {
                    drGetRow = dt.NewRow();
                    int nSripIndex = SelectedCoilIndexList[nCount];

                    if (nSripIndex - 1 <= intTopCanNotMove-1)
                    {
                        EventLogHandler.Instance.EventPush_Message($"排程禁止异动至前三笔");
                        break;
                    }

                    drGetRow.ItemArray = dt.Rows[nSripIndex].ItemArray;
                    dt.Rows.RemoveAt(nSripIndex);
                    dt.Rows.InsertAt(drGetRow, nSripIndex - 1);
                    drGetRow = null;
                }
            }
            catch (Exception ex)
            {
                // 補上錯誤訊息Dialog
            }

            Dgv_ScheduleInfo.ClearSelection();

            //---  上移的鋼捲號碼改為選取狀態  ---//
            for (int nCount = 0; nCount < SelectedCoilIndexList.Count; nCount++)
            {
                if((SelectedCoilIndexList[nCount] - 1 ) >= intTopCanNotMove)
                {
                    Dgv_ScheduleInfo.Rows[SelectedCoilIndexList[nCount] - 1].Selected = true;
                }
                
            }

            //---  選取資料即將超出畫面，則移動捲軸  ---//
            if (Dgv_ScheduleInfo.SelectedRows.Count > 0)
            {
                int nSelRowIndex = Dgv_ScheduleInfo.SelectedRows[0].Index;
                //int nShowRowPage = 14;
                int intNowScrollRowIndex = Dgv_ScheduleInfo.FirstDisplayedScrollingRowIndex;

                if (nSelRowIndex <= intNowScrollRowIndex + 1)
                {
                    if(intNowScrollRowIndex == 0)
                        Dgv_ScheduleInfo.FirstDisplayedScrollingRowIndex = intNowScrollRowIndex;
                    else
                        Dgv_ScheduleInfo.FirstDisplayedScrollingRowIndex = intNowScrollRowIndex - 1;
                }
                else
                {
                    Dgv_ScheduleInfo.FirstDisplayedScrollingRowIndex = intNowScrollRowIndex;
                }
            }

        }

        /// <summary>
        /// 往下移一位
        /// </summary>
        private void Fun_ScheduleIndexDown()
        {
            List<int> SelectedCoilIndexList = new List<int>();
            DataRow drGetRow;

            //---  從下到上掃描鋼捲號碼，找出有選取的鋼捲號碼  ---//
            try
            {
                for (int nIndex = Dgv_ScheduleInfo.Rows.Count - 1; nIndex > -1; nIndex--)
                {
                    if (Dgv_ScheduleInfo.Rows[nIndex].Selected)
                    {
                        // 前三筆不能異動
                        if (nIndex <= intTopCanNotMove-1)
                        {
                            // 補訊息提示
                            break;
                        }
                        SelectedCoilIndexList.Add(nIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                // 補上錯誤訊息Dialog
            }

            try
            {
                //---  將選取的鋼捲號碼往下移一格  ---//
                for (int nCount = 0; nCount < SelectedCoilIndexList.Count ; nCount++)
                {
                    drGetRow = dt.NewRow();
                    int nSripIndex = SelectedCoilIndexList[nCount];

                    if (nSripIndex + 1 >= Dgv_ScheduleInfo.Rows.Count )
                    {
                        EventLogHandler.Instance.EventPush_Message($"排程异动已至底");
                        break;
                    }


                    drGetRow.ItemArray = dt.Rows[nSripIndex].ItemArray;
                    dt.Rows.RemoveAt(nSripIndex);
                    dt.Rows.InsertAt(drGetRow, nSripIndex + 1);
                    drGetRow = null;
                }

            }
            catch (Exception ex)
            {
                // 補上錯誤訊息Dialog
            }

            Dgv_ScheduleInfo.ClearSelection();

            for (int nCount = 0; nCount < SelectedCoilIndexList.Count; nCount++)
            {
                if((SelectedCoilIndexList[nCount] + 1) >= Dgv_ScheduleInfo.Rows.Count)
                {
                    continue ;
                }
                Dgv_ScheduleInfo.Rows[SelectedCoilIndexList[nCount] + 1].Selected = true;
            }

            //---  選取資料即將超出畫面，則移動捲軸  ---//
            if (Dgv_ScheduleInfo.SelectedRows.Count > 0)
            {
                int nSelRowIndex = Dgv_ScheduleInfo.SelectedRows[0].Index;
                int nShowRowPerPage = 25;

                if (nSelRowIndex + SelectedCoilIndexList.Count >= nShowRowPerPage)
                {
                    Dgv_ScheduleInfo.FirstDisplayedScrollingRowIndex = Dgv_ScheduleInfo.FirstDisplayedScrollingRowIndex+1;
                }
                else
                {
                    Dgv_ScheduleInfo.FirstDisplayedScrollingRowIndex = Dgv_ScheduleInfo.FirstDisplayedScrollingRowIndex;
                }
            }
        }

        /// <summary>
        /// 移至最後一位
        /// </summary>
        private void Fun_ScheduleIndexLast()
        {
            List<int> SelectedCoilIndexList = new List<int>();
            DataRow drGetRow;

            //---  從下到上掃描鋼捲號碼，找出有選取的鋼捲號碼  ---//
            #region 检查前三筆不能異動 ---重复
            try
            {
                for (int nIndex = Dgv_ScheduleInfo.Rows.Count - 1; nIndex > -1; nIndex--)
                {
                    if (Dgv_ScheduleInfo.Rows[nIndex].Selected)
                    {
                        // 前三筆不能異動
                        if (nIndex <= intTopCanNotMove-1)
                        {
                            // 補訊息提示
                            break;
                        }
                        SelectedCoilIndexList.Add(nIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                // 補上錯誤訊息Dialog
            }
            #endregion

            try
            {
                //---  將選取的鋼捲號碼移至最後  ---//
                for (int nCount = 0; nCount < SelectedCoilIndexList.Count; nCount++)
                {
                    drGetRow = dt.NewRow();

                    int nSripIndex = SelectedCoilIndexList[nCount];

                    if(nSripIndex  >= Dgv_ScheduleInfo.Rows.Count- SelectedCoilIndexList.Count)
                    {
                        EventLogHandler.Instance.EventPush_Message($"排程异动已至底");
                        break;
                    }

                    drGetRow.ItemArray = dt.Rows[nSripIndex].ItemArray;
                    dt.Rows.RemoveAt(nSripIndex);
                    dt.Rows.InsertAt(drGetRow, Dgv_ScheduleInfo.Rows.Count-nCount);
                    drGetRow = null;
                }

            }
            catch (Exception ex)
            {
                // 補上錯誤訊息Dialog
            }

            Dgv_ScheduleInfo.ClearSelection();

            for (int nCount = 0; nCount < SelectedCoilIndexList.Count; nCount++)
            {
                Dgv_ScheduleInfo.Rows[Dgv_ScheduleInfo.Rows.Count -1 - nCount].Selected = true;
            }

            //---  選取資料即將超出畫面，則移動捲軸  ---//
            if (Dgv_ScheduleInfo.SelectedRows.Count > 0)
            {
                int nSelRowIndex = Dgv_ScheduleInfo.SelectedRows[0].Index;
                int nShowRowPerPage = 14;

                if (nSelRowIndex >= nShowRowPerPage)
                {
                    Dgv_ScheduleInfo.FirstDisplayedScrollingRowIndex = nSelRowIndex - nShowRowPerPage;
                }
            }
        }

        #endregion

        /// <summary>
        /// 確定排程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_MovePdi_Click(object sender, EventArgs e)
        {
            if (Dgv_ScheduleInfo.DgvIsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("无排程不可进行确认排程", "确定排程",0);

                return;
            }
            //if (Dgv_ScheduleInfo.CurrentIsNull())
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk("无选取排程不可进行确认排程", "确定排程", 0);

            //    return;
            //} 
           
            PublicComm.ClientLog.Info($"排程確定動作");

            DataTable dt_ComparisonUpdateTime = new DataTable();
            try
            {
                string strSql = string.Empty;
                int Correct = 0;
                int Error = 0;

                for (int i = 0; i < dt_ScheduleSeq.Rows.Count; i++)
                {
                    string strCoilNo = Dgv_ScheduleInfo.Rows[i].Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value.ToString();

                    strSql = Frm_1_1_SqlFactory.SQL_Select_ScheduleMove_CheckUpdateTime(strCoilNo);
                    dt_ComparisonUpdateTime = DataAccess.Fun_SelectDate(strSql, "排程异动检查更新时间");

                    if (dt_ComparisonUpdateTime != null && dt_ComparisonUpdateTime.Rows.Count > 0)
                    {                      

                        string strUpdateTime1 = dt_ComparisonUpdateTime.Rows[0][nameof(CoilScheduleEntity.TBL_Production_Schedule.UpdateTime)].ToString();
                        DataRow[] draa = dtUpdateTime.Select($"{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)} = '{strCoilNo}'");
                        string strUpdateTime2 = "";
                        if (draa != null && draa.Length > 0)
                            strUpdateTime2 = draa[0][nameof(CoilScheduleEntity.TBL_Production_Schedule.UpdateTime)].ToString();

                            //string strUpdateTime2 = dtUpdateTime.Rows[i][nameof(CoilScheduleEntity.TBL_Production_Schedule.UpdateTime)].ToString();
                        DateTime.TryParse(strUpdateTime1, out DateTime dTime_1);
                        DateTime.TryParse(strUpdateTime2, out DateTime dTime_2);

                        if (dTime_1 <= dTime_2)
                        {
                            Correct += 1;
                        }
                        else
                        {
                            Error += 1;
                        }
                    }

                }
                if (Error == 0)
                {
                    //修改Schedule
                    for (int i = 0; i < dt_ScheduleSeq.Rows.Count; i++)
                    {
                        strSql = Frm_1_1_SqlFactory.SQL_Update_ScheduleSeqNo(dt_ScheduleSeq.Rows[i][nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)].ToString(), Dgv_ScheduleInfo.Rows[i].Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value.ToString());

                        if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, "排程異動"))
                        {
                            EventLogHandler.Instance.EventPush_Message($"修改排程处理失败:第[{i+1}]项排程");
                            return;
                        }
                    }

                    string strSql_TopLock = Frm_1_1_SqlFactory.SQL_Update_SystemSetting_TopScheduleLock("1");
                    if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql_TopLock, "TopLock"))
                    {
                        //EventLogHandler.Instance.EventPush_Message($"");
                        return;
                    }

                    Fun_ComboBoxItem();

                    Fun_DisplayDataGridView();

                    SCCommMsg.CS03_ScheduleChange Msg = new SCCommMsg.CS03_ScheduleChange
                    {
                        Source = "CPL1_HMI",
                        SchStatus = SCCommMsg.ScheduleStatus.ADJUST,
                        OperatorID = string.Empty,
                        ReasonCode = string.Empty,
                        EntryCoilID = string.Empty
                    };

                    PublicComm.Client.Tell(Msg);
                    EventLogHandler.Instance.LogInfo("1-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()}调整排程", "通知Server排程已調整");

                    DialogHandler.Instance.Fun_DialogShowOk("通知Server排程已調整", "确定排程", 4);

                    PublicComm.ClientLog.Info($"已通知Server排程異動");
                    PublicComm.AkkaLog.Info($"已通知Server排程異動");

                    //==========================================================
                    EventLogHandler.Instance.EventPush_Message($"排程调整完成");
                    PublicComm.ClientLog.Info("排程調整完成");

                  
                }
                else
                {
                    DialogHandler.Instance.Fun_DialogShowOk("排程资讯有刷新，请刷新页面再进行调整", "确定排程", 0);
                    EventLogHandler.Instance.EventPush_Message($"排程資訊有刷新，請刷新頁面");
                    EventLogHandler.Instance.LogInfo("1-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()}排程调整", "排程资讯有刷新，请刷新页面再进行调整");
                    PublicComm.ClientLog.Info($"排程資訊有刷新，請刷新頁面");
                }

                Btn_CancelMove.Visible = false;
                Btn_ReLoad.Enabled = true;
                Btn_OutSchedule.Enabled = true;
                Btn_ImportSchedule.Enabled = true;
                Btn_ImportPDI.Enabled = true;
                Btn_MovePdi.Enabled = false;
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"調整排程有誤");
                EventLogHandler.Instance.LogDebug("1-1", "調整排程有誤",$"{ex}");
                PublicComm.ClientLog.Debug($"排程調整有誤:{ex}");
                PublicComm.ExceptionLog.Debug($"排程調整有誤:{ex}");
            }

          
        }

        #endregion

        /// <summary>
        /// 重新整理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ReLoad_Click(object sender, EventArgs e)
        {
            PublicComm.ClientLog.Info($"訊息名稱:入料鋼卷排程資訊 訊息:刷新頁面動作");

            Fun_ReLoad();

            EventLogHandler.Instance.LogInfo("1-1",$"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()}页面重新刷新", "页面重新刷新");
            EventLogHandler.Instance.EventPush_Message($"页面重新刷新");
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public void Fun_ReLoad()
        {

            //鋼卷排程資訊
            try
            {
                Fun_DisplayDataGridView();
            }
            catch (Exception ex)
            {
                DialogHandler.Instance.Fun_DialogShowOk($"查询资料库失败:{ex}", "钢卷排程资讯", 3);

                EventLogHandler.Instance.LogDebug("1-1", $"钢卷排程资讯", $"查询资料库失败:{ex}");
                PublicComm.ClientLog.Info($"鋼卷排程資訊查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Info($"鋼卷排程資訊查詢資料庫失敗:{ex}");
            }


            //ComboBox选项
            try
            {
                Fun_ComboBoxItem();
            }
            catch (Exception ex)
            {
                DialogHandler.Instance.Fun_DialogShowOk($"查询资料库失败:{ex}", "ComboBox选项", 3);

                EventLogHandler.Instance.LogDebug("1-1", $"ComboBox选项", $"查询资料库失败:{ex}");
                PublicComm.ClientLog.Info($"ComboBox選項查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Info($"ComboBox選項查詢資料庫失敗:{ex}");
            }

            Ckb_Plan_No.Checked = false;

            Ckb_Entry_Coil_No.Checked = false;
        }

        #region 刪除排程

        /// <summary>
        /// 刪除排程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_OutSchedule_Click(object sender, EventArgs e)
        {

            if (Dgv_ScheduleInfo.DgvIsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("无单卷排程可删除", "单卷删除", 0);

                return;
            }


            if (Dgv_ScheduleInfo.CurrentIsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("无选取欲单卷删除之排程", "单卷删除", 0);

                return;
            } 


            if (Dgv_ScheduleInfo.CurrentRow.Index <= intTopCanNotMove - 1)
            {
                DialogHandler.Instance.Fun_DialogShowOk("前三笔排程禁止单卷删除", "单卷删除", 0);

                return;
            }

            string strDelCoil = Dgv_ScheduleInfo.CurrentRow.Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim();
            string strMessage = $"删除{Dgv_ScheduleInfo.CurrentRow.Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()}之排程";

            Frm_Reason fzr = new Frm_Reason
            {
                Coil = strDelCoil
            };

            fzr.Fun_CatchTitle("单卷删除", strMessage, "请选择删除原因");
            fzr.ShowDialog();
            fzr.Dispose();


            if (fzr.DialogResult == DialogResult.OK)
            {
                EventLogHandler.Instance.LogInfo("1-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()}删除单卷排程 钢卷编号:{strDelCoil}排程", $"删除单卷排程钢卷:{strDelCoil}原因代码:{fzr.StrReasonCode}原因:{fzr.StrReason}");

                SCCommMsg.CS03_ScheduleChange Msg = new SCCommMsg.CS03_ScheduleChange
                {
                    Source = "CPL1_HMI",
                    SchStatus = SCCommMsg.ScheduleStatus.DELETE,
                    EntryCoilID = strDelCoil,
                    OperatorID = PublicForms.Main.Lbl_LoginUser.Text.Trim(),
                    ReasonCode = fzr.StrReasonCode
                };

                PublicComm.Client.Tell(Msg);
                PublicComm.ClientLog.Info($"通知Server排程单卷删除");
                PublicComm.AkkaLog.Info($"通知Server排程单卷删除");
                EventLogHandler.Instance.LogInfo("1-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()}通知Server:排程单卷删除", $"钢卷编号:{strDelCoil} 人员:{ PublicForms.Main.Lbl_LoginUser.Text.Trim()} 原因代码: {fzr.StrReasonCode} 原因:{fzr.StrReason}");

                DialogHandler.Instance.Fun_DialogShowOk($"已通知Server删除[{strDelCoil}]单卷排程", $"删除[{strDelCoil}]单卷排程", 4);
                
                PublicComm.ClientLog.Info($"已通知Server删除[{strDelCoil}]单卷排程，删除原因[{ fzr.StrReasonCode}]{fzr.StrReason}");
                PublicComm.AkkaLog.Info($"已通知Server删除[{strDelCoil}]单卷排程，删除原因[{ fzr.StrReasonCode}]{fzr.StrReason}");


                //鋼卷排程資訊
                try
                {
                    Fun_DisplayDataGridView();
                }
                catch (Exception ex)
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"查询资料库失败:{ex}", "钢卷排程资讯", 3);

                    EventLogHandler.Instance.LogDebug("1-1", $"钢卷排程资讯", $"查询资料库失败:{ex}");
                    PublicComm.ClientLog.Debug($"鋼卷排程資訊查詢資料庫失敗:{ex}");
                    PublicComm.ExceptionLog.Debug($"鋼卷排程資訊查詢資料庫失敗:{ex}");
                }
            }

        }
     
        #endregion

        /// <summary>
        /// 要求PDI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_RequestPDI_Click(object sender, EventArgs e)
        {

            if (Dgv_ScheduleInfo.DgvIsNull())
            {

                DialogHandler.Instance.Fun_DialogShowOk("无排程不可要求PDI", "要求PDI", 0);

                return;
            }


            if (Dgv_ScheduleInfo.CurrentIsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("无选取排程不可要求PDI", "要求PDI", 0);

                return;
            }


            PublicComm.ClientLog.Info($"要求PDI動作");

            string Coil_ID = Dgv_ScheduleInfo.CurrentRow.Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim();

            DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel($"是否要求[{Coil_ID}]PDI?", $"要求[{Coil_ID}]PDI", Properties.Resources.dialogQuestion, 1);

            if (dialogR.Equals(DialogResult.OK))
            {
                SCCommMsg.CS02_AckPDI Msg = new SCCommMsg.CS02_AckPDI
                {
                    Source = "CPL1_HMI",
                    ID = "AckPDI",
                    Coil_ID = Dgv_ScheduleInfo.CurrentRow.Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim()
                };

                PublicComm.Client.Tell(Msg);

                string strMessage = $"已通知Server要求[{Coil_ID}]PDI";

                EventLogHandler.Instance.LogInfo("1-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text}通知Server要求钢卷编号:[{Coil_ID}]之PDI", $"钢卷编号:{Coil_ID}");
                EventLogHandler.Instance.EventPush_Message(strMessage);
                PublicComm.ClientLog.Info(strMessage);
                PublicComm.AkkaLog.Info(strMessage);

                DialogHandler.Instance.Fun_DialogShowOk(strMessage, "要求PDI", 4);
            }
            else
            { 

                //DialogHandler.Instance.Fun_DialogShowOk("取消要求PDI", "要求PDI", 4);

            }

        }


        #region 要求排程刷新

        /// <summary>
        /// 要求排程刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_NewRec_Click(object sender, EventArgs e)
        {
            EventLogHandler.Instance.EventPush_Message($"要求排程动作");
            PublicComm.ClientLog.Info($"要求排程動作");
            SCCommMsg.CS01_AckSchedule Msg = new SCCommMsg.CS01_AckSchedule();


            //if (Dgv_ScheduleInfo.Rows.Count.Equals(0))
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk("目前无排程", "要求排程", 0);
            //    return;
            //}


            //只能選擇一筆
            if (Dgv_ScheduleInfo.SelectedRows.Count > 1)
            {
                DialogHandler.Instance.Fun_DialogShowOk("选取行禁止大于一笔", "要求排程", 2);
                return;
            }

            if (Dgv_ScheduleInfo.CurrentRow != null)
            {
                //需多判斷為有選取的狀態，不然會導致無選取時要求全部排程被擋住
                if (Dgv_ScheduleInfo.CurrentRow.Index <= intTopCanNotMove - 1 && !Dgv_ScheduleInfo.CurrentIsNull())
                {
                    DialogHandler.Instance.Fun_DialogShowOk("前三笔排程禁止要求排程", "要求排程", 2);
                    return;
                }
            }

            if (Dgv_ScheduleInfo.CurrentIsNull() || Dgv_ScheduleInfo.Rows.Count.Equals(0))
            {
                DialogResult dialogResult = DialogHandler.Instance.Fun_DialogShowOkCancel("是否要求全部排程?", "要求排程资讯确认", Properties.Resources.dialogQuestion, 1);
                
                if (dialogResult == DialogResult.OK)
                    Msg.CoilID = "0";
                else
                    return;
            }
            else
            {
                string strReCoilNo = Dgv_ScheduleInfo.CurrentRow.Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value.ToString().Trim();

                StringBuilder sbText = new StringBuilder();
                sbText.AppendLine($"请选择 ");
                sbText.AppendLine($"按下 [  是  ]:要求钢卷编号{strReCoilNo}以后的排程 ");
                sbText.AppendLine($"按下 [  否  ]:要求全部排程 ");
                sbText.AppendLine($"按下 [ 取消 ]:不动作 ");

                DialogResult dialogResult = DialogHandler.Instance.Fun_DialogShowSelectOk(sbText.ToString(), "是否以钢卷号要求排程");


                if (dialogResult == DialogResult.Yes)
                {
                    Msg.CoilID = strReCoilNo;
                }
                else if (dialogResult == DialogResult.No)
                {
                    Msg.CoilID = "0";
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }
            }
            
            PublicComm.Client.Tell(Msg);
            EventLogHandler.Instance.LogInfo("1-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text}通知Server要求排程", "向MMS要求排程");

            DialogHandler.Instance.Fun_DialogShowOk("已通知Server要求排程", "要求排程", 4);

            PublicComm.ClientLog.Info($"通知Server要求排程");

            Btn_MovePdi.Enabled = false;
            Btn_CancelMove.Visible = false;
            Btn_OutSchedule.Enabled = true;

            Fun_DisplayDataGridView();
        }
        #endregion


        #region Search

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Search_Click(object sender, EventArgs e)
        {

            ////檢查搜尋條件
            //if (Ckb_Plan_No.Checked.Equals(false) && Ckb_Entry_Coil_No.Checked.Equals(false))
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk("请选择要搜寻之作业计划或入口卷号", "查询", 0);
            //}
            //else
            //if (Ckb_Plan_No.Checked && Cob_Plan_No.Text.IsEmpty())
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk("请选择要搜寻的作业计划", "查询", 0);
            //}
            //else if (Ckb_Entry_Coil_No.Checked && Cob_Entry_Coil_No.Text.IsEmpty())
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk("请选择要搜寻的入口卷号", "查询", 0);
            //}
            //else 
            //{ 
            Fun_SearchDataGridView();
            //}
        }

        /// <summary>
        /// 開啟詳細資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_PdiDet_Click(object sender, EventArgs e)
        {
            if (Dgv_ScheduleInfo.CurrentIsNull()) return;

            string strCoilNo = dt.Rows[Dgv_ScheduleInfo.CurrentRow.Index][nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].ToString();

            Fun_SelectedConfirm(Dgv_ScheduleInfo.CurrentRow,nameof(Dgv_ScheduleInfo)); //, strCoilNo//Dgv_ScheduleInfo.CurrentRow.Cells["钢卷编号"].Value.ToString()

            EventLogHandler.Instance.EventPush_Message($"开启[{strCoilNo}]详细资料");
            PublicComm.ClientLog.Info($"開啟詳細資料，鋼卷號:{strCoilNo}");
            
        }

        /// <summary>
        /// 資訊查詢_詳細資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_PdiDet_ForSearch_Click(object sender, EventArgs e)
        {
            if (Dgv_PDISearch.CurrentIsNull()) return;

            Fun_SelectedConfirm(Dgv_PDISearch.CurrentRow, nameof(Dgv_PDISearch));//, Dgv_PDISearch.CurrentRow.Cells[nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)].Value.ToString());
            EventLogHandler.Instance.EventPush_Message($"开启[{Dgv_PDISearch.CurrentRow.Cells[nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)].Value}]详细资料");
            PublicComm.ClientLog.Info($"開啟詳細資料，鋼卷號:{Dgv_PDISearch.CurrentRow.Cells[nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)].Value}");
        }

        /// <summary>
        /// 開啟詳細資料-選取確認
        /// </summary>
        /// <param name="dgv"></param>
        private void Fun_SelectedConfirm(DataGridViewRow dgvrData,string strDgvName)
        {
            if (dgvrData == null)
            {
                EventLogHandler.Instance.EventPush_Message($"请选择要显示详细资料的钢卷");
                PublicComm.ClientLog.Info($"未選擇鋼卷");
                return;
            }
            else
            {
                string strPlan_No  = "";
                string strCoil_ID = "";

                if (strDgvName == nameof(Dgv_ScheduleInfo))
                {
                    strPlan_No = dgvrData.Cells[nameof(CoilPDIEntity.TBL_PDI.Plan_No)].Value.ToString();
                    strCoil_ID = dgvrData.Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value.ToString(); 
                }

                if (strDgvName == nameof(Dgv_PDISearch))
                {                   
                    strPlan_No = dgvrData.Cells[nameof(CoilPDIEntity.TBL_PDI.Plan_No)].Value.ToString();
                    strCoil_ID = dgvrData.Cells[nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)].Value.ToString();
                }

                Frm_1_2_PDIDetailOpen(strCoil_ID, strPlan_No);
                //Event Log
                EventLogHandler.Instance.LogInfo("1-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text}开启详细资料", $"开启钢卷号码:{strCoil_ID}的PDI详细资料");
            }
            
           
        }

        private void Frm_1_2_PDIDetailOpen(string strDgvCurrentCoil, string strPlan_No = "")
        {
            PublicForms.Main.tsMenuItem_1_2.PerformClick();
            PublicForms.PDIDetail.Cob_EntryCoilID.Text = strDgvCurrentCoil;
            PublicForms.PDIDetail.Fun_SelectCoilPDI(strDgvCurrentCoil, strPlan_No);
            PublicComm.ClientLog.Info($"跳轉至1-2入料鋼卷詳細資料");

        }

        #endregion


        private void Tab_MainControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItemHandler.Instance.DrawItem(Tab_MainControl, e);
        }
       
        /// <summary>
        /// 通知排程已刷新
        /// </summary>
        /// <param name="msg"></param>
        public void Handle_SC04_ScheduleChangeNotice(SCCommMsg.SC04_ScheduleChangeNotice msg)
        {
            Lbl_UpdateMsg.Text = $"排程已更新 时间:{GlobalVariableHandler.Instance.getTime}";

            Pnl_NewSchMessage.Visible = true;

            EventLogHandler.Instance.EventPush_Message($"收到排程刷新通知");
            PublicComm.ClientLog.Info($"收到排程刷新通知");
            PublicComm.AkkaLog.Debug($"收到排程刷新通知");

            Timer_MsgPanel.Start();
        }


        /// <summary>
        /// 收到排程刷新時，Panel通知存在五秒後刷新排程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_MsgPanel_Tick(object sender, EventArgs e)
        {
            Pnl_NewSchMessage.Visible = false;

            //鋼卷排程資訊
            try
            {
                Fun_DisplayDataGridView();
            }
            catch (Exception ex)
            {
                DialogHandler.Instance.Fun_DialogShowOk($"查询资料库失败:{ex}", "钢卷排程资讯", 3);
                EventLogHandler.Instance.LogDebug("1-1", $"钢卷排程资讯", $"查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"鋼卷排程資訊查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"鋼卷排程資訊查詢資料庫失敗:{ex}");
            }

            Timer_MsgPanel.Stop();
        }


        private void Timer_NewSchMag_Tick(object sender, EventArgs e)
        {

            Fun_ReLoad();
           
            Pnl_NewSchMessage.Visible = false;
            Btn_CancelMove.Visible = false;
            Btn_OutSchedule.Enabled = true;
            Btn_ImportSchedule.Enabled = true;
            Btn_ImportPDI.Enabled = true;
            Btn_ReLoad.Enabled = true;
        }

        /// <summary>
        /// 匯入排程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ImportSchedule_Click(object sender, EventArgs e)
        {
            string strSql = string.Empty;

            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Excel Worksheets|*.xls;*.xlsx"
            };

            if (dialog.ShowDialog().Equals(DialogResult.OK))
            {
                try
                {
                    var fileName = dialog.FileName;
                    //if (!fileName.ToUpper().Contains("作业命令"))
                    //{
                    //    DialogHandler.Instance.Fun_DialogShowOk($"请开启汇入排程档案", "汇入排程", 2);
                    //    return;
                    //}
                    if (!PublicForms.IsLoadExcel(fileName))
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"{fileName}正在开启中", "汇入排程", 2);
                        return;
                    }

                    DataTable dtGetExcel = Fun_ExcelToDataTable(dialog.FileName);

                    if (dtGetExcel.IsNull())
                    {
                        DialogHandler.Instance.Fun_DialogShowOk("Excel转换结果无资料", "汇入排程", 0);
                        return;
                    }

                    List<string> importList = new List<string>();
                    int row = 0;
                    int col = 0;
                    foreach (DataRow datarow in dtGetExcel.Rows)
                    {
                        row++;
                        if (row < 4) continue;

                        if (datarow[0].ToString() != "")
                        {
                            foreach (DataColumn dc in dtGetExcel.Columns)
                            {
                                col++;
                                if (col < 3) continue;

                                if (datarow[dc].ToString() != "")
                                {
                                    importList.Add(datarow[dc].ToString());
                                }
                            }
                        }
                    }
                    #region Old
                    //int indexRow = 0;
                    //foreach (DataRow datarow in dtGetExcel.Rows)
                    //{
                    //    indexRow++;

                    //    if (indexRow < GlobalVariableHandler.LoadScheduleExcelStarRow) //Instance.
                    //    {
                    //        continue;
                    //    }

                    //    if (!datarow[0].ToString().Trim().Equals(string.Empty) && !datarow[GlobalVariableHandler.LoadScheduleExcelStarCloumn].ToString().Trim().Equals(string.Empty)) //Instance.
                    //    {
                    //        importList.Add(datarow[GlobalVariableHandler.LoadScheduleExcelStarCloumn].ToString()); //Instance.
                    //    }
                    //}
                    #endregion

                  

                    if (importList.Count.Equals(0))
                    {
                        DialogHandler.Instance.Fun_DialogShowOk("Excel转换结果无资料", "汇入排程",0);
                        return;
                    }

                    // 刪除資料庫(TBL_CoilSchedule)
                    strSql = Frm_1_1_SqlFactory.SQL_Clear_Schedule();
                    InsertSchedulePDI(strSql);

                    // 寫入資料庫(TBL_CoilSchedule)
                    for (int Index = 0; Index < importList.Count; Index++)
                    {
                        strSql = Frm_1_1_SqlFactory.SQL_Insert_ImportSchedule(Index + 1, importList[Index].ToString());

                        InsertSchedulePDI(strSql, "提示!排程汇入成功");
                    }

                    EventLogHandler.Instance.LogDebug("1-1", "排程汇入", "排程汇入成功");

                    PublicComm.ClientLog.Info($"排程匯入成功");

                    SCCommMsg.CS16_FinishLoadSchedule Msg = new SCCommMsg.CS16_FinishLoadSchedule();
                    PublicComm.Client.Tell(Msg);

                    DialogHandler.Instance.Fun_DialogShowOk("排程汇入成功並通知Server", "排程汇入", 4);

                    PublicComm.ClientLog.Info($"已通知Server 排程匯入");
                    PublicComm.AkkaLog.Info($"已通知Server 排程匯入");
                }
                catch (Exception ex)
                {
                    DialogHandler.Instance.Fun_DialogShowOk("排程汇入失败", "排程汇入", 3);

                    EventLogHandler.Instance.LogDebug("1-1", "排程汇入", $"排程汇入失败:{ex}");
                    PublicComm.ClientLog.Debug($"排程匯入失敗:{ex}");
                    PublicComm.ExceptionLog.Debug($"排程匯入失敗:{ex}");
                }

                //鋼卷排程資訊
                try
                {
                    Fun_DisplayDataGridView();
                }
                catch (Exception ex)
                {
                    DialogHandler.Instance.Fun_DialogShowOk("查询资料库失败", "钢卷排程资讯", 3);

                    EventLogHandler.Instance.LogDebug( "1-1", $"钢卷排程资讯", $"查询资料库失败:{ex}");
                    PublicComm.ClientLog.Debug($"鋼卷排程資訊查詢資料庫失敗:{ex}");
                    PublicComm.ExceptionLog.Debug($"鋼卷排程資訊查詢資料庫失敗:{ex}");
                }
            }
        }
        /// <summary>
        /// 匯入PDI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ImportPDI_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Excel Worksheets|*.xls;*.xlsx"
            };
            if (dialog.ShowDialog().Equals(DialogResult.OK))
            {
                try
                {
                    string strfileName = dialog.FileName;

                    //if (!fileName.ToUpper().Contains("PDI"))
                    //{
                    //    DialogHandler.Instance.Fun_DialogShowOk($"请开启汇入PDI档案", "汇入PDI", 2);
                    //    return;
                    //}
                    if (!PublicForms.IsLoadExcel(strfileName))
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"{strfileName}正在开启中", "汇入PDI", 2);
                        return;
                    }

                    DataTable dtGetDataTable = Fun_ExcelToDataTable(dialog.FileName);
                    if (dtGetDataTable.IsNull())
                    {
                        DialogHandler.Instance.Fun_DialogShowOk("Excel转换结果无资料", "汇入PDI",  0);
                        return;
                    }



                    DataTable dtNewData = Fun_TurnTable_New(dtGetDataTable);
                   
                    if(dtNewData == null || dtNewData.Rows.Count == 0)
                    {
                        DialogHandler.Instance.Fun_DialogShowOk("Excel资料异常，汇入失败", "汇入PDI", 3);
                        return;
                    }

                    //for (int index = 1; (index <= 3); index++)
                    //{
                    //    if ((dtNewData.Rows.Count > 0))
                    //    {
                    //        dtNewData.Rows.RemoveAt(0);
                    //    }

                    //}

                    //DataTable dtNew = Fun_TurnTable(Fun_ExcelToDataTable(dialog.FileName));

                    //if (dtNew.IsNull())
                    //{
                    //    DialogHandler.Instance.Fun_DialogShowOk("Excel转换无资料", "汇入PDI", 0);

                    //    EventLogHandler.Instance.LogInfo("1-1", $"汇入PDI", $"Excel转换无资料");
                    //    PublicComm.ClientLog.Debug($"Excel转换无资料");
                    //    return;
                    //}

                    ////存進DB
                    //ImportPDI_InsertUpdate(dtNew);


                    //存進DB
                    ImportPDI_InsertUpdate(dtNewData);
                    EventLogHandler.Instance.LogDebug("1-1", "PDI汇入", "PDI汇入成功");

                    //更新下方事件訊息
                    DialogHandler.Instance.Fun_DialogShowOk("PDI汇入成功", "汇入PDI", 4);
                    PublicComm.ClientLog.Info($"PDI匯入成功");
                }
                catch (Exception ex)
                {
                    DialogHandler.Instance.Fun_DialogShowOk("PDI汇入失败", "汇入PDI", 3);

                    EventLogHandler.Instance.LogDebug("1-1", "PDI汇入", $"PDI汇入失败:{ex}");
                    PublicComm.ClientLog.Debug($"PDI匯入失敗:{ex}");
                    PublicComm.ExceptionLog.Debug($"PDI匯入失敗:{ex}");
                    return;
                }

                SCCommMsg.CS17_FinishLoadPDI Msg = new SCCommMsg.CS17_FinishLoadPDI();
                PublicComm.Client.Tell(Msg);
                PublicComm.ClientLog.Info($"已通知Server PDI匯入");
                PublicComm.AkkaLog.Info($"已通知Server PDI匯入");

                //鋼卷排程資訊
                try
                {
                    Fun_DisplayDataGridView();
                }
                catch (Exception ex)
                {
                    DialogHandler.Instance.Fun_DialogShowOk("查询排程失败", "钢卷排程查询", 3);

                    EventLogHandler.Instance.LogDebug("1-1", $"钢卷排程资讯", $"查询资料库失败:{ex}");
                    PublicComm.ClientLog.Debug($"鋼卷排程資訊查詢資料庫失敗:{ex}");
                    PublicComm.ExceptionLog.Debug($"鋼卷排程資訊查詢資料庫失敗:{ex}");
                }

                //ComboBox选项
                try
                {
                    Fun_ComboBoxItem();
                }
                catch (Exception ex)
                {
                    DialogHandler.Instance.Fun_DialogShowOk("下拉式选单查询失败", "下拉式选单查询", 3);

                    EventLogHandler.Instance.LogDebug("1-1", $"ComboBox选项", $"查询资料库失败:{ex}");
                    PublicComm.ClientLog.Debug($"ComboBox選項查詢資料庫失敗:{ex}");
                    PublicComm.ExceptionLog.Debug($"ComboBox選項查詢資料庫失敗:{ex}");
                }

                //ComboBox选项
                try
                {
                    
                }
                catch (Exception ex)
                {
                    DialogHandler.Instance.Fun_DialogShowOk("1-2 下拉式选单查询失败", "下拉式选单查询", 3);

                    EventLogHandler.Instance.LogDebug("1-2", $"ComboBox选项", $"查询资料库失败:{ex}");
                    PublicComm.ClientLog.Debug($"ComboBox選項查詢資料庫失敗:{ex}");
                    PublicComm.ExceptionLog.Debug($"ComboBox選項查詢資料庫失敗:{ex}");
                }
            }
        }

        public DataTable Fun_ListToTable(IList list)
        {
            DataTable dtResult = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    dtResult.Columns.Add(pi.Name, pi.PropertyType);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    dtResult.LoadDataRow(array, true);
                }

            }
            return dtResult;
        }
        
        /// <summary>
        /// 把Table 直向 轉 橫向
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable Fun_TurnTable_New(DataTable dt)
        {           
            DataTable dtPDI_FromExcel = new DataTable();
            foreach(var strCol in typeof(ExcelColumnsHandler.TBL_PDI_FromExcel).GetProperties())
            {
                if (strCol.CanRead)
                {
                    dtPDI_FromExcel.Columns.Add(strCol.Name, typeof(string));
                }
            }
                      
            DataTable dtNew = dtPDI_FromExcel.Clone();

            try
            {
                //for (int i = 1; i < dt.Rows.Count; i++)
                //{
                //    dtNew.Columns.Add(dt.Rows[i][0].ToString());
                //}

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    DataRow dr = dtNew.NewRow();
                    dtNew.Rows.Add(dr);
                }

                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    for (int j = 2; j < dt.Columns.Count; j++)
                    {
                        dtNew.Rows[j - 1][i - 1] = dt.Rows[i][j].ToString();
                    }
                }
                List<DataRow> deletedRows = new List<DataRow>();
                foreach (DataRow dr in dtNew.Rows)
                {
                    if (string.IsNullOrEmpty(dr[nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)].ToString()))
                    {
                        deletedRows.Add(dr);
                    }
                    else
                    {
                        if (IsChinese(dr[nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)].ToString())) deletedRows.Add(dr);
                    }
                }

                foreach (DataRow dataRow in deletedRows)
                {
                    dataRow.Delete();
                }


            }
            catch (Exception ex)
            {
                DialogHandler.Instance.Fun_DialogShowOk("DataTable直式转横式失败", "排程/PDI汇入作业", 3);

                PublicComm.ClientLog.Debug($"DataTable直式轉橫式失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"DataTable直式轉橫式失敗:{ex}");
            }
            return dtNew;
        }
        private void ImportPDI_InsertUpdate(DataTable dtImportPDI)
        {
            string strSql;
            string strSql_Defect = string.Empty;

            
            foreach (DataRow dr in dtImportPDI.Rows)
            {
                string strCoil = dr[nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)].ToString().Trim();
                string strPlanNo = dr[nameof(CoilPDIEntity.TBL_PDI.Plan_No)].ToString().Trim();

                ////檢查鋼卷是否已存在PDI
                strSql = Frm_1_1_SqlFactory.SQL_Select_ImportPDI_CheckPDI(strCoil, strPlanNo);
                DataTable dtSearch = DataAccess.Fun_SelectDate(strSql, $"檢查{strCoil}是否存在PDI");
               
                int Index = dtImportPDI.Rows.IndexOf(dr);

                //存在
                if (!dtSearch.IsNull())
                {
                    //PDI
                    strSql = Frm_1_1_SqlFactory.SQL_Update_ImportPDI_PDI(dtImportPDI, Index);

                    ////缺陷
                    //strSql_Defect = Frm_1_1_SqlFactory.SQL_Update_ImportPDI_DefectData(dtImportPDI, Index);
                }
                //不存在
                else if (dtSearch.IsNull())
                {
                    //PDI
                    strSql = Frm_1_1_SqlFactory.SQL_Insert_ImportPDI_PDI(dtImportPDI, Index);

                    ////缺陷
                    //strSql_Defect = Frm_1_1_SqlFactory.SQL_Insert_ImportPDI_DefectData(dtImportPDI, Index);
                }

                InsertSchedulePDI(strSql, "PDI汇入动作");

                ////檢查鋼卷是否已存在缺陷資料
                strSql_Defect = Frm_1_1_SqlFactory.SQL_Select_ImportPDI_CheckPDI_Def(strCoil, strPlanNo);
                DataTable dtSearch_Def = DataAccess.Fun_SelectDate(strSql_Defect, $"檢查{strCoil}是否存在缺陷");

                //存在
                if (!dtSearch_Def.IsNull())
                {
                    ////PDI
                    //strSql = Frm_1_1_SqlFactory.SQL_Update_ImportPDI_PDI(dtImportPDI, Index);

                    //缺陷
                    strSql_Defect = Frm_1_1_SqlFactory.SQL_Update_ImportPDI_DefectData(dtImportPDI, Index);
                }
                //不存在
                else if (dtSearch_Def.IsNull())
                {
                    ////PDI
                    //strSql = Frm_1_1_SqlFactory.SQL_Insert_ImportPDI_PDI(dtImportPDI, Index);

                    //缺陷
                    strSql_Defect = Frm_1_1_SqlFactory.SQL_Insert_ImportPDI_DefectData(dtImportPDI, Index);
                }

                InsertSchedulePDI(strSql_Defect, "缺陷汇入动作");
            }
        }

        /// <summary>
        /// 判斷字串文字是否皆為中文
        /// </summary>
        /// <param name="strChinese">中文字串</param>
        /// <returns>若字串皆為中文 :true 含有中文以外的文字 :false</returns>
        private static bool IsChinese(string strChinese)
        {
            bool flag = true;
            int dRange;
            int dstringmax = Convert.ToInt32("9fff", 16);
            int dstringmin = Convert.ToInt32("4e00", 16);

            for (int i = 0; i < strChinese.Length; i++)
            {
                dRange = Convert.ToInt32(Convert.ToChar(strChinese.Substring(i, 1)));
                if (dRange >= dstringmin && dRange < dstringmax)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                    break;
                }
            }

            return flag;
        }

        // 把Table 直向 轉 橫向
        private DataTable Fun_TurnTable(DataTable dt)
        {
            DataTable dtNew = new DataTable();
            try
            {
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    dtNew.Columns.Add(dt.Rows[i][0].ToString());
                }

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    DataRow dr = dtNew.NewRow();
                    dtNew.Rows.Add(dr);
                }

                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    for (int j = 2; j < dt.Columns.Count; j++)
                    {
                        dtNew.Rows[j - 1][i - 1] = dt.Rows[i][j].ToString();
                    }
                }
                List<DataRow> deletedRows = new List<DataRow>();
                foreach (DataRow dr in dtNew.Rows)
                {
                    if (string.IsNullOrEmpty(dr[nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)].ToString()))
                    {
                        deletedRows.Add(dr);
                    }
                    else
                    {
                        if (IsChinese(dr[nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)].ToString())) deletedRows.Add(dr);
                    }
                }

                foreach (DataRow dataRow in deletedRows)
                {
                    dataRow.Delete();
                }
            }
            catch (Exception ex)
            {
                DialogHandler.Instance.Fun_DialogShowOk("DataTable直式转横式失败", "排程/PDI汇入作业", 3);

                PublicComm.ClientLog.Debug($"DataTable直式轉橫式失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"DataTable直式轉橫式失敗:{ex}");
            }
            return dtNew;
        }

        // 把Excel轉成DataTable
        private DataTable Fun_ExcelToDataTable(string FileName)
        {
            FileStream stream = File.Open(FileName, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader;

            if (Path.GetExtension(FileName).Equals(".xls"))
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            else
            {
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }

            DataSet result = excelReader.AsDataSet();
            DataTable dt = result.Tables[0];
            stream.Dispose();

            return dt;
        }

        private void InsertSchedulePDI(string strSqlComm,string Message = "")
        {
            if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSqlComm, Message))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Message}失败", Message, 3);

                return;
            }
        }

        private void Cob_Plan_No_Click(object sender, EventArgs e)
        {
            #region 計畫號
            string strSql = Frm_1_1_SqlFactory.SQL_Select_ComboBoxItems($" distinct [{nameof(CoilPDIEntity.TBL_PDI.Plan_No)}] ");
            DataTable dtGetPlanList = DataAccess.Fun_SelectDate(strSql, "1-1钢卷资讯计划号清单");

            if (dtGetPlanList.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无计划号清单");
                return;
            }

            Cob_Plan_No.DisplayMember = nameof(CoilPDIEntity.TBL_PDI.Plan_No);
            Cob_Plan_No.ValueMember = nameof(CoilPDIEntity.TBL_PDI.Plan_No);
            Cob_Plan_No.DataSource = dtGetPlanList;

            PublicComm.ClientLog.Info($"計畫號選項清單查詢");
            #endregion
        }

        private void Cob_Entry_Coil_No_Click(object sender, EventArgs e)
        {
            #region 鋼卷編號
            string strSql = Frm_1_1_SqlFactory.SQL_Select_ComboBoxItems($" [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] ");
            DataTable dtGetCoilList = DataAccess.Fun_SelectDate(strSql, "1-1钢卷资讯钢卷号清单");

            if (dtGetCoilList.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无钢卷号清单");
                return;
            }

            Cob_Entry_Coil_No.DisplayMember = nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID);
            Cob_Entry_Coil_No.ValueMember = nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID);
            Cob_Entry_Coil_No.DataSource = dtGetCoilList;

            PublicComm.ClientLog.Info($"鋼卷號選項清單查詢");
            #endregion
        }

        private void Btn_SleevePaperInfo_Click(object sender, EventArgs e)
        {
            Frm_SleevePaper _SleevePaper = new Frm_SleevePaper();
            _SleevePaper.ShowDialog();
            _SleevePaper.Dispose();
        }

        private void Chk_Auto_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_AutoRefresh.Checked)
                Timer_NewSchMag.Start();
            else
                Timer_NewSchMag.Stop();
        }

      
    }

    #region 匯出範例
    //#region 匯出排程 (暫Excel)
    //private void btnPrint_Click(object sender, EventArgs e)
    //{
    //    ExportExcel(timeData + "未上線鋼卷", dgOff);
    //    ExportExcel(timeData + "上線鋼卷", dgOn);
    //}

    //public static void ExportExcel(string fileName, DataGridView dgv)
    //{
    //    FolderBrowserDialog dlg = new FolderBrowserDialog();
    //    if (dlg.ShowDialog() == DialogResult.OK)
    //    {
    //        this.Text = dlg.SelectedPath;
    //    }
    //    SaveFileDialog saveDialog = new SaveFileDialog();
    //    string saveFileName = "";
    //    if (dgv.RowCount > 1)
    //    {
    //        saveDialog.DefaultExt = "xlsx";
    //        saveDialog.Filter = "Excel檔案|*.xlsx";
    //        saveDialog.FileName = fileName;
    //        saveFileName = saveDialog.FileName;
    //        Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
    //        if (xlApp == null)
    //        {
    //            MessageBox.Show("無法建立Excel物件，可能您未安裝Excel");
    //            return;
    //        }
    //        Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
    //        Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
    //        Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
    //        //寫入標題
    //        for (int i = 0; i < dgv.ColumnCount; i++)
    //        {
    //            worksheet.Cells[1, i + 1] = dgv.Columns[i].HeaderText;
    //        }
    //        //寫入數值
    //        for (int r = 0; r < dgv.Rows.Count; r++)
    //        {
    //            for (int i = 0; i < dgv.ColumnCount; i++)
    //            {
    //                worksheet.Cells[r + 2, i + 1] = dgv.Rows[r].Cells[i].Value;
    //            }
    //            Application.DoEvents();
    //        }
    //        worksheet.Columns.EntireColumn.AutoFit();//列寬自適應
    //        if (saveFileName != "")
    //        {
    //            try
    //            {
    //                //檢查是否有資料夾存在
    //                if (Directory.Exists(@"E:\2-1PDI排程")) { }
    //                else { Directory.CreateDirectory(@"E:\2-1PDI排程"); }//新增資料夾
    //                //檢查是否有相同檔案
    //                if (File.Exists(@"E:\2-1PDI排程\" + saveFileName + ".xlsx"))
    //                {
    //                    File.Delete(@"E:\2-1PDI排程\" + saveFileName + ".xlsx");
    //                    workbook.SaveAs(@"E:\2-1PDI排程\" + saveFileName);
    //                }
    //                else { workbook.SaveAs(@"E:\2-1PDI排程\" + saveFileName); }
    //                //fileSaved = true;
    //            }
    //            catch (Exception ex)
    //            {
    //                //fileSaved = false;
    //                MessageBox.Show("匯出檔案時出錯,檔案可能正被開啟！\n" + ex.Message);
    //            }
    //            xlApp.Quit();
    //            //GC.Collect();//強行銷燬 
    //            MessageBox.Show(fileName + "儲存成功", "提示", MessageBoxButtons.OK);
    //        }
    //        else
    //        {
    //            MessageBox.Show("報表為空,無表格需要匯出", "提示", MessageBoxButtons.OK);
    //        }
    //    }

    //}


    #endregion

}

