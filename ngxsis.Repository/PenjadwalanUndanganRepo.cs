using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ngxsis.DataModel;
using ngxsis.ViewModel;


namespace ngxsis.Repository
{
    public class PenjadwalanUndanganRepo
    {

        public static List<PenjadwalanUndanganViewModel> PenjadwalanUndanganList(int desc, int page, int dataPerPage) //ini buat menampilkan
        {
            List<PenjadwalanUndanganViewModel> result = new List<PenjadwalanUndanganViewModel>();
            using (var db = new ngxsisContext())
            {
                if (desc == 1) //ascending
                {
                    result = db.x_undangan_detail
                    .Where(b => b.is_delete == false)
                    .OrderByDescending(b => b.x_biodata.fullname)
                    .Skip(page * dataPerPage)
                    .Take(dataPerPage)
                    .Select(b => new PenjadwalanUndanganViewModel
                    {
                        id = b.id,
                        invitation_code = b.x_undangan.invitation_code,
                        Fullname = b.x_biodata.fullname,
                        invitation_date = b.x_undangan.invitation_date,
                        SchoolName = db.x_riwayat_pendidikan.Where(rp => rp.biodata_id == b.biodata_id && rp.is_delete == false)
                        .OrderByDescending(rp => rp.graduation_year)
                        .Select(rp => rp.school_name).FirstOrDefault(),
                        Major = db.x_riwayat_pendidikan.Where(rp => rp.biodata_id == b.biodata_id && rp.is_delete == false)
                        .OrderByDescending(rp => rp.graduation_year)
                        .Select(rp => rp.major)
                           .FirstOrDefault(),




                    }).ToList();
                    foreach (var item in result)
                        item.date_string = item.invitation_date.Value.ToString("dd MMMM yyyy");

                }
                else //ascending
                {
                    result = db.x_undangan_detail
                       .Where(b => b.is_delete == false)
                       .OrderBy(b => b.x_biodata.fullname)
                       .Skip(page * dataPerPage)
                       .Take(dataPerPage)
                       .Select(b => new PenjadwalanUndanganViewModel
                       {
                           id = b.id,
                           invitation_code = b.x_undangan.invitation_code,
                           Fullname = b.x_biodata.fullname,
                           invitation_date = b.x_undangan.invitation_date,
                           SchoolName = db.x_riwayat_pendidikan.Where(rp => rp.biodata_id == b.biodata_id && rp.is_delete == false)
                           .OrderByDescending(rp => rp.graduation_year)
                           .Select(rp => rp.school_name).FirstOrDefault(),
                           Major = db.x_riwayat_pendidikan.Where(rp => rp.biodata_id == b.biodata_id && rp.is_delete == false)
                           .OrderByDescending(rp => rp.graduation_year)
                           .Select(rp => rp.major)
                              .FirstOrDefault(),



                       }).ToList();
                    foreach (var item in result)
                        item.date_string = item.invitation_date.Value.ToString("dd MMMM yyyy");

                }

            }

            return result;
        }


        public static List<PenjadwalanUndanganViewModel> GetBySearch(string search, int desc, int page, int dataPerPage)
        {
            List<PenjadwalanUndanganViewModel> result = new List<PenjadwalanUndanganViewModel>();
            using (var db = new ngxsisContext())
            {
                if (desc == 1)
                {

                    result = db.x_undangan_detail
                    .Where(b => b.is_delete == false && (b.x_biodata.fullname.Contains(search)))

                    .OrderByDescending(b => b.x_biodata.fullname)
                    .Skip(page * dataPerPage)
                    .Take(dataPerPage)
                    .Select(b => new PenjadwalanUndanganViewModel
                    {

                        id = b.id,
                        Fullname = b.x_biodata.fullname,
                        invitation_code = b.x_undangan.invitation_code,
                        invitation_date = b.x_undangan.invitation_date,
                        SchoolName = db.x_riwayat_pendidikan.Where(rp => rp.biodata_id == b.biodata_id && rp.is_delete == false)
                       .OrderByDescending(rp => rp.graduation_year)
                       .Select(rp => rp.school_name).FirstOrDefault(),
                        Major = db.x_riwayat_pendidikan.Where(rp => rp.biodata_id == b.biodata_id && rp.is_delete == false)
                       .OrderByDescending(rp => rp.graduation_year)
                       .Select(rp => rp.major)
                          .FirstOrDefault(),



                    }).ToList();

                    foreach (var item in result)
                        item.date_string = item.invitation_date.Value.ToString("dd MMMM yyyy");
                }
                else
                {


                    result = db.x_undangan_detail
                    .Where(b => b.is_delete == false && (b.x_biodata.fullname.Contains(search)))

                    .OrderBy(b => b.x_biodata.fullname)
                    .Skip(page * dataPerPage)
                    .Take(dataPerPage)
                    .Select(b => new PenjadwalanUndanganViewModel
                    {

                        id = b.id,
                        Fullname = b.x_biodata.fullname,
                        invitation_code = b.x_undangan.invitation_code,
                        invitation_date = b.x_undangan.invitation_date,
                        SchoolName = db.x_riwayat_pendidikan.Where(rp => rp.biodata_id == b.biodata_id && rp.is_delete == false)
                       .OrderByDescending(rp => rp.graduation_year)
                       .Select(rp => rp.school_name).FirstOrDefault(),
                        Major = db.x_riwayat_pendidikan.Where(rp => rp.biodata_id == b.biodata_id && rp.is_delete == false)
                       .OrderByDescending(rp => rp.graduation_year)
                       .Select(rp => rp.major)
                          .FirstOrDefault(),



                    }).ToList();
                    foreach (var item in result)
                        item.date_string = item.invitation_date.Value.ToString("dd MMMM yyyy");

                }

            }

            return result != null ? result : new List<PenjadwalanUndanganViewModel>();
        }



        public static List<PenjadwalanUndanganViewModel> pelamarAll()
        {
            List<PenjadwalanUndanganViewModel> result = new List<PenjadwalanUndanganViewModel>();
            using (var db = new ngxsisContext())

            {

                result = db.x_biodata.Where(b => b.addrbook_id == null && b.is_deleted == false)
                    .Select(b => new PenjadwalanUndanganViewModel
                    {
                        biodata_id = b.id,

                        Fullname = b.fullname

                    }).ToList();
            }
            return result;
        }
        public static string GetNewCode()
        {

            string result = "UD";
            using (var db = new ngxsisContext())
            {
                var maxCode = db.x_undangan//cuma dpt 1 ref maksimal
                    .Where(u => u.invitation_code.Contains(result))

                    .Select(o => new
                    {
                        invitation_code = o.invitation_code //ditampung di dlm reference
                    })
                    .OrderByDescending(o => o.invitation_code)  //cari nilai maxcode yg ada di database
                    .FirstOrDefault();
                if (maxCode != null)
                {
                    string oldCode = maxCode.invitation_code;
                    int newInc = int.Parse(oldCode.Substring(2, 10)) + 1;
                    result += newInc.ToString("D10");  //dijadikan 10 digit
                }
                else
                {
                    result += "0000000001";
                }

            }
            return result;

        }


        public static List<PenjadwalanUndanganViewModel> jenisundanganAll()
        {
            List<PenjadwalanUndanganViewModel> result = new List<PenjadwalanUndanganViewModel>();
            using (var db = new ngxsisContext())
            {
                result = (from c in db.x_schedule_type

                          select new PenjadwalanUndanganViewModel
                          { //linkq


                              schedule_type_id = c.id,
                              schedule_type_name = c.name,




                          }).ToList();
            }
            return result;
        }

        public static List<PenjadwalanUndanganViewModel> roAll()
        {
            List<PenjadwalanUndanganViewModel> result = new List<PenjadwalanUndanganViewModel>();
            using (var db = new ngxsisContext())
            {
                long roleID = db.x_role.Where(r => r.name == "RO" && r.is_deleted == false)
                    .Select(r => r.id).FirstOrDefault(); //diambil id yg ada nama ro nya
                result = db.x_userrole.Where(ur => ur.role_id == roleID && ur.is_deleted == false) //id_ro
                    .Select(ur => new PenjadwalanUndanganViewModel
                    {
                        ro = db.x_biodata.Where(b => b.addrbook_id == ur.addrbook_id && b.is_deleted == false) // di db biodata cari yg addrbookid sama dg addrbookid userrole
                        .Select(b => b.id)
                        .FirstOrDefault(),

                        Fullname = db.x_biodata.Where(b => b.addrbook_id == ur.addrbook_id && b.is_deleted == false)
                        .Select(b => b.fullname)
                        .FirstOrDefault(),

                    }).ToList();
            }
            return result;
        }

        public static List<PenjadwalanUndanganViewModel> troAll()
        {
            List<PenjadwalanUndanganViewModel> result = new List<PenjadwalanUndanganViewModel>();
            using (var db = new ngxsisContext())
            {
                long roleID = db.x_role.Where(r => r.name == "TRO" && r.is_deleted == false)
                    .Select(r => r.id).FirstOrDefault();
                result = db.x_userrole.Where(ur => ur.role_id == roleID && ur.is_deleted == false)
                    .Select(ur => new PenjadwalanUndanganViewModel
                    {
                        tro = db.x_biodata.Where(b => b.addrbook_id == ur.addrbook_id && b.is_deleted == false)
                        .Select(b => b.id)
                        .FirstOrDefault(),

                        Fullname = db.x_biodata.Where(b => b.addrbook_id == ur.addrbook_id && b.is_deleted == false)
                        .Select(b => b.fullname)
                        .FirstOrDefault(),

                    }).ToList();
            }
            return result;
        }
        //get by Id dipakai di edit dan delete
        public static PenjadwalanUndanganViewModel ById(int id)
        {
            PenjadwalanUndanganViewModel result = new PenjadwalanUndanganViewModel();
            using (var db = new ngxsisContext())
            {
                result = (from c in db.x_undangan_detail
                          where c.id == id
                          select new PenjadwalanUndanganViewModel
                          { //linkq
                              id = c.id,

                              schedule_type_id = c.x_undangan.schedule_type_id,
                              invitation_date = c.x_undangan.invitation_date,
                              invitation_code = c.x_undangan.invitation_code,
                              biodata_id = c.biodata_id,
                              //Fullname = c.x_biodata.fullname,
                              time = c.x_undangan.time,

                              ro = c.x_undangan.ro,
                              tro = c.x_undangan.tro,

                              other_ro_tro = c.x_undangan.other_ro_tro,
                              location = c.x_undangan.location,
                              status = c.x_undangan.status,
                              notes = c.notes

                          }).FirstOrDefault();

                result.date_string = result.invitation_date.Value.ToString("dd MMMM yyyy");

            }
            return result != null ? result : new PenjadwalanUndanganViewModel();


        }


        public static ResponseResult Update(PenjadwalanUndanganViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {
                    #region Create New/ Insert
                    if (entity.id == 0)
                    {
                        string newCode = GetNewCode();
                        x_undangan und = new x_undangan();

                        und.schedule_type_id = entity.schedule_type_id;
                        und.invitation_date = entity.invitation_date;

                        und.invitation_code = newCode;
                        und.time = entity.time;

                        und.ro = entity.ro;
                        und.tro = entity.tro;
                        und.other_ro_tro = entity.other_ro_tro;

                        und.location = entity.location;
                        und.status = entity.status;
                        und.create_by = entity.user_id;
                        und.create_on = DateTime.Now;

                        und.is_delete = false;


                        //  und.biodata_id = entity.biodata_id;

                        x_undangan_detail detail = new x_undangan_detail();
                        detail.undangan_id = und.id;
                        detail.biodata_id = entity.biodata_id;
                        detail.notes = entity.notes;
                        detail.create_by = entity.user_id;

                        detail.is_delete = false;
                        detail.create_on = DateTime.Now;

                        db.x_undangan.Add(und);
                        db.x_undangan_detail.Add(detail);

                        db.SaveChanges();
                        result.Entity = entity;
                    }

                    #endregion Edit
                    #region
                    else
                    {
                        x_undangan_detail detail = db.x_undangan_detail.Where(o => o.id == entity.id).FirstOrDefault();
                        x_undangan und = db.x_undangan.Where(o => o.id == detail.undangan_id).FirstOrDefault();



                        //disini ditulis semua nama tabelnya

                        und.schedule_type_id = entity.schedule_type_id;
                        und.invitation_date = entity.invitation_date;


                        und.time = entity.time;

                        und.ro = entity.ro;
                        und.tro = entity.tro;
                        und.other_ro_tro = entity.other_ro_tro;

                        und.location = entity.location;
                        und.status = entity.status;
                        und.modified_on = DateTime.Now;
                        und.modified_by = entity.user_id;
                        detail.biodata_id = entity.biodata_id;
                        detail.notes = entity.notes;
                        detail.modified_on = DateTime.Now;
                        detail.modified_by = entity.user_id;
                        //detail.is_delete = false;


                        db.SaveChanges();
                        result.Entity = entity;


                    }
                    #endregion
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;

            }
            return result;

        }
        public static PenjadwalanUndanganViewModel undangandetail(long id)
        {
            PenjadwalanUndanganViewModel result = new PenjadwalanUndanganViewModel();
            using (var db = new ngxsisContext())
            {
                result = db.x_undangan_detail
                    .Where(ud => ud.id == id)
                    .Select(ud => new PenjadwalanUndanganViewModel
                    {
                        id = ud.id,
                        invitation_date = ud.x_undangan.invitation_date,
                        time = ud.x_undangan.time,
                        Fullname = db.x_biodata.Where(b => b.id == ud.biodata_id)
                        .Select(b => b.fullname).FirstOrDefault(),
                        schedule_type_name = db.x_schedule_type.Where(st => st.id == ud.x_undangan.schedule_type_id)
                        .Select(st => st.name).FirstOrDefault(),
                        ro_name = db.x_biodata.Where(b => b.id == ud.x_undangan.ro)
                        .Select(b => b.fullname).FirstOrDefault(),
                        tro_name = db.x_biodata.Where(b => b.id == ud.x_undangan.tro)
                        .Select(b => b.fullname).FirstOrDefault(),
                        other_ro_tro = ud.x_undangan.other_ro_tro,
                        location = ud.x_undangan.location,
                        notes = ud.notes



                    }).FirstOrDefault();

                result.date_string = result.invitation_date.Value.ToString("dd MMMM yyyy");
            }
            return result;
        }


        //delete
        public static ResponseResult Delete(PenjadwalanUndanganViewModel entity)

        {


            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {
                    x_undangan_detail und = db.x_undangan_detail
                        .Where(o => o.id == entity.id)
                        .FirstOrDefault();
                    x_undangan und1 = db.x_undangan
                   .Where(o => o.id == und.undangan_id)
                   .FirstOrDefault();
                    if (und != null)
                    {

                        und.is_delete = true;
                        und.delete_on = DateTime.Now;
                        und.delete_by = entity.user_id;


                        und1.is_delete = true;
                        und1.delete_on = DateTime.Now;
                        und1.delete_by = entity.user_id;
                        db.SaveChanges();
                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "PenjadwalanUndangan not found";

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


        //validasi

        public static bool ValidationTanggalUndangan(DateTime? invitation_date)
        {
            try
            {
                if (invitation_date <= DateTime.Now)
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

        public static bool ROValidation(long id, long ro, DateTime invitation_date, string time)
        {
            x_undangan entity = new x_undangan();
            using (var db = new ngxsisContext())
            {
                entity = db.x_undangan.Where(u => u.ro == ro && u.invitation_date == invitation_date && u.time == time && u.is_delete == false && u.id != id).FirstOrDefault();
            }

            if (entity != null) ///ada data
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool TROValidation(long id, long tro, DateTime invitation_date, string time)
        {
            x_undangan entity = new x_undangan();
            using (var db = new ngxsisContext())
            {
                entity = db.x_undangan.Where(u => u.tro == tro && u.invitation_date == invitation_date && u.time == time && u.is_delete == false && u.id != id).FirstOrDefault();
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
