using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace AdventOfCode.Solutions
{
    public class Day13 : AdventOfCodeBase
    {
        private readonly int _earliestDepartTime;
        private readonly int[] _potentialBusIds;

        public Day13()
        {
            Assert.True(File.Exists(InputFilename));

            var inputLines = File.ReadAllLines(this.InputFilename);
            _earliestDepartTime = int.Parse(inputLines.First());

            _potentialBusIds = inputLines[1].Split(',').Where(s => !"x".Equals(s)).Select(s => int.Parse(s)).ToArray();
        }

        public override string AnswerPartOne()
        {
            // find next department times
            var nextDepartureTime = _potentialBusIds
                .Select(potentialBusId =>
                    {
                        int nextTime = (int)(Math.Ceiling((float)_earliestDepartTime / (float)potentialBusId) * potentialBusId);
                        var waitingTime = nextTime - _earliestDepartTime;
                        return (potentialBusId, waitingTime);
                    }
                )
                .OrderBy(busInfo => busInfo.waitingTime)
                .First();
            var answer = nextDepartureTime.potentialBusId * nextDepartureTime.waitingTime;
            return $"{answer}";
        }

        public override string AnswerPartTwo()
        {
            var answer = 0;
            return $"{answer}";
        }

    }
}
