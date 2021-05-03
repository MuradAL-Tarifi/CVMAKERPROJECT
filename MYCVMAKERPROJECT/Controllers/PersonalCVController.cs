using MYCVMAKERPROJECT.Models;
using MYCVMAKERPROJECT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
