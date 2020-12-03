using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day01
{
    public class ExpenseReport : IDay
    {
        public int DayNumber => 1;

        private IEnumerable<int> _expenses;
        public ExpenseReport(IEnumerable<int> expenses)
        {
            _expenses = expenses;
        }

        public static ExpenseReport LoadFromFile(string filename)
        {
            var expenses = File.ReadAllLines(filename)
                .Select(l => int.Parse(l))
                .ToList();

            return new ExpenseReport(expenses);
        }

        public string Part1()
        {
            var sumToFind = 2020;

            var hashedExpenses = new HashSet<int>(_expenses);

            var expenseSums = _expenses.Select(e => sumToFind - e);

            var requiredItem = expenseSums.First(hashedExpenses.Contains);

            var otherItem = sumToFind - requiredItem;

            return (requiredItem * otherItem).ToString();
        }

        public string Part2()
        {
            var sumToFind = 2020;

            foreach (var expense in _expenses)
            {
                var subSumToFind = sumToFind - expense;
                var subExpenses = _expenses.Where(e => e != expense);

                var subResult = FindSumItems(subSumToFind, subExpenses);
                if (subResult.Found)
                {
                    return (subResult.Item1 * subResult.Item2 * expense).ToString();
                }
            }

            throw new Exception("Could not find result");
        }

        private SumItemsResult FindSumItems(int sumToFind, IEnumerable<int> items)
        {
            var hashedItems = new HashSet<int>(items);

            var itemSums = items.Select(e => sumToFind - e);

            foreach (var itemSum in itemSums)
            {
                if (hashedItems.Contains(itemSum))
                {
                    return new SumItemsResult
                    {
                        Found = true,
                        Item1 = itemSum,
                        Item2 = sumToFind - itemSum
                    };
                }
            }

            return new SumItemsResult
            {
                Found = false
            };
        }

        private class SumItemsResult
        {
            public bool Found { get; set; }
            public int Item1 { get; set; }
            public int Item2 { get; set; }
        }
    }
}
