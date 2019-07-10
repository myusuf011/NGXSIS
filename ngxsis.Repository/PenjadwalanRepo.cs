using ngxsis.DataModel;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.Repository
{
    public class PenjadwalanRepo
    {
        // Bagian Penjadwalan - Rencana

        public static List<RencanaJadwalModel> All(DateTime dari, DateTime sampai, int desc, int page, int dataPerPage)
        {

            List<RencanaJadwalModel> result = new List<RencanaJadwalModel>();
            using (var db = new ngxsisContext())
            {


                if (desc == 1)
                {

                    result = db.x_rencana_jadwal.Where(c => c.is_delete == false && c.schedule_date >= dari && c.schedule_date <= sampai)
                    .OrderByDescending(c => c.create_on)
                    .Skip(page * dataPerPage)
                    .Take(dataPerPage)

                        .Select(c => new RencanaJadwalModel
                        {
                            id = c.id,
                            schedule_code = c.schedule_code,
                            schedule_date = c.schedule_date,
                            time = c.time,
                            ro_name = db.x_biodata.Where(b => b.id == c.ro).Select(b => b.fullname).FirstOrDefault(),
                            tro_name = db.x_biodata.Where(b => b.id == c.tro).Select(b => b.fullname).FirstOrDefault(),
                            scedule_type_name = db.x_schedule_type.Where(s => s.id == c.scedule_type_id).Select(s => s.name).FirstOrDefault(),
                            location = c.location,
                            status = c.status

                        }).ToList();
                }
                else
                {

                    result = db.x_rencana_jadwal.Where(c => c.is_delete == false && c.schedule_date >= dari && c.schedule_date <= sampai)
                    .OrderBy(c => c.create_on)
                    .Skip(page * dataPerPage)
                    .Take(dataPerPage)
                        .Select(c => new RencanaJadwalModel
                        {
                            id = c.id,
                            schedule_code = c.schedule_code,
                            schedule_date = c.schedule_date,
                            time = c.time,
                            ro_name = db.x_biodata.Where(b => b.id == c.ro).Select(b => b.fullname).FirstOrDefault(),
                            tro_name = db.x_biodata.Where(b => b.id == c.tro).Select(b => b.fullname).FirstOrDefault(),
                            scedule_type_name = db.x_schedule_type.Where(s => s.id == c.scedule_type_id).Select(s => s.name).FirstOrDefault(),
                            location = c.location,
                            status = c.status

                        }).ToList();

                }
                foreach (var item in result)
                {
                    string[] w = item.schedule_date.ToString().Split('/');
                    string[] x = w[2].Split(' ');
                    w[2] = x[0];

                    switch (int.Parse(w[0]))
                    {
                        case 1:
                            w[0] = ("Januari");
                            break;
                        case 2:
                            w[0] = ("Februari");
                            break;
                        case 3:
                            w[0] = ("Maret");
                            break;
                        case 4:
                            w[0] = ("April");
                            break;
                        case 5:
                            w[0] = ("Mei");
                            break;
                        case 6:
                            w[0] = ("Juni");
                            break;
                        case 7:
                            w[0] = ("Juli");
                            break;
                        case 8:
                            w[0] = ("Agustus");
                            break;
                        case 9:
                            w[0] = ("September");
                            break;
                        case 10:
                            w[0] = ("Oktober");
                            break;
                        case 11:
                            w[0] = ("November");
                            break;
                        case 12:
                            w[0] = ("Desember");
                            break;
                        default:
                            w[0] = (" ");
                            break;


                    }

                    item.date_name = string.Format("{0} {1} {2}", w[1], w[0], w[2]);
                }

            }

            return result;

        }

        public static List<BiodataViewModel> AllPelamar()
        {

            List<BiodataViewModel> result = new List<BiodataViewModel>();
            using (var db = new ngxsisContext())
            {

                result = db.x_biodata.Where(c => c.addrbook_id == null && c.is_deleted == false).Select(c => new BiodataViewModel
                {
                    id = c.id,
                    fullname = c.fullname


                }).ToList();



            }

            return result;

        }

        public static List<BiodataViewModel> AllRO()
        {

            List<BiodataViewModel> result = new List<BiodataViewModel>();
            using (var db = new ngxsisContext())
            {

                result = db.x_userrole.Where(c => c.role_id == 3 && c.is_deleted == false).Select(c => new BiodataViewModel
                {
                    id = db.x_biodata.Where(b => b.addrbook_id == c.addrbook_id).Select(b => b.id).FirstOrDefault(),
                    fullname = db.x_biodata.Where(b => b.addrbook_id == c.addrbook_id).Select(b => b.fullname).FirstOrDefault()



                }).ToList();



            }

            return result;

        }

        public static List<BiodataViewModel> AllTRO()
        {

            List<BiodataViewModel> result = new List<BiodataViewModel>();
            using (var db = new ngxsisContext())
            {

                result = db.x_userrole.Where(c => c.role_id == 5 && c.is_deleted == false).Select(c => new BiodataViewModel
                {
                    id = db.x_biodata.Where(b => b.addrbook_id == c.addrbook_id).Select(b => b.id).FirstOrDefault(),
                    fullname = db.x_biodata.Where(b => b.addrbook_id == c.addrbook_id).Select(b => b.fullname).FirstOrDefault()



                }).ToList();



            }

            return result;

        }

        public static List<DropDownModel> AllJenisJadwal()
        {

            List<DropDownModel> result = new List<DropDownModel>();
            using (var db = new ngxsisContext())
            {

                result = db.x_schedule_type.Where(c => c.is_delete == false).Select(c => new DropDownModel
                {
                    id = c.id,
                    name = c.name



                }).ToList();



            }

            return result;

        }


        public static string KodeJadwal()
        {

            string num;
            int resultInt;
            string result;
            using (var db = new ngxsisContext())
            {
                result = db.x_rencana_jadwal.Where(o => o.is_delete == false).OrderByDescending(o => o.id).Select(o => o.schedule_code).FirstOrDefault();

                if (result == null)
                {

                    result = "JDW0000000001";

                }
                else
                {

                    resultInt = int.Parse(result.Substring(3));
                    num = (resultInt + 1).ToString();
                    result = "JDW";
                    for (int i = 0; i < (10 - num.Length); i++)
                    {
                        result += "0";
                    }
                    result += num;
                }




            }



            return result;

        }

        public static ResponseResult Update(RencanaJadwalModel entity)
        {

            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {


                    if (entity.sent_date != null)
                    {
                        entity.status = "Otomatis";
                    }
                    else
                    {
                        entity.status = "Manual";
                    }

                    #region Create New / Insert
                    if (entity.id == 0)
                    {


                        x_rencana_jadwal rencana = new x_rencana_jadwal();

                        rencana.schedule_date = entity.schedule_date;
                        rencana.time = entity.time;
                        rencana.scedule_type_id = entity.scedule_type_id;
                        rencana.ro = entity.ro;
                        rencana.tro = entity.tro;
                        rencana.location = entity.location;
                        rencana.other_ro_tro = entity.other_ro_tro;
                        rencana.notes = entity.notes;
                        rencana.is_automatic_mail = entity.is_automatic_mail;
                        rencana.sent_date = entity.sent_date;
                        rencana.schedule_code = KodeJadwal();
                        rencana.is_delete = false;
                        rencana.create_by = entity.user_id;
                        rencana.create_on = DateTime.Now;
                        rencana.status = entity.status;

                        db.x_rencana_jadwal.Add(rencana);

                        foreach (var item in entity.pelamar_list)
                        {


                            x_rencana_jadwal_detail jdw = new x_rencana_jadwal_detail();
                            jdw.create_by = entity.user_id;
                            jdw.create_on = DateTime.Now;
                            jdw.is_delete = false;
                            jdw.biodata_id = item.id;
                            jdw.rencana_jadwal_id = rencana.id;
                            db.x_rencana_jadwal_detail.Add(jdw);

                        }

                        db.SaveChanges();
                        entity.schedule_code = rencana.schedule_code;
                        result.Entity = entity;


                    }
                    #endregion

                    #region edit
                    else
                    {

                        x_rencana_jadwal rencana = db.x_rencana_jadwal.Where(a => a.id == entity.id && a.is_delete == false).FirstOrDefault();

                        rencana.id = entity.id;
                        rencana.schedule_date = entity.schedule_date;
                        rencana.time = entity.time;
                        rencana.scedule_type_id = entity.scedule_type_id;
                        rencana.ro = entity.ro;
                        rencana.tro = entity.tro;
                        rencana.location = entity.location;
                        rencana.other_ro_tro = entity.other_ro_tro;
                        rencana.notes = entity.notes;
                        rencana.is_automatic_mail = entity.is_automatic_mail;
                        rencana.sent_date = entity.sent_date;
                        rencana.status = entity.status;

                        rencana.modified_by = entity.user_id;
                        rencana.modified_on = DateTime.Now;

                        List<x_rencana_jadwal_detail> urByAbId = db.x_rencana_jadwal_detail.Where(ur => ur.rencana_jadwal_id == entity.id && ur.is_delete == false).ToList();

                        //DELETED ROLE
                        foreach (var ur in urByAbId)
                        {
                            bool unChecked = true;
                            foreach (var item in entity.pelamar_list)
                            {
                                if (ur.biodata_id == item.id)
                                {
                                    unChecked = false;
                                    break;
                                }
                            }
                            if (unChecked)
                            {
                                ur.is_delete = true;
                                ur.delete_by = entity.user_id;
                                ur.delete_on = DateTime.Now;
                            }
                        }

                        //ADDED ROLE
                        foreach (var item in entity.pelamar_list)
                        {
                            x_rencana_jadwal_detail userRole = db.x_rencana_jadwal_detail.Where(ur => ur.rencana_jadwal_id == entity.id && ur.biodata_id == item.id && ur.is_delete == false).FirstOrDefault();
                            if (userRole == null)
                            {
                                userRole = new x_rencana_jadwal_detail();
                                userRole.biodata_id = item.id;
                                userRole.rencana_jadwal_id = rencana.id;
                                userRole.is_delete = false;
                                userRole.create_by = entity.user_id;
                                userRole.create_on = DateTime.Now;
                                db.x_rencana_jadwal_detail.Add(userRole);
                            }
                        }

                        entity.schedule_code = rencana.schedule_code;
                        db.SaveChanges();
                        result.Entity = entity;


                    }

                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }
        #endregion

        public static RencanaJadwalModel ByIdJdw(long id)
        {

            RencanaJadwalModel result = new RencanaJadwalModel();
            using (var db = new ngxsisContext())
            {
                result = db.x_rencana_jadwal.Where(o => o.id == id && o.is_delete == false).Select(o => new RencanaJadwalModel
                {


                    id = o.id,
                    schedule_date = o.schedule_date,
                    time = o.time,
                    scedule_type_id = o.scedule_type_id,
                    ro = o.ro,
                    tro = o.tro,
                    location = o.location,
                    other_ro_tro = o.other_ro_tro,
                    notes = o.notes,
                    is_automatic_mail = o.is_automatic_mail,
                    sent_date = o.sent_date,
                    schedule_code = o.schedule_code,
                    status = o.status,

                }).FirstOrDefault();

            }
            return result;
        }

        public static List<DropDownModel> ByIdPel(long id)
        {

            List<DropDownModel> result = new List<DropDownModel>();
            using (var db = new ngxsisContext())
            {
                result = db.x_rencana_jadwal_detail.Where(o => o.rencana_jadwal_id == id && o.is_delete == false).Select(o => new DropDownModel
                {

                    id = o.biodata_id


                }).ToList();

            }
            return result;
        }

        public static ResponseResult Delete(RencanaJadwalModel entity)
        {
            ResponseResult result = new ResponseResult();
            using (var db = new ngxsisContext())
            {
                x_rencana_jadwal ab = db.x_rencana_jadwal.Where(a => a.id == entity.id).FirstOrDefault();
                ab.is_delete = true;
                ab.delete_by = entity.user_id;
                ab.delete_on = DateTime.Now;
                //db.SaveChanges();
                List<x_rencana_jadwal_detail> urByAbId = db.x_rencana_jadwal_detail.Where(ur => ur.rencana_jadwal_id == entity.id && ur.is_delete == false).ToList();
                foreach (var ur in urByAbId)
                {
                    ur.is_delete = true;
                    ur.delete_by = entity.user_id;
                    ur.delete_on = DateTime.Now;

                }
                db.SaveChanges();
                result.Entity = entity;
            }
            return result;
        }

        public static bool ValidationDate(long? ro, string time, DateTime? schedule_date, long id = 0)
        {

            x_rencana_jadwal result = new x_rencana_jadwal();

            using (var db = new ngxsisContext())
            {
                result = db.x_rencana_jadwal.Where(o => o.id != id && o.ro == ro && o.time == time && o.schedule_date == schedule_date && o.is_delete == false).FirstOrDefault();





            }

            if (result != null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public static bool Validationtro(long? tro, string time, DateTime? schedule_date, long id = 0)
        {

            x_rencana_jadwal result = new x_rencana_jadwal();

            using (var db = new ngxsisContext())
            {
                result = db.x_rencana_jadwal.Where(o => o.id != id && o.tro == tro && o.time == time && o.schedule_date == schedule_date && o.is_delete == false).FirstOrDefault();





            }

            if (result != null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        // Bagian Pelamar Terjadwal

        // Menampilkan data pelamar berdasarkan id rencana jadwal
        public static PelamarTerjadwalViewModel ListPelamarTerjadwal(long id)
        {
            PelamarTerjadwalViewModel result = new PelamarTerjadwalViewModel();
            using (var db = new ngxsisContext())
            {
                result = db.x_rencana_jadwal
                    .Where(rj => rj.id == id && rj.is_delete == false)
                    .Select(rj => new PelamarTerjadwalViewModel
                    {
                        id = rj.id,
                        schedule_type_name = db.x_schedule_type.Where(st => st.id == rj.scedule_type_id)
                            .Select(st => st.name).FirstOrDefault(),
                        schedule_date = rj.schedule_date,
                        time = rj.time,
                        ro_name = db.x_biodata.Where(b => b.id == rj.ro)
                            .Select(b => b.fullname).FirstOrDefault(),
                        tro_name = db.x_biodata.Where(b => b.id == rj.tro)
                            .Select(b => b.fullname).FirstOrDefault(),
                        other_ro_tro = rj.other_ro_tro,
                        location = rj.location,
                        notes = rj.notes
                    }).FirstOrDefault();

                result.pelamar = db.x_rencana_jadwal_detail
                    .Where(rjd => rjd.rencana_jadwal_id == id && rjd.is_delete == false)
                    .Select(rjd => new Pelamar
                    {
                        rjd_id = rjd.id,
                        biodata_id = rjd.biodata_id,
                        fullname = db.x_biodata.Where(b => b.id == rjd.biodata_id)
                            .Select(b => b.fullname).FirstOrDefault(),
                        school_name = db.x_riwayat_pendidikan.Where(rp => rp.biodata_id == rjd.biodata_id && rp.is_delete == false)
                            .OrderByDescending(rp => rp.graduation_year)
                            .Select(rp => rp.school_name).FirstOrDefault(),
                        major = db.x_riwayat_pendidikan.Where(rp => rp.biodata_id == rjd.biodata_id && rp.is_delete == false)
                            .OrderByDescending(rp => rp.graduation_year)
                            .Select(rp => rp.major).FirstOrDefault()
                    }).ToList();
            }
            return result;
        }

        // Menampilkan detail kirim undangan
        public static PelamarTerjadwalViewModel KirimUndangan(long id, long bioId)
        {
            PelamarTerjadwalViewModel result = new PelamarTerjadwalViewModel();
            using (var db = new ngxsisContext())
            {
                result = db.x_rencana_jadwal_detail
                    .Where(rjd => rjd.x_rencana_jadwal.id == id && rjd.biodata_id == bioId && rjd.is_delete == false)
                    .Select(rjd => new PelamarTerjadwalViewModel
                    {
                        id = rjd.id,
                        schedule_type_name = db.x_schedule_type.Where(st => st.id == rjd.x_rencana_jadwal.scedule_type_id)
                            .Select(st => st.name).FirstOrDefault(),
                        schedule_date = rjd.x_rencana_jadwal.schedule_date,
                        time = rjd.x_rencana_jadwal.time,
                        ro_name = db.x_biodata.Where(b => b.id == rjd.x_rencana_jadwal.ro)
                            .Select(b => b.fullname).FirstOrDefault(),
                        tro_name = db.x_biodata.Where(b => b.id == rjd.x_rencana_jadwal.tro)
                            .Select(b => b.fullname).FirstOrDefault(),
                        other_ro_tro = rjd.x_rencana_jadwal.other_ro_tro,
                        location = rjd.x_rencana_jadwal.location,
                        notes = rjd.x_rencana_jadwal.notes
                    }).FirstOrDefault();

                result.pelamar = db.x_rencana_jadwal_detail
                   .Where(rjd => rjd.x_rencana_jadwal.id == id && rjd.is_delete == false && rjd.biodata_id == bioId)
                   .Select(rjd => new Pelamar
                   {
                       biodata_id = rjd.biodata_id,
                       fullname = db.x_biodata.Where(b => b.id == rjd.biodata_id)
                           .Select(b => b.fullname).FirstOrDefault(),
                       school_name = db.x_riwayat_pendidikan.Where(rp => rp.biodata_id == rjd.biodata_id && rp.is_delete == false)
                           .OrderByDescending(rp => rp.graduation_year)
                           .Select(rp => rp.school_name).FirstOrDefault(),
                       major = db.x_riwayat_pendidikan.Where(rp => rp.biodata_id == rjd.biodata_id && rp.is_delete == false)
                           .OrderByDescending(rp => rp.graduation_year)
                           .Select(rp => rp.major).FirstOrDefault(),
                       email = db.x_biodata.Where(b => b.id == rjd.biodata_id)
                           .Select(b => b.email).FirstOrDefault()
                   }).ToList();
                result.schedule_date_string = ConvertDateFormat(result.schedule_date);
            }
            return result;
        }

        //Menampilkan detail Kirim Undangan ke Semua
        public static PelamarTerjadwalViewModel KirimUndanganSemua(long id)
        {
            PelamarTerjadwalViewModel result = new PelamarTerjadwalViewModel();
            using (var db = new ngxsisContext())
            {
                result = db.x_rencana_jadwal
                    .Where(rj => rj.id == id && rj.is_delete == false)
                    .Select(rj => new PelamarTerjadwalViewModel
                    {
                        id = rj.id,
                        schedule_type_name = db.x_schedule_type
                            .Where(st => st.id == rj.scedule_type_id)
                            .Select(st => st.name).FirstOrDefault(),
                        schedule_date = rj.schedule_date,
                        time = rj.time,
                        ro_name = db.x_biodata.Where(b => b.id == rj.ro)
                            .Select(b => b.fullname).FirstOrDefault(),
                        tro_name = db.x_biodata.Where(b => b.id == rj.tro)
                            .Select(b => b.fullname).FirstOrDefault(),
                        other_ro_tro = rj.other_ro_tro,
                        location = rj.location,
                        notes = rj.notes
                    }).FirstOrDefault();

                result.pelamar = db.x_rencana_jadwal_detail
                   .Where(rjd => rjd.rencana_jadwal_id == id && rjd.is_delete == false)
                   .Select(rjd => new Pelamar
                   {
                       biodata_id = rjd.biodata_id,
                       fullname = db.x_biodata.Where(b => b.id == rjd.biodata_id).Select(b => b.fullname).FirstOrDefault(),
                       email = db.x_biodata.Where(b => b.id == rjd.biodata_id).Select(b => b.email).FirstOrDefault(),
                   }).ToList();
                result.schedule_date_string = ConvertDateFormat(result.schedule_date);
            }
            return result;
        }
        
        // Mengambil data berdasarkan id x_rencana_jadwal untuk form edit
        public static PelamarTerjadwalViewModel ByRencanaJadwalId(long id, long rjdId)
        {
            PelamarTerjadwalViewModel result = new PelamarTerjadwalViewModel();
            using (var db = new ngxsisContext())
            {
                result = db.x_rencana_jadwal
                    .Where(rj => rj.id == id && rj.is_delete == false)
                    .Select(rj => new PelamarTerjadwalViewModel
                    {
                        id = rj.id,
                        biodata_id = db.x_rencana_jadwal_detail.Where(rjd => rjd.id == rjdId).Select(rjd => rjd.biodata_id).FirstOrDefault(),
                        rencana_jadwal_detail_id = rjdId,
                        schedule_type_id = rj.scedule_type_id,
                        schedule_type_name = db.x_schedule_type
                            .Where(st => st.id == rj.scedule_type_id)
                            .Select(st => st.name).FirstOrDefault(),
                        schedule_date = rj.schedule_date,
                        time = rj.time,
                        ro = rj.ro,
                        ro_name = db.x_biodata.Where(b => b.id == rj.ro)
                            .Select(b => b.fullname).FirstOrDefault(),
                        tro = rj.tro,
                        tro_name = db.x_biodata.Where(b => b.id == rj.tro)
                            .Select(b => b.fullname).FirstOrDefault(),
                        other_ro_tro = rj.other_ro_tro,
                        location = rj.location,
                        notes = rj.notes,
                        is_automatic_mail = rj.is_automatic_mail,
                        sent_date = rj.sent_date,
                        status = rj.status
                    }).FirstOrDefault();
                result.schedule_date_string = ConvertDateFormat(result.schedule_date);
                return result;
            }
        }

        //public static ResponseResult Kirim1(PelamarTerjadwalViewModel entity)
        //{
        //    ResponseResult result = new ResponseResult();
        //    using (var db = new ngxsisContext())
        //    {
        //        x_rencana_jadwal jadwal = db.x_rencana_jadwal.Where(rjd => rjd.id == entity.id).FirstOrDefault();

        //        // semua field di x_rencana_jadwal

        //        jadwal.modified_by = entity.user_id;
        //        jadwal.modified_on = DateTime.Now;
        //        jadwal.is_delete = false;
        //        jadwal.schedule_date = entity.schedule_date;
        //        jadwal.time = entity.time;
        //        jadwal.ro = entity.ro;
        //        jadwal.tro = entity.tro;
        //        jadwal.scedule_type_id = entity.schedule_type_id;
        //        jadwal.location = entity.location;
        //        jadwal.other_ro_tro = entity.other_ro_tro;
        //        jadwal.notes = entity.notes;
        //        jadwal.is_automatic_mail = entity.is_automatic_mail;
        //        jadwal.sent_date = entity.sent_date;
        //        db.SaveChangesAsync().Wait();

        //        x_rencana_jadwal_detail detail = db.x_rencana_jadwal_detail.Where(rjd => rjd.id == entity.rencana_jadwal_detail_id).FirstOrDefault();
        //        detail.modified_by = entity.user_id;
        //        detail.modified_on = DateTime.Now;
        //        detail.is_delete = false;
        //        detail.rencana_jadwal_id = jadwal.id; // rencana_jadwal_id diambil dari id nya x_rencana_jadwal yang baru dibuat
        //        detail.biodata_id = entity.biodata_id;

        //        db.SaveChanges();
        //        entity.schedule_code = jadwal.schedule_code;
        //        result.Entity = entity;

        //        entity.schedule_date_string = ConvertDateFormat(entity.schedule_date);
        //        entity.sent_date_string = entity.sent_date != null ? ConvertDateFormat(entity.sent_date) : "";
        //    }
        //    return result;
        //}

        // Mengedit rencana jadwal
        public static ResponseResult UpdateJadwalPelamarTerjadwal(PelamarTerjadwalViewModel entity)
        {
            PelamarTerjadwalViewModel pelamarTerjadwal = new PelamarTerjadwalViewModel();
            using (var db = new ngxsisContext())
            {
                pelamarTerjadwal = db.x_rencana_jadwal
                    .Where(rj => rj.id == entity.id && rj.is_delete == false)
                    .Select(rj => new PelamarTerjadwalViewModel
                    {
                        id = rj.id,
                        schedule_type_name = db.x_schedule_type.Where(st => st.id == rj.scedule_type_id)
                            .Select(st => st.name).FirstOrDefault(),
                        schedule_date = rj.schedule_date,
                        time = rj.time,
                        ro_name = db.x_biodata.Where(b => b.id == rj.ro)
                            .Select(b => b.fullname).FirstOrDefault(),
                        tro_name = db.x_biodata.Where(b => b.id == rj.tro)
                            .Select(b => b.fullname).FirstOrDefault(),
                        other_ro_tro = rj.other_ro_tro,
                        location = rj.location,
                        notes = rj.notes
                    }).FirstOrDefault();

                pelamarTerjadwal.pelamar = db.x_rencana_jadwal_detail
                    .Where(rjd => rjd.rencana_jadwal_id == entity.id && rjd.is_delete == false)
                    .Select(rjd => new Pelamar
                    {
                        rjd_id = rjd.id,
                        biodata_id = rjd.biodata_id,
                        fullname = db.x_biodata.Where(b => b.id == rjd.biodata_id)
                            .Select(b => b.fullname).FirstOrDefault(),
                        school_name = db.x_riwayat_pendidikan.Where(rp => rp.biodata_id == rjd.biodata_id && rp.is_delete == false)
                            .OrderByDescending(rp => rp.graduation_year)
                            .Select(rp => rp.school_name).FirstOrDefault(),
                        major = db.x_riwayat_pendidikan.Where(rp => rp.biodata_id == rjd.biodata_id && rp.is_delete == false)
                            .OrderByDescending(rp => rp.graduation_year)
                            .Select(rp => rp.major).FirstOrDefault()
                    }).ToList();
            }

            var itemCount = 0;
            foreach (var item in pelamarTerjadwal.pelamar)
            {
                itemCount += 1;
            }


            ResponseResult result = new ResponseResult();
            using (var db = new ngxsisContext())
            {
                // Kalo pelamar > 1 bikin rencana_jadwal baru
                if (itemCount > 1)
                {
                    string newScheduleCode = GetNewScheduleCode();

                    x_rencana_jadwal jadwal = new x_rencana_jadwal();

                    // semua field di x_rencana_jadwal

                    jadwal.create_by = entity.user_id;
                    jadwal.create_on = DateTime.Now;
                    jadwal.is_delete = false;
                    jadwal.schedule_code = newScheduleCode;
                    jadwal.schedule_date = entity.schedule_date;
                    jadwal.time = entity.time;
                    jadwal.ro = entity.ro;
                    jadwal.tro = entity.tro;
                    jadwal.scedule_type_id = entity.schedule_type_id;
                    jadwal.location = entity.location;
                    jadwal.other_ro_tro = entity.other_ro_tro;
                    jadwal.notes = entity.notes;
                    jadwal.is_automatic_mail = entity.is_automatic_mail;
                    jadwal.sent_date = entity.sent_date;
                    entity.schedule_code = newScheduleCode;
                    db.x_rencana_jadwal.Add(jadwal); // dapet id x_rencana_jadwal
                    db.SaveChangesAsync().Wait();

                    long rjdId = jadwal.id;
                    x_rencana_jadwal_detail detail = db.x_rencana_jadwal_detail.Where(rjd => rjd.id == entity.rencana_jadwal_detail_id).FirstOrDefault();
                    detail.modified_by = entity.user_id;
                    detail.modified_on = DateTime.Now;
                    detail.is_delete = false;
                    detail.rencana_jadwal_id = rjdId; // rencana_jadwal_id diambil dari id nya x_rencana_jadwal yang baru dibuat
                    detail.biodata_id = entity.biodata_id;

                    db.SaveChanges();

                    result.Entity = entity;
                }
                // Kalo pelamar cuma 1 kayak edit biasa
                else
                {
                    x_rencana_jadwal jadwal = db.x_rencana_jadwal.Where(rjd => rjd.id == entity.id).FirstOrDefault();

                    // semua field di x_rencana_jadwal

                    jadwal.modified_by = entity.user_id;
                    jadwal.modified_on = DateTime.Now;
                    jadwal.is_delete = false;
                    jadwal.schedule_date = entity.schedule_date;
                    jadwal.time = entity.time;
                    jadwal.ro = entity.ro;
                    jadwal.tro = entity.tro;
                    jadwal.scedule_type_id = entity.schedule_type_id;
                    jadwal.location = entity.location;
                    jadwal.other_ro_tro = entity.other_ro_tro;
                    jadwal.notes = entity.notes;
                    jadwal.is_automatic_mail = entity.is_automatic_mail;
                    jadwal.sent_date = entity.sent_date;
                    db.SaveChangesAsync().Wait();

                    x_rencana_jadwal_detail detail = db.x_rencana_jadwal_detail.Where(rjd => rjd.id == entity.rencana_jadwal_detail_id).FirstOrDefault();
                    detail.modified_by = entity.user_id;
                    detail.modified_on = DateTime.Now;
                    detail.is_delete = false;
                    detail.rencana_jadwal_id = jadwal.id; // rencana_jadwal_id diambil dari id nya x_rencana_jadwal yang baru dibuat
                    detail.biodata_id = entity.biodata_id;

                    db.SaveChanges();
                    entity.schedule_code = jadwal.schedule_code;
                    result.Entity = entity;
                }
                entity.schedule_date_string = ConvertDateFormat(entity.schedule_date);
                entity.sent_date_string = entity.sent_date != null ? ConvertDateFormat(entity.sent_date) : "";
            }
            return result;
        }

        // Untuk dropdownlist RO
        public static List<PelamarTerjadwalViewModel> ROAll()
        {
            List<PelamarTerjadwalViewModel> result = new List<PelamarTerjadwalViewModel>();
            using (var db = new ngxsisContext())
            {
                long roleID = db.x_role.Where(r => r.name == "RO" && r.is_deleted == false)
                    .Select(r => r.id).FirstOrDefault();
                result = db.x_userrole
                    .Where(ur => ur.role_id == roleID && ur.is_deleted == false)
                    .Select(ur => new PelamarTerjadwalViewModel
                    {
                        ro = db.x_biodata
                            .Where(b => b.addrbook_id == ur.addrbook_id && b.is_deleted == false)
                            .Select(b => b.id).FirstOrDefault(),
                        fullname = db.x_biodata
                            .Where(b => b.addrbook_id == ur.addrbook_id && b.is_deleted == false)
                            .Select(b => b.fullname).FirstOrDefault()
                    }).ToList();
            }
            return result;
        }

        // Untuk dropdownlist TRO
        public static List<PelamarTerjadwalViewModel> TROAll()
        {
            List<PelamarTerjadwalViewModel> result = new List<PelamarTerjadwalViewModel>();
            using (var db = new ngxsisContext())
            {
                long roleID = db.x_role.Where(r => r.name == "TRO" && r.is_deleted == false)
                    .Select(r => r.id).FirstOrDefault();
                result = db.x_userrole
                    .Where(ur => ur.role_id == roleID && ur.is_deleted == false)
                    .Select(ur => new PelamarTerjadwalViewModel
                    {
                        tro = db.x_biodata
                            .Where(b => b.addrbook_id == ur.addrbook_id && b.is_deleted == false)
                            .Select(b => b.id).FirstOrDefault(),
                        fullname = db.x_biodata
                            .Where(b => b.addrbook_id == ur.addrbook_id && b.is_deleted == false)
                            .Select(b => b.fullname).FirstOrDefault()
                    }).ToList();
            }
            return result;
        }

        // Untuk dropdownlist Jenis Jadwal
        public static List<PelamarTerjadwalViewModel> JenisJadwalAll()
        {
            List<PelamarTerjadwalViewModel> result = new List<PelamarTerjadwalViewModel>();
            using (var db = new ngxsisContext())
            {
                result = db.x_schedule_type
                    .Where(st => st.is_delete == false)
                    .Select(st => new PelamarTerjadwalViewModel
                    {
                        schedule_type_id = st.id,
                        schedule_type_name = st.name
                    }).ToList();
            }
            return result;
        }

        //Membuat schedule_code baru
        public static string GetNewScheduleCode()
        {
            string result = "JDW";
            using (var db = new ngxsisContext())
            {
                var maxCode = db.x_rencana_jadwal
                    .Where(rj => rj.schedule_code.Contains(result))
                    .Select(r => new
                    {
                        scheduleCode = r.schedule_code
                    })
                    .OrderByDescending(r => r.scheduleCode)
                    .FirstOrDefault();

                if (maxCode != null)
                {
                    string oldCode = maxCode.scheduleCode;
                    int newInc = int.Parse(oldCode.Substring(3, 10)) + 1;
                    result += newInc.ToString("D10");
                }
                else
                {
                    result += "0000000001";
                }
            }
            return result;
        }

        // Validasi Tanggal Rencana Jadwal harus lebih besar dari hari ini
        public static bool ValidationScheduleDate(DateTime? schedule_date)
        {
            try
            {
                if (schedule_date <= DateTime.Now)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Merubah format tanggal
        public static string ConvertDateFormat(DateTime? date)
        {
            string[] w = date.ToString().Split('/'); // date = 7/9/2019 10:50:00AM
            string[] x = w[2].Split(' '); // x = 2019 10:50:00AM
            w[2] = x[0]; // w[2] = 2019

            switch (int.Parse(w[0])) // w[0] = 7
            {
                case 1:
                    w[0] = ("Januari");
                    break;
                case 2:
                    w[0] = ("Februari");
                    break;
                case 3:
                    w[0] = ("Maret");
                    break;
                case 4:
                    w[0] = ("April");
                    break;
                case 5:
                    w[0] = ("Mei");
                    break;
                case 6:
                    w[0] = ("Juni");
                    break;
                case 7:
                    w[0] = ("Juli");
                    break;
                case 8:
                    w[0] = ("Agustus");
                    break;
                case 9:
                    w[0] = ("September");
                    break;
                case 10:
                    w[0] = ("Oktober");
                    break;
                case 11:
                    w[0] = ("November");
                    break;
                case 12:
                    w[0] = ("Desember");
                    break;
                default:
                    w[0] = (" ");
                    break;
            }
            return string.Format("{0} {1} {2}", w[1], w[0], w[2]);
        }

        // Validasi RO tidak boleh memiliki jadwal di tanggal dan jam yang sama
        public static bool ValidationRO(long id, long ro, DateTime schedule_date, string time)
        {
            x_rencana_jadwal entity = new x_rencana_jadwal();
            using (var db = new ngxsisContext())
            {
                entity = db.x_rencana_jadwal.Where(rj => rj.ro == ro && rj.schedule_date == schedule_date && rj.time == time && rj.is_delete == false && rj.id != id).FirstOrDefault();
            }

            if (entity != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Validasi RO tidak boleh memiliki jadwal di tanggal dan jam yang sama
        public static bool ValidationTRO(long id, long tro, DateTime schedule_date, string time)
        {
            x_rencana_jadwal entity = new x_rencana_jadwal();
            using (var db = new ngxsisContext())
            {
                entity = db.x_rencana_jadwal.Where(rj => rj.tro == tro && rj.schedule_date == schedule_date && rj.time == time && rj.is_delete == false && rj.id != id).FirstOrDefault();
            }

            if (entity != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}