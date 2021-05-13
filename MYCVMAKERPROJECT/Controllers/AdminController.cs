using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using MYCVMAKERPROJECT.Models;
using System.Net;

namespace MYCVMAKER.Controllers
{
    public class AdminController : Controller
    {
        private CVMAKER_DBEntities4 db = new CVMAKER_DBEntities4();    

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


        public ActionResult GetAdminList()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var AdminList1 = db.Users.Where(a => a.UserState == 1).ToList();
            return Json(new { data = AdminList1 }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCompanyList()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var AdminList2 = db.Users.Where(a => a.UserState == 2).ToList();
            return Json(new { data = AdminList2 }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPersonalList()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var AdminList3 = db.Users.Where(a => a.UserState == 3).ToList();
            return Json(new { data = AdminList3 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,UserPassword,UserEmail,UserState,Description,InstagramLink,GetHupLink,LinkedInLink,FacebookLink,Address,PhoneNumber")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Admin");
            }

            return View("Admin");
        }

        // GET: Users/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,UserPassword,UserEmail,UserState,Description,InstagramLink,GetHupLink,LinkedInLink,FacebookLink,Address,PhoneNumber")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int? DeleteById)
        {
            User user = db.Users.Find(DeleteById);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Admin","Admin");
        }
    }
}
