using LibraryCoreApp.Models;

namespace LibraryCoreApp.Services
{
    public interface IUserService
    {
        Task<User> GetUser(int Id);
        Task<User> GetUser(string Email, string Password);
        Task<List<User>> GetUsers();
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUser(int UserId);
    }
}
