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
            ViewBag.maritalList = new SelectList(GetAll.AllMarital(), "id", "name");
            ViewBag.identityList = new SelectList(GetAll.AllIdentity(), "id", "name");
             ViewBag.religionList = new SelectList(GetAll.AllReligion(), "id", "name");
            ViewBag.yearList = new SelectList(PelamarRepo.YearMarriage());
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(BiodataViewModel model)
        {
            ResponseResult result = PelamarRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity

            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult VerifyEmail(string email)
        {


            return Json(PelamarRepo.ValidationMail(email).Success);
        }

        [HttpPost]
        public ActionResult VerifyPhone(string phone_number1)
        {


            return Json(PelamarRepo.ValidationPhone(phone_number1).Success);
        }

        [HttpPost]
        public ActionResult VerifyIdentity(string identity_no, long identity_type_id)
        {


            if (!PelamarRepo.ValidationIdentity(identity_no, identity_type_id).Success)

            {
                return Json(PelamarRepo.ValidationIdentity(identity_no, identity_type_id).Message);
            }
            return Json(true);
        }

    }
}