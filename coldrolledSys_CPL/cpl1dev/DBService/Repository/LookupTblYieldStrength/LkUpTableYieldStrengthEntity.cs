using DBService.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBService.Repository.LookupTblYieldStrength
{
    public class LkUpTableYieldStrengthEntity
    {
        public class TBL_LookupTable_YieldStrength : BaseRepositoryModel
        {
            public string Steel_Grade { get; set; } = "";
            public float YS { get; set; } = 0.0f;
          
            public override DateTime UpdateTime { get; set; }
        }
    }
}
