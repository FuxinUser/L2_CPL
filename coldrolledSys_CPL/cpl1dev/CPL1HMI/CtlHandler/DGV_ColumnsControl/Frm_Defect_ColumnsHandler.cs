using DBService.Repository.DefectData;
using System.Windows.Forms;

namespace CPL1HMI
{
    public class Frm_Defect_ColumnsHandler
    {
        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly Frm_Defect_ColumnsHandler INSTANCE = new Frm_Defect_ColumnsHandler();
        }

        public static Frm_Defect_ColumnsHandler Instance { get { return SingletonHolder.INSTANCE; } }


        /// <summary>
        /// 缺陷
        /// </summary>
        /// <param name="Dgv"></param>
        public void Frm_Defect_Columns(DataGridView Dgv)
        {
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("钢卷号", nameof(CoilDefectDataEntity.TBL_Coil_Defect.Coil_ID), 200));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("入口卷号", nameof(CoilDefectDataEntity.TBL_Coil_Defect.Entry_Coil_ID), 200));

            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷代码(1)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Code), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷来源(1)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Origin), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("表面区分(1)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Sid), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽向位置(1)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_W), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向开始位置(1)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_L_Start), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向结束位置(1)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_L_End), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷程度(1)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Level), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷比例(1)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Percent), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("质量等级(1)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_QGRADE), 150));

            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷代码(2)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Code), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷来源(2)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Origin), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("表面区分(2)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Sid), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽向位置(2)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_W), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向开始位置(2)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_L_Start), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向结束位置(2)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_L_End), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷程度(2)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Level), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷比例(2)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Percent), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("质量等级(2)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_QGRADE), 150));

            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷代码(3)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Code), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷来源(3)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Origin), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("表面区分(3)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Sid), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽向位置(3)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_W), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向开始位置(3)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_L_Start), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向结束位置(3)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_L_End), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷程度(3)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Level), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷比例(3)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Percent), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("质量等级(3)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_QGRADE), 150));

            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷代码(4)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Code), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷来源(4)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Origin), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("表面区分(4)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Sid), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽向位置(4)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_W), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向开始位置(4)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_L_Start), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向结束位置(4)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_L_End), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷程度(4)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Level), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷比例(4)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Percent), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("质量等级(4)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_QGRADE), 150));

            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷代码(5)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Code), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷来源(5)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Origin), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("表面区分(5)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Sid), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽向位置(5)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_W), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向开始位置(5)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_L_Start), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向结束位置(5)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_L_End), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷程度(5)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Level), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷比例(5)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Percent), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("质量等级(5)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_QGRADE), 150));

            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷代码(6)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Code), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷来源(6)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Origin), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("表面区分(6)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Sid), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽向位置(6)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_W), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向开始位置(6)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_L_Start), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向结束位置(6)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_L_End), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷程度(6)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Level), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷比例(6)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Percent), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("质量等级(6)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_QGRADE), 150));

            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷代码(7)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Code), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷来源(7)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Origin), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("表面区分(7)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Sid), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽向位置(7)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_W), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向开始位置(7)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_L_Start), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向结束位置(7)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_L_End), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷程度(7)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Level), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷比例(7)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Percent), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("质量等级(7)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_QGRADE), 150));

            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷代码(8)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Code), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷来源(8)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Origin), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("表面区分(8)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Sid), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽向位置(8)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_W), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向开始位置(8)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_L_Start), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向结束位置(8)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_L_End), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷程度(8)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Level), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷比例(8)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Percent), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("质量等级(8)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_QGRADE), 150));

            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷代码(9)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Code), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷来源(9)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Origin), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("表面区分(9)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Sid), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽向位置(9)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_W), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向开始位置(9)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_L_Start), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向结束位置(9)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_L_End), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷程度(9)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Level), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷比例(9)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Percent), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("质量等级(9)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_QGRADE), 150));

            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷代码(10)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Code), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷来源(10)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Origin), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("表面区分(10)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Sid), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("宽向位置(10)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_W), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向开始位置(10)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_L_Start), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("长向结束位置(10)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_L_End), 170));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷程度(10)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Level), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("缺陷比例(10)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Percent), 150));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("质量等级(10)", nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_QGRADE), 150));

            Dgv.Columns[0].Frozen = true;

        }
    }
}
