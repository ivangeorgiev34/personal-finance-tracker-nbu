using PersonalFinanceTracker.Core.Data.Enums;

namespace PersonalFinanceTracker.Core.Data
{
    public class Expense : Transaction
    {
        public Expense(decimal amount, string description, DateTime date, ExpenseCategory category) : base(amount, description, date)
        {
            this.Category = category;
        }
        public ExpenseCategory Category { get; set; }
    }
}
