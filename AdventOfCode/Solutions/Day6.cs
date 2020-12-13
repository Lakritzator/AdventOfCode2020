using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace AdventOfCode.Solutions
{
    public class Day6 : AdventOfCodeBase
    {
        private readonly List<List<string>> _answers = new List<List<string>>();


        public Day6()
        {
            Assert.True(File.Exists(InputFilename));

            var answersFile = File.ReadAllLines(this.InputFilename);
            List<string> groupAnswers = new List<string>();
            foreach(var answerLine in answersFile)
            {
                if (!string.IsNullOrWhiteSpace(answerLine))
                {
                    groupAnswers.Add(answerLine.Trim());
                }
                else
                {
                    if (groupAnswers.Count > 0)
                    {
                        _answers.Add(groupAnswers);
                    }
                    groupAnswers = new List<string>();
                }
            }
            // Process the last
            _answers.Add(groupAnswers);
        }

        public override string AnswerPartOne()
        {
            var answer = _answers
                // combine all answers from the group to one string
                .Select(l => string.Join(string.Empty, l)
                    // Change the string to chars
                    .Select(c => c)
                        // Find all unique answers
                        .Distinct()
                        // Count them
                        .Count())
                // Create the sum
                .Sum();

            return $"{answer}";
        }

        public override string AnswerPartTwo()
        {
            var answer = _answers
                .Select(l =>
                {
                    var groupAnswers = l.First().Select(c => c);
                    if (l.Count > 1)
                    {
                        for (int i = 1; i < l.Count; i++)
                        {
                            groupAnswers = groupAnswers.Intersect(l[i].Select(c => c));
                        }
                    }
                    return groupAnswers.Count();
                }).Sum();
            return $"{answer}";
        }

    }
}
