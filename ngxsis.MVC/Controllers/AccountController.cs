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
        public ActionResult NavBar()
        {
            long RoleId = (long)Session["roleID"];
            return PartialView("_NavBar",AccessRepo.NavBar(RoleId));
        }
        public ActionResult SideBar()
        {
            long RoleId = (long)Session["roleID"];
            return PartialView("_SideBar",AccessRepo.SideBar(RoleId));
        }

        public ActionResult ChangeAccess()
        {
            if(Session["userID"]!=null)
            {
                long sesion = (long)Session["userID"];
                ViewBag.CompanyList=new SelectList(AccessRepo.SelectCompany(sesion),"CompanyId","CompanyName");
                ViewBag.RoleList=new SelectList(AccessRepo.SelectRole(sesion),"RoleId","RoleName");
            }

            return PartialView("_ChangeAccess");
        }
        public ActionResult ConfirmLogin(long idRole, long idCompany)
        {
            Session["newRoleID"] = idRole;
            Session["newCompanyID"]=idCompany;
            return PartialView("_ConfirmLogin");
        }
        [HttpPost]
        public ActionResult ChangeAccess(LoginViewModel model)
        {
            model.id=(long)Session["userID"];
            ResponseResultLogin result = LoginRepo.cekAkunGantiAkses(model);
            if(result.Success)
            {
                Session["roleID"]=Session["newRoleID"];
                Session["companyID"]=Session["newCompanyID"];
                Session.Remove("newRoleID");
                Session.Remove("newCompanyID");
            }
            return Json(new
            {
                message = result.Message,
                success = result.Success
            },JsonRequestBehavior.AllowGet);
        }


    }

}