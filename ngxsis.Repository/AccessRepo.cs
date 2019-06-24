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
        public static List<MenuViewModel> NavBar(long RoleId)
        {
            List<MenuViewModel> result = new List<MenuViewModel>();
            using(var db = new ngxsisContext())
            {
                result=db.x_menu_acces.Where(ma => ma.role_id==RoleId&&ma.is_deleted==false&&ma.x_menutree.is_deleted==false&&ma.x_menutree.menu_type=="NAVBAR")
                    .OrderBy(ma=>ma.x_menutree.menu_level).ThenBy(ma=>ma.x_menutree.menu_parent).ThenBy(ma=>ma.x_menutree.menu_order)
                    .Select(m => new MenuViewModel()
                    {
                        Id = m.x_menutree.id,
                        Title=m.x_menutree.title,
                        Level=m.x_menutree.menu_level,
                        Url = m.x_menutree.menu_url,
                        IsDropdown = db.x_menutree.Where(mt=>mt.menu_parent==m.x_menutree.id&&mt.is_deleted==false).FirstOrDefault()!=null,
                        ParentId = m.x_menutree.menu_parent,
                    }).ToList();
            }
            return result;
        }
        public static List<MenuViewModel> SideBar(long RoleId)
        {
            List<MenuViewModel> result = new List<MenuViewModel>();
            using(var db = new ngxsisContext())
            {
                result=db.x_menu_acces.Where(ma => ma.role_id==RoleId&&ma.is_deleted==false&&ma.x_menutree.is_deleted==false&&ma.x_menutree.menu_type=="SIDEBAR")
                    .OrderBy(ma => ma.x_menutree.menu_level).ThenBy(ma => ma.x_menutree.menu_parent).ThenBy(ma => ma.x_menutree.menu_order)
                    .Select(m => new MenuViewModel()
                    {
                        Id=m.x_menutree.id,
                        Title=m.x_menutree.title,
                        Level=m.x_menutree.menu_level,
                        Url=m.x_menutree.menu_url,
                        IsDropdown=db.x_menutree.Where(mt => mt.menu_parent==m.x_menutree.id&&mt.is_deleted==false).FirstOrDefault()!=null,
                        ParentId=m.x_menutree.menu_parent,
                    }).ToList();
            }
            return result;
        }
    }
}
