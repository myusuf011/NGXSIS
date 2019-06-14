using ngxsis.Repository;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ngxsis.MVC.Controllers
{
    public class OrganisasiController : Controller
    {
        // GET: Organisasi
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List", OrganisasiRepo.All());
        }

        // CREATE
        public ActionResult Create()
        {
            return PartialView("_Create", new OrganisasiViewModel());
        }

        [HttpPost]
        public ActionResult Create(OrganisasiViewModel model)
        {
            if (!ModelState.IsValid || int.Parse(model.exit_year) < int.Parse(model.entry_year))
            {
                return Json(new
                {
                    success = false,
                    message = "Invalid"
                }, JsonRequestBehavior.AllowGet);
            }

            ResponseResult result = OrganisasiRepo.Update(model);
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
            return PartialView("_Edit", OrganisasiRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Edit(OrganisasiViewModel model)
        {
            if (!ModelState.IsValid || int.Parse(model.exit_year) < int.Parse(model.entry_year))
            {
                return Json(new
                {
                    success = false,
                    message = "Invalid"
                }, JsonRequestBehavior.AllowGet);
            }

            ResponseResult result = OrganisasiRepo.Update(model);
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
            return PartialView("_Delete", OrganisasiRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Delete(OrganisasiViewModel model)
        {
            ResponseResult result = OrganisasiRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult IsExitYearValid(string exit_year, string entry_year)
        //{
        //    return Json(OrganisasiRepo.ValidationExitYear(exit_year, entry_year), JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult IsEntryYearValid(string entry_year, string exit_year)
        //{
        //    return Json(OrganisasiRepo.ValidationExitYear(entry_year, exit_year), JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public ActionResult IsExitYearValid(string exit_year, string entry_year)
        //{
        //    if (!OrganisasiRepo.ValidationExitYear(exit_year, entry_year).Success)
        //    {
        //        return Json(OrganisasiRepo.ValidationExitYear(exit_year, entry_year).Message);
        //    }
        //    return Json(true);
        //}
    }
}