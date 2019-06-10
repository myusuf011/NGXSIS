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
        public ActionResult BioBySearch(string search = "0")
        {
            return PartialView("_BioBySearch", UserRoleRepo.BySearch(search));
        }
        public ActionResult RoleList()
        {
            return PartialView("_RoleList", RoleRepo.All());
        }
    }
}