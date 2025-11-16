using PersonalFinanceTracker.Core.Data.Enums;

namespace PersonalFinanceTracker.Core.Data
{
    public class Income : Transaction
    {
        public Income(decimal amount, string description, DateTime date, IncomeCategory category) : base(amount, description, date)
        {
            this.Category = category;
        }
        public IncomeCategory Category { get; set; }
    }
}
