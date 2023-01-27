using DroneDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DroneDelivery.Interfaces
{
    public interface IDeliveryAssignmentStrategy
    {
        void AssignDeliveries(List<Drone> drones, List<DeliveryLocation> locations);
    }
}
