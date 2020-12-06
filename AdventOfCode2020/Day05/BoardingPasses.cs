using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day05
{
    public class BoardingPasses : IDay
    {
        public int DayNumber => 5;
        public string ValidatedPart1 => "890";
        public string ValidatedPart2 => "651";

        private Seat[] _seats;
        private BoardingPasses(Seat[] seats)
        {
            _seats = seats;
        }

        public static BoardingPasses LoadFromFile(string filename)
        {
            var lines = File.ReadAllLines(filename)
                .Select(ParseLine)
                .ToArray();

            return new BoardingPasses(lines);
        }

        private static Seat ParseLine(string seatNumber)
        {
            return new Seat(seatNumber);
        }

        public string Part1() => _seats.Max(s => s.SeatId).ToString();

        public string Part2()
        {
            var seatRange = Enumerable.Range(0, _seats.Max(s => s.SeatId));
            var allSeats = new HashSet<int>(_seats.Select(s => s.SeatId));

            var missingSeats = seatRange.Where(s => !allSeats.Contains(s)).ToList();

            var mySeat = missingSeats.FirstOrDefault(s =>
                !missingSeats.Contains(s + 1) && !missingSeats.Contains(s - 1)
            );

            return mySeat.ToString();
        }

        private class Seat
        {
            public Seat(string seatNumber)
            {
                SeatNumber = seatNumber;

                var row = SeatNumber.Substring(0, 7);
                var col = SeatNumber.Substring(7);

                RowNumber = CalculateRowNumber(row);
                ColumnNumber = CalculateColumnNumber(col);
            }

            private int CalculateRowNumber(string row) => CalculateNumber(row, 'B');

            private int CalculateColumnNumber(string col) => CalculateNumber(col, 'R');

            private int CalculateNumber(string input, char highHalf)
            {
                var value = 0;
                var step = (int)Math.Pow(2, input.Length - 1);
                var index = 0;
                var characters = input.ToCharArray();
                while (step > 0)
                {
                    if (characters[index] == highHalf)
                    {
                        value += step;
                    }

                    step /= 2;
                    index += 1;
                }

                return value;
            }

            public string SeatNumber { get; private set; }
            public int RowNumber { get; private set; }
            public int ColumnNumber { get; private set; }
            public int SeatId => RowNumber * 8 + ColumnNumber;
        }
    }
}
