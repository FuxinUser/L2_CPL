using DBService.Repository.LookupTbl;
using DBService.Repository.LookupTblPaper;
using DBService.Repository.LookupTbSleeve;
using DBService.Repository.ScheduleDelete_CoilReject_Code;
using DBService.Repository.ScheduleDelete_CoilReject_Record_Temp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static CPL1HMI.DataBaseTableFactory;
namespace CPL1HMI
{
    /// <summary>
    /// ComboBox_Type
    /// </summary>
    public enum Cbo_Type
    {
        /// <summary>
        /// 计划种类
        /// </summary>
        Plan_Sort,
        /// <summary>
        /// 入/出口套筒类型
        /// </summary>
        Sleeve_Type,
        /// <summary>
        /// 入/出口垫纸方式
        /// </summary>
        PAPER_REQ_CODE,
        /// <summary>
        /// 入/出口垫纸种类
        /// </summary>
        Paper_Type,
        /// <summary>
        /// 反修类型
        /// </summary>
        Rework_Type,
        /// <summary>
        /// 订单/实际/外表面/内表面精度代码
        /// </summary>
        Surface_Accuracy,
        /// <summary>
        /// 好面朝向
        /// </summary>
        Base_Surface,
        /// <summary>
        /// 开卷方向
        /// </summary>
        Uncoiler_Direction,
        /// <summary>
        /// 取样要求
        /// </summary>
        Samp,
        /// <summary>
        /// 取样位置
        /// </summary>
        SAMPLE_FRQN_CODE,
        /// <summary>
        /// 钢卷来源
        /// </summary>
        Origin,
        /// <summary>
        /// 切边要求/分卷标记
        /// </summary>
        Trim,
        /// <summary>
        /// 导带使用
        /// </summary>
        Leader_Usage,
        /// <summary>
        /// 班次
        /// </summary>
        Shift,
        /// <summary>
        /// 班别
        /// </summary>
        Team,
        /// <summary>
        /// 最终卷标记
        /// </summary>
        End,
        /// <summary>
        /// 废品标记
        /// </summary>
        Scrap,
        /// <summary>
        /// 卷曲方向
        /// </summary>
        Winding_Direction,
        /// <summary>
        /// 翻面标记
        /// </summary>
        FLIP,
        /// <summary>
        /// EventLogLevel
        /// </summary>
        EventLogLevel,
        /// <summary>
        /// 封锁标记
        /// </summary>
        Hold,
        /// <summary>
        /// 系统 Client/Server
        /// </summary>
        System,
        /// <summary>
        /// 脱脂标记
        /// </summary>
        Skim,
        /// <summary>
        /// 抛光类型
        /// </summary>
        PolishingType,
        /// <summary>
        /// 研磨面
        /// </summary>
        GrindingSurface,
        /// <summary>
        /// 缺陷程度
        /// </summary>
        DefectLevel,
        /// <summary>
        /// 缺陷表面区分
        /// </summary>
        DefectSid,
        /// <summary>
        /// 缺陷宽向位置
        /// </summary>
        DefectPosW,
        /// <summary>
        /// 开卷方向
        /// </summary>
        Decoiler,
        /// <summary>
        /// 权限等级
        /// </summary>
        Authority_Class,
        /// <summary>
        /// 回退方式
        /// </summary>
        ReturnMode,
        /// <summary>
        /// 降速代碼
        /// </summary>
        Deceleration,
        /// <summary>
        /// 工序代码
        /// </summary>
        ProcessCode
    }
    
    public class ComboBoxIndexHandler
    {
       
        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly ComboBoxIndexHandler INSTANCE = new ComboBoxIndexHandler();
        }

        public static ComboBoxIndexHandler Instance { get { return SingletonHolder.INSTANCE; } }


        /// <summary>
        /// ComboBox选项
        /// </summary>
        /// <param name="_Type"></param>
        /// <param name="cbo"></param>
        public void Fun_SelectComboBoxItems(Cbo_Type _Type ,ComboBox cbo)
        {
            string strSql = ComboBoxHandler_SqlFactory.SQL_Select_ComboBoxItems(_Type);
            DataTable dtGetComboBoxItems = DataAccess.Fun_SelectDate(strSql, $"[{ _Type}]ComboBox选项");
            //增加空白選項
           
            if (dtGetComboBoxItems.IsNull()) return;

            cbo.DisplayMember = _Type == Cbo_Type.Sleeve_Type ||
                                _Type == Cbo_Type.PAPER_REQ_CODE || 
                                _Type == Cbo_Type.Paper_Type ||
                                _Type == Cbo_Type.ProcessCode ||
                                _Type == Cbo_Type.Origin ||
                                _Type == Cbo_Type.Surface_Accuracy ? cbo.DisplayMember = nameof(TBL_ComboBoxItems.Cbo_Value) : cbo.DisplayMember = nameof(TBL_ComboBoxItems.Cbo_Text);

            cbo.ValueMember = nameof(TBL_ComboBoxItems.Cbo_Value);
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.DataSource = dtGetComboBoxItems;
        }

        /// <summary>
        /// ComboBox选项
        /// </summary>
        /// <param name="_Type"></param>
        /// <param name="cbo"></param>
        public void Fun_SelectComboBoxItems(Cbo_Type _Type, ComboBox cbo,bool bolCanNull= false)
        {
            string strSql = ComboBoxHandler_SqlFactory.SQL_Select_ComboBoxItems(_Type);
            DataTable dtGetComboBoxItems = DataAccess.Fun_SelectDate(strSql, $"[{ _Type}]ComboBox选项");
           
            if (bolCanNull)
            {
                //增加空白選項
                dtGetComboBoxItems.Rows.Add(new object[] { });
                dtGetComboBoxItems.AcceptChanges();
            }
            
            if (dtGetComboBoxItems.IsNull()) return;

            cbo.DisplayMember = _Type == Cbo_Type.Sleeve_Type ||
                                _Type == Cbo_Type.PAPER_REQ_CODE ||
                                _Type == Cbo_Type.Paper_Type ||
                                _Type == Cbo_Type.Origin ||
                                _Type == Cbo_Type.Surface_Accuracy ? cbo.DisplayMember = nameof(TBL_ComboBoxItems.Cbo_Value) : cbo.DisplayMember = nameof(TBL_ComboBoxItems.Cbo_Text);

            cbo.ValueMember = nameof(TBL_ComboBoxItems.Cbo_Value);
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.DataSource = dtGetComboBoxItems;
        }

        /// <summary>
        /// ComboBox选项
        /// </summary>
        /// <param name="_Type"></param>
        /// <param name="cbo"></param>
        public void Fun_SelectComboBoxItems(DataTable dtGetComboBoxItems, ComboBox cbo, string strValue= nameof(TBL_ComboBoxItems.Cbo_Value), string strDisplay= nameof(TBL_ComboBoxItems.Cbo_Text))
        {
            //string strSql = ComboBoxHandler_SqlFactory.SQL_Select_ComboBoxItems(_Type);
            //DataTable dtGetComboBoxItems = DataAccess.Fun_SelectDate(strSql, $"[{ _Type}]ComboBox选项");

            if (dtGetComboBoxItems.IsNull()) return;

            cbo.DisplayMember = strDisplay;

            cbo.ValueMember = strValue;
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.DataSource = dtGetComboBoxItems;
        }


        /// <summary>
        /// 搜寻套筒选项
        /// </summary>
        /// <param name="_Type"></param>
        /// <param name="cbo"></param>
        public void Fun_SelectComboBoxItems_Sleeve(ComboBox cbo)
        {
            string strSql = ComboBoxHandler_SqlFactory.SQL_Select_SleeveComboBoxItems();
            DataTable dtGetSleeveComboBox = DataAccess.Fun_SelectDate(strSql,"[套筒代码]ComboBox选项查询资料库");
           
            //增加空白選項
            dtGetSleeveComboBox.Rows.Add(new object[] { });
            dtGetSleeveComboBox.AcceptChanges();

            if (dtGetSleeveComboBox.IsNull()) return;

            cbo.DisplayMember = nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve.Sleeve_Code);
            cbo.ValueMember = nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve.Sleeve_Code);
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.DataSource = dtGetSleeveComboBox;
        }


        /// <summary>
        /// 搜寻垫纸选项
        /// </summary>
        public void Fun_SelectComboBoxItems_Paper(ComboBox cbo)
        {
            string strSql = ComboBoxHandler_SqlFactory.SQL_Select_PaperComboBoxItems();
            DataTable dtGetPaperComboBox = DataAccess.Fun_SelectDate(strSql, "[垫纸代码]ComboBox选项");

            //增加空白選項
            dtGetPaperComboBox.Rows.Add(new object[] { });
            dtGetPaperComboBox.AcceptChanges();

            if (dtGetPaperComboBox.IsNull()) return;

            cbo.DisplayMember = nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper.Paper_Code);
            cbo.ValueMember = nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper.Paper_Code);
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.DataSource = dtGetPaperComboBox;
        }

      
        /// <summary>
        /// ComboBox选项说明
        /// </summary>
        /// <param name="_Type"></param>
        public void Fun_SelectComboBoxItemsSpare(Cbo_Type _Type,TextBox textBox)
        {
            textBox.Text = string.Empty;
            string strSql = string.Empty;

            if(_Type.Equals(Cbo_Type.Sleeve_Type))
                strSql = ComboBoxHandler_SqlFactory.SQL_Select_SleeveComboBoxItems();

            else if (_Type.Equals(Cbo_Type.Paper_Type))
                strSql = ComboBoxHandler_SqlFactory.SQL_Select_PaperComboBoxItems();

            else if (_Type.Equals(Cbo_Type.PAPER_REQ_CODE))
                strSql = ComboBoxHandler_SqlFactory.SQL_Select_ComboBoxSpare(Cbo_Type.PAPER_REQ_CODE);

            else if(_Type.Equals(Cbo_Type.Surface_Accuracy))
                strSql = ComboBoxHandler_SqlFactory.SQL_Select_ComboBoxSpare(Cbo_Type.Surface_Accuracy);

            else if (_Type.Equals(Cbo_Type.ProcessCode))
                strSql = ComboBoxHandler_SqlFactory.SQL_Select_ComboBoxSpare(Cbo_Type.ProcessCode);

            DataTable dtGetComboBoxSpare = DataAccess.Fun_SelectDate(strSql, "[ComboBox备注]ComboBox选项");

            if (dtGetComboBoxSpare.IsNull()) return;

            if (_Type.Equals(Cbo_Type.Sleeve_Type))
            {
                for (int i = 0; i < dtGetComboBoxSpare.Rows.Count; i++)
                {
                    string strSleeveCode = dtGetComboBoxSpare.Rows[i][nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve.Sleeve_Code)].ToString() ?? string.Empty;

                    string strMaterial = dtGetComboBoxSpare.Rows[i][nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve.Sleeve_Material)].Equals("1") ? "钢套筒" :
                                  dtGetComboBoxSpare.Rows[i][nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve.Sleeve_Material)].Equals("2") ? "纸套筒" : string.Empty;

                    string strSleeveWidth = dtGetComboBoxSpare.Rows[i][nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve.Sleeve_Width)].ToString() ?? string.Empty;

                    string strSleeveThick = dtGetComboBoxSpare.Rows[i][nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve.Sleeve_Thick)].ToString() ?? string.Empty;

                    string strSleeveWeight = dtGetComboBoxSpare.Rows[i][nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve.Sleeve_Weight)].ToString() ?? string.Empty;

                    textBox.Text += $"[{strSleeveCode}]-{strMaterial} 宽:{strSleeveWidth}(mm) 厚:{strSleeveThick}(mm) 重:{strSleeveWeight}(kg)" + Environment.NewLine;

                }
            }
            else if (_Type.Equals(Cbo_Type.Paper_Type))
            {
                for (int i = 0; i < dtGetComboBoxSpare.Rows.Count; i++)
                {
                    string strPaperCode = dtGetComboBoxSpare.Rows[i][nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper.Paper_Code)].ToString() ?? string.Empty;

                    string strBaseWeight = dtGetComboBoxSpare.Rows[i][nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper.Paper_Base_Weight)].ToString() ?? string.Empty;

                    string strPaperWidth = dtGetComboBoxSpare.Rows[i][nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper.Paper_Width)].ToString() ?? string.Empty;

                    string strPaperThick = dtGetComboBoxSpare.Rows[i][nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper.Paper_Thick)].ToString() ?? string.Empty;

                    string strMinThick = dtGetComboBoxSpare.Rows[i][nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper.Paper_Min_Thick)].ToString() ?? string.Empty;

                    string strMaxThick = dtGetComboBoxSpare.Rows[i][nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper.Paper_Max_Thick)].ToString() ?? string.Empty;

                    textBox.Text += $"[{strPaperCode}]基重:{strBaseWeight}(g/m2) 宽:{strPaperWidth}(mm) 厚:{strPaperThick}(um) 最小厚:{strMinThick}(um) 最大厚:{strMaxThick}(um)" + Environment.NewLine;

                }
            }
            else
            {
                for (int i = 0; i < dtGetComboBoxSpare.Rows.Count; i++)
                {
                    textBox.Text += dtGetComboBoxSpare.Rows[i][nameof(TBL_ComboBoxItems.Cbo_Text)].ToString() + Environment.NewLine;
                }
            }
        }


        #region 删除/退料代码

        /// <summary>
        /// 排程刪除、鋼捲回退代碼選項
        /// </summary>
        /// <param name="cbo"></param>
        public void Fun_SelectComboBoxItems_DeleteCode(ComboBox cbo)
        {
            string strSql = ComboBoxHandler_SqlFactory.SQLSelect_ScjeduleDelete_CoilReject_CodeCombBoxItems();
            DataTable dtGetDelCode = DataAccess.Fun_SelectDate(strSql, "[删除代码]ComboBox选项");

            if (dtGetDelCode.IsNull()) return;

            cbo.DisplayMember = nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Name);
            cbo.ValueMember = nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Code);
            cbo.DataSource = dtGetDelCode;
        }


        /// <summary>
        /// 刪除人員選項
        /// </summary>
        /// <param name="cbo"></param>
        public void Fun_SelectComboBoxItems_DeleteUser(ComboBox cbo)
        {
            string strSql = Frm_0_1_SqlFactory.SQL_Select_UserList();
            DataTable dtGetDelUser = DataAccess.Fun_SelectDate(strSql, "[删除人员]ComboBox选项");

            if (dtGetDelUser.IsNull()) return;

            cbo.DisplayMember = nameof(TBL_AuthorityData.User_ID);
            cbo.ValueMember = nameof(TBL_AuthorityData.User_ID);
            cbo.DataSource = dtGetDelUser;
        }


        /// <summary>
        /// 1-3排程刪除記錄-鋼捲號選項
        /// </summary>
        /// <param name="cbo"></param>
        public void Fun_SelectComboBoxItems_DeleteRecordCoilList(ComboBox cbo)
        {
            string strSql = Frm_1_3_SqlFactory.SQL_Select_ScheduleDeleteRecordCoilList();
            DataTable dtGetDelCoil = DataAccess.Fun_SelectDate(strSql, "[删除钢卷]ComboBox选项");

            if (dtGetDelCoil.IsNull()) return;

            cbo.DisplayMember = nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Coil_ID);
            cbo.ValueMember = nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Coil_ID);
            cbo.DataSource = dtGetDelCoil;
        }
        #endregion

        #region frm_4_1_SteelGrade

        /// <summary>
        /// 搜尋鋼種大類選項
        /// </summary>
        /// <param name="cbo"></param>
        public void Fun_SelectComboBoxItems_Material(ComboBox cbo)
        {
            string strSql = ComboBoxHandler_SqlFactory.SQL_Select_Material_ComboBoxItems();
            DataTable dtGetMatericalGrade = DataAccess.Fun_SelectDate(strSql, "[钢种大类]ComboBox选项");

            if (dtGetMatericalGrade.IsNull()) return;

            cbo.DisplayMember = nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade);
            cbo.ValueMember = nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade);
            cbo.DataSource = dtGetMatericalGrade;
        }

        #endregion

        /// <summary>
        /// 從指定資料表,取得ComboBox初始設定(顯示的Text,值),可群組排序,可不顯示Value
        /// </summary>
        /// <param name="cb">要設定的ComboBox</param>
        /// <param name="strSelectKey">資料來源指定Value欄位</param>
        /// <param name="strSelectShow">資料來源指定Text欄位</param>
        /// <param name="dtList">資料來源</param>
        /// <param name="bolGroup">true:群組+排序 ; false:不群組排序</param>
        /// <param name="bolShowValueText">true:[Value] Text ; false:Text</param>
        public void Fun_CobDataFromTable(ComboBox cb, string strSelectKey, string strSelectShow, DataTable dtList, bool bolGroup = false, bool bolShowValueText = false)
        {
            DataTable dtCombData = new DataTable();
           
            if (bolGroup)
            {
                DataTable dtBeforOder = dtList.Copy();

                dtBeforOder = dtBeforOder.AsEnumerable()
                                         .GroupBy(r => new { Col1 = r[strSelectKey] })
                                         .Select(g => g.OrderBy(r => r[strSelectKey]).First())
                                         .CopyToDataTable();
                //先排序
                DataView dv = dtBeforOder.DefaultView;
                dv.Sort = strSelectKey + " ASC ";
                dtCombData = dv.ToTable();
            }
            else
            {
                dtCombData = dtList.Copy();
            }

            List<string> strList = new List<string>();
            if (cb.Items.Count > 0)
            {
                cb.Items.Clear();
            }

            if (dtCombData != null)
            {
                for (int i = 0; i < dtCombData.Rows.Count; i++)
                {
                    string strCOBVALUE = dtCombData.Rows[i][strSelectKey].ToString();
                    string strCOBTEXT;
                    if (bolShowValueText)
                    {
                        strList.Add("[" + strCOBVALUE + "] " + dtCombData.Rows[i][strSelectShow].ToString());
                        strCOBTEXT = "[" + strCOBVALUE + "] " + dtCombData.Rows[i][strSelectShow].ToString();
                    }
                    else
                    {
                        strList.Add(strCOBVALUE);
                        strCOBTEXT = dtCombData.Rows[i][strSelectShow].ToString();
                    }

                    cb.Items.Add(new ComboboxItem(strCOBTEXT, strCOBVALUE));
                }

            }

            //cb.DataSource = strList.ToArray();
            cb.AutoCompleteCustomSource.Clear();
            cb.AutoCompleteCustomSource.AddRange(strList.ToArray());
            cb.DropDownStyle = ComboBoxStyle.DropDownList;//.DropDown;
            cb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;//.SuggestAppend;
            cb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;

        }

        private class ComboboxItem
        {
            public ComboboxItem(string text, string value)
            {
                Value = value;
                Text = text;
            }
            public string Text { get; set; }
            public string Value { get; set; }

            public override string ToString()
            {
                return Text;
            }

        }
    }
}
