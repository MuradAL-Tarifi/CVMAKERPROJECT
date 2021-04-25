using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using MYCVMAKERPROJECT.Models;

namespace MYCVMAKER.Controllers
{
    public class AdminController : Controller
    {
        private CVMAKER_DBEntities db = new CVMAKER_DBEntities();    

        public ActionResult Admin()
        {
            return View();
        }
        public int PersonalCount()
        {
            return db.Users.Where(a => a.UserState == 3).ToList().Count();
        }
        public int CompanyCount()
        {
            return db.Users.Where(a => a.UserState == 2).ToList().Count();
        }
        public int JobAlertCount()
        {   
            return db.JobAlerts.Count();
        }
        public void AddCount(HitCounter HC)
        {
            DateTime today = DateTime.Now.Date;
                var v = db.HitCounters.Where(a => a.IPAddress.Equals(HC.IPAddress) && DbFunctions.TruncateTime(a.CreateDate) == today).FirstOrDefault();
            if (v == null)
          {
                db.HitCounters.Add(HC);
                db.SaveChanges();
           }
        }
        public object[] GetCount()
        {
            object[] o = new object[2];

            DateTime today = DateTime.Now.Date;
            //get Today views
            o[0] = db.HitCounters.Where(a => DbFunctions.TruncateTime(a.CreateDate) == today).Count();

            // get all views 
            o[1] = db.HitCounters.Count();

            return o;
        }

        protected void Page_Load(object sender, EventArgs a)
        {
            object[] o = new object[2];
            o = GetCount();
            ViewBag.ViewDaily = o[0].ToString();
            ViewBag.TotalView = o[1].ToString();
        }

        public ActionResult AddAdmin([Bind(Include = "UserName,UserEmail,UserState,Address,PhoneNumber")] User user)
        {

            return View();
        }


        public ActionResult GetAdminList()
        {
            var AdminList = db.Users.Where(a => a.UserState == 1).ToList();
            return Json(new { data = AdminList }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCompanyList()
        {
            var AdminList = db.Users.Where(a => a.UserState == 2).ToList();
            return Json(new { data = AdminList }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPersonalList()
        {
            var AdminList = db.Users.Where(a => a.UserState == 3).ToList();
            return Json(new { data = AdminList }, JsonRequestBehavior.AllowGet);
        }
    }
}
