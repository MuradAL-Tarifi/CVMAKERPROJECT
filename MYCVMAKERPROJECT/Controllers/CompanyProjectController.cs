using MYCVMAKERPROJECT.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        public JsonResult ProjectsSaveData(List<CompanyProject> projects)
        {
            // int userId = (int)System.Web.HttpContext.Current.Session["CompanyID"];
            using (CVMAKER_DBEntities db = new CVMAKER_DBEntities())
            {

                //Check for NULL.
                if (projects == null)
                {
                    projects = new List<CompanyProject>();
                }
                //var id = new List<int>();
                //Loop and insert records.
                foreach (CompanyProject project in projects)
                {
                    
                    //id.Add(project.Id);
                    project.CompanyId = 1;
                    db.CompanyProjects.Add(project);
                }
                //var myArray = id.ToArray();
                int insertedRecords = db.SaveChanges();
                return Json(true);
            }


        }
        [HttpPost]
        public JsonResult ImageUpload()
        {
            string path = Server.MapPath("~/img");
            HttpFileCollectionBase file2 = Request.Files;
            for (int i = 0; i < file2.Count; i++)
            {
                HttpPostedFileBase file1 = file2[i];
                file1.SaveAs(path + file1.FileName);
            }

            return Json(file2.Count + " Files Uploaded!");

        }

    }
}
