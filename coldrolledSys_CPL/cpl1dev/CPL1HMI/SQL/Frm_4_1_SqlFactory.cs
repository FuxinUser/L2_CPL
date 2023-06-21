using DBService.Repository.DelayLocation;
using DBService.Repository.DelayReasonCode;
using DBService.Repository.FaultCode;
using DBService.Repository.LineFaultRecords;
using System;
using System.Data;
using static CPL1HMI.DataBaseTableFactory;

namespace CPL1HMI
{
    public class Frm_4_1_SqlFactory
    {
        #region --- Display ---

        /// <summary>
        /// 搜尋停復機記錄
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_LineFaultRecord()
        {
            string strSql = $@"Select 

                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.unit_code)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_no)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_group)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location_desc)}],
                                    --[{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey)}],
                                    CONVERT(float, [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey)}])  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey)}],

                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_category)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_code)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_desc)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_remark)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_m)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_e)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_c)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_p)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_u)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_o)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_r)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_rs)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_cause)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_code)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS)}]

                                 From [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords)}]
                                Where [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}]
                              Between '{PublicForms.LineDelayRecord.Dtp_Start_Time.Value:yyyy-MM-dd} 00:00:00.000' and '{PublicForms.LineDelayRecord.Dtp_Finish_Time.Value:yyyy-MM-dd} 23:59:59.999' 
                                  And ({nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey)} = '' Or Convert(Real, {nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey)}) >= {PublicForms.LineDelayRecord.Nud_Stop_Time.Value}) ";

            // 3分鐘內的停復機紀錄要過濾掉
            strSql += $@"  AND ([{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)}] = ''  OR ( [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}] != '' AND [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)}] != '' AND CAST([{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey)}] AS float) >= 3   ))  ";

            if (PublicForms.LineDelayRecord.Chk_shift_no.Checked)
                strSql += $@" And [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_no)}] = '{PublicForms.LineDelayRecord.Cob_shift_no.SelectedValue}'";
            if (PublicForms.LineDelayRecord.Chk_shift_group.Checked)
                strSql += $@" And [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_group)}] = '{PublicForms.LineDelayRecord.Cob_shift_group.SelectedValue}'";

            if (PublicForms.LineDelayRecord.Chk_SendMMS.Checked && PublicForms.LineDelayRecord.Rdb_SendMMS.Checked)
                strSql += $@" And [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS)}] = 'Y'";
            else if (PublicForms.LineDelayRecord.Chk_SendMMS.Checked && PublicForms.LineDelayRecord.Rdb_UnSendMMS.Checked)
                strSql += $@" And ([{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS)}] = 'N' or [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS)}] is  null )";
            
            strSql += $@" Order By [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}] DESC";

            return strSql;
        }

        #endregion


        #region --- Funtion ---

        /// <summary>
        /// 修改停復機記錄
        /// </summary>
        /// <param name="drGetEditRow"></param>
        /// <returns></returns>
        public static string SQL_Update_LineFaultRecord(DataRow drGetEditRow)
        {
            //string strSql = $@"Update [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords)}] Set 
            //                        [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.unit_code)}] = N'{PublicForms.LineDelayRecord.Txt_UnitCode.Text}',
            //                        [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time)}] = N'{PublicForms.LineDelayRecord.Dtp_prod_time.Value:yyyy-MM-dd}',
            //                        [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_no)}] = N'{PublicForms.LineDelayRecord.Cob_prod_shift_no.SelectedValue}',
            //                        [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_group)}] = N'{PublicForms.LineDelayRecord.Cob_prod_shift_group.SelectedValue}',
            //                        [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}] = N'{PublicForms.LineDelayRecord.Dtp_stop_start_time.Value:yyyy-MM-dd HH:mm:ss}',
            //                        [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)}] = N'{PublicForms.LineDelayRecord.Dtp_stop_end_time.Value:yyyy-MM-dd HH:mm:ss}',
            //                        [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location)}] = N'{PublicForms.LineDelayRecord.Cob_delay_location.SelectedValue}',
            //                        [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location_desc)}] = N'{Fun_GetDesc(PublicForms.LineDelayRecord.Cob_delay_location.Text)}',
            //                        [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey)}] = N'{PublicForms.LineDelayRecord.Txt_Stop_Elased_Time.Text}',
            //                        [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_code)}] = N'{PublicForms.LineDelayRecord.Cob_delay_reason_code.SelectedValue}',
            //                        [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_desc)}] = N'{Fun_GetDesc(PublicForms.LineDelayRecord.Cob_delay_reason_code.Text)}',
            //                        [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_remark)}] = N'{PublicForms.LineDelayRecord.Txt_Remark.Text}',
            //                        [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_m)}] = N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_m.Text}',
            //                        [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_e)}] = N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_e.Text}',
            //                        [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_c)}] = N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_c.Text}',
            //                        [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_p)}] = N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_p.Text}',
            //                        [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_u)}] = N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_u.Text}',
            //                        [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_o)}] = N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_o.Text}',
            //                        [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_r)}] = N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_r.Text}',
            //                        [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_rs)}] =  N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_rs.Text}',
            //                        [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_cause)}] = N'{Fun_GetDesc(PublicForms.LineDelayRecord.Cob_deceleration_code.Text)}',
            //                        [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_code)}] = N'{PublicForms.LineDelayRecord.Cob_deceleration_code.SelectedValue}',
            //                        [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UpdateTime)}] = N'{DateTime.Now:yyyy-MM-dd HH:mm:ss}'
            //                Where [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}] = N'{DateTime.Parse(Convert.ToString(drGetEditRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)])):yyyy-MM-dd HH:mm:ss}'
            //                    And [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)}] = N'{DateTime.Parse(Convert.ToString(drGetEditRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)])):yyyy-MM-dd HH:mm:ss}' ";

            string strSql = $@"Update [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords)}] Set 
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.unit_code)}] = N'{PublicForms.LineDelayRecord.Txt_UnitCode.Text}',
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time)}] = N'{PublicForms.LineDelayRecord.Dtp_prod_time.Value:yyyy-MM-dd}',
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_no)}] = N'{PublicForms.LineDelayRecord.Cob_prod_shift_no.SelectedValue}',
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_group)}] = N'{PublicForms.LineDelayRecord.Cob_prod_shift_group.SelectedValue}',
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}] = N'{PublicForms.LineDelayRecord.Dtp_stop_start_time.Value:yyyy-MM-dd HH:mm:ss.fff}',
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)}] = N'{PublicForms.LineDelayRecord.Dtp_stop_end_time.Value:yyyy-MM-dd HH:mm:ss.fff}',
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location)}] = N'{PublicForms.LineDelayRecord.Cob_delay_location.SelectedValue}',
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location_desc)}] = N'{Fun_GetDesc(PublicForms.LineDelayRecord.Cob_delay_location.Text)}',
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey)}] = N'{PublicForms.LineDelayRecord.Txt_Stop_Elased_Time.Text}',
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_code)}] = N'{PublicForms.LineDelayRecord.Cob_delay_reason_code.SelectedValue}',
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_desc)}] = N'{Fun_GetDesc(PublicForms.LineDelayRecord.Cob_delay_reason_code.Text)}',
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_remark)}] = N'{PublicForms.LineDelayRecord.Txt_Remark.Text}',
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_m)}] = N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_m.Text}',
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_e)}] = N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_e.Text}',
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_c)}] = N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_c.Text}',
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_p)}] = N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_p.Text}',
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_u)}] = N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_u.Text}',
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_o)}] = N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_o.Text}',
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_r)}] = N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_r.Text}',
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_rs)}] =  N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_rs.Text}',
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_cause)}] = N'{Fun_GetDesc(PublicForms.LineDelayRecord.Cob_deceleration_code.Text)}',
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_code)}] = N'{PublicForms.LineDelayRecord.Cob_deceleration_code.SelectedValue}',
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UpdateTime)}] = N'{DateTime.Now:yyyy-MM-dd HH:mm:ss}'
                            Where [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}] = N'{((DateTime)drGetEditRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)]):yyyy-MM-dd HH:mm:ss.fff}'
                                And [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)}] = N'{((DateTime)drGetEditRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)]):yyyy-MM-dd HH:mm:ss.fff}' ";

            return strSql;
        }

        /// <summary>
        /// 修改停復機記錄_UploadMMS欄位
        /// </summary>
        /// <param name="drGetCurrentRow"></param>
        /// <returns></returns>
        public static string SQL_Update_LineFaultRecord_UploadMMS(DataRow drGetCurrentRow,string strUploadMMS)
        {
            //string strSql = $@"Update [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords)}] Set 
            //                          [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS)}] = N'{strUploadMMS}'
            //                    Where [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}] = N'{DateTime.Parse(Convert.ToString(drGetCurrentRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)])):yyyy-MM-dd HH:mm:ss}'
            //                    And [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)}] = N'{DateTime.Parse(Convert.ToString(drGetCurrentRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)])):yyyy-MM-dd HH:mm:ss}' ";

            string strSql = $@"Update [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords)}] Set 
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS)}] = N'{strUploadMMS}'
                                Where [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}] = N'{((DateTime)drGetCurrentRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)]):yyyy-MM-dd HH:mm:ss.fff}'
                                And [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)}] = N'{((DateTime)drGetCurrentRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)]):yyyy-MM-dd HH:mm:ss.fff}' ";

            return strSql;
        }
        #endregion


        #region --- ComboBoxItems ---

        /// <summary>
        /// 停機位置
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_DelayLocation()
        {
            // string strSql = $@"Select * From [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition)}] Order by [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Index_No)}] asc";
            string strSql = $@"Select [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Index_No)}],
                               [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationCode)}] as {nameof(TBL_ComboBoxItems.Cbo_Value)},
                               '[' + [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationCode)}] + ']' + [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationName)}] as {nameof(TBL_ComboBoxItems.Cbo_Text)}
                               From [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition)}] Order by [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Index_No)}] asc ";

            return strSql;
        }


        /// <summary>
        /// 停機原因代碼
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_DelayReasonCode()
        {
            //string strSql = $@"Select * From [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition)}] Order by [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Index_No)}] asc";
            string strSql = $@"Select [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Index_No)}],
                               [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonCode)}] as {nameof(TBL_ComboBoxItems.Cbo_Value)},
                               '[' +  [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonCode)}] +']' + [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonName)}] as {nameof(TBL_ComboBoxItems.Cbo_Text)}
                               From [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition)}] 
                               Order by [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Index_No)}] asc ";
            return strSql;
        }


        /// <summary>
        /// 故障代碼
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_FaultCode()
        {
            string strSql = $@"Select * From [{nameof(FaultCodeEntity.TBL_FaultCode)}] Order by [{nameof(FaultCodeEntity.TBL_FaultCode.Sequence_No)}] asc";

            return strSql;
        }

        #endregion


        #region --- 保留 ---

        public static string SQL_Insert_LineFaultRecord()
        {
            //string strSql = $@"Insert into [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords)}]
            //                                 ([{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.unit_code)}],
            //                                  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time)}],
            //                                  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_no)}],
            //                                  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_group)}],
            //                                  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}],
            //                                  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)}],
            //                                  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location)}],
            //                                  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location_desc)}],
            //                                  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey)}],
            //                                  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_code)}],
            //                                  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_desc)}],
            //                                  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_remark)}],
            //                                  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_m)}],
            //                                  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_e)}],
            //                                  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_c)}],
            //                                  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_p)}],
            //                                  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_u)}],
            //                                  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_o)}],
            //                                  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_r)}],
            //                                  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_rs)}],
            //                                  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_cause)}],
            //                                  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_code)}],
            //                                  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.CreateTime)}])
            //                           Values
            //                                 (N'{PublicForms.LineDelayRecord.Txt_UnitCode.Text}',
            //                                  N'{PublicForms.LineDelayRecord.Dtp_prod_time.Value:yyyy-MM-dd}',
            //                                  N'{PublicForms.LineDelayRecord.Cob_prod_shift_no.SelectedValue}',
            //                                  N'{PublicForms.LineDelayRecord.Cob_prod_shift_group.SelectedValue}',
            //                                  N'{PublicForms.LineDelayRecord.Dtp_stop_start_time.Value:yyyy-MM-dd HH:mm:ss}',
            //                                  N'{PublicForms.LineDelayRecord.Dtp_stop_end_time.Value:yyyy-MM-dd HH:mm:ss}',
            //                                  N'{PublicForms.LineDelayRecord.Cob_delay_location.SelectedValue}',
            //                                  N'{Fun_GetDesc(PublicForms.LineDelayRecord.Cob_delay_location.Text, "]")}',
            //                                  N'{PublicForms.LineDelayRecord.Txt_Stop_Elased_Time.Text}',
            //                                  N'{PublicForms.LineDelayRecord.Cob_delay_reason_code.SelectedValue}',
            //                                  N'{Fun_GetDesc(PublicForms.LineDelayRecord.Cob_delay_reason_code.Text, "]")}',
            //                                  N'{PublicForms.LineDelayRecord.Txt_Remark.Text}',
            //                                  N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_m.Text}',
            //                                  N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_e.Text}',
            //                                  N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_c.Text}',
            //                                  N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_p.Text}',
            //                                  N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_u.Text}',
            //                                  N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_o.Text}',
            //                                  N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_r.Text}',
            //                                  N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_rs.Text}',
            //                                  N'{Fun_GetDesc(PublicForms.LineDelayRecord.Cob_deceleration_code.Text,"]")}',
            //                                  N'{PublicForms.LineDelayRecord.Cob_deceleration_code.SelectedValue}',
            //                                  N'{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";

            string strSql = $@"Insert into [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords)}]
                                             ([{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.unit_code)}],
                                              [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time)}],
                                              [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_no)}],
                                              [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_group)}],
                                              [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}],
                                              [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)}],
                                              [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location)}],
                                              [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location_desc)}],
                                              [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey)}],
                                              [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_code)}],
                                              [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_desc)}],
                                              [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_remark)}],
                                              [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_m)}],
                                              [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_e)}],
                                              [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_c)}],
                                              [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_p)}],
                                              [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_u)}],
                                              [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_o)}],
                                              [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_r)}],
                                              [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_rs)}],
                                              [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_cause)}],
                                              [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_code)}],
                                              [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.CreateTime)}])
                                       Values
                                             (N'{PublicForms.LineDelayRecord.Txt_UnitCode.Text}',
                                              N'{PublicForms.LineDelayRecord.Dtp_prod_time.Value:yyyy-MM-dd}',
                                              N'{PublicForms.LineDelayRecord.Cob_prod_shift_no.SelectedValue}',
                                              N'{PublicForms.LineDelayRecord.Cob_prod_shift_group.SelectedValue}',
                                              N'{PublicForms.LineDelayRecord.Dtp_stop_start_time.Value:yyyy-MM-dd HH:mm:ss.fff}',
                                              N'{PublicForms.LineDelayRecord.Dtp_stop_end_time.Value:yyyy-MM-dd HH:mm:ss.fff}',
                                              N'{PublicForms.LineDelayRecord.Cob_delay_location.SelectedValue}',
                                              N'{Fun_GetDesc(PublicForms.LineDelayRecord.Cob_delay_location.Text, "]")}',
                                              N'{PublicForms.LineDelayRecord.Txt_Stop_Elased_Time.Text}',
                                              N'{PublicForms.LineDelayRecord.Cob_delay_reason_code.SelectedValue}',
                                              N'{Fun_GetDesc(PublicForms.LineDelayRecord.Cob_delay_reason_code.Text, "]")}',
                                              N'{PublicForms.LineDelayRecord.Txt_Remark.Text}',
                                              N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_m.Text}',
                                              N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_e.Text}',
                                              N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_c.Text}',
                                              N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_p.Text}',
                                              N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_u.Text}',
                                              N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_o.Text}',
                                              N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_r.Text}',
                                              N'{PublicForms.LineDelayRecord.Txt_Resp_Depart_Delay_Time_rs.Text}',
                                              N'{Fun_GetDesc(PublicForms.LineDelayRecord.Cob_deceleration_code.Text, "]")}',
                                              N'{PublicForms.LineDelayRecord.Cob_deceleration_code.SelectedValue}',
                                              N'{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";

            return strSql;
        }


        /// <summary>
        /// 刪除停復機記錄
        /// </summary>
        /// <returns></returns>
        public static string SQL_Delete_LineFaultRecord()
        {
            //string strSql = $@"Delete From [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords)}] 
            //                         Where [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time)}] = '{PublicForms.LineDelayRecord.Dtp_prod_time.Value:yyyy-MM-dd}'
            //                           And [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}] = '{PublicForms.LineDelayRecord.Dtp_stop_start_time.Value:yyyy-MM-dd HH:mm:ss}'";

            string strSql = $@"Delete From [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords)}] 
                                     Where [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time)}] = '{PublicForms.LineDelayRecord.Dtp_prod_time.Value:yyyy-MM-dd}'
                                       And [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}] = '{PublicForms.LineDelayRecord.Dtp_stop_start_time.Value:yyyy-MM-dd HH:mm:ss.fff}'";

            return strSql;
        }

        #endregion


        public static string Fun_StringCut(string getString)
        {
            int intLocation = getString.IndexOf("-");
            getString = getString.Substring(0, intLocation);
            return getString;
        }

        public static string Fun_GetDesc(string getString, string strCutValue = "]")
        {
            int intLocation = getString.IndexOf(strCutValue);

            getString = getString.Substring(intLocation + 1 );
            return getString;
        }
    }
}
