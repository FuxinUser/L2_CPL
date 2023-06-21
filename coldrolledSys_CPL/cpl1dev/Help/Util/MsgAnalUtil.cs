using Akka.IO;
using System;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

/**
 * Author: ICSC余士鵬 (Modfiy From 陳志銘學長)
 * Date: 2019/9/24
 * Description: Message Analysis解析 
 * Reference: 
 * Modified: 
 */
namespace Core.Util
{
    public static class MsgAnalUtil
    {
        public static ByteString ToByteString(this byte[] data)
        {
            return ByteString.FromBytes(data);
        }


       
        /// <summary>
        /// 取得電文的Msg ID
        /// </summary>
        /// <param name="data">電文資料</param>
        /// <returns></returns>
        public static string GetMsgID(byte[] data)
        {
            string strResult = "";
            byte[] tmp = { 0x00, 0x00 };

            if (data.Length >= 4)
            {
                Buffer.BlockCopy(data, 2, tmp, 0, 2);
                Array.Reverse(tmp);
                strResult = BitConverter.ToInt16(tmp, 0).ToString("000");
            }

            return strResult;
        }

        public static object RawDeserialize(this byte[] byteData, Type type, bool is_ConvertEndian = false)
        {
            try
            {
                int rawsize = Marshal.SizeOf(type);

                if (rawsize > byteData.Length)
                    return null;

                // ' ==============反轉===================
                if (is_ConvertEndian == true)
                    ConvertEndian(byteData, type);
                // ' ====================================

                IntPtr buffer = Marshal.AllocHGlobal(rawsize);
                Marshal.Copy(byteData, 0, buffer, rawsize);
                object structureData = Marshal.PtrToStructure(buffer, type);

                // 釋放
                Marshal.FreeHGlobal(buffer);

                return structureData;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static byte[] RawSerialize(this object type, bool is_ConvertEndian = false)
        {
            try
            {
                int rawsize = Marshal.SizeOf(type);
                IntPtr buffer = Marshal.AllocHGlobal(rawsize);

                Marshal.StructureToPtr(type, buffer, false);

                byte[] byteData = new byte[rawsize - 1 + 1];

                Marshal.Copy(buffer, byteData, 0, rawsize);

                // 釋放
                Marshal.FreeHGlobal(buffer);

                // ' ===============反轉=================
                if (is_ConvertEndian == true)
                    ConvertEndian(byteData, type.GetType());
                // ' ====================================

                return byteData;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private static byte[] ConvertEndian(byte[] byteData, Type type, int iCursor = 0)
        {
            int iMLength = 0;
            bool bIsArray = false;
            int iSizeConst = 0;
            Type FieldType = null;

            try
            {
                foreach (FieldInfo fi in type.GetFields())
                {
                    FieldType = fi.FieldType;

                    //取得Attribute
                    var marshal = fi.GetCustomAttribute<MarshalAsAttribute>();
                    bIsArray = (marshal.Value == UnmanagedType.ByValArray);
                    iSizeConst = marshal.SizeConst;

                    if (bIsArray)
                    {
                        //FieldType
                        Type elementType = FieldType.GetElementType();
                        for (int i = 0; i < iSizeConst; i++)
                        {
                            switch (elementType)
                            {
                                case Type intType when intType == typeof(int):
                                case Type shortType when shortType == typeof(short):
                                case Type longType when longType == typeof(long):
                                case Type floatType when floatType == typeof(float):
                                case Type doubleType when doubleType == typeof(double):
                                    {
                                        // 需要進行轉換
                                        iMLength = Marshal.SizeOf(elementType);
                                        Array.Reverse(byteData, iCursor, iMLength);
                                        iCursor += iMLength;
                                        break;
                                    }
                                case Type charType when charType == typeof(byte):
                                    {
                                        iCursor += 1;
                                        break;
                                    }
                                case Type charType when charType == typeof(char):
                                    {
                                        iCursor += 1;
                                        break;
                                    }
                                case Type stringType when stringType == typeof(string):
                                    {
                                        break;
                                    }

                                default:
                                    {
                                        ConvertEndian(byteData, elementType, iCursor);
                                        break;
                                    }
                            }
                        }
                    }
                    else
                    {//非陣列
                        switch (FieldType)
                        {
                            case Type intType when intType == typeof(int):
                            case Type shortType when shortType == typeof(short):
                            case Type longType when longType == typeof(long):
                            case Type floatType when floatType == typeof(float):
                            case Type doubleType when doubleType == typeof(double):
                                {
                                    // 需要進行轉換
                                    iMLength = Marshal.SizeOf(FieldType);
                                    Array.Reverse(byteData, iCursor, iMLength);
                                    iCursor += iMLength;
                                    break;
                                }
                            case Type byteType when byteType == typeof(byte[]):
                            case Type charType when charType == typeof(char):
                            case Type stringType when stringType == typeof(string):
                                {
                                    iMLength = iSizeConst;
                                    iCursor += iMLength;
                                    break;
                                }

                            default:
                                {
                                    ConvertEndian(byteData, FieldType, iCursor);
                                    break;
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return byteData;
        }

        
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

        public static string DumpByteInfo(this byte[] bytes)
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

    }


}
