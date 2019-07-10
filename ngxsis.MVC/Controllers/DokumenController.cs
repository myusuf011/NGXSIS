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
    public class DokumenController:Controller
    {
        // GET: Dokumen
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DokumenList(long id)
        {
            Bio bio = new Bio();
            bio.id=id;
            bio.dokumen=DokumenRepo.All(id);
            return PartialView("_DokumenList",bio);
        }

        public ActionResult Create(long id)
        {
            TempData["BiodataID"]=id;
            return PartialView("_Create",new DokumenViewModel());
        }
        [HttpPost]
        public ActionResult Create(DokumenViewModel model)
        {
            long biodataId = (long)TempData.Peek("BiodataID");
            model.biodata_id=biodataId;
            long userId = (long)Session["userID"];
            if(model.file!=null||model.foto!=null)
            {
                ResponseResult validate = validateFile(model);
                if(validate.Success)//Validasi sukses
                {
                    model.file_path=Save(model);
                    ResponseResult result = DokumenRepo.Update(model,userId);
                    return Json(new
                    {
                        success = result.Success,
                        message = result.Message
                    },JsonRequestBehavior.AllowGet);
                }
                else//Validasi gagal
                {
                    return Json(new
                    {
                        success = validate.Success,
                        message = validate.Message
                    },JsonRequestBehavior.AllowGet);
                }
            }
            else//file kosong
            {
                return Json(new
                {
                    success = false,
                    message = "File kosong tidak dapat disimpan",
                },JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Edit(long id)
        {
            return PartialView("_Edit",DokumenRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Edit(DokumenViewModel model)
        {
            long userId = (long)Session["userID"];
            if(model.file!=null||model.foto!=null)
            {
                ResponseResult validate = validateFile(model);
                if(validate.Success)//Validasi sukses
                {
                    model.file_path=Save(model);
                    ResponseResult result = DokumenRepo.Update(model,userId);
                    return Json(new
                    {
                        success = result.Success,
                        message = result.Message
                    },JsonRequestBehavior.AllowGet);
                }
                else//Validasi gagal
                {

                    return Json(new
                    {
                        success = validate.Success,
                        message = validate.Message
                    },JsonRequestBehavior.AllowGet);
                }
            }else
            {
                DokumenViewModel data = DokumenRepo.ById(model.id);
                model.is_photo=data.is_photo;
                model.file_path=Save(model);
                ResponseResult result = DokumenRepo.Update(model,userId);
                return Json(new
                {
                    success = result.Success,
                    message = result.Message
                },JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Delete(long id)
        {
            return PartialView("_Delete",DokumenRepo.ById(id));
        }
        [HttpPost]
        public ActionResult Delete(DokumenViewModel model)
        {
            long userId = (long)Session["userID"];
            ResponseResult result = DokumenRepo.Delete(model,userId);
            return Json(new
            {
                success = result.Success,
                message = result.Message
            },JsonRequestBehavior.AllowGet);
        }

        ///////////////////////////////////////////////
        public ResponseResult validateFile(DokumenViewModel model)
        {
            ResponseResult result = new ResponseResult();
            bool size = true;
            bool extension = false;
            HttpPostedFileBase file = null;
            string[] ex;
            if(model.is_photo==true)
            {
                file=model.foto;
                ex=new string[] { ".jpg",".jpeg",".png" };
            }
            else
            {
                file=model.file;
                ex=new string[] { ".doc",".docx",".jpg",".jpeg",".pdf",".png",};
            }
            if(file.ContentLength>512000)
            {
                size=false;
                result.Message="Ukuran file terlalu besar max 500 KB";
                result.Success=result.Success&size;
            }
            foreach(var item in ex)
            {
                if(item==Path.GetExtension(file.FileName))
                {
                    extension=true;
                    break;
                }
            }
            if(extension==false)
            {
                result.Message="Format file tidak didukung";
                result.Success=result.Success&extension;
            }
            return result;
        }
        public string Save(DokumenViewModel model)
        {
            HttpPostedFileBase file = null;
            if(model.is_photo==true)
            {
                file=model.foto;
            }
            else
            {
                file=model.file;
            }

            string location = "/Dokumen/"+model.biodata_id+"/"+DateTime.Now.ToString("yyyyMMddHHmmss")+"-"+model.file_name;
            string name = model.file_name;
            if(file!=null)
            {
                string dir = location;
                string ex = Path.GetExtension(file.FileName);
                var path = Path.Combine(dir,name+ex);
                var PhysPath = Path.Combine(Server.MapPath(dir),name+ex);
                if(!Directory.Exists(Server.MapPath(dir)))
                {
                    Directory.CreateDirectory(Server.MapPath(dir));
                }
                file.SaveAs(PhysPath);
                return path;
            }
            else if(model.id!=0)
            {
                DokumenViewModel data = DokumenRepo.ById(model.id);
                if(data.file_name!=model.file_name)
                {
                    string newDir = location;
                    string dir = data.file_path;
                    string ex = dir.Split(new string[] { data.file_name },StringSplitOptions.None)[2];
                    var path = Path.Combine(newDir,name+ex);
                    var PhysPath = Path.Combine(Server.MapPath(newDir),name+ex);
                    if(!Directory.Exists(Server.MapPath(newDir)))
                    {
                        Directory.CreateDirectory(Server.MapPath(newDir));
                    }
                    System.IO.File.Move(Server.MapPath(dir),PhysPath);
                    DeleteEmptyDir(Server.MapPath("/Dokumen/"+model.biodata_id+"/"));
                    return path;
                }
                else
                {
                    var path = data.file_path;
                    return path;
                }

            }
            return string.Empty;
        }
        public void DeleteEmptyDir(string dir)
        {
            foreach(var item in Directory.GetDirectories(dir))
            {
                if(Directory.EnumerateFileSystemEntries(item).Any())
                {
                    DeleteEmptyDir(item);
                }
                if(!Directory.EnumerateFileSystemEntries(item).Any())
                {
                    Directory.Delete(item);
                }
            }
        }
    }
}