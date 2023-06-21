using Core.Help;
using static DBService.Repository.CoilScheduleEntity;
using static DBService.Repository.PDI.CoilPDIEntity;
using static DBService.Repository.SystemSetting.SystemSettingEntity;

namespace DBService
{
    public class DBColumnDef
    {
        public static string SysNum = IniSystemHelper.Instance.LineNo;

        // Parameter Def
        public static string SysParaGroup = "CPL"+ SysNum;
        public const string SysParaAutoInputFlag = "AutoCoilFeed";
        public const string SysParaAutoPrintFlag = "AutoPrint";
        public const string SysParaL1AliveLastTime = "L1_ALIVE_LastTime";
        public const string SysTopScheduleLock = "TopScheduleLock";
        public const string LoadCoilTime = "LoadCoilTime";
        public const string LoadCoilTimePre = "LoadCoilTimePre";

        // L3L2_Production_Schedule
        public const string SchedTbl = nameof(TBL_Production_Schedule);
        public const string SchedCoilID = nameof(TBL_Production_Schedule.Coil_ID);
        public const string SchedSeqNo = nameof(TBL_Production_Schedule.Seq_No);
        public const string SchedSeqNoL3 = nameof(TBL_Production_Schedule.Seq_No_L3);
        public const string ScheduleStatus = nameof(TBL_Production_Schedule.Schedule_Status);
        public const string SchedUpdateSource = nameof(TBL_Production_Schedule.Update_Source);
        public const string SchedUpdateTime = nameof(TBL_Production_Schedule.UpdateTime);

        // L3L2_PDI
        public const string PDITbl = nameof(TBL_PDI);
        public const string PDIEntryMatNo = nameof(TBL_PDI.Entry_Coil_ID);
        public const string PDIPlanNo = nameof(TBL_PDI.Plan_No);

        // SystemSetting
        public const string SysParameter = nameof(TBL_SystemSetting.Parameter);

    }
}
