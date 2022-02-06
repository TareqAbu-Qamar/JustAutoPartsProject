using JustAutoPartsProject.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JustAutoPartsProject.Controllers
{
    public class AdminController : Controller
    {
        private modelContext DB = new modelContext();
        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddPart()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] != null)
                {
                    return View();
                }
                return new HttpNotFoundResult();
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult AddPart(Part part)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] != null)
                {
                    if (ModelState.IsValid)
                    {
                        if (part != null)
                        {
                            part.DefualtPrice = part.Price;
                            DB.Parts.Add(part);
                            DB.SaveChanges();
                            return RedirectToAction("viewParts");
                        }
                    }
                    return View();
                }
                return new HttpNotFoundResult("Not Allowed");
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult updatePart(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] != null)
                {
                    Part part = new Part();
                    part = (from obj in DB.Parts
                            where obj.PartID == id
                            select obj).FirstOrDefault();

                    DB.SaveChanges();
                    return View(part);
                }
                return new HttpNotFoundResult("Not Allowed");

            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult updatePart(Part part)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] != null)
                {
                    if (ModelState.IsValid)
                    {
                        DB.Entry(part).State = EntityState.Modified;
                        DB.SaveChanges();
                        return RedirectToAction("viewParts");
                    }
                    return View();
                }
                return new HttpNotFoundResult("Not Allowed");
            }
            return RedirectToAction("Login", "Account");
        }     
        public ActionResult deletePart(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] != null)
                {
                    Part part = new Part();
                    part = (from obj in DB.Parts
                            where obj.PartID == id
                            select obj).FirstOrDefault();
                    part.Quantity -= 1;
                    DB.SaveChanges();
                    return RedirectToAction("viewParts");
                }
                return new HttpNotFoundResult("Not Allowed");
            }
            return RedirectToAction("Login", "Account");
        }
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] != null)
                {
                    var user = DB.Users.SingleOrDefault(x => x.Role.Equals("Admin"));
                    return View(user);
                }
                return new HttpNotFoundResult("Not Allowed");
            }
            return RedirectToAction("Login", "Account");
        }
      
        public ActionResult viewUsers()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] != null)
                {
                    var user = (from User in DB.Users
                                select User).ToList();
                    return View(user);
                }
                return new HttpNotFoundResult("Not Allowed");
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult viewUsers(modelContext model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] != null)
                {
                    var user = DB.Users.ToList();
                    return View(user);
                }
                return new HttpNotFoundResult("Not Allowed");
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult viewParts()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] != null)
                {
                    return View(DB.Parts.ToList());
                }
                return new HttpNotFoundResult("Not Allowed");

            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult viewOrders()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] != null)
                {
                    List<Order> orderList = DB.Orders.ToList();
                    List<Part> partList = DB.Parts.ToList();
                    List<Bill> billList = DB.Bills.ToList();

                    var query = from o in orderList
                                join b in billList
                                on o.Bill.BillID equals b.BillID into Orders
                                from b in Orders.DefaultIfEmpty()
                                select o;
                    return View(query);
                }
                return new HttpNotFoundResult("Not Allowed");
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult GetDetails(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] != null)
                {
                    Part item = new Part();
                    item = (from obj in DB.Parts
                            where obj.PartID == id
                            select obj).FirstOrDefault();
                    return View(item);
                }
                return new HttpNotFoundResult("Not Allowed");

            }
            return RedirectToAction("Login", "Account");
        }
    }
}