using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace AdventOfCode.Solutions
{
    public class Day3 : AdventOfCodeBase
    {
        private readonly char[][]_maze;

        public Day3()
        {
            Assert.True(File.Exists(InputFilename));

            // Read file into maze
            var mazeFile = File.ReadAllLines(this.InputFilename);
            _maze = new char[mazeFile.Length][];

            int line = 0;
            foreach(var mazeLine in mazeFile)
            {
                _maze[line++] = mazeLine.ToCharArray();
            }
        }

        private int CountTrees(int stepX, int stepY)
        {
            int x = 0;
            int y = 0;
            int answer = 0;
            int modulo = _maze[0].Length;

            x += stepX;
            y += stepY;
            while (y < _maze.Length)
            {
                if (_maze[y][x % modulo] == '#')
                {
                    answer++;
                }
                x += stepX;
                y += stepY;
            }
            return answer;
        }

        public override string AnswerPartOne()
        {
            int answer = CountTrees(3, 1);
            return $"{answer}";
        }

        public override string AnswerPartTwo()
        {
            var countResults = new int[] {
                CountTrees(1, 1),
                CountTrees(3, 1),
                CountTrees(5, 1),
                CountTrees(7, 1),
                CountTrees(1, 2)
            };
            long answer = 1;
            foreach(int countResult in countResults)
            {
                answer *= countResult;
            }
            return $"{answer}";
        }

    }
}
