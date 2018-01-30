using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HH
{
    class Program
    {


        static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }
        static async Task MainAsync()
        {
            ConsoleKey pressKey;
            do
            {
                Connect client = new Connect();
                CollectStatistic collectStat = new CollectStatistic
                {
                    client = client.CreateConnect(false)
                };
                Console.WriteLine("Это консольное приложения для работы с web-api HeadHunter.");
                Console.WriteLine("Что делаем:");
                Console.WriteLine("1) Выводим названия профессий и ключевых навыков в вакансиях,\n" +
                                  "   объявленная зарплата которых привышает либо равна 120000 руб.");
                Console.WriteLine("2) Выводим названия профессий и ключевых навыков в вакансиях,\n" +
                                  "   объявленная зарплата которых менее либо равна 15000 руб.");
                Console.WriteLine("3) Задай свои параметры и получи результат.");
                Console.WriteLine("4) Выход");
                pressKey = Console.ReadKey(true).Key;

                if (pressKey == ConsoleKey.D1)
                {
                    collectStat.Salary_from = 120000;
                    collectStat.Salary_to = -1;
                    Console.Clear();
                    Console.SetCursorPosition(0, Console.CursorTop + 2);
                    collectStat.Start();
                    do
                    {
                        await Task.Delay(200).ConfigureAwait(false);
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        collectStat.PrintLoad();
                        Console.SetCursorPosition(0, Console.CursorTop + 1);
                        collectStat.PrintStatus();
                    }
                    while (!collectStat.Finished);
                    Console.Clear();
                    collectStat.PrintResultProfession(10);
                    Console.ReadKey();
                    Console.Clear();
                    collectStat.PrintResultSkills(10);
                    Console.ReadKey();
                    Console.Clear();
                }

                if (pressKey == ConsoleKey.D2)
                {
                    collectStat.Salary_from = -1;
                    collectStat.Salary_to = 15000;
                    Console.Clear();
                    Console.SetCursorPosition(0, Console.CursorTop + 2);
                    collectStat.Start();
                    do
                    {
                        await Task.Delay(200).ConfigureAwait(false);
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        collectStat.PrintLoad();
                        Console.SetCursorPosition(0, Console.CursorTop + 1);
                        collectStat.PrintStatus();
                    }
                    while (!collectStat.Finished);
                    Console.Clear();
                    collectStat.PrintResultProfession(10);
                    Console.ReadKey();
                    Console.Clear();
                    collectStat.PrintResultSkills(10);
                    Console.ReadKey();
                    Console.Clear();
                }

                if (pressKey == ConsoleKey.D3)
                {
                    bool flag = false;
                    do
                    {
                        Console.Write("Нижняя ценовая планка(-1 - отсутствует): ");
                        string x = Console.ReadLine();
                        if (Int32.TryParse(x, out int test))
                        {
                            collectStat.Salary_from = test;
                            flag = true;
                        }
                        else flag = false;
                    } while (!flag);

                    do
                    {
                        Console.Write("Верхняя ценовая планка(-1 - отсутствует): ");
                        string x = Console.ReadLine();
                        if (Int32.TryParse(x, out int test))
                        {
                            collectStat.Salary_to = test;
                            flag = true;
                        }
                        else flag = false;
                    } while (!flag);
                    int elements = 0;
                    do
                    {
                        Console.Write("Сколько элементов вывести: ");
                        string x = Console.ReadLine();
                        if (Int32.TryParse(x, out int test))
                        {
                            elements = test > 0 ? test : 0;
                            flag = test > 0;
                        }
                        else flag = false;
                    } while (!flag);

                    Console.Clear();
                    Console.SetCursorPosition(0, Console.CursorTop + 2);
                    collectStat.Start();
                    do
                    {
                        await Task.Delay(200).ConfigureAwait(false);
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        collectStat.PrintLoad();
                        Console.SetCursorPosition(0, Console.CursorTop + 1);
                        collectStat.PrintStatus();
                    }
                    while (!collectStat.Finished);
                    Console.Clear();
                    collectStat.PrintResultProfession(elements);
                    Console.ReadKey();
                    Console.Clear();
                    collectStat.PrintResultSkills(elements);
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            while (pressKey != ConsoleKey.D4);
            Console.WriteLine("Спасибо за использование программы. \n Press any key...");
            Console.ReadKey();
        }
    }
}