using System;

namespace Online_Curse;

/// <summary>
/// Представляет преподавателя онлайн-курса.
/// </summary>
public class Teacher
{
    /// <summary>
    /// Уникальный идентификатор преподавателя.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Полное имя преподавателя.
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Предмет преподавателя.
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// Создаёт преподавателя.
    /// </summary>
    public Teacher(int id, string fullName, string subject)
    {
        if (id <= 0)
            throw new ArgumentException("Id преподавателя должен быть больше 0");

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("ФИО преподавателя не может быть пустым");

        if (string.IsNullOrWhiteSpace(subject))
            throw new ArgumentException("Предмет не может быть пустым");

        Id = id;
        FullName = fullName;
        Subject = subject;
    }

    /// <summary>
    /// Возвращает информацию о преподавателе.
    /// </summary>
    public string GetInfo()
    {
        return $"{FullName} ({Subject})";
    }
}