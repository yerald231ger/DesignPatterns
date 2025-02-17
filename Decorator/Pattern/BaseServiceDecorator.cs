namespace Decorator.Pattern;

public class BaseServiceDecorator(IGasStationService wrapper) : IGasStationService
{
    public IGasStationService Wrapper { get; set; } = wrapper;
    
    public virtual decimal GetServiceCost()
    {
        return Wrapper.GetServiceCost();
    }
}