using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ngxsis.DataModel;
using ngxsis.ViewModel;


namespace ngxsis.Repository
{
    public class PendidikanRepo

    {
        public static List<PendidikanViewModel> All()
        {
            List<PendidikanViewModel> result = new List<PendidikanViewModel>();
            using (var db = new ngxsisContext())
            {
                result = (from c in db.x_riwayat_pendidikan
                          join e in db.x_education_level
                             on c.education_level_id equals e.id //FK
                          orderby c.modified_on descending
                          select new PendidikanViewModel
                          { //linkq
                              id = c.id,
                              school_name = c.school_name,
                              education_level_id = c.education_level_id,
                              educationName = e.name,
                              biodata_id = c.biodata_id,

                              major = c.major,
                              city = c.city,

                              country = c.country,
                              entry_year = c.entry_year,
                              graduation_year = c.graduation_year,

                              gpa = c.gpa,

                              notes = c.notes




                          }).ToList();
            }
            return result;
        }
        public static List<PendidikanViewModel> jenjangAll()
        {
            List<PendidikanViewModel> result = new List<PendidikanViewModel>();
            using (var db = new ngxsisContext())
            {
                result = (from c in db.x_education_level

                          select new PendidikanViewModel
                          { //linkq


                              education_level_id = c.id,
                              educationName = c.name,




                          }).ToList();
            }
            return result;
        }


        public static List<PendidikanViewModel> ByBiodataId(int biodata_id)
        {
            //KeahlianViewModel result = new KeahlianViewModel();
            List<PendidikanViewModel> result = new List<PendidikanViewModel>();
            using (var db = new ngxsisContext())
            {
                result = (from c in db.x_riwayat_pendidikan
                          orderby c.modified_on descending
                          where c.biodata_id == biodata_id && c.is_delete == false  //db sertifikasi nyambung ke db biodata //biodata_id sama kyk yg int biodata_id
                          select new PendidikanViewModel
                          { //linkq
                              id = c.id,
                              school_name = c.school_name,
                              education_level_id = c.education_level_id,
                              educationName = c.x_education_level.name,
                              biodata_id = c.biodata_id,

                              major = c.major,
                              city = c.city,

                              country = c.country,
                              entry_year = c.entry_year,
                              graduation_year = c.graduation_year,

                              gpa = c.gpa,

                              notes = c.notes


                          }).ToList();
                if (result == null)
                {
                    result = new List<PendidikanViewModel>();
                }
            }
            return result; //!= null ? result : new KeahlianViewModel();

        }

        //get by Id dipakai di edit dan delete
        public static PendidikanViewModel ById(int id)
        {
            PendidikanViewModel result = new PendidikanViewModel();
            using (var db = new ngxsisContext())
            {
                result = (from c in db.x_riwayat_pendidikan
                          join e in db.x_education_level
                             on c.education_level_id equals e.id //FK
                          where c.id == id
                          select new PendidikanViewModel
                          { //linkq
                              id = c.id,
                              school_name = c.school_name,
                              education_level_id = c.education_level_id,
                              biodata_id = c.biodata_id,
                              educationName = e.name,
                              major = c.major,
                              city = c.city,
                              country = c.country,
                              entry_year = c.entry_year,
                              graduation_year = c.graduation_year,

                              gpa = c.gpa,

                              notes = c.notes

                          }).FirstOrDefault();
            }
            return result != null ? result : new PendidikanViewModel();



        }
        //edit

        public static ResponseResult Update(PendidikanViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {
                    #region Create New/ Insert
                    if (entity.id == 0)
                    {
                        x_riwayat_pendidikan pend = new x_riwayat_pendidikan();
                        pend.school_name = entity.school_name;
                        pend.education_level_id = entity.education_level_id;

                        pend.major = entity.major;
                        pend.city = entity.city;

                        pend.country = entity.country;
                        pend.entry_year = entity.entry_year;
                        pend.graduation_year = entity.graduation_year;

                        pend.gpa = entity.gpa;
                        pend.modified_on = DateTime.Now;

                        pend.create_by = entity.user_id;


                        pend.create_on = DateTime.Now;
                        pend.modified_on = DateTime.Now;
                        pend.is_delete = false;
                        pend.biodata_id = entity.biodata_id;
                        pend.notes = entity.notes;




                        db.x_riwayat_pendidikan.Add(pend);
                        db.SaveChanges();
                        result.Entity = entity;
                    }

                    #endregion Edit
                    #region
                    else
                    {
                        x_riwayat_pendidikan pend = db.x_riwayat_pendidikan //
                            .Where(o => o.id == entity.id)
                            .FirstOrDefault();
                        if (pend != null) // category bisa ditulis cat saja
                        {

                            //disini ditulis semua nama tabelnya
                            pend.school_name = entity.school_name;
                            pend.education_level_id = entity.education_level_id;

                            pend.major = entity.major;
                            pend.city = entity.city;

                            pend.country = entity.country;
                            pend.entry_year = entity.entry_year;
                            pend.graduation_year = entity.graduation_year;
                            pend.modified_by = entity.user_id;               
                            pend.modified_on = DateTime.Now;
                            pend.gpa = entity.gpa;
                            pend.biodata_id = entity.biodata_id;
                            pend.notes = entity.notes;

                            db.SaveChanges();
                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "pendidikan not found";
                        }

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

        public static List<PendidikanViewModel> BycatId(long id = 0) // single gk pakai list
        {
            List<PendidikanViewModel> result = new List<PendidikanViewModel>(); //instansiasi
            using (var db = new ngxsisContext())
            {
                //gunakan ini jika 0 adalah semua (id == 0 ? v.CategoryId :id)
                result = db.x_riwayat_pendidikan
                    .Where(c => c.education_level_id == id)
                    .Select(c => new PendidikanViewModel
                    { //linkq
                        id = c.id,
                        school_name = c.school_name,
                        education_level_id = c.education_level_id,
                        educationName = c.x_education_level.name, // yg nyambung FK
                        biodata_id = c.biodata_id,

                        major = c.major,
                        city = c.city,

                        country = c.country,
                        entry_year = c.entry_year,
                        graduation_year = c.graduation_year,

                        gpa = c.gpa,

                        notes = c.notes
                    }).ToList();//artinya me return nilai pertama atau nilai default jika tidak ada
            }
            return result;


        }






        //delete
        public static ResponseResult Delete(PendidikanViewModel entity)

        {
            //id -->categoryId
            //CategoryViewModel entity --> int id

            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {
                    x_riwayat_pendidikan pend = db.x_riwayat_pendidikan
                        .Where(o => o.id == entity.id)
                        .FirstOrDefault();
                    if (pend != null)
                    {
                        //db.x_sertifikasi.Remove(sertifikasi);
                        pend.is_delete = true;
                        pend.delete_on = DateTime.Now;
                        pend.delete_by = entity.user_id;
                        db.SaveChanges();
                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "pendidikan not found";

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

        public static bool ValidationGradYear(string graduation_year, string entry_year)
        {
            try
            {
                if (int.Parse(graduation_year) < int.Parse(entry_year))
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {

               return false;
            }
        }
    }

}
