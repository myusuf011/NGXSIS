using ngxsis.DataModel;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.Repository
{
    public class RoleRepo
    {
        /*
        public static List<RoleViewModel> All()
        {
            List<RoleViewModel> result = new List<RoleViewModel>();
            using(var db = new ngxsisContext())
            {
                result=db.x_role
                    .Where(r => r.is_deleted==false)
                    .Select(r => new RoleViewModel
                    {
                        Id=r.id,
                        Code=r.code,
                        Name=r.name,
                        IsDeleted=false,
                        LoginUserId=1
                    }).ToList();
            }
            return result!=null ? result : new List<RoleViewModel>();
        }
        */
        public static RoleViewModel ById(long id)
        {
            RoleViewModel result = new RoleViewModel();
            using (var db = new ngxsisContext())
            {
                result = db.x_role
                    .Where(r => r.is_deleted == false && r.id == id)
                    .Select(r => new RoleViewModel
                    {
                        Id = r.id,
                        Code = r.code,
                        Name = r.name,
                        IsDeleted = false,
                        LoginUserId = 1
                    }).FirstOrDefault();
            }
            return result != null ? result : new RoleViewModel();
        }
        public static List<RoleViewModel> BySearch(string search)
        {
            List<RoleViewModel> result = new List<RoleViewModel>();
            using (var db = new ngxsisContext())
            {
                result = db.x_role
                    .Where(r => r.is_deleted == false && (r.code.Contains(search) || r.name.Contains(search)))
                    .Select(r => new RoleViewModel
                    {
                        Id = r.id,
                        Code = r.code,
                        Name = r.name,
                        IsDeleted = r.is_deleted,
                    }).ToList();
            }
            return result != null ? result : new List<RoleViewModel>();
        }
        public static ResponseResult Update(RoleViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            using (var db = new ngxsisContext())
            {
                try
                {
                    if (entity.Id == 0)
                    {
                        x_role role = new x_role();
                        role.code = entity.Code;
                        role.name = entity.Name;
                        role.is_deleted = false;
                        role.created_by = 1;
                        role.created_on = DateTime.Now;
                        db.x_role.Add(role);
                        db.SaveChanges();
                        result.Entity = entity;
                    }
                    else
                    {
                        x_role role = db.x_role.Where(r => r.id == entity.Id).FirstOrDefault();
                        if (role != null)
                        {
                            role.code = entity.Code;
                            role.name = entity.Name;
                            role.is_deleted = false;
                            role.modified_by = 1;
                            role.modified_on = DateTime.Now;
                            db.SaveChanges();
                            result.Entity = entity;
                        }
                        else
                        {
                            result.Message = "Role tidak ditemukan";
                            result.Success = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Message = ex.Message;
                }
            }
            return result;
        }
        public static ResponseResult Delete(RoleViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            using (var db = new ngxsisContext())
            {
                try
                {
                    x_role role = db.x_role.Where(r => r.id == entity.Id).FirstOrDefault();
                    if (role != null)
                    {
                        role.is_deleted = true;
                        role.deleted_by = 1;
                        role.deleted_on = DateTime.Now;
                        db.SaveChanges();
                        result.Entity = entity;
                    }
                    else
                    {
                        result.Message = "Role tidak ditemukan";
                        result.Success = false;
                    }
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Message = ex.Message;
                }
            }
            return result;
        }
    }
}
