namespace Visitor.Pattern;

public interface IVisitor
{
    void Visit(OxxoGasStation oxxoGasStation);
    void Visit(DealerGasStation dealerGasStation);
}