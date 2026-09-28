using Online_Course;
using System;
using System.Collections.Generic;
using System.IO;

namespace Online_Curse;

public class Program
{
    /// <summary>
    /// Точка входа в программу.
    /// </summary>
    public static void Main()
    {
        try
        {
            Console.WriteLine("Выберите источник данных:");
            Console.WriteLine("1 - InMemory");
            Console.WriteLine("2 - CSV");
            Console.Write("Ваш выбор: ");

            string choice = Console.ReadLine();

            List<Teacher> teachers;
            List<Course> courses;
            List<Student> students;

            switch (choice)
            {
                case "1":
                    InMemoryRepository memoryRepository =
                        new InMemoryRepository();

                    teachers = memoryRepository.GetTeachers();
                    courses = memoryRepository.GetCourses();
                    students = memoryRepository.GetStudents();

                    if (teachers is null ||
                        courses is null ||
                        students is null)
                    {
                        throw new Exception(
                            "Не удалось получить данные из InMemoryRepository"
                        );
                    }

                    break;

                case "2":
                    string dataPath = Path.Combine(
                        AppContext.BaseDirectory,
                        "data"
                    );

                    CsvRepository csvRepository =
                        new CsvRepository(dataPath);

                    teachers = csvRepository.GetTeachers();
                    courses = csvRepository.GetCourses();
                    students = csvRepository.GetStudents();

                    if (teachers == null ||
                        courses == null ||
                        students == null)
                    {
                        throw new Exception(
                            "Не удалось получить данные из CSV"
                        );
                    }

                    break;

                default:
                    throw new Exception("Неверно выбран источник данных");
            }

            Console.WriteLine();

            Teacher teacher = FindTeacher(
                teachers,
                courses,
                "C# для начинающих"
            );

            if (teacher != null)
                Console.WriteLine($"Преподаватель: {teacher.GetInfo()}");
            else
                Console.WriteLine("Преподаватель не найден");

            Console.WriteLine();

            Student student = FindStudent(
                students,
                courses,
                "C# для начинающих"
            );

            if (student != null)
                Console.WriteLine($"Студент: {student.GetInfo()}");
            else
                Console.WriteLine("Студент не найден");

            Console.WriteLine();

            int totalDuration = GetTotalDuration(courses);

            Console.WriteLine(
                $"Общая продолжительность: {totalDuration} часов"
            );

            Console.WriteLine();

            List<Student> topStudents =
                GetTopStudents(students, 3);

            Console.WriteLine("Топ-3 студентов:");

            foreach (Student topStudent in topStudents)
            {
                Console.WriteLine(
                    $"{topStudent.FullName} - {topStudent.Progress}%"
                );
            }

            Console.WriteLine();

            Console.WriteLine("Все курсы:");
            PrintAllCourses(teachers, courses, students);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    /// <summary>
    /// Находит преподавателя указанного курса.
    /// </summary>
    public static Teacher FindTeacher(
        List<Teacher> teachers,
        List<Course> courses,
        string title)
    {
        if (teachers == null || courses == null || title == null)
            return null;

        foreach (Course course in courses)
        {
            if (course == null)
                continue;

            if (course.Title == title)
            {
                foreach (Teacher teacher in teachers)
                {
                    if (teacher == null)
                        continue;

                    if (teacher.Id == course.TeacherId)
                        return teacher;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Находит студента указанного курса.
    /// </summary>
    public static Student FindStudent(
        List<Student> students,
        List<Course> courses,
        string title)
    {
        if (students == null || courses == null || title == null)
            return null;

        foreach (Course course in courses)
        {
            if (course == null)
                continue;

            if (course.Title == title)
            {
                foreach (Student student in students)
                {
                    if (student == null)
                        continue;

                    if (student.CourseId == course.Id)
                        return student;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Возвращает общую продолжительность всех курсов.
    /// </summary>
    public static int GetTotalDuration(List<Course> courses)
    {
        if (courses == null)
            return 0;

        int total = 0;

        foreach (Course course in courses)
        {
            if (course == null)
                continue;

            total += course.Duration;
        }

        return total;
    }

    /// <summary>
    /// Возвращает лучших студентов по прогрессу.
    /// </summary>
    public static List<Student> GetTopStudents(
        List<Student> students,
        int n)
    {
        List<Student> result = new List<Student>();

        if (students == null || n <= 0)
            return result;

        foreach (Student student in students)
        {
            if (student != null)
                result.Add(student);
        }

        // Сортировка пузырьком по убыванию прогресса
        for (int i = 0; i < result.Count - 1; i++)
        {
            for (int j = 0; j < result.Count - 1 - i; j++)
            {
                if (result[j].Progress < result[j + 1].Progress)
                {
                    Student temp = result[j];
                    result[j] = result[j + 1];
                    result[j + 1] = temp;
                }
            }
        }

        if (n > result.Count)
            n = result.Count;

        List<Student> top = new List<Student>();

        for (int i = 0; i < n; i++)
        {
            top.Add(result[i]);
        }

        return top;
    }

    /// <summary>
    /// Выводит информацию обо всех курсах.
    /// </summary>
    public static void PrintAllCourses(
        List<Teacher> teachers,
        List<Course> courses,
        List<Student> students)
    {
        if (teachers == null ||
            courses == null ||
            students == null)
        {
            return;
        }

        foreach (Course course in courses)
        {
            if (course == null)
                continue;

            Teacher teacher = FindTeacher(
                teachers,
                courses,
                course.Title
            );

            bool studentFound = false;

            foreach (Student student in students)
            {
                if (student == null)
                    continue;

                if (student.CourseId == course.Id)
                {
                    studentFound = true;

                    string teacherName = "—";

                    if (teacher != null)
                        teacherName = teacher.FullName;

                    Console.WriteLine(
                        $"{course.GetInfo()} — преподаватель " +
                        $"{teacherName}, студент " +
                        $"{student.FullName} ({student.Progress}%)"
                    );
                }
            }

            if (!studentFound)
            {
                string teacherName = "—";

                if (teacher != null)
                    teacherName = teacher.FullName;

                Console.WriteLine(
                    $"{course.GetInfo()} — преподаватель " +
                    $"{teacherName}, студент —"
                );
            }
        }
    }
}