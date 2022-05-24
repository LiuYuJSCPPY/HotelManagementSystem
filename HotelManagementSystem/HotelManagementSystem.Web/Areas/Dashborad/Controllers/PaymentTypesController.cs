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
    public class PaymentTypesController : Controller
    {
        private HotelManagementSystemContext db = new HotelManagementSystemContext();

        // GET: Dashborad/PaymentTypes
        public ActionResult Index()
        {
            return View(db.paymentTypes.ToList());
        }



        public ActionResult Action(int? Id)
        {
            PaymentType paymentType = new PaymentType();

            if (Id.HasValue)
            {
                PaymentType EditPayMentType = db.paymentTypes.Find(Id);
                paymentType.Id = Id.Value;
                paymentType.PaymentTypeName = EditPayMentType.PaymentTypeName;
            }

            return PartialView("_Action", paymentType);
        }


        [HttpPost]
        public JsonResult Action([Bind(Include = "Id,PaymentTypeName")] PaymentType paymentType)
        {
            JsonResult json = new JsonResult();
            bool Result = false;

            if (paymentType.Id > 0)
            {
                db.Entry(paymentType).State = EntityState.Modified;
                Result = db.SaveChanges() > 0;
            }
            else
            {
                db.paymentTypes.Add(paymentType);
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
       
        // GET: Dashborad/PaymentTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentType paymentType = db.paymentTypes.Find(id);
            if (paymentType == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", paymentType);
        }

        // POST: Dashborad/PaymentTypes/Delete/5
        [HttpPost]
       
        public JsonResult Delete(int id)
        {
            JsonResult json = new JsonResult();
            bool Result = false;
            
            PaymentType paymentType = db.paymentTypes.Find(id);
            db.paymentTypes.Remove(paymentType);
            Result = db.SaveChanges()> 0;


            if (Result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "失敗!" };
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
