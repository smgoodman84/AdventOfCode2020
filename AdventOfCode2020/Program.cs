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

            var invalidCount = 0;
            foreach (var day in days)
            {
                var part1 = day.Part1();
                var part2 = day.Part2();

                var part1Invalid = !string.IsNullOrWhiteSpace(day.ValidatedPart1)
                    && part1 != day.ValidatedPart1;

                var part2Invalid = !string.IsNullOrWhiteSpace(day.ValidatedPart2)
                    && part2 != day.ValidatedPart2;

                invalidCount += part1Invalid ? 1 : 0;
                invalidCount += part2Invalid ? 1 : 0;

                var part1InvalidString = part1Invalid ? " INVALID" : "";
                var part2InvalidString = part2Invalid ? " INVALID" : "";

                Console.WriteLine($"Day {day.DayNumber} Part 1: {day.Part1()}{part1InvalidString}");
                Console.WriteLine($"Day {day.DayNumber} Part 2: {day.Part2()}{part2InvalidString}");
            }

            Console.WriteLine($"{invalidCount} INVALID Results");
        }
    }
}
