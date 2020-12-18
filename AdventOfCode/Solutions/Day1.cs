using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xunit;

namespace AdventOfCode.Solutions
{
    public class Day1 : AdventOfCodeBase
    {
        private readonly List<int> _expenses;

        public Day1()
        {
            Assert.True(File.Exists(this.InputFilename));
            _expenses = File.ReadAllLines(this.InputFilename).Select(l => int.Parse(l)).ToList();
        }

        public override string AnswerPartOne()
        {
            // Find the two expenses which add to 2020
            var twoEntriesWithSumEqualsTo2020 = _expenses
                .SelectMany((expenseValue1) => _expenses
                    // Only the entries which sum to 2020
                    .Where((expenseValue2) => expenseValue1 + expenseValue2 == 2020)
                    .Select((expenseValue2) =>
                    {
                        var solution = new List<int> { expenseValue1, expenseValue2 };
                        solution.Sort();
                        return (First: solution[0], Second: solution[1]);
                    })).Distinct().ToList();

            // Validate input
            Assert.True(twoEntriesWithSumEqualsTo2020.Count == 1);

            var (First, Second) = twoEntriesWithSumEqualsTo2020.First();

            Debug.WriteLine($"Day one, part one, check: {First} + {Second} = 2020, answer: {First} * {Second} = {First * Second}");
            return $"{First * Second}";
        }

        public override string AnswerPartTwo()
        {
            var threeEntriesWithSumEqualsTo2020 = _expenses
                .SelectMany(expenseValue1 => _expenses
                    .SelectMany(expenseValue2 => _expenses
                        // Only the entries which sum to 2020
                        .Where(expenseValue3 => expenseValue1 + expenseValue2 + expenseValue3 == 2020)
                        .Select(expenseValue3 =>
                        {
                            var solution = new List<int> { expenseValue1, expenseValue2, expenseValue3 };
                            solution.Sort();
                            return (First: solution[0], Second: solution[1], Third: solution[2]);
                        }))).Distinct().ToList();

            // Validate input
            Assert.True(threeEntriesWithSumEqualsTo2020.Count == 1);
            var (First, Second, Third) = threeEntriesWithSumEqualsTo2020.First();
            Debug.WriteLine($"Day one, part two, check: {First} + {Second} + {Third} = 2020, answer: {First} * {Second} * {Third}= {First * Second * Third}");
            return $"{First * Second * Third}";
        }

    }
}
