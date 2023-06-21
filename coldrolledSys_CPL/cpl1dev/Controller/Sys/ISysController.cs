using LogSender;
using System;
using static Core.Define.DBParaDef.ConnectionSysDef;
using static DBService.Repository.SystemSetting.SystemParameterEntity;
using static DBService.Repository.SystemSetting.SystemSettingEntity;

namespace Controller.Sys
{
    public interface ISysController
    {
        void UpdateL1LastAliveTime(string time);

        void SetLog(ILog log);

        bool VaildSystemAutoValueOn(string parameterGroup, string parameter, string eventMsg);

        bool UpdateSysValue(string parameterGroup, string parameter, string value);

        bool UpdateSysParam(string name, string value, DateTime dateTime);

        bool UpdateConnectionStatuts(ConnectionType type, string ip, string port, string connectionStatuts);

        bool SaveAPStatusToL25(string name, string status);

        TBL_SystemSetting GetSystemSetting(string group, string param);

        TBL_SystemParameter GetSystemParameter(string name);
    }
}
