using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.L1Repository
{
    public class L2L1MsgDBModel
    {
        public class L2L1_201 : BaseRepositoryModel
        {
			public short MessageLength { get; set; }
			public short MessageId { get; set; }
			public int Date { get; set; }
			public int Time { get; set; }
			public System.DateTime DateTime { get; set; }
			public string Coil_ID { get; set; }
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
			public string Defect1Code { get; set; }
			public float Defect1StartPosition { get; set; }
			public float Defect1EndPosition { get; set; }
			public string Defect2Code { get; set; }
			public float Defect2StartPosition { get; set; }
			public float Defect2EndPosition { get; set; }
			public string Defect3Code { get; set; }
			public float Defect3StartPosition { get; set; }
			public float Defect3EndPosition { get; set; }
			public string Defect4Code { get; set; }
			public float Defect4StartPosition { get; set; }
			public float Defect4EndPosition { get; set; }
			public string Defect5Code { get; set; }
			public float Defect5StartPosition { get; set; }
			public float Defect5EndPosition { get; set; }
			public string Defect6Code { get; set; }
			public float Defect6StartPosition { get; set; }
			public float Defect6EndPosition { get; set; }
			public string Defect7Code { get; set; }
			public float Defect7StartPosition { get; set; }
			public float Defect7EndPosition { get; set; }
			public string Defect8Code { get; set; }
			public float Defect8StartPosition { get; set; }
			public float Defect8EndPosition { get; set; }
			public string Defect9Code { get; set; }
			public float Defect9StartPosition { get; set; }
			public float Defect9EndPosition { get; set; }
			public string Defect10Code { get; set; }
			public float Defect10StartPosition { get; set; }
			public float Defect10EndPosition { get; set; }
			[PrimaryKey]
			public override DateTime CreateTime { get; set; }
		}

        public class L2L1_202 : BaseRepositoryModel
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public int Date { get; set; }
            public int Time { get; set; }
            public System.DateTime DateTime { get; set; }
            public string CoilIDUnc { get; set; }
            public string CoilIDUncSk1 { get; set; }
            public string CoilIDUncSk2 { get; set; }
            public string CoilIDUncTop { get; set; }
            public string CoilIDRec { get; set; }
            public string CoilIDRecSk1 { get; set; }
            public string CoilIDRecSk2 { get; set; }
            public string CoilIDRecTop { get; set; }
            public int PrrPosId { get; set; }
            [PrimaryKey]
            public override DateTime CreateTime { get; set; }
        }

        public class L2L1_203 : BaseRepositoryModel
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public int Date { get; set; }
            public int Time { get; set; }
            public DateTime DateTime { get; set; }
            public string CoilID { get; set; }
            [PrimaryKey]
            public override DateTime CreateTime { get; set; }
        }

        public class L2L1_204 : BaseRepositoryModel
        {
            public short MessageLength { get; set; }
            public short MessageId { get; set; }
            public int Date { get; set; }
            public int Time { get; set; }
            public DateTime DateTime { get; set; }
            public int DelPosId { get; set; }
            [PrimaryKey]
            public override DateTime CreateTime { get; set; }
        }

		public class L2L1_205 : BaseRepositoryModel
		{
			public short MessageLength { get; set; }
			public short MessageId { get; set; }
			public int Date { get; set; }
			public int Time { get; set; }
			public DateTime DateTime { get; set; }
			public string CoilID { get; set; }
			[PrimaryKey]
			public override DateTime CreateTime { get; set; }
		}

	}
}
