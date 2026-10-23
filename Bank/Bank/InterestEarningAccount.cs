
namespace Bank;

/// <summary>
/// Представляет банковский счёт с ежемесячным
/// начислением процентов на остаток.
/// </summary>
public class InterestEarningAccount : BankAccount
{
    /// <summary>
    /// Создаёт банковский счёт с начислением процентов.
    /// </summary>
    /// <param name="name">Имя владельца счёта.</param>
    /// <param name="initialBalance">Начальный баланс счёта.</param>
    public InterestEarningAccount(string name, decimal initialBalance)
        : base(name, initialBalance)
    {
    }

    /// <summary>
    /// Начисляет 2% от остатка при балансе более 500 рублей.
    /// </summary>
    public override void PerformMonthEndTransactions()
    {
         
        base.PerformMonthEndTransactions();

        if (Balance > 500m)
        {
            decimal interest = Balance * 0.02m;

            MakeDeposit(
                interest,
                DateTime.UtcNow,
                "Apply monthly interest");
        }
    }
}
