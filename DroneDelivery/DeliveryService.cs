using DroneDelivery.Interfaces;
using DroneDelivery.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DroneDelivery
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryService _deliveryService;
        private readonly Queue<DeliveryLocation> unassignedPackages;

        public bool IsAvailable { get ; set ; }

        public DeliveryService()
        {
            unassignedPackages = new Queue<DeliveryLocation>();
        }
        public void SendPackage(DeliveryLocation location)
        {
            ReadFile();

        }

        private void ReadFile()
        {
            List<Drone> drones = new List<Drone>();
            List<DeliveryLocation> locations = new List<DeliveryLocation>();
            using (StreamReader reader = new StreamReader("input.txt"))
            {
                string line;
                int i = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    if (i == 0)
                    {
                        // first line contains drone information
                        string[] droneInfo = line.Split(',');
                        for (int j = 0; j < droneInfo.Length; j += 2)
                        {
                            drones.Add(new Drone
                            (droneInfo[j].Trim(), int.Parse(droneInfo[j + 1].Trim()), _deliveryService));
                        }
                    }
                    else
                    {
                        // other lines contain delivery information
                        string[] locationInfo = line.Split(',');
                        string locationName = locationInfo[0].Trim();
                        int packageWeight = int.Parse(locationInfo[1].Trim());
                        locations.Add(new DeliveryLocation(locationName, packageWeight));
                    }
                    i++;
                }
            }
            // sort the drones by maxWeight
            drones = drones.OrderByDescending(x => x.MaxWeight).ToList();
            // sort the packages by weight in descending order
            locations = locations.OrderByDescending(x => x.PackageWeight).ToList();
            // add the packages to the queue
            foreach (var location in locations)
            {
                unassignedPackages.Enqueue(location);
            }
            // assign the packages to the drones
            AssignDeliveries(drones);
        }

        private void AssignDeliveries(List<Drone> drones)
        {
            // assign locations to drones
            foreach (var drone in drones)
            {
                if (IsAvailable)
                {
                    int weight = 0;
                    while (weight <= drone.MaxWeight && unassignedPackages.Count > 0)
                    {
                        var location = unassignedPackages.Dequeue();
                        if (weight + location.PackageWeight <= drone.MaxWeight)
                        {
                            drone.AddDelivery(location);
                            weight += location.PackageWeight;
                        }
                    }
                    IsAvailable = false;
                    SendDeliveries(drone, drones.IndexOf(drone));
                }
            }
        }

        public void SendDeliveries(Drone droneWithDeliveries, int droneNumber)
        {
            using (StreamWriter writer = new StreamWriter("output.txt", true))
            {
                int tripNumber = 1;
                foreach (var trip in droneWithDeliveries.Deliveries)
                {
                    writer.WriteLine("[Drone #" + droneNumber + droneWithDeliveries.Name + "]");
                    writer.WriteLine("[Trip #" + tripNumber + "]");
                    writer.Write("[Location #" + trip.Name + "], ");
                    writer.WriteLine();
                    tripNumber++;
                }
                IsAvailable = true;
            }
        }
    }
}

