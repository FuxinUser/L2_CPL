using DBService.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBService.Repository.MaterialGrade
{
    public class MaterialGradeEntity
    {
		public class TBL_SteelNoToMaterialGrade : BaseRepositoryModel
		{
			public string St_No { get; set; }
			public string Material_Grade { get; set; }
			public override DateTime CreateTime { get; set; }
		}
	}
}
