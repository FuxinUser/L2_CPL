using System;

namespace DataMod.LabelPrint
{
    [Serializable]
    public class SampleInfo
    {
        public string CoilID { get; private set; } = string.Empty;

        public string StNo { get; private set; } = string.Empty;

        public float Thick { get; private set; } = default;

        public string SampleNo { get; private set; } = string.Empty;

        public string SamplePos { get; private set; } = string.Empty;

        public SampleInfo(string coilID, string stNo, float thick, string sampleNo, string samplePos)
        {
            CoilID = coilID;
            StNo = stNo;
            Thick = thick;
            SampleNo = sampleNo;
            SamplePos = samplePos;
        }
    }
}
