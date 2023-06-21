using DBService.Repository.Utility;
using System.Windows.Forms;

namespace CPL1HMI
{
    public class Frm_4_2_ColumnsHandler
    {

        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly Frm_4_2_ColumnsHandler INSTANCE = new Frm_4_2_ColumnsHandler();
        }

        public static Frm_4_2_ColumnsHandler Instance { get { return SingletonHolder.INSTANCE; } }


        /// <summary>
        /// 能源耗用
        /// </summary>
        /// <param name="dgv"></param>
        public void Frm_4_2_Utility(DataGridView dgv)
        {
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("接收时间", nameof(UtilityEntity.TBL_Utility.Receive_Time), 300));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("班次", nameof(UtilityEntity.TBL_Utility.Shift), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("班别", nameof(UtilityEntity.TBL_Utility.Team), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("压缩空气(m³)", nameof(UtilityEntity.TBL_Utility.CompressedAir), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("间接冷却水(kg)", nameof(UtilityEntity.TBL_Utility.IndirectCollingWater), 300));
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

    }
}
