using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TST_Music_Instruments_Store.Models;

namespace TST_Music_Instruments_Store.Controllers
{
    public class ProductsController : Controller
    {
       



        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            List<Product> dbProducts = db.Products.ToList();
            List<Product> filteredProductsByInstrument = new List<Product>();
            List<Product> filteredProducts= new List<Product>();
            List<string> instruments = new List<string>();
            List<string> manufacturer = new List<string>();
            foreach (var form in formCollection)
            {
                if (form.ToString().Equals("Guitar") ||
                    form.ToString().Equals("Violin") ||
                    form.ToString().Equals("Viola") ||
                    form.ToString().Equals("Chello") ||
                    form.ToString().Equals("Bass"))
                {
                    instruments.Add(form.ToString());
                }
                if (form.ToString().Equals("Gibson") ||
                    form.ToString().Equals("Fender") ||
                    form.ToString().Equals("Yamaha") ||
                    form.ToString().Equals("Shure") ||
                    form.ToString().Equals("Sennheiser"))
                {
                    manufacturer.Add(form.ToString());
                }
            }
            if (instruments.Any())
            {
                foreach (var dbItem in dbProducts)
                {
                    if (instruments.Contains(dbItem.ProductCategory))
                    {
                        filteredProductsByInstrument.Add(dbItem);
                    }
                }
            }
            if (manufacturer.Any() && instruments.Any())
            {
                foreach (var dbItem in filteredProductsByInstrument)
                {
                    if (manufacturer.Contains(dbItem.Manufacturer))
                    {
                        filteredProducts.Add(dbItem);
                    }
                }
            } 
            else if (!instruments.Any())
            {
                foreach (var dbItem in dbProducts)
                {
                    if (manufacturer.Contains(dbItem.Manufacturer))
                    {
                        filteredProducts.Add(dbItem);
                    }
                }
            }
            
            return View(filteredProducts);
        }

        // GET: Products/Details/5
        [AllowAnonymous]
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

        // GET: Products/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "ProductId,NameOfProduct,ProductCategory,ProductSubCategory,StringCount,StringSize,Price,ImagePath,Manufacturer,Color,Rating")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Administrator")]
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
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "ProductId,NameOfProduct,ProductCategory,ProductSubCategory,StringCount,StringSize,Price,ImagePath,Manufacturer,Color,Rating")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Administrator")]
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

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
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
