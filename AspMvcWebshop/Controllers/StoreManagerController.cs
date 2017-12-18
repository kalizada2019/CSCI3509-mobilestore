using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AspMvcWebshop.Models;
using System.IO;
using System.Web.Helpers;
 
namespace AspMvcWebshop.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class StoreManagerController : Controller
    {
        private StoreEntities db = new StoreEntities();

        
        public ActionResult Index()
        {            
            var products = db.Products.Include(p => p.Brand).Include(p => p.Category);
            return View(products.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        
        public ActionResult Create()
        {
            ViewBag.brandId = new SelectList(db.Brands, "BrandId", "Name");
            ViewBag.categoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="productId,categoryId,brandId,Name,Price,ProductImg")] Product product)
        {
            if (ModelState.IsValid)
            {
                WebImage ProductImg = null;
                var newFileName = "";
                var imagePath = "";
                
                if (ProductImg != null)
                {
                    newFileName = Guid.NewGuid().ToString() + "_" +
                        Path.GetFileName(ProductImg.FileName);
                    imagePath = @"~\Content\Images\" + newFileName;

                    ProductImg.Save(@"~\" + imagePath);
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.brandId = new SelectList(db.Brands, "BrandId", "Name", product.brandId);
            ViewBag.categoryId = new SelectList(db.Categories, "CategoryId", "Name", product.categoryId);
            return View(product);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.brandId = new SelectList(db.Brands, "BrandId", "Name", product.brandId);
            ViewBag.categoryId = new SelectList(db.Categories, "CategoryId", "Name", product.categoryId);
            return View(product);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="productId,categoryId,brandId,Name,Price,ProductImg")] Product product)
        {
            if (ModelState.IsValid)
            {
                WebImage ProductImg = null;
                var newFileName = "";
                var imagePath = "";
                
                if (ProductImg != null)
                {
                    newFileName = Guid.NewGuid().ToString() + "_" +
                        Path.GetFileName(ProductImg.FileName);
                    imagePath = @"~\Content\Images\" + newFileName;

                    ProductImg.Save(@"~\" + imagePath);
                }

                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.brandId = new SelectList(db.Brands, "BrandId", "Name", product.brandId);
            ViewBag.categoryId = new SelectList(db.Categories, "CategoryId", "Name", product.categoryId);
            return View(product);
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
