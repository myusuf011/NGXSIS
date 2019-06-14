using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ngxsis.Repository;
using ngxsis.ViewModel;

namespace ngxsis.MVC.Controllers
{
    public class KeluargaController : Controller
    {
        // GET: Keluarga
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List", KeluargaRepo.All()); // _list nama viewnya,categoryrepo2 objek yg dipanggil untuk  isi list

        }

        public ActionResult ListByFam(int id)
        {
            return PartialView("_ListByFam", KeluargaRepo.ByfamId(id));
        }


        public ActionResult Create()
        {
            ViewBag.KeluargaList = new SelectList(KeluargaRepo.jeniskelAll(), "family_tree_type_id", "name"); // value = id yg ditampilkan = name --->category list
            ViewBag.Keluarga1List = new SelectList(KeluargaRepo.hubkelAll(), "family_relation_id", "family_relation_name");
            ViewBag.Keluarga2List = new SelectList(KeluargaRepo.eduAll(), "education_level_id", "education_level_name");
            return PartialView("_Create", new KeluargaViewModel());  //add-view create
        }
        //public ActionResult ListByjeniskel()
        //{
        //    return PartialView("_ListByjeniskel", KeluargaRepo.All());
        //}

        //public ActionResult Create()
        //{
        //    ViewBag.KeluargaList = new SelectList(KeluargaRepo.jeniskelAll(), "family_tree_type_id", "famtreeName"); // value = id yg ditampilkan = name --->category list
        //    return PartialView("_Create", new KeluargaViewModel());  //add-view create
        //}

        [HttpPost]
        public ActionResult Create(KeluargaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "InValid"
                }, JsonRequestBehavior.AllowGet);
            }

            ResponseResult result = KeluargaRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }


        //edit
        // controller buat Add view edit
        public ActionResult Edit(int id)
        {
            ViewBag.KeluargaList = new SelectList(KeluargaRepo.jeniskelAll(), "family_tree_type_id", "name"); // value = id yg ditampilkan = name --->category list
            ViewBag.Keluarga1List = new SelectList(KeluargaRepo.hubkelAll(), "family_relation_id", "family_relation_name");
            ViewBag.Keluarga2List = new SelectList(KeluargaRepo.eduAll(), "education_level_id", "education_level_name");
            return PartialView("_Edit", KeluargaRepo.ById(id)); //ById dibikin di CategoryRepo dulu
        }
        [HttpPost]
        public ActionResult Edit(KeluargaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "InValid"
                }, JsonRequestBehavior.AllowGet);
            }

            ResponseResult result = KeluargaRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id) // post
        {
            return PartialView("_Delete", KeluargaRepo.ById(id)); //habis ini di add view
        }

        [HttpPost]
        public ActionResult Delete(KeluargaViewModel model)
        {
            ResponseResult result = KeluargaRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

    }
}