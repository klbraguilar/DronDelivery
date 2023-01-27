using DroneDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DroneDelivery.Interfaces
{
    public interface IDeliveryService
    {
        void SendPackage(DeliveryLocation location);
        void SendDeliveries(Drone drone, int dronNumber);
        bool IsAvailable { get; set; }
    }
}
