using DBService.Repository.CoilScheduleDelete;
using System.Windows.Forms;

namespace CPL1HMI
{
    public class Frm_1_3_ColumnsHandler
    {

        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly Frm_1_3_ColumnsHandler INSTANCE = new Frm_1_3_ColumnsHandler();
        }

        public static Frm_1_3_ColumnsHandler Instance { get { return SingletonHolder.INSTANCE; } }


        /// <summary>
        /// 排程刪除記錄
        /// </summary>
        /// <param name="dgv"></param>
        public void Frm_1_3_ScheduleDeleteRecord(DataGridView dgv)
        {
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("钢卷编号", nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.Coil_ID), 300));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("代码", nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.ReasonCode), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("备注", nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.Remarks), 1000));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("登入者", nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.OperatorId), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("日期", nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.CreateTime), 300));

        }

    }
}
