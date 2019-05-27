using ngxsis.Repository;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ngxsis.MVC.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RoleList(string search = "")
        {
            return PartialView("_RoleList", RoleRepo.BySearch(search));
        }
        public ActionResult Create()
        {
            return PartialView("_Create", new RoleViewModel());
        }

        public ActionResult Edit(long Id)
        {
            return PartialView("_Edit", RoleRepo.ById(Id));
        }
        public ActionResult Delete(long Id)
        {
            return PartialView("_Delete", RoleRepo.ById(Id));
        }

        [HttpPost]
        public ActionResult Create(RoleViewModel model)
        {
            ResponseResult result = RoleRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Edit(RoleViewModel model)
        {
            ResponseResult result = RoleRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Delete(RoleViewModel model)
        {
            ResponseResult result = RoleRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}