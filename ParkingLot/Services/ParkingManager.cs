using ParkingSystem.Enums;
using ParkingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystem.Services;

public class ParkingManager
{
    List<ParkingLot> TwoWheelerParkingSpaces = new List<ParkingLot>();
    List<ParkingLot> FourWheelerParkingSpaces = new List<ParkingLot>();
    List<ParkingLot> HeavyVehicleParkingSpaces = new List<ParkingLot>();
    public void InitilizeLot(int TwoWheelerLots, int FourWheelerLots, int HeavyVehicleLots)
    {
        for(int i =0; i< TwoWheelerLots; i++)
        {
            TwoWheelerParkingSpaces.Add(new ParkingLot(ParkingCategory.TwoWheeler));
        }

        for (int i = 0; i < FourWheelerLots; i++)
        {
            FourWheelerParkingSpaces.Add(new ParkingLot(ParkingCategory.FourWheeler));
        }

        for (int i = 0; i < HeavyVehicleLots; i++)
        {
            HeavyVehicleParkingSpaces.Add(new ParkingLot(ParkingCategory.HeavyVehicle));
        }
    }


}
