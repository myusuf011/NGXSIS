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
