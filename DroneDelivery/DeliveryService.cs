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
        private readonly IDeliveryService deliveryService;

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
                            (droneInfo[j].Trim(), int.Parse(droneInfo[j + 1].Trim()), deliveryService));
                        }
                    }
                    else
                    {
                        // other lines contain delivery information
                        string[] locationInfo = line.Split(',');
                        string locationName = locationInfo[0].Trim();
                        int packageWeight = int.Parse(locationInfo[1].Trim());
                        //Assign the location to the drone with the least weight
                        int minIndex = 0;
                        for (int k = 1; k < drones.Count; k++)
                        {
                            if (drones[k].CurrentWeight < drones[minIndex].CurrentWeight)
                                minIndex = k;
                        }
                        drones[minIndex].CurrentWeight += packageWeight;
                        locations.Add(new DeliveryLocation(locationName, packageWeight));
                    }
                    i++;
                }
            }
            AssignDeliveries(drones, locations);
        }

        private void AssignDeliveries(List<Drone> drones, List<DeliveryLocation> locations)
        {
            // sort the locations based on weight
            locations.Sort((x, y) => x.PackageWeight.CompareTo(y.PackageWeight));

            // assign locations to drones
            foreach (var drone in drones)
            {
                int weight = 0;
                foreach (var location in locations)
                {
                    if (weight + location.PackageWeight <= drone.MaxWeight)
                    {
                        drone.AddDelivery(location);
                        weight += location.PackageWeight;
                    }
                }
            }
        }

        
    }
}

