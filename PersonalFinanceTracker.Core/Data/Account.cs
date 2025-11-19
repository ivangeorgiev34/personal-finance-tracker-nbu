//Ivan Georgiev F114584
namespace PersonalFinanceTracker.Core.Data
{
    /// <summary>
    /// Singleton Account class responsible for manaing the account of the user for the entire app lifetime
    /// </summary>
    public class Account
    {
        private static Account _instance;
        private static readonly object _lock = new object();
        private Account()
        {
            Transactions = new List<Transaction>();
        }

        /// <summary>
        /// Gets singleton instance
        /// </summary>
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