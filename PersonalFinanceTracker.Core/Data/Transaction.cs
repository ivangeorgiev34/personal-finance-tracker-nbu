//Ivan Georgiev F114584
namespace PersonalFinanceTracker.Core.Data
{
    /// <summary>
    /// Base class for transactions
    /// </summary>
    public abstract class Transaction
    {
        protected Transaction(decimal amount, string description, DateTime date)
        {
            this.Amount = amount;
            this.Description = description;
            this.Date = date;
        }
        public int Id { get; set; }
        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public abstract string GetCategory();

    }
}
