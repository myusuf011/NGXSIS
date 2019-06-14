using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ngxsis.DataModel;
using ngxsis.ViewModel;
using System.Data.Entity.Validation;

namespace ngxsis.Repository
{
    public class SumberLokerRepo
    {
        public static List<SumberLokerViewModel> All()
        {
            List<SumberLokerViewModel> result = new List<SumberLokerViewModel>();
            using (var db = new ngxsisContext())
            {
                result = (from sl in db.x_sumber_loker
                          select new SumberLokerViewModel
                          {
                              id = sl.id,
                              vacancy_source = sl.vacancy_source,
                              candidate_type = sl.candidate_type,
                              event_name = sl.event_name,
                              career_center_name = sl.career_center_name,
                              referrer_name = sl.referrer_name,
                              referrer_phone = sl.referrer_phone,
                              referrer_email = sl.referrer_email,
                              other_source = sl.other_source,
                              last_income = sl.last_income,
                              apply_date = sl.apply_date,
                          }).ToList();
            }
            return result;
        }

        //get by id
        public static SumberLokerViewModel ById(int id)
        {
            SumberLokerViewModel result = new SumberLokerViewModel();
            using (var db = new ngxsisContext())
            {
                result = (from sl in db.x_sumber_loker
                          where sl.id == id
                          select new SumberLokerViewModel
                          {

                              id = sl.id,
                             
                              vacancy_source = sl.vacancy_source,
                              candidate_type = sl.candidate_type,
                              event_name = sl.event_name,
                              career_center_name = sl.career_center_name,
                              referrer_name = sl.referrer_name,
                              referrer_phone = sl.referrer_phone,
                              referrer_email = sl.referrer_email,
                              other_source = sl.other_source,
                              last_income = sl.last_income,
                              apply_date = sl.apply_date,
                          }).FirstOrDefault();
            }
            return result != null ? result : new SumberLokerViewModel();
        }

        public static List<SumberLokerViewModel> ByBiodataId(int biodata_id)
        {
            List<SumberLokerViewModel> result = new List<SumberLokerViewModel>();
            using (var db = new ngxsisContext())
            {
                result = (from sl in db.x_sumber_loker
                          where sl.biodata_id == biodata_id
                          select new SumberLokerViewModel
                          {

                              id = sl.id,
                              vacancy_source = sl.vacancy_source,
                              candidate_type = sl.candidate_type,
                              event_name = sl.event_name,
                              career_center_name = sl.career_center_name,
                              referrer_name = sl.referrer_name,
                              referrer_phone = sl.referrer_phone,
                              referrer_email = sl.referrer_email,
                              other_source = sl.other_source,
                              last_income = sl.last_income,
                              apply_date = sl.apply_date,
                          }).ToList();
                if (result == null)
                {
                    result = new List<SumberLokerViewModel>();
                }
            }
            return result;
        }


        public static ResponseResult Update(SumberLokerViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {
                    #region Create New / Insert
                    if (entity.id == 0)
                    {
                        x_sumber_loker sumLok = new x_sumber_loker();

                        sumLok.created_by = 335887;
                        sumLok.created_on = DateTime.Now;
                        sumLok.is_delete = false;

                        sumLok.biodata_id = 1;
                        sumLok.is_delete = false;

                        sumLok.vacancy_source = entity.vacancy_source;
                        sumLok.candidate_type = entity.candidate_type;
                        sumLok.event_name = entity.event_name;
                        sumLok.career_center_name = entity.career_center_name;
                        sumLok.referrer_name = entity.referrer_name;
                        sumLok.referrer_phone = entity.referrer_phone;
                        sumLok.referrer_email = entity.referrer_email;
                        sumLok.other_source = entity.other_source;
                        sumLok.last_income = entity.last_income;
                        sumLok.apply_date = entity.apply_date;

                        db.x_sumber_loker.Add(sumLok);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    #endregion

                    #region Edit
                    else
                    {
                        x_sumber_loker sumLok = db.x_sumber_loker
                                        .Where(o => o.id == entity.id).FirstOrDefault();

                        if (sumLok != null)
                        {
                            sumLok.modified_by = 335887;
                            sumLok.modified_on = DateTime.Now;
                            sumLok.is_delete = false;
                            sumLok.vacancy_source = entity.vacancy_source;
                            sumLok.candidate_type = entity.candidate_type;
                            sumLok.event_name = entity.event_name;
                            sumLok.career_center_name = entity.career_center_name;
                            sumLok.referrer_name = entity.referrer_name;
                            sumLok.referrer_phone = entity.referrer_phone;
                            sumLok.referrer_email = entity.referrer_email;
                            sumLok.other_source = entity.other_source;
                            sumLok.last_income = entity.last_income;
                            sumLok.apply_date = entity.apply_date;

                            db.SaveChanges();
                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Sumber Loker not found";
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


        public static ResponseResult Delete(SumberLokerViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {
                    x_sumber_loker sumLok = db.x_sumber_loker
                               .Where(o => o.id == entity.id).FirstOrDefault();
                    if (sumLok != null)
                    {
                        db.x_sumber_loker.Remove(sumLok);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Sumber Loker not found";
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
