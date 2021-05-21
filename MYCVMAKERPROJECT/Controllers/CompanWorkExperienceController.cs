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
        private CVMAKER_DBEntities4 db = new CVMAKER_DBEntities4();
        public ActionResult CompanyWorkExperience()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ExperiencesSaveData(List<CompanWorkExperience> Experiences)
        {
            int userId = (int)System.Web.HttpContext.Current.Session["ComapnyID"];
            var userInfo = db.Companies.AsNoTracking().Where(x => x.UsersId == (int)userId).ToList().FirstOrDefault();
            using (CVMAKER_DBEntities4 db = new CVMAKER_DBEntities4())
            {
                
                //Check for NULL.
                if (Experiences == null)
                {
                    Experiences = new List<CompanWorkExperience>();
                }

                //Loop and insert records.
                foreach (CompanWorkExperience Experience in Experiences)
                {
                    Experience.CompanyId = userInfo.Id;
                    db.CompanWorkExperiences.Add(Experience);
                }
                int insertedRecords = db.SaveChanges();

                return Json(insertedRecords);
            }

        }
        [HttpPost]
        public JsonResult ServicesSaveData(List<CompanyService> Services)
        {
            int userId = (int)System.Web.HttpContext.Current.Session["ComapnyID"];
            using (CVMAKER_DBEntities4 db = new CVMAKER_DBEntities4())
            {
                var userInfo = db.Companies.AsNoTracking().Where(x => x.UsersId == (int)userId).ToList().FirstOrDefault();
                //Check for NULL.
                if (Services == null)
                {
                    Services = new List<CompanyService>();
                }

                //Loop and insert records.
                foreach (CompanyService Service in Services)
                {
                    Service.CompanyId = userInfo.Id;
                    db.CompanyServices.Add(Service);
                }
                int insertedRecords = db.SaveChanges();

                return Json(insertedRecords);
            }

        }
        [HttpPost]
        public JsonResult SkillsSaveData(List<CompanySkill> skills)
        {

                int userId = (int)System.Web.HttpContext.Current.Session["ComapnyID"];
                using (CVMAKER_DBEntities4 db = new CVMAKER_DBEntities4())
                {
                    var userInfo = db.Companies.AsNoTracking().Where(x => x.UsersId == (int)userId).ToList().FirstOrDefault();
                    //Check for NULL.
                    if (skills == null)
                    {
                        skills = new List<CompanySkill>();
                    }

                    //Loop and insert records.
                    foreach (CompanySkill skill in skills)
                    {
                        skill.CompanyId = userInfo.Id;
                        db.CompanySkills.Add(skill);
                    }
                    int insertedRecords = db.SaveChanges();

                return Json(insertedRecords);
                   }
            }



        }
    }

