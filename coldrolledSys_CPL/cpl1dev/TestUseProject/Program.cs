using Controller.Coil;
using LogSender;
using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace TestUseProject
{
    [Serializable]
    //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class DK99
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] caDeviceName = new byte[6];
        [MarshalAs(UnmanagedType.I4)]
        public Int32 DevicePrice;
    }

    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("1*2+2*3...+(n-1)*n");
              Console.WriteLine("1*2+2*3...+(n-1)*n");

            //string tmpStr = "中文12";
            //// Str 轉 Byte
            //DK99 tmpObj = new DK99();            
            //tmpObj.caDeviceName = Encoding.UTF8.GetBytes(tmpStr.PadRight(6,' '));
            //tmpObj.DevicePrice = 123;

            //// UTF8編碼下，一個中文字會變成3bytes，故以此例子來說，tmpStr("中文12")後面的"12"在Serialize時會被截掉，因為caDeviceName長度定義只有6
            //byte[] tmpAry = tmpObj.RawSerialize();

            //// Byte 轉 Obj       
            //var newObj = (DK99)tmpAry.RawDeserialize(typeof(DK99));
            //var name = Encoding.UTF8.GetString(newObj.caDeviceName);

            //ICoilController coilController = new CoilController();
            //var log = new Log();
            //coilController.SetLog(log);

            //string txt = null;
            //TryFlow(() =>
            //{
            //    var hasPDI = coilController.HasPDIInfo(txt);
            //    Console.WriteLine($"Has PDI===================== {hasPDI}");
            //    //var result = TestMethod(txt);
            //    //Console.WriteLine($"Test Method Result===================== {result}");
            //});

            Console.ReadKey();

        }
        
        public int Recursive(int n)
        {
            if (n == 1)
                return 0;

            return n * (n - 1) + Recursive(n - 1);
        }


        public static int TestMethod(string txt)
        {
            try
            {
                if(txt.Equals(string.Empty))
                    return 1;

                return 0;
            }
            catch(Exception e)
            {
                throw new Exception(MethodBase.GetCurrentMethod().ReflectedType.Name + e.ToString());
            }
        }

        /// <summary>
        ///     Try action of flow
        /// </summary>
        public  static void TryFlow(Action action, bool @throw = false)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ex.Message={ex.Message}");
                Console.WriteLine($"ex.StackTrace={ex.StackTrace}");
                if (@throw) throw;
            }
            finally
            {
            }
        }

    }

    
}
