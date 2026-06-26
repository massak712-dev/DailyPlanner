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
                        //AddTask();
                    }
                    else if (choice == "2")
                    {
                        //ShowTasks();
                    }
                    else if (choice == "3")
                    {
                        //ShowOverdueTasks();
                    }
                    else if (choice == "4")
                    {
                        //DeleteTask();
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

        }
    }
