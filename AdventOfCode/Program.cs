﻿using AdventOfCode.Solutions;
using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of code answers!");

            for(int day = 1; day < 25; day++)
            {
                var typeForSolution = Type.GetType($"AdventOfCode.Solutions.Day{day}", false);
                if (typeForSolution == null)
                {
                    continue;
                }
                var daySolution = Activator.CreateInstance(typeForSolution) as AdventOfCodeBase;
                Console.WriteLine($"For day {day}");
                Console.WriteLine($"\tthe part one answer is: {daySolution.AnswerPartOne()}");
                Console.WriteLine($"\tthe part two answer is: {daySolution.AnswerPartTwo()}");
            }
            Console.WriteLine("Ready.");
            Console.ReadLine();
        }
    }
}
