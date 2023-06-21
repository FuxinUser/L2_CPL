using Controller.Coil;
using LogSender;
using NUnit.Framework;
using static Core.Define.DBParaDef;
using static DataMod.Response.RespnseModel;
using static MsgStruct.L1L2Rcv;
using static MsgStruct.MMSL2Rcv;

namespace CPLUnitTest.ControllerUnitTest
{
    public class CoilSplitAPI
    {
        ICoilController coilController = new CoilController();
        ILog log = new Log(string.Empty, string.Empty, null, false, true);

        // 模擬第一次分切
        [Test]
        public void ColdRolledFirstSplit()
        {
            coilController.SetLog(log);

            // HE999999990000 第一次分切
            var coilID = "HE999999990000";
            var splitID = coilController.GenSplitChildrenCoilID(coilID);
            Assert.IsTrue(splitID.Equals("HE999999991000"));

            // HE999999991000 第一次分切
            coilID = "HE999999991000";
            splitID = coilController.GenSplitChildrenCoilID(coilID);
            Assert.IsTrue(splitID.Equals("HE999999991100"));

            // HE999999991200 第一次分切
            coilID = "HE999999991200";
            splitID = coilController.GenSplitChildrenCoilID(coilID);
            Assert.IsTrue(splitID.Equals("HE999999991210"));

            // HE999999991210 第一次分切
            coilID = "HE999999991210";
            splitID = coilController.GenSplitChildrenCoilID(coilID);
            Assert.IsTrue(splitID.Equals("HE999999991211"));
        }

        // 模擬已分切過一次
        [Test]
        public void ColdRolledSecondSplit()
        {
            coilController.SetLog(log);

            // HE999999990000 第二次分切
            var coilID = "HE999999990000";
            var splitID = coilController.GenSplitChildrenCoilID(coilID, 1);
            Assert.IsTrue(splitID.Equals("HE999999992000"));

            // HE999999990000 第二次分切
            coilID = "HE999999991000";
            splitID = coilController.GenSplitChildrenCoilID(coilID, 1);
            Assert.IsTrue(splitID.Equals("HE999999991200"));

            // HE999999991200 第二次分切
            coilID = "HE999999991200";
            splitID = coilController.GenSplitChildrenCoilID(coilID, 1);
            Assert.IsTrue(splitID.Equals("HE999999991220"));

            // HE999999991210 第二次分切
            coilID = "HE999999991210";
            splitID = coilController.GenSplitChildrenCoilID(coilID, 1);
            Assert.IsTrue(splitID.Equals("HE999999991212"));
        }

        // 模擬已分切過二次
        [Test]
        public void ColdRolledThirdSplit()
        {
            coilController.SetLog(log);

            var coilID = "HE999999990000";
            var splitID = coilController.GenSplitChildrenCoilID(coilID, 2);
            Assert.IsTrue(splitID.Equals("HE999999993000"));

            coilID = "HE999999991000";
            splitID = coilController.GenSplitChildrenCoilID(coilID, 2);
            Assert.IsTrue(splitID.Equals("HE999999991300"));
        }

        [Test]
        public void HotRolledFirstSplit()
        {
            coilController.SetLog(log);

            var coilID = "HE9999999900";
            var splitID = coilController.GenSplitChildrenCoilID(coilID);
            Assert.IsTrue(splitID.Equals("HE9999999910"));


            coilID = "HE9999999910";
            splitID = coilController.GenSplitChildrenCoilID(coilID);
            Assert.IsTrue(splitID.Equals("HE9999999911"));
        }

        [Test]
        public void HotRolledSecondSplit()
        {
            coilController.SetLog(log);

            var coilID = "HE9999999900";
            var splitID = coilController.GenSplitChildrenCoilID(coilID, 1);
            Assert.IsTrue(splitID.Equals("HE9999999920"));


            coilID = "HE9999999910";
            splitID = coilController.GenSplitChildrenCoilID(coilID, 1);
            Assert.IsTrue(splitID.Equals("HE9999999912"));
        }



    }
}
