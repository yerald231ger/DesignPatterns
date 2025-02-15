using Visitor;
using Visitor.Pattern;

var stations = new List<IServiceStation>
{
    DealerGasStation.Create(),
    OxxoGasStation.Create()
};

var visitors = new List<IVisitor>
{
    new InventoryReporter(),
    new AuditorVisitor()
};

foreach (var visitor in visitors)
{
    foreach (var station in stations)
    {
        station.Accept(visitor);
    }
}