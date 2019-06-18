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
      
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(BiodataViewModel model)
        {
            if (!ModelState.IsValid || !PelamarRepo.ValidationIdentity(model.identity_no, model.identity_type_id, model.id).Success || !PelamarRepo.ValidationMail(model.email, model.id).Success ||
                           !PelamarRepo.ValidationPhone(model.phone_number1, model.id).Success)
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
        public ActionResult VerifyEmail(string email, long id=0)
        {


            return Json(PelamarRepo.ValidationMail(email, id).Success);
        }

        [HttpPost]
        public ActionResult VerifyPhone(string phone_number1, long id=0)
        {


            return Json(PelamarRepo.ValidationPhone(phone_number1, id).Success);
        }

        [HttpPost]
        public ActionResult VerifyIdentity(string identity_no, long identity_type_id, long id=0)
        {


            if (!PelamarRepo.ValidationIdentity(identity_no, identity_type_id, id).Success)

            {
                return Json(PelamarRepo.ValidationIdentity(identity_no, identity_type_id, id).Message);
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
   
            return PartialView("_Edit", PelamarRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Edit(BiodataViewModel model)
        {
            if (!ModelState.IsValid || !PelamarRepo.ValidationIdentity(model.identity_no, model.identity_type_id, model.id).Success || !PelamarRepo.ValidationMail(model.email, model.id).Success ||
                           !PelamarRepo.ValidationPhone(model.phone_number1, model.id).Success)
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