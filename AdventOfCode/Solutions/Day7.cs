using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace AdventOfCode.Solutions
{
    public class Day7 : AdventOfCodeBase
    {
        private readonly Dictionary<string, Dictionary<string, int>> _bagRules = new Dictionary<string, Dictionary<string, int>>();


        public Day7()
        {
            Assert.True(File.Exists(InputFilename));

            var bagRules = File.ReadAllLines(this.InputFilename);
            // Parse bag rules
            foreach(var bagRule in bagRules)
            {
                var matchBag = Regex.Match(bagRule, @"^(?<bagname>.*)\sbags contain (?<bagcontents>.*)\.$");
                var bagName = matchBag.Groups["bagname"].Value;
                var bagContentsDescription = matchBag.Groups["bagcontents"].Value;
                var bagContents = new Dictionary<string, int>();
                foreach (var bagContentDescription in bagContentsDescription.Split(','))
                {
                    var matchContent = Regex.Match(bagContentDescription.Trim(), @"^(?<number>\d+)\s(?<containedbagname>.+)\s(bags|bag)$");
                    if (matchContent.Groups.Count ==4)
                    {
                        var containedBagName = matchContent.Groups["containedbagname"].Value;
                        var number = matchContent.Groups["number"].Value;
                        bagContents[containedBagName] = int.Parse(number);
                    }
                }
                _bagRules[bagName] = bagContents;
            }

        }

        private bool CanContain(string bagName, string bagToContain)
        {
            if (!_bagRules.ContainsKey(bagName))
            {
                return false;
            }

            var currentBag = _bagRules[bagName];
            if (currentBag.ContainsKey(bagToContain))
            {
                return true;
            }
            foreach(var containedBag in currentBag.Keys)
            {
                if (CanContain(containedBag, bagToContain))
                {
                    return true;
                }
            }
            return false;
        }

        public override string AnswerPartOne()
        {
            int answer = 0;
            foreach(var bagName in _bagRules.Keys)
            {
                if (CanContain(bagName, "shiny gold"))
                {
                    answer++;
                }
            }
            return $"{answer}";
        }

        private int CountBagsInside(string bagName)
        {
            if (!_bagRules.ContainsKey(bagName))
            {
                return 0;
            }

            var currentBag = _bagRules[bagName];
            int count = 0;

            foreach(var containedBag in currentBag.Keys)
            {
                int containedBagCount = currentBag[containedBag];
                count += containedBagCount;
                var bagsInside = CountBagsInside(containedBag);
                count += containedBagCount * bagsInside;
            }
            return count;
        }
        public override string AnswerPartTwo()
        {
            int answer = CountBagsInside("shiny gold");
            return $"{answer}";
        }

    }
}
