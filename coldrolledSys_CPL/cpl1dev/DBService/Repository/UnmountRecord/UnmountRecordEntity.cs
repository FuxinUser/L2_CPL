using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.UnmountRecord
{
    public class UnmountRecordEntity
    {
        /// <summary>
        /// 斷帶鋼捲記錄
        /// </summary>
        public class TBL_UnmountRecord : BaseRepositoryModel
        {
            /// <summary>
            /// 鋼捲編號
            /// </summary>
            [PrimaryKey]
            public string Coil_ID { get; set; }

            /// <summary>
            /// 原PDI的出口鋼卷號
            /// </summary>
            public string OriPDI_Out_Coil_ID { get; set; }
            /// <summary>
            /// 鋼捲重量
            /// </summary>
            public float CoilWeight { get; set; }
            /// <summary>
            /// 鋼捲長度
            /// </summary>
            public float CoilLength { get; set; }
            /// <summary>
            /// 鋼捲外徑
            /// </summary>
            public float Diameter { get; set; }
            /// <summary>
            /// 鋼捲內徑
            /// </summary>
            public float CoiInsideDiam { get; set; }
            /// <summary>
            /// 鋼捲寬度
            /// </summary>
            public float Width { get; set; }
            /// <summary>
            /// 建立日期
            /// </summary>
            public override DateTime CreateTime { get; set; }

        }
    }
}
