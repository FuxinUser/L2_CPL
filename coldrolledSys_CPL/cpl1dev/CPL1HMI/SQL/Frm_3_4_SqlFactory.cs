using DBService.Repository.PDO;

namespace CPL1HMI
{
    public class Frm_3_4_SqlFactory
    {
        public static string SQL_Select_CoilList()
        {
            string strSql = $@"Select [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}],
                                      [{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}],
                                      convert(char(19), [{nameof(PDOEntity.TBL_PDO.StartTime)}], 120) StartTime,
                                      convert(char(19), [{nameof(PDOEntity.TBL_PDO.FinishTime)}], 120) FinishTime,
                                      [{nameof(PDOEntity.TBL_PDO.Out_Coil_Length)}],
                                      [{nameof(PDOEntity.TBL_PDO.Out_Coil_Width)}],
                                      [{nameof(PDOEntity.TBL_PDO.Out_Coil_Thick)}],
                                      [{nameof(PDOEntity.TBL_PDO.Out_Coil_Wt)}]
                                 From [{nameof(PDOEntity.TBL_PDO)}] 
                                Where [{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)}] = '1' 
                                  And [{nameof(PDOEntity.TBL_PDO.FinishTime)}] between '{PublicForms.Report.Dtp_Start_Time.Value:yyyy-MM-dd HH}:00:00' and '{PublicForms.Report.Dtp_Finish_Time.Value:yyyy-MM-dd HH}:59:59' ";

            if (PublicForms.Report.Chk_shift_no.Checked)
            {
                strSql += $"And [{nameof(PDOEntity.TBL_PDO.Shift)}] = '{PublicForms.Report.Cob_shift_no.SelectedValue}'";
            }

            return strSql;
        }


    }
}
