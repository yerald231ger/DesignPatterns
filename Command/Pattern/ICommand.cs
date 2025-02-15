namespace Command.Pattern;

public interface ICommand;

public interface ICommand<out T> : ICommand
{
    T Execute();
}