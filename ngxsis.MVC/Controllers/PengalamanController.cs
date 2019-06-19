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

        public ActionResult List(int biodata_id)
        {
            return PartialView("_List", PengalamanRepo.ByBiodataId(biodata_id));
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

        public JsonResult IsResignTimeValid(string join_month, string join_year, string resign_month, string resign_year)
        {
            return Json(PengalamanRepo.ValidationResignTime(join_month, join_year, resign_month, resign_year), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateProject()
        {
            ViewBag.TPeriodList = new SelectList(GetAll.AllTpriod(), "id", "name");

            return PartialView("_CreateProject", new RiwayatProyekModel());
        }


        [HttpPost]
        public ActionResult CreateProject(RiwayatProyekModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }

            ResponseResult result = PengalamanRepo.UpdateProject(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EditProject(long id)
        {
            ViewBag.TPeriodList = new SelectList(GetAll.AllTpriod(), "id", "name");

            return PartialView("_EditProject", PengalamanRepo.ProjectById(id));
        }

        [HttpPost]
        public ActionResult EditProject(RiwayatProyekModel model)
        {

            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }
            ResponseResult result = PengalamanRepo.UpdateProject(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteProjet(long id)
        {
            return PartialView("_DeleteProjet", PengalamanRepo.ProjectById(id));
        }


        [HttpPost]
        public ActionResult DeleteProjet(RiwayatProyekModel model)
        {
            ResponseResult result = PengalamanRepo.DeleteProject(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}