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
    }
}
