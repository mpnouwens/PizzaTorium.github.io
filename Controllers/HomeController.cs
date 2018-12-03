using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using PizzaTorium_complete.Models;

namespace PizzaTorium_complete.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {

        public ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Design()
        {

            
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Small (R15)", Value = "Small" });
            items.Add(new SelectListItem { Text = "Medium (R25)", Value = "Medium", Selected = true });
            items.Add(new SelectListItem { Text = "Large (R40)", Value = "Large" });

            ViewBag.PizzaSize = items;

            return View(db.Ingredients.ToList());
         
        }

        [HttpPost]
        public ActionResult Design(List<Ingredients> ingr, string PizzaSize)
        {


            List<Ingredients> IngIsSelected = new List<Ingredients>();

            foreach (var item in ingr)
            {
                if (item.Checked)
                {
                    IngIsSelected.Add(item);
                }
            }


            Session["PizzaSize"] = PizzaSize;
            Session["Ingredients"] = IngIsSelected;

            return RedirectToAction("Final");



        }

        public ActionResult Final()
        {
            List<SelectListItem> areas = new List<SelectListItem>();
            foreach (var item in db.Areas)
            {
                areas.Add(new SelectListItem{ Text = item.Area, Value = item.Area});
            }

            ViewBag.Areas = areas;

            List<SelectListItem> payOptions = new List<SelectListItem>();

            payOptions.Add(new SelectListItem { Text = "Cash", Value = "Cash", Selected = true });
            payOptions.Add(new SelectListItem { Text = "Card", Value = "Card" });

            ViewBag.Payment = payOptions;


            //Get current User
            var theUser = User.Identity.Name;

            var query = db.TblUsers.Where(s => s.Username == theUser);
            Session["User"] = query.FirstOrDefault<Users>();
            ViewBag.User = query.FirstOrDefault<Users>();

            decimal vCost = 0.00m;

            //Get PizzaSize
            string Size = Session["pizzaSize"] as string;

            switch (Size)
            {
                case "Small":
                    vCost += 15.00m;
                    break;
                case "Medium":
                    vCost += 25.00m;
                    break;
                case "Large":
                    vCost += 40.00m;
                    break;
                default:
                    break;
            }

            var model = Session["ingredients"] as List<Ingredients>;

            foreach (var item in model)
            {
                vCost += item.Cost;
            }

            ViewBag.Total = vCost;

            Users myUser = Session["User"] as Users;

            Orders tempOrder = new Orders { User = User.Identity.Name, Address = myUser.Address, Cost = vCost };


            return View(tempOrder);

        
        }

        [Authorize]
        [HttpPost]
        public ActionResult Final(Orders order, string Areas, string Payment)
        {
            if (ModelState.IsValid)
            {
                Users myUser = Session["User"] as Users;
                order.CustomerName = myUser.Name;
                order.Area = Areas;
                order.PayMethod = Payment;
                order.DateOrdered = DateTime.Now;

                var q = (from c in db.Delivery where c.Area == Areas select c).OrderBy(o => SqlFunctions.Rand()).First();

                order.EmpName = q.Name;

                db.Orders.Add(order);
                db.SaveChanges();

                Session["myOrder"] = order;

                return RedirectToAction("ThankYou");
            }
            else
            {
                ModelState.AddModelError("Orders.Address", "You must add an Address!");
                return View(order);
            }
        }

        [Authorize]
        public ActionResult ThankYou(/*Orders order, string Areas, string Payment*/)
        {
            Orders model = Session["myOrder"] as Orders;


            if (model != null)
            {
                return View(model);
            }

            return HttpNotFound();
        }

        [Authorize]
        public ActionResult MyOrders()
        {
            var q = db.Orders.Where(m => m.User == User.Identity.Name);

            List<Orders> orderL = q.ToList<Orders>();

            return View(orderL);
        }


    }
}