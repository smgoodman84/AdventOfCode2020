using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day08
{
    public class GameConsole : IDay
    {
        public int DayNumber => 8;
        public string ValidatedPart1 => "1420";
        public string ValidatedPart2 => "1245";

        private Instruction[] _program;
        private int _instructionPointer;
        private int _accumulator;

        private GameConsole(Instruction[] program)
        {
            _program = program;
            File.Delete("Trace.txt");
            Reset();
        }

        public static GameConsole LoadFromFile(string filename)
        {
            var lines = File.ReadAllLines(filename)
                .Select(ParseLine)
                .ToArray();

            return new GameConsole(lines);
        }

        private static Instruction ParseLine(string instruction)
        {
            return new Instruction(instruction);
        }

        private void Reset()
        {
            _instructionPointer = 0;
            _accumulator = 0;
        }

        private void ExecuteInstruction()
        {
            var ip = _instructionPointer;
            var instruction = _program[_instructionPointer];
            switch (instruction.Operation)
            {
                case "nop":
                    _instructionPointer += 1;
                    break;
                case "acc":
                    _instructionPointer += 1;
                    _accumulator += instruction.Argument;
                    break;
                case "jmp":
                    _instructionPointer += instruction.Argument;
                    break;
            }
            Trace($"Executed [{ip}]\t{instruction.Operation}\t{instruction.Argument}\t\t{_accumulator}\t{_instructionPointer}");
        }

        private void Trace(string message)
        {
            File.AppendAllLines("Trace.txt", new[] { message });
        }

        public string Part1()
        {
            var executed = new HashSet<int>();
            while (!executed.Contains(_instructionPointer))
            {
                executed.Add(_instructionPointer);
                ExecuteInstruction();
            }

            return _accumulator.ToString();
        }

        public string Part2()
        {
            var flippedInstruction = 0;
            Flip(flippedInstruction);

            while (!Terminates())
            {
                Reset();
                Flip(flippedInstruction);
                flippedInstruction += 1;
                Flip(flippedInstruction);
            }
            return _accumulator.ToString();
        }

        private void Flip(int instructionNumber)
        {
            var instruction = _program[instructionNumber];
            switch (instruction.Operation)
            {
                case "jmp":
                    instruction.Operation = "nop";
                    break;
                case "nop":
                    instruction.Operation = "jmp";
                    break;
            }
        }

        private bool Terminates()
        {
            var executed = new HashSet<int>();
            while (!executed.Contains(_instructionPointer))
            {
                executed.Add(_instructionPointer);
                ExecuteInstruction();
                if (_instructionPointer >= _program.Length)
                {
                    return true;
                }
            }
            return false;
        }

        private class Instruction
        {
            public Instruction(string instruction)
            {
                var split = instruction.Split(" ");
                Operation = split[0].Trim();
                Argument = int.Parse(split[1].Trim().Replace("+", ""));
            }

            public string Operation { get; set; }
            public int Argument { get; set; }
        }
    }
}
