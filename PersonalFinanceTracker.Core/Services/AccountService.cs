using PersonalFinanceTracker.Core.Data;
using PersonalFinanceTracker.Core.Data.Enums;
using PersonalFinanceTracker.Core.Services.Contracts;
using PersonalFinanceTracker.Core.Utils;

namespace PersonalFinanceTracker.Core.Services
{
    public class AccountService : IAccountService
    {
        public Transaction AddExpense(decimal amount, string description, DateTime date, ExpenseCategory category)
        {
            var expense = ExpenseFactory.Create(amount, description, date, category);

            Account.Instance.Transactions.Add(expense);
            Account.Instance.Balance += amount;

            return expense;
        }

        public Transaction AddIncome(decimal amount, string description, DateTime date, IncomeCategory category)
        {
            var income = IncomeFactory.Create(amount, description, date, category);

            Account.Instance.Transactions.Add(income);
            Account.Instance.Balance += amount;

            return income;
        }
    }
}
