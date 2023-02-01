using ParkingSystem.Enums;
using ParkingSystem.Models;

namespace ParkingSystem.Services;
public class ParkingManager
{   //declaring variables which are to be used later 
    public int availableTwoWheelerLots;
    public int availableFourWheelerLots;
    public int availableHeavyVehicleLots;
    public List<ParkingLot> parkingSpaces = new List<ParkingLot>();
    public Dictionary<string, Ticket> ticketDict = new Dictionary<string, Ticket>();
    public List<Vehicle> vehicleList = new List<Vehicle>();
    //initilizing parking lots and their number is given input by the user
    public ParkingManager(int TwoWheelerLots, int FourWheelerLots, int HeavyVehicleLots)
    {
        availableTwoWheelerLots = TwoWheelerLots;
        availableFourWheelerLots = FourWheelerLots;
        availableHeavyVehicleLots = HeavyVehicleLots;
    }
    //lot availability is checked and ticket is generated
    public string ParkVehicle(ParkingCategory VehicleCategory, string VehicleNumber)
    {   //vehicle enters the lot
        vehicleList.Add(new Vehicle(VehicleCategory));
        var vehicle = vehicleList.Last();
        string vehicleId = vehicle.VehicleId;
        vehicle.VehicleNumber = VehicleNumber;
        string lotId;
        string ticketId;
        //check for availability
        switch (VehicleCategory)
        {
            case (ParkingCategory.TwoWheeler):
                {
                    if (availableTwoWheelerLots > 0)
                    {
                        ticketId = ticketGenerator(ParkingCategory.TwoWheeler, vehicleId);
                        availableTwoWheelerLots--;
                        return ticketId;
                    }
                    else
                    {
                        return "No slots available, Vehicle can't be parked";
                    }
                }
            case (ParkingCategory.FourWheeler):
                {
                    if (availableFourWheelerLots > 0)
                    {
                        ticketId = ticketGenerator(ParkingCategory.FourWheeler, vehicleId);
                        availableFourWheelerLots--;
                        return ticketId;
                    }
                    else
                    {
                        return "No slots available, Vehicle can't be parked";
                    }
                }
            case (ParkingCategory.HeavyVehicle):
                {
                    if (availableHeavyVehicleLots > 0)
                    {
                        ticketId = ticketGenerator(ParkingCategory.HeavyVehicle, vehicleId);
                        availableHeavyVehicleLots--;
                        return ticketId;
                    }
                    else
                    {
                        return "No slots available, Vehicle can't be parked";
                    }
                }
        }
        return "Error:Vehicle Park failed";
    }


    public bool UnparkVehicle(string ticketId)
    {
        try
        {
            var ticket = ticketDict[ticketId];
            ticket.UnParkTime = DateTime.Now;
            ticketDict[ticketId] = ticket;
            string lotId = ticket.LotId;
            foreach (var lot in parkingSpaces.ToList())
            {
                if (lot.LotId == lotId)
                {
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
                }
            }
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }


    public Dictionary<ParkingCategory, int> lotOccupancyStatus()
    {
        Dictionary<ParkingCategory, int> occupancyStatus = new Dictionary<ParkingCategory, int>();
        occupancyStatus.Add(ParkingCategory.TwoWheeler, availableTwoWheelerLots);
        occupancyStatus.Add(ParkingCategory.FourWheeler, availableFourWheelerLots);
        occupancyStatus.Add(ParkingCategory.HeavyVehicle, availableHeavyVehicleLots);
        return occupancyStatus;
    }

    public string ticketGenerator(ParkingCategory parkingCategory, string vehicleId)
    {
        ParkingLot parkingLot = new ParkingLot(parkingCategory);
        string lotId = parkingLot.LotId;
        parkingLot.LotOccupy();
        parkingSpaces.Add(parkingLot);
        Ticket ticket = new Ticket(lotId, vehicleId);
        string ticketId = ticket.TicketId;
        ticketDict.Add(ticketId, ticket);
        return ticketId;
    }
}
