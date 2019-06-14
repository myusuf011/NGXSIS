using ngxsis.Repository;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ngxsis.MVC.Controllers
{
    public class DokumenController : Controller
    {
        // GET: Dokumen
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult DokumenList()
        //{
        //    return PartialView("_DokumenList",DokumenRepo.All() );
        //}

        //public ActionResult Create()
        //{
        //    return PartialView("_Create", new DokumenViewModel());
        //}

        //[HttpPost]
        //public ActionResult Create(DokumenViewModel model)
        //{
        //    ResponseResult result = DokumenRepo.Update(model);
        //    return Json(new
        //    {
        //        success = result.Success,
        //        message = result.Message,
        //        entity = result.Entity
        //    }, JsonRequestBehavior.AllowGet);

        //}
        //public ActionResult Edit(int id)
        //{
        //    return PartialView("_Edit", DokumenRepo.ById(id)); 
        //}

        //[HttpPost]
        //public ActionResult Edit(DokumenViewModel model)
        //{
        //    ResponseResult result = DokumenRepo.Update(model);
        //    return Json(new
        //    {
        //        success = result.Success,
        //        message = result.Message,
        //        entity = result.Entity
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult Delete(int id) // post
        //{
        //    return PartialView("_Delete", DokumenRepo.ById(id)); //habis ini di add view
        //}

        //[HttpPost]
        //public ActionResult Delete(DokumenViewModel model)
        //{
        //    ResponseResult result = DokumenRepo.Delete(model);
        //    return Json(new
        //    {
        //        success = result.Success,
        //        message = result.Message,
        //        entity = result.Entity
        //    }, JsonRequestBehavior.AllowGet);
        //}
    }
}