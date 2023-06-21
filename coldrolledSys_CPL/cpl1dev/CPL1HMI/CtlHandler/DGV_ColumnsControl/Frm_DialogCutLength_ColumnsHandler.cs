using DBService.Repository.CoilScheduleDelete;
using DBService.Repository.CutReocrd;
using System.Windows.Forms;

namespace CPL1HMI
{
    public class Frm_DialogCutLength_ColumnsHandler
    {

        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly Frm_DialogCutLength_ColumnsHandler INSTANCE = new Frm_DialogCutLength_ColumnsHandler();
        }

        public static Frm_DialogCutLength_ColumnsHandler Instance { get { return SingletonHolder.INSTANCE; } }


        /// <summary>
        /// 分切暫存紀錄
        /// </summary>
        /// <param name="dgv"></param>
        public void Frm_DialogCutLength_CutRecord_Temp(DataGridView dgv)
        {       

            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("钢卷编号", nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.Coil_ID), 180));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口卷号", nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.In_Coil_ID), 180));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("原PDI的出口钢卷号", nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.OriPDI_Out_Coil_ID), 180));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("剪刀设备", nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.CutDevice), 120));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("分切模式", nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.CutMode), 120));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("切割长度", nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.CutLength), 120));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("分切时间", nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.CutTime), 180));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("分切钢卷外径", nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.Coil_OutDiam), 160));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("分切钢卷长度", nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.Coil_Length), 160));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("分切钢卷理论重", nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.Coil_CalcWeight), 160));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("拆纸机标记", nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.Coil_PaperFlag), 160));

            //新增CheckBox在DataGridView
            DataGridViewColumn CheckBoxColumn = new DataGridViewCheckBoxColumn
            {
                Name = "CheckRow",//strShow,
                DataPropertyName = "CheckRow",
                Width = 70,
                HeaderText = "选取"              
            };
            dgv.Columns.Insert(0, CheckBoxColumn);

            //dgv.Columns[0].Frozen = true;
            dgv.Columns[1].Frozen = true;
            //dgv.Columns[2].Frozen = true;
            //dgv.Columns[3].Frozen = true;

            foreach(DataGridViewColumn dgvc in dgv.Columns)
            {
                if (dgvc.Name == "CheckRow") continue;
                dgvc.ReadOnly = true;
            }
        }
       
    }
}
