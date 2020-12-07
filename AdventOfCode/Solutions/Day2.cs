using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace AdventOfCode.Solutions
{
    public class Day2 : AdventOfCodeBase
    {
        private readonly List<(int, int,char, string)> _passwords;

        public Day2()
        {
            Assert.True(File.Exists(InputFilename));

            // Read file
            _passwords = File.ReadAllLines(this.InputFilename).Select(line =>
            {
                var match = Regex.Matches(line, @"^(\d+)\-(\d+)\s(\w)\:\s(\w+)$").Cast<Match>().First();
                return (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), match.Groups[3].Value.ToCharArray()[0], match.Groups[4].Value);
            }).ToList();
        }

        public override string AnswerPartOne()
        {
            var validPasswordCount = _passwords.Count(i =>
            {
                var counted = i.Item4.Count(s => s == i.Item3);
                return counted >= i.Item1 && counted <= i.Item2;
            });
            return $"{validPasswordCount}";
        }

        public override string AnswerPartTwo()
        {
            var validPasswordCount = _passwords.Count(i =>
            {
                var chars = new List<char> {
                    i.Item4[i.Item1 - 1],
                    i.Item4[i.Item2 - 1]
                };

                return chars.Count(c=> c ==  i.Item3) == 1;
            });
            return $"{validPasswordCount}";
        }

    }
}
