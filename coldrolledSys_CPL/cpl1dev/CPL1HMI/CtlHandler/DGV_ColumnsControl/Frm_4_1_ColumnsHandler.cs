using DBService.Repository.LineFaultRecords;
using System.Windows.Forms;

namespace CPL1HMI
{
    public class Frm_4_1_ColumnsHandler
    {

        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly Frm_4_1_ColumnsHandler INSTANCE = new Frm_4_1_ColumnsHandler();
        }

        public static Frm_4_1_ColumnsHandler Instance { get { return SingletonHolder.INSTANCE; } }


        /// <summary>
        /// 停復機記錄 
        /// </summary>
        /// <param name="dgv"></param>
        public void Frm_4_1_LineDelayRecord(DataGridView dgv)
        {
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("上传MMS", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS), 140));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("机组代码", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.unit_code), 140));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("生产日期", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time), 140));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("生产班次", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_no), 140));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("生产班组", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_group), 140));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("停机开始时间", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("停机结束时间", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("停机位置", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location), 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("停机位置描述", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location_desc), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("停机持续时间(Min)", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey), 180));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("停机原因代码", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_code), 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("停机原因描述", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_desc), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("停机原因备注", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_remark), 300));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("机械部门原因停机时间(Min)", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_m), 250));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("电器部门原因停机时间(Min)", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_e), 250));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("L3原因停机时间(Min)", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_c), 220));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("生产部门原因停机时间(Min)", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_p), 250));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("正常停机时间(Min)", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_u), 220));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("其他部门原因停机时间(Min)", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_o), 250));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("换辊原因停机时间(Min)", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_r), 240));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("磨辊间原因停机时间(Min)", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_rs), 240));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("降速代码", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_code), 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("降速原因", nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_cause), 200));
            dgv.ClearSelection();
        }

    }
}
