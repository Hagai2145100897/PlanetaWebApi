using System;
using System.Linq;
using System.Net;
using PlanetaWebApi.Models;

namespace PlanetaWebApi.Repositories.InnerModel
{
    public static class SubnetConverter
    {
        public static SubnetItem Convert(InnerSubnetItem innerSubnet)
        {
            var subnetData = innerSubnet.NetworkPrefix.Split('/');
            var prefix = IPAddress.Parse(subnetData[0]);
            var mask = int.Parse(subnetData[1]);
            return new SubnetItem()
            {
                Id = innerSubnet.Id,
                ClientId = innerSubnet.ClientId,
                NetworkPrefix = prefix,
                SubnetMask = mask
            };
        }

        public static InnerSubnetItem Convert(SubnetItem subnet)
        {
            Validate(subnet);
            return new InnerSubnetItem()
            {
                Id = subnet.Id,
                ClientId = subnet.ClientId,
                NetworkPrefix = $"{subnet.NetworkPrefix}/{subnet.SubnetMask}"
            };
        }

        private static void Validate(SubnetItem subnet)
        {
            var mask = subnet.SubnetMask;
            if (mask < 0 || mask > 32)
                throw new ArgumentException($"Mask expected in the range [0, 32], but was {mask}");
            var thresholdByteNumber = mask / 8;
            var thresholdBitNumber = mask % 8;
            var addressTail = subnet.NetworkPrefix.GetAddressBytes().Skip(thresholdByteNumber);
            var thresholdByte = addressTail.FirstOrDefault() % (1 << (8 - thresholdBitNumber));
            if (thresholdByte != 0 || addressTail.Skip(1).Aggregate(false, (r, nb) => r || nb != 0))
                throw new ArgumentException($"Prefix expected with 0's in tail, but was 1");
        }
    }
}
