namespace InheritanceHomework;

/// <summary>
/// Представляет базовый класс одежды.
/// </summary>
public abstract class Clothing
{
    private string _sku;

    /// <summary>
    /// Название одежды.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Цена одежды.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Сезон одежды. По умолчанию AllSeason.
    /// </summary>
    public Season Season { get; set; } = Season.AllSeason;

    /// <summary>
    /// Цена одежды со скидкой 10%.
    /// </summary>
    public decimal PriceWithDiscount => Price * 0.9m;

    /// <summary>
    /// Создаёт одежду с артикулом, названием и ценой.
    /// </summary>
    /// <param name="sku">Артикул одежды.</param>
    /// <param name="name">Название одежды.</param>
    /// <param name="price">Цена одежды.</param>
    public Clothing(string sku, string name, decimal price)
    {
        _sku = sku;
        Name = name;
        Price = price;
    }

    /// <summary>
    /// Создаёт одежду с названием.
    /// </summary>
    /// <param name="name">Название одежды.</param>
    public Clothing(string name) : this("N/A", name, 0)
    {
    }

    /// <summary>
    /// Выполняет действие ношения одежды.
    /// </summary>
    public abstract void Wear();
}
