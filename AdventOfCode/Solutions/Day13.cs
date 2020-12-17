using System.IO;
using System.Linq;
using Xunit;

namespace AdventOfCode.Solutions
{
    public class Day13 : AdventOfCodeBase
    {
        private readonly long _earliestDepartTime;
        private readonly long[] _potentialBusIds;

        public Day13()
        {
            Assert.True(File.Exists(InputFilename));

            var inputLines = File.ReadAllLines(this.InputFilename);
            _earliestDepartTime = long.Parse(inputLines.First());

            _potentialBusIds = ExtractBusIds(inputLines[1]);
        }
        private static long[] ExtractBusIds(string busData)
        {
            return busData.Split(',').Select(s => "x".Equals(s) ? 0 : long.Parse(s)).ToArray();
        }

        public override string AnswerPartOne()
        {
            // find next department times
            var (BusId, WaitingTime) = _potentialBusIds
                .Where(potentialBusId => potentialBusId > 0)
                .Select(potentialBusId =>
                    {
                        return (BusId: potentialBusId, WaitingTime: CalculateWaitingTime(_earliestDepartTime, potentialBusId));
                    }
                )
                .OrderBy(busInfo => busInfo.WaitingTime)
                .First();
            var answer = BusId * WaitingTime;
            return $"{answer}";
        }

        private static long CalculateWaitingTime(long earliestTime, long busId)
        {
            var modulo = earliestTime % busId;
            return modulo == 0 || earliestTime == 0 ? 0 : busId - modulo;
        }

        public override string AnswerPartTwo()
        {
            // To see if the examples match, use these values
            //var potentialBusIds = ExtractBusIds("17,x,13,19");
            //var potentialBusIds = ExtractBusIds("67,7,59,61");
            //var potentialBusIds = ExtractBusIds("67,x,7,59,61");
            //var potentialBusIds = ExtractBusIds("67,7,x,59,61");
            //var potentialBusIds = ExtractBusIds("1789,37,47,1889");
            var potentialBusIds = _potentialBusIds;

            long time = 0;
            long increment = 1;

            var answer = potentialBusIds
                .Select((id, index) => (Id: id, Offset: index))
                .Where(bus => bus.Id != 0)
                .OrderBy(bus => bus.Offset)
                .Select((bus,index) =>
                {
                    // Skip first
                    if (index == 0)
                    {
                        increment = bus.Id;
                        return 0;
                    }
                    long remainder = 0;
                    do
                    {
                        time += increment;
                        remainder = (time + bus.Offset) % bus.Id;
                    } while (remainder != 0);
                    increment *= bus.Id;
                    return time;
                }).Last();

            return $"{answer}";
        }

    }
}
