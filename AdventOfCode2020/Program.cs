using System;
using AdventOfCode2020.Day01;
using AdventOfCode2020.Day02;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            Day1();
            Day2();
        }

        static void Day1()
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

        static void Day2()
        {
            var day2Part1 = PasswordValidator
                .LoadFromFile("Day02/Passwords.txt")
                .Part1();

            Console.WriteLine($"Day 2 Part 1: {day2Part1}");
        }
    }
}
