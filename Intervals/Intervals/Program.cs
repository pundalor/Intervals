using System;
using System.Collections.Generic;
using System.Linq;

public class SummaryRanges
{
    private List<int> nums; // Список для хранения чисел

    public SummaryRanges()
    {
        nums = new List<int>();
    }

    // Добавляем число в список
    public void AddNum(int value)
    {
        nums.Add(value);
    }

    // Возвращаем интервалы
    public int[][] GetIntervals()
    {
        if (nums.Count == 0) return new int[0][]; // Если нет чисел

        var sortedNums = nums.Distinct().OrderBy(x => x).ToList(); // Убираем дубликаты и сортируем

        List<int[]> intervals = new List<int[]>(); // Список интервалов

        foreach (var num in sortedNums)
        {
            if (intervals.Count == 0 || intervals.Last()[1] < num - 1)
            {
                // Новый интервал
                intervals.Add(new int[] { num, num });
            }
            else
            {
                // Добавляем число в текущий интервал
                intervals.Last()[1] = num;
            }
        }

        return intervals.ToArray(); // Возвращаем итоговые интервалы
    }
}

class Program
{
    static void Main(string[] args)
    {
        var sr = new SummaryRanges();
        var result = new List<object>();

        // Инструкция
        Console.WriteLine("Команды для ввода:");
        Console.WriteLine("\"SummaryRanges\" - Инициализация потока.");
        Console.WriteLine("\"addNum <число>\" - Добавить число в поток.");
        Console.WriteLine("\"getIntervals\" - Показать текущие интервалы.");
        Console.WriteLine("Для завершения введите \"exit\".");

        Console.WriteLine("\nВведите команды (или 'exit' для завершения):");

        while (true)
        {
            string command = Console.ReadLine();
            if (command.ToLower() == "exit") break; // Завершаем программу

            try
            {
                if (command == "SummaryRanges")
                {
                    sr = new SummaryRanges(); // Новый поток
                    result.Add(null);
                    Console.WriteLine("Инициализирован поток.");
                }
                else if (command.StartsWith("addNum"))
                {
                    string[] parts = command.Split(' ');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int num))
                    {
                        sr.AddNum(num); // Добавляем число
                        result.Add(null);
                        Console.WriteLine($"Число {num} добавлено.");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: некорректная команда addNum.");
                    }
                }
                else if (command == "getIntervals")
                {
                    var intervals = sr.GetIntervals(); // Получаем интервалы
                    result.Add(intervals); // Добавляем интервал в результат
                    Console.WriteLine("Текущие интервалы:");
                    PrintIntervals(intervals);
                }
                else
                {
                    Console.WriteLine("Ошибка: неизвестная команда.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при обработке данных: " + ex.Message);
            }

            Console.WriteLine("\nРезультат:");
            PrintResults(result); // Печатаем результаты
        }
    }

    // Печать интервалов
    static void PrintIntervals(int[][] intervals)
    {
        Console.WriteLine("[{0}]", string.Join(", ", intervals.Select(interval => $"[{interval[0]}, {interval[1]}]")));
    }

    // Форматируем и печатаем итоговые результаты
    static void PrintResults(List<object> result)
    {
        Console.WriteLine("[{0}]", string.Join(", ", result.Select(r => r == null ? "null" : FormatInterval(r))));
    }

    // Форматируем интервалы для вывода
    static string FormatInterval(object r)
    {
        if (r is int[][] intervals)
        {
            return "[" + string.Join(", ", intervals.Select(interval => $"[{interval[0]}, {interval[1]}]")) + "]";
        }
        return r.ToString();
    }
}