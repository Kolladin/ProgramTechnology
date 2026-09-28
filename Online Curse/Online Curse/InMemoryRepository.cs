using Online_Course;
using System;
using System.Collections.Generic;

namespace Online_Curse;

/// <summary>
/// Репозиторий для хранения данных в памяти.
/// </summary>
public class InMemoryRepository
{
    private List<Teacher> _teachers;
    private List<Course> _courses;
    private List<Student> _students;

    /// <summary>
    /// Создаёт репозиторий и заполняет его данными.
    /// </summary>
    public InMemoryRepository()
    {
        _teachers = new List<Teacher>
        {
            new Teacher(1, "Петров П.П.", "Программирование"),
            new Teacher(2, "Иванова А.А.", "Математика"),
            new Teacher(3, "Сидоров С.С.", "Базы данных"),
            new Teacher(4, "Кузнецов И.И.", "Компьютерная графика"),
            new Teacher(5, "Смирнова Е.В.", "Английский язык")
        };

        _courses = new List<Course>
        {
            new Course(1, "C# для начинающих", 1, 40, 15000),
            new Course(2, "Основы Python", 1, 50, 18000),
            new Course(3, "Математика для программистов", 2, 30, 10000),
            new Course(4, "SQL и базы данных", 3, 45, 16000),
            new Course(5, "Компьютерная графика", 4, 55, 20000)
        };

        _students = new List<Student>
        {
            new Student(1, "Сидоров С.С.", 1, "sidorov@mail.ru", 75),
            new Student(2, "Иванов И.И.", 1, "ivanov@mail.ru", 95),
            new Student(3, "Петров П.П.", 2, "petrov@mail.ru", 88),
            new Student(4, "Кузнецов К.К.", 2, "kuznetsov@mail.ru", 45),
            new Student(5, "Смирнова С.С.", 3, "smirnova@mail.ru", 92),
            new Student(6, "Орлов О.О.", 4, "orlov@mail.ru", 67),
            new Student(7, "Васильев В.В.", 5, "vasilev@mail.ru", 38)
        };
    }

    /// <summary>
    /// Возвращает преподавателей.
    /// </summary>
    public List<Teacher> GetTeachers()
    {
        return _teachers;
    }

    /// <summary>
    /// Возвращает курсы.
    /// </summary>
    public List<Course> GetCourses()
    {
        return _courses;
    }

    /// <summary>
    /// Возвращает студентов.
    /// </summary>
    public List<Student> GetStudents()
    {
        return _students;
    }
}