using DBService.Repository.EventLog;
using System;
using System.Configuration;

namespace CPL1HMI
{
    public class Frm_5_1_SqlFactory
    {

        #region --- Display ---

        /// <summary>
        /// 搜尋TBL_EventLog (Server)
        /// </summary>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public static string SQL_Select_ServerEventLog(string StartTime, string EndTime, int DataCount)
        {
            string strSql = $@"Select Top ({DataCount})
                               [{nameof(EventLogEntity.TBL_EventLog.FrameGroup_No)}],
                               [{nameof(EventLogEntity.TBL_EventLog.Event_Type)}],
                               [{nameof(EventLogEntity.TBL_EventLog.Event_Description)}],
                               [{nameof(EventLogEntity.TBL_EventLog.Command)}],
                               CONVERT(varchar,[{nameof(EventLogEntity.TBL_EventLog.CreateTime)}],121)CreateTime 
                               FROM [{nameof(EventLogEntity.TBL_EventLog)}] 
                               Where [{nameof(EventLogEntity.TBL_EventLog.CreateTime)}] between '{StartTime}:00:00' and '{EndTime}:59:59'";

            //事件類別
            if (PublicForms.EventLog.Chk_EventType.Checked)
            {
                strSql += $" And [{nameof(EventLogEntity.TBL_EventLog.Event_Type)}] = '{PublicForms.EventLog.Cob_EventType.SelectedValue }'";
            }

            //電腦名稱
            if (PublicForms.EventLog.Chk_ComputerName.Checked)
            {
                strSql += $" And [{nameof(EventLogEntity.TBL_EventLog.FrameGroup_No)}] = '{PublicForms.EventLog.Cob_ComputerName.Text}'";
            }

            //關鍵字
            if (PublicForms.EventLog.Chk_Keyword.Checked)
            {
                strSql += $" And [{nameof(EventLogEntity.TBL_EventLog.Command)}] like '%{PublicForms.EventLog.Txt_Keyword.Text}%'";
            }

            strSql += $" Order by [{nameof(EventLogEntity.TBL_EventLog.CreateTime)}] asc";

            return strSql;
        }


        /// <summary>
        /// 搜尋TBL_EventLog_Client
        /// </summary>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public static string SQL_Select_ClientEventLog(string StartTime, string EndTime, int DataCount)
        {
            string strSql = $@"Select Top ({DataCount})
                               [{nameof(EventLogClientEntity.TBL_EventLog_Client.FrameGroup_No)}],
                               [{nameof(EventLogClientEntity.TBL_EventLog_Client.Event_Type)}],
                               [{nameof(EventLogClientEntity.TBL_EventLog_Client.Event_Description)}],
                               [{nameof(EventLogClientEntity.TBL_EventLog_Client.Command)}],
                               CONVERT(varchar,[{nameof(EventLogClientEntity.TBL_EventLog_Client.CreateTime)}],121)CreateTime 
                               FROM [{nameof(EventLogClientEntity.TBL_EventLog_Client)}] 
                               Where [{nameof(EventLogClientEntity.TBL_EventLog_Client.CreateTime)}] between '{StartTime}:00:00' and '{EndTime}:59:59'";

            //事件類別
            if (PublicForms.EventLog.Chk_EventType.Checked)
            {
                strSql += $" And [{nameof(EventLogClientEntity.TBL_EventLog_Client.Event_Type)}] = '{PublicForms.EventLog.Cob_EventType.SelectedValue }'";
            }

            //電腦名稱
            if (PublicForms.EventLog.Chk_ComputerName.Checked)
            {
                strSql += $" And [{nameof(EventLogClientEntity.TBL_EventLog_Client.FrameGroup_No)}] = '{PublicForms.EventLog.Cob_ComputerName.Text}'";
            }

            //關鍵字
            if (PublicForms.EventLog.Chk_Keyword.Checked)
            {
                strSql += $" And [{nameof(EventLogClientEntity.TBL_EventLog_Client.Command)}] like '%{PublicForms.EventLog.Txt_Keyword.Text}%'";
            }

            strSql += $" Order by [{nameof(EventLogClientEntity.TBL_EventLog_Client.CreateTime)}] asc";

            return strSql;
        }


        #endregion


        #region --- ComboBoxItems ---

        /// <summary>
        /// 電腦名稱下拉式選單
        /// </summary>
        /// <param name="intType">0:Server ; 1:Client </param>
        /// <returns>strSql</returns>
        public static string SQL_Select_FrameGroupComboBoxItems(int intType)
        {
            string strSql = "";
            if (intType == 0)
            {
                 strSql = $@"Select  distinct [{nameof(EventLogEntity.TBL_EventLog.FrameGroup_No)}] From [{nameof(EventLogEntity.TBL_EventLog)}]";
            }
            else if(intType == 1)
            {
                strSql = $@"Select  distinct [{nameof(EventLogClientEntity.TBL_EventLog_Client.FrameGroup_No)}] From [{nameof(EventLogClientEntity.TBL_EventLog_Client)}]";
            }

            return strSql;
        }

        #endregion


        #region --- EventLogClient ---

        /// <summary>
        /// 新增EventLog_Client
        /// </summary>
        /// <param name="Frame_No"></param>
        /// <param name="EventType"></param>
        /// <param name="Event_Description"></param>
        /// <param name="Command"></param>
        /// <returns></returns>
        public static string SQL_Insert_EventLog_Client(string Frame_No, Event_Type EventType, string Event_Description, string Command)
        {
            string strSql = $@"Insert into [{nameof(EventLogClientEntity.TBL_EventLog_Client)}]
                                         ( [{nameof(EventLogClientEntity.TBL_EventLog_Client.FrameGroup_No)}],
                                           [{nameof(EventLogClientEntity.TBL_EventLog_Client.Client_IP)}],
                                           [{nameof(EventLogClientEntity.TBL_EventLog_Client.Frame_No)}],
                                           [{nameof(EventLogClientEntity.TBL_EventLog_Client.Event_Type)}],
                                           [{nameof(EventLogClientEntity.TBL_EventLog_Client.Event_Description)}],
                                           [{nameof(EventLogClientEntity.TBL_EventLog_Client.Command)}],
                                           [{nameof(EventLogClientEntity.TBL_EventLog_Client.CreateTime)}])
                                   Values ( '{ConfigurationManager.AppSettings["PC_Name"]}',
                                           '{PublicForms.Main.Lbl_UserIP.Text}',
                                           '{Frame_No}',
                                           '{(int)EventType}',
                                           N'{Event_Description}',
                                           N'{Command}',
                                           '{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";


            return strSql;
        }

        #endregion


        #region --- 保留 ---

        public static string Frm_5_1_SelectEvent_DB_TBL_EventLog()
        {
            string strSql = $@"Select TOP 50 [{nameof(EventLogEntity.TBL_EventLog.System_ID)}],
                               [{nameof(EventLogEntity.TBL_EventLog.Function_Block)}],
                               [{nameof(EventLogEntity.TBL_EventLog.FrameGroup_No)}],
                               [{nameof(EventLogEntity.TBL_EventLog.Frame_No)}] ,
                               [{nameof(EventLogEntity.TBL_EventLog.Event_Type)}],
                               [{nameof(EventLogEntity.TBL_EventLog.Event_Description)}],
                               [{nameof(EventLogEntity.TBL_EventLog.Command)}],
                               CONVERT(varchar,[{nameof(EventLogEntity.TBL_EventLog.CreateTime)}],121)CreateTime 
                               FROM [{nameof(EventLogEntity.TBL_EventLog)}] 
                               Where [{nameof(EventLogEntity.TBL_EventLog.Event_Type)}] = '3' 
                               Order by [{nameof(EventLogEntity.TBL_EventLog.CreateTime)}] desc";

            return strSql;
        }

        #endregion

    }
}
