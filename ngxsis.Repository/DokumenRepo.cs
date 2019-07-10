using ngxsis.DataModel;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ngxsis.Repository
{
    public class DokumenRepo
    {
        public static List<DokumenViewModel> All(long biodata_id)
        {
            List<DokumenViewModel> result = new List<DokumenViewModel>();
            using (var db = new ngxsisContext())
            {
                result = db.x_biodata_attachment.Where(ba=>ba.biodata_id==biodata_id&&ba.is_delete==false)
                    .Select(ba=>new DokumenViewModel
                          {
                              id = ba.id,
                              file_name = ba.file_name,
                              notes = ba.notes,
                              is_photo = ba.is_photo,
                              file_path = ba.file_path,
                              biodata_id = ba.biodata_id
                          }).ToList();       
            }
            return result != null ? result : new List<DokumenViewModel>();
        }

        public static DokumenViewModel ById(long id)
        {
            DokumenViewModel result = new DokumenViewModel();
            using (var db = new ngxsisContext())
            {
                result=db.x_biodata_attachment.Where(ba => ba.id==id&&ba.is_delete==false)
                     .Select(ba => new DokumenViewModel
                     {
                         id=ba.id,
                         file_name=ba.file_name,
                         notes=ba.notes,
                         is_photo=ba.is_photo,
                         file_path=ba.file_path,
                         biodata_id=ba.biodata_id,
                     }).FirstOrDefault();
            }
            return result != null ? result : new DokumenViewModel();
        }

        public static ResponseResult Update(DokumenViewModel entity, long userId)
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

                        dokumen.file_name = entity.file_name;
                        dokumen.file_path = entity.file_path;
                        dokumen.is_photo = entity.is_photo;
                        dokumen.created_by = userId;
                        dokumen.created_on = DateTime.Now;
                        dokumen.is_delete = false;
                        dokumen.biodata_id=entity.biodata_id;
                        dokumen.notes = entity.notes;
                        db.x_biodata_attachment.Add(dokumen);
                        db.SaveChanges();
                        result.Entity = dokumen;
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
                            dokumen.id = entity.id;
                            dokumen.file_path=entity.file_path;
                            dokumen.file_name = entity.file_name;                            
                            dokumen.notes = entity.notes;
                            dokumen.is_photo=entity.is_photo;

                            dokumen.modified_by=userId;
                            dokumen.modified_on=DateTime.Now;
                            db.SaveChanges();
                            result.Entity = dokumen;
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

        public static ResponseResult Delete(DokumenViewModel entity, long userId)

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
                        dokumen.deleted_by=userId;
                        dokumen.deleted_on=DateTime.Now;
                        dokumen.is_delete=true;
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
