using LibraryCoreApp.Models;

namespace LibraryCoreApp.Services
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetRequestList();
        Task<List<Transaction>> GetRequestList(int UserId);
        Task<List<Transaction>> GetRejectedList();
        Task<List<Transaction>> GetRejectedList(int UserId);
        Task<List<Transaction>> GetBorrowList();
        Task<List<Transaction>> GetBorrowList(int UserId);
        Task<Transaction> AcceptTrans(int TransId);
        Task<Transaction> RejectTrans(int TransId);
        Task<Transaction> RequestBook(int BookId, int UserId);
        Task<Transaction> ReturnBook(int TransId);
        Task<List<Transaction>> GetReturnList();
    }
}