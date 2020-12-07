using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day07
{
    public class LuggageRules : IDay
    {
        public int DayNumber => 7;
        public string ValidatedPart1 => "316";
        public string ValidatedPart2 => string.Empty;

        private Dictionary<string, LuggageRule> _luggageRules;
        private Dictionary<string, List<string>> _canGoIn;
        private LuggageRules(Dictionary<string, LuggageRule> luggageRules)
        {
            _luggageRules = luggageRules;

            _canGoIn = _luggageRules
                .SelectMany(lr => lr.Value.MustContain.Keys
                    .Select(contain => new KeyValuePair<string, string>(contain, lr.Key)))
                .GroupBy(kvp => kvp.Key)
                .ToDictionary(g => g.Key, g => g.Select(kvp => kvp.Value).ToList());
        }

        public static LuggageRules LoadFromFile(string filename)
        {
            var lines = File.ReadAllLines(filename)
                .Select(ParseLine)
                .ToDictionary(lr => lr.Colour, lr => lr);

            return new LuggageRules(lines);
        }

        private static LuggageRule ParseLine(string luggageRule)
        {
            return new LuggageRule(luggageRule);
        }

        public string Part1()
        {
            var shinyGoldCanGoIn = new HashSet<string>();
            var newColours = _canGoIn["shiny gold"];

            while (newColours.Any())
            {
                newColours.ForEach(c => shinyGoldCanGoIn.Add(c));

                newColours = GetCanGoIn(newColours)
                    .Where(c => !shinyGoldCanGoIn.Contains(c))
                    .ToList();
            }

            return shinyGoldCanGoIn.Count().ToString();
        }

        public IEnumerable<string> GetCanGoIn(IEnumerable<string> colours)
        {
            var result = colours
                .SelectMany(c => _canGoIn.ContainsKey(c) ? _canGoIn[c] : new List<string>())
                .Distinct();

            return result;
        }

        public string Part2()
        {
            return string.Empty;
        }

        private class LuggageRule
        {
            public Dictionary<string, int> MustContain { get; private set; }
            public string Colour { get; set; }

            public LuggageRule(string luggageRule)
            {
                var rule = luggageRule
                    .Replace("bags", "bag")
                    .Replace(".", string.Empty);

                var colourRule = rule.Split(" bag contain ");
                Colour = colourRule[0];

                if (colourRule[1] == "no other bag")
                {
                    MustContain = new Dictionary<string, int>();
                }
                else
                {
                    var rules = colourRule[1]
                        .Split(",")
                        .Select(r => r.Trim())
                        .Select(ParseRule)
                        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                    MustContain = rules;
                }
            }

            private KeyValuePair<string, int> ParseRule(string rule)
            {
                rule = rule.Replace("bag", string.Empty).Trim();

                var firstSpace = rule.IndexOf(' ');
                var countString = rule.Substring(0, firstSpace);
                var colour = rule.Substring(firstSpace + 1);
                return new KeyValuePair<string, int>(colour, int.Parse(countString));
            }
        }
    }
}
