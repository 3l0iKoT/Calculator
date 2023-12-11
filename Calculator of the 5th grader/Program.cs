using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Numerics;
using System.Diagnostics.CodeAnalysis;
using System.Security;
using System.Runtime.CompilerServices;

namespace Calculator_of_the_5th_grader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int menuItem = 1;
            bool exitApp = false;
            while (true)
            {
                Menu(menuItem);
                switch (WaitKey(new[] { "UpArrow", "DownArrow", "Enter" }))
                {
                    case "UpArrow":
                        menuItem = menuItem == 1 ? 6 : menuItem - 1;
                        break;
                    case "DownArrow":
                        menuItem = menuItem == 6 ? 1 : menuItem + 1;
                        break;
                    case "Enter":
                        switch (menuItem)
                        {
                            case 1:
                                NumberSystem();
                                break;
                            case 2:
                                NumIntoRoman();
                                break;
                            case 3:
                                Addition();
                                break;
                            case 4:
                                Subtraction();
                                break;
                            case 5:
                                Multiplication();
                                break;
                            case 6:
                                exitApp = true;
                                break;
                        }
                        break;
                }
                if (exitApp) break;
            }
        }

        static List<char> Alphabet(int numberSystem)
        {
            List<char> alphabet = new List<char>();
            for (char i = '0'; i <= '9'; i++)
            {
                alphabet.Add(i);
            }
            for (char i = 'a'; i <= 'z'; i++)
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

        static string WaitKey(string[] keys)
        {
            string key;
            do
            {
                key = Console.ReadKey().Key.ToString();
            } while (!keys.Contains(key));
            return key;
        }

        static void NumberSystem()
        {
            bool exit = false;
            while (true)
            {
                bool restart = false;
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

                while (number[0] == '0')
                {
                    number = number.Substring(1);
                }

                Console.Clear();
                Console.WriteLine("Q - Вернутся в меню\nR - Начать заново\nEnter - Следующий шаг");
                Console.WriteLine($"\n{number}_{numberSystem1}");
                Console.WriteLine($"\nВаше число записано в СС с основанием {numberSystem1}");
                if (numberSystem1 != 10)
                    Console.WriteLine($"Для начала переведём число из {numberSystem1} СС в 10 СС");
                else
                    Console.WriteLine($"Переведём число из 10 СС в {numberSystem2} СС");
                string key = WaitKey(new[] { "Q", "R", "Enter" });
                if (key == "Q")
                    break;
                else if (key == "R")
                    continue;

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
                    key = WaitKey(new[] { "Q", "R", "Enter" });
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
                        key = WaitKey(new[] { "Q", "R", "Enter" });
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
                    key = WaitKey(new[] { "Q", "R", "Enter" });
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
                        key = WaitKey(new[] { "Q", "R" });
                        if (key == "Q")
                            break;
                        else if (key == "R")
                            continue;
                    }
                    Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                    Console.WriteLine($"\n{result10}_10\n");
                    Console.WriteLine($"Теперь переведём число из 10 СС в {numberSystem2} СС");
                    key = WaitKey(new[] { "Q", "R", "Enter" });
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
                key = WaitKey(new[] { "Q", "R", "Enter" });
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
                    intPart /= numberSystem2;
                    Console.WriteLine($"{result}");
                    Console.WriteLine($"\nОстаток получился {remainder}, значит приписываем к результату {alphabet2[remainder]}");
                    if (intPart == 0)
                        Console.WriteLine($"Остаток получился 0, значит ответом будет {result}");
                    key = WaitKey(new[] { "Q", "R", "Enter" });
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
                Console.WriteLine($"\n{number}_{numberSystem1} = {result}_{numberSystem2}\n");
                Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine("Q - Вернутся в меню\nR - Начать заново");
                key = WaitKey(new[] { "Q", "R" });
                if (key == "Q")
                    break;
            }
        }

        static void NumIntoRoman()
        {
            string key;
            while (true)
            {
                bool restart = false;
                bool exit = false;
                Console.Clear();
                Console.WriteLine("Введите целое число");
                int num;
                if (!int.TryParse(Console.ReadLine(), out num))
                {
                    Console.Clear();
                    Console.WriteLine("Число введено не корректно");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                }
                if (num <= 0 || num >= 5001)
                {
                    Console.Clear();
                    Console.WriteLine("Число должно быть в диапозоне от 1 до 5000");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                }

                Console.Clear();
                Console.WriteLine("Q - Вернутся в меню\nR - Начать заново\nEnter - Следующий шаг");
                Console.WriteLine($"\n{num}\n");
                Console.WriteLine("Дя перевода целого числа в римскую систему счисления используем алфавит:\n" +
                    "1 - I\n" +
                    "4 - IV\n" +
                    "5 - V\n" +
                    "9 - IX\n" +
                    "10 - X\n" +
                    "40 - XL\n" +
                    "50 - L\n" +
                    "90 - XC\n" +
                    "100 - C\n" +
                    "400 - CD\n" +
                    "500 - D\n" +
                    "900 - CM\n" +
                    "1000 - M\n" +
                    "4000 - _IV\n" +
                    "5000 - _V");


                List < (int Key, string Value)> romanNumerals = new List<(int, string)>
                {
                    (5000, "_V"),
                    (4000, "_IV"),
                    (1000, "M"),
                    (900, "CM"),
                    (500, "D"),
                    (400, "CD"),
                    (100, "C"),
                    (90, "XC"),
                    (50, "L"),
                    (40, "XL"),
                    (10, "X"),
                    (9, "IX"),
                    (5, "V"),
                    (4, "IV"),
                    (1, "I")
                };

                List<string> result = new List<string>();

                key = WaitKey(new[] { "Q", "R", "Enter" });
                if (key == "Q")
                    break;
                if (key == "R")
                    continue;

                foreach (var pair in romanNumerals)
                {
                    while (num >= pair.Key)
                    {
                        num -= pair.Key;
                        result.Add(pair.Value);
                        Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                        Console.WriteLine($"\n{num + pair.Key} - {pair.Key} = {num}\n{string.Join("", result)}\n");
                        Console.WriteLine($"Максимальное число из списка, которое меньше либо равно нашему числу {pair.Key} - {pair.Value} добавляем его в ответ и вычитаем из нашего числа\n");
                        key = WaitKey(new[] { "Q", "R", "Enter" });
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
                    if (exit || restart)
                        break;
                }
                if (exit)
                    break;
                if (restart)
                    continue;

                Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine($"\n{ string.Join("", result)}");
                Console.WriteLine("Это число является результатом перевода целого числа в римскую СС\n");
                Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");

                Console.WriteLine("Q - Вернутся в меню\nR - Начать заново");
                key = WaitKey(new[] { "Q", "R"});
                if (key == "Q")
                    break;
            }
        }

        static void Addition()
        {
            while (true)
            {
                bool exit = false;
                bool restart = false;
                int numberSystem;
                string key;
                Console.Clear();
                Console.WriteLine("Введите систему счисления для чисел в диапозоне от 2 до 50");
                if (!int.TryParse(Console.ReadLine(), out numberSystem))
                {
                    Console.Clear();
                    Console.WriteLine("Число введено не корректно");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }
                if (numberSystem < 2 || numberSystem > 50)
                {
                    Console.Clear();
                    Console.WriteLine($"Система счисления должна быть в диапозоне от 2 до 50. {numberSystem} не в диапозоне [2; 50]");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }
                List<char> alphabet = Alphabet(numberSystem);

                Console.WriteLine($"Введите первое число используя алфавит: ({ShowList(alphabet, ", ")})");
                string number1 = Console.ReadLine();
                if (string.IsNullOrEmpty(number1))
                {
                    Console.Clear();
                    Console.WriteLine("Введено пустое число");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }
                if (!CorrectNumber(number1, numberSystem))
                {
                    Console.Clear();
                    Console.WriteLine($"Число {number1} использует цифры не из алфавита\n({ShowList(alphabet, ", ")})");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }

                Console.WriteLine($"Введите второе число используя алфавит: ({ShowList(alphabet, ", ")})");
                string number2 = Console.ReadLine();
                if (string.IsNullOrEmpty(number2))
                {
                    Console.Clear();
                    Console.WriteLine("Введено пустое число");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }
                if (!CorrectNumber(number2, numberSystem))
                {
                    Console.Clear();
                    Console.WriteLine($"Число {number2} использует цифры не из алфавита\n({ShowList(alphabet, ", ")})");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }

                while (number1[0] == '0')
                    number1 = number1.Substring(1);
                while (number2[0] == '0')
                    number2 = number2.Substring(1);

                number1 = number1.PadLeft(number2.Length);
                number2 = number2.PadLeft(number1.Length);

                string transfers = new string(' ', number2.Length);
                string result = new string(' ', number2.Length + 1);

                Console.Clear();
                Console.WriteLine("Q - Вернутся в меню\nR - Начать заново\nEnter - Следующий шаг");
                Console.WriteLine($"\n  {number1}\n+ {number2}");
                Console.WriteLine("\nНайдём их сумму столбиком");

                key = WaitKey(new[] { "Q", "R", "Enter" });
                if (key == "Q")
                    break;
                if (key == "R")
                    continue;

                for (int i = number1.Length - 1; i >= 0; i--)
                {
                    (char sum, char transfer) = Sum(transfers[i], number1[i], number2[i], numberSystem);
                    if (i > 0)
                    {
                        transfers = ReplaceCharAtIndex(transfers, i - 1, transfer);
                        result = ReplaceCharAtIndex(result, i + 1, sum);
                    }
                    else
                    {
                        result = ReplaceCharAtIndex(result, i + 1, sum);
                        result = ReplaceCharAtIndex(result, i, transfer);
                    }
                    Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                    Console.WriteLine($"\n  {transfers}\n  {number1}\n+ {number2}");
                    Console.WriteLine("".PadLeft(number1.Length + 2, '-'));
                    Console.WriteLine($" {result}");
                    if (transfers[i] == ' ')
                    {
                        if (number1[i] == ' ' || number2[i] == ' ')
                            Console.WriteLine($"\nПеренесём в ответ {number2.Length - i}-ый разряд {(number2[i] == ' ' ? "первого" : "второго")} числа");
                        else
                            Console.WriteLine($"\nСуммируем {number2.Length - i}-ые разряды чисел. {number1[i]} + {number2[i]} = {transfer}{sum}");
                    }
                    else
                    {
                        if (number1[i] == ' ' || number2[i] == ' ')
                            Console.WriteLine($"\nСуммируем перенос и {number2.Length - i}-ый разряд {(number2[i] == ' ' ? "первого" : "второго")} числа. {transfers[i]} + {(number2[i] == ' ' ? number1[i] : number2[i])} = {transfer}{sum}");
                        else
                            Console.WriteLine($"\nСуммируем перенос и {number2.Length - i}-ые разряды чисел. {transfers[i]} + {number1[i]} + {number2[i]} = {transfer}{sum}");
                    }

                    if (transfer == '1')
                    {
                        if (i > 0)
                            Console.WriteLine("\nТак как сумма цифр двухзначная, то второй разряд суммы переносим над следующим разрядом числа");
                        else
                            Console.WriteLine("\nТак как сумма цифр двухзначная, а разряд последний, то второй разряд суммы переносим на следующй разряд ответа");
                    }

                    key = WaitKey(new[] { "Q", "R", "Enter" });
                    if (key == "Q")
                    {
                        exit = true;
                        break;
                    }
                    if (key == "R")
                    {
                        restart = true;
                        break;
                    }
                }
                if (exit)
                    break;
                if (restart)
                    continue;

                Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine($"\n{number1.Replace(" ","")} + {number2.Replace(" ","")} = {result.Replace(" ", "")}\n");
                Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine("\nQ - Вернутся в меню\nR - Начать заново");
                key = WaitKey(new[] { "Q", "R" });
                if (key == "Q")
                    break;
                Console.Clear();
            }
        }

        static void Subtraction()
        {
            while (true)
            {
                bool exit = false;
                bool restart = false;
                int numberSystem;
                string key;
                Console.Clear();
                Console.WriteLine("Введите систему счисления для чисел в диапозоне от 2 до 50");
                if (!int.TryParse(Console.ReadLine(), out numberSystem))
                {
                    Console.Clear();
                    Console.WriteLine("Число введено не корректно");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }
                if (numberSystem < 2 || numberSystem > 50)
                {
                    Console.Clear();
                    Console.WriteLine($"Система счисления должна быть в диапозоне от 2 до 50. {numberSystem} не в диапозоне [2; 50]");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }
                List<char> alphabet = Alphabet(numberSystem);

                Console.WriteLine($"Введите первое число используя алфавит: ({ShowList(alphabet, ", ")})");
                string number1 = Console.ReadLine();
                if (string.IsNullOrEmpty(number1))
                {
                    Console.Clear();
                    Console.WriteLine("Введено пустое число");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }
                if (!CorrectNumber(number1, numberSystem))
                {
                    Console.Clear();
                    Console.WriteLine($"Число {number1} использует цифры не из алфавита\n({ShowList(alphabet, ", ")})");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }

                Console.WriteLine($"Введите второе число используя алфавит: ({ShowList(alphabet, ", ")})");
                string number2 = Console.ReadLine();
                if (string.IsNullOrEmpty(number2))
                {
                    Console.Clear();
                    Console.WriteLine("Введено пустое число");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }
                if (!CorrectNumber(number2, numberSystem))
                {
                    Console.Clear();
                    Console.WriteLine($"Число {number2} использует цифры не из алфавита\n({ShowList(alphabet, ", ")})");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }

                while (number1[0] == '0')
                    number1 = number1.Substring(1);
                while (number2[0] == '0')
                    number2 = number2.Substring(1);

                if (!FirstNumberGreaterSecond(number1, number2, numberSystem))
                {
                    Console.Clear();
                    Console.WriteLine("Первое число должно быть больше второго");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }

                number1 = number1.PadLeft(number2.Length);
                number2 = number2.PadLeft(number1.Length);

                string result = new string(' ', number2.Length);
                string takeOut = new string(' ', number2.Length);

                Console.Clear();
                Console.WriteLine("Q - Вернутся в меню\nR - Начать заново\nEnter - Следующий шаг");
                Console.WriteLine($"\n  {number1}\n- {number2}");
                Console.WriteLine("\nНайдём их разность столбиком");

                key = WaitKey(new[] { "Q", "R", "Enter" });
                if (key == "Q")
                    break;
                if (key == "R")
                    continue;

                char sub;

                for (int i = number1.Length - 1; i >= 0; i--)
                {
                    (takeOut, sub) = Sub(takeOut, number1, number2[i], i, numberSystem);
                    result = ReplaceCharAtIndex(result, i, sub);

                    Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                    Console.WriteLine($"\n  {takeOut}\n  {number1}\n- {number2}");
                    Console.WriteLine("".PadLeft(number1.Length + 2, '-'));
                    Console.WriteLine($"  {result}\n");

                    if (number2[i] != ' ')
                    {
                        if (alphabet.IndexOf(number1[i]) > alphabet.IndexOf(number2[i]))
                        {
                            if (takeOut[i] == ' ')
                                Console.WriteLine($"Находим разность цифр {number1.Length - i}-го разряда чисел.\n{number1[i]} - {number2[i]} = {result[i]}");
                            else
                                Console.WriteLine($"Находим разность цифр {number1.Length - i}-го разряда чисел с учётом заимствования.\n({number1[i]} - 1) - {number2[i]} = {result[i]}");
                        }
                        else if (alphabet.IndexOf(number1[i]) == alphabet.IndexOf(number2[i]))
                        {
                            if (takeOut[i] == ' ')
                                Console.WriteLine($"Находим разность цифр {number1.Length - i}-го разряда чисел.\n{number1[i]} - {number2[i]} = {result[i]}");
                            else if (takeOut[i] == '*')
                            {
                                Console.WriteLine($"Находим разность цифр {number1.Length - i}-го разряда чисел с учётом заимствования, но так как цифра первого числа будеть меньше второго, то заимствуем 10 и следующего разряда.\n((10 + {number1[i]}) - 1) - {number2[i]} = {result[i]}");
                                if (takeOut[i - 1] == alphabet[numberSystem - 1])
                                    Console.WriteLine($"\nТак как цифра следующего разряда первого числа равна 0, то заимствуем 10 из следующего разряда, но оставляем над разрядам с 0-ом цифру {alphabet[numberSystem - 1]}. И повторяем так пока череда 0-ей подрят не закончится");
                            }
                            else
                                Console.WriteLine($"Находим разность цифр {number1.Length - i}-го разряда чисел учётом дополнения.\n({number1[i]} + {alphabet[numberSystem - 1]}) - {number2[i]} = {result[i]}");
                        }
                        else
                        {
                            if (takeOut[i] == ' ')
                            {
                                Console.WriteLine($"Находим разность цифр {number1.Length - i}-го разряда чисел, но так как цифра первого числа меньше второго, то заимствуем 10 и следующего разряда.\n(10 + {number1[i]}) - {number2[i]} = {result[i]}");
                                if (takeOut[i - 1] == alphabet[numberSystem - 1])
                                    Console.WriteLine($"\nТак как цифра следующего разряда первого числа равна 0, то заимствуем 10 из следующего разряда, но оставляем над разрядам с 0-ом цифру {alphabet[numberSystem - 1]}. И повторяем так пока череда 0-ей подрят не закончится");
                            }
                            else if (takeOut[i] == '*')
                            {
                                Console.WriteLine($"Находим разность цифр {number1.Length - i}-го разряда чисел с учётом заимствования, но так как цифра первого числа меньше второго, то заимствуем 10 и следующего разряда.\n((10 + {number1[i]}) - 1) - {number2[i]} = {result[i]}");
                                if (takeOut[i - 1] == alphabet[numberSystem - 1])
                                    Console.WriteLine($"\nТак как цифра следующего разряда первого числа равна 0, то заимствуем 10 из следующего разряда, но оставляем над разрядам с 0-ом цифру {alphabet[numberSystem - 1]}. И повторяем так пока череда 0-ей подрят не закончится");
                            }
                            else
                                Console.WriteLine($"Находим разность цифр {number1.Length - i}-го разряда чисел учётом дополнения.\n({number1[i]} + {alphabet[numberSystem - 1]}) - {number2[i]} = {result[i]}");
                        }
                    }
                    else
                    {
                        if (takeOut[i] == ' ')
                            Console.WriteLine($"Переписываем цифру из {number1.Length - i}-го разряда числа в ответ");
                        else if (takeOut[i] == '*')
                            Console.WriteLine($"Переписываем цифру из {number1.Length - i}-го разряда числа в ответ с учётом заимствования");
                        else
                            Console.WriteLine($"Переписываем цифру из {number1.Length - i}-го разряда числа в ответ с учётом дополнения");
                    }

                    key = WaitKey(new[] { "Q", "R", "Enter" });
                    if (key == "Q")
                    {
                        exit = true;
                        break;
                    }
                    if (key == "R")
                    {
                        restart = true;
                        break;
                    }
                }
                if (exit)
                    break;
                if (restart)
                    continue;

                while (result[0] == '0')
                {
                    result = result.Substring(1);
                }

                Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine($"\n{number1.Replace(" ", "")} - {number2.Replace(" ", "")} = {result.Replace(" ", "")}\n");
                Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine("\nQ - Вернутся в меню\nR - Начать заново");
                key = WaitKey(new[] { "Q", "R" });
                if (key == "Q")
                    break;
                Console.Clear();
            }
        }

        static void Multiplication()
        {
            while (true)
            {
                bool exit = false;
                bool restart = false;
                int numberSystem;
                string key;
                Console.Clear();
                Console.WriteLine("Введите систему счисления для чисел в диапозоне от 2 до 50");
                if (!int.TryParse(Console.ReadLine(), out numberSystem))
                {
                    Console.Clear();
                    Console.WriteLine("Число введено не корректно");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }
                if (numberSystem < 2 || numberSystem > 50)
                {
                    Console.Clear();
                    Console.WriteLine($"Система счисления должна быть в диапозоне от 2 до 50. {numberSystem} не в диапозоне [2; 50]");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }
                List<char> alphabet = Alphabet(numberSystem);

                Console.WriteLine($"Введите первое число используя алфавит: ({ShowList(alphabet, ", ")})");
                string number1 = Console.ReadLine();
                if (string.IsNullOrEmpty(number1))
                {
                    Console.Clear();
                    Console.WriteLine("Введено пустое число");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }
                if (!CorrectNumber(number1, numberSystem))
                {
                    Console.Clear();
                    Console.WriteLine($"Число {number1} использует цифры не из алфавита\n({ShowList(alphabet, ", ")})");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }

                Console.WriteLine($"Введите второе число используя алфавит: ({ShowList(alphabet, ", ")})");
                string number2 = Console.ReadLine();
                if (string.IsNullOrEmpty(number2))
                {
                    Console.Clear();
                    Console.WriteLine("Введено пустое число");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }
                if (!CorrectNumber(number2, numberSystem))
                {
                    Console.Clear();
                    Console.WriteLine($"Число {number2} использует цифры не из алфавита\n({ShowList(alphabet, ", ")})");
                    Console.WriteLine("\nНажмите на любую клавишу, что бы начать заново");
                    Console.ReadKey();
                    continue;
                }

                while (number1[0] == '0')
                    number1 = number1.Substring(1);
                while (number2[0] == '0')
                    number2 = number2.Substring(1);

                if (number1.Length < number2.Length)
                    (number1, number2) = (number2, number1);

                number2 = number2.PadLeft(number1.Length);

                Console.Clear();
                Console.WriteLine("Q - Вернутся в меню\nR - Начать заново\nEnter - Следующий шаг");
                Console.WriteLine($"\n  {number1}\n* {number2}");
                Console.WriteLine("\nНайдём их произведение столбиком");

                string[] preResult = new string[number2.Replace(" ", "").Length];
                string transfers;
                string result = "";

                for (int i = 0; i < preResult.Length; i++)
                    preResult[i] = "";

                key = WaitKey(new[] { "Q", "R", "Enter" });
                if (key == "Q")
                    break;
                if (key == "R")
                    continue;

                string multi;
                string indent = "";
                int maxLenght = number1.Length + 2;

                for (int i = number2.Replace(" ", "").Length - 1; i >= 0; i--)
                {
                    transfers = new string(' ', number1.Length);
                    for (int j = number1.Length - 1; j >= 0; j--)
                    {
                        multi = Multi(transfers[j], number1[j], number2.Replace(" ", "")[i], j, numberSystem);
                        if (j > 0)
                        {
                            transfers = ReplaceCharAtIndex(transfers, j - 1, multi[0]);
                            preResult[i] = $"{multi[1]}{preResult[i]}";
                        }
                        else
                            preResult[i] = $"{multi.Replace(" ", "")}{preResult[i]}";

                        preResult[i] = preResult[i].PadRight(number2.Replace(" ", "").Length - i);

                        foreach (string preRes in preResult)
                            if (preRes.Length > maxLenght)
                                maxLenght = preRes.Length;

                        indent = "".PadLeft(maxLenght - number1.Length - 2);

                        Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                        Console.WriteLine($"\n{indent}  {transfers}\n{indent}  {number1}\n{indent}* {number2}");
                        Console.WriteLine("".PadLeft(maxLenght, '-'));
                        for (int k = number2.Replace(" ", "").Length - 1; k >= 0; k--)
                            if (preResult[k] != "")
                                Console.WriteLine($"{preResult[k].PadLeft(maxLenght)}");

                        if (transfers[j] != ' ')
                            Console.WriteLine($"\nУмнажаем цифру первого числа под {number1.Length - j}-ым разрядом и цифру второго числа под {number2.Replace(" ", "").Length - i}-разрядом и прибавляем к умножению перенос над {number1.Length - j}-ым разрядом {number1[j]} * {number2.Replace(" ", "")[i]} + {transfers[j]} = {multi}");
                        else
                            Console.WriteLine($"\nУмнажаем цифру первого числа под {number1.Length - j}-ым разрядом и цифру второго числа под {number2.Replace(" ", "").Length - i}-разрядом {number1[j]} * {number2.Replace(" ", "")[i]} = {multi}");
                        if (multi.Length == 2)
                            Console.WriteLine("\nТак как при умножении получилось двухзначное число, то десятки переносим над следующим разрядом");

                        key = WaitKey(new[] { "Q", "R", "Enter" });
                        if (key == "Q")
                        {
                            exit = true;
                            break;
                        }
                        if (key == "R")
                        {
                            restart = true;
                            break;
                        }
                    }
                    if (exit || restart)
                        break;

                }
                if (exit)
                    break;
                if (restart)
                    continue;

                for (int i = number2.Replace(" ", "").Length - 1; i >= 0; i--)
                    preResult[i] = preResult[i].PadLeft(maxLenght);

                Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════\n");

                if (preResult.Length > 1)
                {
                    for (int i = number2.Replace(" ", "").Length - 1; i >= 0; i--)
                        Console.WriteLine($"{(i == number2.Replace(" ", "").Length - 1 ? "  " : "+ ")}{preResult[i]}");
                    Console.WriteLine("\nТеперь необходимо сложить эти числа столбиком. Там где чисел нет представляем, что таv нули");
                    for (int i = preResult.Length - 1; i >= 0; i--)
                        preResult[i] = preResult[i].Replace(" ", "0");
                }
                else
                {
                    result = preResult[0].Replace(" ", "");
                    Console.WriteLine(result);
                    Console.WriteLine("\nЭто число и является ответом");
                }

                key = WaitKey(new[] { "Q", "R", "Enter" });
                if (key == "Q")
                    break;
                if (key == "R")
                    continue;

                if (preResult.Length > 1)
                {
                    indent = "";
                    maxLenght = preResult[preResult.Length - 1].Length + 2;

                    string transfersSum = new string(' ', preResult[preResult.Length - 1].Length);
                    for (int i = preResult[preResult.Length - 1].Length - 1; i >= 0; i--)
                    {
                        int doTransfer = 0;
                        int indexSum = 0;
                        string sum = "";
                        if (transfersSum[i] != ' ')
                            indexSum += alphabet.IndexOf(transfersSum[i]);
                        for (int j = preResult.Length - 1; j >= 0; j--)
                            if (preResult[j][i] != ' ')
                                indexSum += alphabet.IndexOf(preResult[j][i]);
                        while (indexSum > 0)
                        {
                            sum = $"{alphabet[indexSum % numberSystem]}{sum}";
                            indexSum /= numberSystem;
                        }

                        if (i > 0)
                        {
                            if (sum.Length > 0)
                                result = $"{sum[sum.Length - 1]}{result}";
                            else
                                result = "0";

                            if (sum.Length > 2)
                            {
                                doTransfer = 2;
                                for (int j = sum.Length - 2; j >= 0; j--)
                                {
                                    transfersSum = ReplaceCharAtIndex(transfersSum, i - (sum.Length - 1 - j), sum[j]);
                                }
                            }
                            else if (sum.Length == 2)
                            {
                                doTransfer = 1;
                                transfersSum = ReplaceCharAtIndex(transfersSum, i - 1, sum[0]);
                            }
                        }
                        else
                        {
                            result = $"{sum}{result}";
                        }

                        if (result.Length > maxLenght)
                            maxLenght = result.Length;

                        indent = "".PadLeft(maxLenght - preResult[preResult.Length - 1].Length - 2, ' ');

                        Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════\n");
                        Console.WriteLine($"{indent}  {transfersSum}");
                        for (int k = number2.Replace(" ", "").Length - 1; k >= 0; k--)
                            Console.WriteLine($"{indent}{(k == number2.Replace(" ", "").Length - 1 ? "  " : "+ ")}{preResult[k]}");
                        Console.WriteLine("".PadLeft(maxLenght, '-'));
                        if (result.Length <= preResult[preResult.Length - 1].Length)
                            Console.WriteLine($"{indent}  {result.PadLeft(preResult[preResult.Length - 1].Length)}\n");
                        else
                            Console.WriteLine($"{(maxLenght > result.Length ? " " : "")}{result}");

                        if (transfersSum[i] == ' ')
                            Console.WriteLine($"Сложим у всех чисел цифры {preResult[preResult.Length - 1].Length - i}-го разряда\n");
                        else
                            Console.WriteLine($"Сложим у всех чисел цифры {preResult[preResult.Length - 1].Length - i}-го разряда с учётом переноса\n");

                        if (doTransfer == 2)
                        {
                            Console.WriteLine("Сумма получилась больше 100! Перенесем десятки на следующий разряд, сотки на следующий после десяток и так далее");
                        }
                        else if (doTransfer == 1)
                        {
                            Console.WriteLine("Сумма получилась больше 10, перенесём десятки на следующий разряд");
                        }

                        key = WaitKey(new[] { "Q", "R", "Enter" });
                        if (key == "Q")
                        {
                            exit = true;
                            break;
                        }
                        if (key == "R")
                        {
                            restart = true;
                            break;
                        }
                    }
                    if (exit)
                        break;
                    if (restart)
                        continue;
                }

                Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════\n");
                Console.WriteLine($"{number1} * {number2.Replace(" ", "")} = {result}\n");
                Console.WriteLine("══════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine("\nQ - Вернутся в меню\nR - Начать заново");
                key = WaitKey(new[] { "Q", "R" });
                if (key == "Q")
                    break;
                Console.Clear();
            }
        }

        static (char, char) Sum(char transferIn, char number1, char number2, int numberSystem)
        {
            char sum;
            char transferOut = ' ';
            List<char> alphabet = Alphabet(numberSystem);
            int indexSum;

            if (transferIn == ' ')
            {
                if (number1 == ' ')
                    sum = number2;
                else if (number2 == ' ')
                    sum = number1;
                else
                {
                    indexSum = alphabet.IndexOf(number1) + alphabet.IndexOf(number2);
                    transferOut = indexSum >= numberSystem ? '1' : ' ';
                    sum = alphabet[indexSum % numberSystem];
                }
            }
            else
            {
                if (number1 == ' ' && number2 == ' ')
                    sum = transferOut;
                if (number1 == ' ')
                {
                    indexSum = 1 + alphabet.IndexOf(number2);
                    transferOut = indexSum >= numberSystem ? '1' : ' ';
                    sum = alphabet[indexSum % numberSystem];
                }
                if (number2 == ' ')
                {
                    indexSum = 1 + alphabet.IndexOf(number1);
                    transferOut = indexSum >= numberSystem ? '1' : ' ';
                    sum = alphabet[indexSum % numberSystem];
                }
                else
                {
                    indexSum = 1 + alphabet.IndexOf(number1) + alphabet.IndexOf(number2);
                    transferOut = indexSum >= numberSystem ? '1' : ' ';
                    sum = alphabet[indexSum % numberSystem];
                }
            }

            return (sum, transferOut);
        }

        static (string, char) Sub(string takeOut, string number1, char charNumber2, int index, int numberSystem)
        {
            var alphabet = Alphabet(numberSystem);
            char sub;
            int takeOutIndex;
            if (charNumber2 != ' ')
            {
                if (alphabet.IndexOf(number1[index]) > alphabet.IndexOf(charNumber2))
                {
                    if (takeOut[index] == ' ')
                        sub = alphabet[alphabet.IndexOf(number1[index]) - alphabet.IndexOf(charNumber2)];
                    else
                        sub = alphabet[(alphabet.IndexOf(number1[index]) - 1) - alphabet.IndexOf(charNumber2)];
                }
                else if (alphabet.IndexOf(number1[index]) == alphabet.IndexOf(charNumber2))
                {
                    if (takeOut[index] == ' ')
                        sub = '0';
                    else if (takeOut[index] == '*')
                    {
                        sub = alphabet[(alphabet.IndexOf(number1[index]) - 1 + numberSystem) - alphabet.IndexOf(charNumber2)];

                        takeOutIndex = index - 1;

                        do
                        {
                            if (number1[takeOutIndex] == '0')
                                takeOut = ReplaceCharAtIndex(takeOut, takeOutIndex, alphabet[numberSystem - 1]);
                            else
                            {
                                takeOut = ReplaceCharAtIndex(takeOut, takeOutIndex, '*');
                                break;
                            }

                            if (takeOutIndex > 0)
                                takeOutIndex--;
                        } while (number1[takeOutIndex] == '0');
                        if (takeOut[takeOutIndex + 1] == alphabet[numberSystem - 1])
                            takeOut = ReplaceCharAtIndex(takeOut, takeOutIndex, '*');
                    }
                    else
                        sub = alphabet[numberSystem - 1];
                }
                else
                {
                    if (takeOut[index] == ' ')
                        sub = alphabet[(alphabet.IndexOf(number1[index])+ numberSystem) - alphabet.IndexOf(charNumber2)];
                    else if (takeOut[index] == '*')
                        sub = alphabet[(alphabet.IndexOf(number1[index]) - 1 + numberSystem) - alphabet.IndexOf(charNumber2)];
                    else
                        sub = alphabet[(numberSystem - 1) - alphabet.IndexOf(charNumber2)];

                    if (takeOut[index] == ' ' || takeOut[index] == '*')
                    {
                        takeOutIndex = index - 1;

                        do
                        {
                            if (number1[takeOutIndex] == '0')
                                takeOut = ReplaceCharAtIndex(takeOut, takeOutIndex, alphabet[numberSystem - 1]);
                            else
                            {
                                takeOut = ReplaceCharAtIndex(takeOut, takeOutIndex, '*');
                                break;
                            }

                            if (takeOutIndex > 0)
                                takeOutIndex--;
                        } while (number1[takeOutIndex] == '0');
                        if (takeOut[takeOutIndex + 1] == alphabet[numberSystem - 1])
                            takeOut = ReplaceCharAtIndex(takeOut, takeOutIndex, '*');
                    }
                }
            }
            else
            {
                if (takeOut[index] == ' ')
                    sub = number1[index];
                else if (takeOut[index] == '*')
                    sub = alphabet[alphabet.IndexOf(number1[index]) - 1];
                else
                    sub = alphabet[numberSystem - 1];
            }
            return (takeOut, sub);
        }

        static string Multi(char transferIn, char charNumber1, char charNumber2, int j, int numberSystem)
        {
            var alphabet = Alphabet(numberSystem);
            int indexMulti = alphabet.IndexOf(charNumber1) * alphabet.IndexOf(charNumber2) + (transferIn != ' ' ? alphabet.IndexOf(transferIn) : 0);
            string multi = (indexMulti / numberSystem > 0 ? alphabet[indexMulti / numberSystem].ToString() : " ") + alphabet[indexMulti % numberSystem].ToString();
            return multi;
        }

        static bool FirstNumberGreaterSecond(string number1, string number2, int numberSystem)
        {
            var alphabet = Alphabet(numberSystem);
            bool greater = false;
            if (number1.Length > number2.Length)
                return true;
            else if (number1.Length == number2.Length)
            {
                for(int i = 0; i < number1.Length; i++)
                {
                    if (alphabet.IndexOf(number1[i]) == alphabet.IndexOf(number2[i]))
                        continue;

                    if (alphabet.IndexOf(number1[i]) > alphabet.IndexOf(number2[i]))
                    {
                        greater = true;
                        break;
                    }
                    else
                        break;
                }
            }
            else
                return false;
            return greater;
        }

        static string ReplaceCharAtIndex(string original, int index, char newChar)
        {
            char[] chars = original.ToCharArray();

            chars[index] = newChar;

            return new string(chars);
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
