using LibraryCoreApp.Filters;
using LibraryCoreApp.Models;
using LibraryCoreApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryCoreApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IBooksService _booksService;
        private readonly ITransactionService _transactionService;
        public AdminController(IBooksService booksService, ITransactionService transactionService)
        {
            _booksService = booksService;
            _transactionService = transactionService;
        }
        public async Task<IActionResult> Index()
        {
            var booklist = await _booksService.GetAllBooks();
            return View(booklist);
        }
        public async Task<JsonResult> GetAllBooks()
        {
            var booklist = await _booksService.GetAllBooks();
            return Json(booklist);
        }
        public async Task<IActionResult> AddBook(string message)
        {
            if (!string.IsNullOrEmpty(message))
                ViewBag.Message = message;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(Books book) /*[Bind(Include = "Id,Title,Category,Author,Copies,Publication,DateAdded")]*/
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddBook", "Admin", new { message = "Enter correct details to add book." });
            }
            await _booksService.AddBook(book);
            return RedirectToAction("Index", "Admin", new { message = "Book added successfully!" });
        }
        [HttpPost]
        public async Task<bool> DeleteBook(int BookId)
        {
            if (BookId <= 0)
            {
                return false;
            }
            bool result = await _booksService.DeleteBook(BookId);
            return result;
        }

        public async Task<IActionResult> EditBook(int id)
        {
            if(id <= 0)
            {
                return RedirectToAction("Index", "Admin");
            }
            Books book = await _booksService.GetBook(id);
            return View(book);
        }
        [HttpPost]
        public async Task<IActionResult> EditBook(Books book)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Admin", new { message = "something went wrong. Try again" });
            }
            await _booksService.UpdateBook(book);
            return RedirectToAction("Index", "Admin", new { message = "Book updated successfully!" });
        }

        public async Task<IActionResult> GetRequestlist()
        {            
            return View();
        }
        public async Task<JsonResult> GetAllRequestlist()
        {
            List<Transaction> transactions = new List<Transaction>();
            transactions = await _transactionService.GetRequestList();
            return Json(transactions);
        }
        public async Task<IActionResult> AcceptRequest(int tranId)
        {
            Transaction transaction = new Transaction();
            transaction = await _transactionService.AcceptTrans(tranId);
            return View("BorrowList");
        }

        public async Task<IActionResult> RejectRequest(int tranId)
        {
            Transaction transaction = new Transaction();
            transaction = await _transactionService.RejectTrans(tranId);
            return RedirectToAction("RejectedList", "Admin", new { message = "Rejected successfully!" });
        }
        public async Task<IActionResult> BorrowList(string message)
        {
            if (!string.IsNullOrEmpty(message))
                ViewBag.message = message;
            return View();
        }
        public async Task<JsonResult> AllBorrowList()
        {
            List<Transaction> transactionList = await _transactionService.GetBorrowList();
            return Json(transactionList);
        }
        public async Task<IActionResult> RejectedList(string message)
        {
            if (!string.IsNullOrEmpty(message))
                ViewBag.message = message;
            return View();
        }
        public async Task<JsonResult> AllRejectedList()
        {
            List<Transaction> transactionList = await _transactionService.GetRejectedList();
            return Json(transactionList);
        }
        public async Task<IActionResult> ReturnList()
        {
            return View();
        }
        public async Task<JsonResult> AllReturnList()
        {
            List<Transaction> transactionList = await _transactionService.GetReturnList();
            return Json(transactionList);
        }
    }
}
