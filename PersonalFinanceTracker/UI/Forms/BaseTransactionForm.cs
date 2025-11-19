using PersonalFinanceTracker.Core.Data;
using PersonalFinanceTracker.UI.Forms;

//Ivan Georgiev F114584
namespace PersonalFinanceTracker
{
    /// <summary>
    /// Base form for displaying financial transactions
    /// </summary>
    public partial class BaseTransactionForm : Form
    {
        protected ICollection<Transaction> transactions;
        protected DataGridView dataGridView;
        protected ToolStrip toolStrip;
        protected Label lblSummary;
        protected TransactionType formType;

        /// <summary>
        /// Initializes the base transaction form with specified transaction type
        /// </summary>
        /// <param name="type"></param>
        public BaseTransactionForm(TransactionType type)
        {
            formType = type;
            transactions = Account.Instance.Transactions;
            InitializeComponent();
            SetupDataGridView();
            UpdateSummary();
        }

        /// <summary>
        /// Sets up the basic form components and layout
        /// </summary>
        protected virtual void InitializeComponent()
        {
            this.Text = $"Transactions - {formType}";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            toolStrip = new ToolStrip();
            toolStrip.Location = new Point(0, 0);
            toolStrip.Size = new Size(800, 40);

            lblSummary = new Label();
            lblSummary.Location = new Point(20, 45);
            lblSummary.Size = new Size(400, 20);
            lblSummary.Font = new Font("Arial", 9, FontStyle.Bold);
            lblSummary.ForeColor = Color.DarkBlue;

            dataGridView = new DataGridView();
            dataGridView.Location = new Point(20, 70);
            dataGridView.Size = new Size(740, 350);
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.RowHeadersVisible = false;

            this.Controls.Add(toolStrip);
            this.Controls.Add(lblSummary);
            this.Controls.Add(dataGridView);
        }

        /// <summary>
        /// Configures the data grid view columns and styling
        /// </summary>
        protected virtual void SetupDataGridView()
        {
            dataGridView.Columns.Clear();

            dataGridView.Columns.Add("Date", "Date");
            dataGridView.Columns.Add("Amount", "Amount");
            dataGridView.Columns.Add("Category", "Category");
            dataGridView.Columns.Add("Description", "Description");
            dataGridView.Columns.Add("Id", "ID");

            dataGridView.Columns["Date"].Width = 100;
            dataGridView.Columns["Amount"].Width = 100;
            dataGridView.Columns["Category"].Width = 120;
            dataGridView.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["Id"].Visible = false;

            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 9);
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
        }

        /// <summary>
        /// Loads transactions into the data grid view
        /// </summary>
        protected virtual void LoadTransactions()
        {
            RefreshDataGrid();
        }

        /// <summary>
        /// Refreshes the data grid view with current transaction data
        /// </summary>
        protected virtual void RefreshDataGrid()
        {
            dataGridView.Rows.Clear();

            foreach (var transaction in transactions.Where(t => t.GetType().ToString().Contains(formType.ToString())))
            {
                string amountDisplay = formType == TransactionType.Income
                    ? $"+${transaction.Amount:F2}"
                    : $"-${transaction.Amount:F2}";

                dataGridView.Rows.Add(
                    transaction.Date.ToString("MM/dd/yyyy"),
                    amountDisplay,
                    transaction.GetCategory(),
                    transaction.Description,
                    transaction.Id
                );

                var row = dataGridView.Rows[dataGridView.Rows.Count - 1];
                row.Cells["Amount"].Style.ForeColor = formType == TransactionType.Income ? Color.Green : Color.Red;
                row.Cells["Amount"].Style.Font = new Font("Arial", 9, FontStyle.Bold);
            }

            UpdateSummary();
        }

        /// <summary>
        /// Updates the summary information display
        /// </summary>
        protected virtual void UpdateSummary()
        {
            var filteredTransactions = transactions.Where(t => t.GetType().ToString().Contains(formType.ToString())).ToList();
            decimal total = filteredTransactions.Sum(t => t.Amount);
            int count = filteredTransactions.Count;

            lblSummary.Text = $"{formType}s: {count} transactions | Total: ${total:F2}";
            lblSummary.ForeColor = formType == TransactionType.Income ? Color.Green : Color.Red;
        }

        /// <summary>
        /// Adds a new transaction to the collection and refreshes the display
        /// </summary>
        /// <param name="transaction"></param>
        public void AddTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
            RefreshDataGrid();
        }

        /// <summary>
        /// Retrieves all transactions of the current form type
        /// </summary>
        /// <returns></returns>
        public List<Transaction> GetTransactions()
        {
            return transactions.Where(t => t.GetType().ToString().Contains(formType.ToString())).ToList();
        }

        /// <summary>
        /// Handles the form load event to initialize transaction data
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadTransactions();
        }
    }
}