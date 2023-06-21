using DBService.Base;
using System;

namespace DBService.Repository.EventLog
{
    public class EventLogClientEntity
    {

        [Serializable]
        public class TBL_EventLog_Client : BaseRepositoryModel
        { 
            /// <summary>
            /// 電腦名稱
            /// </summary>
            public string FrameGroup_No { get; set; }

            /// <summary>
            /// 電腦IP
            /// </summary>
            public string Client_IP { get; set; }

            /// <summary>
            /// 畫面編號
            /// </summary>
            public string Frame_No { get; set; }

            /// <summary>
            /// 訊息類型 1-Error; 2-Alarm; 3-Info; 4-Debug;
            /// </summary>
            public string Event_Type { get; set; }

            /// <summary>
            /// 事件名稱
            /// </summary>
            public string Event_Description { get; set; }

            /// <summary>
            /// 事件描述
            /// </summary>
            public string Command { get; set; }

            /// <summary>
            /// 建立時間
            /// </summary>
            public override DateTime CreateTime { get; set; }
        }
    }
}
