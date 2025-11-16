using PersonalFinanceTracker.Core.Data;
using PersonalFinanceTracker.Core.Data.Enums;

namespace PersonalFinanceTracker.Core.Utils
{
    public class IncomeFactory
    {
        public static Transaction Create(decimal amount, string description, DateTime date, IncomeCategory category) => new Income(amount, description, date, category);
    }
}
