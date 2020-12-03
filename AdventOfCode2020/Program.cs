using System;
using AdventOfCode2020.Day01;
using AdventOfCode2020.Day02;
using AdventOfCode2020.Day03;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            Day1();
            Day2();
            Day3();
        }

        static void Day1()
        {
            var day1 = ExpenseReport.LoadFromFile("Day01/ExpenseReport.txt");

            var day1Part1 = day1.Part1();
            Console.WriteLine($"Day 1 Part 1: {day1Part1}");

            var day1Part2 = day1.Part2();
            Console.WriteLine($"Day 1 Part 2: {day1Part2}");
        }

        static void Day2()
        {
            var day2 = PasswordValidator.LoadFromFile("Day02/Passwords.txt");

            var day2Part1 = day2.Part1();
            Console.WriteLine($"Day 2 Part 1: {day2Part1}");

            var day2Part2 = day2.Part2();
            Console.WriteLine($"Day 2 Part 2: {day2Part2}");
        }

        static void Day3()
        {
            var day3 = Map.LoadFromFile("Day03/Map.txt");

            var day3Part1 = day3.Part1();
            Console.WriteLine($"Day 3 Part 1: {day3Part1}");

            var day3Part2 = day3.Part2();
            Console.WriteLine($"Day 3 Part 2: {day3Part2}");
        }
    }
}
