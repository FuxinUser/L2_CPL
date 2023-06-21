using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Util
{
    public static class StringUtil
    {
        /// <summary>
        ///     添加字符位置
        /// </summary>
        public enum AddChar { Left, Right }

        /// <summary>
        ///     擷取中間 (擷取到最後)
        /// </summary>
        /// <param name="value"> 來源值 </param>
        /// <param name="startIdx"> 起始索引 </param>
        public static string Mid(this string value, int startIdx)
            => new string(value.Skip(startIdx)
                               .Take(value.Length - startIdx)
                               .ToArray());

        /// <summary>
        ///     擷取中間
        /// </summary>
        /// <param name="value"> 來源值 </param>
        /// <param name="startIdx"> 起始索引 </param>
        /// <param name="takeLen"> 擷取的長度 </param>
        public static string Mid(this string value, int startIdx, int takeLen)
            => new string(value.Skip(startIdx)
                               .Take(takeLen)
                               .ToArray());

        /// <summary>
        ///     擷取左邊
        /// </summary>
        /// <param name="value"> 來源值 </param>
        /// <param name="takeLen"> 擷取的長度 </param>
        public static string Left(this string value, int takeLen)
            => new string(value.Take(takeLen)
                               .ToArray());

        /// <summary>
        ///     擷取右邊
        /// </summary>
        /// <param name="value"> 來源值 </param>
        /// <param name="takeLen"> 擷取的長度 </param>
        public static string Right(this string value, int takeLen)
            => new string(value.Skip(value.Length - takeLen)
                               .ToArray());

        /// <summary>
        /// 根據固定長度做分切
        /// </summary>
        /// <param name="str">需分切字串</param>
        /// <param name="chunkSize">分切每筆長度為多少</param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<string> StrSplitBySpecificLength(this string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize).Select(i => str.Substring(i * chunkSize, chunkSize));
        }
        /// <summary>
        /// 字串中刪除無效的字元
        /// </summary>
        public static string CleanInvalidChar(this string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", "",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters, 
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }

        // 型態轉換
        public static T? ToNullable<T>(this string s) where T : struct
        {
            T? result = new T?();
            try
            {
                if (!string.IsNullOrWhiteSpace(s))
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
                    result = (T)conv.ConvertFrom(s);
                }
            }
            catch { }
            return result != null ? result : default(T);
        }
        public static T? ToNullable<T>(this char[] s) where T : struct
        {
            var str = new string(s);

            T? result = new T?();
            try
            {
                if (!string.IsNullOrWhiteSpace(str))
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
                    result = (T)conv.ConvertFrom(str);
                }
            }
            catch { }
            return result != null ? result : default(T);
        }

        // To Char Type
        public static char[] ToNChar(this string data, int totalWidth)
        {
            try
            {
                return data.PadLeft(totalWidth, '0').ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.CleanInvalidChar());
                return "".PadLeft(totalWidth, '0').ToArray(); ;
            }
        }
        public static char[] ToNChar(this float data, int totalWidth)
        {
            try
            {
                return data.ToString().PadLeft(totalWidth, '0').ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.CleanInvalidChar());
                return "".PadLeft(totalWidth, '0').ToArray(); ;
            }
        }
        public static char[] ToNChar(this int data, int totalWidth)
        {
            try
            {
                return data.ToString().PadLeft(totalWidth, '0').ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.CleanInvalidChar());
                return "".PadLeft(totalWidth, '0').ToArray(); ;
            }
        }
        public static char[] ToCChar(this string data, int totalWidth)
        {
            try
            {
                return data.PadRight(totalWidth, ' ').ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.CleanInvalidChar());
                return "".PadRight(totalWidth, ' ').ToArray(); ;
            }
        }
        public static char[] ToCChar(this int data, int totalWidth)
        {
            try
            {
                return data.ToString().PadRight(totalWidth, ' ').ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "".PadRight(totalWidth, ' ').ToArray(); ;
            }
        }
        // To Str Type
        public static string ToStr(this char[] data)
        {
            return new string(data).Trim();
        }

        public static string ToStr(this byte[] data)
        {
            // EnCode UTF8
            return Encoding.UTF8.GetString(data).Trim('\0').Trim();            
        }


        public static string ToStr(this bool isOk)
        {
            return isOk ? "成功" : "失敗";
        }
        public static string ToHasStr(this bool doesHave)
        {
            return doesHave ? "有" : "無";
        }

        public static byte[] ToByteArray(this string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }

        public static byte[] ToASSICByteArray(this string data)
        {
            return Encoding.ASCII.GetBytes(data);
        }

        public static byte[] ToCByteArray(this string data, int size)
        {
            return Encoding.UTF8.GetBytes(data.PadRight(size, ' '));
        }

        public static byte[] ToNByteArray(this string data, int size)
        {
            return Encoding.UTF8.GetBytes(data.PadLeft(size, '0'));
        }

        public static byte[] ToNByteArray(this int data, int size)
        {
            return Encoding.UTF8.GetBytes(data.ToString().PadLeft(size, '0'));
        }

        public static byte[] ToNByteArray(this float data, int size, int multiple = 1)
        {     
            var multipleValue = Convert.ToInt32(data * multiple);
            return Encoding.UTF8.GetBytes(multipleValue.ToString().PadLeft(size, '0'));
        }

        public static byte[] ToL1EndSignByteArray(this byte[] data, int size)
        {
            var signByte = Encoding.UTF8.GetBytes("".PadRight(size, '\0'));
            var listByte = data.ToList();
            listByte.AddRange(signByte);

            return listByte.ToArray();
        }

        /// <summary>
        ///     固定長度(補字元)
        /// </summary>
        /// <param name="value"> 來源值 </param>
        /// <param name="fixedLen"> 輸出長度 </param>
        /// <param name="charSpace"> 補間隔字元 </param>
        /// <param name="isRight"> 是否補字串右方，是：右，否：左 </param>
        public static string FixedLen(this string value, int fixedLen, char charSpace = ' ', AddChar addChar = AddChar.Right)
        {
            switch (value.Length)
            {
                //  值的長度 > 指定長度 => 擷取指定長度
                case var valLen when valLen > fixedLen:
                    {
                        return new string(value.Take(fixedLen).ToArray());
                    }
                //  值的長度 < 指定長度 => 補左或補右
                case var valLen when valLen < fixedLen:
                    {
                        switch (addChar)
                        {
                            case AddChar.Right: return value.PadRight(fixedLen, charSpace);
                            case AddChar.Left: return value.PadLeft(fixedLen, charSpace);
                            default: return value;
                        }
                    }
                //  無符合條件 => 不處理
                default:
                    {
                        return value;
                    }
            }
        }
    }
}
