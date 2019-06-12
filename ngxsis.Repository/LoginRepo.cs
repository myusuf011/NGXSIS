using ngxsis.DataModel;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.Repository
{
    public class LoginRepo
    {
        public static ResponseResultLogin cekAkun(LoginViewModel entity)
        {
            ResponseResultLogin result = new ResponseResultLogin();
            string passwordmd5 = UserRoleRepo.GetMd5Hash(entity.abpwd);

            using (var db = new ngxsisContext())
            {
                var userDetails = db.x_biodata
                    .Where(o => o.x_addrbook.email == entity.email && o.x_addrbook.abpwd == passwordmd5 || 
                    o.x_addrbook.abuid == entity.email && o.x_addrbook.abpwd == passwordmd5).FirstOrDefault();                

                if (userDetails != null)
                {
                    if (userDetails.x_addrbook.is_deleted == true)
                    {
                        result.Message = "Akun Sudah Tidak Aktif";
                        result.Success = false;
                    }
                    else
                    {
                        result.AkunID = (long)userDetails.addrbook_id;
                        result.NamaAkun = userDetails.nick_name;
                        result.TanggalUbah = userDetails.x_addrbook.modified_on;
                        result.Blokir = userDetails.x_addrbook.is_locked;
                    }                                                            
                }
                else if (userDetails == null)
                {
                    var userName = db.x_biodata
                                .Where(o => o.x_addrbook.email == entity.email || o.x_addrbook.abuid == entity.email)
                                .FirstOrDefault();

                    if (userName == null)
                    {
                        result.Message = "Invalid Email / Password";
                        result.Success = false;                        
                    }

                    else
                    {
                        result.Message = "Invalid Email / Password";
                        result.Success = false;
                        result.AkunID = (long)userName.addrbook_id;
                        result.NamaAkun = userName.nick_name;
                        result.GagalLogin = true;
                        result.TanggalUbah = userName.x_addrbook.modified_on;
                        result.Blokir = userName.x_addrbook.is_locked;
                    }
                }

            }
            return result;
        }

        public static ResponseResultLogin blokirAkun(long id)
        {
            ResponseResultLogin result = new ResponseResultLogin();

            using (var db = new ngxsisContext())
            {
                x_addrbook tblAkun = db.x_addrbook
                    .Where(o => o.id == id)
                    .FirstOrDefault();

                if (tblAkun != null)
                {
                    tblAkun.modified_on = DateTime.Now;
                    tblAkun.is_locked = true;

                    db.SaveChanges();

                    result.TanggalUbah = DateTime.Now;
                    result.Blokir = true;
                }
            }

            return result;
        }

        public static ResponseResultLogin aktifinAkun(long id)
        {
            ResponseResultLogin result = new ResponseResultLogin();

            using (var db = new ngxsisContext())
            {
                x_addrbook tblAkun = db.x_addrbook
                    .Where(o => o.id == id)
                    .FirstOrDefault();

                if (tblAkun != null)
                {
                    tblAkun.modified_on = DateTime.Now;
                    tblAkun.is_locked = false;

                    db.SaveChanges();

                    result.TanggalUbah = DateTime.Now;
                    result.Blokir = false;
                }
            }
            return result;
        }

    }
}
