using JustAutoPartsProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace JustAutoPartsProject.Controllers
{
    public class AccountController : Controller
    {
        private modelContext DB = new modelContext();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            if (ModelState.IsValid)
            {
                using (var modelContext = new modelContext())
                {
                    var auth_user = (from s in modelContext.Users where ((s.Email == Email) && (s.Password == Password)) select s).FirstOrDefault();
                    if (auth_user != null)
                    {
                        FormsAuthentication.SetAuthCookie(auth_user.UserName, false);
                        Session["id"] = auth_user.UserID;
                        if (!String.IsNullOrEmpty(auth_user.Role) && auth_user.Role.Equals("Admin"))
                        {
                            Session["Role"] = auth_user.Role;
                            return RedirectToAction("Dashboard", "Admin");
                        }
                        return RedirectToAction("Dashboard", "Home");

                    }
                    ModelState.AddModelError("", "Invalid email and/or password");
                    return View();
                }

            }
            return View();

        }
        public ActionResult SignUp() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                using (var modelContext = new modelContext())
                {
                    modelContext.Users.Add(user);
                    modelContext.SaveChanges();
                }
                ViewBag.Message = user.UserName + " " + " Successfuly register ";
            }
            return View("Login");
        }
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult manageAccount(User user)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    using (var modelContext = new modelContext())
                    {
                        modelContext.Entry(user).State = EntityState.Modified;
                        modelContext.SaveChanges();
                    }
                    return RedirectToAction("Logout", "Home");
                }
                return View();
            }
            return RedirectToAction("Login");
        }
        [Authorize]
        [HttpGet]
        public ActionResult manageAccount()
        {
            if (User.Identity.IsAuthenticated)
            {
                using (var modelContext = new modelContext())
                {
                    int id = (int)Session["Id"];
                    User user = new User();
                    user = modelContext.Users.FirstOrDefault(x => x.UserID.Equals(id));
                    return View(user);
                }
            }
            return RedirectToAction("Login");
        }
        [Authorize]
        [HttpGet]
        public ActionResult checkBalance()
        {
            if (User.Identity.IsAuthenticated)
            {
                using (var modelContext = new modelContext())
                {
                    int id = (int)Session["Id"];
                    User user = new User();
                    user = modelContext.Users.FirstOrDefault(x => x.UserID.Equals(id));
                    return View(user);
                }
            }
            return RedirectToAction("Login");
        }
    }
}