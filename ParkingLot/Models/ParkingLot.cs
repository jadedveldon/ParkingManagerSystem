using System;
using ParkingSystem.Enums;

namespace ParkingSystem.Models;

public class ParkingLot
{

    public string LotId;
    bool OccupancyStatus;
    public ParkingCategory LotCategory;
    public bool DisplayStatus()
    {
        return OccupancyStatus;
    }
    public ParkingLot(ParkingCategory lotCategory)
    {
        var guid = Guid.NewGuid().ToString();
        LotId = guid;
        LotCategory = lotCategory;
    }
}