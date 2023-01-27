using DroneDelivery.Interfaces;
using DroneDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DroneDelivery
{
    public class WeightBasedAssignmentStrategy : IDeliveryAssignmentStrategy
    {
        public void AssignDeliveries(List<Drone> drones, List<DeliveryLocation> locations)
        {
            // Sort the drones by max weight
            drones = drones.OrderByDescending(x => x.MaxWeight).ToList();
            // Sort the locations by package weight
            locations = locations.OrderByDescending(x => x.PackageWeight).ToList();
            // Assign the deliveries
            foreach (var drone in drones)
            {
                int weight = 0;
                while (weight <= drone.MaxWeight && locations.Count > 0)
                {
                    var location = locations[0];
                    if (weight + location.PackageWeight <= drone.MaxWeight)
                    {
                        drone.AddDelivery(location);
                        weight += location.PackageWeight;
                        locations.RemoveAt(0);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
