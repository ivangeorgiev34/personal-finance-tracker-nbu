using PersonalFinanceTracker.Core.Data;
using PersonalFinanceTracker.Core.Data.Enums;

//Ivan Georgiev F114584
namespace PersonalFinanceTracker.Core.Services.Contracts
{
    /// <summary>
    /// Account service interface
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Adds income to the account
        /// </summary>
        Transaction AddIncome(decimal amount, string description, DateTime date, IncomeCategory category);

        /// <summary>
        /// Add expense to the account
        /// </summary>
        Transaction AddExpense(decimal amount, string description, DateTime date, ExpenseCategory category);
    }
}
