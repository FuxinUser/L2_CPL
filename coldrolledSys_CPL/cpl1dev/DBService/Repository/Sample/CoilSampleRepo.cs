using DBService.Base;
using static DBService.Repository.Sample.SampleEntity;

namespace DBService.Repository.Sample
{
    public class CoilSampleRepo : BaseRepository<TBL_Sample>
    {
        protected override string TableName => nameof(TBL_Sample);

        protected override string[] PKName => new string[] { nameof(TBL_Sample.Plan_No)
                                                           , nameof(TBL_Sample.Mat_Seq_No)
                                                           , nameof(TBL_Sample.Plan_Sort)
                                                           , nameof(TBL_Sample.Sample_ID)};

        public CoilSampleRepo(string connStr) : base(connStr)
        {



        }

    }
}
