using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspMvcWebshop.Models;

namespace AspMvcWebshop.Controllers
{
    public class HomeController : Controller
    {

        StoreEntities storeDB = new StoreEntities();
        
        public ActionResult Index()
        {
            
            var products = GetTopSellingProducts(4);

            return View(products);

            
        }

        
        private List<Product> GetTopSellingProducts(int count)
        {
            
            return storeDB.Products
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(count)
                .ToList();
        }

        public ActionResult Search (String searched)
        {
            

            var products = GetSearched(searched);

            return View(products);
        }

        private List<Product> GetSearched (String search)
        {
            
            return storeDB.Products.
                Where(a => a.Name.Contains(search) || search == null).ToList();
        }

        public ActionResult About()
        {
         

            return View();

            
        }
    }
}
