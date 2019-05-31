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
        public ActionResult RoleList(string search = "", int desc=0, int page=0, int dataPerPage=10)
        {
            List<RoleViewModel> result = RoleRepo.BySearch(search, desc, page, dataPerPage);
            return PartialView("_RoleList", result);
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
                success = RoleRepo.RelationCheck(model.Id),
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsNameUnique(string Name, int Id=0)
        {
            return Json(RoleRepo.ByName(Name, Id),JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsCodeUnique(string Code, int Id=0)
        {
            return Json(RoleRepo.ByCode(Code, Id),JsonRequestBehavior.AllowGet);
        }
    }
}