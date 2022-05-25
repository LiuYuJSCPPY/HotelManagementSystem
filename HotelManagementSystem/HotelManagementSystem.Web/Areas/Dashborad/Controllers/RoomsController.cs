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
using System.IO;

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


        public ActionResult Action()
        {
            RoomViewModel RoomsModel =new RoomViewModel();
            RoomsModel.bookingStatus = db.bookingStatuses.Select(booking => new SelectListItem { Text = booking.BookingStatusName, Value = booking.Id.ToString()}).ToList();

            RoomsModel.roomTypes = db.roomTypes.Select(booking => new SelectListItem { Text = booking.RoomType, Value = booking.Id.ToString() }).ToList();
            return PartialView("_Action", RoomsModel);
        }

        [HttpPost]
        public JsonResult Action([Bind(Include = "RoomNumber,RoomPrice,BookingStatusId,RoomTypesId,RoomCapacity,RoomDescription,IsActive,RoomViewImage")]Rooms rooms,HttpPostedFileBase RoomViewImage)
        {
            JsonResult json = new JsonResult();
            bool Result = false;

          


            string FilePath = Server.MapPath("~/Areas/Dashborad/Image/RoomImage/");

            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            string FileName = Path.GetFileName(RoomViewImage.FileName);
            string _FileName = DateTime.Now.ToString("yyyymmssfff") + FileName;
            string Exesption = Path.GetExtension(RoomViewImage.FileName);
            string path = Path.Combine(FilePath, _FileName);

            rooms.RoomImage = "~/Areas/Dashborad/Image/RoomImage/" + _FileName;

            if(Exesption.ToLower() == ".jpg" || Exesption.ToLower() == ".jepg" || Exesption.ToLower() == ".png")
            {
                RoomViewImage.SaveAs(path);
                db.rooms.Add(rooms);
                Result = db.SaveChanges() > 0;
            }




            if (Result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false,Message = "失敗!" };
            }


            return json;

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
