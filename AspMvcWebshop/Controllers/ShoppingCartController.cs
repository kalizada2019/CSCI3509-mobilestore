using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspMvcWebshop.Models;
using AspMvcWebshop.ViewModels;


namespace AspMvcWebshop.Controllers
{
    public class ShoppingCartController : Controller
    {
        StoreEntities storeDB = new StoreEntities();
        
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            
            return View(viewModel);
        }
        
        public ActionResult AddToCart(int id)
        {
            
            var addedProduct = storeDB.Products
                .Single(product => product.productId == id);

            
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedProduct);

            
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            
            var cart = ShoppingCart.GetCart(this.HttpContext);

            
            string productName = storeDB.Carts
                .Single(item => item.RecordId == id).Product.Name;

            
            int itemCount = cart.RemoveFromCart(id);

            
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(productName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
	}
}