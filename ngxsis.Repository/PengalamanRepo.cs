using ngxsis.DataModel;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.Repository
{
    public class PengalamanRepo
    {
        //Get All
        public static List<PengalamanViewModel> All()
        {
            List<PengalamanViewModel> result = new List<PengalamanViewModel>();
            using (var db = new ngxsisContext())
            {
                result = db.x_riwayat_pekerjaan
                    .OrderByDescending(p => p.modified_on)
                    .Select(p => new PengalamanViewModel
                    {
                        id = p.id,
                        biodata_id = p.biodata_id,
                        company_name = p.company_name,
                        city = p.city,
                        country = p.country,
                        join_year = p.join_year,
                        join_month = p.join_month,
                        resign_year = p.resign_year,
                        resign_month = p.resign_month,
                        last_position = p.last_position,
                        income = p.income,
                        is_it_related = p.is_it_related,
                        about_job = p.about_job,
                        exit_reason = p.exit_reason,
                        notes = p.notes

                    }).ToList();
                if (result == null)
                {
                    result = new List<PengalamanViewModel>();
                }
            }
            return result;
        }

        //Get by Id
        public static PengalamanViewModel ById(int id)
        {
            PengalamanViewModel result = new PengalamanViewModel();
            using (var db = new ngxsisContext())
            {
                result = (from p in db.x_riwayat_pekerjaan
                          where p.id == id
                          select new PengalamanViewModel
                          {
                              id = p.id,
                              biodata_id = p.biodata_id,
                              company_name = p.company_name,
                              city = p.city,
                              country = p.country,
                              join_year = p.join_year,
                              join_month = p.join_month,
                              resign_year = p.resign_year,
                              resign_month = p.resign_month,
                              last_position = p.last_position,
                              income = p.income,
                              is_it_related = p.is_it_related,
                              about_job = p.about_job,
                              exit_reason = p.exit_reason,
                              notes = p.notes
                          }).FirstOrDefault();
            }
            return result != null ? result : new PengalamanViewModel();
        }

        public static List<PengalamanViewModel> ByBiodataId(int biodata_id)
        {
            List<PengalamanViewModel> result = new List<PengalamanViewModel>();
            using (var db = new ngxsisContext())
            {
                result = (from p in db.x_riwayat_pekerjaan
                          orderby p.modified_on descending
                          where p.biodata_id == biodata_id && p.is_deleted == false
                          select new PengalamanViewModel
                          {
                              id = p.id,
                              biodata_id = p.biodata_id,
                              company_name = p.company_name,
                              city = p.city,
                              country = p.country,
                              join_year = p.join_year,
                              join_month = p.join_month,
                              resign_year = p.resign_year,
                              resign_month = p.resign_month,
                              last_position = p.last_position,
                              income = p.income,
                              is_it_related = p.is_it_related,
                              about_job = p.about_job,
                              exit_reason = p.exit_reason,
                              notes = p.notes
                          }).ToList();
                foreach (var item in result)
                {


                    item.proyek = (from o in db.x_riwayat_proyek
                                   where item.id == o.riwayat_pekerjaan_id && o.is_deleted == false
                                   select new RiwayatProyekModel
                                   {


                                       id = o.id,
                                       riwayat_pekerjaan_id = o.riwayat_pekerjaan_id,
                                       poject_name = o.poject_name,
                                       project_position = o.project_position,
                                       start_year = o.start_year,
                                       client = o.client,
                                       description = o.description,
                                       project_duration = o.project_duration,
                                       periodname = o.x_time_period.name,
                                       start_month = o.start_month


                                   }).ToList();

                }

                if (result == null)
                {
                    result = new List<PengalamanViewModel>();
                }
            }
            return result;
        }

        // Create New & Edit
        public static ResponseResult Update(PengalamanViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {
                    #region Create New / Insert
                    if (entity.id == 0)
                    {
                        x_riwayat_pekerjaan job = new x_riwayat_pekerjaan();

                        job.created_by = entity.user_id;
                        job.created_on = DateTime.Now;
                        job.modified_on = DateTime.Now;
                        job.is_deleted = false;
                        job.biodata_id = entity.biodata_id;
                        job.company_name = entity.company_name;
                        job.city = entity.city;
                        job.country = entity.country;
                        job.join_year = entity.join_year;
                        job.join_month = entity.join_month;
                        job.resign_year = entity.resign_year;
                        job.resign_month = entity.resign_month;
                        job.last_position = entity.last_position;
                        job.income = entity.income;
                        job.is_it_related = entity.is_it_related;
                        job.about_job = entity.about_job;
                        job.exit_reason = entity.exit_reason;
                        job.notes = entity.notes;

                        db.x_riwayat_pekerjaan.Add(job);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    #endregion
                    #region
                    else //Edit
                    {
                        x_riwayat_pekerjaan job = db.x_riwayat_pekerjaan
                            .Where(p => p.id == entity.id)
                            .FirstOrDefault();

                        if (job != null)
                        {
                            //job.created_by=123;
                            //job.created_on=DateTime.Now;
                            job.modified_by = entity.user_id;
                            job.modified_on = DateTime.Now;
                            job.is_deleted = false;
                            job.biodata_id = entity.biodata_id;
                            job.company_name = entity.company_name;
                            job.city = entity.city;
                            job.country = entity.country;
                            job.join_year = entity.join_year;
                            job.join_month = entity.join_month;
                            job.resign_year = entity.resign_year;
                            job.resign_month = entity.resign_month;
                            job.last_position = entity.last_position;
                            job.income = entity.income;
                            job.is_it_related = entity.is_it_related;
                            job.about_job = entity.about_job;
                            job.exit_reason = entity.exit_reason;
                            job.notes = entity.notes;

                            db.SaveChanges();

                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Pengalaman not found!";
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
        public static ResponseResult Delete(PengalamanViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {
                    x_riwayat_pekerjaan job = db.x_riwayat_pekerjaan
                        .Where(p => p.id == entity.id)
                        .FirstOrDefault();
                    if (job != null)
                    {
                        job.is_deleted = true;
                        job.deleted_by = entity.user_id;
                        job.deleted_on = DateTime.Now;

                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Pengalaman not found!";
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

        public static bool ValidationResignTime(string join_month, string join_year, string resign_month, string resign_year)
        {
            try
            {
                if (int.Parse(resign_year) < int.Parse(join_year) || (int.Parse(resign_year) == int.Parse(join_year) && int.Parse(resign_month) < int.Parse(join_month)))
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


        public static PengalamanViewModel ByIdProyek(int id)
        {
            PengalamanViewModel result = new PengalamanViewModel();
            using (var db = new ngxsisContext())
            {
                result = (from p in db.x_riwayat_pekerjaan
                          where p.id == id
                          select new PengalamanViewModel
                          {
                              id = p.id,
                              biodata_id = p.biodata_id,
                              company_name = p.company_name,
                              city = p.city,
                              country = p.country,
                              join_year = p.join_year,
                              join_month = p.join_month,
                              resign_year = p.resign_year,
                              resign_month = p.resign_month,
                              last_position = p.last_position,
                              income = p.income,
                              is_it_related = p.is_it_related,
                              about_job = p.about_job,
                              exit_reason = p.exit_reason,
                              notes = p.notes
                          }).FirstOrDefault();
            }
            return result != null ? result : new PengalamanViewModel();
        }


        public static ResponseResult UpdateProject(RiwayatProyekModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {
                    #region Create New / Insert
                    if (entity.id == 0)
                    {
                        x_riwayat_proyek job = new x_riwayat_proyek();

                        job.created_by = entity.user_id;
                        job.created_on = DateTime.Now;
                        job.riwayat_pekerjaan_id = entity.riwayat_pekerjaan_id;
                        job.is_deleted = false;
                        job.start_year = entity.start_year;
                        job.start_month = entity.start_month;
                        job.poject_name = entity.poject_name;
                        job.project_duration = entity.project_duration;
                        job.time_period_id = entity.time_period_id;
                        job.client = entity.client;
                        job.project_position = entity.project_position;
                        job.description = entity.description;


                        db.x_riwayat_proyek.Add(job);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    #endregion
                    #region
                    else //Edit
                    {
                        x_riwayat_proyek job = db.x_riwayat_proyek
                            .Where(p => p.id == entity.id)
                            .FirstOrDefault();

                        if (job != null)
                        {

                            job.modified_by = entity.user_id;
                            job.modified_on = DateTime.Now;

                            job.is_deleted = false;
                            job.start_year = entity.start_year;
                            job.start_month = entity.start_month;
                            job.poject_name = entity.poject_name;
                            job.project_duration = entity.project_duration;
                            job.time_period_id = entity.time_period_id;
                            job.client = entity.client;
                            job.project_position = entity.project_position;
                            job.description = entity.description;

                            db.SaveChanges();

                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Pengalaman not found!";
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

        public static RiwayatProyekModel ProjectById(long id)
        {
            RiwayatProyekModel result = new RiwayatProyekModel();
            using (var db = new ngxsisContext())
            {
                result = (from o in db.x_riwayat_proyek
                          where o.id == id
                          select new RiwayatProyekModel
                          {
                              id = o.id,
                              riwayat_pekerjaan_id = o.riwayat_pekerjaan_id,
                              poject_name = o.poject_name,
                              project_position = o.project_position,
                              start_year = o.start_year,
                              client = o.client,
                              description = o.description,
                              project_duration = o.project_duration,
                              time_period_id = o.time_period_id,
                              start_month = o.start_month


                          }).FirstOrDefault();
            }
            return result != null ? result : new RiwayatProyekModel();
        }

        public static ResponseResult DeleteProject(RiwayatProyekModel entity)
        {
            ResponseResult result = new ResponseResult();

            try
            {
                using (var db = new ngxsisContext())
                {

                    x_riwayat_proyek job = db.x_riwayat_proyek
                        .Where(p => p.id == entity.id)
                        .FirstOrDefault();
                    if (job != null)
                    {
                        job.is_deleted = true;
                        job.deleted_on = DateTime.Now;
                        job.deleted_by = entity.user_id;

                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Pengalaman not found!";
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