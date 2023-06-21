using Core.Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMod.WMS.LogicModel
{
    /// <summary>
    /// 產線入料/出料/退料要求資料
    /// </summary>
    [Serializable]
    public class ProdLineCoilReq
    {
        public string Flag { get; set; } = "";
        public string CoilNo { get; set; } = "";
        public string Spare { get; set; } = "";
        public string Pos { get; set;  } = "";
        public string CoilTurn { get; set; } = "";

        public ProdLineCoilReq(string flag, string coilNo, string pos = "", string spare = "", string direction = "")
        {
            Flag = flag;
            CoilNo = coilNo;
            Spare = spare;
            Pos = pos;

            /*
             * 2:上開捲且帶尾朝南
             * 3:上開捲且帶尾朝北
             * 4:下開捲且帶尾朝南
             * 5:下開捲且帶尾朝北
             */
            switch (direction.ToUpper())
            {
                case "U": CoilTurn = "2"; break;
                case "L": CoilTurn = "5"; break;
            }
        }


        public string ActionStr
        {
            get
            {

                var action = string.Empty;

                switch (Flag)
                {

                    case CoilDef.ReqWMSDeliveryCoil:
                        action = "出料";
                        break;


                    case CoilDef.ReqWMSRejectCoil:
                        action = "回退";
                        break;

                    default:

                        action = "入料";
                        break;
                }


                return action;

            }
        }
    }
}
