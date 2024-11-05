using LibraryCoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryCoreApp.Services
{
    public class TransactionService : ITransactionService
    {
        AppDbContext _db;
        public TransactionService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<List<Transaction>> GetRequestList()
        {
            return await _db.Transactions.Where(y => y.IsRequested && y.Borrow == false && y.IsRejected == false && y.Return == false).OrderByDescending(x => x.Id).ToListAsync(); 
        }

        public async Task<List<Transaction>> GetRequestList(int UserId)
        {
            return await _db.Transactions.Where(y => y.UserId == UserId && y.IsRequested && y.Borrow == false && y.IsRejected == false && y.Return == false).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<List<Transaction>> GetRejectedList()
        {           
            return await _db.Transactions.Where(y => y.IsRequested && y.IsRejected && y.Return == false).OrderByDescending(x => x.Id).ToListAsync();
        }
        public async Task<List<Transaction>> GetRejectedList(int UserId)
        {
            return await _db.Transactions.Where(y => y.UserId == UserId && y.IsRequested && y.IsRejected && y.Return == false).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<List<Transaction>> GetBorrowList()
        {
            return await _db.Transactions.Where(y => y.Borrow && y.Return == false).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<List<Transaction>> GetBorrowList(int UserId)
        {
            return await _db.Transactions.Where(y => y.UserId == UserId && y.Borrow && y.Return == false).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<Transaction> AcceptTrans(int TransId)
        {
            Transaction transaction = await _db.Transactions.FindAsync(TransId);
            transaction.Borrow = true;
            transaction.TransDate = DateTime.Now; 
            _db.Transactions.Update(transaction);
            await _db.SaveChangesAsync();
            return transaction;
        }
        public async Task<Transaction> RejectTrans(int TransId)
        {
            Transaction transaction = await _db.Transactions.FindAsync(TransId);
            transaction.IsRejected = true;
            transaction.TransDate = DateTime.Now;
            _db.Transactions.Update(transaction);
            await _db.SaveChangesAsync();
            return transaction;
        }

        public async Task<Transaction> RequestBook(int BookId,int UserId)
        {
            Books book = await _db.BooksData.FindAsync(BookId);
            User user = await _db.Users.FindAsync(UserId);
            Transaction transaction = new Transaction();
            if (book != null && user != null)
            {
                transaction.TransDate= DateTime.Now;
                transaction.BookId = BookId;
                transaction.UserId = UserId;
                transaction.IsRequested = true;
                transaction.Name= user.Name;
                transaction.BookTitle = book.Title;
                _db.Transactions.Add(transaction);
                await _db.SaveChangesAsync();
            }
            return transaction;
        }

        public async Task<Transaction> ReturnBook(int TransId)
        {
            Transaction transaction = await _db.Transactions.FindAsync(TransId);
            if (transaction != null)
            {
                transaction.TransDate = DateTime.Now;
                transaction.Return = true;
                _db.Transactions.Update(transaction);
                await _db.SaveChangesAsync();
            }
            return transaction;
        }

        public async Task<List<Transaction>> GetReturnList()
        {
            return await _db.Transactions.Where(y => y.Return).OrderByDescending(x => x.Id).ToListAsync(); 
        }
    }
}
