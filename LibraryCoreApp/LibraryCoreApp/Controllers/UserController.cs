using LibraryCoreApp.Models;
using LibraryCoreApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryCoreApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IBooksService _booksService;
        private readonly ITransactionService _transactionService;
        public UserController(IBooksService booksService, ITransactionService transactionService)
        {
            _booksService = booksService;
            _transactionService = transactionService;
        }
        public async Task<IActionResult> Index(string message)
        {
            if (!string.IsNullOrEmpty(message))
                ViewBag.message = message;
            var booklist =await _booksService.GetAllBooks();
            return View(booklist);
        }
        public async Task<ActionResult> GetAllBooks()
        {
            var booklist = await _booksService.GetAllBooks();
            return Json(booklist);
        }
        public async Task<IActionResult> RequestBook(int BookId)
        {
            if(BookId <= 0)
            {
                return View();
            }
            Transaction transaction = await _transactionService.RequestBook(BookId, Convert.ToInt32(HttpContext.Session.GetInt32("_UserId")));
            if(transaction != null && transaction.Id > 0)
            {
                return RedirectToAction("RequestedList", "User", new { message = "Requested successfully!" }); /*, new { userId = userId, userName = userName }*/
            }
            return RedirectToAction("RequestedList", "User", new { message = "Something went wrong!" });
        }

        public async Task<IActionResult> RequestedList(string message)
        {
            if(!string.IsNullOrEmpty(message))
                ViewBag.message = message;
            List<Transaction> transactionlist = await _transactionService.GetRequestList(Convert.ToInt32(HttpContext.Session.GetInt32("_UserId")));
            return View(transactionlist);
        }
        public async Task<IActionResult> RejectedList()
        {
            List<Transaction> transactionlist = await _transactionService.GetRejectedList(Convert.ToInt32(HttpContext.Session.GetInt32("_UserId")));
            return View(transactionlist);
        }
        public async Task<IActionResult> BorrowList(string message)
        {
            if (!string.IsNullOrEmpty(message))
                ViewBag.message = message;
            List<Transaction> transactionlist = await _transactionService.GetBorrowList(Convert.ToInt32(HttpContext.Session.GetInt32("_UserId")));
            return View(transactionlist);
        }

        public async Task<IActionResult> ReturnBook(int TransId)
        {
            if (TransId <= 0)
            {
                return View();
            }
            Transaction transaction = await _transactionService.ReturnBook(TransId);
            if (transaction != null && transaction.Id > 0)
            {
                return RedirectToAction("Index", "User", new { message = "Book return request successfully!" });
            }
            return RedirectToAction("BorrowList", "User", new { message = "Something went wrong!" });
        }
    }
}
