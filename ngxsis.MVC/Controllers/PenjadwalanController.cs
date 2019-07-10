using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ngxsis.Repository;
using ngxsis.ViewModel;
using System.Net.Mail;

namespace ngxsis.MVC.Controllers
{
    public class PenjadwalanController : Controller
    {
        // Bagian Penjadwalan Rencana

        // GET: Penjadwalan
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(DateTime dari, DateTime sampai, int desc = 0, int page = 0, int dataPerPage = 100)
        {
            return PartialView("_List", PenjadwalanRepo.All(dari, sampai, desc, page, dataPerPage));
        }

        public ActionResult ListPelamar(long id = 0)
        {

            ViewBag.RoleList = PenjadwalanRepo.ByIdPel(id);
            return PartialView("_ListPelamar", PenjadwalanRepo.AllPelamar());
        }

        public ActionResult Edit(long id)
        {
            ViewBag.ROList = new SelectList(PenjadwalanRepo.AllRO(), "id", "fullname");
            ViewBag.TROList = new SelectList(PenjadwalanRepo.AllTRO(), "id", "fullname");
            ViewBag.JenisJadwalList = new SelectList(PenjadwalanRepo.AllJenisJadwal(), "id", "name");
            return PartialView("_Edit", PenjadwalanRepo.ByIdJdw(id));

        }

        [HttpPost]
        public ActionResult Edit(RencanaJadwalModel model)
        {
            if (!PenjadwalanRepo.ValidationDate(model.ro, model.time, model.schedule_date, model.id) || !PenjadwalanRepo.Validationtro(model.tro, model.time, model.schedule_date, model.id))
            {
                return PartialView(model);
            }
            ResponseResult result = PenjadwalanRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.ROList = new SelectList(PenjadwalanRepo.AllRO(), "id", "fullname");
            ViewBag.TROList = new SelectList(PenjadwalanRepo.AllTRO(), "id", "fullname");
            ViewBag.JenisJadwalList = new SelectList(PenjadwalanRepo.AllJenisJadwal(), "id", "name");
            return PartialView("_Create", new RencanaJadwalModel());
        }



        [HttpPost]
        public ActionResult Create(RencanaJadwalModel model)
        {
            if (!PenjadwalanRepo.ValidationDate(model.ro, model.time, model.schedule_date, model.id) || !PenjadwalanRepo.Validationtro(model.tro, model.time, model.schedule_date, model.id))
            {
                return PartialView(model);
            }

            ResponseResult result = PenjadwalanRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Delete(long id)
        {
            RencanaJadwalModel model = PenjadwalanRepo.ByIdJdw(id);
            return PartialView("_Delete", model);
        }

        [HttpPost]

        public ActionResult Delete(RencanaJadwalModel model)
        {
            ResponseResult result = PenjadwalanRepo.Delete(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerifyDate(long? ro, string time, DateTime? schedule_date, long id = 0)
        {
            return Json(PenjadwalanRepo.ValidationDate(ro, time, schedule_date, id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Verifytro(long? tro, string time, DateTime? schedule_date, long id = 0)
        {
            return Json(PenjadwalanRepo.Validationtro(tro, time, schedule_date, id), JsonRequestBehavior.AllowGet);
        }

        // Bagian Pelamar Terjadwal

        public ActionResult ListPelamarTerjadwal(long id)
        {
            ViewBag.IdPelamar = id;
            return PartialView("_ListPelamarTerjadwal");
        }

        public ActionResult TableListPelamarTerjadwal(long id)
        {
            return PartialView("_TableListPelamarTerjadwal", PenjadwalanRepo.ListPelamarTerjadwal(id));
        }

        public ActionResult DetailKirimUndangan(long id, long bioId)
        {
            PelamarTerjadwalViewModel model = PenjadwalanRepo.KirimUndangan(id, bioId);
            TempData["model"] = model;
            return PartialView("_DetailKirimUndangan", model);
        }

        public ActionResult EditJadwalPelamarTerjadwal(long id, long rjdId)
        {
            ViewBag.ROList = new SelectList(PenjadwalanRepo.ROAll(), "ro", "fullname");
            ViewBag.TROList = new SelectList(PenjadwalanRepo.TROAll(), "tro", "fullname");
            ViewBag.JenisJadwalList = new SelectList(PenjadwalanRepo.JenisJadwalAll(), "schedule_type_id", "schedule_type_name");
            return PartialView("_EditJadwalPelamarTerjadwal", PenjadwalanRepo.ByRencanaJadwalId(id, rjdId));
        }

        [HttpPost]
        public ActionResult EditJadwalPelamarTerjadwal(PelamarTerjadwalViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "Invalid"
                }, JsonRequestBehavior.AllowGet);
            }

            ResponseResult result = PenjadwalanRepo.UpdateJadwalPelamarTerjadwal(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DetailKirimUndanganSemua(long id)
        {
            PelamarTerjadwalViewModel model1 = PenjadwalanRepo.KirimUndanganSemua(id);
            TempData["model1"] = model1;
            return PartialView("_DetailKirimUndanganSemua", PenjadwalanRepo.KirimUndanganSemua(id));
        }        

        // Mengirim undangan ke 1 email
        [HttpPost]
        public ActionResult Kirim()
        {
            ResponseResult result = new ResponseResult();
            PelamarTerjadwalViewModel entity = (PelamarTerjadwalViewModel)TempData["model"];
            if (ModelState.IsValid)
            {
                var body = "<tr>" +
                                "Dear " + entity.pelamar.Select(p => p.fullname).FirstOrDefault() + "," +
                           "</tr>" +
                           "<tr>" +
                                "PT. Xsis Mitra Utama mengundang Anda untuk mengikuti proses seleksi penerimaan karyawan pada:" +
                           "</tr>" +
                           "<tr>" +
                                "Tanggal: " + entity.schedule_date_string +
                           "</tr>" +
                           "<tr>" +
                                "Pukul: " + entity.time + " WIB" +
                           "</tr>" +
                           "<tr>" +
                                "Lokasi: " + entity.location +
                           "</tr>" +
                           "<tr>" +
                                "Agenda: " + entity.schedule_type_name +
                           "</tr>" +
                           "<tr>" +
                                "RO: " + entity.ro_name +
                           "</tr>" +
                           "<tr>" +
                                "TRO: " + entity.tro_name +
                           "</tr>" +
                           "<tr>" +
                                "RO / TRO Lain: " + entity.other_ro_tro +
                           "</tr>" +
                           "<tr>" +
                                "Catatan: " + entity.notes +
                           "</tr>" +
                           "<tr>" +
                                "Best Regards," +
                           "</tr>" +
                           "<tr>" +
                                "PT. Xsis Mitra Utama" +
                           "</tr>";

                var message = new MailMessage();
                message.To.Add(new MailAddress(entity.pelamar.Select(p => p.email).FirstOrDefault()));
                message.Subject = "Undangan " + entity.schedule_type_name + " PT. Xsis Mitra Utama";
                message.Body = string.Format(body);
                message.IsBodyHtml = true;

                try
                {
                    using (var smtp = new SmtpClient())
                    {
                        smtp.Send(message);
                    }
                }
                catch
                {
                    result.Success = false;
                    result.Message = "Undangan tidak terkirim!";
                }
            }

            return Json(new
            {
                message = result.Message,
                success = result.Success
            }, JsonRequestBehavior.AllowGet);
        }

        // Mengirim undangan ke semua email
        [HttpPost]
        public ActionResult KirimSemua()
        {
            ResponseResult result = new ResponseResult();
            PelamarTerjadwalViewModel entity = (PelamarTerjadwalViewModel)TempData["model1"];
            if (ModelState.IsValid)
            {
                MailAddressCollection collection = new MailAddressCollection();
                foreach (var item in entity.pelamar)
                    collection.Add(entity.pelamar.Select(p => p.email).FirstOrDefault());

                foreach (var item in collection)
                {
                    var body = "<tr>" +
                                    "Dear " + entity.pelamar.Select(p => p.fullname).FirstOrDefault() + "," +
                               "</tr>" +
                               "<tr>" +
                                    "PT. Xsis Mitra Utama mengundang Anda untuk mengikuti proses seleksi penerimaan karyawan pada:" +
                               "</tr>" +
                               "<tr>" +
                                    "Tanggal: " + entity.schedule_date_string +
                               "</tr>" +
                               "<tr>" +
                                    "Pukul: " + entity.time + " WIB" +
                               "</tr>" +
                               "<tr>" +
                                    "Lokasi: " + entity.location +
                               "</tr>" +
                               "<tr>" +
                                    "Agenda: " + entity.schedule_type_name +
                               "</tr>" +
                               "<tr>" +
                                    "RO: " + entity.ro_name +
                               "</tr>" +
                               "<tr>" +
                                    "TRO: " + entity.tro_name +
                               "</tr>" +
                               "<tr>" +
                                    "RO / TRO Lain: " + entity.other_ro_tro +
                               "</tr>" +
                               "<tr>" +
                                    "Catatan: " + entity.notes +
                               "</tr>" +
                               "<tr>" +
                                    "Best Regards," +
                               "</tr>" +
                               "<tr>" +
                                    "PT. Xsis Mitra Utama" +
                               "</tr>";

                    var message = new MailMessage();
                    message.To.Add(new MailAddress(item.ToString()));
                    message.Subject = "Undangan " + entity.schedule_type_name + " PT. Xsis Mitra Utama";
                    message.Body = string.Format(body);
                    message.IsBodyHtml = true;

                    try
                    {
                        using (var smtp = new SmtpClient())
                        {
                            smtp.Send(message);
                        }
                    }
                    catch
                    {
                        result.Success = false;
                        result.Message = "Undangan tidak terkirim!";
                    }
                }
            }
            return Json(new
            {
                message = result.Message,
                success = result.Success
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsScheduleDateValid(DateTime? schedule_date)
        {
            return Json(PenjadwalanRepo.ValidationScheduleDate(schedule_date), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsROValid(long id, long ro, DateTime schedule_date, string time)
        {
            return Json(PenjadwalanRepo.ValidationRO(id, ro, schedule_date, time), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsTROValid(long id, long tro, DateTime schedule_date, string time)
        {
            return Json(PenjadwalanRepo.ValidationTRO(id, tro, schedule_date, time), JsonRequestBehavior.AllowGet);
        }
    }
}