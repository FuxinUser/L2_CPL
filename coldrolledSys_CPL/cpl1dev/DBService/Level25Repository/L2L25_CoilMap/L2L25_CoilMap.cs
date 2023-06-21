using DBService.Base;

namespace DBService.Level25Repository.L2L25_CoilMap
{
    public class L2L25_CoilMap : BaseRepositoryModel
    {
		public string Message_Length { get; set; }
		public string Message_Id { get; set; }
		public string Date { get; set; }
		public string Time { get; set; }
		public string CoilIDUnCoiler { get; set; }
		public string CoilIDUnSkid1 { get; set; }
		public string CoilIDUnSkid2 { get; set; }
		public string CoilIDUnTop { get; set; }
		public string CoilIDCarEntry { get; set; }
		public string EntryTOPPhotoSensor { get; set; }
		public string CoilIDReCoiler { get; set; }
		public string CoilIDRecSkid1 { get; set; }
		public string CoilIDRecSkid2 { get; set; }
		public string CoilIDRecTop { get; set; }
		public string CoilIDCarExit { get; set; }
		public string ExitTOPPhotoSensor { get; set; }
	}
}
