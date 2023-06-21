using Core.Define;
using Core.Util;
using DataMod.WMS.LogicModel;
using DBService.Repository.LookupTbSleeve;
using MsgStruct;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static DataMod.Response.RespnseModel;
using static DBService.Repository.CoilMapEntity;

namespace MsgConvert
{
    public static class WMSMsgFactory
    {
        public static L2_WMS_Snd.PWx1_ScheduleList PWX1ScheduleMsg(this List<string> coilsOfTable)
        {
            var coilIDStr = string.Join("", coilsOfTable.ToArray());

            var pwx1 = new L2_WMS_Snd.PWx1_ScheduleList()
            {
                //Header
                MessageID = WMSSysDef.RcvMsgCode.WMSCoilScheduleInfo.ToCByteArray(4),
                Length = Marshal.SizeOf<L2_WMS_Snd.PWx1_ScheduleList>().ToString("0000").ToCByteArray(4),
                ProcessDateTime = DateTime.Now.ToString("yyyyMMddHHmmss").ToCByteArray(14),
                IDOfDestinationComputer = WMSSysDef.WMS.ToCByteArray(2),
                IDOfSourceComputer = L2SystemDef.L2.ToCByteArray(2),

                //Data
                Count = coilsOfTable.Count.ToString().ToCByteArray(4),
                CoilNo = coilIDStr.ToCByteArray(6000),
                Spare = " ".ToCByteArray(10),
            };

            return pwx1;
        }

        public static L2_WMS_Snd.PWx2_TrackingMap PWX2TrackInfo(this TBL_CoilMap trkInfo)
        {
            var pwx2 = new L2_WMS_Snd.PWx2_TrackingMap
            {
                //Header
                MessageID = WMSSysDef.RcvMsgCode.WMSEntryDeliveryTrk.ToCByteArray(4),
                Length = Marshal.SizeOf<L2_WMS_Snd.PWx2_TrackingMap>().ToString("0000").ToCByteArray(4),
                ProcessDateTime = DateTime.Now.ToString("yyyyMMddHHmmss").ToCByteArray(14),
                IDOfDestinationComputer = WMSSysDef.WMS.ToCByteArray(2),
                IDOfSourceComputer = L2SystemDef.L2.ToCByteArray(2),


                sk11 = trkInfo.Entry_SK01.Trim().ToCByteArray(20),
                sk12 = trkInfo.Entry_SK02.ToCByteArray(20),
                sk13 = trkInfo.Entry_TOP.ToCByteArray(20),
                TopSensor1 = string.Empty.ToCByteArray(1),
                EntryExit1 = WMSSysDef.SkPos.Wx02TrkEntry.ToCByteArray(1),

                sk21 = trkInfo.Delivery_SK01.ToCByteArray(20),
                sk22 = trkInfo.Delivery_SK02.ToCByteArray(20),
                sk23 = trkInfo.Delivery_TOP.ToCByteArray(20),
                TopSensor2 = string.Empty.ToCByteArray(1),
                EntryExit2 = WMSSysDef.SkPos.Wx02TrkDelivery.ToCByteArray(1),

                sk31 = string.Empty.ToCByteArray(20),
                sk32 = string.Empty.ToCByteArray(20),
                sk33 = string.Empty.ToCByteArray(20),
                TopSensor3 = string.Empty.ToCByteArray(1),
                EntryExit3 = WMSSysDef.SkPos.Wx02TrkNoUse.ToCByteArray(1),

                LineSatus = string.Empty.ToCByteArray(1),
                Spare = string.Empty.ToCByteArray(8),

            };

            return pwx2;
        }

        public static L2_WMS_Snd.PWx5_FeedingRequest_EntryExitReturn PWX5ReqMsg(this ProdLineCoilReq prodLineCoilReq)
        {
            var pwx5 = new L2_WMS_Snd.PWx5_FeedingRequest_EntryExitReturn()
            {
                //Header
                MessageID = WMSSysDef.RcvMsgCode.WMSProdLineCoilReq.ToCByteArray(4),
                Length = Marshal.SizeOf<L2_WMS_Snd.PWx5_FeedingRequest_EntryExitReturn>().ToString("0000").ToCByteArray(4),
                ProcessDateTime = DateTime.Now.ToString("yyyyMMddHHmmss").ToCByteArray(14),
                IDOfDestinationComputer = WMSSysDef.WMS.ToCByteArray(2),
                IDOfSourceComputer = L2SystemDef.L2.ToCByteArray(2),
                //Data
                Flag = prodLineCoilReq.Flag.ToCByteArray(1),
                SKIDNo = prodLineCoilReq.Pos.ToCByteArray(1),   // 待修正
                CoilNo = prodLineCoilReq.CoilNo.ToCByteArray(20),
                CoilTurn = prodLineCoilReq.CoilTurn.ToCByteArray(1),
                Spare = prodLineCoilReq.Spare.ToCByteArray(11),
            };

            return pwx5;
        }

        public static L2_WMS_Snd.PWx3_CoilInfo PWX3CoilInfo(this WMSPdoInfomation pdo)
        {

            var gw03 = new L2_WMS_Snd.PWx3_CoilInfo
            {
                //Header
                MessageID = WMSSysDef.RcvMsgCode.WMSCoilPDO.ToCByteArray(4),
                Length = Marshal.SizeOf<L2_WMS_Snd.PWx3_CoilInfo>().ToString("0000").ToCByteArray(4),   //0000 補成四碼
                ProcessDateTime = DateTime.Now.ToString("yyyyMMddHHmmss").ToCByteArray(14),
                IDOfDestinationComputer = WMSSysDef.WMS.ToCByteArray(2),
                IDOfSourceComputer = L2SystemDef.L2.ToCByteArray(2),

                InCoilNo = pdo.In_Mat_No.ToCByteArray(20),
                OutCoilNo = pdo.Out_Mat_No.ToCByteArray(20),
                OrderNo = pdo.OrderNo.ToCByteArray(20),
                PackType = string.Empty.ToCByteArray(5),
                InnerDia = pdo.Out_Mat_Inner.ToString().ToCByteArray(4),
                OuterDia = pdo.Out_Mat_Outer_Diameter.ToString().ToCByteArray(4),
                CoilThick = pdo.Out_Mat_Thick.ToString().ToCByteArray(9),
                CoilWidth = pdo.Out_Mat_Width.ToString().ToCByteArray(7),
                CoilWeight = pdo.Out_Mat_Wt.ToString().ToCByteArray(5),
                //CoilTurn = pdo.Winding_Dire.Equals(DeviceParaDef.WindingDirectionUp) ? WMSSysDef.Cmd.WMSWindingDirectionUp.ToCByteArray(1) : WMSSysDef.Cmd.WMSWindingDirectionDown.ToCByteArray(1),
                CoilTurn = "4".ToCByteArray(1),     //  固定下收 (4=下收帶尾朝南)
                CoilContainsOil = pdo.Oil_Flag.ToCByteArray(1),
                Next_WholeBackLog_Code = "CR".ToCByteArray(2),
                Sleeve_Inner_Dia = pdo.Sleeve_Inner_Dia.ToCByteArray(4),
                Sleeve_Thick = pdo.Sleeve_Thick.ToCByteArray(9),
                Sleeve_Width = pdo.Sleeve_Width.ToCByteArray(7),
                Spare = string.Empty.ToCByteArray(16),
            };

            return gw03;
        }

        public static L2_WMS_Snd.PWx6_ScanCoil PW06ScanCoil(int skIDNo, string coilNo)
        {
            var pw06 = new L2_WMS_Snd.PWx6_ScanCoil();

            pw06.MessageID = WMSSysDef.RcvMsgCode.WMSInfoScanID.ToCByteArray(4);
            pw06.Length = Marshal.SizeOf<L2_WMS_Snd.PWx6_ScanCoil>().ToString("0000").ToCByteArray(4);
            pw06.ProcessDateTime = DateTime.Now.ToString("yyyyMMddHHmmss").ToCByteArray(14);
            pw06.IDOfDestinationComputer = WMSSysDef.WMS.ToCByteArray(2);
            pw06.IDOfSourceComputer = L2SystemDef.L2.ToCByteArray(2);


            pw06.SkidNo = skIDNo.ToNByteArray(1);
            pw06.CoilNo = coilNo.ToCByteArray(20);
            pw06.Spare = string.Empty.ToCByteArray(13);

            return pw06;

        }
        public static WMSPdoInfomation ConvertWMSPdoInfo(this PDO pdo, LkUpTableSleeveEntity.TBL_LookupTable_Sleeve sleeve)
        {
            var pdoInfo = new WMSPdoInfomation
            {
                Out_Mat_No = pdo.Out_Coil_ID,
                OrderNo = pdo.OrderNo,
                In_Mat_No = pdo.In_Coil_ID,
                //PackType = pdo.                   //CPL 無包裝                        
                Out_Mat_Inner = pdo.Out_Coil_Inner,
                Out_Mat_Outer_Diameter = pdo.Out_Coil_Outer_Diameter,
                Out_Mat_Thick = pdo.Out_Coil_Thick,
                Out_Mat_Width = pdo.Out_Coil_Width,
                Out_Mat_Wt = (int)pdo.Out_Theory_Wt,
                Winding_Dire = pdo.Winding_Direction,
                Sleeve_Inner_Dia = sleeve == null ? "" : sleeve?.Out_Coil_Inner_Dia.ToString(),
                Sleeve_Thick = sleeve == null ? "" : sleeve?.Sleeve_Thick.ToString(),
                Sleeve_Width = sleeve == null ? "" : sleeve?.Sleeve_Width.ToString()
            };

            return pdoInfo;
        }

    }
}
