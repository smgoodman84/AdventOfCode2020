using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day10
{
    public class JoltAdapters : IDay
    {
        public int DayNumber => 10;
        public string ValidatedPart1 => string.Empty;
        public string ValidatedPart2 => string.Empty;

        private readonly int[] _adapters;

        public JoltAdapters(int[] adapters)
        {
            _adapters = adapters;
        }

        public static JoltAdapters LoadFromFile(string filename)
        {
            var lines = File.ReadAllLines(filename)
                .Select(int.Parse)
                .ToArray();

            return new JoltAdapters(lines);
        }

        public string Part1()
        {
            var orderedAdapters = _adapters.Append(0).OrderBy(a => a).ToArray();

            var differences = orderedAdapters
                .Select((x, i) =>
                    i < orderedAdapters.Length - 1 ? orderedAdapters[i + 1] - x : 3)
                .ToList();

            var grouped = differences
                .GroupBy(d => d);

            var oneJoltDiffCount = grouped.First(d => d.Key == 1).Count();
            var threeJoltDiffCount = grouped.First(d => d.Key == 3).Count();

            return (oneJoltDiffCount * threeJoltDiffCount).ToString();
        }

        public string Part2()
        {
            return string.Empty;
        }
    }
}
