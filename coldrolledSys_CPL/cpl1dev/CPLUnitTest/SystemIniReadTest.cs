using Core.Define;
using Core.Help;
using Core.Util;
using NUnit.Framework;

namespace CPLUnitTest
{
    public class SystemIniReadTest
    {
        [Test]
        public void ReadSystemIniFile()
        {
            
            Assert.IsFalse(string.IsNullOrEmpty(IniSystemHelper.Instance.DBConn));
            Assert.IsFalse(string.IsNullOrEmpty(IniSystemHelper.Instance.HisDBConn));

            Assert.IsFalse(string.IsNullOrEmpty(IniSystemHelper.Instance.MMSApp));
            Assert.IsFalse(string.IsNullOrEmpty(IniSystemHelper.Instance.MMSLocalIP));
            Assert.IsTrue(IniSystemHelper.Instance.MMSLocalPort!=0);
            Assert.IsFalse(string.IsNullOrEmpty(IniSystemHelper.Instance.MMSRemoteIP));
            Assert.IsTrue(IniSystemHelper.Instance.MMSRemotePort != 0);

            Assert.IsFalse(string.IsNullOrEmpty(IniSystemHelper.Instance.WMSApp));
            Assert.IsFalse(string.IsNullOrEmpty(IniSystemHelper.Instance.WMSLocalIP));
            Assert.IsTrue(IniSystemHelper.Instance.WMSLocalPort != 0);
            Assert.IsFalse(string.IsNullOrEmpty(IniSystemHelper.Instance.WMSRemoteIP));
            Assert.IsTrue(IniSystemHelper.Instance.WMSRemotePort != 0);

            Assert.IsFalse(string.IsNullOrEmpty(IniSystemHelper.Instance.PLCApp));
            Assert.IsFalse(string.IsNullOrEmpty(IniSystemHelper.Instance.PLCLocalIP));
            Assert.IsTrue(IniSystemHelper.Instance.PLCLocalPort != 0);
            Assert.IsFalse(string.IsNullOrEmpty(IniSystemHelper.Instance.PLCRemoteIP));
            Assert.IsTrue(IniSystemHelper.Instance.PLCRemotePort != 0);

        }

    }
}
