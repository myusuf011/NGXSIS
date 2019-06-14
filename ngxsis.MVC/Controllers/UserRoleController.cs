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

        public ActionResult BioBySearch(string search = "0")
        {
            return PartialView("_BioBySearch", UserRoleRepo.BySearch(search));
        }

        public ActionResult RoleList(long addrbookId = 0)
        {
            ViewBag.RoleList=UserRoleRepo.RoleById(addrbookId);
            return PartialView("_RoleList", UserRoleRepo.All());
        }

        public ActionResult Form(long bioId = 0)
        {
            return PartialView("_Form",UserRoleRepo.UserById(bioId));
        }

        [HttpPost]
        public ActionResult Form(UserViewModel model)
        {
            ResponseResult result = UserRoleRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            },JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(long bioId)
        {
            UserViewModel model = UserRoleRepo.UserById(bioId);
            return PartialView("_Delete",model);
        }
        [HttpPost]
        public ActionResult Delete(UserViewModel model)
        {
            ResponseResult result = UserRoleRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            },JsonRequestBehavior.AllowGet);
        }

        public JsonResult PassVerif(string UserLoginPwd = "",int UserLoginId = 0)
        {
            return Json(UserRoleRepo.PassVerif(UserLoginPwd,UserLoginId),JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsUsernameUnique(string Username, int Id = 0)
        {
            return Json(UserRoleRepo.ByUsername(Username, Id), JsonRequestBehavior.AllowGet);
        }
    }
}