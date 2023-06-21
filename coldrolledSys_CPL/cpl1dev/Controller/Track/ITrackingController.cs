using Core.Define;
using DBService.Repository;
using LogSender;
using MsgStruct;
using static DBService.Repository.CoilMapEntity;
using static MsgStruct.L1L2Rcv;

namespace Controller.Track
{
    public interface ITrackingController
    {
        void SetLog(ILog log);
        bool VaildHasEntryTopCoilID();
        bool UpdateEntryTrackMap(Msg_305_TrackMapEn msg);
        bool UpdateExitTrackMap(Msg_306_TrackMapEx msg);
        TBL_CoilMap GetTrackMap();
        bool UpdateEqupMaint(Msg_309_EquipMaint msg);
        bool Create25CoilMap(TBL_CoilMap dao);
    }
}
