using DBService.Repository.LookupTbl;
using DBService.Repository.LookupTblFlattener;
using DBService.Repository.LookupTblSideTrimmer;
using DBService.Repository.LookupTblSideTrimmer1;
using DBService.Repository.LookupTblTensionUnitDepth;
using DBService.Repository.LookupTblYieldStrength;
using System.Windows.Forms;

namespace CPL1HMI
{
    public class Frm_4_3_ColumnsHandler
    {

        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly Frm_4_3_ColumnsHandler INSTANCE = new Frm_4_3_ColumnsHandler();
        }

        public static Frm_4_3_ColumnsHandler Instance { get { return SingletonHolder.INSTANCE; } }



        /// <summary>
        /// 圓盤剪參數查詢表
        /// </summary>
        /// <param name="dgv"></param>
        public void Frm_4_3_Trimmer(DataGridView dgv)
        {
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("屈服強度下限(mm)", nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.YS_Min), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("屈服強度上限(mm)", nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.YS_Max), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("厚度下限(mm)", nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.Coil_Thickness_Min), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("厚度上限(mm)", nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.Coil_Thickness_Max), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("刀具间隙(mm)", nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.KnifeGap), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("刀具重叠量(mm)", nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.KnifeLap), 200));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("修改日期", nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.UpdateTime), 400));
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        /// <summary>
        /// 屈服强度查询表
        /// </summary>
        /// <param name="dgv"></param>
        public void Frm_4_3_YieldStrength(DataGridView dgv)
        {
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("钢种", nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength.Steel_Grade), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("屈服強度(mm)", nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength.YS), 200));
       
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("修改日期", nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.UpdateTime), 400));
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
      
        /// <summary>
        /// 單位張力參數查詢表
        /// </summary>
        /// <param name="dgv"></param>
        public void Frm_4_3_Tension(DataGridView dgv)
        {
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("钢种大类", nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽度(mm)", nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width), 100));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("厚度下限(mm)", nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min), 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("厚度上限(mm)", nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max), 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("POR單位張力(N/mm2)", nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.PORTension), 140));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("TR單位張力(N/mm2)", nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.TRTension), 140));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("修改日期", nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.UpdateTime), 400));
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        /// <summary>
        /// 三輥張力輥參數查詢表
        /// </summary>
        /// <param name="dgv"></param>
        public void Frm_4_3_TensionUnitDepth(DataGridView dgv)
        {
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("钢种大类", nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Material_Grade), 200));           
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("厚度下限(mm)", nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Min), 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("厚度上限(mm)", nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Max), 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("三辊张力辊下压量(mm)", nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.TensionUnitDepth), 200));
            
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("修改日期", nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.UpdateTime), 400));
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        /// <summary>
        /// 五輥整平機參數查詢表
        /// </summary>
        /// <param name="dgv"></param>
        public void Frm_4_3_Flattener(DataGridView dgv)
        {
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("钢种大类", nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Material_Grade), 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("厚度下限(mm)", nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Min), 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("厚度上限(mm)", nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Max), 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("下压1", nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num1), 100,6,true));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("下压2", nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num2), 100,3, true));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("修改日期", nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.UpdateTime), 400));
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

    }
}
