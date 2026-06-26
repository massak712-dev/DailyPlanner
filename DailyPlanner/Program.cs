using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPlanner
{
    internal class Program
    {
        static List<string> tasks = new List<string>();
        static List<DateTime> deadlines = new List<DateTime>();
        static string filePath = "tasks.txt";

        static void Main(string[] args)
        {
            LoadTasks();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ЕЖЕДНЕВНИК ===");
                Console.WriteLine();
                Console.WriteLine("1. Добавить задачу");
                Console.WriteLine("2. Показать все задачи");
                Console.WriteLine("3. Показать просроченные задачи");
                Console.WriteLine("4. Удалить задачу");
                Console.WriteLine("5. Выйти и сохранить");
                Console.WriteLine();
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    AddTask();
                }
                else if (choice == "2")
                {
                    ShowTasks();
                }
                else if (choice == "3")
                {
                    ShowOverdueTasks();
                }
                else if (choice == "4")
                {
                    DeleteTask();
                }
                else if (choice == "5")
                {
                    SaveTasks();
                    Console.WriteLine("Данные сохранены. До свидания!");
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный ввод!");
                }

                Console.WriteLine();
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }

        static void AddTask()
        {
            Console.Clear();
            Console.WriteLine("ДОБАВЛЕНИЕ НОВОЙ ЗАДАЧИ ");
            Console.WriteLine();

            Console.Write("Введите задачу: ");
            string task = Console.ReadLine();

            if (string.IsNullOrEmpty(task))
            {
                Console.WriteLine("Задача не может быть пустой!");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Введите дедлайн:");
            Console.WriteLine("  дд.мм.гггг чч:мм  (например: 28.06.2026 18:00)");
            Console.Write("> ");

            string deadLineInput = Console.ReadLine();

            if (!DateTime.TryParse(deadLineInput, out DateTime deadLine))
            {
                Console.WriteLine("Неверный формат даты! Используйте дд.мм.гггг чч:мм");
                return;
            }

            tasks.Add(task);
            deadlines.Add(deadLine);
            SaveTasks();

            Console.WriteLine();
            Console.WriteLine($"Задача добавлена! Дедлайн: {deadLine:dd.MM.yyyy HH:mm}");
        }

        static void ShowTasks()
        {
            Console.Clear();
            Console.WriteLine(" СПИСОК ЗАДАЧ ");
            Console.WriteLine();

            if (tasks.Count == 0)
            {
                Console.WriteLine("Нет задач!");
                return;
            }

            for (int i = 0; i < tasks.Count; i++)
            {
                Console.Write($"{i + 1}. {tasks[i]}");

                TimeSpan remaining = deadlines[i] - DateTime.Now;

                if (remaining.TotalHours < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($" ПРОСРОЧЕНО! (-{(-remaining).Days}д {(-remaining).Hours}ч {(-remaining).Minutes}мин)");
                    Console.ResetColor();
                }
                else
                {
                    double totalHours = (deadlines[i] - DateTime.Now.Date).TotalHours;

                    if (deadlines[i].Date == DateTime.Now.Date)
                    {
                        double percent = (remaining.TotalHours / 24) * 100;

                        if (percent > 70)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else if (percent > 20)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }

                        Console.Write($" {remaining.Hours}ч {remaining.Minutes}мин (осталось)");
                        Console.ResetColor();
                    }
                    else
                    {
                        int daysLeft = remaining.Days;
                        int hoursLeft = remaining.Hours;
                        int minutesLeft = remaining.Minutes;

                        if (daysLeft > 3)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else if (daysLeft > 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }

                        if (daysLeft > 0)
                            Console.Write($" {daysLeft}д {hoursLeft}ч {minutesLeft}мин (осталось)");
                        else
                            Console.Write($" {hoursLeft}ч {minutesLeft}мин (осталось)");

                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }
        }

        static void ShowOverdueTasks()
        {
            Console.Clear();
            Console.WriteLine("ПРОСРОЧЕННЫЕ ЗАДАЧИ");
            Console.WriteLine();

            bool hasOverdue = false;

            for (int i = 0; i < tasks.Count; i++)
            {
                if (deadlines[i] < DateTime.Now)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{i + 1}. {tasks[i]}");

                    TimeSpan overdue = DateTime.Now - deadlines[i];
                    Console.WriteLine($" (просрочено на {overdue.Days}д {overdue.Hours}ч {overdue.Minutes}мин)");

                    Console.ResetColor();
                    hasOverdue = true;
                }
            }

            if (!hasOverdue)
            {
                Console.WriteLine("Просроченных задач нет");
            }
        }

        static void DeleteTask()
        {
            Console.Clear();
            Console.WriteLine(" УДАЛЕНИЕ ЗАДАЧИ ");
            Console.WriteLine();

            if (tasks.Count == 0)
            {
                Console.WriteLine("Нет задач для удаления!");
                return;
            }

            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tasks[i]} (дедлайн: {deadlines[i]:dd.MM.yyyy HH:mm})");
            }

            Console.WriteLine();
            Console.Write("Введите номер задачи для удаления: ");

            int index = int.Parse(Console.ReadLine());
            if (index > 0 && index <= tasks.Count)
            {
                Console.WriteLine($"Удалена задача: {tasks[index - 1]}");
                tasks.RemoveAt(index - 1);
                deadlines.RemoveAt(index - 1);
                SaveTasks();
            }
            else
            {
                Console.WriteLine("Неверный индекс!");
            }
        }

        static void SaveTasks()
        {
            List<string> lines = new List<string>();

            for (int i = 0; i < tasks.Count; i++)
            {
                string line = $"{tasks[i]}|{deadlines[i]:yyyy-MM-dd HH:mm}";
                lines.Add(line);
            }

            File.WriteAllLines(filePath, lines);
        }

        static void LoadTasks()
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');

                if (parts.Length == 2)
                {
                        tasks.Add(parts[0]);
                        deadlines.Add(DateTime.Parse(parts[1]));
                    
                   
                }
            }
        }
    }
}