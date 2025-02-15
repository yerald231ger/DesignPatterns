using Command.Pattern;

namespace Command.Commands;

public record CleanWindshieldResult(bool IsCleaned);

public record CleanWindshieldCommand(Client Client, Rcc Rcc, decimal Tip) : ICommand<CleanWindshieldResult>
{
    public CleanWindshieldResult Execute()
    {
        var result = Rcc.CleanWindshield(Client, Tip);
        return new CleanWindshieldResult(result);
    }
}