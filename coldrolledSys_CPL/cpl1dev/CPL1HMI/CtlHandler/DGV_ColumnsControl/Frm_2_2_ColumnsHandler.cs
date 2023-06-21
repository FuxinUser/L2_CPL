using DBService.Repository.PDI;
using DBService.Repository.PDO;
using System.Windows.Forms;

namespace CPL1HMI
{
    public class Frm_2_2_ColumnsHandler
    {

        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly Frm_2_2_ColumnsHandler INSTANCE = new Frm_2_2_ColumnsHandler();
        }

        public static Frm_2_2_ColumnsHandler Instance { get { return SingletonHolder.INSTANCE; } }


        /// <summary>
        /// 歷史生產設定清單
        /// </summary>
        /// <param name="dgv"></param>
        public void Frm_2_2_HistoryProduction_Setup(DataGridView dgv)
        {
            //todo 栏位待厘清
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("钢卷号码", "Coil_ID", 200));   
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("原PDI的出口钢卷号", "OriPDI_Out_Coil_ID", 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("内部钢种", "SteelGrade", 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("厚度(mm)", "Thickness", 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽度(mm)", "Width", 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("抗拉值(N/mm²)", "EntryYieldStress", 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("密度(工艺卡)", "Density", 160));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长度(m)", "CoilLength", 100));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("重量(kg)", "CoilWeight", 100));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("工序代码", "ProcessCode", 120));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("钢卷内径(mm)", "InnerDiam", 160));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("钢卷外径(mm)", "Diameter", 160));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口套筒类型", "SleeveCodeEntry", 160));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口套筒内径(mm)", "SleeveDmEntry", 180));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口垫纸方式", "PaperWinderFlag", 180));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口套筒类型", "SleeveCodeExit", 160));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口套筒内径(mm)", "SleeveDmExit", 180));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口垫纸方式", "PaperTypeExit", 160));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口垫纸种类", "PaperCodeExit", 160));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("下压量1(mm)", "FlatenerDepth1", 160));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("下压量2(mm)", "FlatenerDepth2", 160));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("开卷机张力计算值(KN)", "UncoilerTension", 250));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("开卷机张力计算最大值(KN)", "UncoilerTensionMax", 250));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("开卷机张力计算最小值(KN)", "UncoilerTensionMin", 250));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("头段导带长度(m)", "HeadLeaderStripLength", 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("头段导带厚度(mm)", "HeadLeaderStripThickness", 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("头段导带宽度(mm)", "HeadLeaderStripWidth", 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("头段导带钢种", "HeadLeaderStripSteelGrade", 160));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("尾段导带长度(m)", "TailLeaderStripLength", 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("尾段导带厚度(mm)", "TailLeaderStripThickness", 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("尾段导带宽度(mm)", "TailLeaderStripWidth", 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("尾段导带钢种", "TailLeaderStripSteelGrade", 160));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("刀具间隙(mm)", "SideTrimmerGap", 160));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("刀具重迭量(mm)", "SideTrimmerLap", 160));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("刀具宽度(mm)", "SideTrimmerWidth", 160));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("三辊张力辊下压量(mm)", "TensionUnitDepth", 250));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("收卷机张力计算值(KN)", "RecoilerTension", 250));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("收卷机张力计算最大值(KN)", "RecoilerTensionMax", 250));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("收卷机张力计算最小值(KN)", "RecoilerTensionMin", 250));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("垫纸开卷方式", "PaperUnwinderFlag", 160));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("分卷标记", "CoilSplit", 120));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订货重量1(kg)", "Orderwt_1", 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订货重量2(kg)", "Orderwt_2", 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订货重量3(kg)", "Orderwt_3", 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订货重量4(kg)", "Orderwt_4", 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订货重量5(kg)", "Orderwt_5", 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订货重量6(kg)", "Orderwt_6", 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("下抛参数的钢卷位置", "PrrPosId", 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("下抛参数时间", "PresetTime", 200));

            #region Old
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("生产结束时间", nameof(PDOModel.TBL_PDO.FinishTime), 300));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口厚度", nameof(CoilPDIModel.TBL_PDI.Entry_Coil_Thick), 150));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口宽度", nameof(CoilPDIModel.TBL_PDI.Entry_Coil_Width), 150));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口重量", nameof(CoilPDIModel.TBL_PDI.Entry_Coil_Weight), 150));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口长度", nameof(CoilPDIModel.TBL_PDI.Entry_Coil_Length), 150));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口内径", nameof(CoilPDIModel.TBL_PDI.Entry_Coil_Inner), 150));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口外径", nameof(CoilPDIModel.TBL_PDI.Entry_Coil_Dcos), 150));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("抗拉最大值", nameof(CoilPDIModel.TBL_PDI.Ts_Stand_Max), 200));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("抗拉最小值", nameof(CoilPDIModel.TBL_PDI.Ts_Stand_Min), 200));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("内部钢种", nameof(CoilPDIModel.TBL_PDI.St_No), 150));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("密度", nameof(CoilPDIModel.TBL_PDI.Density), 150));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("订单表面精度代码", nameof(CoilPDIModel.TBL_PDI.Surface_Finishing_Code), 220));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("实际表面精度代码", nameof(CoilPDIModel.TBL_PDI.Surface_Accuracy), 220));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口钢卷编号", nameof(CoilPDIModel.TBL_PDI.Out_Coil_ID), 300));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("目标出口宽度", nameof(CoilPDIModel.TBL_PDI.Out_Coil_Width), 200));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽度最大值", nameof(CoilPDIModel.TBL_PDI.Out_Coil_Width_Max), 150));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽度最小值", nameof(CoilPDIModel.TBL_PDI.Out_Coil_Width_Min), 150));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("目标出口厚度", nameof(CoilPDIModel.TBL_PDI.Out_Coil_Thickness), 200));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口钢卷内径", nameof(CoilPDIModel.TBL_PDI.Out_Coil_Inner), 200));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("订货单重", nameof(CoilPDIModel.TBL_PDI.Order_Wt), 150));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("订货单重上限", nameof(CoilPDIModel.TBL_PDI.Order_Wt_Max), 200));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("订货单重下限", nameof(CoilPDIModel.TBL_PDI.Order_Wt_Min), 200));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("分卷数", nameof(CoilPDIModel.TBL_PDI.Dividing_Num), 150));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订货重量1", nameof(CoilPDIModel.TBL_PDI.Orderwt_1), 210));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订货重量2", nameof(CoilPDIModel.TBL_PDI.Orderwt_2), 210));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订货重量3", nameof(CoilPDIModel.TBL_PDI.Orderwt_3), 210));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订货重量4", nameof(CoilPDIModel.TBL_PDI.Orderwt_4), 210));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订货重量5", nameof(CoilPDIModel.TBL_PDI.Orderwt_5), 210));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订货重量6", nameof(CoilPDIModel.TBL_PDI.Orderwt_6), 210));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同号1", nameof(CoilPDIModel.TBL_PDI.Order_No_1), 300));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同号2", nameof(CoilPDIModel.TBL_PDI.Order_No_2), 300));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同号3", nameof(CoilPDIModel.TBL_PDI.Order_No_3), 300));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同号4", nameof(CoilPDIModel.TBL_PDI.Order_No_4), 300));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同号5", nameof(CoilPDIModel.TBL_PDI.Order_No_5), 300));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同号6", nameof(CoilPDIModel.TBL_PDI.Order_No_6), 300));
            #endregion

            dgv.Columns[0].Frozen = true;
        }

    }
}
