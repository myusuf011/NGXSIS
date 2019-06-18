using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ngxsis.Repository;
using ngxsis.ViewModel;
using Rotativa;
using ngxsis.DataModel;
using Rotativa.Options;

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
        //    var report = new ActionAsPdf("List");
        //    return report;
        //}

        public ActionResult PrintViewToPdf()
        {
            List<ProfilViewModel> result = new List<ProfilViewModel>();
            using (var db = new ngxsisContext())
            {
                result = (from b in db.x_biodata
                          join pd in db.x_riwayat_pendidikan on b.id equals pd.biodata_id
                          join pk in db.x_riwayat_pekerjaan on b.id equals pk.biodata_id
                          join pl in db.x_riwayat_pelatihan on b.id equals pl.biodata_id
                          join ba in db.x_biodata_attachment on b.id equals ba.biodata_id
                          join s in db.x_sertifikasi on b.id equals s.biodata_id
                          join kt in db.x_keahlian on b.id equals kt.biodata_id
                          join sl in db.x_skill_level on kt.skill_level_id equals sl.id
                          select new ProfilViewModel
                          {
                              id = b.id,
                              FullName = b.fullname,
                              DOB = b.dob,
                              Gender = b.gender,
                              Photo = ba.file_path,

                              EntryYear = pd.entry_year,
                              GraduationYear = pd.graduation_year,
                              Major = pd.major,
                              GPA = pd.gpa,
                              Country = pd.country,
                              SchoolName = pd.school_name,

                              CompanyName = pk.company_name,
                              JoinYear = pk.join_year,
                              ResignYear = pk.resign_year,
                              LastPosition = pk.last_position,
                              Income = pk.income,

                              SkillName = kt.skill_name,
                              SkillLevel = sl.name,

                              TrainingName = pl.training_name,
                              TrainingYear = pl.training_year,

                              ValidStartYear = s.valid_start_year,
                              UntilYear = s.until_year,
                              CertificateName = s.certificate_name

                          }).ToList();
            }

            //string cusomtSwitches = string.Format("--print-media-type");

            return new PartialViewAsPdf("_List", result)
            {

                PageSize = Size.A4,
                FileName = "Profil-" + DateTime.Now.ToString("yyyyMMdd") + ".pdf",
                PageMargins = {Top = 20, Bottom = 20},
                //CustomSwitches = cusomtSwitches
            };
        }
    }
}