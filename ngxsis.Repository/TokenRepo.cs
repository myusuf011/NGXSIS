using ngxsis.DataModel;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.Repository
{
    public class TokenRepo
    {
        static int num;
        public static string generateToken()
        {
            Random strToken = new Random();
            string newStr = String.Empty;
            for(int i=0;i<10;i++)
            {
                num = strToken.Next(0,9);
                newStr+=num.ToString();
            }
            using(var db = new ngxsisContext())
            {
                x_biodata bio = db.x_biodata.Where(b => b.token==newStr).FirstOrDefault();
                if(bio!=null)
                {
                    newStr = generateToken();
                }
            }
            return newStr;
        }
        public static BiodataViewModel ById(long bioId)
        {
            BiodataViewModel result = new BiodataViewModel();
            using(var db = new ngxsisContext())
            {
                result=db.x_biodata.Where(b => b.id==bioId)
                    .Select(b => new BiodataViewModel
                    {
                        id=b.id,
                        fullname = b.fullname,
                        email = b.email
                    }
                    ).FirstOrDefault();
            }
            return result;
        }
        public static ResponseResult saveToken(long bioId, string token, DateTime date, long userId)
        {
            ResponseResult result = new ResponseResult();
            using(var db = new ngxsisContext())
            {
                x_biodata bio = db.x_biodata.Where(b => b.id==bioId).FirstOrDefault();
                if(bio!=null)
                {
                    bio.modified_by=userId;
                    bio.modified_on=DateTime.Now;
                    bio.token=token;
                    bio.expired_token=date;
                    db.SaveChanges();
                }
            }
            return result;
        }
        public static ResponseResult isComplete(long bioId)
        {
            ResponseResult result = new ResponseResult();
            using(var db = new ngxsisContext())
            {
                x_biodata bio = db.x_biodata.Where(b => b.is_complete==true&&b.is_deleted==false&&b.id==bioId).FirstOrDefault();
                if(bio != null)
                {
                    result.Entity=null;
                    result.Message="Proses komplit";
                    result.Success=false;
                }else
                {
                    result.Entity=bio;
                }
            }
            return result;
        }

    }
}
