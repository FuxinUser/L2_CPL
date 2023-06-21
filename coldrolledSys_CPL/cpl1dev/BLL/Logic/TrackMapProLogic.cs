using Core.Define;
using DBService.Level25Repository.L2L25_CoilMap;
using DBService.Repository;
using System.Linq;
using static DBService.Repository.CoilMapEntity;

/**
 Author: ICSC Spyua
 Desc: Coil Pro BLL
 Date: 2019/12/26
*/

namespace BLL.Trck
{
    public class TrackMapProLogic
    {
        private CoilMapRepo _coilMapRepo;
        private ProductionScheduleRepo _coilScheduleRepo;
        private L2L25_CoilMapRepo _l25CoilMapRepo;
        private L2L25_CoilMapRepo _l25CoilMapHisRepo;

        public TrackMapProLogic()
        {
            _coilMapRepo = new CoilMapRepo(DBParaDef.DBConn);
            _coilScheduleRepo = new ProductionScheduleRepo(DBParaDef.DBConn);

            _l25CoilMapRepo = new L2L25_CoilMapRepo(DBParaDef.Level2_5DBConn);
            _l25CoilMapHisRepo = new L2L25_CoilMapRepo(DBParaDef.HisDBConn);
        }

        /// <summary>
        /// 更新CoilTable
        /// </summary>
        /// <param name="coilMap"></param>
        /// <param name="PositionFlag"></param>
        /// <returns>更新比數</returns>     
        public int UpdateEntryCoilMap(TBL_CoilMap dao)
        {
      
            try
            {
                var updateNum = _coilMapRepo.UpdateEntryMap(dao);
                return updateNum;

            }
            catch 
            {
                throw;

            }            
        }

        public int UpdateLineStatuts(int LineStatus_Entry, int LineStatus_CPL, int LineStatus_Exit)
        {
            try
            {
                var updateNum = _coilMapRepo.UpdateLineStatus(LineStatus_Entry, LineStatus_CPL, LineStatus_Exit);
                return updateNum;
            }
            catch
            {
                throw;
            }
        }
       
        public int UpdateExitCoilMap(TBL_CoilMap dao)
        {


            try
            {
                var updateNum = _coilMapRepo.UpdateExitMap(dao);
                return updateNum;

            }
            catch
            {
                throw;

            }
        }

        public bool VaildHasEntryTopCoilID()
        {
            try
            {
                var coilMap = _coilMapRepo.GetAll().FirstOrDefault();
                return coilMap.Entry_TOP.Replace(" ",string.Empty).Equals(string.Empty) ? false : true;
            }
            catch
            {
                throw;

            }         
        }
     
        public TBL_CoilMap GetCoilMap()
        {
            try
            {
                var coilMap = _coilMapRepo.GetAll().FirstOrDefault();
                return coilMap;
            }
            catch 
            {

                throw;
            }
          
        }

        public bool CreateL25CoilMap(L2L25_CoilMap dao)
        {
            try
            {
                return _l25CoilMapRepo.Insert(dao)>0;
            }
            catch
            {
                throw;
            }
        }


        public bool CreateL25CoilMapHis(L2L25_CoilMap dao)
        {
            try
            {
                return _l25CoilMapHisRepo.Insert(dao) > 0;
            }
            catch
            {
                throw;
            }
        }
    }
}
