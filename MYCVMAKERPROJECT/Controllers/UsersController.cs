using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using MYCVMAKERPROJECT.Models;

namespace MYCVMAKER.Controllers
{
    public class UsersController : Controller
    {
        private CVMAKER_DBEntities db = new CVMAKER_DBEntities();
        public ActionResult HomePage()
        {
            return View();
        }
        public ActionResult GoToComapnyReg()
        {
            Session["UserState"] = 2;
            return RedirectToAction("Login");
        }
        public ActionResult GoToPersonalReg()
        {
            Session["UserState"] = 3;
            return RedirectToAction("Login");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email,string password)
        {
            var userstate = Session["UserState"];
            
            if (userstate!=null)
            {
                var userInfo = db.Users.Where(x => x.UserEmail == email && x.UserPassword == password).ToList().FirstOrDefault();
                var isExist = IsEmailExist(userInfo.UserEmail);
                if (userInfo == null)
                {
                    ModelState.AddModelError("Wrong", "User Name or Password wrong");
                    return View();
                }
                else if(isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View();
                }
                else if (userInfo.UserState == 3)
                {
                    return RedirectToAction("PersonalCV", "PersonalCV");
                }
                else if (userInfo.UserState == 2)
                {
                    return RedirectToAction("CompanyCV", "CompanyCV");
                }
                else
                {
                    return RedirectToAction("Admin", "Admin"); ;
                }
                
            }
            else
            {
                return RedirectToAction("HomePage");
            }
        }

        public ActionResult PersonalReg()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PersonalReg(string UserName,string Email,string Password ,string ConfirmPassword )
        {
            var userstate = Session["UserState"];
            if (userstate.Equals("3"))
             {
                User user = new User();
                user.UserName = UserName;
                user.UserEmail = Email;
                user.UserPassword = Password;
                user.ConfirmPassword = ConfirmPassword;
                db.Users.Add(user);
                db.SaveChanges();
                Session["Email"] = user.Id;
                return RedirectToAction("PersonalDet");
            }
            else
            {
                return RedirectToAction("HomePage");
            }

        }
        public ActionResult PersonalDet()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PersonalDet(string FirstName, string LastName, string JobTitle, int PhoneNumber, string Address, string Description, string GitHupLink, string FaceBookLink, string LinkedinLink, string InstagramLink, HttpPostedFileBase CvFile, HttpPostedFileBase mediaFile)
        {
            var userstate = Session["UserState"];
            var userId = Session["Email"];
            Personal personal = new Personal();

            var userInfo = db.Users.Where(x => x.Id == int.Parse((string)userId)).ToList().FirstOrDefault();
            if (userstate.Equals("3"))
            {

                userInfo.PhoneNumber = PhoneNumber;
                userInfo.Address = Address;
                userInfo.Description = Description;
                userInfo.GetHupLink = GitHupLink;
                userInfo.LinkedInLink = LinkedinLink;
                userInfo.FacebookLink = FaceBookLink;
                userInfo.InstagramLink = InstagramLink;

                personal.P_FirstName = FirstName;
                personal.P_LastName = LastName;
                personal.P_JobTitle = JobTitle;
                personal.UsersId = userInfo.Id;
                string cvpath = "";
                if (CvFile != null)
                {
                    cvpath = "~/CVFile/" + CvFile.FileName;
                    CvFile.SaveAs(Server.MapPath(cvpath));//save image to folder
                }
                personal.P_FileCV = cvpath;

                string Imgpath = "";
                if (mediaFile != null)
                {
                    Imgpath = "~/CVFile/" + mediaFile.FileName;
                    mediaFile.SaveAs(Server.MapPath(Imgpath));//save image to folder
                }
                personal.P_FileCV = Imgpath;
                db.Entry(User).State = EntityState.Modified;
                db.Personals.Add(personal);
                db.SaveChanges();
                return RedirectToAction("PersonalDet");
            }
            else
            {
                return RedirectToAction("HomePage");
            }
        }
        public ActionResult CompanyReg()
        {
            return View();
        }
        public ActionResult CompanyDet()
        {
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            if (IsEmailExist(email))
            {
                var userInfo = db.Users.Where(x => x.UserEmail == email).ToList().FirstOrDefault();
                SendPasswrodLinkEmail(userInfo.UserEmail, userInfo.activationCode);
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
        public void SendPasswrodLinkEmail(string email,string activationCode)
        {
            var verifyUrl = "/HomePage/ForgotPassword/"+activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("muradshaltaf123@gmail.com","Murad Awad");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "********************";//we set here real passwrod 
            var userInfo = db.Users.Where(x => x.UserEmail == email).ToList().FirstOrDefault();
            userInfo.UserPassword = CreateRandomPassword(9);
            userInfo.ConfirmPassword = userInfo.UserPassword;
            string subject = "We send you new password!"+ userInfo.UserPassword;

            string body = "<br/><br/> We are excited to tell you that your password chanded successfully.";

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
