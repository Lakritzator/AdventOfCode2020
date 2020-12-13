using System;
using System.IO;
using System.Linq;
using Xunit;

namespace AdventOfCode.Solutions
{
    public class Day5 : AdventOfCodeBase
    {
        private readonly string[] _boardingPasses;

        public Day5()
        {
            Assert.True(File.Exists(InputFilename));

            // Read file into boarding passes
            _boardingPasses = File.ReadAllLines(this.InputFilename);
        }

        public (int Row, int Seat, int ID) CalculateRowSeat(string partitioningData)
        {
            var currentRange = (Low: 0, High: 127);
            foreach(char part in partitioningData)
            {
                if (part == 'F')
                {
                    currentRange.High = currentRange.Low + ((currentRange.High+1-currentRange.Low)/ 2) - 1;
                }
                else if(part == 'B')
                {
                    currentRange.Low = currentRange.Low + ((currentRange.High+1 - currentRange.Low)/ 2);
                }
            }
            if (currentRange.High != currentRange.Low)
            {
                throw new Exception($"Partitioning row data is invalid: {partitioningData}");
            }
            // Take the row from one of the high/low values
            int row = currentRange.High;
            currentRange = (0, 7);
            foreach (char part in partitioningData)
            {
                if (part == 'L')
                {
                    currentRange.High = currentRange.Low + ((currentRange.High + 1 - currentRange.Low) / 2) - 1;
                }
                else if (part == 'R')
                {
                    currentRange.Low = currentRange.Low + ((currentRange.High + 1 - currentRange.Low) / 2);
                }
            }
            if (currentRange.High != currentRange.Low)
            {
                throw new Exception($"Partitioning seat data is invalid: {partitioningData}");
            }
            // Take the row from one of the high/low values
            int seat = currentRange.High;
            return (row, seat, (row*8)+seat);
        }

        public override string AnswerPartOne()
        {
            int answer = 0;
            foreach(var boardingPass in _boardingPasses)
            {
                var boardingPassRowSeat = CalculateRowSeat(boardingPass);
                answer = Math.Max(boardingPassRowSeat.ID, answer);
            }
            return $"{answer}";
        }

        public override string AnswerPartTwo()
        {
            int answer = 0;

            var foundBoardingPassIds = _boardingPasses.Select(bp => CalculateRowSeat(bp).ID).OrderBy(id => id).ToList();
            var previousId = foundBoardingPassIds.First();
            foreach(var id in foundBoardingPassIds)
            {
                if (id - previousId > 1)
                {
                    answer = id -1;
                    break;
                }
                previousId = id;
            }

            return $"{answer}";
        }

    }
}
