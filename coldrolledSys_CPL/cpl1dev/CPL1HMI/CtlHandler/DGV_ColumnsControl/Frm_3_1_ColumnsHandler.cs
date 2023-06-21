using DBService.Repository.PDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPL1HMI
{
    public class Frm_3_1_ColumnsHandler
    {
        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly Frm_3_1_ColumnsHandler INSTANCE = new Frm_3_1_ColumnsHandler();
        }

        public static Frm_3_1_ColumnsHandler Instance { get { return SingletonHolder.INSTANCE; } }



        public void Frm_3_1PDOColumns(DataGridView dgv)
        {
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("上传状态", nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("合同号", nameof(PDOEntity.TBL_PDO.OrderNo), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("计划号", nameof(PDOEntity.TBL_PDO.Plan_No), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口钢卷号", nameof(PDOEntity.TBL_PDO.Out_Coil_ID), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口钢卷号", nameof(PDOEntity.TBL_PDO.In_Coil_ID), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("生产开始时间", nameof(PDOEntity.TBL_PDO.StartTime), 250));      
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("生产结束时间", nameof(PDOEntity.TBL_PDO.FinishTime), 250));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("班次", nameof(PDOEntity.TBL_PDO.Shift), 100));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("班别", nameof(PDOEntity.TBL_PDO.Team), 100));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口卷外径(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Outer_Diameter)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口卷内径(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Inner)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口净重(kg)", nameof(PDOEntity.TBL_PDO.Out_Coil_Wt)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口毛重(kg)", nameof(PDOEntity.TBL_PDO.Out_Coil_Gross_WT)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口厚度(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Thick)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口宽度(mm)", nameof(PDOEntity.TBL_PDO.Out_Coil_Width)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口长度(m)", nameof(PDOEntity.TBL_PDO.Out_Coil_Length)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口垫纸种类", nameof(PDOEntity.TBL_PDO.Paper_Code)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口垫纸方式", nameof(PDOEntity.TBL_PDO.Paper_Req_Code)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口头部垫纸长(m)", nameof(PDOEntity.TBL_PDO.Out_Head_Paper_Length), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口头部垫纸宽(mm)", nameof(PDOEntity.TBL_PDO.Out_Head_Paper_Width), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口尾部垫纸长(m)", nameof(PDOEntity.TBL_PDO.Out_Tail_Paper_Length), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口尾部垫纸宽(mm)", nameof(PDOEntity.TBL_PDO.Out_Tail_Paper_Width), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口套筒内径(mm)", nameof(PDOEntity.TBL_PDO.Sleeve_Inner_Exit_Diamter)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口套筒类型", nameof(PDOEntity.TBL_PDO.Sleeve_Type_Exit_Code)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("头部打孔位置", nameof(PDOEntity.TBL_PDO.Head_Hole_Position)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("头部导带长度(m)", nameof(PDOEntity.TBL_PDO.Head_Leader_Length)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("头部导带宽度(mm)", nameof(PDOEntity.TBL_PDO.Head_Leader_Width)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("头部导带厚度(mm)", nameof(PDOEntity.TBL_PDO.Head_Leader_Thickness)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("尾部打孔位置", nameof(PDOEntity.TBL_PDO.Tail_PunchHole_Position)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("尾部导带长度(m)", nameof(PDOEntity.TBL_PDO.Tail_Leader_Length)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("尾部导带宽度(mm)", nameof(PDOEntity.TBL_PDO.Tail_Leader_Width)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("尾部导带厚度(mm)", nameof(PDOEntity.TBL_PDO.Tail_Leader_Thickness)));

            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口剪裁切钢卷头部长度(m)", nameof(PDOEntity.TBL_PDO.Header_Cut_Length_Entry)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口剪裁切钢卷尾部长度(m)", nameof(PDOEntity.TBL_PDO.Tail_Cut_Length_Entry)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口剪裁切钢卷头部长度(m)", nameof(PDOEntity.TBL_PDO.Header_Cut_Length_Exit)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("出口剪裁切钢卷尾部长度(m)", nameof(PDOEntity.TBL_PDO.Tail_Cut_Length_Exit)));

            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("头部切废长度(m)", nameof(PDOEntity.TBL_PDO.Scraped_Length_Entry)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("尾不切废长度(m)", nameof(PDOEntity.TBL_PDO.Scraped_Length_Exit)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("头部导带钢种", nameof(PDOEntity.TBL_PDO.Head_Leader_St_No), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("尾部导带钢种", nameof(PDOEntity.TBL_PDO.Tail_Leader_St_No), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("卷曲方向", nameof(PDOEntity.TBL_PDO.Winding_Direction)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("好面朝向",  nameof(PDOEntity.TBL_PDO.Base_Surface)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("封锁责任者", nameof(PDOEntity.TBL_PDO.Inspector)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("封锁标记", nameof(PDOEntity.TBL_PDO.Hold_Flag), 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("封锁原因代码", nameof(PDOEntity.TBL_PDO.Hold_Cause_Code), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("取样标记", nameof(PDOEntity.TBL_PDO.Sample_Flag), 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("切边标记", nameof(PDOEntity.TBL_PDO.Trim_Flag), 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("分卷标记", nameof(PDOEntity.TBL_PDO.Fixed_WT_Flag) , 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("最终卷标记", nameof(PDOEntity.TBL_PDO.End_Flag), 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("废品标记", nameof(PDOEntity.TBL_PDO.Scrap_Flag), 150));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("取样位置", nameof(PDOEntity.TBL_PDO.Sample_Frqn_Code)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("未焊导带代码", nameof(PDOEntity.TBL_PDO.No_Leader_Code)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("表面精度代码", nameof(PDOEntity.TBL_PDO.Surface_Accuracy_Code)));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("头部未轧制区域", nameof(PDOEntity.TBL_PDO.Head_Off_Gauge), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("尾部未轧制区域", nameof(PDOEntity.TBL_PDO.Tail_Off_Gauge), 200));
            dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("建立时间", nameof(PDOEntity.TBL_PDO.CreateTime), 200));

            //dgv.Columns[54].Visible = false;
            dgv.Columns[nameof(PDOEntity.TBL_PDO.StartTime)].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss.fff";
            dgv.Columns[nameof(PDOEntity.TBL_PDO.FinishTime)].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss.fff";

            dgv.Columns[0].Frozen = true;
            dgv.Columns[1].Frozen = true;
            dgv.Columns[2].Frozen = true;
            dgv.Columns[3].Frozen = true;
            dgv.ClearSelection();
        }
    }
}
