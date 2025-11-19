using PersonalFinanceTracker.UI.Forms;

//Ivan Georgiev F114584
namespace PersonalFinanceTracker.UI.Controls
{
    /// <summary>
    /// Summary card control for displaying balance information
    /// </summary>
    public class BalanceCard : SummaryCard
    {
        /// <summary>
        /// Initializes the balance card with default styling
        /// </summary>
        public BalanceCard()
        {
            Title = "Total Balance";
            IconBackColor = Color.FromArgb(219, 234, 254);
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
                decimal balance = Convert.ToDecimal(data[0]);

                Value = FormatCurrency(balance);
                lblValue.Text = Value;
            }
        }

        /// <summary>
        /// Sets the balance value displayed on the card
        /// </summary>
        /// <param name="balance"></param>
        public void SetBalance(decimal balance)
        {
            UpdateData(balance);
        }

        /// <summary>
        /// Handles card click to open balance form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Card_Click(object sender, EventArgs e)
        {
            var form = new BalanceForm();
            form.ShowDialog();
        }
    }
}