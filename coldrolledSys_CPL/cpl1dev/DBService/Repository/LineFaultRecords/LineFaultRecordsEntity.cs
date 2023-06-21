using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.LineFaultRecords
{
    public class LineFaultRecordsEntity
    {
        public class TBL_LineFaultRecords : BaseRepositoryModel
        {
            /// <summary>
            /// 機組號
            /// </summary>
            public string unit_code { get; set; }

            /// <summary>
            /// 日期
            /// </summary>
            [PrimaryKey]
            public DateTime prod_time { get; set; }

            /// <summary>
            /// 停機開始時間
            /// </summary>
            [PrimaryKey]
            public DateTime stop_start_time { get; set; }

            /// <summary>
            /// 停機結束時間
            /// </summary>
            public DateTime stop_end_time { get; set; }


            /// <summary>
            /// 班次
            /// </summary>
            public string prod_shift_no { get; set; }

            /// <summary>
            /// 班別
            /// </summary>
            public string prod_shift_group { get; set; }

            /// <summary>
            /// 停機位置
            /// </summary>
            public string delay_location { get; set; }

            /// <summary>
            /// 停機位置描述
            /// </summary>
            public string delay_location_desc { get; set; }

            /// <summary>
            /// 停機持續時間
            /// </summary>
            public string stop_elased_timey { get; set; }

            /// <summary>
            /// 停機類別
            /// </summary>
            public int stop_category { get; set; }

            /// <summary>
            /// 停機原因代碼
            /// </summary>
            public string delay_reason_code { get; set; }

            /// <summary>
            /// 停機原因描述
            /// </summary>
            public string delay_reason_desc { get; set; }

            /// <summary>
            /// 停機原因備註
            /// </summary>
            public string delay_remark { get; set; }

            /// <summary>
            /// 機械部門原因停機時間
            /// </summary>
            public string resp_depart_delay_time_m { get; set; }

            /// <summary>
            /// 電器部門原因停機時間
            /// </summary>
            public string resp_depart_delay_time_e { get; set; }

            /// <summary>
            /// L3原因停機時間
            /// </summary>
            public string resp_depart_delay_time_c { get; set; }

            /// <summary>
            /// 生產部門原因停機時間
            /// </summary>
            public string resp_depart_delay_time_p { get; set; }

            /// <summary>
            /// 正常停機時間
            /// </summary>
            public string resp_depart_delay_time_u { get; set; }

            /// <summary>
            /// 其他部門原因停機時間
            /// </summary>
            public string resp_depart_delay_time_o { get; set; }

            /// <summary>
            /// 換輥原因停機時間
            /// </summary>
            public string resp_depart_delay_time_r { get; set; }

            /// <summary>
            /// 磨輥間原因停機時間
            /// </summary>
            public string resp_depart_delay_time_rs { get; set; }

            /// <summary>
            /// 降速原因
            /// </summary>
            public string deceleration_cause { get; set; }

            /// <summary>
            /// 降速代码
            /// </summary>
            public string deceleration_code { get; set; }

            /// <summary>
            /// 修改日期
            /// </summary>
            public override DateTime UpdateTime { get; set; }

            /// <summary>
            /// 建立日期
            /// </summary>
            public override DateTime CreateTime { get; set; }
            public string UploadMMS { get; set; }


            // Wait HMI Check
            [IgnoreReflction]
            public int Fault_Code { get; set; }
        }

    }
}

