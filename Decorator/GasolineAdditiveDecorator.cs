using Decorator.Pattern;

namespace Decorator;

public class GasolineAdditiveDecorator(IGasStationService wrapper) : BaseServiceDecorator(wrapper)
{
    public override decimal GetServiceCost()
    {
        return base.GetServiceCost() + 45.99m;
    }
}