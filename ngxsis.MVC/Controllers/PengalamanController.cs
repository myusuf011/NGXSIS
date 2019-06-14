using ngxsis.Repository;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ngxsis.MVC.Controllers
{
    public class PengalamanController : Controller
    {
        // GET: Pengalaman
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List", PengalamanRepo.All());
        }

        // CREATE
        public ActionResult Create()
        {
            return PartialView("_Create", new PengalamanViewModel());
        }

        [HttpPost]
        public ActionResult Create(PengalamanViewModel model)
        {
            if (!string.IsNullOrEmpty(model.resign_month) || !string.IsNullOrEmpty(model.resign_year))
            {
                if (int.Parse(model.resign_year) < int.Parse(model.join_year) || (int.Parse(model.resign_year) == int.Parse(model.join_year) && int.Parse(model.resign_month) < int.Parse(model.join_month)))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Invalid"
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "Invalid"
                }, JsonRequestBehavior.AllowGet);
            }

            ResponseResult result = PengalamanRepo.Update(model);
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
            return PartialView("_Edit", PengalamanRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Edit(PengalamanViewModel model)
        {
            if (!string.IsNullOrEmpty(model.resign_month) || !string.IsNullOrEmpty(model.resign_year))
            {
                if (int.Parse(model.resign_year) < int.Parse(model.join_year) || (int.Parse(model.resign_year) == int.Parse(model.join_year) && int.Parse(model.resign_month) < int.Parse(model.join_month)))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Invalid"
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "Invalid"
                }, JsonRequestBehavior.AllowGet);
            }

            ResponseResult result = PengalamanRepo.Update(model);
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
            return PartialView("_Delete", PengalamanRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Delete(PengalamanViewModel model)
        {
            ResponseResult result = PengalamanRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}