using ngxsis.Repository;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ngxsis.MVC.Controllers
{
    public class PenjadwalanUndanganController : Controller
    {
        // GET: PenjadwalanUndangan
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string search = "", int desc = 0, int page = 0, int dataPerPage = 10)
        {
            List<PenjadwalanUndanganViewModel> result = PenjadwalanUndanganRepo.GetBySearch(search, desc, page, dataPerPage);
            return PartialView("_List", result);
        }

        // tampilkan undangan detail
        public ActionResult undangandetailList(long id)
        {
            return PartialView("_undangandetailList", PenjadwalanUndanganRepo.undangandetail(id));
        }

        public ActionResult Create()
        {
            ViewBag.PenjadwalanUndanganList = new SelectList(PenjadwalanUndanganRepo.pelamarAll(), "biodata_id", "fullname");
            ViewBag.PenjadwalanUndangan1List = new SelectList(PenjadwalanUndanganRepo.jenisundanganAll(), "schedule_type_id", "schedule_type_name");
            ViewBag.PenjadwalanUndangan2List = new SelectList(PenjadwalanUndanganRepo.roAll(), "ro", "fullname");
            ViewBag.PenjadwalanUndangan3List = new SelectList(PenjadwalanUndanganRepo.troAll(), "tro", "fullname");
            return PartialView("_Create", new PenjadwalanUndanganViewModel());
        }


        [HttpPost]
        public ActionResult Create(PenjadwalanUndanganViewModel model)
        {
           
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "InValid"
                }, JsonRequestBehavior.AllowGet);
            }

            ResponseResult result = PenjadwalanUndanganRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Edit(int id)
        {
            ViewBag.PenjadwalanUndanganList = new SelectList(PenjadwalanUndanganRepo.pelamarAll(), "biodata_id", "fullname");
            ViewBag.PenjadwalanUndangan1List = new SelectList(PenjadwalanUndanganRepo.jenisundanganAll(), "schedule_type_id", "schedule_type_name");
            ViewBag.PenjadwalanUndangan2List = new SelectList(PenjadwalanUndanganRepo.roAll(), "ro", "fullname");
            ViewBag.PenjadwalanUndangan3List = new SelectList(PenjadwalanUndanganRepo.troAll(), "tro", "fullname");
            return PartialView("_Edit", PenjadwalanUndanganRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Edit(PenjadwalanUndanganViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "InValid"
                }, JsonRequestBehavior.AllowGet);
            }

            ResponseResult result = PenjadwalanUndanganRepo.Update(model);
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
            return PartialView("_Delete", PenjadwalanUndanganRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Delete(PenjadwalanUndanganViewModel model)
        {
            ResponseResult result = PenjadwalanUndanganRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        //validasi
        public JsonResult IsTanggalUndanganValid(DateTime? invitation_date)
        {
            return Json(PenjadwalanUndanganRepo.ValidationTanggalUndangan(invitation_date), JsonRequestBehavior.AllowGet);
        }


        public JsonResult IsValidRO(long id, long ro, DateTime invitation_date, string time)
        {
            return Json(PenjadwalanUndanganRepo.ROValidation(id, ro, invitation_date, time), JsonRequestBehavior.AllowGet);
        }


        public JsonResult IsValidTRO(long id, long tro, DateTime invitation_date, string time)
        {
            return Json(PenjadwalanUndanganRepo.TROValidation(id, tro, invitation_date, time), JsonRequestBehavior.AllowGet);
        }
    }

}