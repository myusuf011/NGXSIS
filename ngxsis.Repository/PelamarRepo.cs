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
        public static List<PelamarViewModel> GetBySearch(string search)
        {
            List<PelamarViewModel> result = new List<PelamarViewModel>();
            using (var db = new ngxsisContext())
            {
                result = db.x_biodata
                    .Where(v => v.is_deleted == false && (v.fullname.Contains(search)
                    || v.nick_name.Contains(search)))
                    .Select(v => new PelamarViewModel
                    {
                        id = v.id,
                        fullname = v.fullname,
                        nick_name = v.nick_name,
                        email = v.email,
                        phone_number1 = v.phone_number1,

                    }).ToList();
            }

            return result;
        }

        public static List<string> YearMarriage()
        {
            List<string> year = new List<string>();
            string d = DateTime.Now.ToString("yyyy dd MMMM ");
            string[] w = d.Split(' ');
            int a = int.Parse(w[0]);
            for (int i = a; i >= 1980; i--)
            {
                d = i.ToString();
                year.Add(d);
            }


            return year;

        }



        public static ResponseResult ValidationMail(string email)
        {

            List<ValidationModel> result = new List<ValidationModel>();
            ResponseResult validation = new ResponseResult();
            using (var db = new ngxsisContext())
            {

                result = db.x_biodata.Select(c => new ValidationModel
                {


                    email = c.email,
                    phone_number1 = c.phone_number1,
                    identity_type_id = c.identity_type_id,
                    identity_no = c.identity_no



                }).ToList();

                foreach (var item in result)
                {
                    if (email == item.email)
                    {
                        validation.Success = false;

                    }
                }
            }

            return validation;

        }

        public static ResponseResult ValidationPhone(string phone)
        {

            List<ValidationModel> result = new List<ValidationModel>();
            ResponseResult validation = new ResponseResult();
            using (var db = new ngxsisContext())
            {

                result = db.x_biodata.Select(c => new ValidationModel
                {


                    email = c.email,
                    phone_number1 = c.phone_number1,
                    identity_type_id = c.identity_type_id,
                    identity_no = c.identity_no



                }).ToList();

                foreach (var item in result)
                {
                    if (phone == item.phone_number1)
                    {
                        validation.Success = false;
                    }
                }
            }

            return validation;

        }


        public static ResponseResult ValidationIdentity(string IdentityNo, long IdentityId)
        {

            List<ValidationModel> result = new List<ValidationModel>();
            ResponseResult validation = new ResponseResult();
            using (var db = new ngxsisContext())
            {

                result = db.x_biodata.Select(c => new ValidationModel
                {


                    email = c.email,
                    phone_number1 = c.phone_number1,
                    identity_type_id = c.identity_type_id,
                    identity_no = c.identity_no,
                    identity_name = c.x_identity_type.name




                }).ToList();

                foreach (var item in result)
                {
                    if (IdentityNo == item.identity_no && IdentityId == item.identity_type_id)
                    {


                        validation.Success = false;
                        validation.Message = string.Format("{0} dengan Nomor Identitas {1} Telah Terdaftar!", item.identity_name, item.identity_no);

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

                        if (entity.high == null || entity.weight == null || entity.nationality == null ||
                             entity.child_sequence == null || entity.how_many_brothers == null ||
                            entity.address1 == null || entity.address2 == null || entity.rt1 == null || entity.rt2 == null ||
                           entity.rw1 == null || entity.rw2 == null || entity.kelurahan1 == null || entity.kelurahan2 == null ||
                           entity.kecamatan1 == null || entity.kecamatan2 == null || entity.region1 == null || entity.region2 == null ||
                           !ValidationIdentity(entity.identity_no, entity.identity_type_id).Success || !ValidationMail(entity.email).Success ||
                           !ValidationPhone(entity.phone_number1).Success)
                        {
                            result.Success = false;


                        }
                        else
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


                            biodata.created_by = db.x_biodata.Max(o => o.id) + 1;
                            biodata.created_on = DateTime.Now;

                            x_address address = new x_address();

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
                            address.created_by = db.x_biodata.Max(o => o.id) + 1; ;
                            address.created_on = DateTime.Now;
                            address.is_deleted = false;


                            db.x_biodata.Add(biodata);
                            db.x_address.Add(address);



                            db.SaveChanges();

                            result.Entity = entity;

                        }

                    }
                    #endregion

                    #region edit
                    else
                    {


                        x_biodata biodata = db.x_biodata.Where(o => o.id == entity.id).FirstOrDefault();
                        if (biodata != null)
                        {


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
                            biodata.addrbook_id = entity.addrbook_id;
                            biodata.marriage_year = entity.marriage_year;
                            biodata.company_id = entity.company_id;

                            biodata.modified_by = entity.id;
                            biodata.modified_on = DateTime.Now;


                            db.SaveChanges();

                            result.Entity = entity;

                        }
                        #endregion
                        else
                        {
                            result.Success = false;
                            result.Message = "Product Not Found!";
                        }

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
