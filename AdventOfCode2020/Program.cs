using System;
using AdventOfCode2020.Day01;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var day1 = ExpenseReport
                .LoadFromFile("Day01/ExpenseReport.txt")
                .GetResult();

            Console.WriteLine($"Day1: {day1}");
        }
    }
}
