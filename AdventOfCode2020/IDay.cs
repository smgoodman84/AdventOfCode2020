namespace AdventOfCode2020
{
    public interface IDay
    {
        int DayNumber { get; }
        string Part1();
        string Part2();
        string ValidatedPart1 { get; }
        string ValidatedPart2 { get; }
    }
}
