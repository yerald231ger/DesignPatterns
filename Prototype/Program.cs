using Prototype.Pattern;

var station = new Station
{
    StationId = 1,
    Name = "Main Station",
    Description = "Central fuel station",
    Latitude = 19.4326,
    Longitude = -99.1332,
    Pl = "PL001"
};

// Add tanks
station.AddTank(1, "Diesel");
station.AddTank(2, "Regular");
station.AddTank(3, "Premium");

// Add pumps for each product
station.AddPump(1, "Diesel");
station.AddPump(2, "Regular");
station.AddPump(3, "Premium");
station.AddPump(4, "Diesel"); // Additional pump for Diesel

Console.WriteLine(station);

var clonedStation = station.Clone();

clonedStation.StationId = 2;
clonedStation.Name = "Secondary Station";
clonedStation.Description = "Secondary fuel station";
clonedStation.Latitude = 19.4326;
clonedStation.Longitude = -99.1332;
clonedStation.Pl = "PL002";

Console.WriteLine(clonedStation);
