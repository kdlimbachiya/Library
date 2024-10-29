using LibraryCoreApp.Models;

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
            List<Transaction> transactions = new List<Transaction>();
            await Task.Run(() =>
            {
                transactions = _db.Transactions.Where(y => y.IsRequested && y.Borrow == false && y.IsRejected == false && y.Return == false).OrderByDescending(x => x.Id).ToList();
            });
            return transactions;
        }

        public async Task<List<Transaction>> GetRequestList(int UserId)
        {
            List<Transaction> transactions = new List<Transaction>();
            await Task.Run(() =>
            {
                transactions = _db.Transactions.Where(y =>y.UserId == UserId && y.IsRequested && y.Borrow == false && y.IsRejected == false && y.Return == false).OrderByDescending(x => x.Id).ToList();
            });
            return transactions;
        }

        public async Task<List<Transaction>> GetRejectedList()
        {
            List<Transaction> transactions = new List<Transaction>();
            await Task.Run(() =>
            {
                transactions = _db.Transactions.Where(y => y.IsRequested && y.IsRejected && y.Return == false).OrderByDescending(x => x.Id).ToList();
            });
            return transactions;
        }
        public async Task<List<Transaction>> GetRejectedList(int UserId)
        {
            List<Transaction> transactions = new List<Transaction>();
            await Task.Run(() =>
            {
                transactions = _db.Transactions.Where(y => y.UserId == UserId && y.IsRequested && y.IsRejected && y.Return == false).OrderByDescending(x => x.Id).ToList();
            });
            return transactions;
        }

        public async Task<List<Transaction>> GetBorrowList()
        {
            List<Transaction> transactions = new List<Transaction>();
            await Task.Run(() =>
            {
                transactions = _db.Transactions.Where(y => y.Borrow && y.Return == false).OrderByDescending(x => x.Id).ToList();
            });
            return transactions;
        }

        public async Task<List<Transaction>> GetBorrowList(int UserId)
        {
            List<Transaction> transactions = new List<Transaction>();
            await Task.Run(() =>
            {
                transactions = _db.Transactions.Where(y => y.UserId == UserId && y.Borrow && y.Return == false).OrderByDescending(x => x.Id).ToList();
            });
            return transactions;
        }

        public async Task<Transaction> AcceptTrans(int TransId)
        {
            Transaction transaction = _db.Transactions.Where(x => x.Id == TransId).FirstOrDefault();
            transaction.Borrow = true;
            transaction.TransDate = DateTime.Now;
            await Task.Run(() =>
            {
                _db.Transactions.Update(transaction);
                _db.SaveChanges();
            });
            return transaction;
        }
        public async Task<Transaction> RejectTrans(int TransId)
        {
            Transaction transaction = _db.Transactions.Where(x => x.Id == TransId).FirstOrDefault();
            transaction.IsRejected = true;
            transaction.TransDate = DateTime.Now;
            await Task.Run(() =>
            {
                _db.Transactions.Update(transaction);
                _db.SaveChanges();
            });
            return transaction;
        }

        public async Task<Transaction> RequestBook(int BookId,int UserId)
        {
            Books book = _db.BooksData.Where(x => x.Id == BookId).FirstOrDefault();
            User user = _db.Users.Where(x => x.UserId == UserId).FirstOrDefault();
            Transaction transaction = new Transaction();
            if (book != null && user != null)
            {
                transaction.TransDate= DateTime.Now;
                transaction.BookId = BookId;
                transaction.UserId = UserId;
                transaction.IsRequested = true;
                transaction.Name= user.Name;
                transaction.BookTitle = book.Title;
                await Task.Run(() =>
                {
                    _db.Transactions.Update(transaction);
                    _db.SaveChanges();
                });
            }
            return transaction;
        }

        public async Task<Transaction> ReturnBook(int TransId)
        {
            Transaction transaction = _db.Transactions.Where(x => x.Id == TransId).FirstOrDefault();
            if (transaction != null)
            {
                transaction.TransDate = DateTime.Now;
                transaction.Return = true;
                await Task.Run(() =>
                {
                    _db.Transactions.Update(transaction);
                    _db.SaveChanges();
                });
            }
            return transaction;
        }

        public async Task<List<Transaction>> GetReturnList()
        {
            List<Transaction> transactions = new List<Transaction>();
            await Task.Run(() =>
            {
                transactions = _db.Transactions.Where(y => y.Return).OrderByDescending(x => x.Id).ToList();
            });
            return transactions;
        }
    }
}
