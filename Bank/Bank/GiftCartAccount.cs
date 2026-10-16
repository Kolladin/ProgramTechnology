
namespace Bank;

/// <summary>
/// Представляет подарочный банковский счёт
/// с возможностью ежемесячного пополнения.
/// </summary>
public class GiftCartAccount : BankAccount
{
    private readonly decimal _monthlyDeposit = 0m;

    /// <summary>
    /// Создаёт подарочный банковский счёт.
    /// </summary>
    /// <param name="name">Имя владельца счёта.</param>
    /// <param name="initialBalance">Начальный баланс счёта.</param>
    /// <param name="monthlyDeposit">
    /// Сумма ежемесячного пополнения. По умолчанию равна нулю.
    /// </param>
    public GiftCartAccount(
        string name,
        decimal initialBalance,
        decimal monthlyDeposit = 0)
        : base(name, initialBalance)
        => _monthlyDeposit = monthlyDeposit;

    /// <summary>
    /// Выполняет ежемесячное пополнение подарочного счёта.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Возникает, если сумма ежемесячного пополнения отрицательная.
    /// </exception>
    public override void PerformMonthEndTransactions()
    {

        base.PerformMonthEndTransactions();

        if (_monthlyDeposit != 0)
        {
            MakeDeposit(
                _monthlyDeposit,
                DateTime.UtcNow,
                "Add monthly deposit");
        }
    }

    /// <summary>
    /// Возвращает информацию о подарочном счёте.
    /// </summary>
    /// <returns>
    /// Строку с информацией о счёте и суммой ежемесячного пополнения.
    /// </returns>
    public override string ToString()
        => base.ToString() + $"monthly deposit: {_monthlyDeposit}";
}
