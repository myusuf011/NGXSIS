﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ngxsis.ViewModel;
using ngxsis.DataModel;

namespace ngxsis.Repository
{
    public class ProfilRepo
    {
        public static List<ProfilViewModel> All()
        {
            List<ProfilViewModel> result = new List<ProfilViewModel>();
            using (var db = new ngxsisContext())
            {
                result = (from b in db.x_biodata
                          join pd in db.x_riwayat_pendidikan on b.id equals pd.biodata_id
                          join pk in db.x_riwayat_pekerjaan on b.id equals pk.biodata_id
                          join pl in db.x_riwayat_pelatihan on b.id equals pl.biodata_id
                          join s in db.x_sertifikasi on b.id equals s.biodata_id
                          join kt in db.x_keahlian on b.id equals kt.biodata_id
                          join sl in db.x_skill_level on kt.skill_level_id equals sl.id
                          select new ProfilViewModel
                          {
                              id = b.id,
                              FullName = b.fullname,
                              DOB = b.dob,
                              Gender = b.gender,

                              EntryYear = pd.entry_year,
                              GraduationYear = pd.graduation_year,
                              Major = pd.major,
                              GPA = pd.gpa,
                              Country = pd.country,
                              SchoolName = pd.school_name,

                              CompanyName = pk.company_name,
                              JoinYear = pk.join_year,
                              ResignYear = pk.resign_year,
                              LastPosition = pk.last_position,
                              Income = pk.income,

                              SkillName = kt.skill_name,
                              SkillLevel = sl.name,

                              TrainingName = pl.training_name,
                              TrainingYear = pl.training_year,

                              ValidStartYear = s.valid_start_year,
                              UntilYear = s.until_year,
                              CertificateName = s.certificate_name
                              
                          }).ToList();
            }
            return result;
        }
    }
}