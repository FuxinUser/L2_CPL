using DBService.Base;

/**
 * Author:ICSC 余士鵬
 * Date:2019/12/24
 * Desc:DB Table
 */

namespace DBService.Repository
{
    public class CoilRejectResultRepo : BaseRepository<CoilRejResultEntity.TBL_CoilRejectResult>
    {

        public CoilRejectResultRepo(string connStr) : base(connStr)
        {

        }

        protected override string TableName => nameof(CoilRejResultEntity.TBL_CoilRejectResult);

        protected override string[] PKName => new string[] { nameof(CoilRejResultEntity.TBL_CoilRejectResult.Reject_Coil_ID) };

        

    }
}
