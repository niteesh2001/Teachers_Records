using System;
using System.Collections.Generic;
using System.IO;

class Teacher
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string ClassSection { get; set; }
}

class FileHandler
{
    private const string FilePath = "Teachers_Data.txt";

    public static List<Teacher> ReadTeachersFromFile()
    {
        List<Teacher> teachers = new List<Teacher>();

        if (File.Exists(FilePath))
        {
            string[] lines = File.ReadAllLines(FilePath);

            foreach (string line in lines)
            {
                string[] data = line.Split(',');
                Teacher teacher = new Teacher
                {
                    ID = int.Parse(data[0]),
                    Name = data[1],
                    ClassSection = data[2]
                };
                teachers.Add(teacher);
            }
        }

        return teachers;
    }

    public static void WriteTeachersToFile(List<Teacher> teachers)
    {
        using (StreamWriter writer = new StreamWriter(FilePath))
        {
            foreach (Teacher teacher in teachers)
            {
                writer.WriteLine($"{teacher.ID},{teacher.Name},{teacher.ClassSection}");
            }
        }
    }
}

class TeacherManager
{
    private static List<Teacher> teachers;

    static TeacherManager()
    {
        teachers = FileHandler.ReadTeachersFromFile();
    }

    public static void AddTeacher(Teacher teacher)
    {
        teachers.Add(teacher);
        FileHandler.WriteTeachersToFile(teachers);
    }

    public static void UpdateTeacher(Teacher updatedTeacher)
    {
        Teacher existingTeacher = teachers.Find(t => t.ID == updatedTeacher.ID);

        if (existingTeacher != null)
        {
            existingTeacher.Name = updatedTeacher.Name;
            existingTeacher.ClassSection = updatedTeacher.ClassSection;
            FileHandler.WriteTeachersToFile(teachers);
        }
        else
        {
            Console.WriteLine("Teacher not found.");
        }
    }

    public static void DeleteTeacher(int teacherID)
    {
        Teacher teacherToDelete = teachers.Find(t => t.ID == teacherID);

        if (teacherToDelete != null)
        {
            teachers.Remove(teacherToDelete);
            FileHandler.WriteTeachersToFile(teachers);
        }
        else
        {
            Console.WriteLine("Teacher not found.");
        }
    }

    public static void DisplayTeachers()
    {
        foreach (Teacher teacher in teachers)
        {
            Console.WriteLine($"ID: {teacher.ID}, Name: {teacher.Name}, ClassSection: {teacher.ClassSection}");
        }
    }
}

class Program
{
    static void Main()
    {
        int choice;
        do
        {
            Console.WriteLine("1. Add Teacher");
            Console.WriteLine("2. Update Teacher");
            Console.WriteLine("3. Delete Teacher");
            Console.WriteLine("4. Display Teachers");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
            int.TryParse(Console.ReadLine(), out choice);

            switch (choice)
            {
                case 1:
                    Console.Write("Enter Teacher ID: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Enter Teacher Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Class and Section: ");
                    string classSection = Console.ReadLine();
                    TeacherManager.AddTeacher(new Teacher { ID = id, Name = name, ClassSection = classSection });
                    break;
                case 2:
                    Console.Write("Enter Teacher ID to update: ");
                    int updateID = int.Parse(Console.ReadLine());
                    Console.Write("Enter Updated Teacher Name: ");
                    string updateName = Console.ReadLine();
                    Console.Write("Enter Updated Class and Section: ");
                    string updateClassSection = Console.ReadLine();
                    TeacherManager.UpdateTeacher(new Teacher { ID = updateID, Name = updateName, ClassSection = updateClassSection });
                    break;
                case 3:
                    Console.Write("Enter Teacher ID to delete: ");
                    int deleteID = int.Parse(Console.ReadLine());
                    TeacherManager.DeleteTeacher(deleteID);
                    break;
                case 4:
                    TeacherManager.DisplayTeachers();
                    break;
                case 5:
                    Console.WriteLine("Exiting program.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again."); 
                    break;
            }
        } while (choice != 5);
    }
}