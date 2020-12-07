using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace AdventOfCode.Solutions
{
    public class Day4 : AdventOfCodeBase
    {
        private readonly List<Dictionary<string, string>> _passports = new List<Dictionary<string, string>>();

        private void AddPassport(string currentPassport)
        {
            if (string.IsNullOrEmpty(currentPassport))
            {
                return;
            }
            var passport = Regex
                        .Matches(currentPassport, @"([a-zA-Z0-9#]+):([a-zA-Z0-9#]+)\s+")
                        .Cast<Match>()
                        .Select(m => (Key: m.Groups[1].Value, Value: m.Groups[2].Value))
                        .ToDictionary(kv => kv.Key, k => k.Value);
            _passports.Add(passport);
        }

        public Day4()
        {
            Assert.True(File.Exists(InputFilename));

            // Read file into maze
            var passportFile = File.ReadAllLines(this.InputFilename);
            var currentPassport = "";
            foreach(var passportLine in passportFile)
            {
                if (!string.IsNullOrEmpty(passportLine))
                {
                    currentPassport += passportLine + " ";
                }
                else
                {
                    AddPassport(currentPassport);
                    currentPassport = "";
                }
            }
            // Process the last
            AddPassport(currentPassport);
        }

        public override string AnswerPartOne()
        {
            var answer = _passports.Count(p => p.Count == 8 || (p.Count == 7 && !p.ContainsKey("cid")));
            return $"{answer}";
        }

        public override string AnswerPartTwo()
        {
            int answer = 0;
            return $"{answer}";
        }

    }
}
