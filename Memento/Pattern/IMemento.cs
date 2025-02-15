namespace Memento.Pattern;

public interface IMemento<T>
{
    T Save();
    void Restore(T memento);
}