using PersonalFinanceTracker.UI.Forms;

//Ivan Antonov Georgiev F114584
namespace PersonalFinanceTracker
{
    /// <summary>
    /// Form for displaying and managing expense transactions
    /// </summary>
    public partial class ExpenseTransactionsForm : BaseTransactionForm
    {
        /// <summary>
        /// Initializes the expense transactions form with specific transaction type
        /// </summary>
        public ExpenseTransactionsForm() : base(TransactionType.Expense)
        {
        }

        /// <summary>
        /// Customizes the form components for expense transactions display
        /// </summary>
        protected override void InitializeComponent()
        {
            base.InitializeComponent();
            this.Text = "Expense Transactions";
            CustomizeExpenseForm();
        }

        /// <summary>
        /// Applies expense-specific styling to the form
        /// </summary>
        private void CustomizeExpenseForm()
        {
            this.BackColor = Color.LightYellow;
            dataGridView.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
        }

        /// <summary>
        /// Configures the data grid view with expense-specific formatting
        /// </summary>
        protected override void SetupDataGridView()
        {
            base.SetupDataGridView();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                row.Cells["Amount"].Style.ForeColor = Color.Red;
            }
        }
    }
}