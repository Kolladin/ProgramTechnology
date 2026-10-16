
namespace Bank;

/// <summary>
/// Представляет кредитный банковский счёт
/// с возможностью использования кредитного лимита.
/// </summary>
public class LineOfCreditAccount : BankAccount
{
    /// <summary>
    /// Создаёт банковский счёт с кредитным лимитом.
    /// </summary>
    /// <param name="name">Имя владельца счёта.</param>
    /// <param name="initialBalance">Начальный баланс счёта.</param>
    /// <param name="creditLimit">
    /// Кредитный лимит, на который разрешено уходить в минус.
    /// </param>
    public LineOfCreditAccount(
        string name,
        decimal initialBalance,
        decimal creditLimit)
        : base(name, initialBalance, -creditLimit)
    {
    }
}
