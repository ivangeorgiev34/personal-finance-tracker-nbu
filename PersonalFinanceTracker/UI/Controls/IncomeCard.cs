//Ivan Georgiev F114584
namespace PersonalFinanceTracker.UI.Controls
{
    /// <summary>
    /// Summary card control for displaying income information
    /// </summary>
    public class IncomeCard : SummaryCard
    {
        /// <summary>
        /// Initializes the income card with default styling
        /// </summary>
        public IncomeCard()
        {
            Title = "Income";
            IconBackColor = Color.FromArgb(220, 252, 231);
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
                decimal income = Convert.ToDecimal(data[0]);

                Value = FormatCurrency(income);
                lblValue.Text = Value;
            }
        }

        /// <summary>
        /// Sets the income value displayed on the card
        /// </summary>
        /// <param name="income"></param>
        public void SetIncome(decimal income)
        {
            UpdateData(income);
        }

        /// <summary>
        /// Handles card click to open income transactions form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Card_Click(object sender, EventArgs e)
        {
            var form = new IncomeTransactionsForm();
            form.ShowDialog();
        }
    }
}