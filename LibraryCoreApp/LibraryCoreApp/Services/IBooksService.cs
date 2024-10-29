using LibraryCoreApp.Models;

namespace LibraryCoreApp.Services
{
    public interface IBooksService
    {
        Task<List<Books>> GetAllBooks();
        Task<Books> GetBook(int BookId);
        Task<Books> AddBook(Books book);
        Task<Books> UpdateBook(Books book);
        Task<bool> DeleteBook(int BookId);
    }
}