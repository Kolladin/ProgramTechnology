
using System.Text;

namespace Bank;

/// <summary>
/// Представляет банковский счёт с возможностью пополнения,
/// снятия денег и просмотра истории операций.
/// </summary>
public class BankAccount
{
    /// <summary>
    /// Получает или изменяет статус клиента банка.
    /// </summary>
    public AccountStatus Status { get; set; } = AccountStatus.Regular;

    private readonly decimal _minimumBalance;
    static private int s_accountNuberSeed = 1000000000;

    /// <summary>
    /// Получает уникальный номер банковского счёта.
    /// </summary>
    public string Number { get; }

    /// <summary>
    /// Получает имя владельца банковского счёта.
    /// </summary>
    public string Owner { get; private set; }

    /// <summary>
    /// Вычисляет текущий баланс по истории транзакций.
    /// </summary>
    public decimal Balance
    {
        get
        {
            decimal balance = 0;

            foreach (var item in _allTransactions)
            {
                balance += item.Amount;
            }

            return balance;
        }
    }

    private List<Transaction> _allTransactions = new List<Transaction>();

    /// <summary>
    /// Создаёт банковский счёт без минимального остатка.
    /// </summary>
    /// <param name="name">Имя владельца счёта.</param>
    /// <param name="initialBalance">Начальный баланс.</param>
    public BankAccount(string name, decimal initialBalance)
        : this(name, initialBalance, 0)
    {
    }

    /// <summary>
    /// Создаёт банковский счёт с заданным минимальным остатком.
    /// </summary>
    /// <param name="name">Имя владельца счёта.</param>
    /// <param name="initialBalance">Начальный баланс.</param>
    /// <param name="minimumBalance">Минимальный допустимый баланс.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Возникает при некорректной положительной сумме пополнения.
    /// </exception>
    public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
    {
        Owner = name;

        Number = s_accountNuberSeed.ToString();
        s_accountNuberSeed++;

        _minimumBalance = minimumBalance;

        if (initialBalance > 0)
            MakeDeposit(initialBalance, DateTime.UtcNow, "Initial balance");
    }

    /// <summary>
    /// Пополняет банковский счёт на указанную сумму.
    /// </summary>
    /// <param name="amount">Сумма пополнения.</param>
    /// <param name="date">Дата операции.</param>
    /// <param name="note">Комментарий к операции.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Возникает, если сумма меньше или равна нулю.
    /// </exception>
    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Amount of deposit must be positive");
        }

        var deposit = new Transaction(amount, date, note);
        _allTransactions.Add(deposit);

        string operationType = "Deposit";

        if (note == "Apply monthly interest" ||
            note == "Premium monthly interest" ||
            note == "VIP monthly interest")
        {
            operationType = "Interest";
        }

        AuditLog.AddRecord(Number, operationType, amount, date);
    }


    /// <summary>
    /// Списывает деньги со счёта и начисляет кэшбэк VIP-клиентам.
    /// </summary>
    /// <param name="amount">Сумма списания.</param>
    /// <param name="date">Дата операции.</param>
    /// <param name="note">Комментарий к операции.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Возникает, если сумма меньше или равна нулю.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Возникает при превышении допустимого лимита списания.
    /// </exception>
    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

        Transaction? overdraftTransaction =
            ChecklWithdrawalLimit(Balance - amount < _minimumBalance);

        Transaction withdrawal = new(-amount, date, note);
        _allTransactions.Add(withdrawal);

        AuditLog.AddRecord(Number, "Withdrawal", amount, date);

        if (overdraftTransaction is not null)
        {
            _allTransactions.Add(overdraftTransaction);

            AuditLog.AddRecord(
                Number,
                overdraftTransaction.Amount > 0 ? "Deposit" : "Withdrawal",
                Math.Abs(overdraftTransaction.Amount),
                overdraftTransaction.Date);
        }

        if (Status == AccountStatus.VIP)
        {
            decimal cashback = amount * 0.05m;

            MakeDeposit(
                cashback,
                date,
                "VIP cashback");
        }
    }

    /// <summary>
    /// Проверяет превышение допустимого лимита списания.
    /// </summary>
    /// <param name="isOverdrawn">
    /// Признак нарушения ограничения по балансу.
    /// </param>
    /// <returns>
    /// Дополнительную транзакцию или null, если она не требуется.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Возникает при недостаточном остатке на счёте.
    /// </exception>
    protected virtual Transaction? ChecklWithdrawalLimit(bool isOverdrawn)
    {
        if (isOverdrawn)
        {
            throw new InvalidOperationException(
                "Not sufficient rubls for this withdrawal");
        }
        else
        {
            return default;
        }
    }

    /// <summary>
    /// Формирует текстовую историю операций по счёту.
    /// </summary>
    /// <returns>
    /// Строку с датами, суммами, балансами и комментариями операций.
    /// </returns>
    public string GetAccountHistory()
    {
        var report = new StringBuilder();

        decimal balance = 0;
        report.AppendLine("Date\t\tAmount\tBalance\tNote");

        foreach (var item in _allTransactions)
        {
            balance += item.Amount;
            report.AppendLine(
                $"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Note}");
        }

        return report.ToString();
    }

    /// <summary>
    /// Выполняет операции по завершении месяца.
    /// В базовом классе не выполняет действий.
    /// </summary>

    /// <summary>
    /// Выполняет ежемесячное обслуживание счёта
    /// в зависимости от статуса клиента.
    /// </summary>
    public virtual void PerformMonthEndTransactions()
    {
        if (Status == AccountStatus.Regular)
        {
            if (Balance >= 50m)
            {
                MakeWithdrawal(
                    50m,
                    DateTime.UtcNow,
                    "Monthly service fee");
            }
        }
        else if (Status == AccountStatus.Premium)
        {
            if (Balance > 10000m)
            {
                decimal interest = (Balance - 10000m) * 0.01m;

                MakeDeposit(
                    interest,
                    DateTime.UtcNow,
                    "Premium monthly interest");
            }
        }
        else if (Status == AccountStatus.VIP)
        {
            if (Balance > 10000m)
            {
                decimal interest = (Balance - 10000m) * 0.03m;

                MakeDeposit(
                    interest,
                    DateTime.UtcNow,
                    "VIP monthly interest");
            }
        }
    }


    /// <summary>
    /// Возвращает основную информацию о банковском счёте.
    /// </summary>
    /// <returns>
    /// Строку с типом счёта, владельцем, номером и балансом.
    /// </returns>
    public override string ToString()
    {
        return $"Type: {GetType().Name}\tOwner: {Owner}\t Number of account: {Number}\tBalance: {Balance}";
    }
}
