using ngxsis.Repository;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ngxsis.MVC.Controllers
{
    public class AktivasiAkunController : Controller
    {
        // GET: AktivasiAkun
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListIndex(long idBiodata)
        {
            AktivasiAkunViewModel model = new AktivasiAkunViewModel();
            model.biodata_id = idBiodata;

            return PartialView("_ListIndex", model);
        }

        public ActionResult Akun(long idBiodata)
        {
            return PartialView("_Akun", AktivasiAkunRepo.SelectAkun(idBiodata));
        }

        public ActionResult ListTest(long idBiodata)
        {
            return PartialView("_ListTest", AktivasiAkunRepo.SelectAllTest(idBiodata));
        }

        public ActionResult ListTestDetail(long id)
        {
            ViewBag.idTest = id;
            return PartialView("_ListTestDetail");
        }

        public ActionResult TabelListTesDetail(long idTes)
        {
            return PartialView("_TabelListTesDetail", AktivasiAkunRepo.SelectAllTestDetail(idTes));
        }

        public ActionResult TambahTes(long idTes)
        {
            ViewBag.JenisTesList = new SelectList(AktivasiAkunRepo.SelectTypeTestAll(), "test_type_id", "name_type");
            AktivasiAkunViewModel model = new AktivasiAkunViewModel();
            model.online_test_id = idTes;

            return PartialView("_TambahTes", model);
        }

        [HttpPost]
        public ActionResult TambahTes(AktivasiAkunViewModel model)
        {
            long session = (long)Session["userID"];

            ResponseResult result = AktivasiAkunRepo.CreateTesDetail(model, session);
            return Json(new {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteTesDetail(long idTesDetail, string namaTes)
        {
            AktivasiAkunViewModel model = new AktivasiAkunViewModel();
            model.name_type = namaTes;
            model.online_test_detail_id = idTesDetail;

            return PartialView("_DeleteTesDetail", model);
        }

        [HttpPost]
        public ActionResult DeleteTesDetail(AktivasiAkunViewModel model)
        {
            long session = (long)Session["userID"];

            ResponseResult result = AktivasiAkunRepo.DeleteTesDetail(model, session);

            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        
    }
}