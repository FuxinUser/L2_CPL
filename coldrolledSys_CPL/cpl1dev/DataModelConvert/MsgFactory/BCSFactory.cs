using Core.Define;
using DataMod.BarCode.Msg;
using System;

namespace MsgConvert.MsgFactory
{
    public class BCSFactory
    {
        public static BCSModel.CompareScnResult_SB01 ScanResult(bool compareOK, string coilNo, bool parsingSuccess = true)
        {

            var msg = new BCSModel.CompareScnResult_SB01()
            {
                Header = new BCSModel.BCSHeader
                {
                    Message_Id = DeviceParaDef.CompareScanResultId.PadLeft(4, '0').ToCharArray(),
                    Message_Length = DeviceParaDef.ScanResultLength.PadLeft(4, '0').ToCharArray(),
                    Message_DateTime = DateTime.Now.ToString("yyyyMMddHHmmss").ToCharArray()
                },

                Result = compareOK ? "1".ToCharArray() : "2".ToCharArray(),
                CoilNo = coilNo.PadLeft(14, ' ').ToCharArray()
            };

            if (!parsingSuccess) msg.Result = "3".ToCharArray();

            return msg;
        }
    }
}
