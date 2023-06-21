using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CPL1HMI
{
    public class ExcelColumnsHandler
    {

        public class TBL_PDI_FromExcel
        {
            public string Plan_No { get; set; }
            public string Mat_Seq_No { get; set; }
            public string Plan_Sort { get; set; }

            //[PrimaryKey]
            public string Entry_Coil_ID { get; set; }
            public string Entry_Coil_Thick { get; set; }
            public string Entry_Coil_Width { get; set; }
            public string Entry_Coil_Weight { get; set; }
            public string Entry_Coil_Length { get; set; }
            public string Entry_Coil_Inner { get; set; }
            public string Entry_Coil_Dcos { get; set; }
            public string Sleeve_Type_Code { get; set; }
            public string Sleeve_diamter { get; set; }
            public string Paper_Req_Code { get; set; }
            public string Paper_Code { get; set; }
            public string Head_Paper_Length { get; set; }
            public string Head_Paper_Width { get; set; }
            public string Tail_Paper_Length { get; set; }
            public string Tail_Paper_Width { get; set; }
            public string Ts_Stand_Max { get; set; }
            public string Ts_Stand_Min { get; set; }
            public string St_No { get; set; }
            public string Density { get; set; }
            public string REPAIR_TYPE { get; set; }
            public string Surface_Finishing_Code { get; set; }
            public string Surface_Accuracy { get; set; }
            public string Base_Surface { get; set; }
            public string Uncoiler_Direction { get; set; }
            public string Out_Coil_ID { get; set; }
            public string Out_Paper_Req_Code { get; set; }
            public string Out_Paper_Code { get; set; }
            public string Out_Sleeve_Diamter { get; set; }
            public string Out_Sleeve_Type_Code { get; set; }
            public string Out_Strap_Num { get; set; }
            public string Leader_Flag { get; set; }
            public string Sample_Flag { get; set; }
            public string Sample_Frqn_Code { get; set; }
            public string Sample_Lot_No { get; set; }
            public string Coil_Origin { get; set; }
            public string Wholebacklog_Code { get; set; }
            public string Next_Wholebacklog_Code { get; set; }
            public string Trim_Flag { get; set; }
            public string Out_Coil_Width { get; set; }
            public string Out_Coil_Width_Max { get; set; }
            public string Out_Coil_Width_Min { get; set; }
            public string Out_Coil_Thickness { get; set; }
            public string Out_Coil_Inner { get; set; }
            public string Order_No { get; set; }
            public string Order_Wt_Max { get; set; }
            public string Order_Wt_Min { get; set; }
            public string Order_Wt { get; set; }
            public string Dividing_Flag { get; set; }
            public string Dividing_Num { get; set; }
            public string Orderwt_1 { get; set; }
            public string Orderwt_2 { get; set; }
            public string Orderwt_3 { get; set; }
            public string Orderwt_4 { get; set; }
            public string Orderwt_5 { get; set; }
            public string Orderwt_6 { get; set; }
            public string Order_No_1 { get; set; }
            public string Order_No_2 { get; set; }
            public string Order_No_3 { get; set; }
            public string Order_No_4 { get; set; }
            public string Order_No_5 { get; set; }
            public string Order_No_6 { get; set; }
            public string D01_Code { get; set; }
            public string D01_Origin { get; set; }
            public string D01_Sid { get; set; }
            public string D01_Pos_W { get; set; }
            public string D01_Pos_L_Start { get; set; }
            public string D01_Pos_L_End { get; set; }
            public string D01_Level { get; set; }
            public string D01_Percent { get; set; }
            public string D01_QGRADE { get; set; }
            public string D02_Code { get; set; }
            public string D02_Origin { get; set; }
            public string D02_Sid { get; set; }
            public string D02_Pos_W { get; set; }
            public string D02_Pos_L_Start { get; set; }
            public string D02_Pos_L_End { get; set; }
            public string D02_Level { get; set; }
            public string D02_Percent { get; set; }
            public string D02_QGRADE { get; set; }
            public string D03_Code { get; set; }
            public string D03_Origin { get; set; }
            public string D03_Sid { get; set; }
            public string D03_Pos_W { get; set; }
            public string D03_Pos_L_Start { get; set; }
            public string D03_Pos_L_End { get; set; }
            public string D03_Level { get; set; }
            public string D03_Percent { get; set; }
            public string D03_QGRADE { get; set; }
            public string D04_Code { get; set; }
            public string D04_Origin { get; set; }
            public string D04_Sid { get; set; }
            public string D04_Pos_W { get; set; }
            public string D04_Pos_L_Start { get; set; }
            public string D04_Pos_L_End { get; set; }
            public string D04_Level { get; set; }
            public string D04_Percent { get; set; }
            public string D04_QGRADE { get; set; }
            public string D05_Code { get; set; }
            public string D05_Origin { get; set; }
            public string D05_Sid { get; set; }
            public string D05_Pos_W { get; set; }
            public string D05_Pos_L_Start { get; set; }
            public string D05_Pos_L_End { get; set; }
            public string D05_Level { get; set; }
            public string D05_Percent { get; set; }
            public string D05_QGRADE { get; set; }
            public string D06_Code { get; set; }
            public string D06_Origin { get; set; }
            public string D06_Sid { get; set; }
            public string D06_Pos_W { get; set; }
            public string D06_Pos_L_Start { get; set; }
            public string D06_Pos_L_End { get; set; }
            public string D06_Level { get; set; }
            public string D06_Percent { get; set; }
            public string D06_QGRADE { get; set; }
            public string D07_Code { get; set; }
            public string D07_Origin { get; set; }
            public string D07_Sid { get; set; }
            public string D07_Pos_W { get; set; }
            public string D07_Pos_L_Start { get; set; }
            public string D07_Pos_L_End { get; set; }
            public string D07_Level { get; set; }
            public string D07_Percent { get; set; }
            public string D07_QGRADE { get; set; }
            public string D08_Code { get; set; }
            public string D08_Origin { get; set; }
            public string D08_Sid { get; set; }
            public string D08_Pos_W { get; set; }
            public string D08_Pos_L_Start { get; set; }
            public string D08_Pos_L_End { get; set; }
            public string D08_Level { get; set; }
            public string D08_Percent { get; set; }
            public string D08_QGRADE { get; set; }
            public string D09_Code { get; set; }
            public string D09_Origin { get; set; }
            public string D09_Sid { get; set; }
            public string D09_Pos_W { get; set; }
            public string D09_Pos_L_Start { get; set; }
            public string D09_Pos_L_End { get; set; }
            public string D09_Level { get; set; }
            public string D09_Percent { get; set; }
            public string D09_QGRADE { get; set; }
            public string D10_Code { get; set; }
            public string D10_Origin { get; set; }
            public string D10_Sid { get; set; }
            public string D10_Pos_W { get; set; }
            public string D10_Pos_L_Start { get; set; }
            public string D10_Pos_L_End { get; set; }
            public string D10_Level { get; set; }
            public string D10_Percent { get; set; }
            public string D10_QGRADE { get; set; }
            public string Test_Plan_No { get; set; }
            public string Qc_Remark { get; set; }
            public string Head_Off_Gauge { get; set; }
            public string Tail_Off_Gauge { get; set; }
            public string Surface_Accu_Code_In { get; set; }
            public string Surface_Accu_Code_Out { get; set; }
            public string Sg_Sign { get; set; }
            public string Process_Code { get; set; }
            public string CustomerName_E { get; set; }
            public string CustomerName_C { get; set; }
            public string CustomerCode { get; set; }
            public string Surface_Acc_Desc { get; set; }
            public string Surface_Accuracy_Desc { get; set; }
            public string Surface_Acc_Desc_In { get; set; }
            public string Surface_Acc_Desc_Out { get; set; }



        }
    }
}
