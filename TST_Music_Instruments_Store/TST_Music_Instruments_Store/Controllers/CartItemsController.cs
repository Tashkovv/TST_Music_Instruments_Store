using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TST_Music_Instruments_Store.Logic;
using TST_Music_Instruments_Store.Models;

namespace TST_Music_Instruments_Store.Controllers
{
    public class CartItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CartItems
        public ActionResult Index()
        {
            ShoppingCartActions actions = new ShoppingCartActions();
            /*using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
            {
                decimal cartTotal = 0;
                cartTotal = usersShoppingCart.GetTotal();
                if (cartTotal > 0)
                {
                    // Display Total.
                    ViewBag.Total = cartTotal;
                }
                else
                {
                    ViewBag.Total = "0";
                }
            }*/
            return View(actions.GetCartItems());
        }

        //AddToCart
        [AllowAnonymous]
        public ActionResult AddToCart()
        {
            string rawId = Request.QueryString["ProductId"];
            int productId;
            if (!String.IsNullOrEmpty(rawId) && int.TryParse(rawId, out productId))
            {
                using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
                {
                    usersShoppingCart.AddToCart(Convert.ToInt16(rawId));
                }
            }
            else
            {
                Debug.Fail("ERROR : We should never get to /ShoppingCart/AddToCart without a ProductId.");
                throw new Exception("ERROR : It is illegal to load /ShoppingCart/AddToCart without setting a ProductId.");
            }
            return RedirectToAction("Index");
        }
        //Remove item from cart
        [AllowAnonymous]
        public ActionResult RemoveItemFromCart()
        {
            string rawId = Request.QueryString["ProductId"];
            int productsId;
            if (!String.IsNullOrEmpty(rawId) && int.TryParse(rawId, out productsId))
            {
                using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
                {
                    String cartId = usersShoppingCart.GetCartId();
                    usersShoppingCart.RemoveItem(cartId, Convert.ToInt16(rawId));
                }
            }
            else
            {
                Debug.Fail("ERROR : We should never get to /ShoppingCart/AddItemQuantity without a ProductId.");
                throw new Exception("ERROR : It is illegal to load /ShoppingCart/AddItemQuantity without setting a ProductId.");
            }
            return RedirectToAction("Index");
        }
        // GET: CartItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.ShoppingCartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // GET: CartItems/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "NameOfProduct");
            return View();
        }

        // POST: CartItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,CartId,Quantity,DateCreated,ProductId")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                db.ShoppingCartItems.Add(cartItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "NameOfProduct", cartItem.ProductId);
            return View(cartItem);
        }

        // GET: CartItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.ShoppingCartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "NameOfProduct", cartItem.ProductId);
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,CartId,Quantity,DateCreated,ProductId")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "NameOfProduct", cartItem.ProductId);
            return View(cartItem);
        }

        // GET: CartItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.ShoppingCartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CartItem cartItem = db.ShoppingCartItems.Find(id);
            db.ShoppingCartItems.Remove(cartItem);
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
