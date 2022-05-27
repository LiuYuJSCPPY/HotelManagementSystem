using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelManagementSystem.Data;
using HotelManagementSystem.Model;

namespace HotelManagementSystem.Web.Areas.Dashborad.Controllers
{
    public class AdminUsersController : Controller
    {
        private HotelManagementSystemContext db = new HotelManagementSystemContext();

        // GET: Dashborad/AdminUsers
        public ActionResult Index()
        {
            return View(db.AdminUsers.ToList());
        }

        // GET: Dashborad/AdminUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminUser adminUser = db.AdminUsers.Find(id);
            if (adminUser == null)
            {
                return HttpNotFound();
            }
            return View(adminUser);
        }

        // GET: Dashborad/AdminUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dashborad/AdminUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Address,Telephone,UserImagePath,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] AdminUser adminUser)
        {
            if (ModelState.IsValid)
            {
                db.AdminUsers.Add(adminUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adminUser);
        }

        // GET: Dashborad/AdminUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminUser adminUser = db.AdminUsers.Find(id);
            if (adminUser == null)
            {
                return HttpNotFound();
            }
            return View(adminUser);
        }

        // POST: Dashborad/AdminUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Address,Telephone,UserImagePath,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] AdminUser adminUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminUser);
        }

        // GET: Dashborad/AdminUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminUser adminUser = db.AdminUsers.Find(id);
            if (adminUser == null)
            {
                return HttpNotFound();
            }
            return View(adminUser);
        }

        // POST: Dashborad/AdminUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AdminUser adminUser = db.AdminUsers.Find(id);
            db.AdminUsers.Remove(adminUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
