using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ngxsis.Repository;
using ngxsis.ViewModel;

namespace ngxsis.MVC.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult SelectAccess()
        {
            if (Session["userID"] != null)
            {
                long sesion = (long)Session["userID"];
                ViewBag.CompanyList = new SelectList(AccessRepo.SelectCompany(sesion), "CompanyId", "CompanyName");
                ViewBag.RoleList = new SelectList(AccessRepo.SelectRole(sesion), "RoleId", "RoleName");
            }
                        
            return View();
        }

        [HttpPost]
        public ActionResult SetSessionAkses(long idRole, long idCompany)
        {
            Session["roleID"] = idRole;
            Session["companyID"] = idCompany;

            ResponseResult result = new ResponseResult();

            return Json(new
            {
                success = result.Success
            }, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult RemoveSessionAkses()
        {
            Session.Remove("roleID");
            Session.Remove("companyID");

            return RedirectToAction("SelectAccess", "Account");
        }

    }

}