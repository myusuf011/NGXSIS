using ngxsis.DataModel;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.Repository
{
    public class AktivasiAkunRepo
    {
        public static AktivasiAkunViewModel SelectAkun(long idBiodata)
        {
            AktivasiAkunViewModel cari = new AktivasiAkunViewModel();
            AktivasiAkunViewModel result = new AktivasiAkunViewModel();
            using (var db = new ngxsisContext())
            {
                cari = db.x_biodata
                    .Where(o => o.id == idBiodata)
                    .Select(o => new AktivasiAkunViewModel
                    {
                        addrbook_id = o.addrbook_id
                    }).FirstOrDefault();

                if (cari.addrbook_id == null)
                {
                    return new AktivasiAkunViewModel();
                }
                else
                {
                    result = db.x_biodata
                    .Where(o => o.id == idBiodata)
                    .Select(o => new AktivasiAkunViewModel
                    {
                        biodata_id = idBiodata,
                        abuid = o.x_addrbook.abuid,
                        abpwd = o.x_addrbook.abpwd,
                        email = o.x_addrbook.email,
                        is_delete_akun = o.x_addrbook.is_deleted
                    }).FirstOrDefault();

                    return result;
                }
            }
        }

        public static List<AktivasiAkunViewModel> SelectAllTest(long idBiodata)
        {
            List<AktivasiAkunViewModel> result = new List<AktivasiAkunViewModel>();
            using (var db = new ngxsisContext())
            {
                result = db.x_online_test
                    .Where(o => o.biodata_id == idBiodata && o.is_delete == false)
                    .Select(o => new AktivasiAkunViewModel
                    {
                        biodata_id = o.biodata_id,
                        online_test_id = o.id,
                        period_code = o.period_code,
                        period = o.period,
                        test_date = o.test_date,
                        expired_test = o.expired_test,
                        user_access = o.user_access,
                        status = o.status                                                
                    }).ToList();
            }

            return result != null ? result : new List<AktivasiAkunViewModel>();
        }        

        public static List<AktivasiAkunViewModel> SelectAllTestDetail(long id)
        {
            List<AktivasiAkunViewModel> result = new List<AktivasiAkunViewModel>();
            using (var db = new ngxsisContext())
            {
                result = db.x_online_test_detail
                    .OrderBy(o => o.test_order)
                    .Where(o => o.online_test_id == id && o.is_delete == false)
                    .Select(o => new AktivasiAkunViewModel
                    {
                        biodata_id = o.x_online_test.biodata_id,
                        online_test_id = id,
                        online_test_detail_id = o.id,
                        name_type = o.x_test_type.name,
                        test_order = o.test_order
                    }).ToList();
            }

            return result != null ? result : new List<AktivasiAkunViewModel>();
        }

        public static List<AktivasiAkunViewModel> SelectTypeTestAll()
        {
            List<AktivasiAkunViewModel> result = new List<AktivasiAkunViewModel>();
            using (var db = new ngxsisContext())
            {
                result = db.x_test_type
                    .Select(o => new AktivasiAkunViewModel
                    {
                        test_type_id = o.id,
                        name_type = o.name
                    }).ToList();
            }

            return result != null ? result : new List<AktivasiAkunViewModel>();
        }


        public static ResponseResult CreateTesDetail(AktivasiAkunViewModel entity, long session)
        {
            ResponseResult result = new ResponseResult();

            AktivasiAkunViewModel model = new AktivasiAkunViewModel();

            using (var db = new ngxsisContext())
            {
                try
                {
                    int urutan = 0;

                    model = (from c in db.x_online_test_detail
                             orderby c.test_order descending
                             where c.online_test_id == entity.online_test_id
                             select new AktivasiAkunViewModel
                             {
                                 test_order = c.test_order
                             }).FirstOrDefault();

                    if (model == null)
                        urutan = 1;
                    else
                        urutan = (int)model.test_order + 1;


                    x_online_test_detail testDetail = new x_online_test_detail();
                    testDetail.created_by = session;
                    testDetail.create_on = DateTime.Now;
                    testDetail.is_delete = false;
                    testDetail.test_order = urutan;
                    testDetail.online_test_id = entity.online_test_id;
                    testDetail.test_type_id = entity.test_type_id;

                    db.x_online_test_detail.Add(testDetail);
                    db.SaveChanges();
                    result.Entity = entity;
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Message = ex.Message;
                }                
            }

            return result;
        }

        public static ResponseResult DeleteTesDetail(AktivasiAkunViewModel entity, long session)
        {
            ResponseResult result = new ResponseResult();
            using (var db = new ngxsisContext())
            {
                try
                {
                    x_online_test_detail tesDetail = db.x_online_test_detail
                        .Where(r => r.id == entity.online_test_detail_id)
                        .FirstOrDefault();
                    tesDetail.is_delete = true;
                    tesDetail.delete_by = session;
                    tesDetail.delete_on = DateTime.Now;
                    db.SaveChanges();
                    result.Entity = entity;
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Message = ex.Message;
                }
            }
            return result;
        }




        public static string generateAkun()
        {
            Random strAkun = new Random();
            string newStr = String.Empty;
            string tampung = String.Empty;
            for (int i = 0; i < 6; i++)
            {
                if (i % 2 == 0)
                    tampung = Convert.ToChar(strAkun.Next(65, 90)).ToString();
                else
                    tampung = strAkun.Next(0, 9).ToString();
                newStr += tampung;
            }
            using (var db = new ngxsisContext())
            {
                x_addrbook bio = db.x_addrbook.Where(b => b.abuid == newStr).FirstOrDefault();
                if (bio != null)
                {
                    newStr = generateAkun();
                }
            }
            return newStr;
        }
    }
}
