using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day15
{
    public class MemoryGame : IDay
    {
        public int DayNumber => 15;
        public string ValidatedPart1 => "249";
        public string ValidatedPart2 => "41687";

        private readonly List<int> _startingNumbers;
        public MemoryGame(string startingNumbers)
        {
            _startingNumbers = startingNumbers
                .Split(",")
                .Select(int.Parse)
                .ToList();
        }

        public static MemoryGame Create(string startingNumbers) => new MemoryGame(startingNumbers);

        public string Part1() => FindNthSpoken(2020).ToString();
        public string Part2() => FindNthSpoken(30000000).ToString();

        private int FindNthSpoken(int n)
        {
            var count = n;
            var history = new History();
            foreach (var startingNumber in _startingNumbers)
            {
                history.Spoken(startingNumber);
                count -= 1;
            }

            while (count > 0)
            {
                var number = history.GetNextNumber();
                history.Spoken(number);
                count -= 1;
            }

            return history.LastSpoken.Value;
        }

        private class History
        {
            public int? LastSpoken { get; private set; }

            private int _timeStamp = 1;
            private Dictionary<int, NumberHistory> _history = new Dictionary<int, NumberHistory>();

            public void Spoken(int number)
            {
                // Console.WriteLine($"{_timeStamp}: {number}");
                if (!_history.ContainsKey(number))
                {
                    _history[number] = new NumberHistory(_timeStamp);
                }
                else
                {
                    _history[number].Spoken(_timeStamp);
                }

                LastSpoken = number;

                _timeStamp += 1;
            }

            public int GetNextNumber()
            {
                var numberHistory = _history[LastSpoken.Value];

                if (numberHistory.LastSpoken2 == null)
                {
                    return 0;
                }

                return numberHistory.LastSpoken - numberHistory.LastSpoken2.Value;
            }
        }

        private class NumberHistory
        {
            public int LastSpoken { get; private set; }
            public int? LastSpoken2 { get; private set; }

            public NumberHistory(int timeStamp)
            {
                LastSpoken = timeStamp;
            }

            public void Spoken(int timeStamp)
            {
                LastSpoken2 = LastSpoken;
                LastSpoken = timeStamp;
            }
        }
    }
}
