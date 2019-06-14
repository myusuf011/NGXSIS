using ngxsis.ViewModel;
using ngxsis.DataModel;
using ngxsis.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ngxsis.MVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel userModel)
        {
            ResponseResultLogin hasil = LoginRepo.cekAkun(userModel);            

            if (Session[hasil.NamaAkun + "Gagal"] == null)
            {                
                Session[hasil.NamaAkun + "Gagal"] = 0;
            }

            string hariIni = DateTime.Now.ToString("dd/MM/yyyy");
            string hariUbah = hasil.TanggalUbah?.ToString("dd/MM/yyyy");
            bool blokir = hasil.Blokir;

            if (hariUbah != hariIni)
            {
                ResponseResultLogin akunAktif = LoginRepo.aktifinAkun(hasil.AkunID);
                hariUbah = akunAktif.TanggalUbah?.ToString("dd/MM/yyyy");
                blokir = akunAktif.Blokir;
                Session[hasil.NamaAkun + "Gagal"] = 0;
            }

            if ((int)Session[hasil.NamaAkun + "Gagal"] < 3 || hariUbah == hariIni && blokir == false)
            {
                if (hasil.Success == false)
                {
                    if (hasil.GagalLogin == true)
                    {
                        int jumlahGagal = (int)Session[hasil.NamaAkun + "Gagal"] + 1;                        
                        Session[hasil.NamaAkun + "Gagal"] = jumlahGagal;
                        hasil.Message = "Invalid Email / Password \nKesempatan mencoba " + (3 - jumlahGagal) + "x lagi";
                    }
                }
                else
                {
                    Session["userID"] = hasil.AkunID;
                    Session["userName"] = hasil.NamaAkun;
                }
            }

            if ((int) Session[hasil.NamaAkun + "Gagal"] >= 3 || hariUbah == hariIni && blokir == true)
            {
                ResponseResultLogin akunBlokir = LoginRepo.blokirAkun(hasil.AkunID);                
                hariUbah = akunBlokir.TanggalUbah?.ToString("dd/MM/yyyy");
                blokir = akunBlokir.Blokir;
                Session.Remove("userID");
                Session.Remove("userName");
                hasil.Message = "Akun " + hasil.NamaAkun + " terkunci \nSilahkan coba esok hari";
                hasil.Success = false;
            }

            return Json(new
            {
                success = hasil.Success,
                message = hasil.Message
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Logout()
        {
            Session.Remove("userID");
            Session.Remove("userName");
            Session.Remove("roleID");
            Session.Remove("companyID");
            return RedirectToAction("Index", "Login");
        }
    }
}