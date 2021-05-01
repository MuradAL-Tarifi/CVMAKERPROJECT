using MYCVMAKERPROJECT.Models;
using MYCVMAKERPROJECT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
