using PersonalFinanceTracker.Core.Data;
using PersonalFinanceTracker.Core.Data.Enums;

//Ivan Georgiev F114584
namespace PersonalFinanceTracker.Core.Utils
{
    /// <summary>
    /// A factory class for creating Income class instances
    /// </summary>
    public class IncomeFactory
    {
        /// <summary>
        /// Method used for creating the new Income class instances
        /// </summary>
        public static Transaction Create(decimal amount, string description, DateTime date, IncomeCategory category) => new Income(amount, description, date, category);
    }
}
