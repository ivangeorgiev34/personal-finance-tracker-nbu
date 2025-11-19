using System.Drawing.Drawing2D;

//Ivan Antonov Georgiev F114584
namespace PersonalFinanceTracker.UI.Controls
{
    /// <summary>
    /// Abstract base class for summary card controls with rounded styling
    /// </summary>
    public abstract class SummaryCard : Panel
    {
        protected Label lblTitle;
        protected Label lblValue;
        protected PictureBox pbIcon;
        protected Panel iconBackground;

        public string Title { get; set; }
        public string Value { get; set; } = "$0.00";
        public Image Icon { get; set; }
        public Color IconBackColor { get; set; }

        /// <summary>
        /// Initializes the summary card base class
        /// </summary>
        protected SummaryCard()
        {
        }

        /// <summary>
        /// Creates and configures all card controls and layout
        /// </summary>
        protected void InitializeComponent()
        {
            this.Size = new Size(360, 180);
            this.BackColor = Color.White;
            this.Padding = new Padding(40);
            this.Margin = new Padding(0, 50, 0, 0);

            iconBackground = new Panel
            {
                Size = new Size(48, 48),
                Location = new Point(20, 20),
                BackColor = Color.FromArgb(219, 234, 254)
            };

            pbIcon = new PictureBox
            {
                Size = new Size(24, 24),
                Location = new Point(12, 12),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            lblTitle = new Label
            {
                Text = Title,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(107, 114, 128),
                AutoSize = true,
                Location = new Point(20, 75)
            };

            lblValue = new Label
            {
                Text = Value,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55),
                AutoSize = true,
                Location = new Point(20, 92)
            };

            iconBackground.Controls.Add(pbIcon);
            this.Controls.Add(iconBackground);
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblValue);
        }

        /// <summary>
        /// Applies visual styles and rounded corners to the card
        /// </summary>
        protected void SetupStyles()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, iconBackground.Width - 1, iconBackground.Height - 1);
            iconBackground.Region = new Region(path);

            this.Paint += SummaryCard_Paint;
        }

        /// <summary>
        /// Handles custom painting for rounded card corners
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SummaryCard_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            int radius = 12;

            using (GraphicsPath path = GetRoundedRectPath(rect, radius))
            {
                using (SolidBrush brush = new SolidBrush(this.BackColor))
                {
                    g.FillPath(brush, path);
                }

                using (Pen pen = new Pen(Color.FromArgb(229, 231, 235), 1))
                {
                    g.DrawPath(pen, path);
                }
            }
        }

        /// <summary>
        /// Creates a rounded rectangle graphics path
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        /// <summary>
        /// Handles resize events to refresh card appearance
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate();
        }

        /// <summary>
        /// Formats decimal amount as currency string
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        protected string FormatCurrency(decimal amount)
        {
            return amount.ToString("F2");
        }

        /// <summary>
        /// Updates card data
        /// </summary>
        /// <param name="data"></param>
        public abstract void UpdateData(params object[] data);

        /// <summary>
        /// Handles card click events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected abstract void Card_Click(object sender, EventArgs e);
    }
}