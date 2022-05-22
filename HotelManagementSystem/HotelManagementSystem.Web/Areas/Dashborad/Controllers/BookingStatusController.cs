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
    public class BookingStatusController : Controller
    {
        private HotelManagementSystemContext db = new HotelManagementSystemContext();

        // GET: Dashborad/BookingStatus
        public ActionResult Index()
        {
            return View(db.bookingStatuses.ToList());
        }

        public ActionResult Action(int? Id)
        {
            BookingStatus bookingStatus = new BookingStatus();
            if(Id.HasValue)
            {
                var oldStatus = db.bookingStatuses.Find(Id);
                bookingStatus.Id = oldStatus.Id;
                bookingStatus.BookingStatusName = oldStatus.BookingStatusName;
            }

            return PartialView("_Action", bookingStatus);
        }

        [HttpPost]
        public JsonResult Action([Bind(Include = "Id,BookingStatusName")] BookingStatus bookingStatus )
        {
            JsonResult json = new JsonResult();
            bool Result = false;

            if(bookingStatus.Id > 0)
            {

                db.Entry(bookingStatus).State = EntityState.Modified;
                Result = db.SaveChanges() > 0;
            }
            else
            {
                db.bookingStatuses.Add(bookingStatus);
                Result = db.SaveChanges() > 0;
            }
          



            if (Result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false,Message = "上傳失敗" };
            }


            return json;
        }

        // GET: Dashborad/BookingStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingStatus bookingStatus = db.bookingStatuses.Find(id);
            if (bookingStatus == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", bookingStatus);
        }

        // POST: Dashborad/BookingStatus/Delete/5
        [HttpPost]
        public JsonResult Delete(int id)
        {
            JsonResult json = new JsonResult();
            bool Result = false;

            BookingStatus bookingStatus = db.bookingStatuses.Find(id);
            db.bookingStatuses.Remove(bookingStatus);
            Result = db.SaveChanges() > 0;


            if (Result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "刪除失敗!" };
            }

            return json;
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
