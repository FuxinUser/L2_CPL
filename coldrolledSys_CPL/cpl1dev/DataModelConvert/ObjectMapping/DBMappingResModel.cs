using AutoMapper;
using static DataMod.Response.RespnseModel;
using static DBService.Repository.CoilRejResultEntity;
using static DBService.Repository.CoilScheduleEntity;
using static DBService.Repository.CutReocrd.CoilCutRecordTempEntity;
using static DBService.Repository.DefectData.CoilDefectDataEntity;
using static DBService.Repository.Leader.LeaderTempEntity;
using static DBService.Repository.LineFaultRecords.LineFaultRecordsEntity;
using static DBService.Repository.PDI.CoilPDIEntity;
using static DBService.Repository.PDO.PDOEntity;
using static DBService.Repository.Sample.SampleEntity;
using static DBService.Repository.ScheduleDelete_CoilReject_Record_Temp.ScheduleDeleteRecordTempEntity;
using static DBService.Repository.WorkSchedule.WorkScheduleEntity;

namespace MsgConvert.ObjectMapping
{

    /// <summary>
    /// 物件映至: DB Model Map to Response Model
    /// </summary>
    public class DBMappingResModel : Profile
    {
 

        public  DBMappingResModel() {
            
            // DBModel-> Response Model
            CreateMap<TBL_ScheduleDelete_CoilReject_Temp, ScheduleDeleteCoilRejectTempInfo>();
            CreateMap<TBL_ScheduleDelete_CoilReject_Temp, ReturnCoilInfo>();
            CreateMap<TBL_Coil_Defect, DefectData>();
            CreateMap<TBL_PDI, PDI>();
            CreateMap<TBL_Production_Schedule, CoilSchedule>();
            CreateMap<TBL_PDO, PDO>();
            CreateMap<TBL_Sample, SampleCoil>();
            CreateMap<TBL_Coil_CutRecord_Temp, CoilCutRecordTemp>();
            CreateMap<TBL_WorkSchedule, WorkSchedule>();
            CreateMap<TBL_Leader_Temp, LedaerData>();
            CreateMap<TBL_LineFaultRecords, LineFaultRecord > ();

            //Req -> DBModel 
            CreateMap<ReturnCoilInfo, TBL_CoilRejectResult>()
                 .ForMember(dest => dest.Reject_Coil_ID, opt => opt.MapFrom(src => src.Coil_ID));
            CreateMap<PDO, TBL_PDO>();
            CreateMap<PDI, TBL_Sample>();
            
            
        }

    }
}
