
namespace InheritanceHomework;

/// <summary>
/// Представляет футболку.
/// </summary>
public class TShirt : Clothing
{
    private bool _isWorn;

    /// <summary>
    /// Размер футболки.
    /// </summary>
    public string Size { get; set; }

    /// <summary>
    /// Проверяет, является ли размер большим.
    /// </summary>
    public bool IsBigSize => Size == "XL" || Size == "XXL";

    /// <summary>
    /// Создаёт футболку с указанными параметрами.
    /// </summary>
    public TShirt(string sku, string name, decimal price, string size)
        : base(sku, name, price)
    {
        Size = size;
    }

    /// <summary>
    /// Надевает футболку.
    /// </summary>
    public override void Wear()
    {
        _isWorn = true;
        Console.WriteLine($"{Name} worn.");
    }

    /// <summary>
    /// Возвращает описание футболки.
    /// </summary>
    public override string ToString()
    {
        return $"[TShirt] {Name}, {Price}, season: {Season}";
    }
}
