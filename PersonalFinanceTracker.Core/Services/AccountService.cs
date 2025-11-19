using PersonalFinanceTracker.Core.Data;
using PersonalFinanceTracker.Core.Data.Enums;
using PersonalFinanceTracker.Core.Services.Contracts;
using PersonalFinanceTracker.Core.Utils;

//Ivan Antonov Georgiev F114584
namespace PersonalFinanceTracker.Core.Services
{
    /// <summary>
    /// A service used for encapsulating the Account related busines logic
    /// </summary>
    public class AccountService : IAccountService
    {
        /// <summary>
        /// Adds expense to the account
        /// </summary>
        public Transaction AddExpense(decimal amount, string description, DateTime date, ExpenseCategory category)
        {
            var expense = ExpenseFactory.Create(amount, description, date, category);

            Account.Instance.Transactions.Add(expense);
            Account.Instance.Balance += amount;

            return expense;
        }

        /// <summary>
        /// Adds income to the account
        /// </summary>
        public Transaction AddIncome(decimal amount, string description, DateTime date, IncomeCategory category)
        {
            var income = IncomeFactory.Create(amount, description, date, category);

            Account.Instance.Transactions.Add(income);
            Account.Instance.Balance += amount;

            return income;
        }
    }
}
