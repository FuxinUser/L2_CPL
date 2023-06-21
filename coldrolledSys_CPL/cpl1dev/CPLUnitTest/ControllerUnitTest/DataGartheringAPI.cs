using BLL.Logic;
using Controller.DtGtr;
using LogSender;
using NUnit.Framework;
using System;
using System.Linq;
using static DBService.Repository.LineStatus.ProcessDataEntity;
using static MsgStruct.L1L2Rcv;

namespace CPLUnitTest.ControllerUnitTest
{
    public class DataGartheringAPI
    {
        IDataGatheringController dataGatheringController = new DataGartheringController();
        ILog log = new Log(string.Empty, string.Empty, null, false, true);



        [Test]
        public void SaveCoilWeld()
        {
            dataGatheringController.SetLog(log);
            var coilID = "HE1234";

            var msg = new Msg_302_CoilWeld();
            var coilWeld = msg.LoadMockMsgData<Msg_302_CoilWeld>() as Msg_302_CoilWeld;
            coilWeld.CoilID = coilID.ToByteData();
            var insertOK = dataGatheringController.CreateCoilWeld(coilWeld, coilID);
            Assert.IsTrue(insertOK);

            var deleteOK = dataGatheringController.DeleteCoilWeld(coilID);
            Assert.IsTrue(deleteOK);
        }

        [Test]
        public void SaveProcessData()
        {
            dataGatheringController.SetLog(log);
            var msg = new Msg_313_SpdTen();
            msg.ActualSpeed = 12;
            msg.TenRefUnc = 12;
            msg.TenActUnc = 12;
            msg.UncMotorActCurrent = 12;
            msg.TenRefRec = 12;
            msg.TenActRec = 12;
            msg.RecMotorActCurrent = 12;
            msg.WeldActCurrentFront = 12;
            msg.WeldActSpd = 12;
            msg.WeldActCurrentRear = 12;
            msg.WeldActPlanishRollForce = 12;
            msg.WeldTemperature = 12;

            msg.Date = 20210426;
            msg.Time = 125955;

            var insertOK = dataGatheringController.CreateProcessData(msg);
            Assert.IsTrue(insertOK);

            var deleteOK = dataGatheringController.DeleteProcessDataByRecTime(msg.DateTime);
            Assert.IsTrue(deleteOK);
        }


        [Test]
        public void QueryProcessData()
        {
            dataGatheringController.SetLog(log);
            var msg = new Msg_313_SpdTen();
            msg.ActualSpeed = 12;
            msg.TenRefUnc = 12;
            msg.TenActUnc = 12;
            msg.UncMotorActCurrent = 12;
            msg.TenRefRec = 12;
            msg.TenActRec = 12;
            msg.RecMotorActCurrent = 12;
            msg.WeldActCurrentFront = 12;
            msg.WeldActSpd = 12;
            msg.WeldActCurrentRear = 12;
            msg.WeldActPlanishRollForce = 12;
            msg.WeldTemperature = 12;

            msg.Date = 20210908;
            msg.Time = 121150;


            var insertOK = dataGatheringController.CreateProcessData(msg);
            Assert.IsTrue(insertOK);

            var entitys = dataGatheringController.QueryProcessDatas(msg.DateTime, msg.DateTime);
            Assert.IsTrue(entitys.Count() > 0);


            var deleteOK = dataGatheringController.DeleteProcessDataByRecTime(msg.DateTime);
            Assert.IsTrue(deleteOK);
        }

        [Test]
        public void QuerySiderTrimmerData()
        {
            dataGatheringController.SetLog(log);
            var msg = new Msg_318_SideTrimmerInfo();
            msg.SideTrimmerGap = 20;
            msg.SideTrimmerLap = 20;
            msg.SideTrimmerWidth = 20;
            msg.Trimming_OperateSide = 20;
            msg.Trimming_DriveSide = 20;
            msg.Date = 20210908;
            msg.Time = 121150;

            var insertOK = dataGatheringController.CreateSideTrimmer(msg);
            Assert.IsTrue(insertOK);

            var entitys = dataGatheringController.QuerySiderTrimmerTmp(msg.DateTime, msg.DateTime);
            Assert.IsTrue(entitys.Count() > 0);

            var deleteOK = dataGatheringController.DeleteSiderTrimmerByRecTime(msg.DateTime);
            Assert.IsTrue(deleteOK);

        }


        [Test]
        public void DeleteAllSiderTrimmerData()
        {
            dataGatheringController.SetLog(log);
            var msg = new Msg_318_SideTrimmerInfo();
            msg.SideTrimmerGap = 30;
            msg.SideTrimmerLap = 30;
            msg.SideTrimmerWidth = 30;
            msg.Trimming_OperateSide = 20;
            msg.Trimming_DriveSide = 20;
            msg.Date = 20210901;
            msg.Time = 121151;

            var insertOK = dataGatheringController.CreateSideTrimmer(msg);
            Assert.IsTrue(insertOK);

            var deleteNum = dataGatheringController.DeleteAllSiderTrimmer();
            Assert.IsTrue(deleteNum == -1);

        }
    }
}
