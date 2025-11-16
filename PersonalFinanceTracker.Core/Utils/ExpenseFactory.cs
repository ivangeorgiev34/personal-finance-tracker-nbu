using PersonalFinanceTracker.Core.Data;
using PersonalFinanceTracker.Core.Data.Enums;

namespace PersonalFinanceTracker.Core.Utils
{
    public class ExpenseFactory
    {
        public static Transaction Create(decimal amount, string description, DateTime date, ExpenseCategory category) => new Expense(amount, description, date, category);
    }
}
