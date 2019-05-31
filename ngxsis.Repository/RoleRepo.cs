using ngxsis.DataModel;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace ngxsis.Repository
{
    public class RoleRepo
    {

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
                    }).ToList();
            }
            return result!=null ? result : new List<RoleViewModel>();
        }

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
                        LoginUserId = 1
                    }).FirstOrDefault();
            }
            return result != null ? result : new RoleViewModel();
        }
        public static List<RoleViewModel> BySearch(string search, int desc, int page,int dataPerPage)
        {
            List<RoleViewModel> result = new List<RoleViewModel>();
            using (var db = new ngxsisContext())
            {
                if (desc == 1)
                {
                    result = db.x_role
                    .Where(r => r.is_deleted == false && (r.code.Contains(search) || r.name.Contains(search)))
                    .OrderByDescending(r => r.name)
                    .Skip(page*dataPerPage)
                    .Take(dataPerPage)
                    .Select(r => new RoleViewModel
                    {
                        Id = r.id,
                        Code = r.code,
                        Name = r.name
                    }).ToList();
                }
                else
                {
                    result = db.x_role
                    .Where(r => r.is_deleted == false && (r.code.Contains(search) || r.name.Contains(search)))
                    .OrderBy(r => r.name)
                    .Skip(page*dataPerPage)
                    .Take(dataPerPage)
                    .Select(r => new RoleViewModel
                    {
                        Id = r.id,
                        Code = r.code,
                        Name = r.name
                    }).ToList();
                }
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
                    x_role cekRole = new x_role();
                    cekRole=db.x_role.Where(cr => cr.is_deleted==true&&(cr.code==entity.Code||cr.name==entity.Name)).FirstOrDefault();
                    if(cekRole!=null)
                    {
                        entity.Id=cekRole.id;
                    }
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
                catch (DbEntityValidationException ex)
                {
                    //var errorMessages = ex.EntityValidationErrors
                    //.SelectMany(x => x.ValidationErrors)
                    //.Select(x => x.PropertyName);
                    //var fullErrorMessage = string.Join("\n ", errorMessages);
                    //if (fullErrorMessage.ToUpper().Contains("CODE") && fullErrorMessage.ToUpper().Contains("CODE"))
                    //{
                    //    fullErrorMessage = "Kode dan nama role tidak boleh kosong";
                    //} else if (fullErrorMessage.ToUpper().Contains("CODE"))
                    //{
                    //    fullErrorMessage = "Kode role tidak boleh kosong";
                    //}
                    //else if (fullErrorMessage.ToUpper().Contains("NAME"))
                    //{
                    //    fullErrorMessage = "Nama role tidak boleh kosong";
                    //}
                    //result.Success = false;
                    //result.Message = fullErrorMessage;
                }
                catch (DbUpdateException ex )
                {
                    //var fullErrorMessage = ex.InnerException.InnerException.Message;
                    //if (fullErrorMessage.ToUpper().Contains("CODE") && fullErrorMessage.ToUpper().Contains("NAME"))
                    //{
                    //    fullErrorMessage = "Kode dan nama role sudah dipakai";
                    //}
                    //else if (fullErrorMessage.ToUpper().Contains("CODE"))
                    //{
                    //    fullErrorMessage = "Kode role sudah dipakai";
                    //}
                    //else if (fullErrorMessage.ToUpper().Contains("NAME"))
                    //{
                    //    fullErrorMessage = "Nama role sudah dipakai";
                    //}
                    //result.Success = false;
                    //result.Message = fullErrorMessage;
                        
                }
            }
            return result;
        }
        public static ResponseResult Delete(RoleViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            using(var db = new ngxsisContext())
            {
                try
                {
                    x_role role = db.x_role.Where(r => r.id==entity.Id).FirstOrDefault();
                    if(role!=null)
                    {
                        role.is_deleted=true;
                        role.deleted_by=1;
                        role.deleted_on=DateTime.Now;
                        db.SaveChanges();
                        result.Entity=entity;
                    }
                    else
                    {
                        result.Message="Role tidak ditemukan";
                        result.Success=false;
                    }
                }
                catch(Exception ex)
                {
                    result.Success=false;
                    result.Message=ex.Message;
                }
            }
            return result;
        }
        public static bool ByName(string name, int id)
        {
            x_role entity = new x_role();
            using(var db = new ngxsisContext())
            {
                entity=db.x_role.Where(r => r.name==name && r.is_deleted==false && r.id!=id).FirstOrDefault();
            }

            if(entity!=null)
            {
                return false;
            }
            else
            {
                return true;
            }
 
        }
        public static bool ByCode(string code, int id)
        {
            x_role entity = new x_role();
            using(var db = new ngxsisContext())
            {
                entity=db.x_role.Where(r => r.code==code && r.is_deleted==false && r.id != id).FirstOrDefault();
            }

            if(entity!=null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public static bool RelationCheck(long RoleId)
        {
            x_userrole uRole = new x_userrole();
            using(var db = new ngxsisContext())
            {
                uRole=db.x_userrole.Where(ur => ur.role_id==RoleId && ur.is_deleted==false).FirstOrDefault();
            }
            if(uRole!=null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
