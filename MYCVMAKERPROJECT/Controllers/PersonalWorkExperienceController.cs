using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MYCVMAKERPROJECT.Models;

namespace MYCVMAKER.Controllers
{
    public class PersonalWorkExperienceController : Controller
    {

        private CVMAKER_DBEntities4 db = new CVMAKER_DBEntities4();
        public ActionResult PersonalWorkExperience()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ExperienceSaveData(List<PersonalWorkExperience> Works)
        {
            int userId = (int)System.Web.HttpContext.Current.Session["PersoanlID"];
            var userInfo = db.Users.AsNoTracking().Where(x => x.Id == (int)userId).ToList().FirstOrDefault();
            using (CVMAKER_DBEntities4 db = new CVMAKER_DBEntities4())
                {
                    //Check for NULL.
                    if (Works == null)
                    {
                    Works = new List<PersonalWorkExperience>();
                    }

                    //Loop and insert records.
                    foreach (PersonalWorkExperience work in Works)
                    {
                    work.PersonalId = userInfo.Id;
                    db.PersonalWorkExperiences.Add(work);
                    }
                    int insertedRecords = db.SaveChanges();

                     return Json(insertedRecords);
            }         

        }
        [HttpPost]
        public JsonResult EducationsSaveData(List<Education> Educations)
        {
           int userId = (int)System.Web.HttpContext.Current.Session["PersoanlID"];
            var userInfo = db.Users.AsNoTracking().Where(x => x.Id == (int)userId).ToList().FirstOrDefault();
            using (CVMAKER_DBEntities4 db = new CVMAKER_DBEntities4())
            {
                //Check for NULL.
                if (Educations == null)
                {
                    Educations = new List<Education>();
                }

                //Loop and insert records.
                foreach (Education Education in Educations)
                {
                    Education.PersonalId = userInfo.Id;
                    db.Educations.Add(Education);
                }
                int insertedRecords = db.SaveChanges();

                return Json(insertedRecords);
            }

        }
        [HttpPost]
        public JsonResult ServicesSaveData(List<PersonalService> Services)
        {
            int userId = (int)System.Web.HttpContext.Current.Session["PersoanlID"];
            var userInfo = db.Users.AsNoTracking().Where(x => x.Id == (int)userId).ToList().FirstOrDefault();
            using (CVMAKER_DBEntities4 db = new CVMAKER_DBEntities4())
            {
                //Check for NULL.
                if (Services == null)
                {
                    Services = new List<PersonalService>();
                }

                //Loop and insert records.
                foreach (PersonalService Service in Services)
                {
                    Service.PersonalId = userInfo.Id;
                    db.PersonalServices.Add(Service);
                }
                int insertedRecords = db.SaveChanges();

                return Json(insertedRecords);
            }

        }
        [HttpPost]
        public ActionResult PersonalWorkExperience(List<PersonalSkill> skills)
        {
          
                int userId = (int)System.Web.HttpContext.Current.Session["PersoanlID"];
            var userInfo = db.Users.AsNoTracking().Where(x => x.Id == (int)userId).ToList().FirstOrDefault();
            using (CVMAKER_DBEntities4 db = new CVMAKER_DBEntities4())
                {
                    //Check for NULL.
                    if (skills == null)
                    {
                        skills = new List<PersonalSkill>();
                    }

                    //Loop and insert records.
                    foreach (PersonalSkill skill in skills)
                    {
                        skill.PersonalId = userInfo.Id;
                    db.PersonalSkills.Add(skill);
                    }
                    int insertedRecords = db.SaveChanges();

                    return RedirectToAction("PersonalProject", "PersonalProject");
                }


        }
    }
}
