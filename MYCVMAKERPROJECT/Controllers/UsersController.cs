using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MYCVMAKER.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult HomePage()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult PersonalReg()
        {
            return View();
        }
        public ActionResult PersonalDet()
        {
            return View();
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
    }
}
