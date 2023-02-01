using ParkingSystem.Enums;
using ParkingSystem.Services;
using System.Text.RegularExpressions;

namespace ParkingConsoleApp;

public class Program
{
    static void Main(string[] args)
    {

        Console.WriteLine("Welcome to Parking Manager!");

        Console.WriteLine("Enter the number of slots for Two wheeler vehicles, Four Wheeler Vehicles and Heavy Vehicles in that order");
        int TwoWheelerSlots = Convert.ToInt16(Console.ReadLine());
        int FourWheelerSlots = Convert.ToInt16(Console.ReadLine());
        int HeavyVehicleSlots = Convert.ToInt16(Console.ReadLine());
        ParkingManager parkingManager = new ParkingManager(TwoWheelerSlots, FourWheelerSlots, HeavyVehicleSlots);

        while (true)
        {
            Console.WriteLine(@"Enter the number of the opertion you want to perform
1. Park Vehicle
2. Unpark Vehicle
3. Check Occupancy Status
4. Exit");
            int option = Convert.ToInt16(Console.ReadLine());

            switch (option)
            {
                default:
                    {
                        Console.WriteLine("Select a valid option");
                        break;
                    }
                case 1:
                    {
                        Console.WriteLine(@"Select the type of the vehicle you want to park
1. TwoWheeler
2. Four Wheeler
3. Heavy Vehicle");

                        int vehicleType = Convert.ToInt16(Console.ReadLine());
                        if (vehicleType > 0 && vehicleType < 4)
                        {
                            Console.WriteLine("Enter Vehicle number (press enter if unknown) of format MH 01 AX 1234(follow the format strictly)");
                            string vehicleNmber = Console.ReadLine();
                            string pattern = @"^[A-Za-z]{2}\s[0-9]{2}\s[A-Za-z]{2}\s[0-9]{4}$";
                            if (Regex.IsMatch(vehicleNmber, pattern) || vehicleNmber == "")
                            {
                                string ticketId = parkingManager.ParkVehicle((ParkingCategory)vehicleType, vehicleNmber);
                                Console.WriteLine("Your ticket ID is :" + ticketId);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("The vehicle number didn't follow the format");
                                break;
                            }

                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid option");
                            break;
                        }
                    }
                case 2:
                    {
                        Console.WriteLine("Enter your ticket Id");
                        string ticketId = Console.ReadLine();
                        bool unparkStatus = parkingManager.UnparkVehicle(ticketId);
                        if (unparkStatus)
                        {
                            Console.WriteLine("Unparked Successfully");
                        }
                        else
                        {
                            Console.WriteLine("Ticket Not Found");
                        }
                        break;
                    }
                case 3:
                    {
                        var occupanyStatus = parkingManager.lotOccupancyStatus();
                        foreach (KeyValuePair<ParkingCategory, int> entry in occupanyStatus)
                        {
                            Console.WriteLine("There are " + entry.Value + " lots available for" + entry.Key);
                        }
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("Are you sure?(y/n)");
                        string response = Console.ReadLine();
                        if (response == "Y" || response == "y")
                        {
                            Environment.Exit(0);
                        }
                        else if (response == "N" || response == "n")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Select a relevant option");
                            break;
                        }
                        break;
                    }
            }
        }
    }
}
