﻿using System;
using System.Collections.Generic;
using AdventOfCode2020.Day01;
using AdventOfCode2020.Day02;
using AdventOfCode2020.Day03;
using AdventOfCode2020.Day04;
using AdventOfCode2020.Day05;
using AdventOfCode2020.Day06;
using AdventOfCode2020.Day07;
using AdventOfCode2020.Day08;
using AdventOfCode2020.Day09;
using AdventOfCode2020.Day10;
using AdventOfCode2020.Day11;
using AdventOfCode2020.Day12;
using AdventOfCode2020.Day13;
using AdventOfCode2020.Day14;
using AdventOfCode2020.Day15;
using AdventOfCode2020.Day16;
using AdventOfCode2020.Day17;
using AdventOfCode2020.Day18;
using AdventOfCode2020.Day20;
using AdventOfCode2020.Day22;

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
                Declerations.LoadFromFile("Day06/Declerations.txt"),
                LuggageRules.LoadFromFile("Day07/LuggageRules.txt"),
                GameConsole.LoadFromFile("Day08/BootCode.txt"),
                AdditionSystem.LoadFromFile("Day09/Data.txt"),
                JoltAdapters.LoadFromFile("Day10/JoltAdapters.txt"),
                SeatingLayout.LoadFromFile("Day11/SeatingLayout.txt"),
                Navigation.LoadFromFile("Day12/NavigationInstructions.txt"),
                Buses.LoadFromFile("Day13/Buses.txt"),
                DockingProgram.LoadFromFile("Day14/DockingProgram.txt"),
                MemoryGame.Create("15,12,0,14,3,1"),
                TicketAnalyser.LoadFromFile("Day16/TicketData.txt"),
                ConwayCube.LoadFromFile("Day17/ConwayCube.txt"),
                OperationOrder.LoadFromFile("Day18/Expressions.txt"),
                JurassicJigsaw.LoadFromFile("Day20/Tiles.txt"),
                Combat.LoadFromFile("Day22/Cards.txt")
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

                Console.WriteLine($"Day {day.DayNumber} Part 1: {part1}{part1InvalidString}");
                Console.WriteLine($"Day {day.DayNumber} Part 2: {part2}{part2InvalidString}");
            }

            Console.WriteLine($"{invalidCount} INVALID Results");
        }
    }
}