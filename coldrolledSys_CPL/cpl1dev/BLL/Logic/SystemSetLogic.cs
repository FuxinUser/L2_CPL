using Core.Define;
using DBService.Level25Repository.L2L25_L1L2DisConnection;
using DBService.Level25Repository.L2L25_L2APStatus;
using DBService.Repository.APStatus;
using DBService.Repository.ConnectionStatus;
using DBService.Repository.SystemSetting;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using static DBService.Repository.APStatus.APStatusEntity;
using static DBService.Repository.ConnectionStatus.ConnectionStatusEntity;
using static DBService.Repository.SystemSetting.SystemParameterEntity;
using static DBService.Repository.SystemSetting.SystemSettingEntity;

namespace BLL
{
    public class SystemSetLogic
    {
        private SystemParameterRepo _systemParamterRepo;
        private SystemSettingRepo _systemSettingRepo;
        private ConnectionStatusRepo _connectionStatusRepo;
        private APStatusRepo _apStatusRepo;

        private L2L25_L1L2DisConnectionRepo _l2l25_l2DisConnectionRepo;
        private L2L25_L1L2DisConnectionRepo _l2l25_l2DisConnectionHisRepo;

        private L2L25_L2APStatusRepo _l2l25_L2APStatusRepo;
        private L2L25_L2APStatusRepo _l2l25_L2APStatusHisRepo;

        public SystemSetLogic()
        {
            _systemParamterRepo = new SystemParameterRepo(DBParaDef.DBConn);
            _systemSettingRepo = new SystemSettingRepo(DBParaDef.DBConn);
            _connectionStatusRepo = new ConnectionStatusRepo(DBParaDef.DBConn);
            _apStatusRepo = new APStatusRepo(DBParaDef.DBConn);

            _l2l25_l2DisConnectionRepo = new L2L25_L1L2DisConnectionRepo(DBParaDef.Level2_5DBConn);
            _l2l25_l2DisConnectionHisRepo = new L2L25_L1L2DisConnectionRepo(DBParaDef.HisDBConn);

            _l2l25_L2APStatusRepo = new L2L25_L2APStatusRepo(DBParaDef.Level2_5DBConn);
            _l2l25_L2APStatusHisRepo = new L2L25_L2APStatusRepo(DBParaDef.HisDBConn);
        }

        public string GetSysAutoFlag(string parameterGroup)
        {
            try
            {
                var setting = _systemSettingRepo.GetAll(parameterGroup).FirstOrDefault();
                return setting.Value;
            }
            catch 
            {
                throw;

            }

        }
        public bool VaildSystemAutoInputOn(string parameterGroup, string parameter)
        {
          
            try
            {
                var setting = _systemSettingRepo.Get(new string[] { parameterGroup, parameter });

                return setting.Value.Equals(L2SystemDef.AutoInputOn);
         

            }
            catch
            {
                throw;

            }

           
        }
        public int UpdateSysValue(string parameterGroup, string parameter, string value)
        {

            var sysSet = new SystemSettingEntity.TBL_SystemSetting
            {
                Value = value,
            };
            try
            {
               return  _systemSettingRepo.Update(sysSet, new string[] { parameterGroup, parameter });
            }
            catch
            {
                throw;
            }

        }

        public int UpdateSysParam(string name, string value, DateTime dateTime)
        {
            var sysParam = new TBL_SystemParameter
            {
                Value = value,
                ValueDate = dateTime
            };
            try
            {
                return _systemParamterRepo.Update(sysParam, new string[] { name });
            }
            catch
            {
                throw;
            }
        }

        public int UpdateConnectionStatuts(TBL_ConnectionStatus entity)
        {
            try
            {
                entity.Create_DateTime = DateTime.Now;
               return  _connectionStatusRepo.Update(entity, new string[] { entity.Connection_From, entity.Connection_To});
            }
            catch
            {
                throw;
            }
        }

        public int CreateL2DisConnectionRepo(L2L25_L1L2DisConnection entity)
        {
            try
            {

                return _l2l25_l2DisConnectionRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }


        public int CreateL2DisConnectionRepoHis(L2L25_L1L2DisConnection entity)
        {
            try
            {

                return _l2l25_l2DisConnectionHisRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }

        public int UpdateApStatus(string procName, string status)
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append($"Update {nameof(TBL_APStatus)} ");
                sql.Append($"Set {nameof(TBL_APStatus.Status)}='{status}' ");
                sql.Append($"Where {nameof(TBL_APStatus.GroupName)}='AP' And {nameof(TBL_APStatus.Description)}='{procName}' ");

                return _apStatusRepo.DBContext.Update(sql.ToString());
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<TBL_APStatus> GetApStatus(string groupName)
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append($" Select * From {nameof(TBL_APStatus)} Where {nameof(TBL_APStatus.GroupName)}='AP' ");

                var apStatus = _systemSettingRepo.DBContext.Query<TBL_APStatus>(sql.ToString());
                //var pdis = _coilPDIRepo.GetAll($"{nameof(L3L2_PDI.Plan_No)} = '{planNo}'");
                return apStatus;
            }
            catch
            {
                throw;

            }
        }

        public int CreateL25APStatus(L2L25_L2APStatus tb)
        {
            try
            {
                var result = _l2l25_L2APStatusRepo.Insert(tb);

                if (result > 0)
                    return _l2l25_L2APStatusHisRepo.Insert(tb);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public TBL_SystemSetting GetSystemSetting(string group, string param)
        {
            try
            {
                return _systemSettingRepo.Get(new string[] { group, param });
            }
            catch
            {
                throw;
            }
        }

        public TBL_SystemParameter GetSystemParameter(string name)
        {
            try
            {
                return _systemParamterRepo.Get(new string[] { name });
            }
            catch
            {
                throw;
            }
        }
    }
}
