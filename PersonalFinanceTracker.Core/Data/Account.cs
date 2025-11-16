namespace PersonalFinanceTracker.Core.Data
{
    public class Account
    {
        private static Account _instance;
        private static readonly object _lock = new object();
        private Account()
        {
            Transactions = new List<Transaction>();
        }

        public static Account Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null) _instance = new Account();
                    }
                }

                return _instance;
            }
        }

        public decimal Balance { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
