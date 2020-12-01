using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day01
{
    public class ExpenseReport
    {
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

        public int GetResult()
        {
            var sumToFind = 2020;

            var hashedExpenses = new HashSet<int>(_expenses);

            var expenseSums = _expenses.Select(e => sumToFind - e);

            var requiredItem = expenseSums.First(hashedExpenses.Contains);

            var otherItem = sumToFind - requiredItem;

            return requiredItem * otherItem;
        }
    }
}
