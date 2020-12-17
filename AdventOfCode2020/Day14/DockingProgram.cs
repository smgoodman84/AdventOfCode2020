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
        public string ValidatedPart2 => "3278997609887";

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
            var state = new ProgramState(2);
            state.Execute(_statements);
            var result = state.MemorySum();
            return result.ToString();
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
            private ulong _address;
            private ulong _value;
            public MemoryAssignment(string statement)
            {
                var split = statement.Split("] = ");
                _value = ulong.Parse(split[1]);
                _address = ulong.Parse(split[0].Substring("mem[".Length));
            }

            public void Execute(ProgramState programState)
            {
                programState.SetMemory(_address, _value);
            }
        }

        private class ProgramState
        {
            private char[] _bitmask;
            private Dictionary<ulong, ulong> _memory = new Dictionary<ulong, ulong>();
            private readonly int _version;

            public ProgramState(int version = 1)
            {
                _version = version;
            }

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

            public void SetMemory(ulong address, ulong value)
            {
                if (_version == 1)
                {
                    SetMemoryV1(address, value);
                }
                else
                {
                    SetMemoryV2(address, value);
                }
            }

            public void SetMemoryV1(ulong address, ulong value)
            {
                var maskedValue = ApplyBitMask(value);

                _memory[address] = maskedValue;
            }

            public void SetMemoryV2(ulong address, ulong value)
            {
                var maskedAddresses = GetMaskedAddresses(address);
                foreach(var maskedAddress in maskedAddresses)
                {
                    _memory[maskedAddress] = value;
                }
            }

            private List<ulong> GetMaskedAddresses(ulong originalAddress)
            {
                var addresses = new List<ulong>()
                {
                    originalAddress
                };

                ulong orMask = 1;
                foreach (var c in _bitmask)
                {
                    var updatedAddresses = new List<ulong>();
                    foreach(var address in addresses)
                    {
                        switch (c)
                        {
                            case '0':
                                updatedAddresses.Add(address);
                                break;
                            case '1':
                                updatedAddresses.Add(address | orMask);
                                break;
                            case 'X':
                                updatedAddresses.Add(address | orMask);
                                updatedAddresses.Add(address & ~orMask);
                                break;
                        }
                    }
                    addresses = updatedAddresses;

                    orMask <<= 1;
                }

                return addresses;
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
