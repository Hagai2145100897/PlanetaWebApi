using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlanetaWebApi.Repositories.InnerModel;

namespace PlanetaWebApi.Tests.TestConverters.TestSubnetConverter
{
    [TestClass]
    public class TestConvertInnerSubnetToSubnet
    {
        [TestMethod]
        public void TestValidConvertation()
        {
            var testInnerSubnet1 = new InnerSubnetItem() {
                Id = 5013,
                ClientId = 3105,
                NetworkPrefix = "192.168.0.0/24"
            };
            var testInnerSubnet2 = new InnerSubnetItem() {
                Id = 5013,
                ClientId = 3105,
                NetworkPrefix = "0.0.0.0/0"
            };
            var testInnerSubnet3 = new InnerSubnetItem() {
                Id = 5013,
                ClientId = 3105,
                NetworkPrefix = "255.255.255.255/32"
            };

            var result1 = SubnetConverter.Convert(testInnerSubnet1);
            var result2 = SubnetConverter.Convert(testInnerSubnet2);
            var result3 = SubnetConverter.Convert(testInnerSubnet3);

            Assert.AreEqual(testInnerSubnet1.Id, result1.Id);
            Assert.AreEqual(testInnerSubnet1.Id, result1.Id);
            Assert.AreEqual("192.168.0.0", result1.NetworkPrefix.ToString());
            Assert.AreEqual(24, result1.SubnetMask);
            Assert.AreEqual(testInnerSubnet2.Id, result2.Id);
            Assert.AreEqual(testInnerSubnet2.Id, result2.Id);
            Assert.AreEqual("0.0.0.0", result2.NetworkPrefix.ToString());
            Assert.AreEqual(0, result2.SubnetMask);
            Assert.AreEqual(testInnerSubnet3.Id, result3.Id);
            Assert.AreEqual(testInnerSubnet3.Id, result3.Id);
            Assert.AreEqual("255.255.255.255", result3.NetworkPrefix.ToString());
            Assert.AreEqual(32, result3.SubnetMask);
        }
        
        [TestMethod]
        public void TestInvalidNetworkPrefixInAddress()
        {
            var testInnerSubnet1 = new InnerSubnetItem() {
                Id = 5013,
                ClientId = 3105,
                NetworkPrefix = "Text"
            };
            var testInnerSubnet2 = new InnerSubnetItem() {
                Id = 5013,
                ClientId = 3105,
                NetworkPrefix = "Text/8"
            };
            var testInnerSubnet3 = new InnerSubnetItem() {
                Id = 5013,
                ClientId = 3105,
                NetworkPrefix = "256.0.0.0/24"
            };

            try
            {
                var result1 = SubnetConverter.Convert(testInnerSubnet1);
            }
            catch (System.ArgumentException)
            {
                try
                {
                    var result2 = SubnetConverter.Convert(testInnerSubnet2);
                }
                catch (System.ArgumentException)
                {
                    try
                    {
                        var result3 = SubnetConverter.Convert(testInnerSubnet3);
                    }
                    catch (System.ArgumentException)
                    {
                        return;
                    }
                }
            }
            Assert.Fail("Didn't recognize 1 in tail");
        }
        
        [TestMethod]
        public void TestInvalidNetworkPrefixInMask()
        {
            var testInnerSubnet1 = new InnerSubnetItem() {
                Id = 5013,
                ClientId = 3105,
                NetworkPrefix = "Text"
            };
            var testInnerSubnet2 = new InnerSubnetItem() {
                Id = 5013,
                ClientId = 3105,
                NetworkPrefix = "192.168.0.0/Text"
            };
            var testInnerSubnet3 = new InnerSubnetItem() {
                Id = 5013,
                ClientId = 3105,
                NetworkPrefix = "0.0.0.0/-1"
            };
            var testInnerSubnet4 = new InnerSubnetItem() {
                Id = 5013,
                ClientId = 3105,
                NetworkPrefix = "127.0.0.0/33"
            };
        }
        
        [TestMethod]
        public void TestInvalidNetworkPrefixTooManySlash()
        {
            var testInnerSubnet1 = new InnerSubnetItem() {
                Id = 5013,
                ClientId = 3105,
                NetworkPrefix = "0/192.168.0.0/24"
            };
            var testInnerSubnet2 = new InnerSubnetItem() {
                Id = 5013,
                ClientId = 3105,
                NetworkPrefix = "192.168.0.0/24/0"
            };
            var testInnerSubnet3 = new InnerSubnetItem() {
                Id = 5013,
                ClientId = 3105,
                NetworkPrefix = "192.168.0.0/0/24"
            };
        }
    }
}
