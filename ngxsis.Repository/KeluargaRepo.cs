using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ngxsis.DataModel;
using ngxsis.ViewModel;

namespace ngxsis.Repository
{
    public class KeluargaRepo
    {

        public static List<KeluargaViewModel> All()
        {
            List<KeluargaViewModel> result = new List<KeluargaViewModel>();
            using (var db = new ngxsisContext())
            {
                result = (from c in db.x_keluarga
                          orderby c.modified_on descending   //tanggal descending dari lama ke baru
                          select new KeluargaViewModel
                          { //linkq
                              id = c.id,
                              family_tree_type_id = c.family_tree_type_id,
                              family_relation_id = c.family_relation_id,

                              name = c.name,
                              gender = c.gender,

                              dob = c.dob,
                              education_level_id = c.education_level_id,
                              biodata_id = c.biodata_id,
                              job = c.job,
                              notes = c.notes,

                              family_relation_name = c.x_family_relation.name,
                              education_level_name = c.x_education_level.name,
                              //@{ 
                              //    var genderList = Enum.GetValues(typeof(Gender)).OfType<Gender>().Select(m => new { Text = m.ToString(), Value = (int)m }).ToList(); 
                              //}

                          }).ToList();

              

            }
            return result;
        }

        public static List<KeluargaViewModel> ByBiodataId(int biodata_id)  //ini buat list
        {

            List<KeluargaViewModel> result = new List<KeluargaViewModel>();
            using (var db = new ngxsisContext())
            {
                result = (from c in db.x_keluarga
                          orderby c.modified_on descending

                          where c.biodata_id == biodata_id && c.is_delete == false  //db klg nyambung ke db biodata //biodata_id sama kyk yg int biodata_id
                          select new KeluargaViewModel
                          { //linkq
                              id = c.id,
                              family_tree_type_id = c.family_tree_type_id,
                              family_relation_id = c.family_relation_id,

                              name = c.name,
                              gender = c.gender,

                              dob = c.dob,
                              education_level_id = c.education_level_id,

                              biodata_id = c.biodata_id,
                              job = c.job,
                              notes = c.notes,

                              family_relation_name = c.x_family_relation.name,
                              education_level_name = c.x_education_level.name,
                          }).ToList();

                if (result == null)
                {
                    result = new List<KeluargaViewModel>();
                }
            }
            return result;
        }
        public static List<KeluargaViewModel> jeniskelAll()
        {
            List<KeluargaViewModel> result = new List<KeluargaViewModel>();
            using (var db = new ngxsisContext())
            {
                result = (from c in db.x_family_tree_type

                          select new KeluargaViewModel
                          { //linkq


                              family_tree_type_id = c.id,
                              name = c.name,




                          }).ToList();
            }
            return result;
        }

        public static List<KeluargaViewModel> hubkelAll()
        {
            List<KeluargaViewModel> result = new List<KeluargaViewModel>();
            using (var db = new ngxsisContext())
            {
                result = (from c in db.x_family_relation

                          select new KeluargaViewModel
                          { //linkq


                              family_relation_id = c.id,
                              family_relation_name = c.name,





                          }).ToList();
            }
            return result;
        }


        public static List<KeluargaViewModel> eduAll()
        {
            List<KeluargaViewModel> result = new List<KeluargaViewModel>();
            using (var db = new ngxsisContext())
            {
                result = (from c in db.x_education_level

                          select new KeluargaViewModel
                          { //linkq


                              education_level_id = c.id,
                              education_level_name = c.name,




                          }).ToList();
            }
            return result;
        }

        public static List<KeluargaViewModel> ByfamId(long id = 0) // iinya bisa 4 atau 6 kalau 0 buat default
        {
            List<KeluargaViewModel> result = new List<KeluargaViewModel>(); //instansiasi
            using (var db = new ngxsisContext()) // ?
            {
                //gunakan ini jika 0 adalah semua (id == 0 ? v.CategoryId :id)
                result = db.x_family_relation
                    .Where(v => v.family_tree_type_id == id)
                    .Select(v => new KeluargaViewModel
                    { //linkq
                        id = v.id,
                        family_tree_type_id = v.family_tree_type_id,
                        family_tree_type_name = v.x_family_tree_type.name,
                        name = v.name //name di familyrelation

                    }).ToList();//artinya me return nilai pertama atau nilai default jika tidak ada
            }
            return result;


        }





        //get by Id dipakai di edit dan delete
        public static KeluargaViewModel ById(int id)
        {
            KeluargaViewModel result = new KeluargaViewModel();
            using (var db = new ngxsisContext())
            {
                result = (from c in db.x_keluarga
                          where c.id == id // jika id di tbel x_keluarga sama dengan id yg diklik
                          select new KeluargaViewModel
                          { //linkq
                              id = c.id,
                              family_tree_type_id = c.family_tree_type_id,
                              family_relation_id = c.family_relation_id,

                              name = c.name,
                              gender = c.gender,

                              dob = c.dob,
                              education_level_id = c.education_level_id,
                              biodata_id = c.biodata_id,
                              job = c.job,
                              notes = c.notes
                          }).FirstOrDefault();
            }
            return result != null ? result : new KeluargaViewModel();



        }
        //edit
        //create
        public static ResponseResult Update(KeluargaViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {
                    #region Create New/ Insert
                    if (entity.id == 0)
                    {
                        x_keluarga kel = new x_keluarga();
                        kel.family_tree_type_id = entity.family_tree_type_id;
                        kel.family_relation_id = entity.family_relation_id;

                        kel.name = entity.name;
                        kel.gender = entity.gender;

                        kel.dob = entity.dob;
                        kel.education_level_id = entity.education_level_id;
                        kel.job = entity.job;
                        kel.created_by = entity.user_id;

                        kel.created_on = DateTime.Now;
                        kel.modified_on = DateTime.Now;
                        kel.is_delete = false;
                        kel.biodata_id = entity.biodata_id;
                        kel.notes = entity.notes;




                        db.x_keluarga.Add(kel);
                        db.SaveChanges();
                        result.Entity = entity;
                    }

                    #endregion Edit
                    #region
                    else
                    {
                        x_keluarga kel = db.x_keluarga //
                            .Where(o => o.id == entity.id)
                            .FirstOrDefault();
                        if (kel != null) // 
                        {
                            //supaya disabled bisa simpan
                            //disini ditulis semua nama tabelnya 
                            //kel.family_tree_type_id = entity.family_tree_type_id;
                            // kel.family_relation_id = entity.family_relation_id;

                            kel.name = entity.name;
                            kel.gender = entity.gender;

                            kel.dob = entity.dob;
                            kel.education_level_id = entity.education_level_id;
                            kel.job = entity.job;
                            kel.modified_by = entity.user_id;
                            kel.modified_on = DateTime.Now;

                            kel.is_delete = false;
                            kel.biodata_id = entity.biodata_id;
                            kel.notes = entity.notes;


                            db.SaveChanges();
                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "keluarga not found";
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
        public static ResponseResult Delete(KeluargaViewModel entity)

        {
            //id -->categoryId
            //CategoryViewModel entity --> int id

            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {
                    x_keluarga kel = db.x_keluarga
                        .Where(o => o.id == entity.id)
                        .FirstOrDefault();
                    if (kel != null)
                    {
                        kel.is_delete = true;
                        kel.deleted_on = DateTime.Now;
                        kel.deleted_by = entity.user_id;

                        db.SaveChanges();
                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "keluarga not found";

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
