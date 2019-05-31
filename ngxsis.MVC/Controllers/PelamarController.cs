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
        public ActionResult PelamarList(string search = "")
        {
            return PartialView("_PelamarList", PelamarRepo.GetBySearch(search));
        }
        public ActionResult Create()
        {
            return PartialView("_Create", new PelamarViewModel());
        }
    }
}