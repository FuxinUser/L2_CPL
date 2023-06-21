using Core.Help;

namespace Core.Define
{
    public static class L2SystemDef
    {

        public static string CPLSysNumber = IniSystemHelper.Instance.LineNo;
        public static string L2 = "P" + CPLSysNumber;


        public enum SKPOS
        {
            Entry_SK01, Entry_SK02, EntryTOP, Delivery_SK01, Delivery_SK02, DeliveryTop, Entry_Car, POR, TR, Delivery_Car
        }
      
        // 機主號
        public static string SystemIDCode = "P"+ CPLSysNumber;
        public static string CPLGroup = "CPL"+ CPLSysNumber;       
        public const string AutoInputOn = "1";
        public const string AutoInputOff = "0";


        // 產線機組號定義
        public static string CPL1 = "P1";
        public static string CPL2 = "P2";


    }
}
