using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Controllers
{
    public class UserController
    {
        private readonly StoreContext context;

        public UserController(StoreContext context)
        {
            this.context = context;
        }

        public async Task<User?> LoginAsync(string username, string password)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public async Task<User> RegisterAsync(string username, string password)
        {
            var user = new User
            {
                Username = username,
                Password = password,
                Role = Role.User
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }
    }
}
