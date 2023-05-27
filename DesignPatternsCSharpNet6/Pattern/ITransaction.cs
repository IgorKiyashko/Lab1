namespace CommandPattern.Pattern;

public interface ITransaction
{
    ExecutionStatus Status { get; set; }
    void Execute();
}