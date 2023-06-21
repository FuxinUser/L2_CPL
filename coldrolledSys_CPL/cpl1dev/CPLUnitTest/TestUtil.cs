using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace CPLUnitTest
{
    public class TestUtil
    {

        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly TestUtil INSTANCE = new TestUtil();
        }

        public static TestUtil Instance { get { return SingletonHolder.INSTANCE; } }

        public Dictionary<string, string> GetMsgTestDataSetFromXml(string xmlPath)
        {
            Dictionary<string, string> msgDic = new Dictionary<string, string>();
            string xmlFile = File.ReadAllText(xmlPath);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xmlFile);

            var selectnode = "Scenario/TestStep";
            var nodes = xmldoc.SelectNodes(selectnode);
            foreach (XmlNode nod in nodes)
            {
                string target = nod["TestTarget"].InnerText;
                string data = nod["TestData"].InnerText;
                msgDic.Add(target, data);
            }
            return msgDic;
        }

        public object CreateObjData(object output_of_t, Type t, string[] data_of_t, ref int iCursor, int iDimension = 1)
        {
            Int32 iSizeConst = 0;
            Type FieldType = null;
            //string strProcessDateTime = "";

            if (t.Name.Contains("[]"))
            {
                FieldType = Type.GetType(t.FullName.Replace("[]", ""));
                if (FieldType == null)
                {
                    FieldType = t.Assembly.GetType(t.FullName.Replace("[]", ""));
                }

                Array outd = Array.CreateInstance(FieldType, iDimension);
                for (int iii = 0; iii <= iDimension - 1; iii++)
                {
                    outd.SetValue(CreateObjData(outd.GetValue(iii), FieldType, data_of_t, ref iCursor), iii);
                }
                return outd;
            }
            else
            {
                foreach (FieldInfo fi in t.GetFields())
                {
                    FieldType = fi.FieldType;
                    iSizeConst = 0;

                    if (fi.FieldType.IsArray && !fi.Name.ToUpper().Contains("SPARE"))
                    {
                        FieldType = Type.GetType(fi.FieldType.FullName.Replace("[]", ""));
                        if (FieldType == null)
                        {
                            FieldType = fi.FieldType.Assembly.GetType(fi.FieldType.FullName.Replace("[]", ""));
                        }

                        foreach (CustomAttributeNamedArgument NA1 in fi.CustomAttributes.ElementAt(0).NamedArguments)
                        {
                            if (NA1.MemberName == "SizeConst")
                            {
                                iSizeConst = Convert.ToInt32(NA1.TypedValue.Value);
                                break;
                            }
                        }

                        for (int ii = 0; ii <= iSizeConst - 1; ii++)
                        {
                            if (FieldType == typeof(int) || FieldType == typeof(short) || FieldType == typeof(long) || FieldType == typeof(Single) || FieldType == typeof(Double))
                            {
                                FieldInfo fii = t.GetField(fi.Name, BindingFlags.Public | BindingFlags.Instance);

                                if (FieldType == typeof(int))
                                {
                                    int.TryParse(data_of_t[iCursor], out int tmp);
                                    fii.SetValueDirect(__makeref(output_of_t), tmp);
                                }
                                else if (FieldType == typeof(short))
                                {
                                    short.TryParse(data_of_t[iCursor], out short tmp);
                                    fii.SetValueDirect(__makeref(output_of_t), tmp);
                                }
                                else if (FieldType == typeof(long))
                                {
                                    long.TryParse(data_of_t[iCursor], out long tmp);
                                    fii.SetValueDirect(__makeref(output_of_t), tmp);
                                }
                                else if (FieldType == typeof(Single))
                                {
                                    Single.TryParse(data_of_t[iCursor], out Single tmp);
                                    fii.SetValueDirect(__makeref(output_of_t), tmp);
                                }
                                else if (FieldType == typeof(Double))
                                {
                                    Double.TryParse(data_of_t[iCursor], out double tmp);
                                    fii.SetValueDirect(__makeref(output_of_t), tmp);
                                }

                                iCursor++;
                            }
                            else if (FieldType == typeof(char))
                            {
                                if (ii == iSizeConst - 1)
                                {
                                    char[] tmpstring = (char[])Array.CreateInstance(typeof(char), iSizeConst);
                                    Array.Copy(data_of_t[iCursor].ToCharArray(), tmpstring, data_of_t[iCursor].ToCharArray().Length);
                                    FieldInfo fii = t.GetField(fi.Name, BindingFlags.Public | BindingFlags.Instance);
                                    fii.SetValueDirect(__makeref(output_of_t), tmpstring);
                                    iCursor++;
                                }
                            }
                            else
                            {
                                if (ii == iSizeConst - 1)
                                {
                                    FieldInfo fii = t.GetField(fi.Name, BindingFlags.Public | BindingFlags.Instance);
                                    var obj = CreateObjData(fii.GetValueDirect(__makeref(output_of_t)), fii.FieldType, data_of_t, ref iCursor, iSizeConst);
                                    fii.SetValueDirect(__makeref(output_of_t), obj);
                                }

                            }
                        }
                    }
                    else
                    {
                        if (fi.Name.ToUpper().Contains("SPARE"))
                        {
                            //strColName = $"{fi.Name}";
                            //dt.Columns.Add(new DataColumn(strColName));
                        }
                        else
                        {
                            if (FieldType == typeof(int) || FieldType == typeof(short) || FieldType == typeof(long) || FieldType == typeof(Single) || FieldType == typeof(Double))
                            {
                                FieldInfo fii = t.GetField(fi.Name, BindingFlags.Public | BindingFlags.Instance);

                                if (FieldType == typeof(int))
                                {
                                    int.TryParse(data_of_t[iCursor], out int tmp);
                                    fii.SetValueDirect(__makeref(output_of_t), tmp);
                                }
                                else if (FieldType == typeof(short))
                                {
                                    short.TryParse(data_of_t[iCursor], out short tmp);
                                    fii.SetValueDirect(__makeref(output_of_t), tmp);
                                }
                                else if (FieldType == typeof(long))
                                {
                                    long.TryParse(data_of_t[iCursor], out long tmp);
                                    fii.SetValueDirect(__makeref(output_of_t), tmp);
                                }
                                else if (FieldType == typeof(Single))
                                {
                                    Single.TryParse(data_of_t[iCursor], out Single tmp);
                                    fii.SetValueDirect(__makeref(output_of_t), tmp);
                                }
                                else if (FieldType == typeof(Double))
                                {
                                    Double.TryParse(data_of_t[iCursor], out double tmp);
                                    fii.SetValueDirect(__makeref(output_of_t), tmp);
                                }

                                iCursor++;
                            }
                            else
                            {
                                FieldInfo fii = t.GetField(fi.Name, BindingFlags.Public | BindingFlags.Instance);
                                var oo = fii.GetValueDirect(__makeref(output_of_t));
                                if (oo == null)
                                {
                                    oo = Activator.CreateInstance(fii.FieldType);
                                }
                                object obj = CreateObjData(oo, fii.FieldType, data_of_t, ref iCursor);
                                //object obj = CreateDataToSend(fii.GetValueDirect(__makeref(output_of_t)), fii.FieldType, data_of_t, ref iCursor);
                                fii.SetValueDirect(__makeref(output_of_t), obj);
                            }
                        }
                    }
                }
            }
            return output_of_t;
        }

        public  T CreateObjFromTestData<T>(string[] testData)
        {           
            FieldInfo[] fields = typeof(T).GetFields();
            var obj = Activator.CreateInstance<T>();

            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i].Name.ToUpper().StartsWith("SPARE"))
                {

                    if (fields[i].FieldType == typeof(char[]))
                    {
                        var marshal = fields[i].GetCustomAttribute<MarshalAsAttribute>();
                        fields[i].SetValue(obj, "".PadRight(marshal.SizeConst).ToCharArray());
                    }
                    else
                    {
                        fields[i].SetValue(obj, 0);
                    }
                    continue;

                    //var marshal = fields[i].GetCustomAttribute<MarshalAsAttribute>();
                    //fields[i].SetValue(obj, "".PadRight(marshal.SizeConst).ToCharArray());
                    //continue;
                }

                switch (fields[i].FieldType.ToString())
                {
                    case "System.Int16":
                        fields[i].SetValue(obj, short.Parse(testData[i]));
                        break;
                    case "System.Int32":
                        fields[i].SetValue(obj, int.Parse(testData[i]));
                        break;
                    case "System.Single":
                        fields[i].SetValue(obj, float.Parse(testData[i]));
                        break;
                    case "System.Char[]":
                        var marshal = fields[i].GetCustomAttribute<MarshalAsAttribute>();
                        fields[i].SetValue(obj, testData[i].PadRight(marshal.SizeConst).ToCharArray());
                        break;
                }
            }
            return obj;
        }


    

    }
}
