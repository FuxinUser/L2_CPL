using DBService.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBService.Repository.LookupTblYieldStrength
{
    public class LkUpTableYieldStrengthRepo : BaseRepository<LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength>
    {
        protected override string TableName => nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength);
        protected override string[] PKName => new string[] { nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength.Steel_Grade) };

        public LkUpTableYieldStrengthRepo(string connStr) : base(connStr)
        {

        }
    }
}
