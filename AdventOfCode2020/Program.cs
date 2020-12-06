using System;
using System.Collections.Generic;
using AdventOfCode2020.Day01;
using AdventOfCode2020.Day02;
using AdventOfCode2020.Day03;
using AdventOfCode2020.Day04;
using AdventOfCode2020.Day05;
using AdventOfCode2020.Day06;

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
                Map.LoadFromFile("Day03/Map.txt"),
                PassportValidator.LoadFromFile("Day04/PassportData.txt"),
                BoardingPasses.LoadFromFile("Day05/Seats.txt"),
                Declerations.LoadFromFile("Day06/Declerations.txt")
            };

            foreach (var day in days)
            {
                Console.WriteLine($"Day {day.DayNumber} Part 1: {day.Part1()}");
                Console.WriteLine($"Day {day.DayNumber} Part 2: {day.Part2()}");
            }
        }
    }
}
