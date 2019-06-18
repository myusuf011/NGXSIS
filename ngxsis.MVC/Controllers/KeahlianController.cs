using ngxsis.Repository;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ngxsis.MVC.Controllers
{
    public class KeahlianController:Controller
    {
        // GET: Keahlian
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult List()
        //{
        //    return PartialView("_List", KeahlianRepo.All());
        //}

        public ActionResult List(int biodata_id)
        {
            return PartialView("_List",KeahlianRepo.ByBiodataId(biodata_id));
        }

        //public ActionResult ListBySkillLevel(int id)
        //{
        //    return PartialView("_ListBySkillLevel", KeahlianRepo.All());
        //}

        // CREATE
        public ActionResult Create()
        {
            ViewBag.KeahlianList=new SelectList(KeahlianRepo.LevelAll(),"skill_level_id","skill_level_name"); //menggantikan category menjadi dropdown
            return PartialView("_Create",new KeahlianViewModel());
        }

        [HttpPost]
        public ActionResult Create(KeahlianViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "Invalid"
                },JsonRequestBehavior.AllowGet);
            }

            ResponseResult result = KeahlianRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            },JsonRequestBehavior.AllowGet);
        }

        // EDIT
        public ActionResult Edit(int id)
        {
            ViewBag.KeahlianList=new SelectList(KeahlianRepo.LevelAll(),"skill_level_id","skill_level_name");
            return PartialView("_Edit",KeahlianRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Edit(KeahlianViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "Invalid"
                },JsonRequestBehavior.AllowGet);
            }

            ResponseResult result = KeahlianRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            },JsonRequestBehavior.AllowGet);
        }

        // DELETE
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete",KeahlianRepo.ById(id));
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
            },JsonRequestBehavior.AllowGet);
        }
    }
}