using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ngxsis.Repository;
using ngxsis.ViewModel;

namespace ngxsis.MVC.Controllers
{
    public class AccountController:Controller
    {
        // GET: Account
        public ActionResult SelectAccess()
        {
            if(Session["userID"]!=null)
            {
                long sesion = (long)Session["userID"];
                ViewBag.CompanyList=new SelectList(AccessRepo.SelectCompany(sesion),"CompanyId","CompanyName");
                ViewBag.RoleList=new SelectList(AccessRepo.SelectRole(sesion),"RoleId","RoleName");
            }

            return View();
        }

        [HttpPost]
        public ActionResult SetSessionAkses(long idRole,long idCompany)
        {
            Session["roleID"]=idRole;
            Session["companyID"]=idCompany;

            ResponseResult result = new ResponseResult();

            return Json(new
            {
                success = result.Success
            },JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveSessionAkses()
        {
            Session.Remove("roleID");
            Session.Remove("companyID");

            return RedirectToAction("SelectAccess","Account");
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
            long session = (long)Session["userID"];
            ViewBag.CompanyList=new SelectList(AccessRepo.SelectCompany(session),"CompanyId","CompanyName");
            ViewBag.RoleList=new SelectList(AccessRepo.SelectRole(session),"RoleId","RoleName");
            return PartialView("_ChangeAccess");
        }
        public ActionResult ConfirmLogin(long idRole,long idCompany)
        {
            Session["newRoleID"]=idRole;
            Session["newCompanyID"]=idCompany;
            return PartialView("_ConfirmLogin");
        }
        [HttpPost]
        public ActionResult ChangeAccess(LoginViewModel model)
        {
            model.id=(long)Session["userID"];
            Session["Fail"]=0;
            ResponseResultLogin result = LoginRepo.cekAkunGantiAkses(model);
            if(result.Success)
            {
                Session["roleID"]=Session["newRoleID"];
                Session["companyID"]=Session["newCompanyID"];
                Session.Remove("newRoleID");
                Session.Remove("newCompanyID");
            }else if(result.Message=="1")
            {
                Session[result.NamaAkun+"Gagal"]= (int)Session[result.NamaAkun+"Gagal"]+1;
                result.Message="Email/Password salah!\nKesempatan salah sebanyak "+(3 - (int)Session[result.NamaAkun+"Gagal"])+" kali lagi. Lebih dari itu, akun "+result.NamaAkun+" akan terkunci.";
                if((int)Session[result.NamaAkun+"Gagal"]==3)
                {
                    result.Message="Akun anda sudah terkunci!\nHubungi admin untuk mengaktifkan akun";
                    ResponseResultLogin blokir = LoginRepo.blokirAkun(model.id);
                    result.Blokir=blokir.Blokir;
                    Session.RemoveAll();
                }
            }
            return Json(new
            {
                blokir = result.Blokir,
                message = result.Message,
                success = result.Success
            },JsonRequestBehavior.AllowGet);
        }


    }

}