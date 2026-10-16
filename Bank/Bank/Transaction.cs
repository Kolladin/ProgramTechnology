
namespace Bank;

/// <summary>
/// Представляет банковскую транзакцию,
/// содержащую сумму, дату и комментарий.
/// </summary>
/// <param name="Amount">
/// Сумма операции. Положительная для пополнения,
/// отрицательная для списания.
/// </param>
/// <param name="Date">Дата проведения операции.</param>
/// <param name="Note">Комментарий к операции.</param>
public record Transaction(decimal Amount, DateTime Date, string Note);
