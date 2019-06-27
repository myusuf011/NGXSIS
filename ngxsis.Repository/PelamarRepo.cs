using ngxsis.DataModel;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.Repository
{
    public class PelamarRepo
    {
      
        public static BiodataViewModel ById(long id)
        {

            //id category.Id
            BiodataViewModel result = new BiodataViewModel();
            using (var db = new ngxsisContext())
            {

                result = (from v in db.x_address
                          join c in db.x_biodata
                          on v.biodata_id equals c.id
                          where v.biodata_id == id
                          select new BiodataViewModel
                          {
                              id = v.id,
                              biodata_id = v.biodata_id,
                              fullname = c.fullname,
                              nick_name = c.nick_name,
                              pob = c.pob,
                              dob = c.dob,
                              gender = c.gender,

                              namaidentitas = c.x_identity_type.name,
                              namaagama = c.x_religion.name,
                              namastatus = c.x_marital_status.name,

                              religion_id = c.religion_id,
                              high = c.high,
                              weight = c.weight,
                              nationality = c.nationality,
                              ethnic = c.ethnic,
                              hobby = c.hobby,
                              identity_type_id = c.identity_type_id,
                              identity_no = c.identity_no,
                              email = c.email,
                              phone_number1 = c.phone_number1,
                              phone_number2 = c.phone_number2,
                              parent_phone_number = c.parent_phone_number,
                              child_sequence = c.child_sequence,
                              how_many_brothers = c.how_many_brothers,
                              marital_status_id = c.marital_status_id,
                              marriage_year = c.marriage_year,
                              company_id = c.company_id,

                              address1 = v.address1,
                              postal_code1 = v.postal_code1,
                              rt1 = v.rt1,
                              rw1 = v.rw1,
                              kelurahan1 = v.kelurahan1,
                              kecamatan1 = v.kecamatan1,
                              region1 = v.region1,
                              address2 = v.address2,
                              postal_code2 = v.postal_code2,
                              rt2 = v.rt2,
                              rw2 = v.rw2,
                              kelurahan2 = v.kelurahan2,
                              kecamatan2 = v.kecamatan2,
                              region2 = v.region2


                          }).FirstOrDefault();


                string[] w = result.dob.ToString().Split('/');
                string[] x = w[2].Split(' ');
                w[2] = x[0];

                switch (int.Parse(w[0]))
                {
                    case 1:
                        w[0] = ("Januari");
                        break;
                    case 2:
                        w[0] = ("Februari");
                        break;
                    case 3:
                        w[0] = ("Maret");
                        break;
                    case 4:
                        w[0] = ("April");
                        break;
                    case 5:
                        w[0] = ("Mei");
                        break;
                    case 6:
                        w[0] = ("Juni");
                        break;
                    case 7:
                        w[0] = ("Juli");
                        break;
                    case 8:
                        w[0] = ("Agustus");
                        break;
                    case 9:
                        w[0] = ("September");
                        break;
                    case 10:
                        w[0] = ("Oktober");
                        break;
                    case 11:
                        w[0] = ("November");
                        break;
                    case 12:
                        w[0] = ("Desember");
                        break;
                    default:
                        w[0] = (" ");
                        break;


                }

                result.tanggal = string.Format("{0} {1} {2}", w[1], w[0], w[2]);

                if (result.gender == true)
                {
                    result.namagender = "Pria";

                }
                else
                {
                    result.namagender = "Wanita";
                }

            }

            return result;

        }



        public static List<PelamarViewModel> GetBySearch(string search, int desc, int page, int dataPerPage)
        {
            List<PelamarViewModel> result = new List<PelamarViewModel>();
            using (var db = new ngxsisContext())
            {
                if(desc == 1)
                {
                    //result = db.x_biodata.
                    //Join(db.x_riwayat_pendidikan, u => u.id, uir => uir.biodata_id,
                    //(u, uir) => new { u, uir })
                    //.Where(v => v.u.is_deleted == false && (v.u.fullname.Contains(search)
                    //|| v.u.nick_name.Contains(search)))
                    //.OrderByDescending(v => v.u.fullname)
                    //.Skip(page * dataPerPage)
                    //.Take(dataPerPage)
                    //.Select(v => new PelamarViewModel
                    //{
                    //    id = v.u.id,
                    //    fullname = v.u.fullname,
                    //    nick_name = v.u.nick_name,
                    //    email = v.u.email,
                    //    phone_number1 = v.u.phone_number1,
                    //    pendidikan = v.uir.school_name,
                    //    jurusan = v.uir.major

                    //}).ToList();
                    result=db.x_biodata
                        .Where(b => b.is_deleted==false&&(b.fullname.Contains(search)
                    ||b.nick_name.Contains(search)))
                    .OrderByDescending(b => b.fullname)
                    .Skip(page*dataPerPage)
                    .Take(dataPerPage)
                    .Select(b => new PelamarViewModel
                    {
                        id=b.id,
                        fullname=b.fullname,
                        nick_name=b.nick_name,
                        email=b.email,
                        phone_number1=b.phone_number1,
                        pendidikan=db.x_riwayat_pendidikan.Where(rp => rp.biodata_id==b.id)
                        .OrderByDescending(rp => rp.graduation_year)
                        .Select(rp=>rp.school_name)
                        .FirstOrDefault(),
                        jurusan=db.x_riwayat_pendidikan.Where(rp => rp.biodata_id==b.id)
                        .OrderByDescending(rp => rp.graduation_year)
                        .Select(rp => rp.major)
                        .FirstOrDefault(),

                    }).ToList();
                }
                else
                {
                    //result = db.x_biodata.
                    //Join(db.x_riwayat_pendidikan, u => u.id, uir => uir.biodata_id,
                    //(u, uir) => new { u, uir })
                    //.Where(v => v.u.is_deleted == false && (v.u.fullname.Contains(search)
                    //|| v.u.nick_name.Contains(search)))
                    //.OrderBy(v => v.u.fullname)
                    //.Skip(page * dataPerPage)
                    //.Take(dataPerPage)
                    //.Select(v => new PelamarViewModel
                    //{
                    //    id = v.u.id,
                    //    fullname = v.u.fullname,
                    //    nick_name = v.u.nick_name,
                    //    email = v.u.email,
                    //    phone_number1 = v.u.phone_number1,
                    //    pendidikan = v.uir.school_name,
                    //    jurusan = v.uir.major

                    //}).ToList();
                    result=db.x_biodata
                        .Where(b => b.is_deleted==false&&(b.fullname.Contains(search)
                    ||b.nick_name.Contains(search)))
                    .OrderBy(b => b.fullname)
                    .Skip(page*dataPerPage)
                    .Take(dataPerPage)
                    .Select(b => new PelamarViewModel
                    {
                        id=b.id,
                        fullname=b.fullname,
                        nick_name=b.nick_name,
                        email=b.email,
                        phone_number1=b.phone_number1,
                        pendidikan=db.x_riwayat_pendidikan.Where(rp => rp.biodata_id==b.id)
                        .OrderByDescending(rp => rp.graduation_year)
                        .Select(rp => rp.school_name)
                        .FirstOrDefault(),
                        jurusan=db.x_riwayat_pendidikan.Where(rp => rp.biodata_id==b.id)
                        .OrderByDescending(rp => rp.graduation_year)
                        .Select(rp => rp.major)
                        .FirstOrDefault(),

                    }).ToList();
                }
                
            }

            return result != null ? result : new List<PelamarViewModel>();
        }



        public static ResponseResult ValidationMail(string email, long id=0)
        {

            List<BiodataViewModel> result = new List<BiodataViewModel>();
            ResponseResult validation = new ResponseResult();
            using (var db = new ngxsisContext())
            {

                result = db.x_biodata.Select(c => new BiodataViewModel
                {
                    id = c.id,
                    email = c.email,
                    phone_number1 = c.phone_number1,
                    identity_type_id = c.identity_type_id,
                    identity_no = c.identity_no



                }).ToList();

                foreach (var item in result)
                {
                    if (email == item.email && id != item.id)
                    {
                        validation.Success = false;

                    }
                }
            }

            return validation;

        }

        public static ResponseResult ValidationPhone(string phone, long id=0)
        {

            List<BiodataViewModel> result = new List<BiodataViewModel>();
            ResponseResult validation = new ResponseResult();
            using (var db = new ngxsisContext())
            {

                result = db.x_biodata.Select(c => new BiodataViewModel
                {

                    id = c.id,
                    email = c.email,
                    phone_number1 = c.phone_number1,
                    identity_type_id = c.identity_type_id,
                    identity_no = c.identity_no



                }).ToList();

                foreach (var item in result)
                {
                    if (phone == item.phone_number1 && id != item.id)
                    {
                        validation.Success = false;
                    }
                }
            }

            return validation;

        }


        public static ResponseResult ValidationIdentity(string IdentityNo, long IdentityId, long id=0)
        {

            List<BiodataViewModel> result = new List<BiodataViewModel>();
            ResponseResult validation = new ResponseResult();
            using (var db = new ngxsisContext())
            {

                result = db.x_biodata.Select(c => new BiodataViewModel
                {

                    id = c.id,
                    email = c.email,
                    phone_number1 = c.phone_number1,
                    identity_type_id = c.identity_type_id,
                    identity_no = c.identity_no,
                    namaidentitas = c.x_identity_type.name




                }).ToList();



                foreach (var item in result)
                {
                    if (IdentityNo == item.identity_no && IdentityId == item.identity_type_id && id != item.id)
                    {


                        validation.Success = false;
                        validation.Message = string.Format("{0} dengan Nomor Identitas {1} Telah Terdaftar!", item.namaidentitas, item.identity_no);

                    }
                }
            }

            return validation;

        }


        public static ResponseResult Update(BiodataViewModel entity)
        {

            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new ngxsisContext())
                {

                    #region Create New / Insert
                    if (entity.id == 0)
                    {

                        if (entity.marital_status_id == 1)
                        {
                            entity.marriage_year = null;
                        }

                        x_biodata biodata = new x_biodata();
                        biodata.fullname = entity.fullname;
                        biodata.nick_name = entity.nick_name;
                        biodata.pob = entity.pob;
                        biodata.dob = entity.dob;
                        biodata.gender = entity.gender;
                        biodata.religion_id = entity.religion_id;
                        biodata.high = entity.high;
                        biodata.weight = entity.weight;
                        biodata.nationality = entity.nationality;
                        biodata.ethnic = entity.ethnic;
                        biodata.hobby = entity.hobby;
                        biodata.identity_type_id = entity.identity_type_id;
                        biodata.identity_no = entity.identity_no;
                        biodata.email = entity.email;
                        biodata.phone_number1 = entity.phone_number1;
                        biodata.phone_number2 = entity.phone_number2;
                        biodata.parent_phone_number = entity.parent_phone_number;
                        biodata.child_sequence = entity.child_sequence;
                        biodata.how_many_brothers = entity.how_many_brothers;
                        biodata.marital_status_id = entity.marital_status_id;
                        biodata.marriage_year = entity.marriage_year;
                        biodata.company_id = 1;
                        biodata.is_deleted = false;


                        biodata.created_by = entity.user_id;
                        biodata.created_on = DateTime.Now;

                        x_address address = new x_address();
                        address.biodata_id = entity.id;
                        address.address1 = entity.address1;
                        address.postal_code1 = entity.postal_code1;
                        address.rt1 = entity.rt1;
                        address.rw1 = entity.rw1;
                        address.kelurahan1 = entity.kelurahan1;
                        address.kecamatan1 = entity.kecamatan1;
                        address.region1 = entity.region1;
                        address.address2 = entity.address2;
                        address.postal_code2 = entity.postal_code2;
                        address.rt2 = entity.rt2;
                        address.rw2 = entity.rw2;
                        address.kelurahan2 = entity.kelurahan2;
                        address.kecamatan2 = entity.kecamatan2;
                        address.region2 = entity.region2;
                        address.created_by = entity.user_id;
                        address.created_on = DateTime.Now;
                        address.is_deleted = false;


                        db.x_biodata.Add(biodata);
                        db.x_address.Add(address);



                        db.SaveChanges();

                        result.Entity = entity;


                    }
                    #endregion

                    #region edit
                    else
                    {


                        x_biodata biodata = db.x_biodata.Where(o => o.id == entity.id).FirstOrDefault();
                        x_address address = db.x_address.Where(o => o.id == entity.id).FirstOrDefault();

                        #endregion


                        biodata.fullname = entity.fullname;
                        biodata.nick_name = entity.nick_name;
                        biodata.pob = entity.pob;
                        biodata.dob = entity.dob;
                        biodata.gender = entity.gender;
                        biodata.religion_id = entity.religion_id;
                        biodata.high = entity.high;
                        biodata.weight = entity.weight;
                        biodata.nationality = entity.nationality;
                        biodata.ethnic = entity.ethnic;
                        biodata.hobby = entity.hobby;
                        biodata.identity_type_id = entity.identity_type_id;
                        biodata.identity_no = entity.identity_no;
                        biodata.email = entity.email;
                        biodata.phone_number1 = entity.phone_number1;
                        biodata.phone_number2 = entity.phone_number2;
                        biodata.parent_phone_number = entity.parent_phone_number;
                        biodata.child_sequence = entity.child_sequence;
                        biodata.how_many_brothers = entity.how_many_brothers;
                        biodata.marital_status_id = entity.marital_status_id;
                        biodata.marriage_year = entity.marriage_year;
                        biodata.company_id = 1;

                        biodata.modified_by = entity.user_id;
                        biodata.modified_on = DateTime.Now;


                        address.address1 = entity.address1;
                        address.postal_code1 = entity.postal_code1;
                        address.rt1 = entity.rt1;
                        address.rw1 = entity.rw1;
                        address.kelurahan1 = entity.kelurahan1;
                        address.kecamatan1 = entity.kecamatan1;
                        address.region1 = entity.region1;
                        address.address2 = entity.address2;
                        address.postal_code2 = entity.postal_code2;
                        address.rt2 = entity.rt2;
                        address.rw2 = entity.rw2;
                        address.kelurahan2 = entity.kelurahan2;
                        address.kecamatan2 = entity.kecamatan2;
                        address.region2 = entity.region2;
                        address.modified_by = entity.user_id;
                        address.modified_on = DateTime.Now;

                        db.SaveChanges();

                        result.Entity = entity;



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
