using DBService.Base;

namespace DBService.Repository.PresetRecord
{
    public class PresetRecordEntity
    {
        public class TBL_PresetRecord : BaseRepositoryModel
		{
			public string Coil_ID { get; set; }
			public string OriPDI_Out_Coil_ID { get; set; }
			public string SteelGrade { get; set; }
			public float Thickness { get; set; }
			public float Width { get; set; }
			public float EntryYieldStress { get; set; }
			public float Density { get; set; }
			public float CoilLength { get; set; }
			public float CoilWeight { get; set; }
			public string ProcessCode { get; set; }
			public float InnerDiam { get; set; }
			public float Diameter { get; set; }
			public int SleeveCodeEntry { get; set; }
			public float SleeveDmEntry { get; set; }
			public int PaperWinderFlag { get; set; }
			public int SleeveCodeExit { get; set; }
			public float SleeveDmExit { get; set; }
			public int PaperTypeExit { get; set; }
			public int PaperCodeExit { get; set; }
			public float FlatenerDepth1 { get; set; }
			public float FlatenerDepth2 { get; set; }
			public float UncoilerTension { get; set; }
			public float UncoilerTensionMax { get; set; }
			public float UncoilerTensionMin { get; set; }
			public float HeadLeaderStripLength { get; set; }
			public float HeadLeaderStripThickness { get; set; }
			public float HeadLeaderStripWidth { get; set; }
			public int HeadLeaderStripSteelGrade { get; set; }
			public float TailLeaderStripLength { get; set; }
			public float TailLeaderStripThickness { get; set; }
			public float TailLeaderStripWidth { get; set; }
			public int TailLeaderStripSteelGrade { get; set; }
			public float SideTrimmerGap { get; set; }
			public float SideTrimmerLap { get; set; }
			public float SideTrimmerWidth { get; set; }
			public float TensionUnitDepth { get; set; }
			public float RecoilerTension { get; set; }
			public float RecoilerTensionMax { get; set; }
			public float RecoilerTensionMin { get; set; }
			public int PaperUnwinderFlag { get; set; }
			public int CoilSplit { get; set; }
			public float Orderwt_1 { get; set; }
			public float Orderwt_2 { get; set; }
			public float Orderwt_3 { get; set; }
			public float Orderwt_4 { get; set; }
			public float Orderwt_5 { get; set; }
			public float Orderwt_6 { get; set; }
			public int PrrPosId { get; set; }
			public System.DateTime PresetTime { get; set; }
		}

    }
}
