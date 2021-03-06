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
        private CVMAKER_DBEntities4 db = new CVMAKER_DBEntities4();
        public ActionResult CompanyCV()
        {
            CompanyViewModel CVM = new CompanyViewModel();

            var id = (int)System.Web.HttpContext.Current.Session["CompanyID"];
            //var id = 1;
            CVM.Company = db.Companies.Where(x => x.UsersId == id).ToList().FirstOrDefault();
            CVM.User = db.Users.Where(x => x.Id == id).ToList().FirstOrDefault();
            CVM.CompanWorkExperience = db.CompanWorkExperiences.Where(x => x.CompanyId == CVM.Company.Id).ToList();
            CVM.CompanyProject = db.CompanyProjects.Where(x => x.CompanyId == CVM.Company.Id).ToList();
            CVM.CompanyServices = db.CompanyServices.Where(x => x.CompanyId == CVM.Company.Id).ToList();
            CVM.CompanySkills = db.CompanySkills.Where(x => x.CompanyId == CVM.Company.Id).ToList();
            CVM.Nofitication = db.Nofitications.Where(x => x.CompanyId == CVM.Company.Id && x.Submitted==true).ToList();

            return View(CVM);
        }
        public ActionResult CompanyCVView()
        {
            CompanyViewModel CVM = new CompanyViewModel();

            var id = (int)System.Web.HttpContext.Current.Session["CompanyID"];
            CVM.Company = db.Companies.Where(x => x.UsersId == id).ToList().FirstOrDefault();
            CVM.User = db.Users.Where(x => x.Id == id).ToList().FirstOrDefault();
            CVM.CompanWorkExperience = db.CompanWorkExperiences.Where(x => x.CompanyId == CVM.Company.Id).ToList();
            CVM.CompanyProject = db.CompanyProjects.Where(x => x.CompanyId == CVM.Company.Id).ToList();
            CVM.CompanyServices = db.CompanyServices.Where(x => x.CompanyId == CVM.Company.Id).ToList();
            CVM.CompanySkills = db.CompanySkills.Where(x => x.CompanyId == CVM.Company.Id).ToList();
            CVM.Nofitication = db.Nofitications.Where(x => x.CompanyId == CVM.Company.Id && x.Submitted == true).ToList();

            return View(CVM);
        }

        public ActionResult CompanyForgotPassword()
        {
            CompanyViewModel CVM = new CompanyViewModel();

            var id = (int)System.Web.HttpContext.Current.Session["CompanyID"];
            //var id = 1;
            CVM.Company = db.Companies.Where(x => x.UsersId == id).ToList().FirstOrDefault();
            CVM.User = db.Users.Where(x => x.Id == id).ToList().FirstOrDefault();
            CVM.Nofitication = db.Nofitications.Where(x => x.CompanyId == CVM.Company.Id && x.Submitted == true).ToList();

            return View(CVM);
        }
        [HttpPost]
        public ActionResult CompanyForgotPassword(string email)
        {
            if (IsEmailExist(email))
            {
                var userInfo = db.Users.AsNoTracking().Where(x => x.UserEmail == email).ToList().FirstOrDefault();
                SendPasswrodLinkEmail(userInfo.UserEmail);
                var message = "Send new paswword successfully done. has been sent to your email " + userInfo.UserEmail;
                ViewBag.Message = message;
                return View("CompanyCV");
            }
            else
            {
                var message = "Your email not exist";
                ViewBag.Message = message;
                return View();
            }

        }

        [HttpPost]
        [Route("CompanyCV/JobAlertSaveData")]
        public void JobAlertSaveData(List<JobAlert> Jobs)
        {
           int userId = (int)System.Web.HttpContext.Current.Session["CompanyID"];
            using (CVMAKER_DBEntities4 db = new CVMAKER_DBEntities4())
            {
                var cid=db.Companies.Where(x => x.UsersId == userId).ToList().FirstOrDefault();
                //Check for NULL.
                if (Jobs == null)
                {
                    Jobs = new List<JobAlert>();
                }

                //Loop and insert records.
                foreach (JobAlert Job in Jobs)
                {
                    
                    Job.CompanyId = cid.Id;
                    var Personal = db.Personals.Where(x => x.P_JobTitle.Equals(Job.J_Title)).ToList();
                    for (int i = 0; i < Personal.Count; i++)
                    {
                        Nofitication not = new Nofitication();
                        not.JobAlertId = Job.Id;
                        not.CompanyId = Job.CompanyId;
                        not.PersonalId = Personal[i].Id;
                        not.IsReaded = false;
                        not.Submitted = false;
                        db.Nofitications.Add(not);
                    }

                    db.JobAlerts.Add(Job);
                }
                db.SaveChanges();

                
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

