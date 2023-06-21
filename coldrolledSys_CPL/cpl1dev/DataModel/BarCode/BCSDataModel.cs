using Core.Define;
using System;
using static Core.Define.L2SystemDef;

namespace DataMod.BarCode
{
    public class BCSDataModel
    {
        [Serializable]
        public class BarCodeScnContent
        {
            public string ScanCoilNo { get; set; } 
            public SKPOS ScanPosition { get; set; }

            public BarCodeScnContent()
            {

            }

            public BarCodeScnContent(string coilNo, SKPOS pos)
            {
                ScanCoilNo = coilNo;
                ScanPosition = pos;
            }

            public int GetScanPos()
            {

                if (ScanPosition == SKPOS.Entry_SK01)
                    return WMSSysDef.SkPos.ESk03;

                if (ScanPosition == SKPOS.Entry_SK02)
                    return WMSSysDef.SkPos.ESk02;

                if (ScanPosition == SKPOS.EntryTOP)
                    return WMSSysDef.SkPos.ETop;

                if (ScanPosition == SKPOS.Delivery_SK01)
                    return WMSSysDef.SkPos.DTop;

                if (ScanPosition == SKPOS.Delivery_SK02)
                    return WMSSysDef.SkPos.DSk03;

                return WMSSysDef.SkPos.DTop;
            }

            public string GetScanPosStr()
            {

                if (ScanPosition == SKPOS.Entry_SK01)
                    return "ESK01";

                if (ScanPosition == SKPOS.EntryTOP)
                    return "ETOP";

                if (ScanPosition == SKPOS.POR)
                    return "POR";

                if (ScanPosition == SKPOS.Entry_SK02)
                    return "ESK02";

                return string.Empty;

            }

            public void SetScanPos(string pos)
            {
                switch (pos)
                {
                    case DeviceParaDef.BCSDefPOS_ETOP:
                        ScanPosition = SKPOS.EntryTOP;
                        break;
                    case DeviceParaDef.BCSDefPOS_ESK01:
                        ScanPosition = SKPOS.Entry_SK01;
                        break;
                    case DeviceParaDef.BCSDefPOS_ESK02:
                        ScanPosition = SKPOS.Entry_SK02;
                        break;

                    case DeviceParaDef.BCSDefPOS_DTOP:
                        ScanPosition = SKPOS.DeliveryTop;
                        break;
                    case DeviceParaDef.BCSDefPOS_DSK01:
                        ScanPosition = SKPOS.Delivery_SK01;
                        break;
                    case DeviceParaDef.BCSDefPOS_DSK02:
                        ScanPosition = SKPOS.Delivery_SK01;
                        break;
                }
            }


        }

     

    }
}
