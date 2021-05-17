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
        public ActionResult PersonalProject(List<string> ClientName, List<string> Category, List<string> Description, List<string> EndDate, List<string> ProjectName, List<string> Tools, List<string> DomainName, List<HttpPostedFileBase> InterfaceImage, List<HttpPostedFileBase> SubImage)
        {
            
          //  int userId = (int)System.Web.HttpContext.Current.Session["PersoanlID"];
            using (CVMAKER_DBEntities4 db = new CVMAKER_DBEntities4())
            {
               
                //Loop and insert records.
              //  foreach (PersonalProject project in projects)
              //  {
              //      project.PersonalId = userId;
              //      db.PersonalProjects.Add(project);
              //  }
                int insertedRecords = db.SaveChanges();

                return RedirectToAction("PersonalCV", "PersonalCV");
            }

        }
    }
}
