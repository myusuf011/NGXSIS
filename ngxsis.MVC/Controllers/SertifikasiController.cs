using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ngxsis.Repository;
using ngxsis.ViewModel;

namespace ngxsis.MVC.Controllers
{
    public class SertifikasiController : Controller
    {
        // GET: Sertifikasi
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List", SertifikasiRepo.All()); // _list nama viewnya,categoryrepo2 objek yg dipanggil untuk  isi list

        }

        public ActionResult Create()
        {
            return PartialView("_Create", new SertifikasiViewModel());  //add-view create
        }

        [HttpPost]
        public ActionResult Create(SertifikasiViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "InValid"
                }, JsonRequestBehavior.AllowGet);
            }

                ResponseResult result = SertifikasiRepo.Update(model);
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
            return PartialView("_Edit", SertifikasiRepo.ById(id)); //ById dibikin di CategoryRepo dulu
        }

        [HttpPost]
        public ActionResult Edit(SertifikasiViewModel model)
        {
            ResponseResult result = SertifikasiRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);


        }

        public ActionResult Delete(int id) // post
        {
            return PartialView("_Delete", SertifikasiRepo.ById(id)); //habis ini di add view
        }

        [HttpPost]
        public ActionResult Delete(SertifikasiViewModel model)
        {
            ResponseResult result = SertifikasiRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
