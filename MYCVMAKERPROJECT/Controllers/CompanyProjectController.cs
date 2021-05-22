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
        public ActionResult CompanyProject(string ClientName, string Category, string Description, string EndDate, string ProjectName, string Tools, string DomainName, HttpPostedFileBase InterfaceImage, List<HttpPostedFileBase> SubImage)
        {

            int userId = (int)System.Web.HttpContext.Current.Session["CompanyID"];

            using (CVMAKER_DBEntities4 db = new CVMAKER_DBEntities4())
            {
                var userInfo = db.Companies.AsNoTracking().Where(x => x.UsersId == (int)userId).ToList().FirstOrDefault();

                CompanyProject pp = new CompanyProject();
                pp.CompanyId = userInfo.Id;
                pp.ClientName = ClientName;
                pp.Category = Category;
                pp.Description = Description;
                pp.EndDate = EndDate;
                pp.ProjectName = ProjectName;
                pp.Tools = Tools;
                pp.DomainName = DomainName;
                string InImg = "";
                if (InterfaceImage != null)
                {
                    InImg = "/img/portfolio/thumb/" + InterfaceImage.FileName;
                    InterfaceImage.SaveAs(Server.MapPath(InImg));//save file to folder
                }
                pp.InterfaceImage = InImg;
                string SpImg = "";
                string fullpath = "";
                foreach (HttpPostedFileBase photo in SubImage)
                {
                    if (photo != null)
                    {
                        SpImg = "/img/portfolio/large/" + photo.FileName;
                        photo.SaveAs(Server.MapPath(SpImg));//save file to folder
                    }
                    fullpath = SpImg + "," + fullpath;
                }
                pp.SubImage = fullpath;

                db.CompanyProjects.Add(pp);
                db.SaveChanges();

                return View();
            }

        }

    }
}
