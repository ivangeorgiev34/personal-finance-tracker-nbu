namespace PersonalFinanceTracker.Core.Data
{
    public abstract class Transaction
    {
        protected Transaction(decimal amount, string description, DateTime date)
        {
            this.Amount = amount;
            this.Description = description;
            this.Date = date;
        }
        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

    }
}
