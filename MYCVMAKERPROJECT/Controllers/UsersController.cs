﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
        [HttpPost]
        public ActionResult GoToLogin()
        {
            return RedirectToAction("Login");
        }
        
        public ActionResult GoToComapnyReg()
        {
            Session["UserState"] = "2";
            return RedirectToAction("CompanyReg");
        }
        public ActionResult GoToPersonalReg()
        {
            Session["UserState"] = "3";

            return RedirectToAction("PersonalReg");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email,string password,string RetrunUrl="")
        {
                var userInfo = db.Users.Where(x => x.UserEmail == email && x.UserPassword == password).ToList().FirstOrDefault();
                if (userInfo == null)
                {
                    ModelState.AddModelError("Wrong", "User Name or Password wrong");
                    return View();
                }
                else if (userInfo.UserState == 3)
                {

                    int timeout = userInfo.RememberMe ? 525600 : 20;
                    var ticket = new FormsAuthenticationTicket(userInfo.UserEmail,userInfo.RememberMe,timeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName,encrypted);
                    cookie.Expires=DateTime.Now.AddMinutes(timeout);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);

                    if (Url.IsLocalUrl(RetrunUrl))
                    {
                        return Redirect(RetrunUrl);
                    }
                    else
                    {
                        return RedirectToAction("PersonalCV", "PersonalCV");
                    }
         
                }
                else if (userInfo.UserState == 2)
                {
                    int timeout = userInfo.RememberMe ? 525600 : 20;
                    var ticket = new FormsAuthenticationTicket(userInfo.UserEmail, userInfo.RememberMe, timeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    cookie.Expires = DateTime.Now.AddMinutes(timeout);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);

                    if (Url.IsLocalUrl(RetrunUrl))
                    {
                        return Redirect(RetrunUrl);
                    }
                    else
                    {
                        return RedirectToAction("CompanyCV", "CompanyCV");
                    }

                    
                }
                else
                {
                    return RedirectToAction("Admin", "Admin");
                }
                
            
        }
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Users", "Login");
        }
        public ActionResult PersonalReg()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PersonalReg(string name, string email, string password, string confirmpassword)
        {
            if (IsEmailExist(email))
            {
                ViewBag.mess = "This email is exist";
                return View();
            }
            else {
                if (password==confirmpassword)
                {
                    
                    User user = new User();
                    user.UserName = name;
                    user.UserEmail = email;
                    user.UserPassword = password;
                    user.ConfirmPassword = confirmpassword;
                    user.UserState = 3;
                    db.Users.Add(user);
                    db.SaveChanges();
                    Session["PersoanlID"] = user.Id;
                    return RedirectToAction("PersonalDet");
                }
                else
                {
                    ViewBag.mess = "Password and confirmpassword not match";
                    return View();
                }


            }
        }
        public ActionResult PersonalDet()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PersonalDet(string FirstName, string LastName, string JobTitle, string gender, string PhoneNumber, string Address, string Description, string GitHupLink, string FaceBookLink, string LinkedinLink, string InstagramLink, HttpPostedFileBase CvFile, HttpPostedFileBase mediaFile)
        {


            try
            {
                var userstate = Session["UserState"];
                int userId = (int)System.Web.HttpContext.Current.Session["PersoanlID"];
                Personal personal = new Personal();

                var userInfo = db.Users.AsNoTracking().Where(x => x.Id == userId).ToList().FirstOrDefault();
                if (userstate.Equals("3"))
                {

                    userInfo.PhoneNumber = int.Parse(PhoneNumber);
                    userInfo.Address = Address;
                    userInfo.Description = Description;
                    userInfo.GetHupLink = GitHupLink;
                    userInfo.LinkedInLink = LinkedinLink;
                    userInfo.FacebookLink = FaceBookLink;
                    userInfo.InstagramLink = InstagramLink;

                    personal.P_FirstName = FirstName;
                    personal.P_LastName = LastName;
                    personal.P_JobTitle = JobTitle;
                    personal.P_Gender = gender;
                    personal.UsersId = userInfo.Id;

                    string cvpath = "";
                    if (CvFile != null)
                    {
                        cvpath = "/CVFile/" + CvFile.FileName;
                        CvFile.SaveAs(Server.MapPath(cvpath));//save image to folder
                    }
                    personal.P_FileCV = cvpath;

                    string Imgpath = "";
                    if (mediaFile != null)
                    {
                        Imgpath = "/img/" + mediaFile.FileName;
                        mediaFile.SaveAs(Server.MapPath(Imgpath));//save image to folder
                    }
                    personal.P_Image = Imgpath;
                    personal.User = userInfo;
                    db.Entry(userInfo).State = EntityState.Modified;
                    db.Personals.Add(personal);
                    db.SaveChanges();
                    return RedirectToAction("PersonalWorkExperience", "PersonalWorkExperience");
                }
                else
                    {
                        return RedirectToAction("HomePage");
                    }
                
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

        }
        public ActionResult CompanyReg()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CompanyReg(string UserName, string Email, string Password, string ConfirmPassword)
        {
            var userstate = Session["UserState"];
            if (userstate.Equals("2"))
            {
                if (Password == ConfirmPassword)
                {
                    User user = new User();
                    user.UserName = UserName;
                    user.UserEmail = Email;
                    user.UserPassword = Password;
                    user.ConfirmPassword = ConfirmPassword;
                    db.Users.Add(user);
                    db.SaveChanges();
                    Session["ComapnyID"] = user.Id;
                    return RedirectToAction("CompanyDet");
                }
                else
                {
                    ViewBag.mess = "Password and confirmpassword not match";
                    return View();
                }
            }
            else
            {
                return RedirectToAction("HomePage");
            }
        }
        public ActionResult CompanyDet()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CompanyDet( string CoumpanyName, string JobTitle, int PhoneNumber, string CompanyLocation, string CompanyType, string Description, string GitHupLink, string FaceBookLink, string LinkedinLink, string InstagramLink, HttpPostedFileBase mediaFile)
        {
            var userstate = Session["UserState"];
            var userId = (int)System.Web.HttpContext.Current.Session["ComapnyID"];
            Company company = new Company();

            var userInfo = db.Users.Where(x => x.Id == userId).ToList().FirstOrDefault();
            if (userstate.Equals("2"))
            {
                userInfo.PhoneNumber = PhoneNumber;
                userInfo.Address = CompanyLocation;
                userInfo.Description = Description;
                userInfo.GetHupLink = GitHupLink;
                userInfo.LinkedInLink = LinkedinLink;
                userInfo.FacebookLink = FaceBookLink;
                userInfo.InstagramLink = InstagramLink;

                company.C_Name = CoumpanyName;
                company.C_JobTitle = JobTitle;
                company.C_Type = CompanyType;
                company.UsersId = userInfo.Id;
                string Imgpath = "";
                if (mediaFile != null)
                {
                    Imgpath = "~/img/" + mediaFile.FileName;
                    mediaFile.SaveAs(Server.MapPath(Imgpath));//save image to folder
                }
                company.C_logo = Imgpath;
                db.Entry(userInfo).State = EntityState.Modified;
                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("CompanyWorkExperience", "CompanyWorkExperience"); ;
            }
            else
            {
                return RedirectToAction("HomePage");
            }
        }
       [HttpGet]
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

            var fromEmail = new MailAddress("muradshaltaf123@gmail.com","Murad Awad");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "*******";//we set here real passwrod for the email
            var userInfo = db.Users.AsNoTracking().Where(x => x.UserEmail == email).ToList().FirstOrDefault();
            userInfo.UserPassword = CreateRandomPassword(9);
            db.Entry(userInfo).State = EntityState.Modified;
            string subject = "We send you new password!"+ userInfo.UserEmail;

            string body = "<br/><br/> We are excited to tell you that your password chanded successfully.<br/><br/>" +"Your New Password : "+userInfo.UserPassword;

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
        [HttpPost]
        public ActionResult contact(string Name, string mail, string Subject, string Message)
        {

                try
                {
                    MailMessage msz = new MailMessage();
                    msz.From = new MailAddress(mail);//Email which you are getting 
                                                         //from contact us page 
                    msz.To.Add("emailaddrss@gmail.com");//Where mail will be sent 
                    msz.Subject = Subject;
                    msz.Body = Name + "  "+Message;
                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";

                    smtp.Port = 587;

                    smtp.Credentials = new System.Net.NetworkCredential
                    ("muradshaltaf123@gmail.com", "password");

                    smtp.EnableSsl = true;

                    smtp.Send(msz);

                    ModelState.Clear();
                    ViewBag.Message = "Thank you for Contacting us ";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Sorry we are facing Problem here {ex.Message}";
                }

            return View();
        }

    }

}
