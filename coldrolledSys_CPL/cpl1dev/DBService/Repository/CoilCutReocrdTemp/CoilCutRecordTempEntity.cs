using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.CutReocrd
{
    public class CoilCutRecordTempEntity
    {
        /// <summary>
        /// 分切暫存紀錄
        /// </summary>
        public class TBL_Coil_CutRecord_Temp : BaseRepositoryModel
        {
            /// <summary>
            /// 鋼卷號
            /// </summary>
            [PrimaryKey]
            public string Coil_ID { get; set; } = "";
            
            /// <summary>
            /// 入口卷號
            /// </summary>
            public string In_Coil_ID { get; set; } = "";

            /// <summary>
            /// 原PDI給定出口捲
            /// </summary>
            public string OriPDI_Out_Coil_ID { get; set; } = "";

            /// <summary>
            /// 1-入口剪刀 2-出口剪刀
            /// </summary>
            public string CutDevice { get; set; } = "";

            /// <summary>
            /// 分切模式 1-出口剪WeldCut 2-出口剪SplitCut 3-出口剪ScrapCut 4-出口剪SampleCut 5-入口剪裁切鋼捲頭部 6-入口剪裁切鋼捲尾部
            /// </summary>
            public string CutMode { get; set; }

            /// <summary>
            /// 切割長度
            /// </summary>
            public float CutLength { get; set; } = 0f;

            /// <summary>
            /// 分切時間
            /// </summary>
            [PrimaryKey]
            public DateTime CutTime { get; set; }

            /// <summary>
            /// 分切鋼卷外徑
            /// </summary>
            public string Coil_OutDiam { get; set; } = "";

            /// <summary>
            /// 分切鋼捲長度
            /// </summary>
            public string Coil_Length { get; set; } = "";

            /// <summary>
            /// 分切鋼卷理論重
            /// </summary>
            public float Coil_CalcWeight { get; set; } = 0;

            /// <summary>
            /// 拆紙機標記
            /// </summary>
            public string Coil_PaperFlag { get; set; } = "";

            public override DateTime CreateTime { get; set; }

                 
        }
    }
}
