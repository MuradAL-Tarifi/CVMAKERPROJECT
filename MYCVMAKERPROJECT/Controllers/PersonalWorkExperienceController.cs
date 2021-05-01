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
        

        public ActionResult PersonalWorkExperience()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PersonalWorkExperience(List<PersonalSkill> skills)
        {
            try
            {
                using (CVMAKER_DBEntities db = new CVMAKER_DBEntities())
                {
                    //Check for NULL.
                    if (skills == null)
                    {
                        skills = new List<PersonalSkill>();
                    }

                    //Loop and insert records.
                    foreach (PersonalSkill skill in skills)
                    {
                        db.PersonalSkills.Add(skill);
                    }
                    int insertedRecords = db.SaveChanges();

                    return RedirectToAction("PersonalProject", "PersonalProject");
                }
            }
            catch (Exception)
            {

                return Json(false);
            }


        }
    }
}
