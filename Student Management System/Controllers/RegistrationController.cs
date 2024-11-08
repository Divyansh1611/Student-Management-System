using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Student_Management_System.Models;

namespace Student_Management_System.Controllers
{
    public class RegistrationController : Controller
    {
        private lindaaEntities db = new lindaaEntities();

        // GET: Registration
        public ActionResult Index()
        {
            var registation = db.registation.Include(r => r.batch).Include(r => r.course);
            return View(registation.ToList());
        }

        // GET: Registration/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registation registation = db.registation.Find(id);
            if (registation == null)
            {
                return HttpNotFound();
            }
            return View(registation);
        }

        // GET: Registration/Create
        public ActionResult Create()
        {
            ViewBag.batch_id = new SelectList(db.batch, "ID", "batch1");
            ViewBag.course_id = new SelectList(db.course, "Id", "course1");
            return View();
        }

        // POST: Registration/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,firstname,lastname,nic,course_id,batch_id,telno")] registation registation)
        {
            if (ModelState.IsValid)
            {
                db.registation.Add(registation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.batch_id = new SelectList(db.batch, "ID", "batch1", registation.batch_id);
            ViewBag.course_id = new SelectList(db.course, "Id", "course1", registation.course_id);
            return View(registation);
        }

        // GET: Registration/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registation registation = db.registation.Find(id);
            if (registation == null)
            {
                return HttpNotFound();
            }
            ViewBag.batch_id = new SelectList(db.batch, "ID", "batch1", registation.batch_id);
            ViewBag.course_id = new SelectList(db.course, "Id", "course1", registation.course_id);
            return View(registation);
        }

        // POST: Registration/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,firstname,lastname,nic,course_id,batch_id,telno")] registation registation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.batch_id = new SelectList(db.batch, "ID", "batch1", registation.batch_id);
            ViewBag.course_id = new SelectList(db.course, "Id", "course1", registation.course_id);
            return View(registation);
        }

        // GET: Registration/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registation registation = db.registation.Find(id);
            if (registation == null)
            {
                return HttpNotFound();
            }
            return View(registation);
        }

        // POST: Registration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            registation registation = db.registation.Find(id);
            db.registation.Remove(registation);
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
