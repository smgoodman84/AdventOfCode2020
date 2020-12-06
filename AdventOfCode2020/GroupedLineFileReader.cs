using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    public static class GroupedLineFileReader
    {
        public static List<List<string>> ReadGroups(string filename)
        {
            var groups = new List<List<string>>();
            var lines = File.ReadAllLines(filename);

            var currentLines = new List<string>();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    groups.Add(currentLines);
                    currentLines = new List<string>();
                }
                else
                {
                    currentLines.Add(line);
                }
            }

            if (currentLines.Any())
            {
                groups.Add(currentLines);
            }

            return groups;
        }
    }
}
