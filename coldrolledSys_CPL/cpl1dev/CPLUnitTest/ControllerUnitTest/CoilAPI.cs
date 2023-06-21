using Controller.Coil;
using LogSender;
using NUnit.Framework;
using static Core.Define.DBParaDef;
using static DataMod.Response.RespnseModel;
using static MsgStruct.L1L2Rcv;
using static MsgStruct.MMSL2Rcv;

namespace CPLUnitTest.ControllerUnitTest
{
    public class CoilAPI
    {

        ICoilController coilController = new CoilController();
        ILog log = new Log(string.Empty, string.Empty, null, false, true);

        [Test]
        public void SaveRetrunCoilInfo()
        {
            coilController.SetLog(log);

            // 新增 PDI 測試資料
            var mockPdiData = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            mockPdiData.Entry_Coil_No = "HE3001103010000".ToByteData();
            var insertOK = coilController.CreatePDI(mockPdiData);
            Assert.IsTrue(insertOK);

            var msg317 = new Msg_317_ReturnCoilInfo();
            msg317.CoilID = "HE3001103010000".ToByteData();
            msg317.CoilWeight = 10;
            msg317.CoilLength = 10;
            msg317.Diameter = 10;
            msg317.CoiInsideDiam = 10;
            msg317.Width = 10;
            insertOK = coilController.CreateL1RetrunCoil(msg317);
            Assert.IsTrue(insertOK);


            var deleteOK = coilController.DeleteSchDelCoilRejectTempRecord("HE3001103010000");
            Assert.IsTrue(deleteOK);

            // 測試完畢 刪除測試資料
            deleteOK = coilController.DeletePDIByEntryCoilID(mockPdiData.PlanNo, mockPdiData.EntryCoilNo);
            Assert.IsTrue(deleteOK);
        }


        [Test]
        public void GetReturnCoilTemp()
        {
            coilController.SetLog(log);
            // 新增 PDI 測試資料
            var mockPdiData = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            mockPdiData.Entry_Coil_No = "HE2001103010000".ToByteData();
            var insertOK = coilController.CreatePDI(mockPdiData);
            Assert.IsTrue(insertOK);


            var msg317 = new Msg_317_ReturnCoilInfo();
            msg317.CoilID = "HE2001103010000".ToByteData();
            msg317.CoilWeight = 10;
            msg317.CoilLength = 10;
            msg317.Diameter = 10;
            msg317.CoiInsideDiam = 10;
            msg317.Width = 10;
            insertOK = coilController.CreateL1RetrunCoil(msg317);
            Assert.IsTrue(insertOK);

            var res = coilController.GetReturnCoilTemp("HE2001103010000");
            Assert.IsNotNull(res);

            var deleteOK = coilController.DeleteSchDelCoilRejectTempRecord("HE2001103010000");
            Assert.IsTrue(deleteOK);


            deleteOK = coilController.DeletePDIByEntryCoilID(mockPdiData.PlanNo, mockPdiData.EntryCoilNo);
            Assert.IsTrue(deleteOK);

        }

      
        [Test]
        public void SaveTempToCoilReject()
        {
            coilController.SetLog(log);

            var mockData = new ReturnCoilInfo().LoadMockDBData<ReturnCoilInfo>() as ReturnCoilInfo;

            var insertOK = coilController.CreateTempToCoilReject(mockData);
            Assert.IsTrue(insertOK);

            var deleteOK = coilController.DeleteCoilRejectByCoilID(mockData.Coil_ID);
            Assert.IsTrue(deleteOK);

        }

        [Test]
        public void SaveEnCoilCutRecord()
        {
            coilController.SetLog(log);

            var coilID = "HE123456789";

            var mockPdiData = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            mockPdiData.Entry_Coil_No = coilID.ToByteData();

            var insertOK = coilController.CreatePDI(mockPdiData);
            Assert.IsTrue(insertOK);

            var msg301 = new Msg_301_EnCoilCut();
            msg301.Date = 20210426;
            msg301.Time = 125955;
            msg301.CoilID = coilID.ToByteData();
            msg301.CutMode = 1;
            msg301.CutLength = 100f;

            var saveOK = coilController.CreateEnCoilCutRecordTemp(msg301);
            Assert.IsTrue(saveOK);

            var deleteOK = coilController.DeleteCoilCutRecordTemp(coilID);
            Assert.IsTrue(deleteOK);

            var deletePDIOK = coilController.DeletePDIByEntryCoilID(mockPdiData.PlanNo, mockPdiData.EntryCoilNo);
            Assert.IsTrue(deletePDIOK);

        }

        [Test]
        public void SaveExitCoilCutRecordTemp()
        {
            coilController.SetLog(log);

            var coilID = "TE202230010000";
            var outCoilID = "CE202230010000";

            var mockPdiData = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            mockPdiData.Entry_Coil_No = coilID.ToByteData();
            mockPdiData.Out_Mat_No = outCoilID.ToByteData();            
            var insertOK = coilController.CreatePDI(mockPdiData);
            Assert.IsTrue(insertOK);

            var msg = new Msg_311_ExCoilCut();
            msg.Date = 20210426;
            msg.Time = 125955;
            msg.CoilID = coilID.ToByteData();
            msg.CutMode = 1;
            msg.CutLength = 100f;
            msg.DiamRec = 10;

            var saveOK = coilController.CreateExitCoilCutRecordTemp(msg, outCoilID, coilID, outCoilID);
            Assert.IsTrue(saveOK);

            var deleteOK = coilController.DeleteCoilCutRecordTemp(outCoilID);
            Assert.IsTrue(deleteOK);

            var deletePDIOK = coilController.DeletePDIByEntryCoilID(mockPdiData.PlanNo, mockPdiData.EntryCoilNo);
            Assert.IsTrue(deletePDIOK);
        }

        [Test]
        public void SplitCoilTest()
        {
            coilController.SetLog(log);

            var coilID = "TE502230010000";
            var outCoilID = "CE502230010000";

            var mockPdiData = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            mockPdiData.Entry_Coil_No = coilID.ToByteData();
            mockPdiData.Out_Mat_No = outCoilID.ToByteData();
            var insertOK = coilController.CreatePDI(mockPdiData);
            Assert.IsTrue(insertOK);

            var mag = new Msg_311_ExCoilCut();
            mag.Date = 20210426;
            mag.Time = 125955;
            mag.CoilID = coilID.ToByteData();
            mag.CutMode = 2;
            mag.CutLength = 100f;

            var childCoilID = coilController.GenSplitChildrenCoilID(outCoilID);
            var saveOK = coilController.CreateExitCoilCutRecordTemp(mag, childCoilID, coilID, outCoilID);
            Assert.IsTrue(saveOK);

            var deleteOK = coilController.DeleteCoilCutRecordTemp(childCoilID);
            Assert.IsTrue(deleteOK);

            var deletePDIOK = coilController.DeletePDIByEntryCoilID(mockPdiData.PlanNo, mockPdiData.EntryCoilNo);
            Assert.IsTrue(deletePDIOK);
        }


        // 斷帶紀錄測試
        [Test]
        public void SaveUnmountRecord()
        {
            // 新增PDI資料
            coilController.SetLog(log);

            var coilID = "TE402230010002";

            var mockPdiData = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            mockPdiData.Entry_Coil_No = coilID.ToByteData();

            var insertOK = coilController.CreatePDI(mockPdiData);
            Assert.IsTrue(insertOK);

            // 新增測試資料
            var msg = new Msg_311_ExCoilCut();
            msg.Date = 20210426;
            msg.Time = 125955;
            msg.CoilID = coilID.ToByteData();
            msg.CutMode = 2;
            msg.CutLength = 100f;

            // 測試存取
            var childCoilID = coilController.GenSplitChildrenCoilID(coilID);
            var saveOK = coilController.CreateUmontRecord(msg, childCoilID, coilID);
            Assert.IsTrue(saveOK);

            // 刪除測試資料
            var deleteOK = coilController.DeleteUmountRecord(childCoilID);
            Assert.IsTrue(deleteOK);

            var deletePDIOK = coilController.DeletePDIByEntryCoilID(mockPdiData.PlanNo, mockPdiData.EntryCoilNo);
            Assert.IsTrue(deletePDIOK);
        }

        // 斷帶存取排程跳軋/鋼捲退料暫存記錄
        [Test]
        public void SaveStripBrekInScheduleDeleteCoilRejectTemp()
        {
            // 新增PDI資料
            coilController.SetLog(log);

            var coilID = "TTP902230010000".ToByteData();

            var mockPdiData = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            mockPdiData.Entry_Coil_No = coilID;

            // 存取測試資料
            var insertOK = coilController.CreatePDI(mockPdiData);
            Assert.IsTrue(insertOK);

            var pdi = coilController.GetPDI("TTP902230010000", PDISchema.EntryCoilID);
            Assert.IsTrue(pdi != null);

            var childCoilID = coilController.GenSplitChildrenCoilID("TTP902230010000");
            var saveOK = coilController.CreateStripBrekInScheduleDeleteCoilRejectTemp(childCoilID, pdi);
            Assert.IsTrue(saveOK);

            var deleteOK = coilController.DeleteSchDelCoilRejectTempRecord(childCoilID);
            Assert.IsTrue(deleteOK);

            var deletePDIOK = coilController.DeletePDIByEntryCoilID(mockPdiData.PlanNo, mockPdiData.EntryCoilNo);
            Assert.IsTrue(deletePDIOK);

        }

        // L1 Coil Dismount 存取 排程跳軋/鋼捲退料暫存記錄資料測試
        [Test]
        public void SaveCoilDismountInfoInScheduleDeleteCoilRejectTemp()
        {

            // 新增PDI資料
            coilController.SetLog(log);

            var forkID = "T999999999";

            var msg = new Msg_307_CoilDismount();
            msg.CoilID = forkID.ToByteData();
            msg.CoilWeight = 10;
            msg.CoilLength = 20;
            msg.Diameter = 30;
            msg.CoiInsideDiam = 40;


            // 測試
            coilController.CreateCoilDismountInfoInScheduleDeleteCoilRejectTemp(msg, forkID);
            var res = coilController.GetCoilScheduleDelTempRecord(forkID);
            Assert.IsTrue(res.Weight_Of_Rejected_Coil.Equals("10"));
            Assert.IsTrue(res.Length_Of_Rejected_Coil.Equals("20"));
            Assert.IsTrue(res.Outer_Diameter_Of_RejectedCoil.Equals("30"));
            Assert.IsTrue(res.Inner_Diameter_Of_RejectedCoil.Equals("40"));

            // 刪除測試資料
            var deleteOK = coilController.DeleteSchDelCoilRejectTempRecord(forkID);
            Assert.IsTrue(deleteOK);


        }

        [Test]
        public void SaveCoilWeightScaleInScheduleDeleteCoilRejectTemp() {

            // 新增PDI資料
            coilController.SetLog(log);

            var forkID = "T999999999";

            var msg = new Msg_308_CoilWeightScale();
            msg.CoilID = forkID.ToByteData();
            msg.CoilWeight = 10;

            // 測試
            coilController.CreateCoilWeightScaleInScheduleDeleteCoilRejectTemp(msg, forkID);
            var res = coilController.GetCoilScheduleDelTempRecord(forkID);
            Assert.IsTrue(res.Weight_Of_Rejected_Coil.Equals("10"));

            // 刪除測試資料
            var deleteOK = coilController.DeleteSchDelCoilRejectTempRecord(forkID);
            Assert.IsTrue(deleteOK);
        }


        [Test]
        public void SaveSampleCoil()
        {
            // 新增PDI資料
            coilController.SetLog(log);


            var coilID = "TMP902230010000";

            var mockPdiData = new Msg_PDI().LoadMockMsgData<Msg_PDI>() as Msg_PDI;
            mockPdiData.Entry_Coil_No = coilID.ToByteData();
            mockPdiData.Sample_Frqn_Code = "100".ToByteData();
            // 存取測試資料
            var insertOK = coilController.CreatePDI(mockPdiData);
            Assert.IsTrue(insertOK);


            var msg = new Msg_311_ExCoilCut();
            msg.Date = 20210426;
            msg.Time = 125955;
            msg.CoilID = coilID.ToByteData();
            msg.CutMode = 2;
            msg.CutLength = 100f;
            msg.CalculateWeightRec = 100;
            msg.DiamRec = 100;
            msg.PUWFlag = 1;

            var pdi = coilController.GetPDI(coilID, PDISchema.EntryCoilID);
            var samplePos = pdi.SamplePosStr;
            var sampleCoilID = coilID + samplePos;
            var saveOK = coilController.CreateSampleCoil(msg, pdi, sampleCoilID);
            var sampleCoil = coilController.GetCoilSampleInfo(pdi.Plan_No, pdi.Mat_Seq_No, pdi.Plan_Sort, sampleCoilID);
            Assert.IsTrue(sampleCoil.Sample_Length == 100f);
            Assert.IsTrue(sampleCoil.Calculate_Weight == 100);
            Assert.IsTrue(sampleCoil.Outer_Diameter == 100);
            Assert.IsTrue(sampleCoil.Paper_Unwinder.Equals("1"));

            var deleteOK = coilController.DeleteCoilSampleInfo(pdi.Plan_No, pdi.Mat_Seq_No, pdi.Plan_Sort, sampleCoilID);
            Assert.IsTrue(deleteOK);

            var deletePDIOK = coilController.DeletePDIByEntryCoilID(mockPdiData.PlanNo, coilID);
            Assert.IsTrue(deletePDIOK);

        }
    }
}
