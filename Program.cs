using System;
using System.IO;

namespace StudentDataViewer
{
    public class Student
    {
        public string StudentID;
        public string Name;
        public int Age;
        public string Grade;

        public override string ToString()
        {
            return $"Student id : {StudentID}\nName : {Name}\nAge : {Age}\nGrade : {Grade}\n";
        }
    }

    class Program
    {
        static Student[] ReadStudentDataFromFile(string filename)
        {
            Student[] students = null;

            try
            {
                string[] lines = File.ReadAllLines(filename);

                students = new Student[lines.Length / 4];
                int studentIndex = -1;

                for (int i = 0; i < lines.Length; i += 4)
                {
                    string studentIDLine = lines[i];
                    string nameLine = lines[i + 1];
                    string ageLine = lines[i + 2];
                    string gradeLine = lines[i + 3];

                    string[] studentIDData = studentIDLine.Split(':');
                    string[] nameData = nameLine.Split(':');
                    string[] ageData = ageLine.Split(':');
                    string[] gradeData = gradeLine.Split(':');

                    if (studentIDData.Length == 2 && nameData.Length == 2 && ageData.Length == 2 && gradeData.Length == 2)
                    {
                        string studentID = studentIDData[1].Trim();
                        string name = nameData[1].Trim();
                        if (int.TryParse(ageData[1].Trim(), out int age))
                        {
                            string grade = gradeData[1].Trim();

                            Student student = new Student
                            {
                                StudentID = studentID,
                                Name = name,
                                Age = age,
                                Grade = grade
                            };

                            students[++studentIndex] = student;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error: Invalid student data format in lines {i + 1}-{i + 4}");
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading the file: " + ex.Message);
            }

            return students;
        }

        static void DisplayStudentData(Student[] students)
        {
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }

        static void Main(string[] args)
        {
            string filename = "studentgrades.txt";
            Student[] students = ReadStudentDataFromFile(filename);
            DisplayStudentData(students);
            Console.ReadKey();
        }
    }
}