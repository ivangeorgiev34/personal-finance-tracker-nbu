using PersonalFinanceTracker.Core.Data.Enums;

//Ivan Antonov Georgiev F114584
namespace PersonalFinanceTracker.Core.Data
{
    /// <summary>
    /// Expense transaction
    /// </summary>
    public class Expense : Transaction
    {
        /// <summary>
        /// Creates expense
        /// </summary>
        public Expense(decimal amount, string description, DateTime date, ExpenseCategory category) : base(amount, description, date)
        {
            this.Category = category;
        }
        public ExpenseCategory Category { get; set; }

        /// <summary>
        /// Gets category
        /// </summary>
        /// <returns></returns>
        public override string GetCategory() => this.Category.ToString();
    }
}