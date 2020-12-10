using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day10
{
    public class JoltAdapters : IDay
    {
        public int DayNumber => 10;
        public string ValidatedPart1 => "1920";
        public string ValidatedPart2 => "1511207993344";

        private readonly int[] _adapters;
        private readonly int[] _orderedAdapters;

        public JoltAdapters(int[] adapters)
        {
            _adapters = adapters;
            _orderedAdapters = _adapters.Append(0).OrderBy(a => a).ToArray();
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
            var differences = _orderedAdapters
                .Select((x, i) =>
                    i < _orderedAdapters.Length - 1 ? _orderedAdapters[i + 1] - x : 3)
                .ToList();

            var grouped = differences
                .GroupBy(d => d);

            var oneJoltDiffCount = grouped.First(d => d.Key == 1).Count();
            var threeJoltDiffCount = grouped.First(d => d.Key == 3).Count();

            return (oneJoltDiffCount * threeJoltDiffCount).ToString();
        }

        public string Part2()
        {
            var result = CountCombinationsFrom(0);

            return result.ToString();
        }

        private Dictionary<int, long> _combinationCache = new Dictionary<int, long>();
        public long CachedCountCombinationsFrom(int index)
        {
            if (!_combinationCache.ContainsKey(index))
            {
                _combinationCache.Add(index, CountCombinationsFrom(index));
            }

            return _combinationCache[index];
        }

        public long CountCombinationsFrom(int index)
        {
            if (index == _orderedAdapters.Length - 1)
            {
                return 1;
            }

            var nextIndex = index + 1;

            long possibleCombinations = 0;
            while (nextIndex < _orderedAdapters.Length &&
                _orderedAdapters[nextIndex] - _orderedAdapters[index] <= 3)
            {
                possibleCombinations += CachedCountCombinationsFrom(nextIndex);
                nextIndex += 1;
            }

            return possibleCombinations;
        }
    }
}
