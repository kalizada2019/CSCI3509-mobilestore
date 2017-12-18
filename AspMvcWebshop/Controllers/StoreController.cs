using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspMvcWebshop.Models;

namespace AspMvcWebshop.Controllers
{
    public class StoreController : Controller
    {
        StoreEntities storeDB = new StoreEntities();

        
        public ActionResult Index()
        {
            var categories = storeDB.Categories.ToList();        
            return View(categories);
        }
        
        public ActionResult Browse(string category)
        {
            
            var categoryModel = storeDB.Categories.Include("Products")
                .SingleOrDefault(c => c.Name.Contains(category));
                                
            
            return View(categoryModel);
        }

        
        public ActionResult Details(int id)
        {
            var product = storeDB.Products.Find(id);

         

            return View(product);
        }

        
        [ChildActionOnly]
        public ActionResult CategoryMenu()
        {
            var genres = storeDB.Categories.ToList();
            return PartialView(genres);
        }

        
        [ChildActionOnly]
        public ActionResult AdminMenu()
        {
            if (User.IsInRole("Administrator")) 
            {
                return PartialView("AdminMenu");
            }
            return new EmptyResult();
        }

        [ChildActionOnly]
        public ActionResult GuestMenu()
        {
            if (!User.Identity.IsAuthenticated) 
            {
                return PartialView("GuestMenu");
            }
            return new EmptyResult();
        }

        [ChildActionOnly]
        public ActionResult RegisteredUser()
        {
            if (User.Identity.IsAuthenticated && !User.IsInRole("Administrator")) 
            {
                return PartialView("RegisteredUserMenu");
            }
            return new EmptyResult();
        }  
    }
}
