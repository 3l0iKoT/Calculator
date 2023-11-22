using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_of_the_5th_grader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int menuItem = 1;
            while (true)
            {
                Menu(menuItem);
                var key = Console.ReadKey();
                if (key.Key.ToString() != "UpArrow" && key.Key.ToString() != "DownArrow" && key.Key.ToString() != "Enter")
                {
                    continue;
                }
                if (key.Key.ToString() == "UpArrow")
                    menuItem = menuItem == 1 ? 6 : menuItem - 1;
                else if (key.Key.ToString() == "DownArrow")
                    menuItem = menuItem == 6 ? 1 : menuItem + 1;
                else if (key.Key.ToString() == "Enter")
                {
                    if (menuItem == 6)
                        break;
                }
            }
        }

        static void Menu(int menuItem)
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                                       ║");
            switch(menuItem)
            {
                case 1:
                    Console.WriteLine("║ >>> Перевод целых чисел из любой системы счисления в любую другую <<< ║");
                    Console.WriteLine("║             Перевод в римскую систему счисления и обратно             ║");
                    Console.WriteLine("║             Процесс сложения в разных системах счисления              ║");
                    Console.WriteLine("║             Процесс вычитания в разных системах счисления             ║");
                    Console.WriteLine("║             Процесс умножения в разных системах счисления             ║");
                    Console.WriteLine("║                          Выход из программы                           ║");
                    break;
                case 2:
                    Console.WriteLine("║     Перевод целых чисел из любой системы счисления в любую другую     ║");
                    Console.WriteLine("║         >>> Перевод в римскую систему счисления и обратно <<<         ║");
                    Console.WriteLine("║             Процесс сложения в разных системах счисления              ║");
                    Console.WriteLine("║             Процесс вычитания в разных системах счисления             ║");
                    Console.WriteLine("║             Процесс умножения в разных системах счисления             ║");
                    Console.WriteLine("║                          Выход из программы                           ║");
                    break;
                case 3:
                    Console.WriteLine("║     Перевод целых чисел из любой системы счисления в любую другую     ║");
                    Console.WriteLine("║             Перевод в римскую систему счисления и обратно             ║");
                    Console.WriteLine("║         >>> Процесс сложения в разных системах счисления <<<          ║");
                    Console.WriteLine("║             Процесс вычитания в разных системах счисления             ║");
                    Console.WriteLine("║             Процесс умножения в разных системах счисления             ║");
                    Console.WriteLine("║                          Выход из программы                           ║");
                    break;
                case 4:
                    Console.WriteLine("║     Перевод целых чисел из любой системы счисления в любую другую     ║");
                    Console.WriteLine("║             Перевод в римскую систему счисления и обратно             ║");
                    Console.WriteLine("║             Процесс сложения в разных системах счисления              ║");
                    Console.WriteLine("║         >>> Процесс вычитания в разных системах счисления <<<         ║");
                    Console.WriteLine("║             Процесс умножения в разных системах счисления             ║");
                    Console.WriteLine("║                          Выход из программы                           ║");
                    break;
                case 5:
                    Console.WriteLine("║     Перевод целых чисел из любой системы счисления в любую другую     ║");
                    Console.WriteLine("║             Перевод в римскую систему счисления и обратно             ║");
                    Console.WriteLine("║             Процесс сложения в разных системах счисления              ║");
                    Console.WriteLine("║             Процесс вычитания в разных системах счисления             ║");
                    Console.WriteLine("║         >>> Процесс умножения в разных системах счисления <<<         ║");
                    Console.WriteLine("║                          Выход из программы                           ║");
                    break;

                case 6:
                    Console.WriteLine("║     Перевод целых чисел из любой системы счисления в любую другую     ║");
                    Console.WriteLine("║             Перевод в римскую систему счисления и обратно             ║");
                    Console.WriteLine("║             Процесс сложения в разных системах счисления              ║");
                    Console.WriteLine("║             Процесс вычитания в разных системах счисления             ║");
                    Console.WriteLine("║             Процесс умножения в разных системах счисления             ║");
                    Console.WriteLine("║                      >>> Выход из программы <<<                       ║");
                    break;
            }
            Console.WriteLine("║                                                                       ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════╝");
        }
    }
}
