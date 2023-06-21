using Controller.Sys;
using Controller.Track;
using Core.Define;
using DBService;
using LogSender;
using NUnit.Framework;
using static MsgStruct.L1L2Rcv;

namespace CPLUnitTest.ControllerUnitTest
{
    public class TrkAPI
    {
        ITrackingController trkController = new TrackingController();
        ISysController sysController = new SysController();
        ILog log = new Log(string.Empty, string.Empty, null, false, true);

        [Test]
        public void IsSystemAutoValueOn()
        {
            sysController.SetLog(log);

            var autoFeedOn = sysController.VaildSystemAutoValueOn(DBColumnDef.SysParaGroup,
                                                               DBColumnDef.SysParaAutoInputFlag,
                                                               "是否為自動進料");
            Assert.IsTrue(autoFeedOn);
        }

        [Test]
        public void UpdateSysValue()
        {
            sysController.SetLog(log);

            var updateOK = sysController.UpdateSysValue(L2SystemDef.CPLGroup, DBColumnDef.SysTopScheduleLock, DBParaDef.NOTUSE);
            Assert.IsTrue(updateOK);
        }

        [Test]
        public void UpdateEntryTrackMap()
        {
            trkController.SetLog(log);

            var entrkMap = new Msg_305_TrackMapEn();
            entrkMap.CoilIDUnCoiler = "HE00100010".ToByteData();
            entrkMap.CoilIDUnSkid1 = "HE00100010".ToByteData();
            entrkMap.CoilIDUnSkid2 = "HE00100010".ToByteData();
            entrkMap.CoilIDUnTop = "HE00100010".ToByteData();
            entrkMap.CoilIDCar = "HE00100010".ToByteData();
            Assert.IsTrue(trkController.UpdateEntryTrackMap(entrkMap));


            entrkMap.CoilIDUnCoiler = string.Empty.ToByteData();
            entrkMap.CoilIDUnSkid1 = string.Empty.ToByteData();
            entrkMap.CoilIDUnSkid2 = string.Empty.ToByteData();
            entrkMap.CoilIDUnTop = string.Empty.ToByteData();
            entrkMap.CoilIDCar = string.Empty.ToByteData();
            Assert.IsTrue(trkController.UpdateEntryTrackMap(entrkMap));

        }

        [Test]
        public void UpdateExitTrackMap()
        {
            trkController.SetLog(log);

            var exitMap = new Msg_306_TrackMapEx();
            exitMap.CoilIDCar = "HE00100010".ToByteData();
            exitMap.CoilIDReCoiler = "HE00100010".ToByteData();
            exitMap.CoilIDRecSkid1 = "HE00100010".ToByteData();
            exitMap.CoilIDRecSkid2 = "HE00100010".ToByteData();
            exitMap.CoilIDRecTop = "HE00100010".ToByteData();
            Assert.IsTrue(trkController.UpdateExitTrackMap(exitMap));


            exitMap.CoilIDCar = string.Empty.ToByteData();
            exitMap.CoilIDReCoiler = string.Empty.ToByteData();
            exitMap.CoilIDRecSkid1 = string.Empty.ToByteData();
            exitMap.CoilIDRecSkid2 = string.Empty.ToByteData();
            exitMap.CoilIDRecTop = string.Empty.ToByteData();
            Assert.IsTrue(trkController.UpdateExitTrackMap(exitMap));

        }


        [Test]
        public void GetTrackMap()
        {
            trkController.SetLog(log);

            var entrkMap = new Msg_305_TrackMapEn();
            entrkMap.CoilIDUnCoiler = "HE00100010".ToByteData();
            entrkMap.CoilIDUnSkid1 = "HE00100010".ToByteData();
            entrkMap.CoilIDUnSkid2 = "HE00100010".ToByteData();
            entrkMap.CoilIDUnTop = "HE00100010".ToByteData();
            entrkMap.CoilIDCar = "HE00100010".ToByteData();
            Assert.IsTrue(trkController.UpdateEntryTrackMap(entrkMap));

            var map = trkController.GetTrackMap();
            Assert.IsTrue(map.Entry_Car.Trim().Equals("HE00100010"));

            entrkMap.CoilIDUnCoiler = string.Empty.ToByteData();
            entrkMap.CoilIDUnSkid1 = string.Empty.ToByteData();
            entrkMap.CoilIDUnSkid2 = string.Empty.ToByteData();
            entrkMap.CoilIDUnTop = string.Empty.ToByteData();
            entrkMap.CoilIDCar = string.Empty.ToByteData();
            Assert.IsTrue(trkController.UpdateEntryTrackMap(entrkMap));

        }


        [Test]
        public void UpdateEqupMaint()
        {
            trkController.SetLog(log);

            var eqMaint = new Msg_309_EquipMaint();
            var mockData = eqMaint.LoadMockMsgData<Msg_309_EquipMaint>() as Msg_309_EquipMaint;
            var updateOK = trkController.UpdateEqupMaint(mockData);
            Assert.IsTrue(updateOK);
        }

    }
}
