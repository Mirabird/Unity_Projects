using LinkedListExample;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Collection
{
    public class Task1
    {
        public void TaskLoop()
        {
            List<string> strings = new List<string>() { "строка1", "строка2", "строка3" };

            Console.WriteLine("Список строк:");
            PrintList(strings);

            Console.Write("Введите новую строку: ");
            string newString = Console.ReadLine();
            if (newString == "exit")
            {
                Environment.Exit(0);
            }

            strings.Add(newString);

            Console.WriteLine("Содержимое списка после добавления новой строки:");
            PrintList(strings);

            Console.Write("Введите ещё одну строку: ");
            string additionalString = Console.ReadLine();

            int index = strings.Count / 2;

            strings.Insert(index, additionalString);

            Console.WriteLine("Содержимое списка после добавления строки в середину:");
            PrintList(strings);

            Console.ReadLine();
        }

        private void PrintList(List<string> strings)
        {
            foreach (string str in strings)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine();
        }
    }

    public class Task2
    {
        public void TaskLoop()
        {
            Dictionary<string, double> studentGrades = new Dictionary<string, double>();

            while (true)
            {
                Console.WriteLine("Введите имя студента (или 'exit' для выхода):");
                string name = Console.ReadLine();

                if (name == "exit")
                {
                    break;
                }

                Console.WriteLine("Введите оценку студента (от 2 до 5):");
                double grade;
                bool isValidGrade = double.TryParse(Console.ReadLine(), out grade);

                if (isValidGrade && grade >= 2 && grade <= 5)
                {
                    studentGrades[name] = grade;
                    Console.WriteLine($"Оценка для студента {name} добавлена.");
                }
                else
                {
                    Console.WriteLine($"Некорректная оценка. Оценка должна быть от 2 до 5.");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Введите имя студента для получения его оценки (или 'q' для выхода):");
            string studentName = Console.ReadLine();

            while (studentName != "q")
            {
                if (studentGrades.ContainsKey(studentName))
                {
                    double grade = studentGrades[studentName];
                    Console.WriteLine($"Оценка для студента {studentName}: {grade}");
                }
                else
                {
                    Console.WriteLine($"Студента с именем {studentName} не существует.");
                }

                Console.WriteLine();
                Console.WriteLine("Введите имя студента для получения его оценки (или 'q' для выхода):");
                studentName = Console.ReadLine();
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Выберите номер задачи:");
            Console.WriteLine("1. Создать список строк и добавить в него элементы.");
            Console.WriteLine("2. Создать словарь, связывая имена студентов с их средними оценками.");
            Console.WriteLine("3. Написать реализацию двусвязного списка.");
            Console.WriteLine("Выход - exit!");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Task1 task1 = new Task1();
                    task1.TaskLoop();
                    break;
                case "2":
                    Task2 task2 = new Task2();
                    task2.TaskLoop();
                    break;
                case "3":
                    DoublyLinkedListTask task = new DoublyLinkedListTask();
                    task.TaskLoop();
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Неверный номер задачи.");
                    break;
            }
        }
    }
}