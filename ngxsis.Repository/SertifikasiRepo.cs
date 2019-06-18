using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ngxsis.DataModel;
using ngxsis.ViewModel;

namespace ngxsis.Repository
{
    public class SertifikasiRepo
    {
        public static List<SertifikasiViewModel> All()
        {
            List<SertifikasiViewModel> result = new List<SertifikasiViewModel>();
            using (var db = new ngxsisContext())
            {
                result = (from c in db.x_sertifikasi
                          orderby c.modified_on descending   //tanggal descending dari lama ke baru
                          select new SertifikasiViewModel
                          { //linkq
                              id = c.id,
                              certificate_name = c.certificate_name,
                              publisher = c.publisher,

                              valid_start_year = c.valid_start_year,
                              valid_start_month = c.valid_start_month,

                              until_year = c.until_year,
                              until_month = c.until_month,
                              notes = c.notes




                          }).ToList();
            }
            return result;
        }


        public static List<SertifikasiViewModel> ByBiodataId(int biodata_id)
        {
            //KeahlianViewModel result = new KeahlianViewModel();
            List<SertifikasiViewModel> result = new List<SertifikasiViewModel>();
            using (var db = new ngxsisContext())
            {
                result = (from c in db.x_sertifikasi
                          orderby c.modified_on descending
                          where c.biodata_id == biodata_id && c.is_delete == false  //db sertifikasi nyambung ke db biodata //biodata_id sama kyk yg int biodata_id
                          select new SertifikasiViewModel
                          { //linkq
                              id = c.id,
                              certificate_name = c.certificate_name,
                              publisher = c.publisher,

                              valid_start_year = c.valid_start_year,
                              valid_start_month = c.valid_start_month,

                              until_year = c.until_year,
                              until_month = c.until_month,
                              notes = c.notes
                          }).ToList();
                if (result == null)
                {
                    result = new List<SertifikasiViewModel>();
                }
            }
            return result; //!= null ? result : new KeahlianViewModel();

        }
        //get by Id dipakai di edit dan delete
        public static SertifikasiViewModel ById(int id)
        {
            SertifikasiViewModel result = new SertifikasiViewModel();
            using (var db = new ngxsisContext())
            {
                result = (from c in db.x_sertifikasi
                          where c.id == id
                          select new SertifikasiViewModel
                          { //linkq
                              id = c.id,
                              certificate_name = c.certificate_name,
                              publisher = c.publisher,

                              valid_start_year = c.valid_start_year,
                              valid_start_month = c.valid_start_month,

                              until_year = c.until_year,
                              until_month = c.until_month,
                              notes = c.notes
                          }).FirstOrDefault();
            }
            return result != null ? result : new SertifikasiViewModel();



        }
        //edit

        public static ResponseResult Update(SertifikasiViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {
                    #region Create New/ Insert
                    if (entity.id == 0)
                    {
                        x_sertifikasi sertifikasi = new x_sertifikasi();
                        sertifikasi.certificate_name = entity.certificate_name;
                        sertifikasi.publisher = entity.publisher;
                        sertifikasi.valid_start_year = entity.valid_start_year;
                        sertifikasi.valid_start_month = entity.valid_start_month;
                        sertifikasi.until_year = entity.until_year;
                        sertifikasi.until_month = entity.until_month;
                        sertifikasi.created_by = entity.user_id;                      
                        sertifikasi.created_on = DateTime.Now;
                        sertifikasi.modified_on = DateTime.Now;
                        sertifikasi.is_delete = false;
                        sertifikasi.biodata_id = entity.biodata_id;
                        sertifikasi.notes = entity.notes;




                        db.x_sertifikasi.Add(sertifikasi);
                        db.SaveChanges();
                        result.Entity = entity;
                    }

                    #endregion Edit
                    #region
                    else
                    {
                        x_sertifikasi sertifikasi = db.x_sertifikasi //
                            .Where(o => o.id == entity.id)
                            .FirstOrDefault();
                        if (sertifikasi != null) // category bisa ditulis cat saja
                        {

                            //disini ditulis semua nama tabelnya
                            sertifikasi.certificate_name = entity.certificate_name;
                            sertifikasi.publisher = entity.publisher;

                            sertifikasi.valid_start_year = entity.valid_start_year;
                            sertifikasi.valid_start_month = entity.valid_start_month;
                  
                            sertifikasi.modified_by = entity.user_id;
                            sertifikasi.modified_on = DateTime.Now;
                            sertifikasi.until_year = entity.until_year;
                            sertifikasi.until_month = entity.until_month;
                            sertifikasi.notes = entity.notes;
                            sertifikasi.biodata_id = entity.biodata_id;
                            db.SaveChanges();
                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "sertifikasi not found";
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

        //delete
        public static ResponseResult Delete(SertifikasiViewModel entity)

        {
            //id -->categoryId
            //CategoryViewModel entity --> int id

            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {
                    x_sertifikasi sertifikasi = db.x_sertifikasi
                        .Where(o => o.id == entity.id)
                        .FirstOrDefault();
                    if (sertifikasi != null)
                    {
                        //db.x_sertifikasi.Remove(sertifikasi);
                        sertifikasi.is_delete = true;
                        sertifikasi.deleted_on = DateTime.Now;
                   
                        sertifikasi.deleted_by = entity.user_id;
                        db.SaveChanges();
                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "sertifikasi not found";

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
        public static bool ValidationBerlakuYear(string valid_start_year, string valid_start_month, string until_year, string until_month)
        {
            try
            {
                if (int.Parse(until_year) < int.Parse(valid_start_year) || (int.Parse(until_year) == int.Parse(valid_start_year) && int.Parse(until_month) < int.Parse(valid_start_month)))
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



