using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day14
{
    public class DockingProgram : IDay
    {
        public int DayNumber => 14;
        public string ValidatedPart1 => "13496669152158";
        public string ValidatedPart2 => string.Empty;

        private readonly List<IStatement> _statements;


        private DockingProgram(List<IStatement> statements)
        {
            _statements = statements;
        }

        public static DockingProgram LoadFromFile(string filename)
        {
            var lines = File.ReadAllLines(filename)
                .Select(ParseLine)
                .ToList();

            return new DockingProgram(lines);
        }

        private static IStatement ParseLine(string line)
        {
            if (line.StartsWith("mask"))
            {
                return new UpdateBitmask(line);
            }

            return new MemoryAssignment(line);
        }

        public string Part1()
        {
            var state = new ProgramState();
            state.Execute(_statements);
            var result = state.MemorySum();
            return result.ToString();
        }

        public string Part2()
        {
            return string.Empty;
        }

        private interface IStatement
        {
            void Execute(ProgramState programState);
        }

        private class UpdateBitmask : IStatement
        {
            private char[] _bitmask { get; set; }

            public UpdateBitmask(string statement)
            {
                _bitmask = statement
                    .Substring("mask = ".Length)
                    .Reverse()
                    .ToArray();
            }

            public void Execute(ProgramState programState)
            {
                programState.SetBitmask(_bitmask);
            }
        }

        private class MemoryAssignment : IStatement
        {
            private int _address;
            private ulong _value;
            public MemoryAssignment(string statement)
            {
                var split = statement.Split("] = ");
                _value = ulong.Parse(split[1]);
                _address = int.Parse(split[0].Substring("mem[".Length));
            }

            public void Execute(ProgramState programState)
            {
                programState.SetMemory(_address, _value);
            }
        }

        private class ProgramState
        {
            private char[] _bitmask;
            private Dictionary<int, ulong> _memory = new Dictionary<int, ulong>();

            public ulong MemorySum()
            {
                ulong total = 0;
                foreach (var value in _memory.Values)
                {
                    total += value;
                }
                return total;
            }

            public void SetBitmask(char[] bitmask)
            {
                _bitmask = bitmask;
            }

            private ulong ApplyBitMask(ulong input)
            {
                ulong orMask = 1;
                foreach (var c in _bitmask)
                {
                    switch (c)
                    {
                        case '0':
                            input = input & ~orMask;
                            break;
                        case '1':
                            input = input | orMask;
                            break;
                    }
                    orMask <<= 1;
                }

                return input;
            }

            public void SetMemory(int address, ulong value)
            {
                var maskedValue = ApplyBitMask(value);

                _memory[address] = maskedValue;
            }

            public void Execute(IEnumerable<IStatement> statements)
            {
                foreach(var statement in statements)
                {
                    statement.Execute(this);
                }
            }
        }
    }
}
