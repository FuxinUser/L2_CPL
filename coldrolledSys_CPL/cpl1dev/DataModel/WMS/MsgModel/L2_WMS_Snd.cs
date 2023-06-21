using Core.Util;
using System;
using System.Runtime.InteropServices;
namespace MsgStruct
{
    public class L2_WMS_Snd
    {
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class PWx1_ScheduleList
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] MessageID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public byte[] ProcessDateTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] IDOfSourceComputer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] IDOfDestinationComputer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Count;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6000)]
            public byte[] CoilNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] Spare;

            public string MsgID { get => MessageID.ToStr(); }
            public string CoilNos { get => CoilNo.ToStr(); }
        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class PWx2_TrackingMap
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] MessageID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public byte[] ProcessDateTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] IDOfSourceComputer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] IDOfDestinationComputer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] sk11;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] sk12;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] sk13;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] TopSensor1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] EntryExit1;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] sk21;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] sk22;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] sk23;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] TopSensor2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] EntryExit2;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] sk31;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] sk32;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] sk33;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] TopSensor3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] EntryExit3;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] LineSatus;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] Spare;

            public string MsgID { get => MessageID.ToStr(); }

        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class PWx3_CoilInfo
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] MessageID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public byte[] ProcessDateTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] IDOfSourceComputer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] IDOfDestinationComputer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] InCoilNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] OutCoilNo;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] OrderNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] PackType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] InnerDia;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] OuterDia;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public byte[] CoilThick;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] CoilWidth;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] CoilWeight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] CoilTurn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] CoilContainsOil;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Next_WholeBackLog_Code;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Sleeve_Inner_Dia;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public byte[] Sleeve_Thick;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] Sleeve_Width;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] Spare;

            public string MsgID { get => MessageID.ToStr(); }

            public string CoilID { get => InCoilNo.ToStr(); }

        }
       
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class PWx5_FeedingRequest_EntryExitReturn
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] MessageID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public byte[] ProcessDateTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] IDOfSourceComputer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] IDOfDestinationComputer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] Flag;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] SKIDNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] CoilNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] CoilTurn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public byte[] Spare;

            public string MsgID { get => MessageID.ToStr(); }
            public string CoilID { get => CoilNo.ToStr(); }
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class PWx6_ScanCoil
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] MessageID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public byte[] ProcessDateTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] IDOfSourceComputer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] IDOfDestinationComputer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Length;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] SkidNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] CoilNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public byte[] Spare;

            public string MsgID { get => MessageID.ToStr(); }
            public string CoilIDNo { get => CoilNo.ToStr(); }
        }


    }
}