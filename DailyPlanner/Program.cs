using System;
using System.Collections.Generic;
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

            //LoadTasks();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ЕЖЕДНЕВНИК ===");
                Console.WriteLine();
                Console.WriteLine("1. Добавить задачу");
                Console.WriteLine("2. Показать все задачи");
                Console.WriteLine("3. Показать просроченные задачи");
                Console.WriteLine("4. Удалить задачу");
                Console.WriteLine("5. Выйти");
                Console.WriteLine();
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                    if (choice == "1")
                    {
                        AddTask();
                    }
                    else if (choice == "2")
                    {
                        showTasks();
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
                        //SaveTasks();
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
            Console.WriteLine("Добавлние новой задачи");
            Console.Write("Введите задачу: ");
            string task=Console.ReadLine();
            if(string.IsNullOrEmpty(task))
            {
                Console.WriteLine("задача не может быть пустой");
                return;
            }
            Console.WriteLine("\n Введите дедлайн:");
            Console.WriteLine(" дд.мм.гггг чч:мм  (например: 28.06.2026 18:00)");
            string deadLineInput=Console.ReadLine();
            DateTime deadLine= DateTime.Parse(deadLineInput);
            tasks.Add(task);
            deadlines.Add(deadLine);
            //сохранение
            Console.WriteLine("задачу добавлена");
        }
        static void showTasks()
        {
            if(tasks.Count==0)
            {
                Console.WriteLine("нет заданий");
                return;
            }
            for(int i=0;i<tasks.Count;i++)
            {
                Console.Write($"{i + 1}. {tasks[i]}");
                TimeSpan remaining = deadlines[i]-DateTime.Now; ;
                double remainingHours=remaining.TotalHours;
                double totalTime = (deadlines[i] - DateTime.Now).TotalHours;
                if (remaining.TotalHours < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($" ПРОСРОЧЕНО! (-{(-remaining).Days}д {(-remaining).Hours}ч)");
                    Console.ResetColor();
                }
                else
                {
                    double totalHourse = (deadlines[i] - DateTime.Now).TotalHours;
                    if (deadlines[i] == DateTime.Now.Date)
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
                        Console.Write($"{remaining:hh\\:mm} (осталось)");
                        Console.ResetColor();
                    }
                    else
                    {
                        int daysLeft = remaining.Days;
                        int HourseLeft = remaining.Hours;
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
                            Console.Write($"{daysLeft}д {HourseLeft}ч (осталось)");
                        else
                            Console.Write($"{HourseLeft}ч (осталось)");

                        Console.ResetColor();
                    }

                }
            }
        }
        static void ShowOverdueTasks()
        {
            Console.WriteLine(" ПРОСРОЧЕННЫЕ ЗАДАЧИ");
            for (int i = 0; i < tasks.Count; i++)
            {
                if (deadlines[i] < DateTime.Now)
                {
                    Console.Write($"{i + 1}. {tasks[i]}");
                    TimeSpan overdue = DateTime.Now - deadlines[i];
                    Console.Write($" (просрочено на {overdue.Days}д {overdue.Hours}ч)");

                }
            }
        }
        static void DeleteTask()
        {
            Console.WriteLine("Удаление задач");
            if (tasks.Count == 0)
            {
                Console.WriteLine("нет задач для удаления");
                return;
            }
            showTasks();
            Console.Write("Введите номре задачи, который хотите удалить: ");
            int index = int.Parse(Console.ReadLine());
            if (index > 0 && index <= tasks.Count)
            {
                Console.WriteLine($"вы удалили задачу: {tasks[index - 1]}");
                tasks.RemoveAt(index - 1);
                deadlines.RemoveAt(index - 1);
                //save
            }
            else
            {
                Console.WriteLine("Неверный индекс");
                return;
            }

        }

    }
    
    }
