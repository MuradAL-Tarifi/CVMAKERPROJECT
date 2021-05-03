using MYCVMAKERPROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MYCVMAKER.Controllers
{
    public class PersonalProjectController : Controller
    {
        public ActionResult PersonalProject()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProjectsSaveData(List<PersonalProject> projects)
        {
            int userId = (int)System.Web.HttpContext.Current.Session["PersoanlID"];
            using (CVMAKER_DBEntities db = new CVMAKER_DBEntities())
            {
                //Check for NULL.
                if (projects == null)
                {
                    projects = new List<PersonalProject>();
                }

                //Loop and insert records.
                foreach (PersonalProject project in projects)
                {
                    project.PersonalId = userId;
                    db.PersonalProjects.Add(project);
                }
                int insertedRecords = db.SaveChanges();

                return RedirectToAction("PersonalCV", "PersonalCV");
            }

        }
    }
}
