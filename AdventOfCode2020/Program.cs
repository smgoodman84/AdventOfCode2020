using System;
using AdventOfCode2020.Day01;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var day1Part1 = ExpenseReport
                .LoadFromFile("Day01/ExpenseReport.txt")
                .GetResult();

            Console.WriteLine($"Day 1 Part 1: {day1Part1}");

            var day1Part2 = ExpenseReport
                .LoadFromFile("Day01/ExpenseReport.txt")
                .GetPart2Result();

            Console.WriteLine($"Day 1 Part 2: {day1Part2}");
        }
    }
}
