using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ngxsis.Repository;
using ngxsis.ViewModel;

namespace ngxsis.MVC.Controllers
{
    public class SumberLokerController : Controller
    {
        // GET: SumberLoker
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List", SumberLokerRepo.All());
        }

        public ActionResult Create()
        {
            return PartialView("_Create", new SumberLokerViewModel());
        }

        [HttpPost]
        public ActionResult Create(SumberLokerViewModel model)
        {

            ResponseResult result = SumberLokerRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            return PartialView("_Edit", SumberLokerRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Edit(SumberLokerViewModel model)
        {
            ResponseResult result = SumberLokerRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}