using DBService.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBService.Repository.LookupTblSideTrimmer1
{
    public class LkUpTableSideTrimmer1Entity
    {
        public class TBL_LookupTable_SideTrimmer1 : BaseRepositoryModel
        {           
            public float YS_Min { get; set; } = 0.0f;
            public float YS_Max { get; set; } = 0.0f;
            public float Coil_Thickness_Min { get; set; } = 0.0f;
            public float Coil_Thickness_Max { get; set; } = 0.0f;
            public float KnifeGap { get; set; } = 0.0f;
            public float KnifeLap { get; set; } = 0.0f;
            public override DateTime UpdateTime { get; set; }
        }

    }
}

