using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.Leader
{
    public class LeaderTempEntity
    {
        [Serializable]
        public class TBL_Leader_Temp : BaseRepositoryModel
        {
            [PrimaryKey]
            public string Coil_ID { get; set; }
            public string OriPDI_Out_Coil_ID { get; set; }
            public string Head_Leader_St_No { get; set; }
            public float Head_Leader_Length { get; set; }
            public float Head_Leader_Width { get; set; }
            public float Head_Leader_Thickness { get; set; }
            public string Tail_Leader_St_No { get; set; }
            public float Tail_Leader_Length { get; set; }
            public float Tail_Leader_Width { get; set; }
            public float Tail_Leader_Thickness { get; set; }
            public string Create_UserID { get; set; }
            public override DateTime CreateTime { get; set; }


            // 尾段導帶體積
            [IgnoreReflction]
            public float  HeadStripVolume
            {
                get
                {
                    return Head_Leader_Length * Head_Leader_Width / 1000 * Head_Leader_Thickness / 1000;
                }
            }
            //頭段導帶體積
            [IgnoreReflction]
            public float TailStripVolume
            {
                get
                {
                    return Tail_Leader_Length * Tail_Leader_Width / 1000 * Tail_Leader_Thickness / 1000;
                }
            }

        }
    }
}
