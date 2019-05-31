using ngxsis.DataModel;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.Repository
{
    public class UserRoleRepo
    {
        public static UserRoleViewModel ById(long id)
        {
            UserRoleViewModel result = new UserRoleViewModel();
            using(var db = new ngxsisContext())
            {
                result=db.x_biodata
                    .Where(b => b.addrbook_id==id)
                    .Select(b => new UserRoleViewModel
                    {
                        AddrBookId=b.addrbook_id,
                        FullName=b.fullname,
                        Email = b.x_addrbook.email
                    }).FirstOrDefault();
            }
                return result;
        }
    }
}
