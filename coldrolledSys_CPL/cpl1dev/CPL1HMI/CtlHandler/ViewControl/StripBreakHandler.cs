using System;
using System.Data;
using Akka.Actor;
using Controller.Coil;
using DataModel.HMIServerCom.Msg;
using DBService.Repository.PDI;
using DBService.Repository.UnmountRecord;

namespace CPL1HMI
{
    public class StripBreakHandler
    {

        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly StripBreakHandler INSTANCE = new StripBreakHandler();

        }

        public static StripBreakHandler Instance { get { return SingletonHolder.INSTANCE; } }


        /// <summary>
        /// 取得POR上的鋼捲PDI
        /// </summary>
        /// <param name="POR_Coil_ID"></param>
        public void Fun_StripBreak(string POR_Coil_ID)
        {
            // 取得出口捲號、長度、寬、厚、理論重、密度
            string strSql = Frm_2_1_SqlFactory.SQL_Select_StripBreakPDI(POR_Coil_ID);

            DataTable dtGetStripBreak_PDI = DataAccess.Fun_SelectDate(strSql, "取得断带钢卷PDI");

            if (dtGetStripBreak_PDI.IsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("无POR断带钢卷PDI","断带",0);

                return;
            }


            string OriPDI_Out_Coil_ID = dtGetStripBreak_PDI.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_ID)].ToString();

            if (Fun_StripBreak_Checked(OriPDI_Out_Coil_ID))
            {
                DialogHandler.Instance.Fun_DialogShowOk("无断带记录", "断带", 0);

                return;
            }

            PublicComm.ClientLog.Info($"[{POR_Coil_ID}]有斷帶記錄");

            // 有断带则给定新卷号
            // 使用出口捲號去編
            ICoilController _coilController = new CoilController();

            string New_Coil_ID = _coilController.GenSplitChildrenCoilID(OriPDI_Out_Coil_ID);


            Fun_Calculate(dtGetStripBreak_PDI, New_Coil_ID);

        }


        /// <summary>
        /// 检查断带记录
        /// </summary>
        /// <param name="OriPDI_Out_Coil_ID"></param>
        /// <returns></returns>
        private static bool Fun_StripBreak_Checked(string OriPDI_Out_Coil_ID)
        {
            string strSql = Frm_DialogReject_SqlFactory.SQL_Select_BrakeStripRecordCheck(OriPDI_Out_Coil_ID);
            DataTable dt = DataAccess.Fun_SelectDate(strSql, "断带记录");

            if (dt.IsNull())
                return false;
            else
                return true;

        }


        /// <summary>
        /// 計算POR捲長及理論重
        /// </summary>
        /// <param name="dt"></param>
        private void Fun_Calculate(DataTable dt,string New_Coil_ID)
        {

            // 取得TBL_UnmountRecord取得TR鋼捲長度、重量
            string strSql = Frm_2_1_SqlFactory.SQL_Select_UnmountRecord(dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_ID)].ToString());

            DataTable dtGetUnmount = DataAccess.Fun_SelectDate(strSql, "UnmountRecord");


            // POR捲長 = 母捲PDI捲長 - TR子捲長
            int Length = Convert.ToInt32(dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Length)].ToString()) - Convert.ToInt32(dtGetUnmount.Rows[0][nameof(UnmountRecordEntity.TBL_UnmountRecord.CoilLength)].ToString());

            // POR捲重 = 母捲PDI捲重 - TR子捲重
            int Weight = Convert.ToInt32(dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Length)].ToString()) - Convert.ToInt32(dtGetUnmount.Rows[0][nameof(UnmountRecordEntity.TBL_UnmountRecord.CoilLength)].ToString());


            // 儲存進TBL_ScheduleDelete_CoilReject_Temp
            strSql = Frm_2_1_SqlFactory.SQL_Insert_StripBreakCoil_CoilRejectTemp(dt, New_Coil_ID,Length, Weight);

            if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, "POR断带钢卷资料新增"))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"钢卷号[{ New_Coil_ID.Trim()}]PDI新增失敗", "POR断带钢卷资料新增", 3);

                return;
            }

            // 通知Server對L1發送Preset 201
            Fun_AkkaTellServer(New_Coil_ID);

        }


        /// <summary>
        /// 通知Server對L1發送Preset 201
        /// </summary>
        /// <param name="New_Coil_ID"></param>
        private void Fun_AkkaTellServer(string New_Coil_ID)
        {
            //給子捲號讓Server至TBL_ScheduleDelete_CoilReject_Temp撈取資料
            SCCommMsg.CS19_POR_StripBreak _StripBreak = new SCCommMsg.CS19_POR_StripBreak
            {
                Coil_ID = New_Coil_ID
            };

            PublicComm.Client.Tell(_StripBreak);

            DialogHandler.Instance.Fun_DialogShowOk($"已通知Server发送Preset给L1", "断带", 4);

            PublicComm.ClientLog.Info($"已通知Server发送钢卷号:[{New_Coil_ID.Trim()}]Preset给L1");

            EventLogHandler.Instance.LogInfo("2-1", "断带", $"通知Server发送钢卷号:[{New_Coil_ID.Trim()}]Preset给L1");
        }
    }
}
