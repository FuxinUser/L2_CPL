using DBService.Repository.PDI;
using DBService.Repository.PDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPL1HMI
{
    public class Frm_SelectDataOpen_ColumnsHandler
    {
        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly Frm_SelectDataOpen_ColumnsHandler INSTANCE = new Frm_SelectDataOpen_ColumnsHandler();
        }

        public static Frm_SelectDataOpen_ColumnsHandler Instance { get { return SingletonHolder.INSTANCE; } }



        public void Frm_DetailOpenColumns(DataGridView dgv,string strDataType)
        {
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("上传状态", nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag), 150));
            if (strDataType == "PDO")
            {
                #region DataGridView设定 ( P D O  )
                dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("计划号", nameof(PDOEntity.TBL_PDO.Plan_No), 200));
                dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口钢卷号", nameof(PDOEntity.TBL_PDO.Out_Coil_ID), 200));
                dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷号", nameof(PDOEntity.TBL_PDO.In_Coil_ID), 200));
                dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("生产开始时间", nameof(PDOEntity.TBL_PDO.StartTime), 200));
                dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("生产结束时间", nameof(PDOEntity.TBL_PDO.FinishTime), 200));
                #endregion
            }
            else if (strDataType == "PDI")
            {
                #region DataGridView设定 ( P D I  )
                dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("计划号", nameof(CoilPDIEntity.TBL_PDI.Plan_No), 200));
                dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷号", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID), 200));
                dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("更新时间", nameof(CoilPDIEntity.TBL_PDI.CreateTime), 270));

                ////填满剩余的空间
                //dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;//.DisplayedCells; 

                #endregion
            }
            else
            {
                dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("计划号", "Plan_No", 200));
            }
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ClearSelection();
        }
    }
}
