namespace Core.Define
{
    public static class L25SysDef
    {
        public class MsgCode
        {
            public static string Msg101PDI = "101";
            public static string Msg102PDO = "102";
            public static string Msg103DownTime = "103";
            public static string Msg104ENGC = "104";
            public static string Msg105SpeedCT="105"; 
            public static string Msg106UNCTensionCT="106";
            public static string Msg107UNCCurrentCT="107";
            public static string Msg108RECTensionCT="108";
            public static string Msg109RECCurrentCT="109";
            public static string Msg110CPLPRESET="110";
            public static string Msg111CoilMap="111";
            public static string Msg112L1L2DisConnection="112";
            public static string Msg113L2APStatus="113";
            public static string Msg114Alive="114";
            public static string Msg115CoilRejectResult = "115";
            public static string Msg116WeldCT = "116";
        }

        public class MsgLength
        {
            public static string Msg101PDI = "1808";
            public static string Msg102PDO = "588";
            public static string Msg103DownTime = "638";
            public static string Msg104ENGC = "41";

            public static string Msg105SpeedCT = "8150";
            public static string Msg106UNCTensionCT = "8150";
            public static string Msg107UNCCurrentCT = "8150";
            public static string Msg108RECTensionCT = "8150";
            public static string Msg109RECCurrentCT = "8150";

            public static string Msg110CPLPRESET = "713";
            public static string Msg111CoilMap = "281";
            public static string Msg112L1L2DisConnection = "32";
            public static string Msg114Alive = "25";
            public static string Msg115CoilRejectResult = "118";

            public static string Msg116WeldCT = "8150";
        }

        public class CTData
        {

            public class Code
            {
                public static string Speed = "CPUNC0"+ L2SystemDef.CPLSysNumber + "C0001";
                public static string UNCTensionAct = "CPUNC0"+ L2SystemDef.CPLSysNumber + "C0002";
                public static string UNCCurrent = "CPUNC0"+ L2SystemDef.CPLSysNumber + "C0003";
                public static string UNCTensionRef = "CPUNC0" + L2SystemDef.CPLSysNumber + "C0004";
                public static string RECTensionAct = "CPREC0"+ L2SystemDef.CPLSysNumber + "C0001";
                public static string RECCurrent = "CPREC0"+ L2SystemDef.CPLSysNumber+"C0002";
                public static string RECTensionRef = "CPREC0" + L2SystemDef.CPLSysNumber + "C0003";
                //public static string WELD_Current_Actual_Front = "WeldCurActF";
                //public static string WELD_Current_Actual_Rear = "WeldCurActR";
                //public static string WELD_Speed_Actual = "WeldSpeedAct";
                //public static string WELD_PlanishWheelForce_Actual = "WeldPWFAct";
                //public static string WELD_Temperture = "WeldTemp";
                public static string WELD_Current_Actual_Front = "CPWEL0" + L2SystemDef.CPLSysNumber + "C0001";
                public static string WELD_Current_Actual_Rear = "CPWEL0" + L2SystemDef.CPLSysNumber + "C0002";
                public static string WELD_Speed_Actual = "CPWEL0" + L2SystemDef.CPLSysNumber + "C0003";
                public static string WELD_PlanishWheelForce_Actual = "CPWEL0" + L2SystemDef.CPLSysNumber + "C0004";
                public static string WELD_Temperture = "CPWEL0" + L2SystemDef.CPLSysNumber + "C0005";
            }

            public class Desc
            {
                public static string Speed = "speed";
                public static string Tension = "tension";
                public static string Current = "current";
                public static string Weld = "weld";
            }
         
            public class SeqUnit
            {
                public static string Speed = "M";
                public static string UNCTension = "M";
                public static string UNCCurrent = "M";
                public static string RECTension = "M";
                public static string RECCurrent = "M";
                public static string WELD_Current_Actual_Front = "Second";
                public static string WELD_Current_Actual_Rear = "Second";
                public static string WELD_Speed_Actual = "Second";
                public static string WELD_PlanishWheelForce_Actual = "Second";
                public static string WELD_Temperture = "Second";
            }

            public class ResultUnit
            {
                public static string Speed = "m/min";
                public static string UNCTension = "N/Kg";
                public static string UNCCurrent = "A";
                public static string RECTension = "N/Kg";
                public static string RECCurrent = "A";
                public static string WELD_Current_Actual_Front = "Amps";
                public static string WELD_Current_Actual_Rear = "Amps";
                public static string WELD_Speed_Actual = "mm/min";
                public static string WELD_PlanishWheelForce_Actual = "kg/mm2";
                public static string WELD_Temperture = "degC";
            }


            public class Frenquency
            {
                public static string FSecond = "5s";
            }
        }

        public enum L25CTData
        {
            Speed, UNCTensionActCT, UNCTensionRefCT, UNCCurrentCT,
            RECTensionActCT, RECTensionRefCT, RECCurrentCT,
            WELD_Current_Actual_Front, WELD_Current_Actual_Rear,
            WELD_Speed_Actual, WELD_PlanishWheelForce_Actual, WELD_Temperture
        }
     

        public static string L25CTDataClassifyToStr(this L25CTData classify)
        {
            var str = string.Empty;

            switch (classify)
            {
                case L25CTData.Speed:
                    str = "Speed";
                    break;

                case L25CTData.UNCTensionActCT:
                    str = "UNCTensionActCT";
                    break;

                case L25CTData.UNCTensionRefCT:
                    str = "UNCTensionRefCT";
                    break;

                case L25CTData.UNCCurrentCT:
                    str = "UNCCurrentCT";
                    break;

                case L25CTData.RECTensionActCT:
                    str = "RECTensionActCT";
                    break;

                case L25CTData.RECTensionRefCT:
                    str = "RECTensionRefCT";
                    break;

                case L25CTData.RECCurrentCT:
                    str = "RECCurrentCT";
                    break;

                case L25CTData.WELD_Speed_Actual:
                    str = "WeldSpeedAct";
                    break;

                case L25CTData.WELD_Current_Actual_Front:
                    str = "WeldCurActF";
                    break;

                case L25CTData.WELD_Current_Actual_Rear:
                    str = "WeldCurActR";
                    break;

                case L25CTData.WELD_PlanishWheelForce_Actual:
                    str = "WeldPWFAct";
                    break;

                case L25CTData.WELD_Temperture:
                    str = "WeldTemp";
                    break;

            }

            return str;
        }
    }
}
