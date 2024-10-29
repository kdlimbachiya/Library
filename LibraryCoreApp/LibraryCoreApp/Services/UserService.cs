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
            User user = new User();
            await Task.Run(() =>
            {
                user = _db.Users.FirstOrDefault(x => x.UserId == Id);
            }); 
            return user;
        }
        public async Task<User> GetUser(string Email, string Password)
        {
            User user = new User();
            await Task.Run(() =>
            {
                user = _db.Users.FirstOrDefault(x => x.Email == Email && x.Password == Password);
            });
            return user;
        }
        public async Task<List<User>> GetUsers()
        {
            List<User> users = new List<User>();
            await Task.Run(() =>
            {
                users = _db.Users.OrderByDescending(x => x.UserId).ToList();
            });
            return users;
        }

        public async Task<User> AddUser(User user)
        {
            _db.Users.AddAsync(user);
            _db.SaveChanges();
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            await Task.Run(() =>
            {
                _db.Users.Update(user);
                _db.SaveChanges();
            });
            return user;
        }
        public async Task<bool> DeleteUser(int UserId)
        {
            User user = _db.Users.FirstOrDefault(x => x.UserId == UserId);
            if(user != null)
            {
                await Task.Run(() =>
                {
                    _db.Users.Remove(user);
                    _db.SaveChanges();
                });                
                return true;
            }
            return false;
        }
    }
}
