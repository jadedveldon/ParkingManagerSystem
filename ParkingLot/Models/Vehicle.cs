﻿using ParkingSystem.Enums;

namespace ParkingSystem.Models;

public class Vehicle
{
    public string VehicleId;
    public ParkingCategory VehicleCategory;
    public string VehicleNumber;

    public Vehicle(ParkingCategory vehicleCategory)
    {
        var guid = Guid.NewGuid().ToString();
        VehicleId = guid;
        VehicleCategory = vehicleCategory;
    }
}
