using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Numerics;

namespace Calculator_of_the_5th_grader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int menuItem = 1;
            string key = "";
            while (true)
            {
                Menu(menuItem);
                key = WaitKey(key, new[] { "UpArrow", "DownArrow", "Enter" });
                if (key == "UpArrow")
                    menuItem = menuItem == 1 ? 6 : menuItem - 1;
                else if (key == "DownArrow")
                    menuItem = menuItem == 6 ? 1 : menuItem + 1;
                else if (key == "Enter")
                {
                    if (menuItem == 1)
                        NumberSystem();
                    if (menuItem == 6)
                        break;
                }
            }
        }

        static List<char> Alphabet(int numberSystem)
        {
            List<char> alphabet = new List<char>();
            for(char i = '0'; i <= '9'; i++)
            {
                alphabet.Add(i);
            }
            for(char i = 'a'; i <= 'z'; i++)
            {
                alphabet.Add(i);
            }
            for (char i = 'A'; i <= 'N'; i++)
            {
                alphabet.Add(i);
            }
            return alphabet.GetRange(0, numberSystem);
        }

        static bool CorrectNumber(string number, int numberSystem)
        {
            int correctNumber = 0;
            var alph = Alphabet(numberSystem);
            foreach (char c in alph)
            {
                foreach (char c2 in number)
                {
                    if (c == c2)
                    {
                        correctNumber++;
                    }
                }
            }
            return correctNumber == number.Length;
        }

        static string WaitKey(string key, string[] keys)
        {
            do
            {
                key = Console.ReadKey().Key.ToString();
            } while (!keys.Contains(key));
            return key;
        }

        static void NumberSystem()
        {
            bool exit = false;
            bool restart = false;
            string key = "";
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Введите систему счисления для числа в диапозоне от 2 до 50");
                if (!int.TryParse(Console.ReadLine(), out int numberSystem1))
                {
                    Console.Clear();
                    Console.WriteLine("Число введено не корректно");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }
                if (numberSystem1 < 2 || numberSystem1 > 50)
                {
                    Console.Clear();
                    Console.WriteLine($"Система счисления должна быть в диапозоне от 2 до 50. {numberSystem1} не в диапозоне [2; 50]");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }
                var alphabet1 = Alphabet(numberSystem1);

                Console.WriteLine("Введите систему счисления для результата в диапозоне от 2 до 50");
                if (!int.TryParse(Console.ReadLine(), out int numberSystem2))
                {
                    Console.Clear();
                    Console.WriteLine("Число введено не корректно");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }
                if (numberSystem2 < 2 || numberSystem2 > 50)
                {
                    Console.Clear();
                    Console.WriteLine($"Система счисления должна быть в диапозоне от 2 до 50. {numberSystem2} не в диапозоне [2; 50]");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }
                if (numberSystem1 == numberSystem2)
                {
                    Console.Clear();
                    Console.WriteLine($"Системы счисления должны быть разными");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }
                var alphabet2 = Alphabet(numberSystem2);

                Console.WriteLine($"Введите число используя алфавит: ({ShowList(alphabet1, ", ")})");
                string number = Console.ReadLine();
                if (string.IsNullOrEmpty(number))
                {
                    Console.Clear();
                    Console.WriteLine("Введено пустое число");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }
                if (!CorrectNumber(number, numberSystem1))
                {
                    Console.Clear();
                    Console.WriteLine($"Число {number} использует цифры не из алфавита\n({ShowList(alphabet1, ", ")})");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }
                
                Console.Clear();
                Console.WriteLine("Q - Вернутся в меню\nR - Начать заново\nEnter - Следующий шаг");
                Console.WriteLine($"\n{number}_{numberSystem1}");
                Console.WriteLine($"\nВаше число записано в СС с основанием {numberSystem1}");
                if (numberSystem1 != 10)
                    Console.WriteLine($"Для начала переведём число из {numberSystem1} СС в 10 СС");
                else
                    Console.WriteLine($"Переведём число из 10 СС в {numberSystem2}");
                key = WaitKey(key, new[] {"Q", "R", "Enter"});
                if (key == "Q")
                    break;
                else if (key == "R")
                    continue;
                
                if (number[0] == '0')
                {
                    int cut = 0;
                    foreach (char c in number)
                    {
                        if (c == '0') cut++;
                        else break;
                    }
                    number = number.Substring(cut);
                    Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                    Console.WriteLine($"\n{number}");
                    Console.WriteLine($"\nИзбавимся от 0 в начале");
                    key = WaitKey(key, new[] { "Q", "R", "Enter" });
                    if (key == "Q")
                        break;
                    else if (key == "R")
                        continue;
                }

                string solution = "";
                BigInteger result10 = 0;

                if (numberSystem1 != 10)
                {
                    List<string> digitNumber = new List<string>();
                    for (int i = number.Length - 1; i >= 0; i--)
                    {
                        digitNumber.Add(i.ToString());
                    }
                    Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                    Console.WriteLine($"\n{ShowList(digitNumber, " ")}\n{number}");
                    Console.WriteLine($"\nНапишем над числом разряды цифр числа следующим образом");
                    Console.WriteLine($"Следующим шагом нам необходимо найти сумму произведений цифр числа на его основание в степени разряда.\r\nЕсли какая-то цифра числа будет например 'a' то думаем, какое число оно должно заменить, так как в нашем алфавите ({ShowList(Alphabet(50), ", ")}) 'a' идёт после 9, то 'a' будет 10.\r\nb - 11, A - 36, N - 49");
                    key = WaitKey(key, new[] { "Q", "R", "Enter" });
                    if (key == "Q")
                        break;
                    else if (key == "R")
                        continue;

                    for (int i = 0; i < number.Length; i++)
                    {
                        solution += $"({alphabet1.IndexOf(number[i])} * {numberSystem1}^{digitNumber[i]})";
                        result10 += (BigInteger)alphabet1.IndexOf(number[i]) * (BigInteger)Math.Pow(numberSystem1, double.Parse(digitNumber[i]));
                        Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                        Console.WriteLine($"\n{ShowList(digitNumber, " ")}\n{number}_{numberSystem1} = {solution}");
                        Console.WriteLine($"\nДобавляем произведение цифры под {digitNumber[i]} разрядом числа на его основание в степени разряда этой цифры");
                        solution += " + ";
                        key = WaitKey(key, new[] { "Q", "R", "Enter" });
                        if (key == "Q")
                        {
                            exit = true;
                            break;
                        }
                        else if (key == "R")
                        {
                            restart = true;
                            break;
                        }
                    }
                    if (exit)
                        break;
                    else if (restart)
                        continue;

                    Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                    Console.WriteLine($"\n{ShowList(digitNumber, " ")}\n{number}_{numberSystem1} = {solution.Substring(0, solution.Length - 3)} = {result10}_10");
                    Console.WriteLine($"\nРезультатом вычисления этого выражения будет переводом числа {number} из {numberSystem1} СС в 10 СС");
                    key = WaitKey(key, new[] { "Q", "R", "Enter" });
                    if (key == "Q")
                        break;
                    else if (key == "R")
                        continue;
                    if (numberSystem2 == 10)
                    {
                        Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                        Console.WriteLine($"\n{number}_{numberSystem1} = {result10}_10");
                        Console.WriteLine($"\nТак как нам нужно было перевести только в 10 СС, то это и является ответом");
                        Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                        Console.WriteLine("Q - Вернутся в меню\nR - Начать заново");
                        key = WaitKey(key, new[] { "Q", "R" });
                        if (key == "Q")
                            break;
                        else if (key == "R")
                            continue;
                    }
                    Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                    Console.WriteLine($"\n{result10}_10\n");
                    Console.WriteLine($"Теперь переведём число из 10 СС в {numberSystem2} СС");
                    key = WaitKey(key, new[] { "Q", "R", "Enter" });
                    if (key == "Q")
                        break;
                    else if (key == "R")
                        continue;
                }
                else
                {
                    result10 = BigInteger.Parse(number);
                }
                int remainder = (int)(result10 % numberSystem2);
                string result = alphabet2[remainder].ToString();
                BigInteger intPart = result10 / numberSystem2;
                Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine($"\n{result10} % {numberSystem2} = {remainder} - Остаток от деления");
                Console.WriteLine($"{result10} / {numberSystem2} = {intPart} - Целая часть от деления");
                Console.WriteLine($"{result} - Результат");
                Console.WriteLine($"\nНайдём остаток и целую часть от деления на основание СС, на которую нам нужно перевести число.\r\nОстаток сохраняем, дописывая в начало числа, показывающего результат.\r\nОстаток получился {remainder}, значит приписываем к результату {alphabet2[remainder]}");
                Console.WriteLine($"По нашему алфавиту ({ShowList(Alphabet(50), ", ")}) такие числа как 10 или 11 могут быть записаны символами 'a' или 'b'.\r\n10 - a, 11 - b, 36 - A, 49 - N");
                key = WaitKey(key, new[] { "Q", "R", "Enter" });
                if (key == "Q")
                    break;
                else if (key == "R")
                    continue;
                while (intPart != 0)
                {
                    remainder = (int)(intPart % numberSystem2);
                    Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                    Console.WriteLine($"\n{intPart} % {numberSystem2} = {remainder}");
                    result = alphabet2[remainder].ToString() + result;
                    Console.WriteLine($"{intPart} / {numberSystem2} = {intPart / numberSystem2}");
                    intPart = intPart / numberSystem2;
                    Console.WriteLine($"{result}");
                    Console.WriteLine($"\nОстаток получился {remainder}, значит приписываем к результату {alphabet2[remainder]}");
                    if (intPart == 0)
                        Console.WriteLine($"Остаток получился 0, значит ответом будет {result}");
                    key = WaitKey(key, new[] { "Q", "R", "Enter" });
                    if (key == "Q")
                    {
                        exit = true;
                        break;
                    }
                    else if (key == "R")
                    {
                        restart = true;
                        break;
                    }
                }
                if (exit) break;
                else if (restart) continue;
                Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine($"\n{number}_{numberSystem1} = {result}_{numberSystem2}");
                Console.WriteLine($"\nЭто число является результатом перевода числа {number} из {numberSystem1} СС в {numberSystem2} СС");
                Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine("Q - Вернутся в меню\nR - Начать заново");
                key = WaitKey(key, new[] { "Q", "R" });
                if (key == "Q")
                    break;
            }
        }

        static string ShowList<T>(List<T> list, string split)
        {
            string strList = "";
            foreach (T item in list)
            {
                strList += $"{item}{split}";
            }
            strList = strList.Substring(0, strList.Length - split.Length);
            return strList;
        }

        static void Menu(int menuItem)
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                                       ║");
            (string left, string right) = menuItem == 1 ? (">>>", "<<<") : ("   ", "   ");
            Console.WriteLine($"║ {left} Перевод целых чисел из любой системы счисления в любую другую {right} ║");
            (left, right) = menuItem == 2 ? (">>>", "<<<") : ("   ", "   ");
            Console.WriteLine($"║         {left} Перевод в римскую систему счисления и обратно {right}         ║");
            (left, right) = menuItem == 3 ? (">>>", "<<<") : ("   ", "   ");
            Console.WriteLine($"║         {left} Процесс сложения в разных системах счисления {right}          ║");
            (left, right) = menuItem == 4 ? (">>>", "<<<") : ("   ", "   ");
            Console.WriteLine($"║         {left} Процесс вычитания в разных системах счисления {right}         ║");
            (left, right) = menuItem == 5 ? (">>>", "<<<") : ("   ", "   ");
            Console.WriteLine($"║         {left} Процесс умножения в разных системах счисления {right}         ║");
            (left, right) = menuItem == 6 ? (">>>", "<<<") : ("   ", "   ");
            Console.WriteLine($"║                      {left} Выход из программы {right}                       ║");
            Console.WriteLine($"║                                                                       ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════╝");
        }
    }
}
