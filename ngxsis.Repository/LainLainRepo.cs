using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ngxsis.ViewModel;
using ngxsis.DataModel;

namespace ngxsis.Repository
{
    public class LainLainRepo
    {
        public static KeteranganTambahanViewModel SelectByBiodataID(long id)
        {
            KeteranganTambahanViewModel result = new KeteranganTambahanViewModel();
            using (var db = new ngxsisContext())
            {
                result = (from b in db.x_biodata
                          join kt in db.x_keterangan_tambahan
                          on b.id equals kt.biodata_id
                          where b.id == id
                          select new KeteranganTambahanViewModel
                          {
                              biodata_id = kt.biodata_id,
                              tambahanId = kt.id,
                              created_by = kt.created_by,
                              created_on = kt.created_on,
                              modified_by = kt.modified_by,
                              modified_on = kt.modified_on,
                              deleted_by = kt.deleted_by,
                              deleted_on = kt.deleted_on,
                              emergency_contact_name = kt.emergency_contact_name,
                              emergency_contact_phone = kt.emergency_contact_phone,
                              expected_salary = kt.expected_salary,
                              is_negotiable = kt.is_negotiable,
                              start_working = kt.start_working,
                              is_ready_to_outoftown = kt.is_ready_to_outoftown,
                              is_apply_other_place = kt.is_apply_other_place,
                              apply_place = kt.apply_place,
                              selection_phase = kt.selection_phase,
                              is_ever_badly_sick = kt.is_ever_badly_sick,
                              disease_name = kt.disease_name,
                              disease_time = kt.disease_time,
                              is_ever_psychotest = kt.is_ever_psychotest,
                              psychotest_needs = kt.psychotest_needs,
                              psychotest_time = kt.psychotest_time,
                              requirementes_required = kt.requirementes_required,
                              other_notes = kt.other_notes
                          }).FirstOrDefault();
            }
            return result != null ? result : new KeteranganTambahanViewModel();
        }

        public static List<LainLainViewModel> SelectAllReferensi(long idBiodata)
        {
            List<LainLainViewModel> result = new List<LainLainViewModel>();
            using (var db = new ngxsisContext())
            {
                result = db.x_pe_referensi
                    .Where(o => o.biodata_id == idBiodata && o.is_delete == false)
                    .Select(o => new LainLainViewModel
                    {
                        biodata_id = o.biodata_id,
                        referensiId = o.id,
                        name = o.name,
                        position = o.position,
                        address_phone = o.address_phone,
                        relation = o.relation
                    }).ToList();
            }

            return result != null ? result : new List<LainLainViewModel>();
        }

        public static LainLainViewModel SelectReferensiByID(long id)
        {
            LainLainViewModel result = new LainLainViewModel();
            using (var db = new ngxsisContext())
            {
                result = (from r in db.x_pe_referensi
                          where r.id == id
                          select new LainLainViewModel
                          {
                              biodata_id = r.biodata_id,
                              referensiId = r.id,
                              name = r.name,
                              position = r.position,
                              address_phone = r.address_phone,
                              relation = r.relation
                          }).FirstOrDefault();
            }
            return result != null ? result : new LainLainViewModel();
        }

        public static ResponseResult UpdateReferensi(LainLainViewModel entity, long session)
        {
            ResponseResult result = new ResponseResult();
            using (var db = new ngxsisContext())
            {
                if (entity.referensiId == 0)
                {
                    x_pe_referensi referensi = new x_pe_referensi();
                    referensi.created_by = session;
                    referensi.created_on = DateTime.Now;
                    referensi.is_delete = false;
                    referensi.biodata_id = entity.biodata_id;
                    referensi.name = entity.name;
                    referensi.position = entity.position;
                    referensi.address_phone = entity.address_phone;
                    referensi.relation = entity.relation;

                    db.x_pe_referensi.Add(referensi);
                    db.SaveChanges();
                    result.Entity = entity;
                }
                else
                {
                    x_pe_referensi referensi = db.x_pe_referensi
                        .Where(r => r.id == entity.referensiId)
                        .FirstOrDefault();

                    referensi.modified_by = session;
                    referensi.modified_on = DateTime.Now;
                    referensi.is_delete = false;
                    referensi.biodata_id = entity.biodata_id;
                    referensi.name = entity.name;
                    referensi.position = entity.position;
                    referensi.address_phone = entity.address_phone;
                    referensi.relation = entity.relation;

                    db.SaveChanges();
                    result.Entity = entity;
                }
            }
            return result;
        }

        public static ResponseResult DeleteReferensi(LainLainViewModel entity, long session)
        {
            ResponseResult result = new ResponseResult();
            using (var db = new ngxsisContext())
            {
                try
                {
                    x_pe_referensi referensi = db.x_pe_referensi
                        .Where(r => r.id == entity.referensiId)
                        .FirstOrDefault();
                    referensi.is_delete = true;
                    referensi.deleted_by = session;
                    referensi.deleted_on = DateTime.Now;
                    db.SaveChanges();
                    result.Entity = entity;
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static ResponseResult UpdateTambahan(KeteranganTambahanViewModel entity, long session)
        {
            ResponseResult result = new ResponseResult();

            using (var db = new ngxsisContext())
            {   
                try
                {
                    x_keterangan_tambahan tambahan = db.x_keterangan_tambahan
                        .Where(r => r.id == entity.tambahanId)
                        .FirstOrDefault();

                    tambahan.modified_by = session;
                    tambahan.modified_on = DateTime.Now;
                    tambahan.is_delete = false;
                    tambahan.emergency_contact_name = entity.emergency_contact_name;
                    tambahan.emergency_contact_phone = entity.emergency_contact_phone;
                    tambahan.expected_salary = entity.expected_salary;
                    tambahan.is_negotiable = entity.is_negotiable;
                    tambahan.start_working = entity.start_working;
                    tambahan.is_ready_to_outoftown = entity.is_ready_to_outoftown;
                    tambahan.is_apply_other_place = entity.is_apply_other_place;
                    tambahan.apply_place = entity.apply_place;
                    tambahan.selection_phase = entity.selection_phase;
                    tambahan.is_ever_badly_sick = entity.is_ever_badly_sick;
                    tambahan.disease_name = entity.disease_name;
                    tambahan.disease_time = entity.disease_time;
                    tambahan.is_ever_psychotest = entity.is_ever_psychotest;
                    tambahan.psychotest_needs = entity.psychotest_needs;
                    tambahan.psychotest_time = entity.psychotest_time;
                    tambahan.requirementes_required = entity.requirementes_required;
                    tambahan.other_notes = entity.other_notes;

                    db.SaveChanges();
                    result.Entity = entity;
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Message = ex.Message;
                }
            }


            return result;
        }


    }
}
