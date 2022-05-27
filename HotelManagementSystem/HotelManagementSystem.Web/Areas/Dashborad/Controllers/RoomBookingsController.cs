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
using HotelManagementSystem.Web.Areas.Dashborad.ViewModel;

namespace HotelManagementSystem.Web.Areas.Dashborad.Controllers
{
    public class RoomBookingsController : Controller
    {
        private HotelManagementSystemContext db = new HotelManagementSystemContext();

        // GET: Dashborad/RoomBookings
        public ActionResult Index()
        {
            var roomBookings = db.roomBookings.Include(r => r.rooms);
            return View(roomBookings.ToList());
        }



        public ActionResult Action()
        {
            RoomBookings roombookings = new RoomBookings();
            return PartialView("_Action", roombookings);
        }


        // GET: Dashborad/RoomBookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomBookings roomBookings = db.roomBookings.Find(id);
            if (roomBookings == null)
            {
                return HttpNotFound();
            }
            return View(roomBookings);
        }

        // GET: Dashborad/RoomBookings/Create
        public ActionResult Create()
        {
            ViewBag.RoomsId = new SelectList(db.rooms, "Id", "RoomNumber");
            return View();
        }

        // POST: Dashborad/RoomBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CutomerName,CutomerAddress,BookingFrom,BookingTo,RoomsId,NoOfMeMbers,TotalAmount")] RoomBookings roomBookings)
        {
            if (ModelState.IsValid)
            {
                db.roomBookings.Add(roomBookings);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoomsId = new SelectList(db.rooms, "Id", "RoomNumber", roomBookings.RoomsId);
            return View(roomBookings);
        }

        // GET: Dashborad/RoomBookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomBookings roomBookings = db.roomBookings.Find(id);
            if (roomBookings == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomsId = new SelectList(db.rooms, "Id", "RoomNumber", roomBookings.RoomsId);
            return View(roomBookings);
        }

        // POST: Dashborad/RoomBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CutomerName,CutomerAddress,BookingFrom,BookingTo,RoomsId,NoOfMeMbers,TotalAmount")] RoomBookings roomBookings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomBookings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoomsId = new SelectList(db.rooms, "Id", "RoomNumber", roomBookings.RoomsId);
            return View(roomBookings);
        }

        // GET: Dashborad/RoomBookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomBookings roomBookings = db.roomBookings.Find(id);
            if (roomBookings == null)
            {
                return HttpNotFound();
            }
            return View(roomBookings);
        }

        // POST: Dashborad/RoomBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomBookings roomBookings = db.roomBookings.Find(id);
            db.roomBookings.Remove(roomBookings);
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
