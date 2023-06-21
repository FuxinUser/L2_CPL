using DBService.Repository.EventLog;
using System.Windows.Forms;


namespace CPL1HMI
{
    public class Frm_5_1_ColumnsHandler
    {
        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly Frm_5_1_ColumnsHandler INSTANCE = new Frm_5_1_ColumnsHandler();
        }

        public static Frm_5_1_ColumnsHandler Instance { get { return SingletonHolder.INSTANCE; } }


        /// <summary>
        /// 事件記錄
        /// </summary>
        /// <param name="dgv"></param>
        public void Frm_5_1_EventLog(DataGridView dgv)
        {

            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("建立时间", nameof(EventLogEntity.TBL_EventLog.CreateTime), 260));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("电脑名称", nameof(EventLogEntity.TBL_EventLog.FrameGroup_No), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("事件类别", nameof(EventLogEntity.TBL_EventLog.Event_Type)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("事件名称", nameof(EventLogEntity.TBL_EventLog.Event_Description), 700));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("内容", nameof(EventLogEntity.TBL_EventLog.Command), 1000));

            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //事件内容 填满剩余的空间
            dgv.Columns[nameof(EventLogEntity.TBL_EventLog.Command)].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;//.DisplayedCells;
            dgv.Columns[nameof(EventLogEntity.TBL_EventLog.Event_Description)].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.Columns[nameof(EventLogEntity.TBL_EventLog.Command)].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.ClearSelection();
        }
    }
}
