using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TST_Music_Instruments_Store.Controllers
{
    public class HomeController : Controller
    {
        /*ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext()
                                    .GetUserManager<ApplicationUserManager>()
                                    .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        var FirstName = user.FirstName;*/
        [AllowAnonymous]
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
    }
}