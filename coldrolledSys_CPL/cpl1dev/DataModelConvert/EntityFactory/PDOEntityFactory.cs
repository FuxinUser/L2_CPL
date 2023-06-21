using Core.Define;
using DBService.Repository.PDI;
using DBService.Repository.PDO;
using MsgStruct;
using System;
using DataMod.PLC;
using Core.Util;
using static DBService.Repository.WieldRecord.WeldRecordEntity;

namespace MsgConvert.EntityFactory
{
    public static class PDOEntityFactory
    {
        public static PDOEntity.TBL_PDO TblCoilPDO(this L1L2Rcv.Msg_307_CoilDismount msg, CoilPDIEntity.TBL_PDI pdi, GenCoilInfoModel.GenPDODataPara genPDOPara, TBL_WeldRecords weld)
        {
            var Surface_Accuracy_Code = new Func<string>(() => {
                if (!string.IsNullOrEmpty(pdi.REPAIR_TYPE) && pdi.Entry_Coil_ID.Left(2) == "CM")
                    return "P3";
                switch (L2SystemDef.CPLSysNumber)
                {
                    case "1": return "P1";
                    case "2": return "P2";
                    default: return "";
                }
            })();

            var pdo = new PDOEntity.TBL_PDO();
            pdo.PDO_Uploaded_Flag = DBParaDef.FALSE;                            // Upload Flag預設0

            pdo.OrderNo = pdi.Order_No;                                         //合同號 - L3-PDI
            pdo.Plan_No = pdi.Plan_No;                                          //計畫號 - L3-PDI
            pdo.Out_Coil_ID = msg.CoilIDNo;                                     //出口卷 - L1-307
            pdo.In_Coil_ID = pdi.Entry_Coil_ID;                                 //入口卷 - L3-PDI
            pdo.OriPDI_Out_Coil_ID = pdi.Out_Coil_ID;
            pdo.StartTime = pdi.Start_Time;                                     //生產開始時間 - 鋼捲上POR
            pdo.FinishTime = pdi.Finish_Time;                                   //生產結束時間 - 鋼捲下TR           
            pdo.Shift = genPDOPara.Shift.ToString();                            //班次 - 待確定 根據生產結束時間判斷          
            pdo.Team = genPDOPara.Team;                                         //班別 - 待確定 根據排班表查表
            //pdo.Shift = "1";                                                  //班次 - 待確定 根據生產結束時間判斷          
            //pdo.Team = "A";                                                   //班別 - 待確定 根據排班表查表

            pdo.Out_Coil_Outer_Diameter = msg.Diameter;                         //出口卷外徑 - L1-307
            pdo.Out_Coil_Inner = msg.CoiInsideDiam;                             //出口卷内径 - L1-307
            pdo.Out_Theory_Wt = msg.CoilWeight;                                 //出口卷重[理論重] 
            //Out_Mat_Wt = (int)msg.CoilWeight - coilCutRecord.TotalWt;           //出口卷重[净重] = 理論重[毛重] -導帶-襯紙-套筒 (結算不做)
            //pdo.Out_Mat_Gross_WT = msg.CoilWeight;                              //出口卷重[毛重] - L1-307
            pdo.Out_Coil_Thick = pdi.Entry_Coil_Thick;                          //出口厚度 - L1-預設帶入口鋼捲厚度
            //Out_Mat_Width = 0;                                                  //出口寬度 - pdo (待後續輸入)
            pdo.Out_Coil_Length = (int)msg.CoilLength;                                //出口卷長度 - L1-307
            pdo.Paper_Code = msg.PaperCode.ToStr();                             //出口垫纸种类 - L1-307

         
            pdo.Paper_Req_Code = pdi.Paper_Req_Code;                            //出口垫纸方式 - L3-PDI

            //if(pdi.Paper_Req_Code.Equals())

            //pdo.Out_Coil_Width = pdi.Out_Coil_Width;                            //出口寬度(填pdi出口寬度)
            pdo.Out_Coil_Width = pdi.Entry_Coil_Width;                          //出口寬度(填pdi入口寬度)


            // - 人工輸入
            pdo.Out_Head_Paper_Length = 0;                                      //出口头部垫纸长度 - L3-PDI
            pdo.Out_Head_Paper_Width = 0;                                       //出口头部垫纸宽度 - L3-PDI
            pdo.Out_Tail_Paper_Length = 0;                                      //出口尾部垫纸长度 - L3-PDI
            pdo.Out_Tail_Paper_Width = 0;                                       //出口尾部垫纸宽度 - L3-PDI
            pdo.Sleeve_Inner_Exit_Diamter = pdi.Out_Sleeve_Diamter;             //出口套筒内径 - L3-PDI
            pdo.Sleeve_Type_Exit_Code = pdi.Out_Sleeve_Type_Code;               //出口套筒类型 - L3-PDI

            // 導帶
            if (pdi.Leader_Flag.Equals(DBParaDef.USE))                          // CPL頭尾焊接
            {
                pdo.Head_Hole_Position = weld == null ?
                        DeviceParaDef.HeadHolePosition :
                        weld.HeadEndLeaderWeldPointDistanceFromPunchHole;       //头部打孔位置 - Weld有取到用weld的, 沒有填固定位置600mm
                pdo.Head_Leader_Length = genPDOPara.Head_Leader_Length;         //头部导带长度 - PDI(Head_Strip_Length)
                pdo.Head_Leader_Width = genPDOPara.Head_Leader_Width;           //头部导带宽度 - PDI(Head_Strip_Width)
                pdo.Head_Leader_Thickness = genPDOPara.Head_Leader_Thickness;   //头部导带厚度 - PDI(Head_Strip_Thickness)
                        
                pdo.Tail_PunchHole_Position = weld == null ?
                        DeviceParaDef.HeadHolePosition :
                        weld.TailEndLeaderWeldPointDistanceFromPunchHole;       //尾部打孔位置 - Weld有取到用weld的, 沒有填固定位置600mm
                pdo.Tail_Leader_Length = genPDOPara.Tail_Leader_Length;         //尾部导带长度 - PDI(Tail_Strip_Length)
                pdo.Tail_Leader_Width = genPDOPara.Tail_Leader_Width;           //尾部导带宽度 - PDI(Tail_Strip_Width)
                pdo.Tail_Leader_Thickness = genPDOPara.Tail_Leader_Thickness;   //尾部导带厚度 - PDI(Tail_Strip_Thickness)

                pdo.Head_Leader_St_No = genPDOPara.Head_Leader_St_No;           //头部导带钢种 - PDI(Head_Strip_St_No)
                pdo.Tail_Leader_St_No = genPDOPara.Tail_Leader_St_No;           //尾部导带钢种 - PDI(Tail_leader_st_no)
            }

            pdo.Scraped_Length_Entry = genPDOPara.ScrapedLengthEntry;           //头部切废长度 - 查表CutRecord *
            pdo.Scraped_Length_Exit = genPDOPara.ScrapedLengthExit;             //尾部切废长度 - 查表CutRecord * 

         

            // DefectCode (後續輸入)

            pdo.Winding_Direction = DeviceParaDef.WindingDirectionDown;         //卷曲方向 - CPL 下收
            pdo.Base_Surface = pdi.Base_Surface;                                //好面朝向 PDI(Base_Surface)
            pdo.Inspector = "";                                                 //封锁责任者 pdo (待後續輸入)
            pdo.Hold_Flag = "";                                                 //封锁标记 pdo   (待後續輸入)
            pdo.Hold_Cause_Code = "";                                           //封锁原因代码pdo (待後續輸入)
            
            pdo.Sample_Flag = pdi.Sample_Flag;                                  //取样标记 PDI(Samp_Flag)
            pdo.Trim_Flag = pdi.Trim_Flag;                                      //切边标记 pdo (待後續輸入)          
            pdo.Fixed_WT_Flag = msg.DividFlag.ToString();                       //分卷标记 pdo (待後續輸入)
            pdo.End_Flag = "";                                                  //最终卷标记 pdo (待後續輸入)
            pdo.Scrap_Flag = "";                                                //废品标记 pdo (待後續輸入)
            pdo.Sample_Frqn_Code = pdi.Sample_Frqn_Code;                        //取样位置 pdo (待後續輸入)

            pdo.No_Leader_Code = genPDOPara.NoLeaderCode;                       //未焊引带代码 -  查表WeldRecord *

            //pdo.Surface_Accuracy_Code = pdi.Surface_Accuracy;                   //表面精度代码 - 根據pdi查表冷軋各產線鋼捲表面等級狀態管理 (CPL不用查表)
            pdo.Surface_Accuracy_Code = Surface_Accuracy_Code;

            pdo.Head_Off_Gauge = pdi.Tail_Off_Gauge;                            //头部未轧制区域 - PDI(Tail_Off_Gauge)
            pdo.Tail_Off_Gauge = pdi.Head_Off_Gauge;                            //尾部未轧制区域 - PDI(Head_Off_Gauge)
            //pdo.Surface_Accu_Code_In = pdi.Surface_Accu_Code_Out;               //内表面精度代码 - PDI(Surface_Accu_Code_Out) 翻面
            pdo.Surface_Accu_Code_In = Surface_Accuracy_Code;
            //pdo.Surface_Accu_Code_Out = pdi.Surface_Accu_Code_In;               //外表面精度代码 - PDI(Surface_Accu_Code_In) 翻面
            pdo.Surface_Accu_Code_Out = Surface_Accuracy_Code;
            pdo.Flip_Tag = "1";                                                 //翻面标记 CPL Always Y
            pdo.Process_Code = pdi.Process_Code;
            pdo.Decoiler_Direction = DeviceParaDef.WindingDirectionUp;
            pdo.CreateTime = DateTime.Now;

            return pdo;
        }



     

        private static string IsUse(int value)
        {
            return value == PlcSysDef.Cmd.NotUse ? PlcSysDef.Cmd.NotUseStr : PlcSysDef.Cmd.UseStr;
        }
    }
}
