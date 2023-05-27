namespace CommandPattern.Pattern;
//Транзакція, через яку проходять всі команди, щоб вони виконались
public class Transaction
{
    private readonly List<ITransaction> _transactions = 
        new List<ITransaction>();

    public bool HasPendingTransactions =>
        _transactions.Any(x =>
            x.Status == ExecutionStatus.Unprocessed ||
            x.Status == ExecutionStatus.InsufficientFunds ||
            x.Status == ExecutionStatus.ExecuteFailed);

    public void AddTransaction(ITransaction transaction)
    {
        _transactions.Add(transaction);
    }

    public void StartingTransaction()
    {
        // Виконання транзакцій, які не оброблені,
        // або не вдалося успішно виконати їх.
        foreach (ITransaction transaction in _transactions.Where(x =>
                    x.Status == ExecutionStatus.Unprocessed ||
                    x.Status == ExecutionStatus.InsufficientFunds ||
                    x.Status == ExecutionStatus.ExecuteFailed))
        {
            try
            {
                transaction.Execute();
            }
            catch (Exception e)
            {
                transaction.Status = ExecutionStatus.ExecuteFailed;
            }
        }
    }
}