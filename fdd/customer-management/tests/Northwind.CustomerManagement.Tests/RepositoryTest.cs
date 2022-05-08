using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.Foundation;

namespace Northwind.CustomerManagement.Tests
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void Test()
        {
            var c = new Currency("USD", 1234.45M);
            var d = 7894.12M;
            
            Assert.AreEqual("USD 1,234.45", c.ToString());
            Assert.IsTrue(c < d);
            Assert.IsTrue(d > c);
        }
    }
}
