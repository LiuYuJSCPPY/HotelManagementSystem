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
    public class RoomTypesController : Controller
    {
        private HotelManagementSystemContext db = new HotelManagementSystemContext();

        // GET: Dashborad/RoomTypes
        public ActionResult Index()
        {
            return View(db.roomTypes.ToList());
        }

        // GET: Dashborad/RoomTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomTypes roomTypes = db.roomTypes.Find(id);
            if (roomTypes == null)
            {
                return HttpNotFound();
            }
            return View(roomTypes);
        }

        public ActionResult Action(int? Id)
        {
            RoomTypes roomTypes = new RoomTypes();
            if (Id.HasValue)
            {
                var OldRoomType = db.roomTypes.Find(Id);
                roomTypes.Id = OldRoomType.Id;
                roomTypes.RoomType = OldRoomType.RoomType;
            }


            return PartialView("_Action", roomTypes);
        }



        [HttpPost]
        public JsonResult Action([Bind(Include ="Id,RoomType")] RoomTypes roomTypes )
        {
            JsonResult json = new JsonResult();
            bool Result = false;

            if(roomTypes.Id > 0)
            {
                RoomTypes room = new RoomTypes();
                room.Id = roomTypes.Id;
                room.RoomType = roomTypes.RoomType;
                db.Entry(room).State = EntityState.Modified;
                Result = db.SaveChanges() > 0;
            }
            else
            {
                db.roomTypes.Add(roomTypes);
                Result = db.SaveChanges() > 0;
               
            }




            if (Result)
            {
                json.Data = new { Success = true };

            }
            else
            {
                json.Data = new { Success = false, Message = "上傳時失敗" };
            }

            return json;
        }

        // GET: Dashborad/RoomTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dashborad/RoomTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RoomType")] RoomTypes roomTypes)
        {
            if (ModelState.IsValid)
            {
                db.roomTypes.Add(roomTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roomTypes);
        }

        // GET: Dashborad/RoomTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomTypes roomTypes = db.roomTypes.Find(id);
            if (roomTypes == null)
            {
                return HttpNotFound();
            }
            return View(roomTypes);
        }

        // POST: Dashborad/RoomTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RoomType")] RoomTypes roomTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roomTypes);
        }

        // GET: Dashborad/RoomTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomTypes roomTypes = db.roomTypes.Find(id);
            if (roomTypes == null)
            {
                return HttpNotFound();
            }
            return View(roomTypes);
        }

        // POST: Dashborad/RoomTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomTypes roomTypes = db.roomTypes.Find(id);
            db.roomTypes.Remove(roomTypes);
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
