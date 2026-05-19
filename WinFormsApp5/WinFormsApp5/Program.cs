using System.Linq;

namespace WinFormsApp5
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var context = new Data.StoreContext())
            {
                context.Database.EnsureCreated(); // Ensure DB is created
                if (!context.Users.Any(u => u.Username == "admin"))
                {
                    context.Users.Add(new Data.Entities.User
                    {
                        Username = "admin",
                        Password = "admin",
                        Role = Data.Entities.Role.Admin
                    });
                    context.SaveChanges();
                }
            }
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Login());
        }
    }
}