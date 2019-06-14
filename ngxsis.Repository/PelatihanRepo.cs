using ngxsis.DataModel;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.Repository
{
    public class PelatihanRepo
    {
        //Get All
        public static List<PelatihanViewModel> All()
        {
            List<PelatihanViewModel> result = new List<PelatihanViewModel>();
            using (var db = new ngxsisContext())
            {
                result = db.x_riwayat_pelatihan
                    .OrderByDescending(p => p.modified_on)
                    .Select(p => new PelatihanViewModel
                    {
                        id = p.id,
                        biodata_id = p.biodata_id,
                        training_name = p.training_name,
                        organizer = p.organizer,
                        training_year = p.training_year,
                        training_month = p.training_month,
                        training_duration = p.training_duration,
                        time_period_id = p.time_period_id,
                        time_period_name = p.x_time_period.name,
                        city = p.city,
                        country = p.country,
                        notes = p.notes
                    }).ToList();
                if (result == null)
                {
                    result = new List<PelatihanViewModel>();
                }
            }
            return result;
        }

        public static List<PelatihanViewModel> PeriodAll()
        {
            List<PelatihanViewModel> result = new List<PelatihanViewModel>();
            using (var db = new ngxsisContext())
            {
                result = (from t in db.x_time_period
                          select new PelatihanViewModel
                          {
                              time_period_id = t.id,
                              time_period_name = t.name
                          }).ToList();
            }
            return result;
        }

        //Get by Id
        public static PelatihanViewModel ById(int id)
        {
            PelatihanViewModel result = new PelatihanViewModel();
            using (var db = new ngxsisContext())
            {
                result = (from p in db.x_riwayat_pelatihan
                          join t in db.x_time_period
                          on p.time_period_id equals t.id
                          where p.id == id
                          select new PelatihanViewModel
                          {
                              id = p.id,
                              biodata_id = p.biodata_id,
                              training_name = p.training_name,
                              organizer = p.organizer,
                              training_year = p.training_year,
                              training_month = p.training_month,
                              training_duration = p.training_duration,
                              time_period_id = p.time_period_id,
                              time_period_name = t.name,
                              city = p.city,
                              country = p.country,
                              notes = p.notes
                          }).FirstOrDefault();
            }
            return result != null ? result : new PelatihanViewModel();
        }

        // Create New & Edit
        public static ResponseResult Update(PelatihanViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {
                    #region Create New / Insert
                    if (entity.id == 0)
                    {
                        x_riwayat_pelatihan training = new x_riwayat_pelatihan();

                        training.created_by = 123;
                        training.created_on = DateTime.Now;
                        training.modified_on = DateTime.Now;
                        training.is_delete = false;
                        training.biodata_id = 1;
                        training.training_name = entity.training_name;
                        training.organizer = entity.organizer;
                        training.training_year = entity.training_year;
                        training.training_month = entity.training_month;
                        training.training_duration = entity.training_duration;
                        training.time_period_id = entity.time_period_id;
                        training.city = entity.city;
                        training.country = entity.country;
                        training.notes = entity.notes;

                        db.x_riwayat_pelatihan.Add(training);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    #endregion
                    #region
                    else //Edit
                    {
                        x_riwayat_pelatihan training = db.x_riwayat_pelatihan
                            .Where(p => p.id == entity.id)
                            .FirstOrDefault();

                        if (training != null)
                        {
                            training.created_by = 123;
                            training.created_on = DateTime.Now;
                            training.modified_on = DateTime.Now;
                            training.is_delete = false;
                            training.biodata_id = 1;
                            training.training_name = entity.training_name;
                            training.organizer = entity.organizer;
                            training.training_year = entity.training_year;
                            training.training_month = entity.training_month;
                            training.training_duration = entity.training_duration;
                            training.time_period_id = entity.time_period_id;
                            training.city = entity.city;
                            training.country = entity.country;
                            training.notes = entity.notes;

                            db.SaveChanges();

                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Pelatihan not found!";
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
        public static ResponseResult Delete(PelatihanViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {
                    x_riwayat_pelatihan training = db.x_riwayat_pelatihan
                        .Where(p => p.id == entity.id)
                        .FirstOrDefault();
                    if (training != null)
                    {
                        db.x_riwayat_pelatihan.Remove(training);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Pelatihan not found!";
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