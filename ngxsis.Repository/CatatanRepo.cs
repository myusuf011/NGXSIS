using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ngxsis.DataModel;
using ngxsis.ViewModel;

namespace ngxsis.Repository
{
    public class CatatanRepo
    {
        public static List<CatatanViewModel> All()
        {
            List<CatatanViewModel> result = new List<CatatanViewModel>();
            using (var db = new ngxsisContext())
            {
                result = (from c in db.x_catatan
                          orderby c.modified_on descending   //tanggal descending dari lama ke baru
                          select new CatatanViewModel
                          { //linkq
                              id = c.id,
                              title = c.title,
                              note_type_id = c.note_type_id,
                              note_type_name = c.x_note_type.name,
                           
                              biodata_name = c.x_biodata.nick_name,
                              notes = c.notes
                          }).ToList();
            }
            return result;
        }

        public static List<CatatanViewModel> ByBiodataId(int biodata_id)
        {
            //KeahlianViewModel result = new KeahlianViewModel();
            List<CatatanViewModel> result = new List<CatatanViewModel>();
            using (var db = new ngxsisContext())
            {
                result = (from c in db.x_catatan
                          orderby c.modified_on descending
                          //  join e in db.x_note_type
                          //on c.note_type_id equals e.id //FK
                          where c.biodata_id == biodata_id && c.is_delete == false  //db sertifikasi nyambung ke db biodata //biodata_id sama kyk yg int biodata_id
                          select new CatatanViewModel
                          { //linkq

                              id = c.id,
                              title = c.title,
                              note_type_id = c.note_type_id,
                              note_type_name = c.x_note_type.name,
                              //user_id = c.created_by,
                              biodata_name = db.x_biodata
                              .Where(b => b.addrbook_id == c.created_by)
                              .Select(b=>b.fullname).FirstOrDefault(),
                              notes = c.notes

                          }).ToList();
                if (result == null)
                {
                    result = new List<CatatanViewModel>();
                }
            }
            return result; //!= null ? result : new KeahlianViewModel();

        }
        public static List<CatatanViewModel> jeniscatAll()
        {
            List<CatatanViewModel> result = new List<CatatanViewModel>();
            using (var db = new ngxsisContext())
            {
                result = (from c in db.x_note_type

                          select new CatatanViewModel
                          { //linkq


                              note_type_id = c.id,
                              note_type_name = c.name,




                          }).ToList();
            }
            return result;
        }


        //get by Id dipakai di edit dan delete
        public static CatatanViewModel ById(int id)
        {
            CatatanViewModel result = new CatatanViewModel();
            using (var db = new ngxsisContext())
            {
                result = (from c in db.x_catatan
                          where c.id == id
                          select new CatatanViewModel
                          { //linkq
                              id = c.id,
                         
                              title = c.title,
                              note_type_id = c.note_type_id,

                            
                              notes = c.notes
                          }).FirstOrDefault();
            }
            return result != null ? result : new CatatanViewModel();



        }
        //edit

        public static ResponseResult Update(CatatanViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {
                    #region Create New/ Insert
                    if (entity.id == 0)
                    {
                        x_catatan ca = new x_catatan();
                        ca.title = entity.title;
                        ca.note_type_id = entity.note_type_id;

                        ca.notes= entity.notes;
                        ca.created_by = entity.user_id;
                    
                        ca.created_on = DateTime.Now;
                        ca.modified_on = DateTime.Now;
                        ca.is_delete = false;
                        ca.biodata_id = entity.biodata_id;
                  
                        db.x_catatan.Add(ca);
                        db.SaveChanges();
                        result.Entity = entity;
                    }

                    #endregion Edit
                    #region
                    else
                    {
                        x_catatan ca = db.x_catatan //
                            .Where(o => o.id == entity.id)
                            .FirstOrDefault();
                        if (ca != null) // category bisa ditulis cat saja
                        {

                            //disini ditulis semua nama tabelnya
                            ca.title = entity.title;
                            ca.note_type_id = entity.note_type_id;

                            ca.notes = entity.notes;
                            //ini ws bner

                            ca.modified_by = entity.user_id;
                            ca.modified_on = DateTime.Now;
                            ca.is_delete = false;
                            ca.biodata_id = entity.biodata_id;

                            db.SaveChanges();
                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "catatan not found";
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
        public static ResponseResult Delete(CatatanViewModel entity)

        {
            //id -->categoryId
            //CategoryViewModel entity --> int id

            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {
                    x_catatan ca = db.x_catatan
                        .Where(o => o.id == entity.id)
                        .FirstOrDefault();
                    if (ca != null)
                    {
                        ca.is_delete = true;
                        ca.deleted_on = DateTime.Now;
                        ca.deleted_by = entity.user_id;
                        db.SaveChanges();
                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "catatan not found";

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

