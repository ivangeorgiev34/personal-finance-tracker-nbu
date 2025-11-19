using PersonalFinanceTracker.Core.Data.Enums;

//Ivan Antonov Georgiev F114584
namespace PersonalFinanceTracker.Core.Data
{
    /// <summary>
    /// Income transaction
    /// </summary>
    public class Income : Transaction
    {
        /// <summary>
        /// Creates income
        /// </summary>
        public Income(decimal amount, string description, DateTime date, IncomeCategory category) : base(amount, description, date)
        {
            this.Category = category;
        }
        public IncomeCategory Category { get; set; }

        /// <summary>
        /// Gets category
        /// </summary>
        /// <returns></returns>
        public override string GetCategory() => this.Category.ToString();
    }
}