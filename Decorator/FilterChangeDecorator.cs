using Decorator.Pattern;

namespace Decorator;

public class FilterChangeDecorator(IGasStationService wrapper) : BaseServiceDecorator(wrapper)
{
    public override decimal GetServiceCost()
    {
        return base.GetServiceCost() + 19.99m;
    }
}