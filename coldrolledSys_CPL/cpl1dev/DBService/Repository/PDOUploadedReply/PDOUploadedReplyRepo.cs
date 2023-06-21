using DBService.Base;
using System;

namespace DBService.Repository.PDO
{
    public class PDOUploadedReplyRepo : BaseRepository<PDOUploadedReplyEntity.TBL_PDOUploadedReply>
    {
        protected override string TableName => nameof(PDOUploadedReplyEntity.TBL_PDOUploadedReply);

        protected override string[] PKName => throw new NotImplementedException();

        public PDOUploadedReplyRepo(string connStr) : base(connStr)
        {
        }
    }
}
