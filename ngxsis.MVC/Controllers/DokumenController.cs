using ngxsis.Repository;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ngxsis.MVC.Controllers
{
    public class DokumenController : Controller
    {
        // GET: Dokumen
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult DokumenList()
        //{
        //    return PartialView("_DokumenList",DokumenRepo.All() );
        //}
        public ActionResult DokumenList()
        {
            return PartialView("_DokumenList", DokumenRepo.All());
        }

        //public ActionResult Create()
        //{
        //    return PartialView("_Create", new DokumenViewModel());
        //}
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("_Create", new DokumenViewModel());
        }

        //[HttpPost]
        //public ActionResult Create(DokumenViewModel model)
        //{
        //    try
        //    {
        //        if (file.ContentLength > 0)
        //        {
        //            string _FileName = Path.GetFileName(file.FileName);
        //            string _path = Path.Combine(Server.MapPath("~/Content/Images"), _FileName);
        //            file.SaveAs(_path);
        //        }
        //        ViewBag.Message = "File Uploaded Successfully!!";
        //        return PartialView("_Create", new DokumenViewModel());
        //    }
        //    catch
        //    {
        //        ViewBag.Message = "File upload failed!!";
        //        return PartialView("_Create", new DokumenViewModel());
        //    }
        //}

        [HttpPost]
        public ActionResult CreateUpload()
        {
            string FileName = "";
            HttpFileCollectionBase files = Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";    
                //string filename = Path.GetFileName(Request.Files[i].FileName);    

                HttpPostedFileBase file = files[i];
                string fname;

                // Checking for Internet Explorer    
                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                {
                    string[] testfiles = file.FileName.Split(new char[] { '\\' });
                    fname = testfiles[testfiles.Length - 1];
                }
                else
                {
                    fname = file.FileName;
                    FileName = file.FileName;
                }

                // Get the complete folder path and store the file inside it.    
                fname = Path.Combine(Server.MapPath("~/Content/Images"), fname);
                file.SaveAs(fname);
            }
            return Json(FileName, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Create(HttpPostedFileBase file, string type = "f")
        //{
        //    try
        //    {
        //        if (file.ContentLength > 0)
        //        {
        //            string _FileName = Path.GetFileName(file.FileName);
        //            string _path = Path.Combine(Server.MapPath("~/Content/Images"), _FileName);
        //            file.SaveAs(_path);
        //        }
        //        ViewBag.Message = "File Uploaded Successfully!!";
        //        return PartialView("_Create", new DokumenViewModel());
        //    }
        //    catch
        //    {
        //        ViewBag.Message = "File upload failed!!";
        //        return PartialView("_Create", new DokumenViewModel());
        //    }
        //}

        //public ActionResult uploadFoto()
        //{
        //    return PartialView("_UploadFoto",)
        //}

        //[HttpGet]
        //public ActionResult UploadFile()
        //{
        //    return View();
        //}

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/Images"), _FileName);
                    file.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }
    }
}