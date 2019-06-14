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
        public static List<PelamarViewModel> GetBySearch(string search, int desc, int page, int dataPerPage)
        {
            List<PelamarViewModel> result = new List<PelamarViewModel>();
            using (var db = new ngxsisContext())
            {
                if(desc == 1)
                {
                    result = db.x_biodata
                    .Where(v => v.is_deleted == false && (v.fullname.Contains(search) 
                    || v.nick_name.Contains(search)))
                    .OrderByDescending(v => v.fullname)
                    .Skip(page * dataPerPage)
                    .Take(dataPerPage)
                    .Select(v => new PelamarViewModel
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
                result.tanggal = result.dob.ToString("dd MMMM yyyy");

                if (result.gender == true)
                              fullname = v.fullname,
                              nick_name = v.nick_name,
                              email = v.email,
                              phone_number1 = v.phone_number1,
                              
                          }).ToList();
                }else
                {
                    result = db.x_biodata
                    .Where(v => v.is_deleted == false && (v.fullname.Contains(search)
                    || v.nick_name.Contains(search)))
                    .OrderBy(v => v.fullname)
                    .Skip(page * dataPerPage)
                    .Take(dataPerPage)
                    .Select(v => new PelamarViewModel
                    {
                        id = v.id,
                        fullname = v.fullname,
                        nick_name = v.nick_name,
                        email = v.email,
                        phone_number1 = v.phone_number1,

                    }).ToList();
                }
                
            }

            return result != null ? result : new List<PelamarViewModel>();
        }
    }
}
