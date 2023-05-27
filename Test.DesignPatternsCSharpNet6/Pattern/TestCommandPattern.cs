using CommandPattern.Pattern;

namespace TestCommandPattern.Pattern;

public class TestCommandPattern
{
    [Fact]
    public void Test_AllTransactionsSuccessful()
    {
        Transaction trans = new Transaction();
        // Створюємо профіль
        Account newAcc1 = new Account("Нікіта", 0);
        // Використовуємо команду, щоб додати грошей на баланс
        Deposit deposit = new Deposit(newAcc1, 100);
        // Додаємо команду в транзакцію
        trans.AddTransaction(deposit);

        // Команду додано до черги, але не виконано
        Assert.True(trans.HasPendingTransactions);
        Assert.Equal(0, newAcc1.Balance);

        // Виконуємо транзакцію
        trans.StartingTransaction();

        Assert.False(trans.HasPendingTransactions);
        Assert.Equal(100, newAcc1.Balance);

        //Перевіряємо зняття балансу та зміну балансу
        Withdraw withdrawal = new Withdraw(newAcc1, 50);
        trans.AddTransaction(withdrawal);

        trans.StartingTransaction();

        Assert.False(trans.HasPendingTransactions);
        Assert.Equal(50, newAcc1.Balance);
    }

    //Переірка обміну грошей
    [Fact]
    public void Test_Transfer()
    {
        Transaction trans = new Transaction();

        Account newAcc3 = new Account("Алена", 1000);
        Account newAcc4 = new Account("Скам", 100);

        trans.AddTransaction(new Transfer(newAcc3, newAcc4, 750));

        trans.StartingTransaction();
        //Перевіряємо чи вийшло
        Assert.Equal(250, newAcc3.Balance);
        Assert.Equal(850, newAcc4.Balance);
    }


    [Fact]
    public void Test_WithdrawMoreMoney()
    {
        Transaction trans = new Transaction();

        // Створюємо новий профіль з деяким балансом
        Account newAcc2 = new Account("Микола", 175);

        // Змімемо сумму, яка перевищує баланс Миколи
        // Дія не буде виконана через перевірку в Withdraw.Execute
        trans.AddTransaction(new Withdraw(newAcc2, 200));
        // Депозит буде успішним.
        trans.AddTransaction(new Deposit(newAcc2, 75));

        trans.StartingTransaction();

        // Зняття 200 не було завершено, тому що на рахунку недостатньо грошей
        // Отже, це все ще очікує на розгляд
        Assert.True(trans.HasPendingTransactions);
        Assert.Equal(250, newAcc2.Balance);

        // Відкладені транзакції (зняття 200) мають бути виконані зараз
        trans.StartingTransaction();

        Assert.False(trans.HasPendingTransactions);
        Assert.Equal(50, newAcc2.Balance);
    }

}