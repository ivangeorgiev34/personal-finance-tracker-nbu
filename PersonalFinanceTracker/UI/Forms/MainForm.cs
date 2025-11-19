using PersonalFinanceTracker.Core.Data;
using PersonalFinanceTracker.Core.Data.Enums;
using PersonalFinanceTracker.Core.Utils;
using PersonalFinanceTracker.UI.Controls;
using PersonalFinanceTracker.UI.Forms;

//Ivan Antonov Georgiev F114584
namespace PersonalFinanceTracker
{
    /// <summary>
    /// Main application form that serves as the primary interface for the personal finance tracker
    /// </summary>
    public partial class MainForm : Form
    {
        private TableLayoutPanel tlpSummaryCards;
        private BalanceCard balanceCard;
        private IncomeCard incomeCard;
        private ExpenseCard expenseCard;

        /// <summary>
        /// Initializes the main form with header toolbar and summary cards section
        /// </summary>
        public MainForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1280, 720);

            InitializeComponent();
            CreateHeaderToolbar();
            CreateSummaryCardsSection();
        }

        /// <summary>
        /// Creates the header toolbar with application title and new transaction button
        /// </summary>
        private void CreateHeaderToolbar()
        {
            Panel headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 70;
            headerPanel.BackColor = Color.White;
            headerPanel.Padding = new Padding(10, 10, 10, 10);
            TableLayoutPanel layout = new TableLayoutPanel();
            layout.Dock = DockStyle.Fill;
            layout.ColumnCount = 3;
            layout.RowCount = 1;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            PictureBox icon = new PictureBox();
            icon.Image = Properties.Resources.euro;
            icon.SizeMode = PictureBoxSizeMode.StretchImage;
            icon.Width = 40;
            icon.Height = 40;
            icon.Margin = new Padding(0, 0, 10, 0);

            Label title = new Label();
            title.Text = "Personal Finance Tracker";
            title.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            title.ForeColor = Color.Black;
            title.AutoSize = true;

            FlowLayoutPanel leftGroup = new FlowLayoutPanel();
            leftGroup.AutoSize = true;
            leftGroup.WrapContents = false;
            leftGroup.Controls.Add(icon);
            leftGroup.Controls.Add(title);

            Button btnNew = new Button();
            btnNew.Text = "New Transaction";
            btnNew.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnNew.Height = 40;
            btnNew.Width = 180;
            btnNew.BackColor = Color.RoyalBlue;
            btnNew.ForeColor = Color.White;
            btnNew.FlatStyle = FlatStyle.Flat;
            btnNew.FlatAppearance.BorderSize = 0;
            btnNew.Click += BtnAddTransaction_Click;

            layout.Controls.Add(leftGroup, 0, 0);
            layout.Controls.Add(new Panel(), 1, 0);
            layout.Controls.Add(btnNew, 2, 0);

            headerPanel.Controls.Add(layout);

            this.Controls.Add(headerPanel);
        }

        /// <summary>
        /// Creates the summary cards section displaying balance, income, and expense information
        /// </summary>
        private void CreateSummaryCardsSection()
        {
            tlpSummaryCards = new TableLayoutPanel
            {
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(20, 40, 20, 10),
                BackColor = Color.FromArgb(249, 250, 251),
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                AutoSize = true,
                Dock = DockStyle.Bottom,
                AutoScroll = true,

            };

            tlpSummaryCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpSummaryCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpSummaryCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            balanceCard = new BalanceCard();
            incomeCard = new IncomeCard();
            expenseCard = new ExpenseCard();

            tlpSummaryCards.Controls.Add(balanceCard, 0, 0);
            tlpSummaryCards.Controls.Add(incomeCard, 1, 0);
            tlpSummaryCards.Controls.Add(expenseCard, 2, 0);

            this.Controls.Add(tlpSummaryCards);
        }

        /// <summary>
        /// Updates the summary cards with current financial data
        /// </summary>
        /// <param name="balance"></param>
        /// <param name="income"></param>
        /// <param name="expenses"></param>
        public void UpdateSummaryCards(decimal balance, decimal income, decimal expenses)
        {
            balanceCard.SetBalance(balance);
            incomeCard.SetIncome(income);
            expenseCard.SetExpenses(expenses);
        }

        /// <summary>
        /// Refreshes the dashboard by recalculating and updating all financial summaries
        /// </summary>
        public void RefreshDashboard()
        {
            var balance = Account.Instance.Balance;
            var income = Account.Instance.Transactions.OfType<Income>().Sum(x => x.Amount);
            var expense = Account.Instance.Transactions.OfType<Expense>().Sum(x => x.Amount);
            UpdateSummaryCards(balance, income, expense);
        }

        /// <summary>
        /// Handles the click event for adding a new transaction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddTransaction_Click(object sender, EventArgs e)
        {
            using (var addForm = new AddTransactionForm())
            {
                var result = addForm.ShowDialog();

                if (result == DialogResult.OK && addForm.IsSaved)
                {
                    Transaction transaction;
                    if (addForm.TransactionType == TransactionType.Income
                        && Enum.TryParse(addForm.Category, true, out IncomeCategory incomeCategory))
                    {
                        transaction = IncomeFactory.Create(addForm.Amount, addForm.Description, addForm.Date, incomeCategory);
                    }
                    else if (addForm.TransactionType == TransactionType.Expense
                        && Enum.TryParse(addForm.Category, true, out ExpenseCategory expenseCategory))
                    {
                        transaction = ExpenseFactory.Create(addForm.Amount, addForm.Description, addForm.Date, expenseCategory);
                    }
                    else
                    {
                        throw new InvalidOperationException("Could not save transaction");
                    }


                    SaveTransaction(transaction);
                    RefreshDashboard();

                    MessageBox.Show("Transaction added successfully!", "Success",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// Saves a transaction to the account's transaction collection
        /// </summary>
        /// <param name="transaction"></param>
        private void SaveTransaction(Transaction transaction)
        {
            Account.Instance.Transactions.Add(transaction);
        }
    }
}