using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository
{
    public class CoilRejResultEntity
    {
        /// <summary>
        /// 鋼卷回退實績
        /// </summary>
        [Serializable]
        public class TBL_CoilRejectResult : BaseRepositoryModel
        {
            /// <summary>
            /// 回退鋼卷號
            /// </summary>
            [PrimaryKey]
            public string Reject_Coil_ID { get; set; }
            /// <summary>
            /// 入口卷號
            /// </summary>
            public string Entry_Coil_ID { get; set; }
            /// <summary>
            /// 原PDI的出口鋼卷號
            /// </summary>
            public string OriPDI_Out_Coil_ID { get; set; }
            /// <summary>
            /// 計畫號
            /// </summary>
            public string Plan_No { get; set; }
            /// <summary>
            /// 回退方式
            /// </summary>
            public string Mode_Of_Reject { get; set; }
            /// <summary>
            /// 回退卷長
            /// </summary>
            public string Length_Of_Rejected_Coil { get; set; }
            /// <summary>
            /// 回退卷重
            /// </summary>
            public string Weight_Of_Rejected_Coil { get; set; }
            /// <summary>
            /// 回退卷內徑
            /// </summary>
            public string Inner_Diameter_Of_RejectedCoil { get; set; }
            /// <summary>
            /// 回退卷外徑
            /// </summary>
            public string Outer_Diameter_Of_RejectedCoil { get; set; }


            /// <summary>
            /// 回退原因代碼 01：设备故障 02：入料错误 03：更改作业排程 04：半卷分切
            /// </summary>
            public string Reason_Of_Reject { get; set; }

            /// <summary>
            /// 回退時間
            /// </summary>
            public string Time_Of_Reject { get; set; }

            /// <summary>
            /// 班次
            /// </summary>
            public string Shift_Of_Reject { get; set; }

            /// <summary>
            /// 班別
            /// </summary>
            public string Turn_Of_Reject { get; set; }

            /// <summary>
            /// 墊紙方式
            /// </summary>
            public string Paper_exit_Code { get; set; }

            /// <summary>
            /// 墊紙類型
            /// </summary>
            public string Paper_Type { get; set; }

            /// <summary>
            ///  最后钢卷标记0：非最终分卷 1：最终卷
            /// </summary>
            public string FINAL_COIL_FLAG { get; set; }

            /// <summary>
            /// 头部垫纸长度
            /// </summary>
            public string HEAD_PAPER_LENGTH { get; set; }

            /// <summary>
            /// 头部垫纸宽度
            /// </summary>
            public string HEAD_PAPER_WIDTH { get; set; }

            /// <summary>
            /// 尾部垫纸长度
            /// </summary>
            public string TAIL_PAPER_LENGTH { get; set; }

            /// <summary>
            /// 尾部垫纸宽度
            /// </summary>
            public string TAIL_PAPER_WIDTH { get; set; }
            public string UserID { get; set; }
            public override DateTime CreateTime { get; set; }
        }


       

    }
}
