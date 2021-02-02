using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
            using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
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
            }
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
        //RemoveItem
        [AllowAnonymous]
        public ActionResult RemoveItemQuantity()
        {
            string rawId = Request.QueryString["ProductId"];
            int productId;
            if (!String.IsNullOrEmpty(rawId) && int.TryParse(rawId, out productId))
            {
                using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
                {
                    String cartId = usersShoppingCart.GetCartId();
                    var allItems = usersShoppingCart.GetCartItems();
                    int currentQuantity = 0;
                    foreach (var item in allItems)
                    {
                        if (Convert.ToInt16(rawId) == item.ProductId)
                        {
                            currentQuantity = item.Quantity;
                        }
                    }
                    currentQuantity -= 1;
                    if (currentQuantity == 0)
                    {
                        usersShoppingCart.RemoveItem(cartId, Convert.ToInt16(rawId));
                    }
                    else
                    {
                        usersShoppingCart.UpdateItem(cartId, Convert.ToInt16(rawId), currentQuantity);
                    }

                }
            }
            else
            {
                Debug.Fail("ERROR : We should never get to /ShoppingCart/RemoveItem without a ProductId.");
                throw new Exception("ERROR : It is illegal to load /ShoppingCart/RemoveItem without setting a ProductId.");
            }
            return RedirectToAction("Index");
        }
        //AddItemQuantity
        [AllowAnonymous]
        public ActionResult AddItemQuantity()
        {
            string rawId = Request.QueryString["ProductId"];
            int productId;
            if (!String.IsNullOrEmpty(rawId) && int.TryParse(rawId, out productId))
            {
                using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
                {
                    String cartId = usersShoppingCart.GetCartId();
                    var allItems = usersShoppingCart.GetCartItems();
                    int currentQuantity = 0;
                    foreach (var item in allItems)
                    {
                        if (Convert.ToInt16(rawId) == item.ProductId)
                        {
                            currentQuantity = item.Quantity;
                        }
                    }
                    currentQuantity += 1;
                    usersShoppingCart.UpdateItem(cartId, Convert.ToInt16(rawId), currentQuantity);
                }
            }
            else
            {
                Debug.Fail("ERROR : We should never get to /ShoppingCart/AddItemQuantity without a ProductId.");
                throw new Exception("ERROR : It is illegal to load /ShoppingCart/AddItemQuantity without setting a ProductId.");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Purchase()
        {

            using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
            {
                if (usersShoppingCart.GetCartItems().Count != 0 && this.User.Identity.IsAuthenticated)
                {
                    ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext()
                        .GetUserManager<ApplicationUserManager>()
                        .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                    ViewBag.FirstName = user.FirstName;
                    ViewBag.LastName = user.LastName;
                    ViewBag.Address = user.Address;
                    usersShoppingCart.EmptyCart();
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }

        }

    }
}
