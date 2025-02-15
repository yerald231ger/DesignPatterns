namespace Visitor.Pattern;

public interface IServiceStation
{
    void Accept(IVisitor visitor);
}