using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day12
{
    public class Navigation : IDay
    {
        public int DayNumber => 12;
        public string ValidatedPart1 => "1221";
        public string ValidatedPart2 => string.Empty;

        private readonly List<NavigationInstruction> _instructions;

        private Navigation(List<NavigationInstruction> instructions)
        {
            _instructions = instructions;
        }

        public static Navigation LoadFromFile(string filename)
        {
            var lines = File.ReadAllLines(filename)
                .Select(ParseLine)
                .ToList();

            return new Navigation(lines);
        }

        private static NavigationInstruction ParseLine(string line)
        {
            var instruction = line[0];
            var distance = int.Parse(line.Substring(1));

            return new NavigationInstruction()
            {
                Instruction = instruction,
                Distance = distance
            };
        }

        public string Part1()
        {
            var north = 0;
            var east = 0;
            var currentDirection = 'E';

            foreach (var instruction in _instructions)
            {
                var direction = instruction.Instruction;
                if (direction == 'F')
                {
                    direction = currentDirection;
                }

                switch(direction)
                {
                    case 'N':
                        north += instruction.Distance;
                        break;
                    case 'E':
                        east += instruction.Distance;
                        break;
                    case 'S':
                        north -= instruction.Distance;
                        break;
                    case 'W':
                        east -= instruction.Distance;
                        break;
                    case 'L':
                    case 'R':
                        currentDirection = GetBearing(currentDirection, direction, instruction.Distance);
                        break;
                }
            }

            return (Math.Abs(north) + Math.Abs(east)).ToString();
        }

        private char GetBearing(char initialBearing, char direction, int degrees)
        {
            var compassPoints = new[] { 'N', 'E', 'S', 'W' };

            var currentIndex = Array.IndexOf(compassPoints, initialBearing);
            var indexStep = degrees / 90;
            if (direction == 'L')
            {
                indexStep *= -1;
            }

            var newIndex = (currentIndex + indexStep + compassPoints.Length) % compassPoints.Length;

            return compassPoints[newIndex];
        }

        public string Part2()
        {
            return string.Empty;
        }

        private class NavigationInstruction
        {
            public char Instruction { get; set; }
            public int Distance { get; set; }
        }
    }
}
