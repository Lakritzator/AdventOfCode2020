using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace AdventOfCode.Solutions
{
    public class Day8 : AdventOfCodeBase
    {
        private readonly (string Opcode, int Argument)[] _opcodes;

        public Day8()
        {
            Assert.True(File.Exists(InputFilename));

            // Read file, every line has "opcode +/-digit"
            _opcodes = File.ReadAllLines(this.InputFilename).Select(line =>
            {
                var split = line.Split(' ');

                return (split[0],int.Parse(split[1]));
            }).ToArray();
        }

        /// <summary>
        /// A simple record for the return value
        /// </summary>
        private record ProgramStats(int Acc, int LastInstruction, bool IsLoop);

        /// <summary>
        /// Process the given program, stop as soon as an instruction is used the second time.
        /// </summary>
        /// <param name="program">Array of opcode and argument</param>
        /// <returns>ProgramStats</returns>
        private ProgramStats RunProgram((string Opcode, int Argument)[] program)
        {
            int acc = 0;
            int instruction = 0;
            var visited = new HashSet<int>();
            bool loopDetected = false;
            while (instruction < program.Length)
            {
                if (visited.Contains(instruction))
                {
                    loopDetected = true;
                    break;
                }
                visited.Add(instruction);
                switch (program[instruction].Opcode)
                {
                    case "nop":
                        instruction++;
                        break;
                    case "acc":
                        acc += program[instruction].Argument;
                        instruction++;
                        break;
                    case "jmp":
                        instruction += program[instruction].Argument;
                        break;
                    default:
                        throw new Exception($"Illegal Opcode {program[instruction].Opcode}");
                }
            }
            return new ProgramStats(acc, instruction, loopDetected);
        }

        public override string AnswerPartOne()
        {
            var stats = RunProgram(_opcodes);
            return $"{stats.Acc}";
        }

        public override string AnswerPartTwo()
        {
            ProgramStats stats;
            int lastJmpModified = _opcodes.Length;
            bool modified;
            do
            {
                var opcodes = _opcodes.ToArray();
                modified = false;
                for (int i = lastJmpModified-1; i > 0; i--)
                {
                    if ("jmp".Equals(opcodes[i].Opcode))
                    {
                        opcodes[i].Opcode = "nop";
                        lastJmpModified = i;
                        modified = true;
                        break;
                    }
                }
                stats = RunProgram(opcodes);
            } while (modified && stats.IsLoop);
            return $"{stats.Acc}";
        }

    }
}
