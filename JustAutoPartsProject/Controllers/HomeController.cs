using JustAutoPartsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace JustAutoPartsProject.Controllers
{
    public class HomeController : Controller
    {
        public modelContext DB = new modelContext();
        public ActionResult Dashboard()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] == null)
                {
                    return View();
                }
                return new HttpNotFoundResult("Not Allowed");
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult Store()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] == null)
                {
                    return View();
                }
                return new HttpNotFoundResult("Not Allowed");
            }
            return RedirectToAction("Login", "Account");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult body()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] == null)
                {
                    if (DB.Parts.Count() > 0)
                    {
                        var query = (from x in DB.Parts where x.Category == "Body" select x);
                        return View(query);
                    }
                }
                return new HttpNotFoundResult("Not Allowed");
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult engine()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] == null)
                {
                    if (DB.Parts.Count() > 0)
                    {
                        var query = (from x in DB.Parts where x.Category == "Engine" select x);
                        return View(query);
                    }
                }
                return new HttpNotFoundResult("Not Allowed");
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult Brake()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] == null)
                {
                    if (DB.Parts.Count() > 0)
                    {
                        var query = (from x in DB.Parts where x.Category == "Brake-System" select x);
                        return View(query);
                    }
                }
                return new HttpNotFoundResult("Not Allowed");
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult Filter()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] == null)
                {
                    if (DB.Parts.Count() > 0)
                    {
                        var query = (from x in DB.Parts where x.Category == "FILTER" select x);
                        return View(query);
                    }
                }
                return new HttpNotFoundResult("Not Allowed");
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult Exhaust()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] == null)
                {
                    if (DB.Parts.Count() > 0)
                    {
                        var query = (from x in DB.Parts where x.Category == "EXHAUST" select x);
                        return View(query);
                    }
                }
                return new HttpNotFoundResult("Not Allowed");
            }
            return RedirectToAction("Login", "Account");
        }

    }
}