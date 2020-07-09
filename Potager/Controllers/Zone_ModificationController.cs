using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Potager.Models;

namespace Potager.Controllers
{
    public class Zone_ModificationController : Controller
    {
        private PotagerDBEntities db = new PotagerDBEntities();

        // GET: Zone_Modification
        public ActionResult Index()
        {
            var zone_Modification = db.Zone_Modification.Include(z => z.Zone);
            return View(zone_Modification.ToList());
        }

        // GET: Zone_Modification/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zone_Modification zone_Modification = db.Zone_Modification.Find(id);
            if (zone_Modification == null)
            {
                return HttpNotFound();
            }
            return View(zone_Modification);
        }

        // GET: Zone_Modification/Create
        public ActionResult Create()
        {
            ViewBag.zone_modification_id = new SelectList(db.Zone, "zone_id", "nom");
            return View();
        }

        // POST: Zone_Modification/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "zone_modification_id,zone_id,date,modification")] Zone_Modification zone_Modification)
        {
            if (ModelState.IsValid)
            {
                db.Zone_Modification.Add(zone_Modification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.zone_modification_id = new SelectList(db.Zone, "zone_id", "nom", zone_Modification.zone_modification_id);
            return View(zone_Modification);
        }

        // GET: Zone_Modification/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zone_Modification zone_Modification = db.Zone_Modification.Find(id);
            if (zone_Modification == null)
            {
                return HttpNotFound();
            }
            ViewBag.zone_modification_id = new SelectList(db.Zone, "zone_id", "nom", zone_Modification.zone_modification_id);
            return View(zone_Modification);
        }

        // POST: Zone_Modification/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "zone_modification_id,zone_id,date,modification")] Zone_Modification zone_Modification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zone_Modification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.zone_modification_id = new SelectList(db.Zone, "zone_id", "nom", zone_Modification.zone_modification_id);
            return View(zone_Modification);
        }

        // GET: Zone_Modification/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zone_Modification zone_Modification = db.Zone_Modification.Find(id);
            if (zone_Modification == null)
            {
                return HttpNotFound();
            }
            return View(zone_Modification);
        }

        // POST: Zone_Modification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zone_Modification zone_Modification = db.Zone_Modification.Find(id);
            db.Zone_Modification.Remove(zone_Modification);
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
