using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySecurityLib;

namespace MyUnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string result = Security.GetOTP("1", 5);
            Assert.AreNotEqual(string.Empty,result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            string pwd = "sunbeam";
            string encData = null;
            bool result = Security.Encrypt(pwd,out encData);
            Assert.AreEqual(true, result);
        }
    }
}
