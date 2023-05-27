namespace CommandPattern.Pattern;
//Клас трансфер, дозволяє перекидумати частину грошей з одного балансу на інший
public class Transfer : ITransaction
{
    private readonly decimal _amount;
    private readonly Account _fromAccount;
    private readonly Account _toAccount;

    public ExecutionStatus Status { get; set; }

    public Transfer(Account fromAccount, Account toAccount, decimal amount)
    {
        _fromAccount = fromAccount;
        _toAccount = toAccount;
        _amount = amount;

        Status = ExecutionStatus.Unprocessed;
    }

    public void Execute()
    {
        if(_fromAccount.Balance >= _amount)
        {
            _fromAccount.Balance -= _amount;
            _toAccount.Balance += _amount;

            Status = ExecutionStatus.ExecuteSucceeded;
        }
        else
        {
            Status = ExecutionStatus.InsufficientFunds;
        }
    }
}