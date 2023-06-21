using System;

namespace DataMod.BarCode.Msg
{
    [Serializable]
    public class ScanResult
    {
        public string CoilID { get; set; }

        public int SKID { get; set; }

        public ScanResult(int skid, string coilID)
        {
            SKID = skid;
            CoilID = coilID;
        }
    }
}
