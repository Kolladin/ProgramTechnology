using System;

namespace Online_Curse;

/// <summary>
/// Представляет студента онлайн-курса.
/// </summary>
public class Student
{
    /// <summary>
    /// Уникальный идентификатор студента.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Полное имя студента.
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Идентификатор курса.
    /// </summary>
    public int CourseId { get; set; }

    /// <summary>
    /// Электронная почта студента.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Прогресс студента в процентах.
    /// </summary>
    public int Progress { get; set; }

    /// <summary>
    /// Показывает, является ли студент отличником.
    /// </summary>
    public bool IsExcellent
    {
        get
        {
            return Progress >= 90;
        }
    }

    /// <summary>
    /// Показывает, имеет ли студент низкий прогресс.
    /// </summary>
    public bool IsFailing
    {
        get
        {
            return Progress < 50;
        }
    }

    /// <summary>
    /// Создаёт студента.
    /// </summary>
    public Student(int id, string fullName, int courseId, string email, int progress)
    {
        if (id <= 0)
            throw new ArgumentException("Id студента должен быть больше 0");

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("ФИО студента не может быть пустым");

        if (courseId <= 0)
            throw new ArgumentException("CourseId должен быть больше 0");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email не может быть пустым");

        if (progress < 0 || progress > 100)
            throw new ArgumentException("Прогресс должен быть от 0 до 100");

        Id = id;
        FullName = fullName;
        CourseId = courseId;
        Email = email;
        Progress = progress;
    }

    /// <summary>
    /// Возвращает информацию о студенте.
    /// </summary>
    public string GetInfo()
    {
        return $"{FullName} (прогресс {Progress}%)";
    }
}