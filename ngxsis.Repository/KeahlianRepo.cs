using ngxsis.DataModel;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.Repository
{
    public class KeahlianRepo
    {
        //Get All
        public static List<KeahlianViewModel> All()
        {
            List<KeahlianViewModel> result = new List<KeahlianViewModel>();
            using (var db = new ngxsisContext())
            {
                result = db.x_keahlian
                    .OrderByDescending(k => k.modified_on)
                    .Select(k => new KeahlianViewModel
                    {
                        id = k.id,
                        biodata_id = k.biodata_id,
                        skill_name = k.skill_name,
                        skill_level_id = k.skill_level_id,
                        skill_level_name = k.x_skill_level.name,
                        notes = k.notes
                    }).ToList();
                if (result == null)
                {
                    result = new List<KeahlianViewModel>();
                }
            }
            return result;
        }

        public static List<KeahlianViewModel> LevelAll()
        {
            List<KeahlianViewModel> result = new List<KeahlianViewModel>();
            using (var db = new ngxsisContext())
            {
                result = (from s in db.x_skill_level
                          select new KeahlianViewModel
                          {
                              skill_level_id = s.id,
                              skill_level_name = s.name
                          }).ToList();
            }
            return result;
        }

        //Get by Id
        public static KeahlianViewModel ById(int id)
        {
            KeahlianViewModel result = new KeahlianViewModel();
            using (var db = new ngxsisContext())
            {
                result = (from k in db.x_keahlian
                          join s in db.x_skill_level
                          on k.skill_level_id equals s.id
                          where k.id == id
                          select new KeahlianViewModel
                          {
                              id = k.id,
                              biodata_id = k.biodata_id,
                              skill_name = k.skill_name,
                              skill_level_id = k.skill_level_id,
                              skill_level_name = s.name,
                              notes = k.notes
                          }).FirstOrDefault();
            }
            return result != null ? result : new KeahlianViewModel();
        }

        //public static List<KeahlianViewModel> BySkillLevelId(long id = 0)
        //{
        //    // id => Skill Level Id
        //    List<KeahlianViewModel> result = new List<KeahlianViewModel>();
        //    using (var db = new ngxsisContext())
        //    {
        //        result = db.x_keahlian
        //            .Where(k => k.skill_level_id == id)
        //            .Select(k => new KeahlianViewModel
        //            {
        //                id = k.id,
        //                biodata_id = k.biodata_id,
        //                skill_name = k.skill_name,
        //                skill_level_id = k.skill_level_id,
        //                skill_level_name = k.x_skill_level.name,
        //                notes = k.notes
        //            }).ToList();
        //    }
        //    return result;
        //}

        // Create New & Edit
        public static ResponseResult Update(KeahlianViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {
                    #region Create New / Insert
                    if (entity.id == 0)
                    {
                        x_keahlian ahli = new x_keahlian();

                        ahli.created_by = 123;
                        ahli.created_on = DateTime.Now;
                        ahli.modified_on = DateTime.Now;
                        ahli.is_delete = false;
                        ahli.biodata_id = 1;
                        ahli.skill_name = entity.skill_name;
                        ahli.skill_level_id = entity.skill_level_id;
                        ahli.notes = entity.notes;

                        db.x_keahlian.Add(ahli);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    #endregion
                    #region
                    else //Edit
                    {
                        x_keahlian ahli = db.x_keahlian
                            .Where(k => k.id == entity.id)
                            .FirstOrDefault();

                        if (ahli != null)
                        {
                            ahli.created_by = 123;
                            ahli.created_on = DateTime.Now;
                            ahli.modified_on = DateTime.Now;
                            ahli.is_delete = false;
                            ahli.biodata_id = 1;
                            ahli.skill_name = entity.skill_name;
                            ahli.skill_level_id = entity.skill_level_id;
                            ahli.notes = entity.notes;

                            db.SaveChanges();

                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Category not found!";
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

        ////Delete
        public static ResponseResult Delete(KeahlianViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {
                    x_keahlian ahli = db.x_keahlian
                        .Where(k => k.id == entity.id)
                        .FirstOrDefault();
                    if (ahli != null)
                    {
                        db.x_keahlian.Remove(ahli);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Category not found!";
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
