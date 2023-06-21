using DBService.Base;
using System;
using static Core.Define.L2SystemDef;

namespace DBService.Repository
{
    public class CoilMapEntity
    {
        [Serializable]
        public class TBL_CoilMap : BaseRepositoryModel
        {

            public override DateTime UpdateTime { get; set; }
            public string Entry_Car { get; set; }
            public string Entry_TOP { get; set; }
            public string Entry_SK01 { get; set; }
            public string Entry_SK02 { get; set; }
            public string POR { get; set; }
            public string TR { get; set; }
            public string Delivery_SK01 { get; set; }
            public string Delivery_SK02 { get; set; }
            public string Delivery_TOP { get; set; }
            public string Delivery_Car { get; set; }
          
            public int LineStatus_Entry { get; set; }
            public int LineStatus_CPL { get; set; }
            public int LineStatus_Exit { get; set; }


            public bool CompareMap(string coilID, SKPOS pos)
            {
                bool isTheSame = true;

                switch(pos){
                    case SKPOS.Entry_Car:
                        isTheSame = this.Entry_Car.Trim().Equals(coilID);
                        break;
                    case SKPOS.Entry_SK01:
                        isTheSame = this.Entry_SK01.Trim().Equals(coilID);
                        break;
                    case SKPOS.Entry_SK02:
                        isTheSame = this.Entry_SK02.Trim().Equals(coilID);
                        break;
                    case SKPOS.EntryTOP:
                        isTheSame = this.Entry_TOP.Trim().Equals(coilID);
                        break;
                    case SKPOS.Delivery_SK01:
                        isTheSame = this.Delivery_SK01.Trim().Equals(coilID);
                        break;
                    case SKPOS.Delivery_SK02:
                        isTheSame = this.Delivery_SK02.Trim().Equals(coilID);
                        break;
                    case SKPOS.DeliveryTop:
                        isTheSame = this.Delivery_TOP.Trim().Equals(coilID);
                        break;
                    case SKPOS.Delivery_Car:
                        isTheSame = this.Delivery_Car.Trim().Equals(coilID);
                        break;
                }
                return isTheSame;
            }
            public string GetCoilNoFromPOS(SKPOS pos)
            {
                string coilNo = "";

                switch (pos)
                {
                    case SKPOS.Entry_Car:
                        coilNo = Entry_Car.Trim();
                        break;
                    case SKPOS.Entry_SK01:
                        coilNo = Entry_SK01.Replace(" ",string.Empty);
                        break;
                    case SKPOS.Entry_SK02:
                        coilNo = Entry_SK02.Trim();
                        break;
                    case SKPOS.EntryTOP:
                        coilNo = Entry_TOP.Trim();
                        break;
                    case SKPOS.Delivery_SK01:
                        coilNo = Delivery_SK01.Trim();
                        break;
                    case SKPOS.Delivery_SK02:
                        coilNo = Delivery_SK02.Trim();
                        break;
                    case SKPOS.DeliveryTop:
                        coilNo = Delivery_TOP.Trim();
                        break;
                    case SKPOS.Delivery_Car:
                        coilNo = Delivery_Car.Trim();
                        break;
                }
                return coilNo;
            }
            public bool IsPosEmpty(SKPOS pos)
            {
                bool isPosEmpty = true;

                switch (pos)
                {
                    case SKPOS.Entry_Car:
                        isPosEmpty = Entry_Car.Trim().Equals(string.Empty);
                        break;
                    case SKPOS.Entry_SK01:
                        isPosEmpty = Entry_SK01.Trim().Equals(string.Empty);
                        break;
                    case SKPOS.Entry_SK02:
                        isPosEmpty = Entry_SK02.Trim().Equals(string.Empty);
                        break;
                    case SKPOS.EntryTOP:
                        isPosEmpty = Entry_TOP.Trim().Equals(string.Empty);
                        break;

                }

                return isPosEmpty;
            }

        }     
    }
}
