using DBService.Repository;
using DBService.Repository.PDI;
using System.Windows.Forms;

namespace CPL1HMI
{
    public class Frm_1_1_ColumnsHandler
    {

        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly Frm_1_1_ColumnsHandler INSTANCE = new Frm_1_1_ColumnsHandler();
        }

        public static Frm_1_1_ColumnsHandler Instance { get { return SingletonHolder.INSTANCE; } }


        /// <summary>
        /// 鋼捲排程
        /// </summary>
        /// <param name="dgv"></param>
        public void Frm_1_1_ScheduleColumns(DataGridView dgv)
        {
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("序号", "No", 80));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("顺序号", nameof(CoilScheduleModel.TBL_Production_Schedule.Seq_No), 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("钢卷编号", nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID), 300));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("计划号", nameof(CoilPDIEntity.TBL_PDI.Plan_No), 180));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷长度(m)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Length),220));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷宽度(mm)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Width), 220));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷厚度(mm)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Thick), 220));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷重量(kg)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Weight), 220));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷内径(mm)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Inner), 220));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷外径(mm)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Dcos), 220));

            //鎖定順序號及鋼捲號
            dgv.Columns[0].Frozen = true;
            dgv.Columns[1].Frozen = true;

        }


        /// <summary>
        /// 鋼捲PDI清單
        /// </summary>
        /// <param name="dgv"></param>
        public void Frm_1_1_PDI_ListColumns(DataGridView dgv)
        {
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("序号", "No", 80));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("钢卷编号", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("作业计划号", nameof(CoilPDIEntity.TBL_PDI.Plan_No), 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷厚度(mm)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Thick), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷宽度(mm)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Width), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷重量(kg)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Weight), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷长度(m)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Length), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷内径(mm)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Inner), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷外径(mm)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Dcos), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口套筒类型", nameof(CoilPDIEntity.TBL_PDI.Sleeve_Type_Code), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口套筒内径", nameof(CoilPDIEntity.TBL_PDI.Sleeve_diamter), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口垫纸方式", nameof(CoilPDIEntity.TBL_PDI.Paper_Req_Code), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口垫纸类型", nameof(CoilPDIEntity.TBL_PDI.Paper_Code), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口头部垫纸长度", nameof(CoilPDIEntity.TBL_PDI.Head_Paper_Length), 220));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口头部垫纸宽度(mm)", nameof(CoilPDIEntity.TBL_PDI.Head_Paper_Width), 300));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口尾部垫纸长度", nameof(CoilPDIEntity.TBL_PDI.Tail_Paper_Length), 220));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口尾部垫纸宽度(mm)", nameof(CoilPDIEntity.TBL_PDI.Tail_Paper_Width), 300));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("抗拉最大值", nameof(CoilPDIEntity.TBL_PDI.Ts_Stand_Max), 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("抗拉最小值", nameof(CoilPDIEntity.TBL_PDI.Ts_Stand_Min), 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("内部钢种", nameof(CoilPDIEntity.TBL_PDI.St_No), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("密度(kg/m³)", nameof(CoilPDIEntity.TBL_PDI.Density), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("返修类型", nameof(CoilPDIEntity.TBL_PDI.REPAIR_TYPE), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("订单表面精度代码", nameof(CoilPDIEntity.TBL_PDI.Surface_Finishing_Code), 250));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("实际表面精度代码", nameof(CoilPDIEntity.TBL_PDI.Surface_Accuracy), 250));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("实际好面朝向", nameof(CoilPDIEntity.TBL_PDI.Base_Surface), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("开卷方向", nameof(CoilPDIEntity.TBL_PDI.Uncoiler_Direction), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口钢卷编号", nameof(CoilPDIEntity.TBL_PDI.Out_Coil_ID), 300));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口垫纸方式", nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Req_Code), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口垫纸种类", nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Code), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口绑带方式", nameof(CoilPDIEntity.TBL_PDI.Out_Strap_Num), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("导带使用方式", nameof(CoilPDIEntity.TBL_PDI.Leader_Flag), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("取样要求", nameof(CoilPDIEntity.TBL_PDI.Sample_Flag), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("取样位置", nameof(CoilPDIEntity.TBL_PDI.Sample_Frqn_Code), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("试批号", nameof(CoilPDIEntity.TBL_PDI.Sample_Lot_No), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("钢卷来源", nameof(CoilPDIEntity.TBL_PDI.Coil_Origin), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("上游工序", nameof(CoilPDIEntity.TBL_PDI.Wholebacklog_Code), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("下游工序", nameof(CoilPDIEntity.TBL_PDI.Next_Wholebacklog_Code), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("切边要求", nameof(CoilPDIEntity.TBL_PDI.Trim_Flag), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("目标宽度(mm)", nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width), 170));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("目标宽度最大值(mm)", nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Max), 250));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("目标宽度最小值(mm)", nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Min), 250));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("目标厚度(mm)", nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Thickness), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口钢卷内径(mm)", nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Inner), 250));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同号", nameof(CoilPDIEntity.TBL_PDI.Order_No), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("订货单重上限(kg)", nameof(CoilPDIEntity.TBL_PDI.Order_Wt_Max), 250));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("订货单重下限(kg)", nameof(CoilPDIEntity.TBL_PDI.Order_Wt_Min), 250));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("目标订货单重(kg)", nameof(CoilPDIEntity.TBL_PDI.Order_Wt), 250));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("分卷标记", nameof(CoilPDIEntity.TBL_PDI.Dividing_Flag), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("分卷数", nameof(CoilPDIEntity.TBL_PDI.Dividing_Num), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订货重量1", nameof(CoilPDIEntity.TBL_PDI.Orderwt_1), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订货重量2", nameof(CoilPDIEntity.TBL_PDI.Orderwt_2), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订货重量3", nameof(CoilPDIEntity.TBL_PDI.Orderwt_3), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订货重量4", nameof(CoilPDIEntity.TBL_PDI.Orderwt_4), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订货重量5", nameof(CoilPDIEntity.TBL_PDI.Orderwt_5), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订货重量6", nameof(CoilPDIEntity.TBL_PDI.Orderwt_6), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同号1", nameof(CoilPDIEntity.TBL_PDI.Order_No_1), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同号2", nameof(CoilPDIEntity.TBL_PDI.Order_No_2), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同号3", nameof(CoilPDIEntity.TBL_PDI.Order_No_3), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同号4", nameof(CoilPDIEntity.TBL_PDI.Order_No_4), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同号5", nameof(CoilPDIEntity.TBL_PDI.Order_No_5), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同号6", nameof(CoilPDIEntity.TBL_PDI.Order_No_6), 200));

            dgv.Columns[0].Frozen = true;
        }


        /// <summary>
        /// 套筒資訊
        /// </summary>
        /// <param name="dgv"></param>
        public void Frm_SleevePaer_Sleeve(DataGridView dgv)
        {
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("套筒类型", nameof(CoilPDIEntity.TBL_PDI.Out_Sleeve_Type_Code), 120));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("数量", "Count", 120));
        }


        /// <summary>
        /// 墊紙資訊
        /// </summary>
        /// <param name="dgv"></param>
        public void Frm_SleevePaer_Paper(DataGridView dgv)
        {
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("垫纸类型", nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Code), 120));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("数量", "Count", 120));
        }

    }
}
