using System;

namespace Online_Course;

/// <summary>
/// Представляет онлайн-курс.
/// </summary>
public class Course
{
    /// <summary>
    /// Уникальный идентификатор курса.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название курса.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Идентификатор преподавателя.
    /// </summary>
    public int TeacherId { get; set; }

    /// <summary>
    /// Продолжительность курса в часах.
    /// </summary>
    public int Duration { get; set; }

    /// <summary>
    /// Стоимость курса.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Стоимость одного часа курса.
    /// </summary>
    public decimal PricePerHour
    {
        get
        {
            if (Duration == 0)
                return 0;

            return Price / Duration;
        }
    }

    /// <summary>
    /// Показывает, является ли курс длительным.
    /// </summary>
    public bool IsLong
    {
        get
        {
            return Duration > 40;
        }
    }

    /// <summary>
    /// Создаёт курс.
    /// </summary>
    public Course(int id, string title, int teacherId, int duration, decimal price)
    {
        if (id <= 0)
            throw new ArgumentException("Id курса должен быть больше 0");

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Название курса не может быть пустым");

        if (teacherId <= 0)
            throw new ArgumentException("TeacherId должен быть больше 0");

        if (duration < 0)
            throw new ArgumentException("Продолжительность не может быть отрицательной");

        if (price < 0)
            throw new ArgumentException("Цена не может быть отрицательной");

        Id = id;
        Title = title;
        TeacherId = teacherId;
        Duration = duration;
        Price = price;
    }

    /// <summary>
    /// Возвращает информацию о курсе.
    /// </summary>
    public string GetInfo()
    {
        return $"{Title} ({Duration} ч, {Price} руб.)";
    }
}