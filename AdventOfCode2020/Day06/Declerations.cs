using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day06
{
    public class Declerations : IDay
    {
        public int DayNumber => 6;
        public string ValidatedPart1 => "6630";
        public string ValidatedPart2 => "3437";

        private List<GroupDecleration> _groupDeclerations;
        private Declerations(List<GroupDecleration> groupDeclerations)
        {
            _groupDeclerations = groupDeclerations;
        }

        public static Declerations LoadFromFile(string filename)
        {
            var groupDeclerations = GroupedLineFileReader.ReadGroups(filename)
                .Select(CreateGroupDecleration)
                .ToList();

            return new Declerations(groupDeclerations);
        }

        private static GroupDecleration CreateGroupDecleration(List<string> lines)
        {
            var individualDeclerations = lines
                .Select(l => new IndividualDecleration
                {
                    YesAnswers = l.Trim().ToCharArray()
                })
                .ToList();

            var groupDecleration = new GroupDecleration
            {
                IndividualDeclerations = individualDeclerations
            };

            return groupDecleration;
        }

        public string Part1()
        {
            return _groupDeclerations
                .Sum(gd => gd.GetUniqueYesCount())
                .ToString();
        }

        public string Part2()
        {
            return _groupDeclerations
                .Sum(gd => gd.GetConsistentYesCount())
                .ToString();
        }

        private class GroupDecleration
        {
            public List<IndividualDecleration> IndividualDeclerations { get; set; }

            public int GetUniqueYesCount()
            {
                return IndividualDeclerations
                    .SelectMany(id => id.YesAnswers)
                    .Distinct()
                    .Count();
            }

            public int GetConsistentYesCount()
            {
                var uniqueAnswers = 
                    IndividualDeclerations
                    .SelectMany(id => id.YesAnswers)
                    .Distinct();

                var allYes = uniqueAnswers
                    .Where(a => IndividualDeclerations.All(d => d.YesAnswers.Contains(a)))
                    .ToList();

                return allYes.Count();
            }
        }

        private class IndividualDecleration
        {
            public char[] YesAnswers { get; set; }
        }
    }
}
