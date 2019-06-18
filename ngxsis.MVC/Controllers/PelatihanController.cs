using ngxsis.Repository;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ngxsis.MVC.Controllers
{
    public class PelatihanController : Controller
    {
        // GET: Pelatihan
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int biodata_id)
        {
            return PartialView("_List", PelatihanRepo.ByBiodataId(biodata_id));
        }

        // CREATE
        public ActionResult Create()
        {
            ViewBag.PelatihanList = new SelectList(PelatihanRepo.PeriodAll(), "time_period_id", "time_period_name");
            return PartialView("_Create", new PelatihanViewModel());
        }

        [HttpPost]
        public ActionResult Create(PelatihanViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "Invalid"
                }, JsonRequestBehavior.AllowGet);
            }
            ResponseResult result = PelatihanRepo.Update(model);
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
            ViewBag.PelatihanList = new SelectList(PelatihanRepo.PeriodAll(), "time_period_id", "time_period_name");
            return PartialView("_Edit", PelatihanRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Edit(PelatihanViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "Invalid"
                }, JsonRequestBehavior.AllowGet);
            }
            ResponseResult result = PelatihanRepo.Update(model);
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
            return PartialView("_Delete", PelatihanRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Delete(PelatihanViewModel model)
        {
            ResponseResult result = PelatihanRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}