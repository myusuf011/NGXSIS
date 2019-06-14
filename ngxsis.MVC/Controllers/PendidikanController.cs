using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ngxsis.Repository;
using ngxsis.ViewModel;

namespace ngxsis.MVC.Controllers
{
    public class PendidikanController : Controller
    {
        // GET: Pendidikan
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List", PendidikanRepo.All()); // _list nama viewnya,categoryrepo2 objek yg dipanggil untuk  isi list

        }

        public ActionResult Create()
        {
            ViewBag.PendidikanList = new SelectList(PendidikanRepo.jenjangAll(), "education_level_id", "educationName"); // value = id yg ditampilkan = name --->category list
            return PartialView("_Create", new PendidikanViewModel());  //add-view create
        }

        public ActionResult ListByEdu()
        {
            return PartialView("_ListByEdu", PendidikanRepo.All());
        }


        [HttpPost]
        public ActionResult Create(PendidikanViewModel model)
        {
            if (!ModelState.IsValid || (int.Parse(model.graduation_year) < int.Parse(model.entry_year) && model.graduation_year!=null && model.entry_year!=null) )
            {
                return Json(new
                {
                    success = false,
                    message = "InValid"
                }, JsonRequestBehavior.AllowGet);
            }

            ResponseResult result = PendidikanRepo.Update(model);
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
            ViewBag.PendidikanList = new SelectList(PendidikanRepo.jenjangAll(), "education_level_id", "educationName"); // value = id yg ditampilkan = name --->category list
            return PartialView("_Edit", PendidikanRepo.ById(id)); //ById dibikin di CategoryRepo dulu
        }


        [HttpPost]
        public ActionResult Edit(PendidikanViewModel model)
        {
            if (!ModelState.IsValid || int.Parse(model.graduation_year) < int.Parse(model.entry_year))
            {
                return Json(new
                {
                    success = false,
                    message = "InValid"
                }, JsonRequestBehavior.AllowGet);
            }


            ResponseResult result = PendidikanRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Delete(int id) // post
        {
            return PartialView("_Delete", PendidikanRepo.ById(id)); //habis ini di add view
        }

        [HttpPost]
        public ActionResult Delete(PendidikanViewModel model)
        {
            ResponseResult result = PendidikanRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

    }
}