using ngxsis.Repository;
using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ngxsis.MVC.Controllers
{
    public class TokenController : Controller
    {
        // GET: Email
        public ActionResult KirimToken()
        {
            return PartialView("_KirimToken");
        }

        [HttpPost]
        public ActionResult Kirim(DateTime date, long bioId)
        {
            ResponseResult result = new ResponseResult();
            ResponseResult cek= TokenRepo.isComplete(bioId);
            if(!cek.Success)
            {
                result.Message=cek.Message;
                result.Success=cek.Success;
            }
             else if(ModelState.IsValid)
            {
                string dateStr = date.ToString("dd MMMM yyyy");
                string token = TokenRepo.generateToken();
                long userId = (long)Session["UserID"];
                BiodataViewModel entity=TokenRepo.ById(bioId);

                var body = "<tr>"+
                                "<td>Nama</td>"+
                                "<td> : </td>"+
                                "<td>"+entity.fullname+"</td>"+
                           "<tr>"+
                                "<td>Token</td>"+
                                "<td> : </td>"+
                                "<td>"+token+"</td>"+
                           "</tr>"+
                           "<tr>"+
                                "<td>Expired Date</td>"+
                                "<td> : </td>"+
                                "<td>"+dateStr+"</td>"+
                           "</tr>";
                
                try
                {
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(entity.email));
                    message.Subject="Token XSIS 2.0";
                    message.Body=string.Format(body);
                    message.IsBodyHtml=true;
                    using(var smtp = new SmtpClient())
                    {
                        smtp.Send(message);
                    }

                    result=TokenRepo.saveToken(bioId,token,date,userId);

                }catch
                {
                    result.Success=false;
                    result.Message="Token tidak terkirim!";
                }
            }
            return Json(new
            {
                message = result.Message,
                success = result.Success
            },JsonRequestBehavior.AllowGet);
        }
    }
}