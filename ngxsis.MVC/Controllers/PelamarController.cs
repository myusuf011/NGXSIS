using ngxsis.Repository;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ngxsis.MVC.Controllers
{
    public class PelamarController : Controller
    {
        // GET: Pelamar
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PelamarList(string search = "", int desc = 0, int page = 0, int dataPerPage = 10)
        {
            List<PelamarViewModel> result = PelamarRepo.GetBySearch(search, desc, page, dataPerPage);
            return PartialView("_PelamarList", result);
        }
        public ActionResult Create()
        {
            return PartialView("_Create", new PelamarViewModel());
        }
    }
}