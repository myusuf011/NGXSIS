using ngxsis.DataModel;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.Repository
{
    public class OrganisasiRepo
    {
        //Get All
        public static List<OrganisasiViewModel> All()
        {
            List<OrganisasiViewModel> result = new List<OrganisasiViewModel>();
            using (var db = new ngxsisContext())
            {
                result = (from c in db.x_organisasi                         
                          orderby c.modified_on descending
                          select new OrganisasiViewModel
                          {
                              id = c.id,
                              //created_by = c.created_by,
                              //created_on = c.created_on,
                              //modified_by = c.modified_by,
                              //modified_on = c.modified_on,
                              //deleted_by = c.deleted_by,
                              //deleted_on = c.deleted_on,
                              //is_delete = c.is_delete,
                              //biodata_id = c.biodata_id,
                              name = c.name,
                              position = c.position,
                              entry_year = c.entry_year,
                              exit_year = c.exit_year,
                              responsibility = c.responsibility,
                              notes = c.notes,                              
                          }).ToList();

            }
            return result;
        }

        //Get by Id
        public static OrganisasiViewModel ById(int id)
        {
            OrganisasiViewModel result = new OrganisasiViewModel();
            using (var db = new ngxsisContext())
            {
                result = (from c in db.x_organisasi
                          where c.id == id
                          select new OrganisasiViewModel
                          {
                              id = c.id,
                              //created_by = c.created_by,
                              //created_on = c.created_on,
                              //modified_by = c.modified_by,
                              //modified_on = c.modified_on,
                              //deleted_by = c.deleted_by,
                              //deleted_on = c.deleted_on,
                              //is_delete = c.is_delete,
                              //biodata_id = c.biodata_id,
                              name = c.name,
                              position = c.position,
                              entry_year = c.entry_year,
                              exit_year = c.exit_year,
                              responsibility = c.responsibility,
                              notes = c.notes
                          }).FirstOrDefault();
            }
            return result != null ? result : new OrganisasiViewModel();
        }

        //Create New & Edit
        public static ResponseResult Update(OrganisasiViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {
                    #region Create New / Insert
                    //Create
                    if (entity.id == 0)
                    {
                        x_organisasi org = new x_organisasi();

                        //org.id = entity.id;
                        org.created_by = 123;
                        org.created_on = DateTime.Now;
                        //org.modified_by = entity.modified_by;
                        org.modified_on = DateTime.Now;
                        //org.deleted_by = entity.deleted_by;
                        //org.deleted_on = entity.deleted_on;
                        org.is_delete = false;
                        org.biodata_id = 1;
                        org.name = entity.name;
                        org.position = entity.position;
                        org.entry_year = entity.entry_year;
                        org.exit_year = entity.exit_year;
                        org.responsibility = entity.responsibility;
                        org.notes = entity.notes;

                        db.x_organisasi.Add(org);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    #endregion
                    #region EDIT
                    else
                    //edit
                    {
                        x_organisasi org = db.x_organisasi
                            .Where(o => o.id == entity.id)
                            .FirstOrDefault();

                        if (org != null)
                        {
                            //org.id = entity.id;
                            //org.created_by = 123;
                            //org.created_on = DateTime.Now;
                            //org.modified_by = entity.modified_by;
                            org.modified_on = DateTime.Now;
                            //org.deleted_by = entity.deleted_by;
                            //org.deleted_on = entity.deleted_on;
                            //org.is_delete = false;
                            //org.biodata_id = 1;
                            org.name = entity.name;
                            org.position = entity.position;
                            org.entry_year = entity.entry_year;
                            org.exit_year = entity.exit_year;
                            org.responsibility = entity.responsibility;
                            org.notes = entity.notes;

                            db.SaveChanges();

                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Organisasi not found!";
                        }
                    }
                    #endregion Edit
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        //Delete
        public static ResponseResult Delete(OrganisasiViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {
                    x_organisasi org = db.x_organisasi
                        .Where(o => o.id == entity.id)
                        .FirstOrDefault();
                    if (org != null)
                    {
                        db.x_organisasi.Remove(org);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Organisasi not found!";
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
