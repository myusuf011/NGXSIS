using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ngxsis.MVC.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult SelectAccess()
        {
            return View();
        }
    }
}