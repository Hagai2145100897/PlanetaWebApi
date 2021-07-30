using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlanetaWebApi.Models;
using PlanetaWebApi.Repositories.InnerModel;

namespace PlanetaWebApi.Tests.TestConverters.TestSubnetConverter
{
    [TestClass]
    public class TestConvertSubnetToInnerSubnet
    {
        [TestMethod]
        public void TestValidConvertation()
        {
            var testSubnet1 = new SubnetItem() {
                Id = 5013,
                ClientId = 3105,
                NetworkPrefix = IPAddress.Parse("192.16.0.0"),
                SubnetMask = 12
            };
            var testSubnet2 = new SubnetItem() {
                Id = 5013,
                ClientId = 3105,
                NetworkPrefix = IPAddress.Parse("192.168.0.1"),
                SubnetMask = 32
            };
            var testSubnet3 = new SubnetItem() {
                Id = 5013,
                ClientId = 3105,
                NetworkPrefix = IPAddress.Parse("0.0.0.0"),
                SubnetMask = 0
            };

            var result1 = SubnetConverter.Convert(testSubnet1);
            var result2 = SubnetConverter.Convert(testSubnet2);
            var result3 = SubnetConverter.Convert(testSubnet3);

            Assert.AreEqual(testSubnet1.Id, result1.Id);
            Assert.AreEqual(testSubnet1.Id, result1.Id);
            Assert.AreEqual("192.16.0.0/12", result1.NetworkPrefix, "Incorrect format");
            Assert.AreEqual(testSubnet2.Id, result2.Id);
            Assert.AreEqual(testSubnet2.Id, result2.Id);
            Assert.AreEqual("192.168.0.1/32", result2.NetworkPrefix, "Incorrect format");
            Assert.AreEqual(testSubnet3.Id, result3.Id);
            Assert.AreEqual(testSubnet3.Id, result3.Id);
            Assert.AreEqual("0.0.0.0/0", result3.NetworkPrefix, "Incorrect format");
        }
        
        [TestMethod]
        public void TestInvalidSubnetMask()
        {
            var testSubnet1 = new SubnetItem() {
                Id = 5013,
                ClientId = 3105,
                NetworkPrefix = IPAddress.Parse("0.0.0.0"),
                SubnetMask = -1
            };
            var testSubnet2 = new SubnetItem() {
                Id = 5013,
                ClientId = 3105,
                NetworkPrefix = IPAddress.Parse("192.168.0.0"),
                SubnetMask = 33
            };

            try
            {
                var result1 = SubnetConverter.Convert(testSubnet1);
            }
            catch (System.ArgumentException)
            {
                try
                {
                    var result2 = SubnetConverter.Convert(testSubnet2);
                }
                catch (System.ArgumentException)
                {
                    return;
                }
            }
            Assert.Fail("Didn't recognize invalid mask");
        }
        
        [TestMethod]
        public void TestInvalidNetworkPrefix()
        {
            var testSubnet1 = new SubnetItem() {
                Id = 5013,
                ClientId = 3105,
                NetworkPrefix = IPAddress.Parse("127.0.0.1"),
                SubnetMask = 8
            };
            var testSubnet2 = new SubnetItem() {
                Id = 5013,
                ClientId = 3105,
                NetworkPrefix = IPAddress.Parse("192.8.0.0"),
                SubnetMask = 12
            };
            var testSubnet3 = new SubnetItem() {
                Id = 5013,
                ClientId = 3105,
                NetworkPrefix = IPAddress.Parse("128.0.0.0"),
                SubnetMask = 0
            };

            try
            {
                var result1 = SubnetConverter.Convert(testSubnet1);
            }
            catch (System.ArgumentException)
            {
                try
                {
                    var result2 = SubnetConverter.Convert(testSubnet2);
                }
                catch (System.ArgumentException)
                {
                    try
                    {
                        var result3 = SubnetConverter.Convert(testSubnet3);
                    }
                    catch (System.ArgumentException)
                    {
                        return;
                    }
                }
            }
            Assert.Fail("Didn't recognize 1 in tail");
        }
    }
}
