using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CPL1
{
    public class MessageFactory
    {
        #region Data convert to byte array
        /// <summary>
        ///     Get the byte array of the check swap address
        /// </summary>
        /// <param name="bytes"> Pending byte array </param>
        /// <param name="isSwap"> Swap flag (true：swap，false：no swap) </param>
        /// <returns></returns>
        private static byte[] GetBytes(byte[] bytes, bool isSwap)
        {
            if (isSwap) Array.Reverse(bytes);
            return bytes;
        }


        /// <summary>
        ///     Get the byte array of the short
        /// </summary>
        /// <param name="i16"> Pending short </param>
        /// <param name="isSwap"> Swap flag (default false) </param>
        /// <returns></returns>
        public static byte[] ShortToBytes(short i16, bool isSwap = false)
        {
            byte[] bytes = BitConverter.GetBytes(i16);
            return GetBytes(bytes, isSwap);
        }


        /// <summary>
        ///     Get the byte array of the ushort
        /// </summary>
        /// <param name="ui16"> Pending ushort </param>
        /// <param name="isSwap"> Swap flag (default false) </param>
        /// <returns></returns>
        public static byte[] UShortToBytes(ushort ui16, bool isSwap = false)
        {
            byte[] bytes = BitConverter.GetBytes(ui16);
            return GetBytes(bytes, isSwap);
        }


        /// <summary>
        ///     Get the byte array of the int
        /// </summary>
        /// <param name="i32"> Pending int </param>
        /// <param name="isSwap"> Swap flag (default false) </param>
        /// <returns></returns>
        public static byte[] IntToBytes(int i32, bool isSwap = false)
        {
            byte[] bytes = BitConverter.GetBytes(i32);
            return GetBytes(bytes, isSwap);
        }


        /// <summary>
        ///     Get the byte array of the uint
        /// </summary>
        /// <param name="ui32"> Pending uint </param>
        /// <param name="isSwap"> Swap flag (default false) </param>
        /// <returns></returns>
        public static byte[] UIntToBytes(uint ui32, bool isSwap = false)
        {
            byte[] bytes = BitConverter.GetBytes(ui32);
            return GetBytes(bytes, isSwap);
        }


        /// <summary>
        ///     Get the byte array of the single
        /// </summary>
        /// <param name="sig"> Pending single </param>
        /// <param name="isSwap"> Swap flag (default false) </param>
        /// <returns></returns>
        public static byte[] SingleToBytes(Single sig, bool isSwap = false)
        {
            byte[] bytes = BitConverter.GetBytes(sig);
            return GetBytes(bytes, isSwap);
        }


        /// <summary>
        ///     Get the byte array of the string
        /// </summary>
        /// <param name="str"> Pending string </param>
        /// <returns></returns>
        public static byte[] StringToBytes(string str)
        {
            return Encoding.Default.GetBytes(str);
        }
        #endregion


        #region Byte array convert to data
        /// <summary>
        ///     Check if swap byte array and convert to short
        /// </summary>
        /// <param name="bytes"> Pending byte array </param>
        /// <param name="isSwap"> Swap flag (true：swap，false：no swap) </param>
        /// <returns></returns>
        public static short BytesToShort(byte[] bytes, bool isSwap = false)
        {
            if (isSwap) Array.Reverse(bytes);
            return BitConverter.ToInt16(bytes, 0);
        }


        /// <summary>
        ///     Check if swap byte array and convert to ushort
        /// </summary>
        /// <param name="bytes"> Pending byte array </param>
        /// <param name="isSwap"> Swap flag (true：swap，false：no swap) </param>
        /// <returns></returns>
        public static ushort BytesToUShort(byte[] bytes, bool isSwap = false)
        {
            if (isSwap) Array.Reverse(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }


        /// <summary>
        ///     Check if swap byte array and convert to int
        /// </summary>
        /// <param name="bytes"> Pending byte array </param>
        /// <param name="isSwap"> Swap flag (true：swap，false：no swap) </param>
        /// <returns></returns>
        public static int BytesToInt(byte[] bytes, bool isSwap = false)
        {
            if (isSwap) Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }


        /// <summary>
        ///     Check if swap byte array and convert to uint
        /// </summary>
        /// <param name="bytes"> Pending byte array </param>
        /// <param name="isSwap"> Swap flag (true：swap，false：no swap) </param>
        /// <returns></returns>
        public static uint BytesToUInt(byte[] bytes, bool isSwap = false)
        {
            if (isSwap) Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }


        /// <summary>
        ///     Check if swap byte array and convert to single
        /// </summary>
        /// <param name="bytes"> Pending byte array </param>
        /// <param name="isSwap"> Swap flag (true：swap，false：no swap) </param>
        /// <returns></returns>
        public static Single BytesToSingle(byte[] bytes, bool isSwap = false)
        {
            if (isSwap) Array.Reverse(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }


        /// <summary>
        ///     Check if swap byte array and convert to string
        /// </summary>
        /// <param name="bytes"> Pending byte array </param>
        /// <returns></returns>
        public static string BytesToString(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }
        #endregion


        #region Transfer class to list
        /// <summary>
        ///     Transfer class to list
        /// </summary>
        /// <typeparam name="T"> Input type </typeparam>
        /// <param name="listByte"></param>
        /// <param name="t"></param>
        /// <param name="ignore"></param>
        public static void ClassToListbyte<T>(ref List<byte> listByte, T t, string[] ignore) where T : new()
        {
            foreach (PropertyInfo prop in t.GetType().GetProperties())
            {
                if (Array.IndexOf(ignore, prop.Name) >= 0) { }
                else
                {
                    switch (prop.PropertyType)
                    {
                        case Type shortType when shortType == typeof(short):
                            {   //  short
                                listByte.AddRange(ShortToBytes((short)prop.GetValue(t), true));
                                break;
                            }
                        case Type ushortType when ushortType == typeof(ushort):
                            {   //  ushort
                                listByte.AddRange(UShortToBytes((ushort)prop.GetValue(t), true));
                                break;
                            }
                        case Type intType when intType == typeof(int):
                            {   //  int
                                listByte.AddRange(IntToBytes((int)prop.GetValue(t), true));
                                break;
                            }
                        case Type uintType when uintType == typeof(uint):
                            {   //  uint
                                listByte.AddRange(UIntToBytes((uint)prop.GetValue(t), true));
                                break;
                            }
                        case Type singleType when singleType == typeof(Single):
                            {   //  Single
                                listByte.AddRange(SingleToBytes((Single)prop.GetValue(t), true));
                                break;
                            }
                        case Type strType when strType == typeof(string):
                            {   //  string
                                listByte.AddRange(StringToBytes(prop.GetValue(t).ToString()));
                                break;
                            }
                    }
                }
            }
        }
        #endregion


        #region Bit data handler
        /// <summary>
        ///     Get the length of the byte array converted to bits (1 byte = 8 bits)
        /// </summary>
        /// <param name="bytes"> Pending byte array </param>
        /// <returns></returns>
        public static int GetBitsLength(byte[] bytes)
        {
            return bytes.Count() * 8;
        }


        /// <summary>
        ///     Display the contents of the byte array converted to bits
        /// </summary>
        /// <param name="bytes"> Pending byte array </param>
        /// <returns></returns>
        public static string DisplayBits(byte[] bytes)
        {
            string str = "";

            for (int idx = 0; idx < bytes.Count() * 8; idx++)
                str += GetBitValue(bytes, idx).ToString();

            return str;
        }


        /// <summary>
        ///     Display the contents of the short converted to bits
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string DisplayBits(short s)
        {
            byte[] bytes = BitConverter.GetBytes(s);

            return DisplayBits(bytes);
        }


        /// <summary>
        ///     Display the contents of the integer converted to bits
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string DisplayBits(int i)
        {
            byte[] bytes = BitConverter.GetBytes(i);

            return DisplayBits(bytes);
        }


        /// <summary>
        ///     Get the value of the bit of the specified index in the byte array
        /// </summary>
        /// <param name="bytes"> Pending byte array </param>
        /// <param name="bitIdx"> Bit index in the byte array </param>
        /// <returns></returns>
        public static int GetBitValue(byte[] bytes, int bitIdx)
        {
            //  99 = error
            if (bitIdx < 0 || bitIdx >= GetBitsLength(bytes)) return 99;

            byte[] bisMask = new byte[] { 0x80, 0x40, 0x20, 0x10, 0x8, 0x4, 0x2, 0x1 };
            int whichByte = bitIdx / 8;
            int whichBit = bitIdx % 8;

            return ((bytes[whichByte] & bisMask[whichBit]) > 0) ? 1 : 0;
        }


        /// <summary>
        ///     Get the bit value of the specified index in the byte array of converted from short
        /// </summary>
        /// <param name="s"> Pending short </param>
        /// <param name="bitIdx"> Bit index in the byte array </param>
        /// <returns></returns>
        public static int GetBitValue(short s, int bitIdx)
        {
            byte[] bytes = BitConverter.GetBytes(s);
            Array.Reverse(bytes);
            return GetBitValue(bytes, bitIdx);
        }


        /// <summary>
        ///     Get the bit value of the specified index in the byte array of converted from int
        /// </summary>
        /// <param name="i"> Pending int </param>
        /// <param name="bitIdx"> Bit index in the byte array </param>
        /// <returns></returns>
        public static int GetBitValue(int i, int bitIdx)
        {
            byte[] bytes = BitConverter.GetBytes(i);
            Array.Reverse(bytes);
            return GetBitValue(bytes, bitIdx);
        }


        public void SetBitValue(ref byte[] bytes, int bitIdx, int bitVal)
        {

        }
        #endregion


        #region Other method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"> Input value </param>
        /// <param name="fixedLen"> Result length </param>
        /// <param name="padWord"> Default padded with space </param>
        /// <returns></returns>
        public static string GetFixedString(string str, int fixedLen, string padWord = " ")
        {
            if (fixedLen == str.Length)
            {
                return str;
            }
            else if (fixedLen < str.Length)
            {
                return str.Substring(0, fixedLen);
            }
            else
            {
                if (string.IsNullOrEmpty(padWord)) padWord = " ";

                while (str.Length < fixedLen) str += padWord;

                return str.Substring(0, fixedLen);
            }
        }


        public static string DumpByteInfo(byte[] bytes)
        {
            string tmpHexString = "";
            string tmpAsciiString = "";
            string retString = Environment.NewLine + "-----------------------------------------------------------------" + Environment.NewLine;

            for (int idx = 0; idx < bytes.Length; idx++)
            {
                string tmpStr = bytes[idx].ToString("X");

                if (tmpStr.Length == 1) tmpStr = "0" + tmpStr;

                tmpHexString += " " + tmpStr;

                if (bytes[idx] < 20)
                {
                    tmpAsciiString += ".";
                }
                else
                {
                    tmpAsciiString += Encoding.ASCII.GetString(bytes, idx, 1);
                }

                if (idx % 16 == 15)
                {
                    retString += tmpHexString + " " + tmpAsciiString + Environment.NewLine;
                    tmpHexString = "";
                    tmpAsciiString = "";
                }
            }

            if ((bytes.Length - 1) % 16 != 15)
            {
                for (int idx = 0; idx < 15 - ((bytes.Length - 1) % 16); idx++)
                {
                    tmpHexString += "   ";
                    tmpAsciiString += " ";
                }

                retString += tmpHexString + " " + tmpAsciiString + Environment.NewLine;
            }
            retString += "-----------------------------------------------------------------" + Environment.NewLine;
            return retString;
        }
        #endregion
    }
}
