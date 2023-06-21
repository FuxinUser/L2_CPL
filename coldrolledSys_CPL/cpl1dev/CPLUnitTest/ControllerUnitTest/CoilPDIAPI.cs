using Controller.Coil;
using LogSender;
using NUnit.Framework;
using System;
using static Core.Define.DBParaDef;
using static MsgStruct.MMSL2Rcv;

namespace CPLUnitTest.ControllerUnitTest
{
    class CoilPDIAPI
    {
        ICoilController coilController = new CoilController();
        ILog log = new Log(string.Empty, string.Empty, null, false, true);

        // 查詢是否有PDI測試
        [Test]
        public void HasPDI()
        {
            coilController.SetLog(log);
            // 新增 PDI 測試資料
            var mockPdiData = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            var insertOK = coilController.CreatePDI(mockPdiData);
            Assert.IsTrue(insertOK);

            // 查詢是否有 PDI
            var hasPDI = coilController.VaildHasPDI(mockPdiData.PlanNo, mockPdiData.EntryCoilNo);
            Assert.IsTrue(hasPDI);

            // 測試完畢 刪除測試資料
            var deleteOK = coilController.DeletePDIByEntryCoilID(mockPdiData.PlanNo, mockPdiData.EntryCoilNo);
            Assert.IsTrue(deleteOK);
        }

        // 索取PDI計畫號
        [Test]
        public void GetPDIPlanNo()
        {
            coilController.SetLog(log);
            // 新增 PDI 測試資料
            var mockPdiData = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            mockPdiData.Entry_Coil_No = "TYXXXXXX".ToByteData();
            mockPdiData.Plan_No = "PLAN000".ToByteData();
            var insertOK = coilController.CreatePDI(mockPdiData);
            Assert.IsTrue(insertOK);

            var mockPdiData2 = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            mockPdiData2.Entry_Coil_No = "TYXXXXXX".ToByteData();
            mockPdiData2.Plan_No = "PLAN001".ToByteData();
            var insertOK2 = coilController.CreatePDI(mockPdiData2);
            Assert.IsTrue(insertOK2);

            // 查詢是否有 PDI
            var planNo = coilController.GetPDIPlanNo("TYXXXXXX");
            Assert.IsTrue(planNo.Equals("PLAN001"));



            // 測試完畢 刪除測試資料
            var deleteOK = coilController.DeletePDIByEntryCoilID(mockPdiData.PlanNo, mockPdiData.EntryCoilNo);
            Assert.IsTrue(deleteOK);

            var deleteOK2 = coilController.DeletePDIByEntryCoilID(mockPdiData2.PlanNo, mockPdiData2.EntryCoilNo);
            Assert.IsTrue(deleteOK2);
        }


        // 更新掃描輸入ID
        [Test]
        public void UpdatePDIEntryScanCoilInfo()
        {
            coilController.SetLog(log);
            // 新增 PDI 測試資料
            var mockPdiData = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            mockPdiData.Entry_Coil_No = "OYXXXXXX".ToByteData();
            mockPdiData.Plan_No = "PLAN000".ToByteData();
            var insertOK = coilController.CreatePDI(mockPdiData);
            Assert.IsTrue(insertOK);

            var mockPdiData2 = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            mockPdiData2.Entry_Coil_No = "OYXXXXXX".ToByteData();
            mockPdiData2.Plan_No = "PLAN001".ToByteData();
            var insertOK2 = coilController.CreatePDI(mockPdiData2);
            Assert.IsTrue(insertOK2);

            // 查詢是否有 PDI
            var updateOK = coilController.UpdatePDIEntryScanCoilInfo("OYXXXXXX", true);
            Assert.IsTrue(updateOK);

            var pdi = coilController.GetPDI("OYXXXXXX", PDISchema.EntryCoilID);
            Assert.IsTrue(pdi.Entry_Coil_ID_Checked.Equals("1"));

            // 測試完畢 刪除測試資料
            var deleteOK = coilController.DeletePDIByEntryCoilID(mockPdiData.PlanNo, mockPdiData.EntryCoilNo);
            Assert.IsTrue(deleteOK);

            var deleteOK2 = coilController.DeletePDIByEntryCoilID(mockPdiData2.PlanNo, mockPdiData2.EntryCoilNo);
            Assert.IsTrue(deleteOK2);
        }


        // 更新PDI開始時間
        [Test]
        public void UpdatePDIStarTime()
        {
            coilController.SetLog(log);
            // 新增 PDI 測試資料
            var mockPdiData = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            mockPdiData.Entry_Coil_No = "GGGGGGG".ToByteData();
            mockPdiData.Plan_No = "PLAN000".ToByteData();
            var insertOK = coilController.CreatePDI(mockPdiData);
            Assert.IsTrue(insertOK);

            var mockPdiData2 = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            mockPdiData2.Entry_Coil_No = "GGGGGGG".ToByteData();
            mockPdiData2.Plan_No = "PLAN001".ToByteData();
            var insertOK2 = coilController.CreatePDI(mockPdiData2);
            Assert.IsTrue(insertOK2);


            var nowTime = DateTime.Now;
            var updateOK = coilController.UpdatePDIStarTime("GGGGGGG", nowTime);
            Assert.IsTrue(updateOK);

            var pdi = coilController.GetPDI("GGGGGGG", PDISchema.EntryCoilID);
            var pdiStarTime = pdi.Start_Time.ToString(DBDateTimeFromat);
            Assert.IsTrue(!pdiStarTime.Equals(NullTime));

            // 測試完畢 刪除測試資料
            var deleteOK = coilController.DeletePDIByEntryCoilID(mockPdiData.PlanNo, mockPdiData.EntryCoilNo);
            Assert.IsTrue(deleteOK);

            var deleteOK2 = coilController.DeletePDIByEntryCoilID(mockPdiData2.PlanNo, mockPdiData2.EntryCoilNo);
            Assert.IsTrue(deleteOK2);
        }

        // 更新PDI生產結束時間
        [Test]
        public void UpdatePDIEndime()
        {
            coilController.SetLog(log);

            var mockPdiData = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            mockPdiData.Out_Mat_No = "ABCDABCD".ToByteData();
            mockPdiData.Plan_No = "PLAN000".ToByteData();
            var insertOK = coilController.CreatePDI(mockPdiData);
            Assert.IsTrue(insertOK);

            var mockPdiData2 = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            mockPdiData2.Out_Mat_No = "ABCDABCD".ToByteData();
            mockPdiData2.Plan_No = "PLAN001".ToByteData();
            var insertOK2 = coilController.CreatePDI(mockPdiData2);
            Assert.IsTrue(insertOK2);


            var nowTime = DateTime.Now;
            var updateOK = coilController.UpdatePDIFinishTime("ABCDABCD", nowTime);
            Assert.IsTrue(updateOK);

            var pdi = coilController.GetPDI("ABCDABCD", PDISchema.OutCoilID);
            var pdiFinishTime = pdi.Finish_Time.ToString(DBDateTimeFromat);
            Assert.IsTrue(!pdiFinishTime.Equals(NullTime));


            // 測試完畢 刪除測試資料
            var deleteOK = coilController.DeletePDIByEntryCoilID(mockPdiData.PlanNo, mockPdiData.EntryCoilNo);
            Assert.IsTrue(deleteOK);

            var deleteOK2 = coilController.DeletePDIByEntryCoilID(mockPdiData2.PlanNo, mockPdiData2.EntryCoilNo);
            Assert.IsTrue(deleteOK2);
        }

        // 更新PDI鋼捲到來時間
        [Test]
        public void UpdatePDIEntryTime()
        {
            coilController.SetLog(log);

            var mockPdiData = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            mockPdiData.Entry_Coil_No = "ABDEABDE".ToByteData();
            mockPdiData.Plan_No = "PLAN000".ToByteData();
            var insertOK = coilController.CreatePDI(mockPdiData);
            Assert.IsTrue(insertOK);

            var mockPdiData2 = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            mockPdiData2.Entry_Coil_No = "ABDEABDE".ToByteData();
            mockPdiData2.Plan_No = "PLAN001".ToByteData();
            var insertOK2 = coilController.CreatePDI(mockPdiData2);
            Assert.IsTrue(insertOK2);

            var nowTime = DateTime.Now;
            var updateOK = coilController.UpdatePDIEntryTime("ABDEABDE", nowTime);
            Assert.IsTrue(updateOK);

            var pdi = coilController.GetPDI("ABDEABDE", PDISchema.EntryCoilID);
            var pdiEntryTime = pdi.Entry_Arrive_Time.ToString(DBDateTimeFromat);
            Assert.IsTrue(!pdiEntryTime.Equals(NullTime));


            // 測試完畢 刪除測試資料
            var deleteOK = coilController.DeletePDIByEntryCoilID(mockPdiData.PlanNo, mockPdiData.EntryCoilNo);
            Assert.IsTrue(deleteOK);

            var deleteOK2 = coilController.DeletePDIByEntryCoilID(mockPdiData2.PlanNo, mockPdiData2.EntryCoilNo);
            Assert.IsTrue(deleteOK2);
        }

        // 新增PDI測試
        [Test]
        public void NewPDIData()
        {
            coilController.SetLog(log);

            // 新增 PDI 測試資料
            var mockPdiData = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;

            // 插入存取測試
            var insertOK = coilController.CreatePDI(mockPdiData);
            Assert.IsTrue(insertOK);

            // 刪除測試資料
            var deleteOK = coilController.DeletePDIByEntryCoilID(mockPdiData.PlanNo, mockPdiData.EntryCoilNo);
            Assert.IsTrue(deleteOK);

        }

        // 更新PDI測試
        [Test]
        public void UpdatePDIData()
        {
            coilController.SetLog(log);


            var mockPdiData = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;

            var insertOK = coilController.CreatePDI(mockPdiData);
            Assert.IsTrue(insertOK);


            var updateOK = coilController.UpdatePDI(mockPdiData.PlanNo, mockPdiData.EntryCoilNo, mockPdiData);
            Assert.IsTrue(updateOK);


            var deleteOK = coilController.DeletePDIByEntryCoilID(mockPdiData.PlanNo, mockPdiData.EntryCoilNo);
            Assert.IsTrue(deleteOK);
        }


        [Test]
        public void SavePDIDefect()
        {
            coilController.SetLog(log);
            var mockPdiData = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;

            var insertOK = coilController.CreateDefect(mockPdiData);
            Assert.IsTrue(insertOK);

            var deleteOK = coilController.DeleteDefect(mockPdiData.PlanNo, mockPdiData.EntryCoilNo);
            Assert.IsTrue(deleteOK);

        }


        [Test]
        public void UpdatePDIDefect()
        {
            coilController.SetLog(log);

            var mockPdiData = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            mockPdiData.Entry_Coil_No = "HE001".ToByteData();

            var insertOK = coilController.CreateDefect(mockPdiData);
            Assert.IsTrue(insertOK);

            mockPdiData.D01_Code = "12".ToByteData();
            var updateOK = coilController.UpdateDefect(mockPdiData);
            Assert.IsTrue(updateOK);

            var deleteOK = coilController.DeleteDefect(mockPdiData.PlanNo, mockPdiData.EntryCoilNo);
            Assert.IsTrue(deleteOK);

        }

    }
}
