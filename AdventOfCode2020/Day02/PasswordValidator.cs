using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day02
{
    public class PasswordValidator
    {
        private IEnumerable<PasswordWithPolicy> _passwordPolicies;
        private PasswordValidator(IEnumerable<PasswordWithPolicy> passwordPolicies)
        {
            _passwordPolicies = passwordPolicies;
        }

        public static PasswordValidator LoadFromFile(string filename)
        {
            var expenses = File.ReadAllLines(filename)
                .Select(ParseLine)
                .ToList();

            return new PasswordValidator(expenses);
        }

        private static PasswordWithPolicy ParseLine(string line)
        {
            var policyPasswordSplit = line.Split(':');
            var policy = policyPasswordSplit[0].Trim();
            var password = policyPasswordSplit[1].Trim();

            var requiredCharacterSplit = policy.Split(' ');
            var minMax = requiredCharacterSplit[0].Trim();
            var requiredCharacter = requiredCharacterSplit[1].Trim()[0];

            var minMaxSplit = minMax.Split('-');
            var min = int.Parse(minMaxSplit[0].Trim());
            var max = int.Parse(minMaxSplit[1].Trim());

            return new PasswordWithPolicy
            {
                Password = password,
                RequiredCharacter = requiredCharacter,
                MinOccurences = min,
                MaxOccurences = max
            };
        }

        public int Part1()
        {
            return _passwordPolicies.Count(IsValid);
        }

        private bool IsValid(PasswordWithPolicy passwordWithPolicy)
        {
            var count = passwordWithPolicy.Password
                .Count(c => c == passwordWithPolicy.RequiredCharacter);

            return passwordWithPolicy.MinOccurences <= count
                && count <= passwordWithPolicy.MaxOccurences;
        }

        private class PasswordWithPolicy
        {
            public string Password { get; set; }
            public char RequiredCharacter { get; set; }
            public int MinOccurences { get; set; }
            public int MaxOccurences { get; set; }
        }
    }
}
