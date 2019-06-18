using ngxsis.Repository;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ngxsis.MVC.Controllers
{
    public class LainLainController : Controller
    {
        // GET: LainLain
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListIndex(long idBiodata)
        {
            return PartialView("_ListIndex", LainLainRepo.SelectByBiodataID(idBiodata));
        }

        public ActionResult ListReferensi(long idBiodata)
        {
            return PartialView("_ListReferensi", LainLainRepo.SelectAllReferensi(idBiodata));
        }

        public ActionResult ListTambahan(long idBiodata)
        {
            return PartialView("_ListTambahan", LainLainRepo.SelectByBiodataID(idBiodata));
        }

        public ActionResult DeleteReferensi(long idBiodata)
        {
            return PartialView("_DeleteReferensi", LainLainRepo.SelectByBiodataID(idBiodata));
        }

        [HttpPost]
        public ActionResult DeleteReferensi(LainLainViewModel model)
        {
            long session = 0;
            ResponseResult result = new ResponseResult();

            if (Session["userID"] != null)
            {
                session = (long)Session["userID"];
                result = LainLainRepo.DeleteReferensi(model, session);
            }
            else
            {
                result.Success = false;
                result.Message = "Anda Harus Login Terlebih Dahulu";
            }                    

            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        
    }
}