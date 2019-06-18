using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ngxsis.Repository;
using ngxsis.ViewModel;

namespace ngxsis.MVC.Controllers
{
    public class CatatanController : Controller
    {
     
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int biodata_id) //karena nampilkan list biodata_id
        {
            return PartialView("_List", CatatanRepo.ByBiodataId(biodata_id));  // _list nama viewnya,categoryrepo2 objek yg dipanggil untuk  isi list

        }

        public ActionResult Create()
        {
            ViewBag.CatatanList = new SelectList(CatatanRepo.jeniscatAll(), "note_type_id", "note_type_name");
            return PartialView("_Create", new CatatanViewModel());  //add-view create
        }

        [HttpPost]
        public ActionResult Create(CatatanViewModel model)
  
        {
            //if (!ModelState.IsValid)
            //{
            //    return Json(new
            //    {
            //        success = false,
            //        message = "Invalid"
            //    }, JsonRequestBehavior.AllowGet);
            //}
            ResponseResult result = CatatanRepo.Update(model);
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
            ViewBag.CatatanList = new SelectList(CatatanRepo.jeniscatAll(), "note_type_id", "note_type_name");
            return PartialView("_Edit", CatatanRepo.ById(id)); //ById dibikin di CategoryRepo dulu
        }


        [HttpPost]
        public ActionResult Edit(CatatanViewModel model)
        {


            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "InValid"
                }, JsonRequestBehavior.AllowGet);
            }

            ResponseResult result = CatatanRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Delete(int id) // post
        {
            return PartialView("_Delete", CatatanRepo.ById(id)); //habis ini di add view
        }

        [HttpPost]
        public ActionResult Delete(CatatanViewModel model)
        {
            ResponseResult result = CatatanRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
