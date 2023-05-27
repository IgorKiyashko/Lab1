namespace CommandPattern.Pattern;
//Класс, за допомогою якого створюються профілі
public class Account
{
    public string NameAccount { get; set; }
    public decimal Balance { get; set; }

    public Account(string NameAcc, decimal balance)
    {
        NameAccount = NameAcc;
        Balance = balance;
    }
}