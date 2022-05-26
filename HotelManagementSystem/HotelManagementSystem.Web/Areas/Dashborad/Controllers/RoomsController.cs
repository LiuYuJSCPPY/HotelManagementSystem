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


        public ActionResult Action(int? Id)
        {
            RoomViewModel RoomsModel =new RoomViewModel();
            RoomsModel.bookingStatus = db.bookingStatuses.Select(booking => new SelectListItem { Text = booking.BookingStatusName, Value = booking.Id.ToString()}).ToList();

            RoomsModel.roomTypes = db.roomTypes.Select(booking => new SelectListItem { Text = booking.RoomType, Value = booking.Id.ToString() }).ToList();


            if (Id.HasValue)
            {
                var OldRoom = db.rooms.Find(Id);
                RoomsModel.Id = Id.Value;
                RoomsModel.RoomNumber = OldRoom.RoomNumber;
                RoomsModel.RoomPrice = OldRoom.RoomPrice;
                RoomsModel.RoomImage = OldRoom.RoomImage;
                RoomsModel.RoomDescription = OldRoom.RoomDescription;
                RoomsModel.BookingStatusId = OldRoom.BookingStatusId;
                RoomsModel.RoomTypesId = OldRoom.RoomTypesId;
                RoomsModel.IsActive = OldRoom.IsActive;
                RoomsModel.RoomCapacity = OldRoom.RoomCapacity;
            }

            return PartialView("_Action", RoomsModel);
        }

        [HttpPost]
        public JsonResult Action([Bind(Include = "Id,RoomNumber,RoomPrice,BookingStatusId,RoomTypesId,RoomCapacity,RoomDescription,IsActive,RoomViewImage")]Rooms rooms,HttpPostedFileBase RoomViewImage,int? Id)
        {
            JsonResult json = new JsonResult();
            bool Result = false;

            if (Id > 0)
            {
                var EditRoom = db.rooms.Find(Id);
                string OldRoomImage =  Request.MapPath( db.rooms.Find(rooms.Id).RoomImage.ToString());
                string FilePath = Server.MapPath("~/Areas/Dashborad/Image/RoomImage/");
                string FileName = Path.GetFileName(RoomViewImage.FileName);
                string _FileName = DateTime.Now.ToString("yyyymmssfff")+FileName;
                string Exesption = Path.GetExtension(RoomViewImage.FileName);
                string path = Path.Combine(FilePath, _FileName);

                

                if (Exesption.ToLower() == ".jpg" || Exesption.ToLower() == ".jepg" || Exesption.ToLower() == ".png")
                {
                    if (System.IO.File.Exists(OldRoomImage))
                    {
                        System.IO.File.Delete(OldRoomImage);
                        rooms.RoomImage = "~/Areas/Dashborad/Image/RoomImage/" + _FileName;
                        RoomViewImage.SaveAs(path);
                        if (ModelState.IsValid)
                        {
                            db.Entry(EditRoom).CurrentValues.SetValues(rooms);
                            db.Entry(EditRoom).State = EntityState.Modified;
                            Result = db.SaveChanges() > 0;
                        }
                    }
                          
                }

            }
            else
            {
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
            return PartialView("_Delete",rooms);
        }

        // POST: Dashborad/Rooms/Delete/5
        [HttpPost]
        public JsonResult Delete(int id)
        {
            JsonResult json = new JsonResult();
            bool Result = false;

            Rooms rooms = db.rooms.Find(id);



            if (System.IO.File.Exists(rooms.RoomImage))
            {
                System.IO.File.Delete(rooms.RoomImage);
                db.rooms.Remove(rooms);
                Result = db.SaveChanges() > 0;
            }
            else
            {
                db.rooms.Remove(rooms);
                Result = db.SaveChanges() > 0;
            }



            if (Result)
            {
                json.Data = new {Success = true};
            }
            else
            {
                json.Data = new { Success = false,Message ="刪除失敗!"};
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
