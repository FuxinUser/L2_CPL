using System;

namespace DataMod.Common
{
    [Serializable]
    public class ProcessCTModel
    {
        public string TotalLength { get; set; } = string.Empty;
        public int DataCnt { get; set; } = 0;
        public string SpeedStr { get; set; } = string.Empty;
        public string PorTensionActStr { get; set; } = string.Empty;
        public string PorTensionRefStr { get; set; } = string.Empty;
        public string PorCurrentStr { get; set; } = string.Empty;
        public string TrTensionActStr { get; set; } = string.Empty;
        public string TrTensionRefStr { get; set; } = string.Empty;
        public string TrCurrentStr { get; set; } = string.Empty;

        public float TR_Avg_Tenstion { get; set; } = 0;
    }

    [Serializable]
    public class ProcessCTWeldModel
    {
        public int DataCnt { get; set; } = 0;
        public int PassNo { get; set; } = 0;
        public string WeldSpeedActStr { get; set; } = string.Empty;
        public string WeldCurrentActFStr { get; set; } = string.Empty;
        public string WeldCurrentActRStr { get; set; } = string.Empty;
        public string WeldPlanishWFActStr { get; set; } = string.Empty;
        public string WeldTempertureStr { get; set; } = string.Empty;
    }
}
