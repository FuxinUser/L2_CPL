using System.Windows.Forms;
using static CPL1HMI.DataBaseTableFactory;

namespace CPL1HMI
{
    public class Frm_5_5_ColumnsHandler
    {
        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly Frm_5_5_ColumnsHandler INSTANCE = new Frm_5_5_ColumnsHandler();
        }

        public static Frm_5_5_ColumnsHandler Instance { get { return SingletonHolder.INSTANCE; } }


        /// <summary>
        /// 中英文對照
        /// </summary>
        /// <param name="Dgv"></param>
        public void Frm_5_5_LanguageTitle(DataGridView Dgv)
        {
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("中文标题", nameof(TBL_LangSwitch_Nav.ZH), 600));
            Dgv.Columns.Add(DGVColumnsHandler.Instance.Fun_SetGridViewColumn("英文标题", nameof(TBL_LangSwitch_Nav.EN), 600));
        }

    }
}
