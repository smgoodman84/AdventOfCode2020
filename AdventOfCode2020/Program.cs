using System;
using System.Collections.Generic;
using AdventOfCode2020.Day01;
using AdventOfCode2020.Day02;
using AdventOfCode2020.Day03;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var days = new List<IDay>()
            {
                ExpenseReport.LoadFromFile("Day01/ExpenseReport.txt"),
                PasswordValidator.LoadFromFile("Day02/Passwords.txt"),
                Map.LoadFromFile("Day03/Map.txt")
            };

            foreach (var day in days)
            {
                Console.WriteLine($"Day {day.DayNumber} Part 1: {day.Part1()}");
                Console.WriteLine($"Day {day.DayNumber} Part 2: {day.Part2()}");
            }
        }
    }
}
