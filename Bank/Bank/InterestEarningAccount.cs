namespace Bank;

public class InterestEarningAccount: BankAccount
{
    public InterestEarningAccount(string name, decimal initialBalance) : base(name, initialBalance)
    {

    }

    // override позволяет в дочернем классе 

    public override void PerformMonthEndTransactions()
    {
        if (Balance > 500m)
        {
            decimal interest = Balance * 0.02m;
            MakeDeposit(interest, DateTime.UtcNow, "Apply monthly interest");
        }
    }

}
