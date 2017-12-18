using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspMvcWebshop.Models;

namespace AspMvcWebshop.Controllers
{
    
    [Authorize]
    public class CheckoutController : Controller
    {
        StoreEntities storeDB = new StoreEntities();
        const string PromoCode = "FREE"; 

        
        public ActionResult AddressAndPayment()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;

                    
                    storeDB.Orders.Add(order);
                    storeDB.SaveChanges();
                    
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete",
                        new { id = order.OrderId });
                }
            }
            catch
            {
                
                return View(order);
            }
        }

        
        public ActionResult Complete(int id)
        {
            
            bool isValid = storeDB.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
	}
}