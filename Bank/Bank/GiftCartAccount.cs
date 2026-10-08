namespace Bank;

public class GiftCartAccount: BankAccount
{
    private readonly decimal _monthlyDeposit = 0m;

    // monthlyDeposit - параметр по умолчанию, если не передан, то будет 0
    // при создании new GiftCartAccount("Yana", 1000m, 50m) - monthlyDeposit = 0
    // new GiftCartAccount("Yana", 1000, 5000); => monthlyDeposit = 5000
    public GiftCartAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0)
        :base(name, initialBalance)
        => _monthlyDeposit = monthlyDeposit;

    public override void PerformMonthEndTransactions()
    {
        if (_monthlyDeposit != 0)
        {
            MakeDeposit(_monthlyDeposit, DateTime.UtcNow, "Add monthly deposit");
        }
    }

    public override string ToString()
        => base.ToString() + $"monthly deposit: {_monthlyDeposit}";


}
