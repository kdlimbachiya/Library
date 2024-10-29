using LibraryCoreApp.Models;

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
            List<Books> books = new List<Books>();
            await Task.Run(() =>
            {
                books = _db.BooksData.OrderByDescending(x => x.DateAdded).ToList();
            });
            return books;
        }

        public async Task<Books> GetBook(int BookId)
        {
            Books book = new Books();
            await Task.Run(() =>
            {
                book = _db.BooksData.FirstOrDefault(x => x.Id == BookId);
            });            
            return book;
        }
        public async Task<Books> AddBook(Books book)
        {
            await Task.Run(() =>
            {
                _db.BooksData.Add(book);
                _db.SaveChanges();
            });
            return book;
        }
        public async Task<Books> UpdateBook(Books book)
        {
            await Task.Run(() =>
            {
                _db.BooksData.Update(book);
                _db.SaveChanges();
            });            
            return book;
        }
        public async Task<bool> DeleteBook(int BookId)
        {
            Books book = _db.BooksData.FirstOrDefault(x => x.Id == BookId);
            if (book != null)
            {
                var transactions = _db.Transactions.Where(x => x.BookId == BookId).ToList();
                if(transactions != null)
                {
                    _db.Transactions.RemoveRange(transactions);
                    _db.SaveChanges();
                }
                _db.BooksData.Remove(book);
                _db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
