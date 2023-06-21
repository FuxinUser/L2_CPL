using System;

namespace CPL1HMI
{
    public class DataBaseTableFactory
    {
       
        #region 系統設定類

        public class TBL_ComboBoxItems
        {
            public int Cbo_Type { get; set; }
            public int Cbo_Index { get; set; }
            public string Cbo_Value { get; set; }
            public string Cbo_Text { get; set; }
            public DateTime UpdateTime { get; set; }
            public string Spare { get; set; }
        }
        public class TBL_AuthorityData
        {
            public string User_ID { get; set; }
            public string Password { get; set; }
            public string Department { get; set; }
            public string Team { get; set; }
            public string Authority_Class { get; set; }
            public DateTime Create_DateTime { get; set; }
        }
        public class TBL_AuthorityData_Frame
        {
            public string User_ID { get; set; }
            public string Frame_ID { get; set; }
            public string Frame_Function { get; set; }
            public DateTime Create_DateTime { get; set; }
        }
        public class TBL_ConnectionStatus
        {
            public string Connection_From { get; set; }
            public string Connection_To { get; set; }
            public string Connection_Type { get; set; }
            public string Connection_IP { get; set; }
            public string Connection_Port { get; set; }
            public string Connection_Status { get; set; }
            public DateTime Create_DateTime { get; set; }
        }
       
        #endregion


        #region Language
        public class TBL_LanguageSwitch
        {
            public string PKey { get; set; }
            public string ZH { get; set; }
            public string EN { get; set; }
            public string UpdateUser { get; set; }
            public string UpdateDateTime { get; set; }
        }

        public class TBL_LangSwitch_Nav
        { 
            public string PKey { get; set; }
            public string ZH { get; set; }
            public string EN { get; set; }
            public string UpdateUser { get; set; }
            public DateTime UpdateDateTime { get; set; }
        }

        public class TBL_LangSwitch_Ctr
        {
            public string FormName { get; set; }
            public string CtrName { get; set; }
            public string ZH { get; set; }
            public string EN { get; set; }
            public string ColumnName { get; set; }
        }

        #endregion

        public class TBL_StripBrakeSignal
        {
            public string UncoilerCoil_No { get; set; }
            public double UncoilerCoil_Thick { get; set; }
            public int UncoilerCoil_Width { get; set; }
            public int UncoilerCoil_Length { get; set; }
            public int UncoilerCoil_InnerDiameter { get; set; }
            public int UncoilerCoil_OuterDiameter { get; set; }
            public int UncoilerCoil_TheorticalWt { get; set; }
            public string RecoilerCoil_No { get; set; }
            public double RecoilerCoil_Thick { get; set; }
            public int RecoilerCoil_Width { get; set; }
            public int RecoilerCoil_Length { get; set; }
            public int RecoilerCoil_InnerDiameter { get; set; }
            public int RecoilerCoil_OuterDiameter { get; set; }
            public int RecoilerCoil_TheoreticalWt { get; set; }
            public DateTime ReceiveTime { get; set; }
            public DateTime CreateTime { get; set; }

        }
    }
}
