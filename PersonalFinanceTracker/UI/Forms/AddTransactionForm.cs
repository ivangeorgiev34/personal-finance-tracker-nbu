using PersonalFinanceTracker.Core.Data;
using PersonalFinanceTracker.Core.Data.Enums;

//Ivan Georgiev F114584
namespace PersonalFinanceTracker.UI.Forms
{
    /// <summary>
    /// Specifies the type of transaction
    /// </summary>
    public enum TransactionType
    {
        Income,
        Expense
    }

    /// <summary>
    /// Form for adding new financial transactions
    /// </summary>
    public partial class AddTransactionForm : Form
    {

        public TransactionType TransactionType { get; private set; }
        public decimal Amount { get; private set; }
        public string Description { get; private set; }
        public DateTime Date { get; private set; }
        public string Category { get; private set; }
        public bool IsSaved { get; private set; }

        private ComboBox comboCategory;

        /// <summary>
        /// Initializes the add transaction form
        /// </summary>
        public AddTransactionForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        /// <summary>
        /// Sets up initial form values and state
        /// </summary>
        private void InitializeForm()
        {
            radioIncome.Checked = true;
            dtpDate.Value = DateTime.Now;
            TransactionType = TransactionType.Income;
            UpdateCategoryComboBox();
        }

        /// <summary>
        /// Creates and configures all form controls and layout
        /// </summary>
        private void InitializeComponent()
        {
            this.Text = "Add New Transaction";
            this.Size = new System.Drawing.Size(450, 350);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.AutoScroll = true;
            this.AutoSize = true;

            var lblTitle = new Label();
            lblTitle.Text = "Add New Transaction";
            lblTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Size = new Size(300, 25);
            lblTitle.ForeColor = Color.DarkBlue;

            var grpTransactionType = new GroupBox();
            grpTransactionType.Text = "Transaction Type";
            grpTransactionType.Location = new Point(20, 70);
            grpTransactionType.Size = new Size(350, 50);

            radioIncome = new RadioButton();
            radioIncome.Text = "Income";
            radioIncome.Location = new Point(20, 20);
            radioIncome.Font = new Font("Arial", 9, FontStyle.Bold);
            radioIncome.ForeColor = Color.Green;
            radioIncome.CheckedChanged += Radio_CheckedChanged;
            radioIncome.AutoSize = true;

            radioExpense = new RadioButton();
            radioExpense.Text = "Expense";
            radioExpense.Location = new Point(150, 20);
            radioExpense.Font = new Font("Arial", 9, FontStyle.Bold);
            radioExpense.ForeColor = Color.Red;
            radioExpense.AutoSize = true;

            grpTransactionType.Controls.Add(radioIncome);
            grpTransactionType.Controls.Add(radioExpense);

            var lblCategory = new Label();
            lblCategory.Text = "Category:";
            lblCategory.Location = new Point(20, 130);
            lblCategory.Size = new Size(100, 20);
            lblCategory.Font = new Font("Arial", 9);

            comboCategory = new ComboBox();
            comboCategory.Location = new Point(130, 130);
            comboCategory.Size = new Size(200, 25);
            comboCategory.Font = new Font("Arial", 9);
            comboCategory.DropDownStyle = ComboBoxStyle.DropDownList;

            var lblAmount = new Label();
            lblAmount.Text = "Amount:";
            lblAmount.Location = new Point(20, 170);
            lblAmount.Size = new Size(80, 20);
            lblAmount.Font = new Font("Arial", 9);

            txtAmount = new TextBox();
            txtAmount.Location = new Point(130, 170);
            txtAmount.Size = new Size(150, 25);
            txtAmount.Font = new Font("Arial", 9);
            txtAmount.Text = "0.00";
            txtAmount.TextAlign = HorizontalAlignment.Right;
            txtAmount.Enter += TxtAmount_Enter;

            var lblDescription = new Label();
            lblDescription.Text = "Description:";
            lblDescription.Location = new Point(20, 210);
            lblDescription.Size = new Size(110, 20);
            lblDescription.Font = new Font("Arial", 9);

            txtDescription = new TextBox();
            txtDescription.Location = new Point(130, 210);
            txtDescription.Size = new Size(250, 25);
            txtDescription.Font = new Font("Arial", 9);
            txtDescription.MaxLength = 100;

            var lblDate = new Label();
            lblDate.Text = "Date:";
            lblDate.Location = new Point(20, 250);
            lblDate.Size = new Size(80, 20);
            lblDate.Font = new Font("Arial", 9);

            dtpDate = new DateTimePicker();
            dtpDate.Location = new Point(130, 250);
            dtpDate.Size = new Size(150, 25);
            dtpDate.Format = DateTimePickerFormat.Short;
            dtpDate.Font = new Font("Arial", 9);

            btnSave = new Button();
            btnSave.Text = "💾 Save";
            btnSave.Location = new Point(180, 290);
            btnSave.Size = new Size(105, 35);
            btnSave.BackColor = Color.DodgerBlue;
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Arial", 9, FontStyle.Bold);
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button();
            btnCancel.Text = "❌ Cancel";
            btnCancel.Location = new Point(290, 290);
            btnCancel.Size = new Size(105, 35);
            btnCancel.BackColor = Color.LightGray;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Arial", 9);
            btnCancel.Click += BtnCancel_Click;

            this.Controls.Add(lblTitle);
            this.Controls.Add(grpTransactionType);
            this.Controls.Add(lblCategory);
            this.Controls.Add(comboCategory);
            this.Controls.Add(lblAmount);
            this.Controls.Add(txtAmount);
            this.Controls.Add(lblDescription);
            this.Controls.Add(txtDescription);
            this.Controls.Add(lblDate);
            this.Controls.Add(dtpDate);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
        }

        private RadioButton radioIncome;
        private RadioButton radioExpense;
        private TextBox txtAmount;
        private TextBox txtDescription;
        private DateTimePicker dtpDate;
        private Button btnSave;
        private Button btnCancel;

        /// <summary>
        /// Handles transaction type radio button changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            TransactionType = radioIncome.Checked ? TransactionType.Income : TransactionType.Expense;
            UpdateCategoryComboBox();
            UpdateAmountColor();
        }

        /// <summary>
        /// Updates category combo box based on transaction type
        /// </summary>
        private void UpdateCategoryComboBox()
        {
            comboCategory.Items.Clear();

            if (TransactionType == TransactionType.Income)
            {
                foreach (IncomeCategory category in Enum.GetValues(typeof(IncomeCategory)))
                {
                    comboCategory.Items.Add(FormatCategoryName(category.ToString()));
                }
            }
            else
            {
                foreach (ExpenseCategory category in Enum.GetValues(typeof(ExpenseCategory)))
                {
                    comboCategory.Items.Add(FormatCategoryName(category.ToString()));
                }
            }

            if (comboCategory.Items.Count > 0)
                comboCategory.SelectedIndex = 0;
        }

        /// <summary>
        /// Formats category names for display
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        private string FormatCategoryName(string categoryName)
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();

            for (int i = 0; i < categoryName.Length; i++)
            {
                if (i > 0 && char.IsUpper(categoryName[i]))
                {
                    result.Append(' ');
                }
                result.Append(categoryName[i]);
            }

            return result.ToString();
        }

        /// <summary>
        /// Handles amount text box focus event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtAmount_Enter(object sender, EventArgs e)
        {
            txtAmount.SelectAll();
        }

        /// <summary>
        /// Updates amount text color based on transaction type
        /// </summary>
        private void UpdateAmountColor()
        {
            txtAmount.ForeColor = TransactionType == TransactionType.Income ? Color.Green : Color.Red;
        }

        /// <summary>
        /// Handles save button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                SaveTransaction();
                Account.Instance.Balance += Amount;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// Handles cancel button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Validates all form input fields
        /// </summary>
        /// <returns></returns>
        private bool ValidateInput()
        {
            if (comboCategory.SelectedItem == null)
            {
                ShowError("Please select a category.", comboCategory);
                return false;
            }

            var amountParse = decimal.TryParse(txtAmount.Text, out decimal amount);

            if (!amountParse || TransactionType == TransactionType.Income && amount < 0)
            {
                ShowError("Please enter a valid positive amount.", txtAmount);
                return false;
            }
            else if (!amountParse || TransactionType == TransactionType.Expense && amount > 0)
            {
                ShowError("Please enter a valid negative amount.", txtAmount);
                return false;
            }

            if (amount > 1000000)
            {
                ShowError("Amount seems too large. Please verify.", txtAmount);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                ShowError("Please enter a description.", txtDescription);
                return false;
            }

            if (txtDescription.Text.Trim().Length < 2)
            {
                ShowError("Description should be at least 2 characters long.", txtDescription);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Displays error message and focuses problematic control
        /// </summary>
        /// <param name="message"></param>
        /// <param name="control"></param>
        private void ShowError(string message, Control control)
        {
            MessageBox.Show(message, "Validation Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning);
            control.Focus();
            if (control is TextBox textBox)
                textBox.SelectAll();
        }

        /// <summary>
        /// Saves the transaction data from form fields
        /// </summary>
        private void SaveTransaction()
        {
            Amount = decimal.Parse(txtAmount.Text);
            Description = txtDescription.Text.Trim();
            Date = dtpDate.Value;
            Category = comboCategory.SelectedItem.ToString();
            IsSaved = true;
        }
    }
}