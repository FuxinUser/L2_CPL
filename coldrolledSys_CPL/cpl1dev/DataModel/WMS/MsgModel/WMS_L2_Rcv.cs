using Core.Define;
using Core.Util;
using System;
using System.Runtime.InteropServices;
using static DataModel.HMIServerCom.Msg.SCCommMsg;

namespace MsgStruct
{
    public class WMS_L2_Rcv
    {
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class WPx1_CompleteOfFeeding
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
            public byte[] SkidNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] CoilNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] BarCodeCoilNo;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Spare;

            public string CoilNoID { get => CoilNo.ToStr(); }

            public string SkidNoID { get => SkidNo.ToStr(); }

            public string ScanNoID { get => BarCodeCoilNo.ToStr(); }


            public L2SystemDef.SKPOS GetEntryPOS()
            {
                if (SkidNo.Equals(WMSSysDef.SkPos.ESk03.ToString()))
                    return L2SystemDef.SKPOS.Entry_SK01;

                if (SkidNo.Equals(WMSSysDef.SkPos.ESk02.ToString()))
                    return L2SystemDef.SKPOS.Entry_SK02;

                return L2SystemDef.SKPOS.EntryTOP;
            }

            public L2SystemDef.SKPOS GetDeliveryPOS()
            {
                if (SkidNo.Equals(WMSSysDef.SkPos.DSk03.ToString()))
                    return L2SystemDef.SKPOS.Delivery_SK01;

                if (SkidNo.Equals(WMSSysDef.SkPos.DSk02.ToString()))
                    return L2SystemDef.SKPOS.Delivery_SK02;

                return L2SystemDef.SKPOS.DeliveryTop;
            }


            public CoilSkPosition CoilSKPos()
            {

                CoilSkPosition pos;

                switch (SkidNo.ToStr())
                {
                    case "1":
                        pos = CoilSkPosition.POR;
                        break;
                    case "2":
                        pos = CoilSkPosition.ESK01;
                        break;
                    case "3":
                        pos = CoilSkPosition.ESK02;
                        break;
                    default:
                        pos = CoilSkPosition.ETOP;
                        break;
                }

                return pos;
            }


            /*
             #1CPL     1 : ESK01 2 : ESK02 3 : ETOP 4 : DSK01 5 : DSK02 6 : DTOP
             #2CPL     1 : ESK01 2 : ESK02 3 : ETOP 4 : DSK01 5 : DSK02 6 : DTOP

             */

            public int L1201PresetPos
            {
                get
                {
                    var pos = PlcSysDef.Pos.Preset201POR;
                    var skid = int.Parse(SkidNoID);
                    switch (skid)
                    {
                        case WMSSysDef.SkPos.ESk03:
                            pos = PlcSysDef.Pos.Preset201SK1;
                            break;
                        case WMSSysDef.SkPos.ESk02:
                            pos = PlcSysDef.Pos.Preset201SK2;
                            break;
                        case WMSSysDef.SkPos.ETop:
                            pos = PlcSysDef.Pos.Preset201ETOP;
                            break;
                        case WMSSysDef.SkPos.DSk03:
                        case WMSSysDef.SkPos.DSk02:
                        case WMSSysDef.SkPos.DTop:
                            throw new Exception($"非入料位置,SkidNoID={SkidNoID}");
                    }


                    return pos;

                }
            }
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class WPx3_RequestResponse
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
            public char[] PosFlag;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] CoilNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] ProcessFlag;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] ReasonCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
            public byte[] Reason;
            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            //public byte[] Spare;


            public string ActionResultStr
            {
                get
                {

                    var actionResult = string.Empty;

                    switch (ProcessFlag.ToStr())
                    {
                        case "0":
                            actionResult = "NO";
                            break;

                        case "1":
                            actionResult = "YES";
                            break;
                    }

                    return actionResult;
                }
            }
            public string PositionStr
            {
                get
                {

                    var actionResult = string.Empty;

                    switch (new string(PosFlag))
                    {
                        case "1":
                            actionResult = "入料";
                            break;

                        case "2":
                            actionResult = "出料";
                            break;

                        case "3":
                            actionResult = "退料";
                            break;
                    }

                    return actionResult;
                }
            }

        }


    }
}