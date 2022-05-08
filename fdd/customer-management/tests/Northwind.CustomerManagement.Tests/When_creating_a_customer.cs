using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Northwind.CustomerManagement.DataMaintenance.Domain;

namespace Northwind.CustomerManagement.Tests
{
    [TestClass]
    public class When_creating_a_customer
    {
        [DataTestMethod]
        [DataRow(null, DisplayName = "null")]
        [DataRow("", DisplayName = "Empty string")]
        [DataRow("  ", DisplayName = "Whitespace")]
        [DataRow("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxy", DisplayName = "51 characters")]
        public void Given_invalid_CompanyName_validation_should_fail(string invalidName)
        {
            Customer c = new Customer(invalidName);
            var results = c.Validate();
            Assert.IsTrue(results.Any());
            Assert.AreEqual("CompanyName", results.First().MemberNames.First());
        }

        [DataTestMethod]
        [DataRow("A", DisplayName = "Single Character")]
        [DataRow("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwx", DisplayName = "50 characters")]
        public void Given_valid_CompanyName_validation_should_succeed(string invalidName)
        {
            Customer c = new Customer(invalidName);
            var results = c.Validate();
            Assert.IsFalse(results.Any());
        }


        public void Test()
        {
            var host = new WebApplicationFactory<Foo>();
            
            var client = host.CreateClient();

            IHost h = new HostBuilder()
                .Build();

            var server = h.GetTestServer();
            server.CreateHandler();

        }
        
    }

    public class Foo
    {
    }
}