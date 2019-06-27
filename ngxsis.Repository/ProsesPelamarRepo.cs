using ngxsis.DataModel;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.Repository
{
    public class ProsesPelamarRepo
    {
        public static long JumlahPelamar()
        {
            long result = 0;
            using(var db = new ngxsisContext())
            {
                result=db.x_biodata.Where(b => b.is_complete==false && b.is_deleted==false).Count();
            }
                return result;
        }
        public static List<ProsesPelamarViewModel> ProsesPelamarList(int desc,int page,int dataPerPage)
        {
            List<ProsesPelamarViewModel> result = new List<ProsesPelamarViewModel>();
            using (var db = new ngxsisContext())
            {
                if(desc==1)
                {
                    result=db.x_biodata
                    .Where(b => b.is_complete==false&&b.is_deleted==false)
                    .OrderByDescending(b => b.fullname)
                    .Skip(page*dataPerPage)
                    .Take(dataPerPage)
                    .Select(b => new ProsesPelamarViewModel
                    {
                        Id=b.id,
                        Fullname=b.fullname,

                        SchoolName=db.x_riwayat_pendidikan.Where(rp => rp.biodata_id==b.id&&rp.is_delete==false)
                        .OrderByDescending(rp => rp.graduation_year)
                        .Select(rp => rp.school_name).FirstOrDefault(),

                        Major=db.x_riwayat_pendidikan.Where(rp => rp.biodata_id==b.id&&rp.is_delete==false)
                        .OrderByDescending(rp => rp.graduation_year)
                        .Select(rp => rp.major)
                        .FirstOrDefault(),

                        IsProcess=b.is_process,
                        IsComplete=b.is_complete

                    }).ToList();
                }else
                {
                    result=db.x_biodata
                    .Where(b => b.is_complete==false&&b.is_deleted==false)
                    .OrderBy(b => b.fullname)
                    .Skip(page*dataPerPage)
                    .Take(dataPerPage)
                    .Select(b => new ProsesPelamarViewModel
                    {
                        Id=b.id,
                        Fullname=b.fullname,
                        SchoolName=db.x_riwayat_pendidikan.Where(rp => rp.biodata_id==b.id&&rp.is_delete==false)
                        .OrderByDescending(rp => rp.graduation_year)
                        .Select(rp => rp.school_name).FirstOrDefault(),
                        Major=db.x_riwayat_pendidikan.Where(rp => rp.biodata_id==b.id&&rp.is_delete==false)
                        .OrderByDescending(rp=>rp.graduation_year)
                        .Select(rp => rp.major)
                        .FirstOrDefault(),
                        IsProcess=b.is_process,
                        IsComplete=b.is_complete

                    }).ToList();
                }
                
            }
            return result;
        }

        public static List<JadwalViewModel> Jadwal(long id)
        {
            List<JadwalViewModel> result = new List<JadwalViewModel>();
            using(var db = new ngxsisContext())
            {
                result = db.x_rencana_jadwal_detail.Where(j => j.biodata_id == id && j.is_delete == false)
                    .Select(j => new JadwalViewModel {
                        Id = j.id,
                        JadwalId = j.rencana_jadwal_id,
                        ScheduleTypeName = j.x_rencana_jadwal.x_schedule_type.name,
                        ScheduleDate = j.x_rencana_jadwal.schedule_date,
                        ScheduleTime=j.x_rencana_jadwal.time,
                        RoName=db.x_biodata.Where(b => b.id==j.x_rencana_jadwal.ro)
                        .Select(b => b.fullname).FirstOrDefault(),
                        TroName=db.x_biodata.Where(b => b.id==j.x_rencana_jadwal.tro)
                        .Select(b=>b.fullname).FirstOrDefault(),
                        Location=j.x_rencana_jadwal.location,
                    }).ToList();
                foreach(var item in result)
                {
                    item.ScheduleDateStr=item.ScheduleDate.Value.ToString("dd MMMM yyyy");
                }
            }
                return result;
        }

        public static List<UndanganViewModel> Undangan(long id)
        {
            List<UndanganViewModel> result = new List<UndanganViewModel>();
            using(var db = new ngxsisContext())
            {
                result = db.x_undangan_detail.Where(j => j.biodata_id == id && j.is_delete == false)
                    .Select(j => new UndanganViewModel
                    {
                        Id = j.id,
                        UndanganId = j.undangan_id,
                        ScheduleTypeName = j.x_undangan.x_schedule_type.name,
                        InvitationDate = j.x_undangan.invitation_date,
                        InvitationTime = j.x_undangan.time,
                        RoName=db.x_biodata.Where(b => b.id==j.x_undangan.ro)
                        .Select(b => b.fullname).FirstOrDefault(),
                        TroName=db.x_biodata.Where(b => b.id==j.x_undangan.tro)
                        .Select(b => b.fullname).FirstOrDefault(),
                        Location=j.x_undangan.location,
                    }).ToList();
            }
            foreach(var item in result)
            {
                item.InvitationDateStr=item.InvitationDate.Value.ToString("dd MMMM yyyy");
            }
            return result;
        }

        /// <summary>
        /// Edit isProcess value
        /// </summary>
        /// <param name="BiodataId"></param>
        /// <returns></returns>
        public static ResponseResult Edit(long BiodataId, long userId)
        {
            ResponseResult result = new ResponseResult();
            using (var db = new ngxsisContext())
            {
                x_biodata bio = db.x_biodata.Where(b => b.id == BiodataId).FirstOrDefault();
                bio.is_process = true;
                bio.modified_by=userId;
                bio.modified_on=DateTime.Now;
                db.SaveChanges();
            }
            return result;

        }

        public static ResponseResult Delete(long Id,long biodataId, int type, long userId)
        {
            ResponseResult result = new ResponseResult();
            using (var db = new ngxsisContext())
            {
                if (type == 0)
                {
                    x_undangan_detail undangan_detail = db.x_undangan_detail.Where(ut => ut.undangan_id == Id && ut.biodata_id==biodataId).FirstOrDefault();
                    undangan_detail.is_delete = true;
                    undangan_detail.delete_by = userId;
                    undangan_detail.delete_on = DateTime.Now;
                    db.SaveChanges();
                }
                else
                {
                    x_rencana_jadwal_detail rencana_detail = db.x_rencana_jadwal_detail.Where(ut => ut.rencana_jadwal_id == Id && ut.biodata_id == biodataId).FirstOrDefault();
                    rencana_detail.is_delete = true;
                    rencana_detail.delete_by = userId;
                    rencana_detail.delete_on = DateTime.Now;
                    db.SaveChanges();
                }
            }
                return result;
        }
    }
}
