using MYCVMAKERPROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MYCVMAKER.Controllers
{
    public class CompanWorkExperienceController : Controller
    {
        public ActionResult CompanyWorkExperience()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ExperiencesSaveData(List<CompanWorkExperience> Experiences)
        {
            int userId = (int)System.Web.HttpContext.Current.Session["CompanyID"];
            using (CVMAKER_DBEntities db = new CVMAKER_DBEntities())
            {
                //Check for NULL.
                if (Experiences == null)
                {
                    Experiences = new List<CompanWorkExperience>();
                }

                //Loop and insert records.
                foreach (CompanWorkExperience Experience in Experiences)
                {
                    Experience.CompanyId = userId;
                    db.CompanWorkExperiences.Add(Experience);
                }
                int insertedRecords = db.SaveChanges();

                return Json(insertedRecords);
            }

        }
        [HttpPost]
        public JsonResult ServicesSaveData(List<CompanyService> Services)
        {
            int userId = (int)System.Web.HttpContext.Current.Session["CompanyID"];
            using (CVMAKER_DBEntities db = new CVMAKER_DBEntities())
            {
                //Check for NULL.
                if (Services == null)
                {
                    Services = new List<CompanyService>();
                }

                //Loop and insert records.
                foreach (CompanyService Service in Services)
                {
                    Service.CompanyId = userId;
                    db.CompanyServices.Add(Service);
                }
                int insertedRecords = db.SaveChanges();

                return Json(insertedRecords);
            }

        }
        [HttpPost]
        public ActionResult SkillsSaveData(List<CompanySkill> skills)
        {
            try
            {
                int userId = (int)System.Web.HttpContext.Current.Session["CompanyID"];
                using (CVMAKER_DBEntities db = new CVMAKER_DBEntities())
                {
                    //Check for NULL.
                    if (skills == null)
                    {
                        skills = new List<CompanySkill>();
                    }

                    //Loop and insert records.
                    foreach (CompanySkill skill in skills)
                    {
                        skill.CompanyId = userId;
                        db.CompanySkills.Add(skill);
                    }
                    int insertedRecords = db.SaveChanges();

                    return RedirectToAction("CompanyProject", "CompanyProject");
                }
            }
            catch (Exception)
            {

                return Json(false);
            }


        }
    }
}
