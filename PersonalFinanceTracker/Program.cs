//Ivan Antonov Georgiev F114584
namespace PersonalFinanceTracker
{
    /// <summary>
    /// Main program
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// App starting point
        /// </summary>
        [STAThread]
        public static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}