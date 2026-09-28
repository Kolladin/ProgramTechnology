using Online_Course;
using System;
using System.Collections.Generic;
using System.IO;

namespace Online_Curse;

/// <summary>
/// Репозиторий для загрузки данных из CSV-файлов.
/// </summary>
public class CsvRepository
{
    private string _basePath;

    /// <summary>
    /// Создаёт CSV-репозиторий.
    /// </summary>
    public CsvRepository(string basePath)
    {
        if (string.IsNullOrWhiteSpace(basePath))
            throw new ArgumentException("Путь к папке не может быть пустым");

        _basePath = basePath;
    }

    /// <summary>
    /// Загружает преподавателей из CSV.
    /// </summary>
    public List<Teacher> GetTeachers()
    {
        string filePath = Path.Combine(_basePath, "teachers.csv");

        if (!File.Exists(filePath))
            throw new FileNotFoundException("Файл teachers.csv не найден");

        string[] lines = File.ReadAllLines(filePath);

        List<Teacher> teachers = new List<Teacher>();

        if (lines == null || lines.Length < 2)
            return teachers;

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
                continue;

            string[] parts = lines[i].Split(',');

            if (parts == null || parts.Length < 3)
                throw new Exception("Некорректная строка в teachers.csv");

            int id = int.Parse(parts[0]);
            string fullName = parts[1];
            string subject = parts[2];

            teachers.Add(new Teacher(id, fullName, subject));
        }

        return teachers;
    }

    /// <summary>
    /// Загружает курсы из CSV.
    /// </summary>
    public List<Course> GetCourses()
    {
        string filePath = Path.Combine(_basePath, "courses.csv");

        if (!File.Exists(filePath))
            throw new FileNotFoundException("Файл courses.csv не найден");

        string[] lines = File.ReadAllLines(filePath);

        List<Course> courses = new List<Course>();

        if (lines == null || lines.Length < 2)
            return courses;

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
                continue;

            string[] parts = lines[i].Split(',');

            if (parts == null || parts.Length < 5)
                throw new Exception("Некорректная строка в courses.csv");

            int id = int.Parse(parts[0]);
            string title = parts[1];
            int teacherId = int.Parse(parts[2]);
            int duration = int.Parse(parts[3]);
            decimal price = decimal.Parse(parts[4]);

            courses.Add(
                new Course(id, title, teacherId, duration, price)
            );
        }

        return courses;
    }

    /// <summary>
    /// Загружает студентов из CSV.
    /// </summary>
    public List<Student> GetStudents()
    {
        string filePath = Path.Combine(_basePath, "students.csv");

        if (!File.Exists(filePath))
            throw new FileNotFoundException("Файл students.csv не найден");

        string[] lines = File.ReadAllLines(filePath);

        List<Student> students = new List<Student>();

        if (lines == null || lines.Length < 2)
            return students;

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
                continue;

            string[] parts = lines[i].Split(',');

            if (parts == null || parts.Length < 5)
                throw new Exception("Некорректная строка в students.csv");

            int id = int.Parse(parts[0]);
            string fullName = parts[1];
            int courseId = int.Parse(parts[2]);
            string email = parts[3];
            int progress = int.Parse(parts[4]);

            students.Add(
                new Student(id, fullName, courseId, email, progress)
            );
        }

        return students;
    }
}