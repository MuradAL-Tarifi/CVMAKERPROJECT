using MYCVMAKERPROJECT.Models;
using MYCVMAKERPROJECT.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MYCVMAKER.Controllers
{
    public class CompanyCVController : Controller
    {
        private CVMAKER_DBEntities db = new CVMAKER_DBEntities();
        public ActionResult CompanyCV()
        {

            CompanyViewModel CVM = new CompanyViewModel();

            //var id = (int)System.Web.HttpContext.Current.Session["CompanyID"];
            var id = 1;
            CVM.Company = db.Companies.Where(x => x.UsersId == id).ToList().FirstOrDefault();
            CVM.User = db.Users.Where(x => x.Id == id).ToList().FirstOrDefault();
            CVM.CompanWorkExperience = db.CompanWorkExperiences.Where(x => x.CompanyId == CVM.Company.Id).ToList();
            CVM.CompanyProject = db.CompanyProjects.Where(x => x.CompanyId == CVM.Company.Id).ToList();
            CVM.CompanyServices = db.CompanyServices.Where(x => x.CompanyId == CVM.Company.Id).ToList();
            CVM.CompanySkills = db.CompanySkills.Where(x => x.CompanyId == CVM.Company.Id).ToList();
            CVM.Nofitication = db.Nofitications.Where(x => x.CompanyId == CVM.Company.Id).ToList();

            return View(CVM);
        }
        public ActionResult CompanyForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CompanyForgotPassword(string email)
        {
            if (IsEmailExist(email))
            {
                var userInfo = db.Users.Where(x => x.UserEmail == email).ToList().FirstOrDefault();
                SendPasswrodLinkEmail(userInfo.UserEmail);
                var message = "Send new paswword successfully done. has been sent to your email " + userInfo.UserEmail;
                ViewBag.Message = message;
                return View("Login");
            }
            else
            {
                var message = "Your email not exist";
                ViewBag.Message = message;
                return View();
            }

        }

        [HttpPost]
        public ActionResult JobAlertSaveData(List<JobAlert> Jobs)
        {
            int userId = (int)System.Web.HttpContext.Current.Session["CompanyID"];
            using (CVMAKER_DBEntities db = new CVMAKER_DBEntities())
            {
                //Check for NULL.
                if (Jobs == null)
                {
                    Jobs = new List<JobAlert>();
                }

                //Loop and insert records.
                foreach (JobAlert Job in Jobs)
                {
                    Job.CompanyId = userId;
                    db.JobAlerts.Add(Job);
                }
                db.SaveChanges();

                return View("CompanyCV");
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
            var verifyUrl = "/HomePage/ForgotPassword/";
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("muradshaltaf123@gmail.com", "Murad Awad");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "*******";//we set here real passwrod for the email
            var userInfo = db.Users.AsNoTracking().Where(x => x.UserEmail == email).ToList().FirstOrDefault();
            userInfo.UserPassword = CreateRandomPassword(9);
            db.Entry(userInfo).State = EntityState.Modified;
            string subject = "We send you new password!" + userInfo.UserEmail;

            string body = "<br/><br/> We are excited to tell you that your password chanded successfully.<br/><br/>" + "Your New Password : " + userInfo.UserPassword;

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

