using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day04
{
    public class PassportValidator : IDay
    {
        public int DayNumber => 4;

        private List<Passport> _passports;
        private PassportValidator(List<Passport> passports)
        {
            _passports = passports;
        }

        public static PassportValidator LoadFromFile(string filename)
        {
            var passports = new List<Passport>();
            var lines = File.ReadAllLines(filename);

            var currentLines = new List<string>();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    passports.Add(CreatePassport(currentLines));
                    currentLines = new List<string>();
                }

                currentLines.Add(line);
            }

            if (currentLines.Any())
            {
                passports.Add(CreatePassport(currentLines));
            }


            return new PassportValidator(passports);
        }

        private static Passport CreatePassport(List<string> lines)
        {
            var passport = new Passport();
            var joinedLines = string.Join(' ', lines);
            var fields = joinedLines.Split(' ').Select(f => f.Trim()).ToList();
            foreach (var field in fields)
            {
                var keyValue = field.Split(':').ToArray();
                var key = keyValue[0].Trim();
                var value = keyValue.Length > 1 ? keyValue[1].Trim() : null;
                switch(key)
                {
                    case "byr":
                        passport.BirthYear = int.Parse(value);
                        break;
                    case "iyr":
                        passport.IssueYear = int.Parse(value);
                        break;
                    case "eyr":
                        passport.ExpirationYear = int.Parse(value);
                        break;
                    case "hgt":
                        passport.Height = value;
                        break;
                    case "hcl":
                        passport.HairColour = value;
                        break;
                    case "ecl":
                        passport.EyeColour = value;
                        break;
                    case "pid":
                        passport.PassportId = value;
                        break;
                    case "cid":
                        passport.CountryId = int.Parse(value);
                        break;
                }
            }

            return passport;
        }

        private bool IsValidPassport(Passport passport)
        {
            return passport.BirthYear != null
                && passport.IssueYear != null
                && passport.ExpirationYear != null
                && passport.Height != null
                && passport.HairColour != null
                && passport.EyeColour != null
                && passport.PassportId != null;
        }

        public string Part1() => _passports.Count(IsValidPassport).ToString();

        public string Part2()
        {
            return string.Empty;
        }

        private class Passport
        {
            public int? BirthYear { get; set; }
            public int? IssueYear { get; set; }
            public int? ExpirationYear { get; set; }
            public string Height { get; set; }
            public string HairColour { get; set; }
            public string EyeColour { get; set; }
            public string PassportId { get; set; }
            public int? CountryId { get; set; }
        }
    }
}
