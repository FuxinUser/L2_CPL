
using DBService.Repository.Leader;
using DBService.Repository.LookupTbl;
using DBService.Repository.LookupTblSideTrimmer;
using DBService.Repository.MaterialGrade;
using DBService.Repository.PDI;
using DBService.Repository.PDO;

namespace CPL1HMI
{
    public class Frm_2_2_SqlFactory
    {

        #region --- Display ---


        public static string SQL_Select_ProductionSetup()
        {
            string strSql = $@"Select 
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Thick)}],
                            CONVERT(varchar,b.[{nameof(PDOEntity.TBL_PDO.StartTime)}],120)StartTime,
                            CONVERT(varchar,b.[{nameof(PDOEntity.TBL_PDO.FinishTime)}],120)FinishTime,
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Width)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Weight)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Length)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Inner)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Dcos)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Ts_Stand_Max)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Ts_Stand_Min)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.St_No)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Density)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Surface_Finishing_Code)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Surface_Accuracy)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_ID)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Max)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Min)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Thickness)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Inner)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Order_Wt)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Order_Wt_Max)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Order_Wt_Min)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Dividing_Num)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_1)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_2)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_3)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_4)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_5)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_6)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Order_No_1)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Order_No_2)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Order_No_3)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Order_No_4)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Order_No_5)}],
                            a.[{nameof(CoilPDIEntity.TBL_PDI.Order_No_6)}]
                    From [{nameof(CoilPDIEntity.TBL_PDI)}] a 
                    Left join [{nameof(PDOEntity.TBL_PDO)}] b on a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = b.[{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}]
                    Where b.[{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)}] = '1'
                      And ( b.[{nameof(PDOEntity.TBL_PDO.FinishTime)}] <> '' 
                       Or b.[{nameof(PDOEntity.TBL_PDO.FinishTime)}] <> null )";
            string strSql2 = $@"SELECT      
                                Coil_ID, OriPDI_Out_Coil_ID, SteelGrade, 
                                Thickness, Width, EntryYieldStress, Density, CoilLength, 
                                CoilWeight, ProcessCode, InnerDiam, Diameter, 
                                SleeveCodeEntry, SleeveDmEntry, PaperWinderFlag, 
                                SleeveCodeExit, SleeveDmExit, PaperTypeExit, PaperCodeExit

                                , FlatenerDepth1, FlatenerDepth2, UncoilerTension, 
                                UncoilerTensionMax, UncoilerTensionMin,
                                HeadLeaderStripLength, HeadLeaderStripThickness, 
                                HeadLeaderStripWidth, HeadLeaderStripSteelGrade, 
                                TailLeaderStripLength, TailLeaderStripThickness, 
                                TailLeaderStripWidth, TailLeaderStripSteelGrade,

                                SideTrimmerGap, SideTrimmerLap, SideTrimmerWidth, 
                                TensionUnitDepth, RecoilerTension, RecoilerTensionMax, RecoilerTensionMin, 
                                PaperUnwinderFlag, CoilSplit, 
                                Orderwt_1, Orderwt_2, Orderwt_3, Orderwt_4, Orderwt_5, Orderwt_6, 
                                PrrPosId, PresetTime
                                FROM   TBL_PresetRecord
                                WHERE  Coil_ID  <> '' ";

            return strSql2;
        }


        public static string SQL_Select_ProcessData(string Coil_ID)
        {
            string strSql = $@"Select 
                              leader.[{nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_St_No)}],
                              leader.[{nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Length)}],
                              leader.[{nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Width)}],
                              leader.[{nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Thickness)}],
                              leader.[{nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_St_No)}],
                              leader.[{nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Length)}],
                              leader.[{nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Width)}],
                              leader.[{nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Thickness)}],
                              Tension.[{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade)}],
                              Tension.[{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width)}],
                              Tension.[{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max)}],
                              Tension.[{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min)}],
                              Tension.[{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.PORTension)}],
                              Tension.[{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.TRTension)}],                 
                              Trimmer.[{nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.Material_Grade)}],
                              Trimmer.[{nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.Coil_Thickness_Max)}],
                              Trimmer.[{nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.Coil_Thickness_Min)}],
                              Trimmer.[{nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.KnifeGap)}],
                              Trimmer.[{nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.KnifeLap)}]
                        From [{nameof(CoilPDIEntity.TBL_PDI)}] pdi
                        Left Join [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade)}] Steel
                        On Steel.[{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.St_No)}] = pdi.[{nameof(CoilPDIEntity.TBL_PDI.St_No)}]
                        Left Join [{nameof(LeaderTempEntity.TBL_Leader_Temp)}] leader
                        On leader.[{nameof(LeaderTempEntity.TBL_Leader_Temp.Coil_ID)}] = pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}]
                        Left Join [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension)}] Tension
                        On Tension.[{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade)}] = Steel.[{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Material_Grade)}]
                        And pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Thick)}] between Tension.[{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min)}] and Tension.[{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max)}]
                        And pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Width)}] = Tension.[{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width)}]
                        Left Join [{nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer)}] Trimmer 
                        On Trimmer.[{nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.Material_Grade)}] = Steel.[{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Material_Grade)}]
                        And pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Thick)}] between Trimmer.[{nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.Coil_Thickness_Min)}] and Trimmer.[{nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.Coil_Thickness_Max)}]
                        Where pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = '{Coil_ID}'
                        And pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Thick)}] Between Trimmer.[{nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.Coil_Thickness_Min)}] and Trimmer.[{nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.Coil_Thickness_Max)}] ";

            return strSql;
        }

        #endregion


        #region --- ComboBoxItems ---

        public static string Frm_2_2_ComboBoxCoilList_DB_PDI()
        {
            string strSql = $@"Select distinct pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}]
                                 From [{nameof(CoilPDIEntity.TBL_PDI)}] pdi
                                 Left join [{nameof(PDOEntity.TBL_PDO)}] b on pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = b.[{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}]
                                Where b.[{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)}] = '1'
                                  And ( b.[{nameof(PDOEntity.TBL_PDO.FinishTime)}] <> '' 
                                   Or b.[{nameof(PDOEntity.TBL_PDO.FinishTime)}] <> null) ";

            string strSql2 = $@"SELECT  distinct Coil_ID  
                                FROM    TBL_PresetRecord
                                WHERE   Coil_ID  <> '' ";

            return strSql2;
        }


        #endregion

    }
}
