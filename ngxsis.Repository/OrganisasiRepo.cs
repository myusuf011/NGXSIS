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
                result = (from o in db.x_organisasi
                          orderby o.modified_on descending
                          select new OrganisasiViewModel
                          {
                              id = o.id,
                              name = o.name,
                              position = o.position,
                              entry_year = o.entry_year,
                              exit_year = o.exit_year,
                              responsibility = o.responsibility,
                              notes = o.notes,
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
                result = (from o in db.x_organisasi
                          where o.id == id
                          select new OrganisasiViewModel
                          {
                              id = o.id,
                              name = o.name,
                              position = o.position,
                              entry_year = o.entry_year,
                              exit_year = o.exit_year,
                              responsibility = o.responsibility,
                              notes = o.notes
                          }).FirstOrDefault();
            }
            return result != null ? result : new OrganisasiViewModel();
        }

        public static List<OrganisasiViewModel> ByBiodataId(int biodata_id)
        {
            List<OrganisasiViewModel> result = new List<OrganisasiViewModel>();
            using (var db = new ngxsisContext())
            {
                result = db.x_organisasi
                    .OrderByDescending(o => o.modified_on)
                    .Where(o => o.biodata_id == biodata_id && o.is_delete == false)
                    .Select(o => new OrganisasiViewModel
                    {
                        id = o.id,
                        name = o.name,
                        position = o.position,
                        entry_year = o.entry_year,
                        exit_year = o.exit_year,
                        responsibility = o.responsibility,
                        notes = o.notes
                    }).ToList();
                if (result == null)
                {
                    result = new List<OrganisasiViewModel>();
                }
            }
            return result;
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

                        org.created_by = entity.user_id;
                        org.created_on = DateTime.Now;
                        org.modified_on = DateTime.Now;
                        org.is_delete = false;
                        org.biodata_id = entity.biodata_id;
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
                            org.modified_by = entity.user_id;
                            org.modified_on = DateTime.Now;
                            org.biodata_id = entity.biodata_id;
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
                        org.is_delete = true;
                        org.deleted_by = entity.user_id;
                        org.deleted_on = DateTime.Now;

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

        public static bool ValidationExitYear(string exit_year, string entry_year)
        {
            try
            {
                if (int.Parse(exit_year) < int.Parse(entry_year))
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
    }
}
