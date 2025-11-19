using PersonalFinanceTracker.Core.Data;
using PersonalFinanceTracker.Core.Data.Enums;

//Ivan Georgiev F114584
namespace PersonalFinanceTracker.Core.Utils
{
    /// <summary>
    /// A factory class for creating Expense class instances
    /// </summary>
    public class ExpenseFactory
    {
        /// <summary>
        /// Method used for creating the new Expense clas instances
        /// </summary>
        public static Transaction Create(decimal amount, string description, DateTime date, ExpenseCategory category) => new Expense(amount, description, date, category);
    }
}
