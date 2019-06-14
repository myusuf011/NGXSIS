using ngxsis.DataModel;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.Repository
{
    public class UserRoleRepo
    {
        #region Create_Edit
        public static ResponseResult Update(UserViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            using(var db = new ngxsisContext())
            {
                #region CREATE
                if(entity.Id==null||entity.Id==0)
                {
                    x_addrbook user = new x_addrbook();
                    user.abuid=entity.Email;
                    user.email=entity.Email;
                    user.abpwd=GetMd5Hash(entity.Abpwd);

                    user.created_by=entity.UserLoginId;
                    user.created_on=DateTime.Now;
                    user.is_deleted=false;
                    user.is_locked=false;

                    db.x_addrbook.Add(user);

                    x_biodata bio = db.x_biodata.Where(b => b.id==entity.BiodataId&&b.is_deleted==false).FirstOrDefault();
                    bio.addrbook_id=user.id;
                    bio.modified_by=entity.UserLoginId;
                    bio.modified_on=DateTime.Now;
                    foreach(var item in entity.UserRoleList)
                    {
                        x_userrole userRole = new x_userrole();
                        userRole.role_id=item.RoleId;
                        userRole.addrbook_id=user.id;

                        userRole.is_deleted=false;
                        userRole.created_by=entity.UserLoginId;
                        userRole.created_on=DateTime.Now;
                        db.x_userrole.Add(userRole);
                    }

                    db.SaveChanges();
                }
                #endregion
                #region EDIT
                else
                {
                    x_addrbook user = db.x_addrbook.Where(a => a.id==entity.Id&&a.is_deleted==false).FirstOrDefault();
                    //user.abuid = entity.Username;
                    user.email = entity.Email;
                    user.modified_by=entity.UserLoginId;
                    user.modified_on=DateTime.Now;

                    List<x_userrole> urByAbId = db.x_userrole.Where(ur => ur.addrbook_id==entity.Id&&ur.is_deleted==false).ToList();
                    foreach(var ur in urByAbId)
                    {
                        ur.is_deleted=true;
                        ur.deleted_by=entity.UserLoginId;
                        ur.deleted_on=DateTime.Now;
                    }

                    foreach(var item in entity.UserRoleList)
                    {
                        x_userrole userRole = db.x_userrole.Where(ur => ur.addrbook_id==entity.Id&&ur.role_id==item.RoleId).FirstOrDefault();
                        if(userRole==null)
                        {
                            userRole=new x_userrole();
                            userRole.role_id=item.RoleId;
                            userRole.addrbook_id=user.id;
                            userRole.is_deleted=false;
                            userRole.created_by=entity.UserLoginId;
                            userRole.deleted_by=null;
                            userRole.deleted_on=null;
                            userRole.created_on=DateTime.Now;
                            db.x_userrole.Add(userRole);
                        }
                        else
                        {
                            userRole.is_deleted=false;
                            userRole.modified_by=entity.UserLoginId;
                            userRole.modified_on=DateTime.Now;
                        }
                        db.SaveChanges();
                    }
                }
            }
            #endregion
            return result;
        }
        #endregion
        #region Read
        public static List<UserViewModel> BySearch(string search)
        {
            List<UserViewModel> result = new List<UserViewModel>();
            using(var db = new ngxsisContext())
            {
                result=db.x_biodata.Where(bio => bio.fullname.Contains(search))
                    .Take(5)
                    .Select(bio => new UserViewModel
                    {
                        Id=bio.x_addrbook.is_deleted ? 0 : bio.x_addrbook.id,
                        BiodataId=bio.id,
                        FullName=bio.fullname,
                    }).ToList();
            }
            return result;
        }
        public static UserViewModel UserById(long bioId)
        {
            UserViewModel result = new UserViewModel();
            using(var db = new ngxsisContext())
            {
                result=db.x_biodata.Where(b => b.id==bioId&&b.is_deleted==false)
                    .Select(b => new UserViewModel
                    {
                        BiodataId=bioId,
                        FullName=b.fullname,
                        Username=b.x_addrbook.abuid,
                        Email=b.email,
                        Id=b.x_addrbook.is_deleted==true||b.addrbook_id==null ? null : b.addrbook_id
                    }).FirstOrDefault();
            }
            return result;
        }

        public static List<UserRoleViewModel> All()
        {
            List<UserRoleViewModel> result = new List<UserRoleViewModel>();
            using(var db = new ngxsisContext())
            {
                result=(from r in db.x_role
                        where r.is_deleted==false
                        select new UserRoleViewModel
                        {
                            RoleId=r.id,
                            RoleName=r.name
                        }).ToList();
            }
            return result!=null ? result : new List<UserRoleViewModel>();
        }

        public static List<UserRoleViewModel> RoleById(long addrbookId)
        {
            List<UserRoleViewModel> result = new List<UserRoleViewModel>();
            using(var db = new ngxsisContext())
            {
                result=(from r in db.x_role
                        join ur in db.x_userrole on r.id equals ur.role_id
                        where ur.addrbook_id==addrbookId&&ur.is_deleted==false
                        select new UserRoleViewModel
                        {
                            Id=ur.id,
                            RoleId=r.id
                        }).ToList();
            }
            return result!=null ? result : new List<UserRoleViewModel>();
        }
        #endregion
        #region DELETE
        public static ResponseResult Delete(UserViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            using(var db = new ngxsisContext())
            {
                x_addrbook ab = db.x_addrbook.Where(a => a.id==entity.Id).FirstOrDefault();
                ab.abuid=ab.abuid+"("+ab.id.ToString()+")";
                ab.is_deleted=true;
                ab.deleted_by=entity.UserLoginId;
                ab.deleted_on=DateTime.Now;
                //db.SaveChanges();
                List<x_userrole> urByAbId = db.x_userrole.Where(ur => ur.addrbook_id==entity.Id&&ur.is_deleted==false).ToList();
                foreach(var ur in urByAbId)
                {
                    ur.is_deleted=true;
                    ur.deleted_by=entity.UserLoginId;
                    ur.deleted_on=DateTime.Now;

                }
                db.SaveChanges();
            }
            return result;
        }
        #endregion
        public static string GetMd5Hash(string input)
        {
            StringBuilder sBuilder = new StringBuilder();
            using(MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                for(int i = 0;i<data.Length;i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
            }
            return sBuilder.ToString();
        }

        public static bool VerifyMd5Hash(string input,string hash)
        {
            string hashOfInput = GetMd5Hash(input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if(0==comparer.Compare(hashOfInput,hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool PassVerif(string pass,long userId)
        {
            string userPwd;
            using(var db = new ngxsisContext())
            {
                userPwd = db.x_addrbook.Where(ab=>ab.id==userId&&ab.is_deleted==false)
                    .Select(ab=>ab.abpwd).FirstOrDefault();
            }
            return VerifyMd5Hash(pass,userPwd);
        }
        public static bool ByUsername(string name, int id)
        {
            x_addrbook entity = new x_addrbook();
            using (var db = new ngxsisContext())
            {
                entity = db.x_addrbook.Where(r => (r.abuid == name || r.email==name) && r.is_deleted==false && r.id != id).FirstOrDefault();
            }

            if (entity != null)
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
