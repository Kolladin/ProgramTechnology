namespace Bank;

/// <summary>
/// Хранит журнал банковских операций.
/// </summary>
public class AuditLog
{
    private static List<string> _records = new List<string>();

    /// <summary>
    /// Добавляет запись об операции в журнал.
    /// </summary>
    /// <param name="accountNumber">Номер счёта.</param>
    /// <param name="operationType">Тип операции.</param>
    /// <param name="amount">Сумма операции.</param>
    /// <param name="date">Дата операции.</param>
    public static void AddRecord(
        string accountNumber,
        string operationType,
        decimal amount,
        DateTime date)
    {
        string record =
            $"{accountNumber} | {operationType} | {amount} | {date}";

        _records.Add(record);
    }

    /// <summary>
    /// Выводит журнал операций в консоль.
    /// </summary>
    public static void PrintLog()
    {
        Console.WriteLine("\nЖурнал аудита");

        foreach (string record in _records) 
        {
            Console.WriteLine(record);
        }
    }
}
