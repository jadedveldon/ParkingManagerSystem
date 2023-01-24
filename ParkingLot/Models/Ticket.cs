using System;

namespace ParkingSystem.Models;

public class Ticket
{
    public string TicketId;
    public string LotId;
    public string VehicleId;
    public DateTime ParkTime = new DateTime();
    public DateTime UnParkTime = new DateTime();

    public void DisplayStatus()
    {
        Console.WriteLine(LotId);
    }

    public Ticket(string lotId, string vehicleId)
    {
        var guid = Guid.NewGuid().ToString();
        TicketId = guid;
        LotId = lotId;
        VehicleId = vehicleId;
        ParkTime = DateTime.Now;
    }
}
