using MYCVMAKERPROJECT.Models;
using MYCVMAKERPROJECT.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MYCVMAKER.Controllers
{
    public class PersonalCVController : Controller
    {
        private CVMAKER_DBEntities4 db = new CVMAKER_DBEntities4();
        //[Authorize]
        public ActionResult PersonalCV()
        {
            PersonalViewModel PVM = new PersonalViewModel();

            var id = Session["PersoanlID"];
            PVM.Personal = db.Personals.Where(x => x.UsersId == (int)id).ToList().FirstOrDefault();
            PVM.User = db.Users.Where(x => x.Id == (int)id).ToList().FirstOrDefault();
            PVM.PersonalWorkExperience = db.PersonalWorkExperiences.Where(x => x.PersonalId == PVM.Personal.Id).ToList();
            PVM.PersonalProject = db.PersonalProjects.Where(x => x.PersonalId == PVM.Personal.Id).ToList();
            PVM.PersonalService = db.PersonalServices.Where(x => x.PersonalId == PVM.Personal.Id).ToList();
            PVM.PersonalSkill = db.PersonalSkills.Where(x => x.PersonalId == PVM.Personal.Id).ToList();
            PVM.Education = db.Educations.Where(x => x.PersonalId == PVM.Personal.Id).ToList();
            PVM.Nofitication = db.Nofitications.Where(x => x.PersonalId == PVM.Personal.Id).ToList();

            return View(PVM);
        }
        
        public ActionResult PersonalView(int Id)
        {
            PersonalViewModel PVM = new PersonalViewModel();

            //var id = (int)System.Web.HttpContext.Current.Session["PersonalID"];
            //var id 28;
            PVM.Personal = db.Personals.Where(x => x.Id == Id).ToList().FirstOrDefault();
            PVM.User = db.Users.Where(x => x.Id == Id).ToList().FirstOrDefault();
            PVM.PersonalWorkExperience = db.PersonalWorkExperiences.Where(x => x.PersonalId == PVM.Personal.Id).ToList();
            PVM.PersonalProject = db.PersonalProjects.Where(x => x.PersonalId == PVM.Personal.Id).ToList();
            PVM.PersonalService = db.PersonalServices.Where(x => x.PersonalId == PVM.Personal.Id).ToList();
            PVM.PersonalSkill = db.PersonalSkills.Where(x => x.PersonalId == PVM.Personal.Id).ToList();
            PVM.Education = db.Educations.Where(x => x.PersonalId == PVM.Personal.Id).ToList();
            return View(PVM);
        }
        [HttpPost]
        public ActionResult PersonalCV(string search)
        {

            var id = db.Companies.Where(x => x.C_Name.Equals(search)).FirstOrDefault();
            Session["CompanyID"] = id.Id;

            return RedirectToAction("CompanyCVView", "CompanyCV");
        }
        public JsonResult GetJobtById(int Id)
        {
            JobAlert model = db.JobAlerts.Where(x => x.Id == Id).SingleOrDefault();
            Session["id"] = Id;
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult submited()
        {
            var id = Session["PersoanlID"];
           
            int jobid = (int)System.Web.HttpContext.Current.Session["id"];
            var idp = db.Personals.AsNoTracking().Where(x => x.UsersId ==(int)id).ToList().FirstOrDefault();
            
            Nofitication model = db.Nofitications.AsNoTracking().Where(x => x.PersonalId == idp.Id && x.JobAlertId== jobid).FirstOrDefault();

            model.Submitted = true;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return Json(true);
        }

        [HttpPost]
        public ActionResult AutoComplete(string Prefix)
        {

            var Companies = (from Companie in db.Companies
                             where Companie.C_Name.StartsWith(Prefix)
                             select new
                             {
                                 label = Companie.C_Name,
                                 val = Companie.Id
                             }).ToList();

            return Json(Companies, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PersonalForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PersonalForgotPassword(string email)
        {

            if (IsEmailExist(email))
            {
                var userInfo = db.Users.Where(x => x.UserEmail == email).ToList().FirstOrDefault();
                SendPasswrodLinkEmail(userInfo.UserEmail);
                var message = "Send new paswword successfully done. has been sent to your email " + userInfo.UserEmail;
                ViewBag.Message = message;
                return View("PersonalCV");
            }
            else
            {
                var message = "Your email not exist";
                ViewBag.Message = message;
                return View();
            }

        }


        [NonAction]
        public bool IsEmailExist(string email)
        {
            var v = db.Users.Where(a => a.UserEmail == email).FirstOrDefault();
            return v != null;
        }

        [NonAction]
        public void SendPasswrodLinkEmail(string email)
        {
            var verifyUrl = "/Users/ForgotPassword/";
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var fromEmail = new MailAddress("cvmk90@gmail.com", "CV Maker");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "cvmaker@2152021";//we set here real passwrod for the email
            var userPass = db.Users.Where(x => x.UserEmail == email).ToList().FirstOrDefault();
            userPass.UserPassword = CreateRandomPassword(9);
            db.Entry(userPass).State = EntityState.Modified;
            db.SaveChanges();
            string subject = "We send you new password!" + userPass.UserEmail;

            string body = "<br/><br/> We are excited to tell you that your password chanded successfully.<br/><br/>" + "Your New Password : " + userPass.UserPassword;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })


                smtp.Send(message);
        }
        [NonAction]
        public string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
    }
}
