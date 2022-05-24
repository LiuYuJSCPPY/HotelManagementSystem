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
        // GET: Dashborad/PaymentTypes/Details/5
        public ActionResult Details(int? id)
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
            return View(paymentType);
        }

        // GET: Dashborad/PaymentTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dashborad/PaymentTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PaymentTypeName")] PaymentType paymentType)
        {
            if (ModelState.IsValid)
            {
                db.paymentTypes.Add(paymentType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paymentType);
        }

        // GET: Dashborad/PaymentTypes/Edit/5
        public ActionResult Edit(int? id)
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
            return View(paymentType);
        }

        // POST: Dashborad/PaymentTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PaymentTypeName")] PaymentType paymentType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paymentType);
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
            return View(paymentType);
        }

        // POST: Dashborad/PaymentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentType paymentType = db.paymentTypes.Find(id);
            db.paymentTypes.Remove(paymentType);
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
