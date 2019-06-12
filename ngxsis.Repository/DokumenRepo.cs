using ngxsis.DataModel;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.Repository
{
    public class DokumenRepo
    {
        public static List<DokumenViewModel> All()
        {
            List<DokumenViewModel> result = new List<DokumenViewModel>();
            using (var db = new ngxsisContext())
            {
                result = (from v in db.x_biodata_attachment
                          select new DokumenViewModel
                          {
                              id = v.id,
                              file_name = v.file_name,
                              notes = v.notes

                          }).ToList();
                         
            }
            return result;
        }

        public static DokumenViewModel ById(int id)
        {
            DokumenViewModel result = new DokumenViewModel();
            using (var db = new ngxsisContext())
            {
                result = (from v in db.x_biodata_attachment
                          where v.id == id
                          select new DokumenViewModel
                          {
                              id = v.id,
                              file_name = v.file_name,
                              notes = v.notes

                          }).FirstOrDefault();
            }
            return result != null ? result : new DokumenViewModel();
        }

        public static ResponseResult Update(DokumenViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {
                    #region Create New
                    if (entity.id == 0)
                    {
                        x_biodata_attachment dokumen = new x_biodata_attachment();

                        dokumen.id = entity.id;
                        dokumen.file_name = entity.file_name;

                        dokumen.created_by = 335887;
                        dokumen.created_on = DateTime.Now;
                        dokumen.is_delete = false;
                        dokumen.biodata_id = 1;
                        dokumen.notes = entity.notes;




                        db.x_biodata_attachment.Add(dokumen);
                        db.SaveChanges();
                        result.Entity = entity;
                    }

                    #endregion 
                    #region Edit
                    else
                    {
                        x_biodata_attachment dokumen = db.x_biodata_attachment
                            .Where(o => o.id == entity.id)
                            .FirstOrDefault();
                        if (dokumen != null) 
                        {

                            //disini ditulis semua nama tabelnya
                            dokumen.id = entity.id;
                            dokumen.file_name = entity.file_name;                            
                            dokumen.notes = entity.notes;
                            
                            db.SaveChanges();
                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Dokumen tidak ditemukan!";
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

        public static ResponseResult Delete(DokumenViewModel entity)

        {
            
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {
                    x_biodata_attachment dokumen = db.x_biodata_attachment
                        .Where(o => o.id == entity.id)
                        .FirstOrDefault();
                    if (dokumen != null)
                    {
                        db.x_biodata_attachment.Remove(dokumen);
                        db.SaveChanges();
                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Dokumen tidak ditemukan!";

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
    }
}
