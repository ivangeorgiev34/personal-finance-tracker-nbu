//Ivan Antonov Georgiev F114584
namespace PersonalFinanceTracker.UI.Controls
{
    /// <summary>
    /// Summary card control for displaying expense information
    /// </summary>
    public class ExpenseCard : SummaryCard
    {
        /// <summary>
        /// Initializes the expense card with default styling
        /// </summary>
        public ExpenseCard()
        {
            Title = "Expenses";
            IconBackColor = Color.FromArgb(254, 226, 226);
            InitializeComponent();
            SetupStyles();

            this.Click += Card_Click;
        }

        /// <summary>
        /// Updates the card data
        /// </summary>
        /// <param name="data"></param>
        public override void UpdateData(params object[] data)
        {
            if (data.Length == 1)
            {
                decimal expenses = Convert.ToDecimal(data[0]);

                Value = FormatCurrency(expenses);
                lblValue.Text = Value;
            }
        }

        /// <summary>
        /// Sets the expense value displayed on the card
        /// </summary>
        /// <param name="expenses"></param>
        public void SetExpenses(decimal expenses)
        {
            UpdateData(expenses);
        }

        /// <summary>
        /// Handles card click to open expense transactions form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Card_Click(object sender, EventArgs e)
        {
            var form = new ExpenseTransactionsForm();
            form.ShowDialog();
        }
    }
}