using System;
using System.Reflection;
using System.Text;

namespace CPLUnitTest
{
    public static class UnitTestHelp
    {
      
        public static  object LoadMockMsgData<T>(this object data)
        {
            //產生ORM實例:
            T orm = Activator.CreateInstance<T>();
            var t = typeof(T);

            foreach (var fi in t.GetFields())
            {
                var fiType = fi.FieldType;

                if(fiType == typeof(byte[]))
                {
                    var rnd = new Random();
                    var mockData = rnd.Next(1,10).ToString();
                    var mockByteData = Encoding.UTF8.GetBytes(mockData);
                    fi.SetValue(orm, mockByteData);
                    continue;
                }

                if(fiType == typeof(int))
                {
                    var rnd = new Random();
                    var mockData = rnd.Next(1, 10);
                    fi.SetValue(orm, mockData);
                    continue;
                }

                if (fiType == typeof(short))
                {
                    var rnd = new Random();
                    var mockData = rnd.Next(1, 10);
                    fi.SetValue(orm, (short)mockData);
                    continue;
                }

                if (fiType == typeof(float))
                {
                    var rnd = new Random();
                    var mockData = rnd.Next(1, 10);
                    fi.SetValue(orm, mockData);
                    continue;
                }

                if (fiType == typeof(double))
                {
                    var rnd = new Random();
                    var mockData = rnd.Next(1, 10);
                    fi.SetValue(orm, mockData);
                    continue;
                }

                if (fiType == typeof(string))
                {
                    var rnd = new Random();
                    var mockData = rnd.Next(1, 10).ToString();
                    fi.SetValue(orm, mockData);
                    continue;
                }

            }

        
            return orm;
        }

        public static object LoadMockDBData<T>(this object data)
        {
            //產生ORM實例:
            T orm = Activator.CreateInstance<T>();
            var t = typeof(T);
        
            foreach (var fi in t.GetProperties())
            {
                var propertyType = fi.PropertyType;

                if (propertyType == typeof(byte[]))
                {
                    var rnd = new Random();
                    var mockData = rnd.Next(1, 10).ToString();
                    var mockByteData = Encoding.UTF8.GetBytes(mockData);
                    fi.SetValue(orm, mockByteData);
                    continue;
                }

                if (propertyType == typeof(int))
                {
                    var rnd = new Random();
                    var mockData = rnd.Next(1, 10);
                    fi.SetValue(orm, mockData);
                    continue;
                }

                if (propertyType == typeof(float))
                {
                    var rnd = new Random();
                    var mockData = rnd.Next(1, 10);
                    fi.SetValue(orm, mockData);
                    continue;
                }

                if (propertyType == typeof(double))
                {
                    var rnd = new Random();
                    var mockData = rnd.Next(1, 10);
                    fi.SetValue(orm, mockData);
                    continue;
                }

                if (propertyType == typeof(string))
                {
                    var rnd = new Random();
                    var mockData = rnd.Next(1, 10).ToString();
                    fi.SetValue(orm, mockData);
                    continue;
                }
            }

            return orm;
        }

        public static byte[] ToByteData(this string txt)
        {
            return Encoding.UTF8.GetBytes(txt);
        }

    }
}
