using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MYCVMAKER.Controllers
{
    public class PersonalWorkExperienceController : Controller
    {
        public ActionResult PersonalWorkExperience()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PersonalWorkExperience(FormCollection fc)
        {
            string se = fc["JobTitle"];
            string des = fc["Description"];
            string skill = fc["skill"];
            string leval = fc["level"];


            return View();
        }
    }
}
