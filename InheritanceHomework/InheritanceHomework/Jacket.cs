namespace InheritanceHomework;

/// <summary>
/// Представляет куртку.
/// </summary>
public class Jacket : Clothing, IWashable
{
    private bool _isWashed;

    /// <summary>
    /// Уровень утепления куртки.
    /// </summary>
    public int Insulation { get; set; }

    /// <summary>
    /// Определяет, является ли куртка тёплой.
    /// </summary>
    public bool IsWarm => Insulation >= 5;

    /// <summary>
    /// Создаёт куртку с указанными параметрами.
    /// </summary>
    public Jacket(string sku, string name, decimal price, int insulation)
        : base(sku, name, price)
    {
        Insulation = insulation;
    }

    /// <summary>
    /// Надевает куртку.
    /// </summary>
    public override void Wear()
    {
        Console.WriteLine($"{Name} worn.");
    }

    /// <summary>
    /// Стирает куртку.
    /// </summary>
    public void Wash()
    {
        _isWashed = true;
        Console.WriteLine($"{Name} washed.");
    }

    /// <summary>
    /// Возвращает описание куртки.
    /// </summary>
    public override string ToString()
    {
        return $"[Jacket] {Name}, {Price}, season: {Season}";
    }
}
