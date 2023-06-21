using DBService.Repository.DelayLocation;
using DBService.Repository.DelayReasonCode;
using DBService.Repository.MaterialGrade;
using DBService.Repository.ScheduleDelete_CoilReject_Code;
using System.Windows.Forms;

namespace CPL1HMI
{
    public class Frm_5_2_ColumnsHandler
    {
        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly Frm_5_2_ColumnsHandler INSTANCE = new Frm_5_2_ColumnsHandler();
        }

        public static Frm_5_2_ColumnsHandler Instance { get { return SingletonHolder.INSTANCE; } }


        /// <summary>
        /// 排程刪除、鋼捲回退代碼
        /// </summary>
        /// <param name="Dgv"></param>
        public void Frm_5_2_ScheduleDelete_CoilReject_Code(DataGridView Dgv)
        {
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("代码编号", nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_GroupNo), 150,4));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("代码", nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Code), 150,2));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("原因", nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Name), 1000,50));
            //Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("建立者", nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition.Create_UserID), 200));
            //Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("更新时间", nameof(ScheduleDelete_CoilReject_CodeModel.TBL_ScheduleDelete_CoilReject_CodeDefinition.CreateTime), 300));
            Dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }


        /// <summary>
        /// 停復機位置代碼
        /// </summary>
        /// <param name="Dgv"></param>
        public void Frm_5_2_DelayLocation(DataGridView Dgv)
        {
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("顺序号", nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Index_No), 150));//int
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("位置代码", nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationCode), 150,6));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("位置", nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationName), 1000,50));
            //Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("建立者", nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Create_UserID), 200));
            //Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("更新时间", nameof(DelayLocationModel.TBL_DelayLocation_Definition.CreateTime), 300));
            Dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }


        /// <summary>
        /// 停機原因代碼
        /// </summary>
        /// <param name="Dgv"></param>
        public void Frm_5_2_DelayReasonCode(DataGridView Dgv)
        {
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("顺序号", nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Index_No), 150));//int
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("原因代码", nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonCode), 150, 2));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("原因名称", nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonName), 300, 50));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("群组代码", nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupCode), 150,1));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("群组名称", nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupName), 500,50));
          
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("负责部门", nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Responsible_Department), 500,50));
            //Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("建立者", nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Create_UserID), 200));
            //Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("更新时间", nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.CreateTime), 300));
            Dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }


        /// <summary>
        /// 鋼種
        /// </summary>
        /// <param name="Dgv"></param>
        public void Frm_5_2_MaterialGrade(DataGridView Dgv)
        {
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("钢种", nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.St_No), 200,8));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("群组名称", nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Material_Grade), 600,20));
            //Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("更新时间", nameof(MaterialGradeModel.TBL_SteelNoToMaterialGrade.CreateTime), 300));
            Dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }


       
    }
}
