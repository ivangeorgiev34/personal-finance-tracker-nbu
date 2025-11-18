using PersonalFinanceTracker.Core.Data;
using PersonalFinanceTracker.Core.Data.Enums;

namespace PersonalFinanceTracker.Core.Services.Contracts
{
    public interface IAccountService
    {
        Transaction AddIncome(decimal amount, string description, DateTime date, IncomeCategory category);
        Transaction AddExpense(decimal amount, string description, DateTime date, ExpenseCategory category);
    }
}
