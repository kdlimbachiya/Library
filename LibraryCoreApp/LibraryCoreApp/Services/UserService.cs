using LibraryCoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryCoreApp.Services
{
    public class UserService : IUserService
    {
        AppDbContext _db;
        public IConfiguration _configuration { get; }
        public UserService(AppDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<User> GetUser(int Id)
        {
            //User user = new User();
            //await Task.Run(() =>
            //{
            //    user = _db.Users.FirstOrDefault(x => x.UserId == Id);
            //}); 
            return await _db.Users.SingleOrDefaultAsync(x => x.UserId == Id);
        }
        public async Task<User> GetUser(string Email, string Password)
        {
            return await _db.Users.SingleOrDefaultAsync(x => x.Email == Email && x.Password == Password);
        }
        public async Task<List<User>> GetUsers()
        {
            return await _db.Users.OrderByDescending(x => x.UserId).ToListAsync();
        }

        public async Task<User> AddUser(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;                
        }

        public async Task<User> UpdateUser(User user)
        {
            _db.Entry(user).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return user;
        }
        public async Task<bool> DeleteUser(int UserId)
        {
            User user = await _db.Users.FindAsync(UserId);
            if(user != null)
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
