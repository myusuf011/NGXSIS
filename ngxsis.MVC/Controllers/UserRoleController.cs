using ngxsis.Repository;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ngxsis.MVC.Controllers
{
    public class UserRoleController : Controller
    {
        // GET: Pengguna
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Form()
        {
            return PartialView("_Form",new UserRoleViewModel());
        }
    }
}