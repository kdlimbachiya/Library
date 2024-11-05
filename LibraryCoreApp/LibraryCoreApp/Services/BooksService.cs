using LibraryCoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryCoreApp.Services
{
    
    public class BooksService : IBooksService
    {
        AppDbContext _db;
        public BooksService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<List<Books>> GetAllBooks()
        {
            return await _db.BooksData.OrderByDescending(x => x.DateAdded).ToListAsync();
        }

        public async Task<Books> GetBook(int BookId)
        {         
            return await _db.BooksData.FindAsync(BookId);
        }
        public async Task<Books> AddBook(Books book)
        {
            _db.BooksData.Add(book);
            await _db.SaveChangesAsync();
            return book;
        }
        public async Task<Books> UpdateBook(Books book)
        {
            //_db.BooksData.Update(book);
            _db.Entry(book).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return book;
        }
        public async Task<bool> DeleteBook(int BookId)
        {
            Books book = await _db.BooksData.SingleOrDefaultAsync(x => x.Id == BookId);
            if (book != null)
            {
                var transactions = await _db.Transactions.Where(x => x.BookId == BookId).ToListAsync();
                if(transactions != null)
                {
                    _db.Transactions.RemoveRange(transactions);
                    await _db.SaveChangesAsync();
                }
                return true;
            }
            return false;
        }
    }
}
