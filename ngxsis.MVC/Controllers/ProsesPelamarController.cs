using ngxsis.Repository;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ngxsis.MVC.Controllers
{
    public class ProsesPelamarController : Controller
    {
        // GET: ProsesPelamar
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(int desc = 0,int page = 0,int dataPerPage = 10)
        {
            return PartialView("_List", ProsesPelamarRepo.ProsesPelamarList(desc,page,dataPerPage));
        }

        [HttpPost]
        public ActionResult Edit(long bioId, long userId)
        {
            ResponseResult result = ProsesPelamarRepo.Edit(bioId, userId);
            return Json(new
            {
                success = result.Success
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Detail()
        {
            return PartialView("_Detail");
        }

        public ActionResult UndanganList(long bioId)
        {
            return PartialView("_UndanganList",ProsesPelamarRepo.Undangan(bioId));
        }
        public ActionResult JadwalList(long bioId)
        {
            return PartialView("_JadwalList",ProsesPelamarRepo.Jadwal(bioId));
        }
        [HttpPost]
        public ActionResult Delete(long id,long bioId, int type, long userId)
        {
            ResponseResult result = ProsesPelamarRepo.Delete(id,bioId, type, userId);
            return Json(new
            {
                success = result.Success
            }, JsonRequestBehavior.AllowGet);
        }
    }
}