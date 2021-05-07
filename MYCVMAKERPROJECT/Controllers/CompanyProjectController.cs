using MYCVMAKERPROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MYCVMAKER.Controllers
{
    public class CompanyProjectController : Controller
    {
        public ActionResult CompanyProject()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProjectsSaveData(List<CompanyProject> projects)
        {
            int userId = (int)System.Web.HttpContext.Current.Session["CompanyID"];
            using (CVMAKER_DBEntities db = new CVMAKER_DBEntities())
            {
                //Check for NULL.
                if (projects == null)
                {
                    projects = new List<CompanyProject>();
                }
                
                //Loop and insert records.
                foreach (CompanyProject project in projects)
                {
                    //for (int i = 0; i < project.SubImage.Length; i++)
                    //{
                    //    //HttpPostedFileBase imgpath = project.SubImage[i];
                    //    //string img = "/img/portfolio/large/project-" + i + "/"+project.SubImage[i];
                    //    //HttpPostedFile.SaveAs(project.SubImage[i]);
                    //    //project.SubImage[i].
                    //}


                    project.CompanyId = userId;
                    db.CompanyProjects.Add(project);
                }
                int insertedRecords = db.SaveChanges();

                return RedirectToAction("PersonalCV", "PersonalCV");
            }

        }
    }
}
