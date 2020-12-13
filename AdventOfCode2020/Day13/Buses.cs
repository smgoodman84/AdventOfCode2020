using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day13
{
    public class Buses : IDay
    {
        public int DayNumber => 13;
        public string ValidatedPart1 => "4808";
        public string ValidatedPart2 => string.Empty;

        private readonly int _earliestDeparture;
        private readonly List<int> _busIds;

        private Buses(int earliestDeparture, List<int> busIds)
        {
            _earliestDeparture = earliestDeparture;
            _busIds = busIds;
        }

        public static Buses LoadFromFile(string filename)
        {
            var lines = File.ReadAllLines(filename);

            var earliestDeparture = int.Parse(lines[0]);

            var busIds = lines[1].Split(',')
                .Where(lines => lines != "x")
                .Select(int.Parse)
                .ToList();

            return new Buses(earliestDeparture, busIds);
        }

        public string Part1()
        {
            var buses = _busIds
                .Select(CalculateNextDeparture)
                .ToList();

            var nextBus = buses
                .OrderBy(b => b.NextDeparture)
                .First();

            var wait = nextBus.NextDeparture - _earliestDeparture;
            return (wait * nextBus.BusId).ToString();
        }

        private Bus CalculateNextDeparture(int busId)
        {
            var divider = _earliestDeparture / busId;
            var nextDeparture = busId * divider;
            if (nextDeparture < _earliestDeparture)
            {
                nextDeparture += busId;
            }

            return new Bus
            {
                NextDeparture = nextDeparture,
                BusId = busId
            };
        }

        public string Part2()
        {
            return string.Empty;
        }

        private class Bus
        {
            public int BusId { get; set; }
            public int NextDeparture { get; set; }
        }
    }
}
