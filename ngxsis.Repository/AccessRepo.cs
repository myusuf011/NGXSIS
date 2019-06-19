using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ngxsis.ViewModel;
using ngxsis.DataModel;

namespace ngxsis.Repository
{
    public class AccessRepo
    {
        public static List<SelectAccessViewModel> SelectCompany(long id)
        {
            List<SelectAccessViewModel> result = new List<SelectAccessViewModel>();
            using (var db = new ngxsisContext())
            {
                result = db.x_biodata
                    .Where(o => o.addrbook_id == id && o.x_company.is_delete == false)
                    .Select(o => new SelectAccessViewModel
                    {
                        CompanyId = o.company_id,
                        CompanyName = o.x_company.name
                    }).ToList();
            }
            return result;
        }

        public static List<SelectAccessViewModel> SelectRole(long id)
        {
            List<SelectAccessViewModel> result = new List<SelectAccessViewModel>();
            using (var db = new ngxsisContext())
            {
                result = db.x_userrole
                    .Where(o => o.addrbook_id == id && o.x_role.is_deleted == false && o.is_deleted == false)                    
                    .Select(o => new SelectAccessViewModel
                    {
                        RoleId = o.role_id,
                        RoleName = o.x_role.name
                    }).ToList();
            }
            return result;
        }
    }
}
