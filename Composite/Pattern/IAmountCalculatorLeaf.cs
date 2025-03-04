namespace Composite.Pattern;

public interface IAmountCalculatorComposite : IAmountCalculatorLeaf
{
    decimal GetAmount(Product productId);
}

public interface IAmountCalculatorLeaf
{
    decimal GetAmount();
}