using PersonalFinanceTracker.UI.Forms;

//Ivan Georgiev F114584
namespace PersonalFinanceTracker
{
    /// <summary>
    /// Form for displaying and managing income transactions
    /// </summary>
    public partial class IncomeTransactionsForm : BaseTransactionForm
    {
        /// <summary>
        /// Initializes the income transactions form with specific transaction type
        /// </summary>
        public IncomeTransactionsForm() : base(TransactionType.Income)
        {
        }

        /// <summary>
        /// Customizes the form components for income transactions display
        /// </summary>
        protected override void InitializeComponent()
        {
            base.InitializeComponent();
            this.Text = "Income Transactions";
            CustomizeIncomeForm();
        }

        /// <summary>
        /// Applies income-specific styling to the form
        /// </summary>
        private void CustomizeIncomeForm()
        {
            this.BackColor = Color.LightCyan;
            dataGridView.DefaultCellStyle.BackColor = Color.AliceBlue;
        }

        /// <summary>
        /// Configures the data grid view with income-specific formatting
        /// </summary>
        protected override void SetupDataGridView()
        {
            base.SetupDataGridView();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                row.Cells["Amount"].Style.ForeColor = Color.Green;
            }
        }
    }
}