using Controller.Coil;
using Core.Define;
using LogSender;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Define.DBParaDef;
using static DataMod.Response.RespnseModel;
using static MsgStruct.L1L2Rcv;
using static MsgStruct.MMSL2Rcv;


namespace CPLUnitTest.ControllerUnitTest
{
    class ScheduleAPI
    {
        ICoilController coilController = new CoilController();
        ILog log = new Log(string.Empty, string.Empty, null, false, true);


        // 批次插入鋼捲排程測試
        [Test]
        public void BatchInsertSchedule()
        {
            coilController.SetLog(log);

            // 新增排程測試資料
            var coils = "TE10000000          ,TE20000000          ";
            var insertNum = 2;

            // 批次插入測試
            var proOK = coilController.BatchInsertSchedule(coils, insertNum);
            Assert.IsTrue(proOK);

            // 刪除測試資料
            var deleteOK = coilController.DeleteAllIdleSchedule();
            Assert.IsTrue(deleteOK);

        }


        // 刪除排程鋼捲測試
        [Test]
        public void DeleteAllSchedule()
        {
            // 刪除所有鋼捲排程
            coilController.SetLog(log);
            var deleteOk = coilController.DeleteAllIdleSchedule();
            Assert.IsTrue(deleteOk);
        }

        // 更新鋼捲狀態
        [Test]
        public void UpdateScheduleStatuts()
        {

            coilController.SetLog(log);

            // 建立鋼捲排程測試資料
            var coils = "TE10000000          TE20000000          ";
            var insertNum = 2;

            var proOK = coilController.BatchInsertSchedule(coils, insertNum);
            Assert.IsTrue(proOK);

            // 測試更新鋼捲狀態
            var updteOK = coilController.UpdateScheduleStatuts("TE10000000", "R");
            Assert.IsTrue(updteOK);

            var schedule = coilController.GetScheduleInfo("TE10000000");
            Assert.IsTrue(schedule.Schedule_Status.Equals("R"));

            //刪除排程
            var delete10Ok = coilController.DeleteCoilScheduleByCoilID("TE10000000");
            Assert.IsTrue(delete10Ok);

            //刪除排程
            var delete20Ok = coilController.DeleteCoilScheduleByCoilID("TE20000000");
            Assert.IsTrue(delete20Ok);


        }


        // 根據計劃號撈取鋼捲排程
        [Test]
        public void GetCollScheduleByPlanNo()
        {
            coilController.SetLog(log);

            // 新增PDI
            var mockPdiData = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            mockPdiData.Entry_Coil_No = "TY00100010".ToByteData();
            mockPdiData.Plan_No = "PALTEST".ToByteData();
            var insertOK = coilController.CreatePDI(mockPdiData);
            Assert.IsTrue(insertOK);

            // 新增排程
            var coils = "TY00100010          ";
            var insertNum = 1;
            var proOK = coilController.BatchInsertSchedule(coils, insertNum);
            Assert.IsTrue(proOK);

            // 撈取
            var schedules = coilController.GetCollScheduleByPlanNo("PALTEST");
            Assert.IsTrue(schedules.Count() > 0);

            // 刪除PDI
            var deleteOK = coilController.DeletePDIByEntryCoilID(mockPdiData.PlanNo, "TY00100010");
            Assert.IsTrue(deleteOK);

            //刪除排程

            var deleteOk = coilController.DeleteCoilScheduleByCoilID("TY00100010");
            Assert.IsTrue(deleteOk);
        }

        // 存取鋼捲刪除紀錄測試
        [Test]
        public void SaveCoilScheduleDelRecords()
        {
            // 存取測試
            coilController.SetLog(log);
            var saveOK = coilController.CreateCoilScheduleDelRecords("TE201230010002");
            Assert.IsTrue(saveOK);

            // 刪除測試資料
            var delOK = coilController.DeleteDelScheduleRecord("TE201230010002");
            Assert.IsTrue(delOK);

        }

        // 存取暫存鋼捲刪除紀錄
        [Test]
        public void SaveTempCoilScheduleDelRecord()
        {
            coilController.SetLog(log);

            var mockPdiData = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            mockPdiData.Entry_Coil_No = "TE201230010002".ToByteData();
            var insertOK = coilController.CreatePDI(mockPdiData);
            Assert.IsTrue(insertOK);


            // 存取測試
            var saveOK = coilController.CreateCoilScheduleDelTempRecord("TE201230010002", "S", string.Empty, string.Empty, string.Empty);
            Assert.IsTrue(saveOK);

            // 刪除測試資料
            var delOK = coilController.DeleteSchDelCoilRejectTempRecord("TE201230010002");
            Assert.IsTrue(delOK);

            // 測試完畢 刪除測試資料
            var deleteOK = coilController.DeletePDIByEntryCoilID(mockPdiData.PlanNo, mockPdiData.EntryCoilNo);
            Assert.IsTrue(deleteOK);

        }

        // 刪除暫存鋼捲紀錄
        [Test]
        public void DelCoilScheduleDelTempRecord()
        {

            coilController.SetLog(log);

            var mockPdiData = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            mockPdiData.Entry_Coil_No = "HE201230010002".ToByteData();
            var insertOK = coilController.CreatePDI(mockPdiData);
            Assert.IsTrue(insertOK);

            // 存取測試資料
            var saveOK = coilController.CreateCoilScheduleDelTempRecord("HE201230010002", "S", string.Empty, string.Empty, string.Empty);
            Assert.IsTrue(saveOK);

            // 刪除測試
            var delOK = coilController.DeleteSchDelCoilRejectTempRecord("HE201230010002");
            Assert.IsTrue(delOK);

            // 測試完畢 刪除測試資料
            var deleteOK = coilController.DeletePDIByEntryCoilID(mockPdiData.PlanNo, mockPdiData.EntryCoilNo);
            Assert.IsTrue(deleteOK);
        }

        // 刪除單筆鋼捲排成測試
        [Test]
        public void DeleteCoilSchedule()
        {
            // 批次插入所有鋼捲排程
            coilController.SetLog(log);

            var coils = "HE10000000          HE20000000          ";
            var insertNum = 2;

            var proOK = coilController.BatchInsertSchedule(coils, insertNum);
            Assert.IsTrue(proOK);

            // 刪除測試
            var del10Ok = coilController.DeleteCoilScheduleByCoilID("HE10000000");
            Assert.IsTrue(del10Ok);

            // 刪除測試
            var del20Ok = coilController.DeleteCoilScheduleByCoilID("HE20000000");
            Assert.IsTrue(del20Ok);
        }

        // 撈除鋼捲刪除暫存記錄
        [Test]
        public void GetCoilScheduleDelTempRecord()
        {
            coilController.SetLog(log);

            var mockPdiData = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            mockPdiData.Entry_Coil_No = "HE10000000".ToByteData();
            var insertOK = coilController.CreatePDI(mockPdiData);
            Assert.IsTrue(insertOK);


            // 新增測試資料
            insertOK = coilController.CreateCoilScheduleDelTempRecord("HE10000000", "N", string.Empty);
            Assert.IsTrue(insertOK);
            // 撈取測試
            var response = coilController.GetCoilScheduleDelTempRecord("HE10000000");
            Assert.IsNotNull(response);
            // 刪除測試資料
            var deleteOK = coilController.DeleteSchDelCoilRejectTempRecord("HE10000000");
            Assert.IsTrue(deleteOK);

            // 測試完畢 刪除測試資料
            deleteOK = coilController.DeletePDIByEntryCoilID(mockPdiData.PlanNo, mockPdiData.EntryCoilNo);
            Assert.IsTrue(deleteOK);
        }

        [Test]
        public void GetTopCoilSchedule()
        {

            coilController.SetLog(log);

            var coils = "HE10000000          HE20000000          ";
            var insertNum = 2;

            var proOK = coilController.BatchInsertSchedule(coils, insertNum);
            Assert.IsTrue(proOK);

            var schedules = coilController.QueryTopCoilSchedule(2, CoilDef.NewCoil_Statuts);
            Assert.IsTrue(schedules.Count() == 2);


            var del10Ok = coilController.DeleteCoilScheduleByCoilID("HE10000000");
            Assert.IsTrue(del10Ok);

            var del20Ok = coilController.DeleteCoilScheduleByCoilID("HE20000000");
            Assert.IsTrue(del20Ok);

        }
    }
}
