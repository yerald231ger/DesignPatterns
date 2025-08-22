using Builder.Models;
using Builder.Builders;

namespace Builder.Data;

public static class SampleDataGenerator
{
    public static ReceptionInventory[] GenerateReceptionInventories()
    {
        return new[]
        {
            new ReceptionInventory
            {
                ReceptionInventoryId = 1001,
                TankId = 1, // Magna tank
                ReceivedVolume = 45000.50m,
                ReceptionDateTime = DateTime.Now.AddDays(-7),
                DocumentNumber = "REC-2025-001"
            },
            new ReceptionInventory
            {
                ReceptionInventoryId = 1002,
                TankId = 2, // Premium tank
                ReceivedVolume = 42000.75m,
                ReceptionDateTime = DateTime.Now.AddDays(-5),
                DocumentNumber = "REC-2025-002"
            },
            new ReceptionInventory
            {
                ReceptionInventoryId = 1003,
                TankId = 3, // Diesel tank
                ReceivedVolume = 78000.25m,
                ReceptionDateTime = DateTime.Now.AddDays(-3),
                DocumentNumber = "REC-2025-003"
            },
            new ReceptionInventory
            {
                ReceptionInventoryId = 1004,
                TankId = 1, // Magna tank (second delivery)
                ReceivedVolume = 38000.00m,
                ReceptionDateTime = DateTime.Now.AddDays(-1),
                DocumentNumber = "REC-2025-004"
            }
        };
    }

    public static DetailedDeliveryInventory[] GenerateDeliveryInventories()
    {
        var random = new Random(42); // Fixed seed for consistent data
        var deliveries = new List<DetailedDeliveryInventory>();
        int deliveryId = 5001;

        // Generate deliveries for the last 7 days
        for (int day = 7; day >= 1; day--)
        {
            var date = DateTime.Now.AddDays(-day);
            
            // Generate random deliveries throughout the day
            for (int hour = 6; hour < 22; hour++) // Station operates 6 AM to 10 PM
            {
                // Random number of deliveries per hour (0-5)
                int deliveriesThisHour = random.Next(0, 6);
                
                for (int delivery = 0; delivery < deliveriesThisHour; delivery++)
                {
                    var pumpNumber = (short)random.Next(1, 6); // Pumps 1-5
                    var hoseNumber = (short)random.Next(1, 3); // Hoses 1-2
                    var volume = (decimal)(random.NextDouble() * 60 + 10); // 10-70 liters
                    var pricePerLiter = hoseNumber == 1 ? 22.50m : 24.80m; // Magna vs Premium pricing
                    
                    deliveries.Add(new DetailedDeliveryInventory
                    {
                        DeliveryInventoryId = deliveryId++,
                        PumpNumber = pumpNumber,
                        HoseNumber = hoseNumber,
                        DeliveredVolume = Math.Round(volume, 2),
                        DeliveredAmount = Math.Round(volume * pricePerLiter, 2),
                        SaleTypeId = 1, // Regular sale
                        InterfaceTransactionDateTime = date.AddHours(hour).AddMinutes(random.Next(0, 60))
                    });
                }
            }
        }

        return deliveries.ToArray();
    }

    public static void DisplayStationSummary(Builders.Station station)
    {
        Console.WriteLine("\n" + new string('=', 80));
        Console.WriteLine("GAS STATION CONFIGURATION SUMMARY");
        Console.WriteLine(new string('=', 80));

        Console.WriteLine($"\nError Margin: {station.ErrorMargin:P2}");

        Console.WriteLine($"\nTANKS ({station.Tanks.Count}):");
        Console.WriteLine(new string('-', 80));
        
        foreach (var tank in station.Tanks)
        {
            Console.WriteLine($"Tank {tank.TankId}: {tank.Product?.ProductName} ({tank.Product?.ProductColor})");
            Console.WriteLine($"  Code: {tank.Code}");
            Console.WriteLine($"  Description: {tank.Description}");
            Console.WriteLine($"  Capacity: {tank.UtilCapacity:N2} L (Operating: {tank.OperatingCapacity:N2} L)");
            Console.WriteLine($"  Current Volume: {tank.LastTankReading?.CurrentVolume:N2} L");
            Console.WriteLine($"  Calibration: {tank.CalibrationDate:yyyy-MM-dd}");
            Console.WriteLine($"  Status: {(tank.IsActive ? "Active" : "Inactive")}");
            
            if (tank.ReceptionInventories.Any())
            {
                Console.WriteLine($"  Recent Receptions: {tank.ReceptionInventories.Count}");
                foreach (var reception in tank.ReceptionInventories.Take(3))
                {
                    Console.WriteLine($"    - {reception.ReceptionDateTime:yyyy-MM-dd}: {reception.ReceivedVolume:N2} L ({reception.DocumentNumber})");
                }
            }
            Console.WriteLine();
        }

        Console.WriteLine($"DISPENSERS ({station.Dispensers.Count}):");
        Console.WriteLine(new string('-', 80));
        
        foreach (var dispenser in station.Dispensers)
        {
            Console.WriteLine($"Dispenser {dispenser.DispenserId}: {dispenser.Description}");
            Console.WriteLine($"  Code: {dispenser.Code}");
            Console.WriteLine($"  Measurement Uncertainty: {dispenser.MeasurementUncertainty:P2}");
            Console.WriteLine($"  Pumps: {dispenser.Pumps.Count}");
            
            foreach (var pump in dispenser.Pumps)
            {
                Console.WriteLine($"    Pump {pump.PumpNumber}: {pump.Description}");
                foreach (var hose in pump.Hoses)
                {
                    var totalVolume = hose.DeliveryInventories.Sum(d => d.DeliveredVolume);
                    var totalAmount = hose.DeliveryInventories.Sum(d => d.DeliveredAmount);
                    
                    Console.WriteLine($"      Hose {hose.HoseNumber}: {hose.Product?.ProductName} " +
                                    $"(Tank {hose.TankId}) - " +
                                    $"Delivered: {totalVolume:N2} L / ${totalAmount:N2}");
                }
            }
            Console.WriteLine();
        }

        // Summary statistics
        var totalTankCapacity = station.Tanks.Sum(t => t.UtilCapacity);
        var totalCurrentVolume = station.Tanks.Sum(t => t.LastTankReading?.CurrentVolume ?? 0);
        var totalPumps = station.Dispensers.Sum(d => d.Pumps.Count);
        var totalHoses = station.Dispensers.Sum(d => d.Pumps.Sum(p => p.Hoses.Count));
        var totalDeliveries = station.Dispensers
            .Sum(d => d.Pumps
                .Sum(p => p.Hoses
                    .Sum(h => h.DeliveryInventories.Count)));
        var totalDeliveredVolume = station.Dispensers
            .Sum(d => d.Pumps
                .Sum(p => p.Hoses
                    .Sum(h => h.DeliveryInventories.Sum(di => di.DeliveredVolume))));

        Console.WriteLine("STATION STATISTICS:");
        Console.WriteLine(new string('-', 80));
        Console.WriteLine($"Total Tank Capacity: {totalTankCapacity:N2} L");
        Console.WriteLine($"Current Fuel Volume: {totalCurrentVolume:N2} L ({(totalCurrentVolume/totalTankCapacity):P1} full)");
        Console.WriteLine($"Total Pumps: {totalPumps}");
        Console.WriteLine($"Total Hoses: {totalHoses}");
        Console.WriteLine($"Total Deliveries: {totalDeliveries:N0}");
        Console.WriteLine($"Total Volume Delivered: {totalDeliveredVolume:N2} L");
        Console.WriteLine(new string('=', 80));
    }
}