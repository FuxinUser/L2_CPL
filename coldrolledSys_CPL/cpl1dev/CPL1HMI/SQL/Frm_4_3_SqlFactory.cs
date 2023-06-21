using DBService.Repository.LookupTbl;
using DBService.Repository.LookupTblFlattener;
using DBService.Repository.LookupTblSideTrimmer;
using DBService.Repository.LookupTblSideTrimmer1;
using DBService.Repository.LookupTblTensionUnitDepth;
using DBService.Repository.LookupTblYieldStrength;
using System;
using System.Data;

namespace CPL1HMI
{

    /// <summary>
    /// 單位張力參數查詢表 SQL
    /// </summary>
    public static class Frm_4_3_SqlFactory_Tension
    {

        #region --- Display ---

        /// <summary>
        /// 搜尋單位張力參數查詢表
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_Tension()
        {
            string strSql = $@"Select [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade)}],
                                      [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width)}],
                                      [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min)}],
                                      [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max)}],
                                      [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.PORTension)}],
                                      [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.TRTension)}],
                                    
                                      convert(char(23), [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.UpdateTime)}], 121) UpdateTime
                                 From [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension)}]";

            if (PublicForms.PresetTable.Chk_SteelGrade.Checked && PublicForms.PresetTable.Chk_Thickness.Checked)
            {
                strSql += $@" Where

                          [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade)}] = '{PublicForms.PresetTable.Cob_SteelGrade.Text.Trim()}'
                          and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min)}] <= {PublicForms.PresetTable.Txt_Thickness.Text.Trim()}
                          and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max)}] >= {PublicForms.PresetTable.Txt_Thickness.Text.Trim()}";  
            }
            else if (PublicForms.PresetTable.Chk_SteelGrade.Checked)
            {
                strSql += $@" Where [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade)}] = '{PublicForms.PresetTable.Cob_SteelGrade.Text.Trim()}'";
            }
            else if (PublicForms.PresetTable.Chk_Thickness.Checked)
            {
                strSql += $@" Where [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min)}] <= {PublicForms.PresetTable.Txt_Thickness.Text.Trim()}
                             and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max)}] >= {PublicForms.PresetTable.Txt_Thickness.Text.Trim()}";
            }

            strSql += $@" order by [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade)}] asc";

            return strSql;
        }

        #endregion


        #region --- Funtion ---

        /// <summary>
        /// 單位張力參數查詢表-新增
        /// </summary>
        /// <returns></returns>
        public static string SQL_Insert_Tension()
        {
            string strSql = $@"Insert into [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension)}]
                            ([{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade)}],
                             [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width)}],
                             [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max)}],
                             [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min)}],
                             [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.PORTension)}],
                             [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.TRTension)}],                          
                             [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.UpdateTime)}])
                        Values
                            ('{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[0].Value.ToString().Trim()}',
                             '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[1].Value.ToString().Trim()}',
                             '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[3].Value.ToString().Trim()}',
                             '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[2].Value.ToString().Trim()}',
                             '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[4].Value.ToString().Trim()}',                     
                             '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[5].Value.ToString().Trim()}',
                             '{GlobalVariableHandler.Instance.getTime}')";

            return strSql;
        }


        /// <summary>
        /// 單位張力參數查詢表-修改
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public static string SQL_Update_Tension(DataRow dr)
        {

            string strSql = $@"Update [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension)}] set 
                            [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade)}] = '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[0].Value.ToString().Trim()}',
                            [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width)}] = {PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[1].Value.ToString().Trim()},
                            [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max)}] = {PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[3].Value.ToString().Trim()},
                            [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min)}] = {PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[2].Value.ToString().Trim()},
                            [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.PORTension)}] = {PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[4].Value.ToString().Trim()},
                            [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.TRTension)}] = {PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[5].Value.ToString().Trim()},  
                            [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.UpdateTime)}] = '{GlobalVariableHandler.Instance.getTime}'
                      Where [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade)]}'
                        and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width)}] = {dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width)]}
                        and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max)}] = {dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max)]}
                        and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min)}] = {dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min)]}
                        and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.PORTension)}] = {dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.PORTension)]} 
                        and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.TRTension)}] = {dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.TRTension)]}
                        ";
                        //and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.UpdateTime)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.UpdateTime)]}'

            return strSql;
        }


        /// <summary>
        /// 單位張力參數查詢表-刪除
        /// </summary>
        /// <returns></returns>
        public static string SQL_Delete_Tension(DataRow dr)
        {
            string strSql = $@"DELETE FROM [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension)}]
                        Where [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade)]}'
                          and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width)]}'
                          and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max)]}'
                          and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min)]}'
                          and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.PORTension)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.PORTension)]}'
                          and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.TRTension)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.TRTension)]}'  
                          and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.UpdateTime)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.UpdateTime)]}'";

            return strSql;
        }

        #endregion


    }

    /// <summary>
    /// 三輥張力輥參數查詢表 SQL
    /// </summary>
    public static class Frm_4_3_SqlFactory_TensionUnitDepth
    {

        #region --- Display ---

        /// <summary>
        /// 搜尋三輥張力輥參數查詢表
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_TensionUnitDepth()
        {
            string strSql = $@"Select [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Material_Grade)}],                                    
                                      [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Max)}],
                                      [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Min)}],
                                      [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.TensionUnitDepth)}],    
                                      convert(char(23), [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.UpdateTime)}], 121) UpdateTime
                                 From [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth)}]";

            if (PublicForms.PresetTable.Chk_SteelGrade.Checked && PublicForms.PresetTable.Chk_Thickness.Checked)
            {
                strSql += $@" Where

                          [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Material_Grade)}] = '{PublicForms.PresetTable.Cob_SteelGrade.Text.Trim()}'
                          and [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Min)}] <= {PublicForms.PresetTable.Txt_Thickness.Text.Trim()}
                          and [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Max)}] >= {PublicForms.PresetTable.Txt_Thickness.Text.Trim()}";
            }
            else if (PublicForms.PresetTable.Chk_SteelGrade.Checked)
            {
                strSql += $@" Where [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Material_Grade)}] = '{PublicForms.PresetTable.Cob_SteelGrade.Text.Trim()}'";
            }
            else if (PublicForms.PresetTable.Chk_Thickness.Checked)
            {
                strSql += $@" Where [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Min)}] <= {PublicForms.PresetTable.Txt_Thickness.Text.Trim()}
                             and [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Max)}] >= {PublicForms.PresetTable.Txt_Thickness.Text.Trim()}";
            }

            strSql += $@" order by [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Material_Grade)}] asc";

            return strSql;
        }

        #endregion


        #region --- Funtion ---

        /// <summary>
        /// 三輥張力輥參數查詢表-新增
        /// </summary>
        /// <returns></returns>
        public static string SQL_Insert_TensionUnitDepth()
        {
            string strSql = $@"Insert into [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth)}]
                            ([{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Material_Grade)}],  
                             [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Min)}],
                             [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Max)}],                         
                             [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.TensionUnitDepth)}],                          
                             [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.UpdateTime)}])
                        Values
                            ('{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Material_Grade)].Value.ToString().Trim()}',
                             '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Min)].Value.ToString().Trim()}',
                             '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Max)].Value.ToString().Trim()}',
                             '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.TensionUnitDepth)].Value.ToString().Trim()}',                                                                             
                             '{GlobalVariableHandler.Instance.getTime}')";

            return strSql;
        }


        /// <summary>
        /// 三輥張力輥參數查詢表-修改
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public static string SQL_Update_TensionUnitDepth(DataRow dr)
        {

            string strSql = $@"Update [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth)}] set 
                            [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Material_Grade)}] = '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Material_Grade)].Value.ToString().Trim()}',                           
                            [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Min)}] = {PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Min)].Value.ToString().Trim()},
                            [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Max)}] = {PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Max)].Value.ToString().Trim()},                         
                            [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.TensionUnitDepth)}] = {PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.TensionUnitDepth)].Value.ToString().Trim()},  
                            [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.UpdateTime)}] = '{GlobalVariableHandler.Instance.getTime}'
                      Where [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Material_Grade)}] = '{dr[nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Material_Grade)]}'
                      
                        and [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Max)}] = {dr[nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Max)]}
                        and [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Min)}] = {dr[nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Min)]}
                        and [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.TensionUnitDepth)}] = {dr[nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.TensionUnitDepth)]} 
                        ";
            //and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.UpdateTime)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.UpdateTime)]}'

            return strSql;
        }


        /// <summary>
        /// 三輥張力輥參數查詢表-刪除
        /// </summary>
        /// <returns></returns>
        public static string SQL_Delete_TensionUnitDepth(DataRow dr)
        {
            string strSql = $@"DELETE FROM [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth)}]
                        Where [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Material_Grade)}] = '{dr[nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Material_Grade)]}'                        
                          and [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Max)}] = '{dr[nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Max)]}'
                          and [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Min)}] = '{dr[nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Min)]}'
                          and [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.TensionUnitDepth)}] = '{dr[nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.TensionUnitDepth)]}' 
                          and [{nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.UpdateTime)}] = '{dr[nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.UpdateTime)]}'";

            return strSql;
        }

        #endregion


    }

    /// <summary>
    /// 圓盤剪參數查詢表 SQL
    /// </summary>
    public static class Frm_4_3_SqlFactory_Trimmer
    {

        #region --- Display ---

        /// <summary>
        /// 圓盤剪參數查詢表
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_SideTrimmer1(bool bolSearch = false)
        {
            string strSql = $@"Select [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.YS_Min)}],
                                      [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.YS_Max)}],
                                      [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.Coil_Thickness_Min)}],
                                      [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.Coil_Thickness_Max)}],
                                      [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.KnifeGap)}],
                                      [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.KnifeLap)}],
                                      convert(char(23), [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.UpdateTime)}], 121) UpdateTime
                       From [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1)}]";

            if (bolSearch)
            {
                if (PublicForms.PresetTable.Chk_Thickness.Checked || PublicForms.PresetTable.Chk_SteelGrade.Checked)
                    if (!PublicForms.PresetTable.Cob_SteelGrade.Text.IsEmpty() || !PublicForms.PresetTable.Txt_Thickness.Text.IsEmpty())
                    {
                        strSql += " Where ";
                    }


                if (PublicForms.PresetTable.Chk_Thickness.Checked)
                {
                    strSql += $@" [{nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.Coil_Thickness_Min)}] <= {PublicForms.PresetTable.Txt_Thickness.Text.Trim()}
                              and [{nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.Coil_Thickness_Max)}] >= {PublicForms.PresetTable.Txt_Thickness.Text.Trim()}";
                }

                if (PublicForms.PresetTable.Chk_Thickness.Checked && PublicForms.PresetTable.Chk_SteelGrade.Checked)
                {
                    strSql += $" and [{nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.Material_Grade)}] = '{PublicForms.PresetTable.Cob_SteelGrade.Text.Trim()}'";
                }
                else if (PublicForms.PresetTable.Chk_Thickness.Checked == false && PublicForms.PresetTable.Chk_SteelGrade.Checked)
                {
                    strSql += $" [{nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.Material_Grade)}] = '{PublicForms.PresetTable.Cob_SteelGrade.Text.Trim()}'";
                }
            }

            strSql += $" Order by [{nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.Coil_Thickness_Min)}] asc";

            return strSql;
        }

        #endregion


        #region --- Funtion ---

        /// <summary>
        /// 圓盤剪參數查詢表-新增
        /// </summary>
        /// <returns></returns>
        public static string SQL_Insert_SideTrimmer()
        {
            string strSql = $@"Insert into [TBL_LookupTable_SideTrimmer1]
                               ([{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.YS_Min)}],
                                [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.YS_Max)}],
                                [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.Coil_Thickness_Min)}],
                                [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.Coil_Thickness_Max)}],
                                [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.KnifeGap)}],
                                [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.KnifeLap)}],
                                [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.UpdateTime)}])
                        Values ('{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[0].Value.ToString().Trim()}',
                                '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[1].Value.ToString().Trim()}',
                                '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[2].Value.ToString().Trim()}',
                                '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[3].Value.ToString().Trim()}',
                                '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[4].Value.ToString().Trim()}',
                                '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[5].Value.ToString().Trim()}',
                                '{GlobalVariableHandler.Instance.getTime}')";

            return strSql;
        }


        /// <summary>
        /// 圓盤剪參數查詢表-修改
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public static string SQL_Update_SideTrimmer(DataRow dr)
        {

            string strSql = $@"UPDATE [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1)}] SET 
                                      [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.YS_Min)}] = '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[0].Value.ToString().Trim()}',
                                      [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.YS_Max)}] = '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[1].Value.ToString().Trim()}',
                                      [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.Coil_Thickness_Min)}] = '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[2].Value.ToString().Trim()}',
                                      [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.Coil_Thickness_Max)}] = '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[3].Value.ToString().Trim()}',
                                      [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.KnifeGap)}] = '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[4].Value.ToString().Trim()}',
                                      [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.KnifeLap)}] = '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[5].Value.ToString().Trim()}',
                                      [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.UpdateTime)}] = '{GlobalVariableHandler.Instance.getTime}'
                                Where [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.Coil_Thickness_Min)}] = '{dr[nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.Coil_Thickness_Min)]}'
                                  and [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.Coil_Thickness_Max)}] = '{dr[nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.Coil_Thickness_Max)]}'
                                  and [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.YS_Min)}] = '{dr[nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.YS_Min)]}'                                  
                                  and [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.YS_Max)}] = '{dr[nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.YS_Max)]}'";

                                  //and [{nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.UpdateTime)}] = '{dr[nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.UpdateTime)]}'

            return strSql;
        }


        /// <summary>
        /// 圓盤剪參數查詢表-刪除
        /// </summary>
        /// <returns></returns>
        public static string SQL_Delete_SideTrimmer(DataRow dr)
        {
            string strSql = $@"DELETE FROM [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1)}] 
                        Where [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.Coil_Thickness_Min)}] = '{dr[nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.Coil_Thickness_Min)]}'
                          and [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.Coil_Thickness_Max)}] = '{dr[nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.Coil_Thickness_Max)]}'
                          and [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.YS_Min)}] = '{dr[nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.YS_Min)]}'
                          and [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.YS_Max)}] = '{dr[nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.YS_Max)]}' ";
            return strSql;
        }

        #endregion

    }

    public static class Frm_4_3_SqlFactory_YieldStrength
    {
        #region --- Display ---
        /// <summary>
        /// 屈服强度查询表
        /// </summary>
        /// <param name="bolSearch"></param>
        /// <returns></returns>
        public static string SQL_Select_YieldStrength(bool bolSearch = false)
        {
            string strSql = $@"Select [{nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength.Steel_Grade)}],
                                      [{nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength.YS)}],
                                      convert(char(23), [{nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength.UpdateTime)}], 121) UpdateTime
                       From [{nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength)}]";
            //if (bolSearch)
            //{
            //    if ( PublicForms.PresetTable.Chk_SteelGrade.Checked)
            //        if (!PublicForms.PresetTable.Cob_SteelGrade.Text.IsEmpty() )
            //        {
            //            strSql += " Where ";
            //            strSql += $"  [{nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength.Steel_Grade)}] = '{PublicForms.PresetTable.Cob_SteelGrade.Text.Trim()}'";
            //        }               
            //}
            return strSql;
        }

        #endregion


        #region --- Funtion ---

        /// <summary>
        /// 屈服强度查询表-新增
        /// </summary>
        /// <returns></returns>
        public static string SQL_Insert_YieldStrength()
        {
            string strSql = $@"Insert into [{nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength)}]
                               ([{nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength.Steel_Grade)}],
                                [{nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength.YS)}],                               
                                [{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.UpdateTime)}])
                        Values ('{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[0].Value.ToString().Trim()}',
                                '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[1].Value.ToString().Trim()}',                             
                                '{GlobalVariableHandler.Instance.getTime}')";

            return strSql;
        }


        /// <summary>
        /// 圓盤剪參數查詢表-修改
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public static string SQL_Update_YieldStrength(DataRow dr)
        {

            string strSql = $@"UPDATE [{nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength)}] SET 
                                   -- [{nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength.Steel_Grade)}] = '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[0].Value.ToString().Trim()}',
                                      [{nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength.YS)}] = '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[1].Value.ToString().Trim()}', 
                                      [{nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength.UpdateTime)}] = '{GlobalVariableHandler.Instance.getTime}'
                                Where [{nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength.Steel_Grade)}] = '{dr[nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength.Steel_Grade)]}'";

            return strSql;
        }


        /// <summary>
        /// 圓盤剪參數查詢表-刪除
        /// </summary>
        /// <returns></returns>
        public static string SQL_Delete_YieldStrength(DataRow dr)
        {
            string strSql = $@"DELETE FROM [{nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength)}] 
                        Where [{nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength.Steel_Grade)}] = '{dr[nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength.Steel_Grade)]}' ";
            return strSql;
        }

        #endregion
    }

    /// <summary>
    /// 五輥整平機參數查詢表 SQL
    /// </summary>
    public static class Frm_4_3_SqlFactory_Flattener
    {

        #region --- Display ---

        /// <summary>
        /// 五輥整平機參數查詢表
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_Flattener(bool bolSearch = false)
        {
            string strSql = $@"Select [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Material_Grade)}],
                                      [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Min)}],
                                      [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Max)}],
                                      [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num1)}],
                                      [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num2)}],
                                      convert(char(23), [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.UpdateTime)}], 121) UpdateTime 
                          From [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener)}] ";

            if (bolSearch)
            {
                if (PublicForms.PresetTable.Chk_Thickness.Checked || PublicForms.PresetTable.Chk_SteelGrade.Checked)
                    if (!PublicForms.PresetTable.Cob_SteelGrade.Text.IsEmpty() || !PublicForms.PresetTable.Txt_Thickness.Text.IsEmpty())
                    {
                        strSql += " Where ";
                    }

                if (PublicForms.PresetTable.Chk_Thickness.Checked)
                {
                    strSql += $@"    [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Min)}] <= {PublicForms.PresetTable.Txt_Thickness.Text.Trim()}
                             and [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Max)}] >={PublicForms.PresetTable.Txt_Thickness.Text.Trim()}";
                }
               
                if (PublicForms.PresetTable.Chk_Thickness.Checked && PublicForms.PresetTable.Chk_SteelGrade.Checked)
                {
                    strSql += $" and [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Material_Grade)}] = '{PublicForms.PresetTable.Cob_SteelGrade.Text.Trim()}'";
                }
                else if (PublicForms.PresetTable.Chk_Thickness.Checked.Equals(false) && PublicForms.PresetTable.Chk_SteelGrade.Checked)
                {
                    strSql += $" [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Material_Grade)}] = '{PublicForms.PresetTable.Cob_SteelGrade.Text.Trim()}'";
                }

            }

            strSql += $@" Order by [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Strip_Yield_Stress_Min)}] asc";

            return strSql;
        }


        #endregion


        #region --- Funtion ---

        /// <summary>
        /// 五輥整平機參數查詢表-新增
        /// </summary>
        /// <returns></returns>
        public static string SQL_Insert_Flattener()
        {
            string strSql = $@"Insert into [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener)}]
                                ([{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Material_Grade)}],
                                 [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Min)}],
                                 [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Max)}],
                                 [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num1)}],
                                 [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num2)}],
                                 [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.UpdateTime)}])
                        Values ('{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[0].Value.ToString().Trim()}',
                                '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[1].Value.ToString().Trim()}',
                                '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[2].Value.ToString().Trim()}',
                                '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[3].Value.ToString().Trim()}',
                                '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[4].Value.ToString().Trim()}',
                                '{GlobalVariableHandler.Instance.getTime}')";

            return strSql;
        }


        /// <summary>
        /// 五輥整平機參數查詢表-修改
        /// </summary>
        public static string SQL_Update_Flattener(DataRow dr)
        {

            string strSql = $@"Update [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener)}] set 
                                [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Material_Grade)}] = '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[0].Value.ToString().Trim()}',
                                [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Min)}] = '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[1].Value.ToString().Trim()}',
                                [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Max)}] = '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[2].Value.ToString().Trim()}',
                                [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num1)}] = '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[3].Value.ToString().Trim()}',
                                [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num2)}] = '{PublicForms.PresetTable.Dgv_CurrentRow.Rows[0].Cells[4].Value.ToString().Trim()}',
                                [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.UpdateTime)}] = '{GlobalVariableHandler.Instance.getTime}'
                          Where [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Min)}] = '{dr[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Min)]}'
                            and [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Max)}] = '{dr[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Max)]}'
                            and [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num1)}] = '{dr[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num1)]}'
                            and [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num2)}] = '{dr[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num2)]}'";

                            //and [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.UpdateTime)}] = '{dr[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.UpdateTime)]}'

            return strSql;
        }


        /// <summary>
        /// 五輥整平機參數查詢表-刪除
        /// </summary>
        /// <returns></returns>
        public static string SQL_Delete_Flattener(DataRow dr)
        {
            string strSql = $@"Delete From [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener)}]
                        Where [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Material_Grade)}] = '{dr[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Material_Grade)]}'
                          and [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Min)}] = '{dr[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Min)]}'
                          and [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Max)}] = '{dr[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Max)]}'
                          and [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num1)}] = '{dr[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num1)]}'
                          and [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num2)}] = '{dr[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num2)]}'
                          and [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.UpdateTime)}] = '{dr[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.UpdateTime)]}'";

            return strSql;
        }

        #endregion

    }

}
