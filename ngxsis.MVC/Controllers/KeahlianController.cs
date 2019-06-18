using ngxsis.Repository;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ngxsis.MVC.Controllers
{
    public class KeahlianController : Controller
    {
        // GET: Keahlian
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int biodata_id)
        {
            return PartialView("_List", KeahlianRepo.ByBiodataId(biodata_id));
        }

        // CREATE
        public ActionResult Create()
        {
            ViewBag.KeahlianList = new SelectList(KeahlianRepo.LevelAll(), "skill_level_id", "skill_level_name"); //menggantikan category menjadi dropdown
            return PartialView("_Create", new KeahlianViewModel());
        }

        [HttpPost]
        public ActionResult Create(KeahlianViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "Invalid"
                }, JsonRequestBehavior.AllowGet);
            }

            ResponseResult result = KeahlianRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        // EDIT
        public ActionResult Edit(int id)
        {
            ViewBag.KeahlianList = new SelectList(KeahlianRepo.LevelAll(), "skill_level_id", "skill_level_name");
            return PartialView("_Edit", KeahlianRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Edit(KeahlianViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "Invalid"
                }, JsonRequestBehavior.AllowGet);
            }

            ResponseResult result = KeahlianRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        // DELETE
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", KeahlianRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Delete(KeahlianViewModel model)
        {
            ResponseResult result = KeahlianRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult IsSkillLevelExist(long? skill_level_id, int biodata_id, int id)
        //{
        //    return Json(KeahlianRepo.ValidationSkillLevel(skill_level_id, biodata_id, id), JsonRequestBehavior.AllowGet);
        //}
    }
}