
namespace InheritanceHomework;

/// <summary>
/// Представляет обувь.
/// </summary>
public class Shoes : Clothing
{
    private bool _isWorn;

    /// <summary>
    /// Толщина подошвы.
    /// </summary>
    public decimal SoleThickness { get; set; }

    /// <summary>
    /// Определяет, является ли обувь спортивной.
    /// </summary>
    public bool IsSporty => SoleThickness >= 3;

    /// <summary>
    /// Создаёт обувь с указанными параметрами.
    /// </summary>
    public Shoes(string sku, string name, decimal price, decimal soleThickness)
        : base(sku, name, price)
    {
        SoleThickness = soleThickness;
    }

    /// <summary>
    /// Надевает обувь.
    /// </summary>
    public override void Wear()
    {
        _isWorn = true;
        Console.WriteLine($"{Name} worn.");
    }

    /// <summary>
    /// Возвращает описание обуви.
    /// </summary>
    public override string ToString()
    {
        return $"[Shoes] {Name}, {Price}, season: {Season}";
    }
}
    