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
            return PartialView( "_Delete",roomTypes);
        }

        // POST: Dashborad/RoomTypes/Delete/5
        [HttpPost]
        public JsonResult Delete(RoomTypes roomTypes)
        {
            RoomTypes DeleteroomTypes = db.roomTypes.Find(roomTypes.Id);
            JsonResult json = new JsonResult();
            bool Result = false;

            db.roomTypes.Remove(DeleteroomTypes);
            Result = db.SaveChanges() > 0;

            if (Result)
            {
                json.Data = new { Success = true };

            }
            else
            {
                json.Data = new { Success = false, Message = "刪除失敗" };
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
