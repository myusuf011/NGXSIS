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


        public ActionResult PelamarList(string search = "", int desc = 0, int page = 0, int dataPerPage = 5)
        {
            List<PelamarViewModel> result = PelamarRepo.GetBySearch(search, desc, page, dataPerPage);
            return PartialView("_PelamarList", result);
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
            if (!ModelState.IsValid || !PelamarRepo.ValidationIdentity(model.identity_no, model.identity_type_id, model.dob).Success || !PelamarRepo.ValidationMail(model.email, model.dob).Success ||
                           !PelamarRepo.ValidationPhone(model.phone_number1, model.dob).Success)
            {
                return PartialView(model);
            }

            ResponseResult result = PelamarRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity

            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult VerifyEmail(string email, DateTime dob)
        {


            return Json(PelamarRepo.ValidationMail(email, dob).Success);
        }

        [HttpPost]
        public ActionResult VerifyPhone(string phone_number1, DateTime dob)
        {


            return Json(PelamarRepo.ValidationPhone(phone_number1, dob).Success);
        }

        [HttpPost]
        public ActionResult VerifyIdentity(string identity_no, long identity_type_id, DateTime dob)
        {


            if (!PelamarRepo.ValidationIdentity(identity_no, identity_type_id, dob).Success)

            {
                return Json(PelamarRepo.ValidationIdentity(identity_no, identity_type_id, dob).Message);
            }
            return Json(true);
        }


        public ActionResult IndexDetail(long id)
        {
            return PartialView("_IndexDetail", PelamarRepo.ById(id));
        }


        public ActionResult BioList(long id)
        {
            return PartialView("_BioList", PelamarRepo.ById(id));
        }


        public ActionResult Edit(long id)
        {

            ViewBag.maritalList = new SelectList(GetAll.AllMarital(), "id", "name");
            ViewBag.identityList = new SelectList(GetAll.AllIdentity(), "id", "name");
            ViewBag.religionList = new SelectList(GetAll.AllReligion(), "id", "name");
            ViewBag.yearList = new SelectList(PelamarRepo.YearMarriage());
            return PartialView("_Edit", PelamarRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Edit(BiodataViewModel model)
        {
            if (!ModelState.IsValid || !PelamarRepo.ValidationIdentity(model.identity_no, model.identity_type_id, model.dob).Success || !PelamarRepo.ValidationMail(model.email, model.dob).Success ||
                           !PelamarRepo.ValidationPhone(model.phone_number1, model.dob).Success)
            {
                return PartialView(model);
            }


            ResponseResult result = PelamarRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity

            }, JsonRequestBehavior.AllowGet);
        }
    }
}