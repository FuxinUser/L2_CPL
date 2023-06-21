using BLL.Trck;
using Core.Util;
using LogSender;
using MsgConvert.EntityFactory;
using System;
using System.Data.SqlClient;
using static DBService.Repository.CoilMapEntity;
using static MsgStruct.L1L2Rcv;

namespace Controller.Track
{
    public class TrackingController : ITrackingController
    {
     
        private TrackMapProLogic _trakMapPro;

        private ILog _log;

        public TrackingController()
        {      
            _trakMapPro = new TrackMapProLogic();     
        }

     
        public void SetLog(ILog log)
        {
            _log = log;
        }

        public TBL_CoilMap GetTrackMap()
        {
            try
            {
                var coilMap = _trakMapPro.GetCoilMap();
                _log.I("撈取鋼捲追蹤成功", "撈取鋼捲追蹤目前狀態");
                return coilMap;
            }
            catch (Exception e)
            {
                throw new Exception("撈取鋼捲追蹤失敗" + e.ToString().CleanInvalidChar());           
            }
         
        }

        public bool VaildHasEntryTopCoilID()
        {
            try
            {
                var hasCoil = _trakMapPro.VaildHasEntryTopCoilID();
                _log.I("EntryTop是否有鋼捲", $"檢查EntryTop是否有鋼捲:{hasCoil}");
                return hasCoil;
            }
            catch (Exception e)
            {               
                _log.I("EntryTop是否有鋼捲", StringUtil.CleanInvalidChar(e.Message.CleanInvalidChar()));
                return false;
            }
         
        }

        public bool UpdateEntryTrackMap(Msg_305_TrackMapEn msg)
        {
            msg.VaildObjectNull("msg", "更新入口段鋼捲追蹤失敗");
            
            try
            {
                var coilMap = msg.ToTBEntryCoilMapEntity();
                var updateNum = _trakMapPro.UpdateEntryCoilMap(coilMap);
                _log.I("更新入口段鋼捲追蹤成功", $"目前入口段鋼捲資料 Car:{msg.EntryCar}. TOP:{msg.EntryTOP}. SK1:{msg.EntrySK01}. SK2:{msg.EntrySK02}. POR:{msg.POR}");
                return updateNum > 0;
            }
            catch (Exception e)
            {
                _log.E("更新入口段鋼捲追蹤失敗", e.ToString().CleanInvalidChar());
                return false;
            }
        }

        public bool UpdateEqupMaint(Msg_309_EquipMaint msg)
        {
            msg.VaildObjectNull("msg", "更新LineStatus資料失敗");
            
            try
            {
                var updateNum = _trakMapPro.UpdateLineStatuts(msg.StatusEn, msg.StatusCPL, msg.StatusEx);
                var updateOK = updateNum > 0;
                _log.I("更新LineStatus資料", $"更新資料{updateNum}筆成功");
                return updateOK;
            }
            catch (Exception e)
            {
                _log.E("更新LineStatus資料失敗", e.ToString().CleanInvalidChar());
                return false;
            }
        }

        public bool UpdateExitTrackMap(Msg_306_TrackMapEx msg)
        {
            _log.I("更新Delivery CoilMap", $"目前Delivery鋼捲資料 Car:{msg.DeliveryCar}. TOP:{msg.DeliveryTOP}. SK1:{msg.DeliverySK01}. SK2:{msg.DeliverySK02}. TR:{msg.TR}");

            try
            {
                var tbl = msg.ToTBExitCoilMapEntity();
                var updateNum = _trakMapPro.UpdateExitCoilMap(tbl);
                var updateOK = updateNum > 0;
                _log.I("更新Delivery CoilMap", $"更新DeliveryCoilMap:{updateOK}");
                return updateOK;
            }
            catch (SqlException e)
            {
                _log.E("更新Delivery CoilMap失敗", e.ToString().CleanInvalidChar());
                return false;
            }

          
        }


        public bool Create25CoilMap(TBL_CoilMap dao)
        {
           
            try
            {
                var entity = dao.ToL25CoilMapEntity();
                var insertOK = _trakMapPro.CreateL25CoilMap(entity);
                _log.D("將CoilMap新增至L25 CoilMap", $"新增=>{insertOK}");


                 insertOK = _trakMapPro.CreateL25CoilMapHis(entity);
                _log.D("將CoilMap新增至L25 CoilMap歷史資料", $"新增=>{insertOK}");

                return insertOK;
            }
            catch(Exception e)
            {

                _log.E("新增L25 CoilMap 失敗", e.ToString().CleanInvalidChar());
                return false;
            }

        }

    }
}
