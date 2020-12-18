using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using JM.LinqFaster;

namespace AdventOfCode.Solutions
{
    public class Day9 : AdventOfCodeBase
    {
        private readonly long[] _values;

        public Day9()
        {
            Assert.True(File.Exists(InputFilename));

            // Read file
            _values = File.ReadAllLines(this.InputFilename).Select(line => long.Parse(line)).ToArray();
        }

        private static long FindMismatch(long[] values, int size)
        {
            for (int i = size; i < values.Length; i++)
            {
                var preamble = values.AsSpan().Slice(i - size, size);
                var currentValue = values[i];
                bool found = false;
                for (int pi = 0; pi < preamble.Length; pi++)
                {
                    var searchValue = currentValue - preamble[pi];
                    if (preamble.Contains(searchValue))
                    {
                        found = true;
                        break;
                    }

                }
                if (!found)
                {
                    return currentValue;
                }
            }
            return -1;
        }

        public override string AnswerPartOne()
        {
            long answer = FindMismatch(_values, 25);
            return $"{answer}";
        }

        private static long FindMinMaxSumInRange(long []values, long targetValue)
        {
            for (int i = 0; i < values.Length; i++)
            {
                long sum = 0;
                int sumRangeStart = i;
                int sumIndex = i;
                do
                {
                    sum += values[sumIndex];
                    int rangeSize = 1 + (sumIndex - sumRangeStart);
                    if (sum == targetValue && rangeSize >= 2)
                    {
                        var foundRange = values.AsSpan(sumRangeStart, rangeSize);
                        long min = foundRange.MinF();
                        long max = foundRange.MaxF();
                        return min + max;
                    }
                    sumIndex++;
                } while (sum < targetValue && sumIndex < values.Length);
            }
            return -1;
        }
        public override string AnswerPartTwo()
        {
            // Take the answer from one
            long lookingForSum = FindMismatch(_values, 25);
            long answer = FindMinMaxSumInRange(_values, lookingForSum);

            return $"{answer}";
        }

    }
}
