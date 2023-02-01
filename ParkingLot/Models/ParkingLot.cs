using ParkingSystem.Enums;

namespace ParkingSystem.Models;

public class ParkingLot
{
    public string LotId;
    public OccupancyStatus Occupancy = OccupancyStatus.Unoccupied;
    public ParkingCategory LotCategory;
    public OccupancyStatus DisplayStatus()
    {
        return Occupancy;
    }
    public void LotOccupy()
    {
        if (Occupancy == OccupancyStatus.Unoccupied)
        {
            Occupancy = OccupancyStatus.Occupied;
        }
    }
    public void LotUnoccupy()
    {
        if (Occupancy == OccupancyStatus.Occupied)
        {
            Occupancy = OccupancyStatus.Unoccupied;
        }
    }
    public ParkingLot(ParkingCategory lotCategory)
    {
        var guid = Guid.NewGuid().ToString();
        LotId = guid;
        LotCategory = lotCategory;
    }
}