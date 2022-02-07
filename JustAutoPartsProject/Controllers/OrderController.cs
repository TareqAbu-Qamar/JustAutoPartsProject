using JustAutoPartsProject.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JustAutoPartsProject.Controllers
{
    public class OrderController : Controller
    {
        modelContext DB = new modelContext();
        static Order CHorder = new Order();

        // GET: Order
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] != null)
                {
                    List<Order> orders = new List<Order>();
                    orders = (from obj in DB.Orders
                              select obj).ToList();
                    return View(orders);
                }
                return new HttpNotFoundResult("Not Allowed");
            }
            return RedirectToAction("Login", "Account");
        }
        public ActionResult OrderDetails(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] != null)
                {
                    Order order = new Order();
                    order = (from obj in DB.Orders
                             where obj.OrderID == id
                             select obj).FirstOrDefault();
                    return View(order);
                }
                return new HttpNotFoundResult("Not Allowed");
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult MyBill(Order o)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] == null)
                {
                   // return View(o);
                    return View(CHorder);
                }
                return new HttpNotFoundResult("Not Allowed");
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult CheckOuts()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] == null)
                {
                    return View(CHorder);
                }
                return new HttpNotFoundResult("Not Allowed");
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult CheckOuts(Order order)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] == null)
                {
                    int id = (int)Session["id"];
                    var user = DB.Users.SingleOrDefault(x => x.UserID.Equals(id));
                    if (order != null)
                    {
                        order.Bill.Value = order.part.Price * order.part.Quantity;
                        order.Bill.Status = "processing";
                        order.Bill.generateBill(CHorder);
                        //order.Bill = bill;

                        DB.Bills.Add(order.Bill);
                        user.pay(order.Bill.Value);
                        DB.SaveChanges();
                        using (SqlConnection connect = new SqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                        {

                            var a = String.Join(",", CHorder.part.Name);
                            string query = "Insert Into [JustDB].[dbo].[Orders] (OrderID,Bill_BillID,Items,part_PartID)" +
                                "Values('" + order.OrderID + "','" + order.Bill.BillID + "','" + a + "','" + "','" + order.part.PartID + "')'";

                            SqlCommand command = new SqlCommand(query, connect);
                            connect.Open();
                            command.ExecuteNonQuery();

                        }
                        CHorder.Bill = order.Bill;
                        DB.Orders.Add(order);
                        DB.SaveChanges();

                        return RedirectToAction("MyBill", order);
                    }
                    RedirectToAction("Store", "Home");
                }
                return new HttpNotFoundResult("Not Allowed");
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult PlaceOrder(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] == null)
                {
                    using (modelContext db = new modelContext())
                    {
                        int user_id = (int)Session["Id"];
                        User user = db.Users.FirstOrDefault(x => x.UserID.Equals(user_id));
                        Part part = db.Parts.FirstOrDefault(x => x.PartID.Equals(id));

                        part.Quantity -= 1;
                        db.SaveChanges();
                        part.Quantity = 1;

                        ShoppingCart shoppingCart = new ShoppingCart();
                        shoppingCart.ShopCartID = db.ShoppingCarts.Count() + 1;
                        shoppingCart.addToShoppingCart(part);
                        user.Cart = shoppingCart;

                        Order order = new Order();
                        order.OrderID = db.Orders.Count() + 1;
                        order.createOrder(shoppingCart);
                        CHorder = order;

                        order.Bill.Value = order.part.Price * order.part.Quantity;
                        order.Bill.Status = "processing";
                        order.Bill.generateBill(CHorder);
                        //order.Bill = bill;

                       // DB.Bills.Add(order.Bill);
                        order.Items = part.Name;
                        user.pay(order.Bill.Value);
                        user.Balance -= order.Bill.Value;
                       // DB.SaveChanges();

                        CHorder.Bill = order.Bill;
                        DB.Orders.Add(order);
                        DB.SaveChanges();

                    }
                    return RedirectToAction("MyBill", "Order", CHorder);
                }
                return new HttpNotFoundResult("Not Allowed");
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult PlaceOrder(Part part)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] == null && part != null)
                {
                    int id = (int)Session["Id"];
                    User user = new User();
                    user = DB.Users.SingleOrDefault(x => x.UserID.Equals(id));
                    part.Quantity -= 1;
                    user.Cart.part.Quantity = 1;
                    user.Cart.addToShoppingCart(part);
                    Session["Part"] = part;
                    user.Cart.part = part;
                    CHorder.createOrder(user.Cart);
                    DB.Orders.Add(CHorder);
                    DB.SaveChanges();


                    return RedirectToAction("CheckOuts", "Order", CHorder);
                }
                return new HttpNotFoundResult("Not Allowed");
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult addToCart(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] == null)
                {
                    if (DB.Parts.Count() > 0)
                    {
                        var query = DB.Parts.SingleOrDefault(x => x.PartID.Equals(id));
                        query.Quantity = 1;
                        return View(query);
                    }
                }
                return new HttpNotFoundResult("Not Allowed");
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult cancelOrder(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] == null)
                {
                    if (DB.Parts.Count() > 0)
                    {
                        var query = DB.Parts.SingleOrDefault(x => x.PartID.Equals(id));
                        if (query.Category.Equals("Body"))
                            return RedirectToAction("Body", "Home");

                        else if (query.Category.Equals("Engine"))
                            return RedirectToAction("engine", "Home");

                        else if (query.Category.Equals("Brake-System"))
                            return RedirectToAction("Brake", "Home");

                        else if (query.Category.Equals("FILTER"))
                            return RedirectToAction("Filter", "Home");

                        else if (query.Category.Equals("EXHAUST"))
                            return RedirectToAction("Exhaust", "Home");
                    }
                    return new HttpNotFoundResult("Not Allowed");
                }
                return new HttpNotFoundResult("Not Allowed");
            }
            return RedirectToAction("Login", "Account");
        }
    }
}