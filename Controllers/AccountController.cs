using Microsoft.AspNetCore.Mvc;
using NRGUserPortal.Models;
using System.Linq;

namespace NRGUserPortal.Controllers
{
    // account controller - written in normal fresher style
    public class AccountController : Controller
    {
        private readonly AppDbContext db;

        public AccountController(AppDbContext context)
        {
            db = context;
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            // show register page
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // model invalid - show back to user
                return View(model);
            }

            // check email exists (simple)
            var exists = db.Users.Any(u => u.Email == model.Email);
            if (exists)
            {
                ModelState.AddModelError(string.Empty, "Email already registered");
                return View(model);
            }

            // hash password and save
            var hashed = BCrypt.Net.BCrypt.HashPassword(model.Password ?? "");

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                Email = model.Email,
                Password = hashed
            };

            db.Users.Add(user);
            db.SaveChanges();

            // set session (very simple)
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("FirstName", user.FirstName ?? "User");

            return RedirectToAction("MyAccount");
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var u = db.Users.SingleOrDefault(x => x.Email == model.Email);
            if (u == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid credentials");
                return View(model);
            }

            var ok = BCrypt.Net.BCrypt.Verify(model.Password ?? "", u.Password ?? "");
            if (!ok)
            {
                ModelState.AddModelError(string.Empty, "Invalid credentials");
                return View(model);
            }

            HttpContext.Session.SetInt32("UserId", u.Id);
            HttpContext.Session.SetString("FirstName", u.FirstName ?? "User");

            return RedirectToAction("MyAccount");
        }

        // GET: /Account/MyAccount
        public IActionResult MyAccount()
        {
            var id = HttpContext.Session.GetInt32("UserId");
            if (id == null) return RedirectToAction("Login");

            ViewBag.FirstName = HttpContext.Session.GetString("FirstName") ?? "User";
            return View();
        }

        // GET: /Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
