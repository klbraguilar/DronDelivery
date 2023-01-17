using DroneDelivery.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DroneDelivery.Models
{
    class Drone
    {
        private readonly IDeliveryService _deliveryService;

        public void Deliver(DeliveryLocation location)
        {
            _deliveryService.SendPackage(location);
        }
        public Drone(string name, int maxWeight, IDeliveryService deliveryService)
        {
            Name = name;
            MaxWeight = maxWeight;
            Deliveries = new List<DeliveryLocation>();
            _deliveryService = deliveryService;
        }
        public string Name { get; }
        public int MaxWeight { get; }
        public List<DeliveryLocation> Deliveries { get; }

        public void AddDelivery(DeliveryLocation location)
        {
            Deliveries.Add(location);
        }
        public int CurrentWeight { get; internal set; }
    }
}
