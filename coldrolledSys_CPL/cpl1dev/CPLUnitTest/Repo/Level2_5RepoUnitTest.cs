using Core.Define;
using DBService.Level25Repository;
using DBService.Level25Repository.L2L25_Alive;
using DBService.Level25Repository.L2L25_CoilMap;
using DBService.Level25Repository.L2L25_CoilPDI;
using DBService.Level25Repository.L2L25_CoilPDO;
using DBService.Level25Repository.L2L25_CPL1PRESET;
using DBService.Level25Repository.L2L25_DownTime;
using DBService.Level25Repository.L2L25_ENGC;
using DBService.Level25Repository.L2L25_L1L2DisConnection;
using DBService.Level25Repository.L2L25_L2APStatus;
using DBService.Level25Repository.L2L25_RECCurrentCT;
using DBService.Level25Repository.L2L25_RECTensionCT;
using DBService.Level25Repository.L2L25_SpeedCT;
using DBService.Level25Repository.L2L25_UNCCurrentCT;
using DBService.Level25Repository.L2L25_UNCTensionCT;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPLUnitTest.Repo
{
    public class Level2_5RepoUnitTest
    {


        [Test]
        public void L2L25_Alive()
        {
            var repo = new L2L25_AliveRepo(DBParaDef.Level2_5DBConn);
            var dbData = new L2L25_Alive().LoadMockDBData<L2L25_Alive>() as L2L25_Alive;
            var insertNum = repo.Insert(dbData);
            Assert.IsTrue(insertNum > 0);
        }

        [Test]
        public void L2L25_CoilMapRepo()
        {

            var repo = new L2L25_CoilMapRepo(DBParaDef.Level2_5DBConn);
            var dbData = new L2L25_CoilMap().LoadMockDBData<L2L25_CoilMap>() as L2L25_CoilMap;
            var insertNum = repo.Insert(dbData);
            Assert.IsTrue(insertNum > 0);        
        }

        [Test]
        public void L2L25_CoilPDIRepo()
        {

            var repo = new L2L25_CoilPDIRepo(DBParaDef.Level2_5DBConn);
            var dbData = new L2L25_CoilPDI().LoadMockDBData<L2L25_CoilPDI>() as L2L25_CoilPDI;
            var insertNum = repo.Insert(dbData);
            Assert.IsTrue(insertNum > 0);
        }


        [Test]
        public void L2L25_CoilPDORepo()
        {

            var repo = new L2L25_CoilPDORepo(DBParaDef.Level2_5DBConn);
            var dbData = new L2L25_CoilPDO().LoadMockDBData<L2L25_CoilPDO>() as L2L25_CoilPDO;
            var insertNum = repo.Insert(dbData);
            Assert.IsTrue(insertNum > 0);
        }


        [Test]
        public void L2L25_CPL1PRESETRepo()
        {

            var repo = new L2L25_CPLPRESETRepo(DBParaDef.Level2_5DBConn);
            var dbData = new L2L25_CPLPRESET().LoadMockDBData<L2L25_CPLPRESET>() as L2L25_CPLPRESET;
            var insertNum = repo.Insert(dbData);
            Assert.IsTrue(insertNum > 0);
        }

      


        [Test]
        public void L2L25_DownTimeRepo()
        {
            var repo = new L2L25_DownTimeRepo(DBParaDef.Level2_5DBConn);
            var dbData = new L2L25_DownTime().LoadMockDBData<L2L25_DownTime>() as L2L25_DownTime;
            var insertNum = repo.Insert(dbData);
            Assert.IsTrue(insertNum > 0);
        }

        [Test]
        public void L2L25_ENGCRepo()
        {
            var repo = new L2L25_ENGCRepo(DBParaDef.Level2_5DBConn);
            var dbData = new L2L25_ENGC().LoadMockDBData<L2L25_ENGC>() as L2L25_ENGC;
            var insertNum = repo.Insert(dbData);
            Assert.IsTrue(insertNum > 0);
        }

        [Test]
        public void L2L25_L1L2DisConnectionRepo()
        {
            var repo = new L2L25_L1L2DisConnectionRepo(DBParaDef.Level2_5DBConn);
            var dbData = new L2L25_L1L2DisConnection().LoadMockDBData<L2L25_L1L2DisConnection>() as L2L25_L1L2DisConnection;
            var insertNum = repo.Insert(dbData);
            Assert.IsTrue(insertNum > 0);
        }

        [Test]
        public void L2L25_L2APStatusRepo()
        {
            var repo = new L2L25_L2APStatusRepo(DBParaDef.Level2_5DBConn);
            var dbData = new L2L25_L2APStatus().LoadMockDBData<L2L25_L2APStatus>() as L2L25_L2APStatus;
            var insertNum = repo.Insert(dbData);
            Assert.IsTrue(insertNum > 0);
        }

        [Test]
        public void L2L25_RECCurrentCTRepo()
        {
            var repo = new L2L25_RECCurrentCTRepo(DBParaDef.Level2_5DBConn);
            var dbData = new L2L25_RECCurrentCT().LoadMockDBData<L2L25_RECCurrentCT>() as L2L25_RECCurrentCT;
            var insertNum = repo.Insert(dbData);
            Assert.IsTrue(insertNum > 0);
        }

        [Test]
        public void L2L25_RECTensionCTRepo()
        {
            var repo = new L2L25_RECTensionCTRepo(DBParaDef.Level2_5DBConn);
            var dbData = new L2L25_RECTensionCT().LoadMockDBData<L2L25_RECTensionCT>() as L2L25_RECTensionCT;
            var insertNum = repo.Insert(dbData);
            Assert.IsTrue(insertNum > 0);
        }

        [Test]
        public void L2L25_SpeedCTRepo()
        {
            var repo = new L2L25_SpeedCTRepo(DBParaDef.Level2_5DBConn);
            var dbData = new L2L25_SpeedCT().LoadMockDBData<L2L25_SpeedCT>() as L2L25_SpeedCT;
            var insertNum = repo.Insert(dbData);
            Assert.IsTrue(insertNum > 0);
        }

        

        [Test]
        public void L2L25_UNCCurrentCTRepo()
        {
            var repo = new L2L25_UNCCurrentCTRepo(DBParaDef.Level2_5DBConn);
            var dbData = new L2L25_UNCCurrentCT().LoadMockDBData<L2L25_UNCCurrentCT>() as L2L25_UNCCurrentCT;
            var insertNum = repo.Insert(dbData);
            Assert.IsTrue(insertNum > 0);
        }

        [Test]
        public void L2L25_UNCTensionCTRepo()
        {
            var repo = new L2L25_UNCTensionCTRepo(DBParaDef.Level2_5DBConn);
            var dbData = new L2L25_UNCTensionCT().LoadMockDBData<L2L25_UNCTensionCT>() as L2L25_UNCTensionCT;
            var insertNum = repo.Insert(dbData);
            Assert.IsTrue(insertNum > 0);
        }
    }
}
