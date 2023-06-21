using BLL;
using Core.Define;
using Core.Util;
using DBService;
using DBService.Level25Repository.L2L25_L2APStatus;
using LogSender;
using MsgConvert.EntityFactory;
using System;
using System.Linq;
using static Core.Define.DBParaDef.ConnectionSysDef;
using static DBService.Repository.SystemSetting.SystemParameterEntity;
using static DBService.Repository.SystemSetting.SystemSettingEntity;

namespace Controller.Sys
{
    public class SysController : ISysController
    {
        private SystemSetLogic _sysLogic;

        private ILog _log;

        public SysController()
        {    
            _sysLogic = new SystemSetLogic();
        }

        public void SetLog(ILog log)
        {
            _log = log;
        }

        public void UpdateL1LastAliveTime(string time)
        {
            int updateNum = 0;

            try
            {
                updateNum = _sysLogic.UpdateSysValue(L2SystemDef.CPLGroup, DBColumnDef.SysParaL1AliveLastTime, time);
            }
            catch(Exception e)
            {
                _log.E("更新L1 Alive最後時間失敗", e.Message.CleanInvalidChar());
            }

            if (updateNum > 0)
                _log.I("更新L1 Alive最後時間", $"更新成功，時間為{time}");
            else
                _log.E("更新L1 Alive最後時間", $"更新失敗，時間為{time}");
        }

        public bool VaildSystemAutoValueOn(string parameterGroup, string parameter, string eventMsg)
        {
            parameterGroup.VaildStrNullOrEmpty("parameterGroup", "撈取系統設置失敗");
            parameter.VaildStrNullOrEmpty("parameter", "撈取系統設置失敗");
            eventMsg.VaildStrNullOrEmpty("eventMsg", "撈取系統設置失敗");

            try
            {
                var isOn = _sysLogic.VaildSystemAutoInputOn(parameterGroup, parameter);
                _log.I($"撈取系統設置成功{parameter} => {isOn}", $"{eventMsg}:{isOn}");
                return isOn;
            }
            catch (Exception e)
            {
                throw new Exception("撈取系統設置失敗" + e.ToString().CleanInvalidChar());
            }
        }
     
        public bool UpdateSysValue(string parameterGroup, string parameter, string value)
        {
            try
            {
                _log.I($"更新{parameterGroup}  {parameter} 成功",$"更新Value=>{value}");
                var updateNum = _sysLogic.UpdateSysValue(parameterGroup, parameter, value);
                return updateNum > 0;
            }
            catch (Exception e)
            {
                _log.E($"更新{parameterGroup}  {parameter} 失敗", e.Message.CleanInvalidChar());
                return false;
            }
        }

        public bool UpdateSysParam(string name, string value, DateTime dateTime)
        {
            try
            {
                _log.I($"更新參數{name}成功", $"更新Value=>{value}");
                var updateNum = _sysLogic.UpdateSysParam(name, value, dateTime);
                return updateNum > 0;
            }
            catch (Exception e)
            {
                _log.E($"更新參數{name}失敗", e.Message.CleanInvalidChar());
                return false;
            }
        }

        public bool UpdateConnectionStatuts(ConnectionType type, string ip, string port, string connectionStatuts)
        {
            var tb = EntityFactory.ToTblConnectionStatusEntity(type, ip, port, connectionStatuts);
            int insertOrUpdateNum = 0;

            try
            {
                 insertOrUpdateNum = _sysLogic.UpdateConnectionStatuts(tb);
                _log?.I($"更新連線狀態=>{insertOrUpdateNum > 0}", $"{tb.Connection_From}-{tb.Connection_To} IP:{ip}:{port} Statuts:{connectionStatuts}");

                var entity = type.ToL2L25L1L2DisConnection(connectionStatuts);
                insertOrUpdateNum = _sysLogic.CreateL2DisConnectionRepo(entity);
                _log?.I($"新增L25連線狀態資料庫=>{insertOrUpdateNum > 0}", $"{tb.Connection_From}-{tb.Connection_To} IP:{ip}:{port} Statuts:{connectionStatuts}");
            
                insertOrUpdateNum = _sysLogic.CreateL2DisConnectionRepoHis(entity);
                _log?.I($"新增L25連線狀態歷史資料庫=>{insertOrUpdateNum > 0}", $"{tb.Connection_From}-{tb.Connection_To} IP:{ip}:{port} Statuts:{connectionStatuts}");

                return insertOrUpdateNum > 0;
            }
            catch (Exception e)
            {
                _log?.E($"更新連線狀態失敗", e.Message.CleanInvalidChar());
                return false;
            }

        }

        public bool SaveAPStatusToL25(string name, string status)
        {
            try
            {
                if (_sysLogic.UpdateApStatus(name, status) <= 0)
                    return false;

                var now = DateTime.Now;
                var apStatus = _sysLogic.GetApStatus("AP");
                var l25ApStatus = new L2L25_L2APStatus()
                {
                    Message_Length = "41",
                    Message_Id = "113",
                    Date = now.ToString("yyyyMMdd"),
                    Time = now.ToString("HHmmssff"),
                    SystemAP_1 = apStatus.Where(x => x.Name == nameof(L2L25_L2APStatus.SystemAP_1)).FirstOrDefault()?.Status ?? "0",
                    SystemAP_2 = apStatus.Where(x => x.Name == nameof(L2L25_L2APStatus.SystemAP_2)).FirstOrDefault()?.Status ?? "0",
                    SystemAP_3 = apStatus.Where(x => x.Name == nameof(L2L25_L2APStatus.SystemAP_3)).FirstOrDefault()?.Status ?? "0",
                    SystemAP_4 = apStatus.Where(x => x.Name == nameof(L2L25_L2APStatus.SystemAP_4)).FirstOrDefault()?.Status ?? "0",
                    SystemAP_5 = apStatus.Where(x => x.Name == nameof(L2L25_L2APStatus.SystemAP_5)).FirstOrDefault()?.Status ?? "0",
                    SystemAP_6 = apStatus.Where(x => x.Name == nameof(L2L25_L2APStatus.SystemAP_6)).FirstOrDefault()?.Status ?? "0",
                    SystemAP_7 = apStatus.Where(x => x.Name == nameof(L2L25_L2APStatus.SystemAP_7)).FirstOrDefault()?.Status ?? "0",
                    SystemAP_8 = apStatus.Where(x => x.Name == nameof(L2L25_L2APStatus.SystemAP_8)).FirstOrDefault()?.Status ?? "0",
                    SystemAP_9 = apStatus.Where(x => x.Name == nameof(L2L25_L2APStatus.SystemAP_9)).FirstOrDefault()?.Status ?? "0",
                    SystemAP_10 = apStatus.Where(x => x.Name == nameof(L2L25_L2APStatus.SystemAP_10)).FirstOrDefault()?.Status ?? "0",
                    SystemAP_11 = apStatus.Where(x => x.Name == nameof(L2L25_L2APStatus.SystemAP_11)).FirstOrDefault()?.Status ?? "0",
                    SystemAP_12 = apStatus.Where(x => x.Name == nameof(L2L25_L2APStatus.SystemAP_12)).FirstOrDefault()?.Status ?? "0",
                    SystemAP_13 = apStatus.Where(x => x.Name == nameof(L2L25_L2APStatus.SystemAP_13)).FirstOrDefault()?.Status ?? "0",
                    SystemAP_14 = apStatus.Where(x => x.Name == nameof(L2L25_L2APStatus.SystemAP_14)).FirstOrDefault()?.Status ?? "0",
                    SystemAP_15 = apStatus.Where(x => x.Name == nameof(L2L25_L2APStatus.SystemAP_15)).FirstOrDefault()?.Status ?? "0",
                    SystemAP_16 = apStatus.Where(x => x.Name == nameof(L2L25_L2APStatus.SystemAP_16)).FirstOrDefault()?.Status ?? "0"
                };

                var result = _sysLogic.CreateL25APStatus(l25ApStatus);

                return result > 0;
            }
            //catch (Exception e)
            //{
            //    return false;
            //};
            catch
            {
                throw;
            };
        }

        public TBL_SystemSetting GetSystemSetting(string group, string param)
        {
            try
            {
                var tb = _sysLogic.GetSystemSetting(group, param);

                if (tb == null)
                    _log.E("撈取SystemSetting參數失敗", $"group={group},param={param}");

                return tb;
            }
            catch
            {
                throw;
            };
        }

        public TBL_SystemParameter GetSystemParameter(string name)
        {
            try
            {
                var tb = _sysLogic.GetSystemParameter(name);

                if (tb == null)
                    _log.E("撈取SystemParameter參數失敗", $"name={name}");

                return tb;
            }
            catch
            {
                throw;
            };
        }
    }
}
