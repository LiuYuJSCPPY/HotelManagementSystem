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
    public class RoomsController : Controller
    {
        private HotelManagementSystemContext db = new HotelManagementSystemContext();

        // GET: Dashborad/Rooms
        public ActionResult Index()
        {
            var rooms = db.rooms.Include(r => r.bookingStatus).Include(r => r.roomTypes);
            return View(rooms.ToList());
        }

        // GET: Dashborad/Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rooms rooms = db.rooms.Find(id);
            if (rooms == null)
            {
                return HttpNotFound();
            }
            return View(rooms);
        }

        // GET: Dashborad/Rooms/Create
        public ActionResult Create()
        {
            ViewBag.BookingStatusId = new SelectList(db.bookingStatuses, "Id", "BookingStatusName");
            ViewBag.RoomTypesId = new SelectList(db.roomTypes, "Id", "RoomType");
            return View();
        }

        // POST: Dashborad/Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RoomNumber,RoomImage,RoomPrice,BookingStatusId,RoomTypesId,RoomCapacity,RoomDescription,IsActive")] Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                db.rooms.Add(rooms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookingStatusId = new SelectList(db.bookingStatuses, "Id", "BookingStatusName", rooms.BookingStatusId);
            ViewBag.RoomTypesId = new SelectList(db.roomTypes, "Id", "RoomType", rooms.RoomTypesId);
            return View(rooms);
        }

        // GET: Dashborad/Rooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rooms rooms = db.rooms.Find(id);
            if (rooms == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookingStatusId = new SelectList(db.bookingStatuses, "Id", "BookingStatusName", rooms.BookingStatusId);
            ViewBag.RoomTypesId = new SelectList(db.roomTypes, "Id", "RoomType", rooms.RoomTypesId);
            return View(rooms);
        }

        // POST: Dashborad/Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RoomNumber,RoomImage,RoomPrice,BookingStatusId,RoomTypesId,RoomCapacity,RoomDescription,IsActive")] Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rooms).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookingStatusId = new SelectList(db.bookingStatuses, "Id", "BookingStatusName", rooms.BookingStatusId);
            ViewBag.RoomTypesId = new SelectList(db.roomTypes, "Id", "RoomType", rooms.RoomTypesId);
            return View(rooms);
        }

        // GET: Dashborad/Rooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rooms rooms = db.rooms.Find(id);
            if (rooms == null)
            {
                return HttpNotFound();
            }
            return View(rooms);
        }

        // POST: Dashborad/Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rooms rooms = db.rooms.Find(id);
            db.rooms.Remove(rooms);
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
