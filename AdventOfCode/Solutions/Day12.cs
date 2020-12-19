using System;
using System.IO;
using Xunit;

namespace AdventOfCode.Solutions
{
    public class Day12 : AdventOfCodeBase
    {
        private readonly string[] _instructions;

        public Day12()
        {
            Assert.True(File.Exists(InputFilename));

            // Read file
            _instructions = File.ReadAllLines(this.InputFilename);
        }

        public override string AnswerPartOne()
        {
            // Action N means to move north by the given value.
            // Action S means to move south by the given value.
            // Action E means to move east by the given value.
            // Action W means to move west by the given value.
            // Action L means to turn left the given number of degrees.
            // Action R means to turn right the given number of degrees.
            // Action F means to move forward by the given value in the direction the ship is currently facing.

            int ns = 0;
            int ew = 0;

            int direction = 90;

            foreach (var instruction in _instructions)
            {
                var iSpan = instruction.AsSpan();
                var argument = int.Parse(iSpan[1..]);

                switch (iSpan[0])
                {
                    case 'F':
                        switch ( direction)
                        {
                            case 0:
                                ns += argument;
                                break;
                            case 90:
                                ew += argument;
                                break;
                            case 180:
                                ns -= argument;
                                break;
                            case 270:
                                ew -= argument;
                                break;
                        }
                        break;
                    case 'N':
                        ns += argument;
                        break;
                    case 'S':
                        ns -= argument;
                        break;
                    case 'E':
                        ew += argument;
                        break;
                    case 'W':
                        ew -= argument;
                        break;
                    case 'L':
                        direction -= argument;
                        if (direction < 0)
                        {
                            direction += 360;
                        }
                        break;
                    case 'R':
                        direction += argument;
                        direction %= 360;
                        break;
                    default:
                        throw new Exception(instruction);

                }
            }
            long answer = Math.Abs(ns)+Math.Abs(ew);
            return $"{answer}";
        }

        public override string AnswerPartTwo()
        {
            long answer = 0;
            return $"{answer}";
        }
    }
}
