using LoginFormWithCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LoginFormWithCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Db_2012Context _dbcontext;


        public HomeController(ILogger<HomeController> logger, Db_2012Context dbcontext)
        {
            _logger = logger;
            _dbcontext = dbcontext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UsrTbl user)
        {
            if(ModelState.IsValid)
            {
                await _dbcontext.UsrTbls.AddAsync(user);
                await _dbcontext.SaveChangesAsync();
                TempData["Success"] = "Registered Successfully.";
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return RedirectToAction("Dashboard");
            }

            return View();
        }
        [HttpPost]
        public IActionResult Login(UsrTbl user)
        {
            var myUser = _dbcontext.UsrTbls.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            if (myUser != null) 
            {
                HttpContext.Session.SetString("UserSession", value: myUser.Email);
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.Message = "LoginFailed";
            }
            return View();
        }

        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetString("UserSession") != null) 
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                HttpContext.Session.Remove("UserSession");
                return RedirectToAction("Login"); 
            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
