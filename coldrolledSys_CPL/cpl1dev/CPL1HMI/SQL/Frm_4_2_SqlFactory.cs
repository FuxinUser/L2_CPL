using DBService.Repository.Utility;
using System;

namespace CPL1HMI
{
    public class Frm_4_2_SqlFactory
    {

        #region --- Display ---

        /// <summary>
        /// 搜尋能源耗用 - 用班別搜尋
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_UnityWithTeam()
        {
            string strSql = $@"Select convert(char(23), [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}], 121) Receive_Time,
                                   [{nameof(UtilityEntity.TBL_Utility.Shift)}],
                                   [{nameof(UtilityEntity.TBL_Utility.Team)}],
                                   [{nameof(UtilityEntity.TBL_Utility.CompressedAir)}],
                                   [{nameof(UtilityEntity.TBL_Utility.IndirectCollingWater)}]
                              From [{nameof(UtilityEntity.TBL_Utility)}]
                             Where [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}] >= '{PublicForms.Utility.Dtp_DateShitf.Value:yyyy-MM-dd}' 
                               And [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}] < '{PublicForms.Utility.Dtp_DateShitf.Value.AddDays(1):yyyy-MM-dd}' 
                               And [{nameof(UtilityEntity.TBL_Utility.Shift)}] = '{PublicForms.Utility.Cob_Shift_S.SelectedValue}'
                             Order By [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}] Desc";

            return strSql;
        }


        /// <summary>
        /// 搜尋能源耗用 - 日期區間
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_UnityWithDate()
        {
            string strSql = $@"Select convert(char(23), [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}], 121) Receive_Time,
                                   [{nameof(UtilityEntity.TBL_Utility.Shift)}],
                                   [{nameof(UtilityEntity.TBL_Utility.Team)}],
                                   [{nameof(UtilityEntity.TBL_Utility.CompressedAir)}],
                                   [{nameof(UtilityEntity.TBL_Utility.IndirectCollingWater)}]
                              From [{nameof(UtilityEntity.TBL_Utility)}]
                             Where [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}] Between '{PublicForms.Utility.Dtp_DateStart.Value:yyyy-MM-dd} 00:00:00' and '{PublicForms.Utility.Dtp_DateEnd.Value:yyyy-MM-dd} 23:59:59'
                             Order By [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}] Desc";

            return strSql;
        }
        #endregion


        #region --- 保留 ---

        /// <summary>
        /// 刪除能源耗用
        /// </summary>
        /// <param name="Dt"></param>
        /// <returns></returns>
        public static string SQL_Delete_Utility(DateTime Dt)
        {
            string strSql = $@"Delete From [{nameof(UtilityEntity.TBL_Utility)}]
                                     Where [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}] = '{Dt:yyyy-MM-dd HH:mm:ss.fff}'
                                       And [{nameof(UtilityEntity.TBL_Utility.Shift)}] = '{PublicForms.Utility.Cob_Shift.SelectedValue}'
                                       And [{nameof(UtilityEntity.TBL_Utility.Team)}] = '{PublicForms.Utility.Cob_Team.SelectedValue}'";

            return strSql;
        }


        /// <summary>
        /// 新增能源耗用
        /// </summary>
        /// <returns></returns>
        public static string SQL_Insert_Utility()
        {
            string strSql = $@"Insert into [{nameof(UtilityEntity.TBL_Utility)}]
                                          ([{nameof(UtilityEntity.TBL_Utility.Receive_Time)}],
                                           [{nameof(UtilityEntity.TBL_Utility.Shift)}],
                                           [{nameof(UtilityEntity.TBL_Utility.Team)}],
                                           [{nameof(UtilityEntity.TBL_Utility.CompressedAir)}],
                                           [{nameof(UtilityEntity.TBL_Utility.IndirectCollingWater)}])
                                    Values
                                          ('{GlobalVariableHandler.Instance.getTime}',
                                           '{PublicForms.Utility.Cob_Shift.SelectedValue}',
                                           '{PublicForms.Utility.Cob_Team.SelectedValue}',
                                           '{PublicForms.Utility.Txt_ComeAir.Text}',
                                           '{PublicForms.Utility.Txt_CoolingWater.Text}')";

            return strSql;
        }


        /// <summary>
        /// 修改能源耗用
        /// </summary>
        /// <param name="Dt"></param>
        /// <returns></returns>
        public static string SQL_Update_Utility(DateTime Dt)
        {
            string strSql = $@"Update [{nameof(UtilityEntity.TBL_Utility)}] Set
                                      [{nameof(UtilityEntity.TBL_Utility.CompressedAir)}] = '{PublicForms.Utility.Txt_ComeAir.Text}',
                                      [{nameof(UtilityEntity.TBL_Utility.Shift)}] = '{PublicForms.Utility.Cob_Shift.SelectedValue}',
                                      [{nameof(UtilityEntity.TBL_Utility.Team)}] = '{PublicForms.Utility.Cob_Team.SelectedValue}',
                                      [{nameof(UtilityEntity.TBL_Utility.IndirectCollingWater)}] = '{PublicForms.Utility.Txt_CoolingWater.Text}'
                                Where [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}] = '{Dt:yyyy-MM-dd HH:mm:ss.fff}'";

            return strSql;
        }


        #endregion

    }
}
