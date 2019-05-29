using ngxsis.Repository;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ngxsis.MVC.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RoleList(string search = "", int desc=0, int page=0, int dataPerPage=10)
        {
            List<RoleViewModel> result = RoleRepo.BySearch(search, desc, page, dataPerPage);
            List<RoleViewModel> selectedResult = new List<RoleViewModel>();
            int start = 0;
            int end = 0;
            int maxPage = result.Count() / dataPerPage;
            //Set Maks Page
            if (result.Count() % dataPerPage == 0)
            {
                maxPage -= 1;
            }
            //Set starting point
            if (page * dataPerPage < result.Count())
            {
                start = page * dataPerPage;
            }
            else
            {
                start = result.Count() - (result.Count() % dataPerPage);
            }
            //Set end point
            if (start + dataPerPage <= result.Count())
            {
                end = start + dataPerPage;
            }
            else
            {
                end = result.Count();
            }
            //Show data
            for (int i = start; i < end; i++)
            {
                selectedResult.Add(result[i]);
            }

            ViewBag.MaxPage = maxPage;
            return PartialView("_RoleList", selectedResult);
        }
        public ActionResult Create()
        {
            return PartialView("_Create", new RoleViewModel());
        }

        public ActionResult Edit(long Id)
        {
            return PartialView("_Edit", RoleRepo.ById(Id));
        }
        public ActionResult Delete(long Id)
        {
            return PartialView("_Delete", RoleRepo.ById(Id));
        }

        [HttpPost]
        public ActionResult Create(RoleViewModel model)
        {
            ResponseResult result = RoleRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Edit(RoleViewModel model)
        {
            ResponseResult result = RoleRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Delete(RoleViewModel model)
        {
            ResponseResult result = RoleRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}