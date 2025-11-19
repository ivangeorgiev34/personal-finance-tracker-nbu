using PersonalFinanceTracker.Core.Data;

//Ivan Georgiev F114584
namespace PersonalFinanceTracker.UI.Forms
{
    /// <summary>
    /// Form for displaying complete financial balance including all transactions
    /// </summary>
    public partial class BalanceForm : BaseTransactionForm
    {
        private Label lblIncomeTotal;
        private Label lblExpenseTotal;
        private Label lblNetBalance;

        /// <summary>
        /// Initializes the balance form for displaying all transactions
        /// </summary>
        public BalanceForm() : base(TransactionType.Income) { }

        /// <summary>
        /// Customizes the form components for balance display
        /// </summary>
        protected override void InitializeComponent()
        {
            base.InitializeComponent();
            this.Text = "Balance - All Transactions";
            CustomizeBalanceForm();
            AddBalanceSummaryControls();
        }

        /// <summary>
        /// Applies balance-specific styling to the form
        /// </summary>
        private void CustomizeBalanceForm()
        {
            this.BackColor = Color.LightGray;
            dataGridView.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.Size = new Size(900, 550);
        }

        /// <summary>
        /// Adds detailed balance summary controls to the form
        /// </summary>
        private void AddBalanceSummaryControls()
        {
            this.Controls.Remove(lblSummary);

            var summaryPanel = new Panel();
            summaryPanel.Location = new Point(20, 45);
            summaryPanel.Size = new Size(850, 60);
            summaryPanel.BorderStyle = BorderStyle.FixedSingle;
            summaryPanel.BackColor = Color.LightSteelBlue;

            lblIncomeTotal = new Label();
            lblIncomeTotal.Location = new Point(10, 10);
            lblIncomeTotal.Size = new Size(250, 20);
            lblIncomeTotal.Font = new Font("Arial", 9, FontStyle.Bold);
            lblIncomeTotal.ForeColor = Color.Green;

            lblExpenseTotal = new Label();
            lblExpenseTotal.Location = new Point(270, 10);
            lblExpenseTotal.Size = new Size(250, 20);
            lblExpenseTotal.Font = new Font("Arial", 9, FontStyle.Bold);
            lblExpenseTotal.ForeColor = Color.Red;

            lblNetBalance = new Label();
            lblNetBalance.Location = new Point(550, 10);
            lblNetBalance.Size = new Size(300, 20);
            lblNetBalance.Font = new Font("Arial", 10, FontStyle.Bold);

            summaryPanel.Controls.Add(lblIncomeTotal);
            summaryPanel.Controls.Add(lblExpenseTotal);
            summaryPanel.Controls.Add(lblNetBalance);

            dataGridView.Location = new Point(20, 120);
            dataGridView.Size = new Size(850, 350);

            this.Controls.Add(summaryPanel);
        }

        /// <summary>
        /// Configures the data grid view with balance-specific columns
        /// </summary>
        protected override void SetupDataGridView()
        {
            dataGridView.Columns.Clear();

            dataGridView.Columns.Add("Type", "Type");
            dataGridView.Columns.Add("Date", "Date");
            dataGridView.Columns.Add("Amount", "Amount");
            dataGridView.Columns.Add("Category", "Category");
            dataGridView.Columns.Add("Description", "Description");
            dataGridView.Columns.Add("Id", "ID");

            dataGridView.Columns["Type"].Width = 80;
            dataGridView.Columns["Date"].Width = 90;
            dataGridView.Columns["Amount"].Width = 100;
            dataGridView.Columns["Category"].Width = 120;
            dataGridView.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["Id"].Visible = false;

            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 9);
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
        }

        /// <summary>
        /// Loads all transactions into the data grid view
        /// </summary>
        protected override void LoadTransactions()
        {
            RefreshDataGrid();
        }

        /// <summary>
        /// Refreshes the data grid view with all transactions sorted by date
        /// </summary>
        protected override void RefreshDataGrid()
        {
            dataGridView.Rows.Clear();

            var sortedTransactions = transactions.OrderByDescending(t => t.Date).ToList();

            foreach (var transaction in sortedTransactions)
            {
                string amountDisplay = transaction is Income
                    ? $"+${transaction.Amount:F2}"
                    : $"-${transaction.Amount:F2}";

                string typeDisplay = transaction is Income ? "Income" : "Expense";

                dataGridView.Rows.Add(
                    typeDisplay,
                    transaction.Date.ToString("MM/dd/yyyy"),
                    amountDisplay,
                    transaction.GetCategory(),
                    transaction.Description,
                    transaction.Id
                );

                var row = dataGridView.Rows[dataGridView.Rows.Count - 1];

                if (transaction is Income)
                {
                    row.Cells["Type"].Style.ForeColor = Color.Green;
                    row.Cells["Type"].Style.Font = new Font("Arial", 9, FontStyle.Bold);
                    row.Cells["Amount"].Style.ForeColor = Color.Green;
                    row.Cells["Amount"].Style.Font = new Font("Arial", 9, FontStyle.Bold);
                }
                else
                {
                    row.Cells["Type"].Style.ForeColor = Color.Red;
                    row.Cells["Type"].Style.Font = new Font("Arial", 9, FontStyle.Bold);
                    row.Cells["Amount"].Style.ForeColor = Color.Red;
                    row.Cells["Amount"].Style.Font = new Font("Arial", 9, FontStyle.Bold);
                }
            }

            UpdateSummary();
        }

        /// <summary>
        /// Updates the balance summary with current financial totals
        /// </summary>
        protected override void UpdateSummary()
        {
            var incomeTransactions = transactions.Where(t => t is Income).ToList();
            var expenseTransactions = transactions.Where(t => t is Expense).ToList();

            decimal incomeTotal = incomeTransactions.Sum(t => t.Amount);
            decimal expenseTotal = expenseTransactions.Sum(t => t.Amount);
            decimal netBalance = Account.Instance.Balance;

            lblIncomeTotal.Text = $"Total Income: ${incomeTotal:F2}";
            lblExpenseTotal.Text = $"Total Expenses: ${expenseTotal:F2}";
            lblNetBalance.Text = $"Net Balance: ${netBalance:F2}";

            lblNetBalance.ForeColor = netBalance >= 0 ? Color.Green : Color.Red;
        }

        /// <summary>
        /// Adds a new transaction and refreshes the display
        /// </summary>
        /// <param name="transaction"></param>
        public new void AddTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
            RefreshDataGrid();
        }

        /// <summary>
        /// Retrieves the complete financial summary
        /// </summary>
        /// <returns></returns>
        public (decimal incomeTotal, decimal expenseTotal, decimal netBalance) GetFinancialSummary()
        {
            var incomeTotal = transactions.Where(t => t is Income).Sum(t => t.Amount);
            var expenseTotal = transactions.Where(t => t is Expense).Sum(t => t.Amount);
            var netBalance = incomeTotal - expenseTotal;

            return (incomeTotal, expenseTotal, netBalance);
        }
    }
}