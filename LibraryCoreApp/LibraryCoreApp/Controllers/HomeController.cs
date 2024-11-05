using LibraryCoreApp.Models;
using LibraryCoreApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace LibraryCoreApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private const string SessionUserId = "_UserId";
        private const string SessionIsAdmin = "_IsAdmin";
        private readonly IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, IUserService userService,IConfiguration configuration)
        {
            _logger = logger;
            _userService = userService;
            _configuration = configuration;
        }      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login(string message)
        {
            if (!string.IsNullOrEmpty(message))
                ViewBag.Message = message;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
                using (var cn = new SqlConnection(connectionString))
                {
                    await cn.OpenAsync();
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandText = "UserLogin";
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.Parameters.AddWithValue("@Email", user.Email);
                        cm.Parameters.AddWithValue("@Password", user.Password);

                        using (SqlDataReader dr = await cm.ExecuteReaderAsync())
                        {
                            if (await dr.ReadAsync())
                            {
                                user.UserId = dr.GetInt32(0);
                                user.Name = dr.GetString(1);
                                user.Contact = dr.GetString(2);
                                user.Gender = dr.GetString(3);
                                user.Email = dr.GetString(4);
                                user.Password = dr.GetString(5);
                                user.IsAdmin = dr.GetBoolean(6);
                            }
                        }
                    }
                }
                //user = await _userService.GetUser(user.Email,user.Password);
                if (user.UserId > 0)
                {
                    HttpContext.Session.SetInt32(SessionUserId, user.UserId);
                    HttpContext.Session.SetString(SessionIsAdmin, user.IsAdmin.ToString());
                    if (user.IsAdmin)
                        return RedirectToAction("Index", "Admin");
                    else
                        return RedirectToAction("Index", "User");
                }
                else if (user.Email == null && user.Password == null)
                {
                    return View();
                }
                ViewBag.Message = "User name and password are not matching";
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                throw;
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("_UserId");
            HttpContext.Session.Remove("_IsAdmin");
            return RedirectToAction("Login", "Home");
        }

        public IActionResult Register(string message)
        {
            if(!string.IsNullOrEmpty(message))
                ViewBag.Message = message;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Register", "Home", new { message = "Enter correct details to register." });
            }
            await _userService.AddUser(user);
            return RedirectToAction("Login", "Home", new { message = "User registered successfully!" });
        }
    }
}