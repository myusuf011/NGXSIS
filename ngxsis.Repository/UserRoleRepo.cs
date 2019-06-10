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
        public static List<UserRoleViewModel> BySearch(string search)
        {
            List<UserRoleViewModel> result = new List<UserRoleViewModel>();
            using (var db = new ngxsisContext())
            {
                result = db.x_biodata.Where(bio => bio.fullname.Contains(search))
                    .Take(5)
                    .Select(bio => new UserRoleViewModel
                    {
                        BiodataId = bio.id,
                        FullName = bio.fullname,
                        AddrBookId = bio.addrbook_id,
                    }).ToList();
            }
            return result;
        }

        //Get Email From AddrBook Table
        public static UserRoleViewModel ByAddrbookId(long addrBookId)
        {
            UserRoleViewModel result = new UserRoleViewModel();
            using (var db = new ngxsisContext())
            {
                result = db.x_addrbook.Where(a => a.id == addrBookId)
                    .Select(a => new UserRoleViewModel
                    {
                        AddrBookId = a.id,
                        Email = a.email,
                    }).FirstOrDefault();
            }
            return result;
        }
        //Get Role List Related With AddrBookId
        public static List<UserRoleViewModel> RoleByAddrBookId(long addrBookId)
        {
            List<UserRoleViewModel> result = new List<UserRoleViewModel>();

            using (var db = new ngxsisContext())
            {
                result = db.x_userrole.Where(ur => ur.addrbook_id == addrBookId)
                    .Select(ur => new UserRoleViewModel
                    {
                        RoleId = ur.role_id,
                        RoleName = ur.x_role.name
                    }).ToList();
            }
            return result;
        }

        public static ResponseResult Delete(UserRoleViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            int userCount;
            using (var db = new ngxsisContext())
            {
                userCount = db.x_userrole.Where(ur => ur.addrbook_id == entity.AddrBookId).Count();
                for (int i = 0; i < userCount; i++)
                {
                    x_userrole userRole = db.x_userrole.Where(ur => ur.addrbook_id == entity.AddrBookId && ur.is_deleted == false).FirstOrDefault();
                    userRole.deleted_by = 1;
                    userRole.deleted_on = DateTime.Now;
                    userRole.is_deleted = true;
                }
            }
            return result;
        }
    }
}
