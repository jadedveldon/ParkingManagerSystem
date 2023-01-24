using ParkingSystem.Enums;
using System;

namespace ParkingSystem.Models;

public class Vehicle
{
    public string VehicleId;
    public ParkingCategory VehicleCategory;

    public Vehicle(ParkingCategory vehicleCategory)
    {
        var guid = Guid.NewGuid().ToString();
        VehicleId = guid;
        VehicleCategory = vehicleCategory;
    }
}
