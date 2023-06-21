using DBService.Repository;
using DBService.Repository.Leader;
using DBService.Repository.PDI;
using System.Windows.Forms;

namespace CPL1HMI
{
    public class Frm_2_1_ColumnsHandler
    {

        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly Frm_2_1_ColumnsHandler INSTANCE = new Frm_2_1_ColumnsHandler();
        }

        public static Frm_2_1_ColumnsHandler Instance { get { return SingletonHolder.INSTANCE; } }


        /// <summary>
        /// Tracking Map Top 10 排程鋼捲清單
        /// </summary>
        /// <param name="dgv"></param>
        public void Frm_2_1_Schedule(DataGridView dgv)
        {
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("序号", "No", 80));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("钢卷编号", nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID), 300));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("工序代码", nameof(CoilPDIEntity.TBL_PDI.Process_Code), 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷厚度(mm)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Thick)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷宽度(mm)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Width)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷重量(kg)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Weight)));
           
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷长度(m)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Length)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷内径(mm)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Inner)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷外径(mm)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Dcos)));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口垫纸方式", nameof(CoilPDIModel.TBL_PDI.Paper_Req_Code)));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口垫纸类型", nameof(CoilPDIModel.TBL_PDI.Paper_Code),150));

            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("内部钢种", nameof(CoilPDIModel.TBL_PDI.St_No), 200));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("表面精度代码", nameof(CoilPDIModel.TBL_PDI.Surface_Finishing_Code)));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("好面朝向", nameof(CoilPDIModel.TBL_PDI.Base_Surface)));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口钢卷编号", nameof(CoilPDIModel.TBL_PDI.Out_Coil_ID), 200));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口垫纸方式", nameof(CoilPDIModel.TBL_PDI.Out_Paper_Code)));

            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("绑带方式", nameof(CoilPDIModel.TBL_PDI.Out_Strap_Num),100));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("取样要求", nameof(CoilPDIModel.TBL_PDI.Sample_Flag)));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("取样位置", nameof(CoilPDIModel.TBL_PDI.Sample_Frqn_Code)));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("试批号", nameof(CoilPDIModel.TBL_PDI.Sample_Lot_No), 200));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("钢卷来源", nameof(CoilPDIModel.TBL_PDI.Coil_Origin),100));

            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("上游工序", nameof(CoilPDIModel.TBL_PDI.Wholebacklog_Code)));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("下游工序", nameof(CoilPDIModel.TBL_PDI.Next_Wholebacklog_Code)));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("切边要求", nameof(CoilPDIModel.TBL_PDI.Trim_Flag)));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("目标宽度(mm)", nameof(CoilPDIModel.TBL_PDI.Out_Coil_Width)));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("目标宽度最大值(mm)", nameof(CoilPDIModel.TBL_PDI.Out_Coil_Width_Max)));

            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("目标宽度最小值(mm)", nameof(CoilPDIModel.TBL_PDI.Out_Coil_Width_Min)));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口钢卷内径(mm)", nameof(CoilPDIModel.TBL_PDI.Out_Coil_Inner)));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("订货单重上限(kg)", nameof(CoilPDIModel.TBL_PDI.Order_Wt_Max)));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("订货单重下限(kg)", nameof(CoilPDIModel.TBL_PDI.Order_Wt_Min)));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("目标订货单重(kg)", nameof(CoilPDIModel.TBL_PDI.Order_Wt)));

            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("测试计划号", nameof(CoilPDIModel.TBL_PDI.Test_Plan_No), 200));
            //dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("建立时间", nameof(CoilPDIModel.TBL_PDI.CreateTime), 200));

            //dgv.Columns[29].Visible = false;
            //dgv.Columns[30].Visible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //鎖定順序號及鋼捲號
            dgv.Columns[0].Frozen = true;
            dgv.Columns[1].Frozen = true;
        }


        /// <summary>
        /// Tracking Map 鋼捲資訊
        /// </summary>
        /// <param name="dgv"></param>
        public void Frm_2_1_TrackingMap(DataGridView dgv)
        {
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷编号", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("工序代码", nameof(CoilPDIEntity.TBL_PDI.Process_Code), 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷厚度(mm)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Thick)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷宽度(mm)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Width)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷重量(kg)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Weight)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷长度(m)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Length)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷内径(mm)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Inner)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷外径(mm)", nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Dcos)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口套筒类型", nameof(CoilPDIEntity.TBL_PDI.Sleeve_Type_Code)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口套筒内径(mm)", nameof(CoilPDIEntity.TBL_PDI.Sleeve_diamter)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口垫纸方式", nameof(CoilPDIEntity.TBL_PDI.Paper_Req_Code)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口垫纸类型", nameof(CoilPDIEntity.TBL_PDI.Paper_Code),150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("头部垫纸长度(m)", nameof(CoilPDIEntity.TBL_PDI.Head_Paper_Length)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("头部垫纸宽度(mm)", nameof(CoilPDIEntity.TBL_PDI.Head_Paper_Width)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("尾部垫纸长度(m)", nameof(CoilPDIEntity.TBL_PDI.Tail_Paper_Length)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("尾部垫纸宽度(mm)", nameof(CoilPDIEntity.TBL_PDI.Tail_Paper_Width)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("抗拉最大值", nameof(CoilPDIEntity.TBL_PDI.Ts_Stand_Max)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("抗拉最小直", nameof(CoilPDIEntity.TBL_PDI.Ts_Stand_Min)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("内部钢种", nameof(CoilPDIEntity.TBL_PDI.St_No), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("密度(kg/m³)", nameof(CoilPDIEntity.TBL_PDI.Density)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("返修类型", nameof(CoilPDIEntity.TBL_PDI.REPAIR_TYPE)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("订单表面精度代码", nameof(CoilPDIEntity.TBL_PDI.Surface_Accuracy), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("实际表面精度代码", nameof(CoilPDIEntity.TBL_PDI.Surface_Finishing_Code), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("好面朝向", nameof(CoilPDIEntity.TBL_PDI.Base_Surface)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("开卷方向", nameof(CoilPDIEntity.TBL_PDI.Uncoiler_Direction)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口钢卷编号", nameof(CoilPDIEntity.TBL_PDI.Out_Coil_ID), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口垫纸方式", nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Req_Code)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口垫纸种类", nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Code),150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口套筒内径(mm)", nameof(CoilPDIEntity.TBL_PDI.Out_Sleeve_Diamter)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口套筒类型", nameof(CoilPDIEntity.TBL_PDI.Out_Sleeve_Type_Code)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口绑带方式", nameof(CoilPDIEntity.TBL_PDI.Out_Strap_Num)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("导带选用方式", nameof(CoilPDIEntity.TBL_PDI.Leader_Flag),150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("取样要求", nameof(CoilPDIEntity.TBL_PDI.Sample_Flag)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("取样位置", nameof(CoilPDIEntity.TBL_PDI.Sample_Frqn_Code)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("试批号", nameof(CoilPDIEntity.TBL_PDI.Sample_Lot_No), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("钢卷来源", nameof(CoilPDIEntity.TBL_PDI.Coil_Origin),100));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("上游工序", nameof(CoilPDIEntity.TBL_PDI.Wholebacklog_Code)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("下游工序", nameof(CoilPDIEntity.TBL_PDI.Next_Wholebacklog_Code)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("切边要求", nameof(CoilPDIEntity.TBL_PDI.Trim_Flag)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("目标宽度(mm)", nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("目标宽度最大值(mm)", nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Max)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("目标宽度最小值(mm)", nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Min)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("目标出口厚度(mm)", nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Thickness)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口钢卷内径(mm)", nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Inner)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同号", nameof(CoilPDIEntity.TBL_PDI.Order_No), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("订货单重上限(kg)", nameof(CoilPDIEntity.TBL_PDI.Order_Wt_Max)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("订货单重下限(kg)", nameof(CoilPDIEntity.TBL_PDI.Order_Wt_Min)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("目标订货单重(kg)", nameof(CoilPDIEntity.TBL_PDI.Order_Wt)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("分卷标记", nameof(CoilPDIEntity.TBL_PDI.Dividing_Flag),100));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("分卷数", nameof(CoilPDIEntity.TBL_PDI.Dividing_Num),100));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订单重量1(kg)", nameof(CoilPDIEntity.TBL_PDI.Orderwt_1)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订单重量2(kg)", nameof(CoilPDIEntity.TBL_PDI.Orderwt_2)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订单重量3(kg)", nameof(CoilPDIEntity.TBL_PDI.Orderwt_3)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订单重量4(kg)", nameof(CoilPDIEntity.TBL_PDI.Orderwt_4)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订单重量5(kg)", nameof(CoilPDIEntity.TBL_PDI.Orderwt_5)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同订单重量6(kg)", nameof(CoilPDIEntity.TBL_PDI.Orderwt_6)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同号1", nameof(CoilPDIEntity.TBL_PDI.Order_No_1),100));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同号2", nameof(CoilPDIEntity.TBL_PDI.Order_No_2), 100));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同号3", nameof(CoilPDIEntity.TBL_PDI.Order_No_3), 100));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同号4", nameof(CoilPDIEntity.TBL_PDI.Order_No_4), 100));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同号5", nameof(CoilPDIEntity.TBL_PDI.Order_No_5), 100));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同号6", nameof(CoilPDIEntity.TBL_PDI.Order_No_6), 100));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("测试计划号", nameof(CoilPDIEntity.TBL_PDI.Test_Plan_No), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("头段导带钢种", nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_St_No),150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("头段导带长度(m)", nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Length)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("头段导带宽度(mm)", nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Width)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("头段导带厚度(mm)", nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Thickness)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("尾段导带钢种", nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_St_No),150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("尾段导带长度(m)", nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Length)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("尾段导带宽度(mm)", nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Width)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("尾段导带厚度(mm)", nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Thickness)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("QC备注", nameof(CoilPDIEntity.TBL_PDI.Qc_Remark), 1000));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("头部未轧制区域", nameof(CoilPDIEntity.TBL_PDI.Head_Off_Gauge)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("尾部未轧制区域", nameof(CoilPDIEntity.TBL_PDI.Tail_Off_Gauge)));

            dgv.Columns[0].Frozen = true;
        }
    }
}
