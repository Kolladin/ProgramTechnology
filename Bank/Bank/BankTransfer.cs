
namespace Bank;
/// <summary>
/// Предоставляет возможность перевода денег между банковскими счетами.
/// </summary>
public static class BankTransfer
{
    /// <summary>
    /// Переводит деньги между банковскими счетами.
    /// </summary>
    /// <param name="from">Счёт отправителя.</param> 
    /// <param name="to">Счёт получателя.</param>
    /// <param name="amount">Сумма перевода.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если один из счетов равен null.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Возникает, если сумма перевода неположительная.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Возникает при переводе на тот же счёт
    /// или недостаточном остатке.
    /// </exception>
    public static void Transfer(
        BankAccount from,
        BankAccount to,
        decimal amount)
    {
        ArgumentNullException.ThrowIfNull(from);
        ArgumentNullException.ThrowIfNull(to);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

        if (ReferenceEquals(from, to))
        {
            throw new InvalidOperationException(
                "Нельзя переводить деньги на тот же счёт");
        }

        from.MakeWithdrawal(
            amount,
            DateTime.UtcNow,
            $"Transfer to {to.Number}");

        to.MakeDeposit(
            amount,
            DateTime.UtcNow,
            $"Transfer from {from.Number}");
    }
}
