using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DroneDelivery.Models
{
    public class DeliveryLocation
    {
        public DeliveryLocation(string name, int packageWeight)
        {
            Name = name;
            PackageWeight = packageWeight;
        }
        public string Name { get; }
        public int PackageWeight { get; }
    }
}
