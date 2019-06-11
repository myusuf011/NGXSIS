using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ngxsis.Repository;
using ngxsis.ViewModel;
//using Rotativa;
//using Rotativa.Options;
using ngxsis.DataModel;

namespace ngxsis.MVC.Controllers
{
    public class ProfilController : Controller
    {
        // GET: Profil
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List", ProfilRepo.All());
        }


        //public ActionResult PrintViewToPdf()
        //{
        //    var report = new Rotativa.ActionAsPdf("Index");
        //    return report;
        //}
    }
}