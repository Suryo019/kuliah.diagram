using diagram.Services;

namespace diagram
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            DbHelper.Initialize("localhost", 5432, "postgres", "indra1978", "sistem_basis_data", "public");
            Application.Run(new Form1());
        }
    }
}