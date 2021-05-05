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
    public class PersonalCVController : Controller
    {
        private CVMAKER_DBEntities db = new CVMAKER_DBEntities();
        [Authorize]
        public ActionResult PersonalCV()
        {
            PersonalViewModel PVM = new PersonalViewModel();

            //var id = (int)System.Web.HttpContext.Current.Session["PersonalID"];
            var id = 1;
            PVM.Personal = db.Personals.Where(x => x.UsersId == id).ToList().FirstOrDefault();
            PVM.User = db.Users.Where(x => x.Id == id).ToList().FirstOrDefault();
            PVM.PersonalWorkExperience = db.PersonalWorkExperiences.Where(x => x.PersonalId == PVM.Personal.Id).ToList();
            PVM.PersonalProject = db.PersonalProjects.Where(x => x.PersonalId == PVM.Personal.Id).ToList();
            PVM.PersonalService = db.PersonalServices.Where(x => x.PersonalId == PVM.Personal.Id).ToList();
            PVM.PersonalSkill = db.PersonalSkills.Where(x => x.PersonalId == PVM.Personal.Id).ToList();
            PVM.Education = db.Educations.Where(x => x.PersonalId == PVM.Personal.Id).ToList();
            PVM.Nofitication = db.Nofitications.Where(x => x.PersonalId == PVM.Personal.Id).ToList();

            return View(PVM);
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
                return View("Login");
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
