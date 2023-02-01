using ParkingSystem.Enums;
using ParkingSystem.Models;

namespace ParkingSystem.Services;
public class ParkingManager
{   //declaring variables which are to be used later 
    private int availableTwoWheelerLots;
    private int availableFourWheelerLots;
    private int availableHeavyVehicleLots;
    private List<ParkingLot> parkingSpaces = new();
    private Dictionary<string, Ticket> ticketDict = new();
    private List<Vehicle> vehicleList = new();
    //initilizing parking lots and their number is given input by the user
    public ParkingManager(int TwoWheelerLots, int FourWheelerLots, int HeavyVehicleLots)
    {
        availableTwoWheelerLots = TwoWheelerLots;
        availableFourWheelerLots = FourWheelerLots;
        availableHeavyVehicleLots = HeavyVehicleLots;
    }
    //lot availability is checked and ticket is generated
    public string? ParkVehicle(ParkingCategory VehicleCategory, string VehicleNumber)
    {   //vehicle enters the lot
        vehicleList.Add(new Vehicle(VehicleCategory, VehicleNumber));
        var vehicle = vehicleList.Last();
        string? vehicleId = vehicle.VehicleId;
        string? ticketId;
        //check for availability
        switch (VehicleCategory)
        {
            case (ParkingCategory.TwoWheeler):
                {
                    ticketId = TicketGenerator(ParkingCategory.TwoWheeler, vehicleId, VehicleNumber);
                    availableTwoWheelerLots--;
                    return ticketId;
                }
            case (ParkingCategory.FourWheeler):
                {
                    ticketId = TicketGenerator(ParkingCategory.FourWheeler, vehicleId, VehicleNumber);
                    availableFourWheelerLots--;
                    return ticketId;
                }
            case (ParkingCategory.HeavyVehicle):
                {
                    ticketId = TicketGenerator(ParkingCategory.HeavyVehicle, vehicleId, VehicleNumber);
                    availableHeavyVehicleLots--;
                    return ticketId;
                }
        }
        return null;
    }


    public bool UnparkVehicle(string ticketId)
    {
        try
        {
            var ticket = ticketDict[ticketId];
            ticket.UnParkTime = DateTime.Now;
            ticketDict[ticketId] = ticket;
            string? lotId = ticket.LotId;
            var lot = (from l in parkingSpaces
                       where l.LotId == lotId
                       select l).Single();

            switch (lot.LotCategory)
            {
                case (ParkingCategory.TwoWheeler):
                    {
                        availableTwoWheelerLots++;
                        break;
                    }
                case (ParkingCategory.FourWheeler):
                    {
                        availableFourWheelerLots++;
                        break;
                    }
                case (ParkingCategory.HeavyVehicle):
                    {
                        availableHeavyVehicleLots++;
                        break;
                    }
            }
            parkingSpaces.Remove(lot);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }


    public Dictionary<ParkingCategory, int> LotOccupancyStatus()
    {
        Dictionary<ParkingCategory, int> occupancyStatus = new();
        occupancyStatus.Add(ParkingCategory.TwoWheeler, availableTwoWheelerLots);
        occupancyStatus.Add(ParkingCategory.FourWheeler, availableFourWheelerLots);
        occupancyStatus.Add(ParkingCategory.HeavyVehicle, availableHeavyVehicleLots);
        return occupancyStatus;
    }

    private string TicketGenerator(ParkingCategory parkingCategory, string vehicleId, string vehicleNumber)
    {
        ParkingLot parkingLot = new(parkingCategory);
        string? lotId = parkingLot.LotId;
        parkingLot.LotOccupy();
        parkingSpaces.Add(parkingLot);
        Ticket ticket = new(lotId, vehicleId, vehicleNumber);
        string? ticketId = ticket.TicketId;
        ticketDict.Add(ticketId, ticket);
        return ticketId;
    }

    public bool IsParkingLotAvailable(ParkingCategory parkingCategory)
    {
        if (parkingCategory == ParkingCategory.TwoWheeler && availableTwoWheelerLots > 0)
        {
            return true;
        }
        else if (parkingCategory == ParkingCategory.FourWheeler && availableFourWheelerLots > 0)
        {
            return true;
        }
        else if (parkingCategory == ParkingCategory.HeavyVehicle && availableHeavyVehicleLots > 0)
        {
            return true;
        }

        return false;
    }
}
