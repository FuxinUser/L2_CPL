using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Core.Define;
using Core.Util;

namespace MsgStruct
{
    public class MMSL2Rcv
    {

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_HeartBeat
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode = new byte[1];

            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            //public byte[] End;
        }


        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_PDI
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] Plan_No = new byte[10];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Mat_Seq_No = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Plan_Sort = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Entry_Coil_No = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Entry_Coil_Thick = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Entry_Coil_Width = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Entry_Coil_Weight = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Entry_Coil_Length = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Entry_Coil_Inner = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Entry_Coil_Dcos = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Sleeve_Type_Code = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Sleeve_diamter = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Paper_Req_Code = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Paper_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Head_Paper_Length = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Head_Paper_Width = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Tail_Paper_Length = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Tail_Paper_Width = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Ts_Stand_Max = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Ts_Stand_Min = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] St_No = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Density = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] REPAIR_TYPE = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Surface_Finishing_Code = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Surface_Accuracy = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Base_Surface = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Uncoiler_Direction = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Out_Mat_No = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Out_Paper_Req_Code = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Out_Paper_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Out_Sleeve_Diamter = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Out_Sleeve_Type_Code = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Out_Strap_Num = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Leader_Flag = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Samp_Flag = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Sample_Frqn_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Sample_Lot_No = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Coil_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Wholebacklog_Code = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Next_Wholebacklog_Code = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Trim_Flag = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Out_Mat_Width = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Out_Mat_Width_Max = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Out_Mat_Width_Min = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Out_Mat_Thickness = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Out_Coil_Inner = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] Order_No = new byte[10];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Order_Wt_Max = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Order_Wt_Min = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Order_Wt = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Dividing_Flag = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Dividing_Num = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Orderwt_1 = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Orderwt_2 = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Orderwt_3 = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Orderwt_4 = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Orderwt_5 = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Orderwt_6 = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] Order_No_1 = new byte[10];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] Order_No_2 = new byte[10];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] Order_No_3 = new byte[10];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] Order_No_4 = new byte[10];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] Order_No_5 = new byte[10];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] Order_No_6 = new byte[10];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D01_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D01_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D01_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D01_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D01_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D01_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D01_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D01_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D01_QGRADE = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D02_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D02_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D02_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D02_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D02_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D02_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D02_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D02_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D02_QGRADE = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D03_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D03_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D03_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D03_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D03_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D03_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D03_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D03_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D03_QGRADE = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D04_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D04_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D04_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D04_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D04_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D04_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D04_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D04_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D04_QGRADE = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D05_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D05_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D05_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D05_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D05_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D05_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D05_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D05_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D05_QGRADE = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D06_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D06_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D06_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D06_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D06_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D06_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D06_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D06_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D06_QGRADE = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D07_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D07_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D07_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D07_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D07_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D07_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D07_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D07_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D07_QGRADE = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D08_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D08_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D08_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D08_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D08_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D08_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D08_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D08_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D08_QGRADE = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D09_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D09_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D09_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D09_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D09_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D09_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D09_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D09_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D09_QGRADE = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D10_Code = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D10_Origin = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D10_Sid = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D10_Pos_W = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D10_Pos_L_Start = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] D10_Pos_L_End = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D10_Level = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] D10_Percent = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] D10_QGRADE = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
            public byte[] Test_Plan_No = new byte[50];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1000)]
            public byte[] Qc_Remark = new byte[1000];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Head_Off_Gauge = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Tail_Off_Gauge = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Surface_Accu_Code_In = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Surface_Accu_Code_Out = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
            public byte[] Sg_Sign = new byte[50];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Process_Code = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
            public byte[] Customer_Name_E = new byte[200];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
            public byte[] Customer_Name_C = new byte[200];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Customer_No = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Surface_Acc_Desc = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Surface_Accuracy_Desc = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Surface_Acc_Desc_In = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Surface_Acc_Desc_Out = new byte[20];


            public string MsgID { get => Code.ToStr();  }
            public int MatSeqNo { get => Mat_Seq_No.ToStr().ToNullable<int>()??0; }
            public string EntryCoilNo { get => Entry_Coil_No.ToStr(); }
            public string PlanNo { get => Plan_No.ToStr(); }
          
        }


        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Sleeve_Value_Synchronize
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Deal_Flag = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] SleeveCode = new byte[10];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Out_Mat_Inner_Dia = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Sleeve_Thick = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Sleeve_Width = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] Sleeve_Wt = new byte[7];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Sleeve_Type = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Out_Mat_Width_Min = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Out_Mat_Width_Max = new byte[5];

            public string Action
            {
                get
                {

                    var action = string.Empty;

                    switch (Deal_Flag.ToStr())
                    {
                        case MMSSysDef.Cmd.SyncValueInsert:
                            action = "·s¼W";
                            break;
                        case MMSSysDef.Cmd.SyncValueUpdate:
                            action = "§ó·s";
                            break;
                        case MMSSysDef.Cmd.SyncValueDelete:
                            action = "§R°£";
                            break;
                        default:
                            action = "µL¦¹©R¥O¥N¸¹";
                            break;
                    }

                    return action;

                }
            }

        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Paper_Value_Synchronize
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Deal_Flag = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] PaperCode = new byte[10];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Paper_Wt = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Paper_Width = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Paper_Min_Thick = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Paper_Max_Thick = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Paper_Thick = new byte[3];

            public string Action
            {
                get
                {

                    var action = string.Empty;

                    switch (Deal_Flag.ToStr())
                    {
                        case MMSSysDef.Cmd.SyncValueInsert:
                            action = "新增";
                            break;
                        case MMSSysDef.Cmd.SyncValueUpdate:
                            action = "更新";
                            break;
                        case MMSSysDef.Cmd.SyncValueDelete:
                            action = "刪除";
                            break;
                        default:
                            action = "錯誤指令";
                            break;
                    }

                    return action;

                }
            }

        }



        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Coil_Schedule
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] CoilNo = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] NumberOfCoils = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6000)]
            public byte[] EntryCoilNo = new byte[6000];
            
            public string MsgID { get => Code.ToStr(); }
            public string CoilSchedules { get => Encoding.UTF8.GetString(EntryCoilNo); }
            public int ScheduleCnt { get => NumberOfCoils.ToStr().ToNullable<int>() ?? 0; }

        
        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Coil_Schedule_Test
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] CoilNo = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] NumberOfCoils = new byte[3];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo1 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo2 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo3 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo4 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo5 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo6 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo7 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo8 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo9 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo10 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo11 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo12 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo13 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo14 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo15 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo16 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo17 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo18 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo19 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo20 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo21 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo22 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo23 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo24 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo25 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo26 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo27 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo28 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo29 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo30 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo31 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo32 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo33 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo34 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo35 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo36 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo37 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo38 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo39 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo40 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo41 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo42 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo43 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo44 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo45 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo46 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo47 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo48 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo49 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo50 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo51 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo52 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo53 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo54 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo55 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo56 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo57 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo58 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo59 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo60 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo61 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo62 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo63 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo64 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo65 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo66 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo67 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo68 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo69 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo70 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo71 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo72 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo73 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo74 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo75 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo76 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo77 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo78 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo79 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo80 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo81 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo82 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo83 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo84 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo85 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo86 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo87 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo88 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo89 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo90 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo91 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo92 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo93 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo94 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo95 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo96 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo97 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo98 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo99 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo100 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo101 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo102 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo103 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo104 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo105 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo106 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo107 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo108 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo109 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo110 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo111 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo112 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo113 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo114 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo115 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo116 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo117 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo118 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo119 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo120 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo121 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo122 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo123 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo124 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo125 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo126 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo127 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo128 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo129 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo130 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo131 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo132 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo133 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo134 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo135 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo136 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo137 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo138 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo139 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo140 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo141 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo142 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo143 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo144 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo145 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo146 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo147 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo148 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo149 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo150 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo151 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo152 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo153 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo154 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo155 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo156 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo157 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo158 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo159 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo160 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo161 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo162 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo163 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo164 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo165 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo166 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo167 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo168 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo169 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo170 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo171 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo172 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo173 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo174 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo175 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo176 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo177 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo178 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo179 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo180 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo181 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo182 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo183 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo184 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo185 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo186 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo187 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo188 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo189 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo190 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo191 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo192 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo193 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo194 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo195 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo196 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo197 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo198 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo199 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo200 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo201 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo202 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo203 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo204 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo205 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo206 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo207 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo208 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo209 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo210 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo211 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo212 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo213 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo214 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo215 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo216 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo217 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo218 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo219 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo220 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo221 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo222 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo223 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo224 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo225 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo226 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo227 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo228 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo229 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo230 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo231 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo232 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo233 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo234 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo235 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo236 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo237 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo238 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo239 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo240 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo241 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo242 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo243 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo244 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo245 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo246 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo247 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo248 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo249 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo250 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo251 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo252 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo253 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo254 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo255 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo256 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo257 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo258 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo259 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo260 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo261 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo262 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo263 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo264 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo265 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo266 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo267 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo268 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo269 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo270 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo271 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo272 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo273 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo274 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo275 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo276 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo277 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo278 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo279 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo280 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo281 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo282 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo283 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo284 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo285 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo286 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo287 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo288 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo289 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo290 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo291 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo292 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo293 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo294 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo295 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo296 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo297 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo298 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo299 = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] EntryCoilNo300 = new byte[20];

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Res_For_No_Coil_Schedule
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Mat_No;

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Res_For_No_Coil_PDI
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Mat_No = new byte[20];

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Res_For_PDO_Uploaded
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Mat_No = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] Plan_No = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Succ_Flag = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
            public byte[] Err_Msg = new byte[20];

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Dummy_Coil_List
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Dummy_Coil_No = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Dummy_Coil_Thick = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Dummy_Coil_Width = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Dummy_Coil_Weight = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] Dummy_Coil_Length = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Dummy_Coil_Inner = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Dummy_Coil_Diam = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
            public byte[] SteelGradeSign = new byte[50];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] SteelGradeCode = new byte[8];

            public string MsgID { get => Code.ToStr(); }
            public string CoilNoID { get => Dummy_Coil_No.ToStr(); }
            public string DummyCoilThick { get => Dummy_Coil_Thick.ToStr(); }
            public string DummyCoilWidth { get => Dummy_Coil_Width.ToStr(); }
            public string DummyCoilWeight { get => Dummy_Coil_Weight.ToStr(); }
            public string DummyCoilLength { get => Dummy_Coil_Length.ToStr(); }
            public string DummyCoilInner { get => Dummy_Coil_Inner.ToStr(); }
            public string DummyCoilDiam { get => Dummy_Coil_Diam.ToStr(); }
            public string GradeCode { get => SteelGradeCode.ToStr(); }
            public string GradeSign { get => SteelGradeSign.ToStr(); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Res_of_Dummy_Coil_List_Req
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Dummy_Coil_No = new byte[20];

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_OP_Plan_Req_Del
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] plan_no = new byte[10];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] OperatorId = new byte[10];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] ReasonCode = new byte[4];

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Package_Cmd
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] COIL_NO = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] PACK_TYPE_BW = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] PACK_PLAN_NO = new byte[10];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] ORDER_NO = new byte[10];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
            public byte[] SG_SIGN = new byte[50];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] ST_no = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] UNIT_CODE = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] DELIVY_DATE = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] PLAN_FIN_DATE = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] MAT_ACT_THICK = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] MAT_ACT_WIDTH = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] MAT_ACT_LEN = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] MAT_ACT_WT = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] MAT_GROSS_WT = new byte[5];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] RG_Surface_Direction = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
            public byte[] SG_STD = new byte[100];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] QUALITY_GRADE = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SURFACE_ACCURACY = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] TRADE_CODE = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] TRIM_FLAG = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] PSC = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] HEAT_NO = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] ORDER_THICK = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] ORDER_WIDTH = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] PAPER_FLAG = new byte[1];

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Package_Material_Del_Req
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FLAG = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] PLAN_NO = new byte[10];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] COIL_NO = new byte[20];

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_UnPackage_Plan
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FLAG = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] PLAN_NO = new byte[10];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] COIL_NO = new byte[20];

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_UnPackage_Cmd
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] COIL_NO = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] PLAN_NO = new byte[10];

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Res_For_Coil_Reject_Result
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] RequestedCoilNo = new byte[20];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] ProcessResult = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
            public byte[] RejectCause = new byte[80];

            public string MsgID { get => Code.ToStr(); }
            public string RequestedCoilNoID { get => RequestedCoilNo.ToStr(); }
            public string ProResult { get => ProcessResult.ToStr(); }
            public string RejectReason { get => RejectCause.ToStr(); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Product_Result_Request
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] CoilNo = new byte[20];
            public string MsgID { get => Code.ToStr(); }
            public string CoilNoID { get => CoilNo.ToStr(); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Msg_Req_Delete_Schedule_Plan
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length = new byte[4];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Code = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Date = new byte[8];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Time = new byte[6];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] SendWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RcvWho = new byte[2];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] FuncCode = new byte[1];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] Plan_No = new byte[10];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] Operator_Id = new byte[10];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Reason_Code = new byte[4];
            public string MsgID { get => Code.ToStr(); }
            public string PlanNo { get => Plan_No.ToStr(); }

            public string OperatorID { get => Operator_Id.ToStr(); }
            public string ReasonCode { get => Reason_Code.ToStr(); }

        }

    }
}