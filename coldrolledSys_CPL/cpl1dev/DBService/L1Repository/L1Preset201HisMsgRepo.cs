using DBService.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBService.L1Repository
{
    public class L1Preset201HisMsgRepo : BaseRepository<L2L1MsgDBModel.L2L1_201>
    {
        protected override string TableName => nameof(L2L1MsgDBModel.L2L1_201);

        protected override string[] PKName => new string[] { nameof(L2L1MsgDBModel.L2L1_201.CreateTime) };

        public L1Preset201HisMsgRepo(string connStr) : base(connStr)
        {

        }
    }
}
