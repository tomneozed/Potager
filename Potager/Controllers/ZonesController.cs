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
    public class ZonesController : Controller
    {
        private PotagerDBEntities db = new PotagerDBEntities();

        // GET: Zones
        public ActionResult Index()
        {
            var zone = db.Zone.Include(z => z.Sujet).Include(z => z.Zone_Modification);
            return View(zone.ToList());
        }

        // GET: Zones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zone zone = db.Zone.Find(id);
            if (zone == null)
            {
                return HttpNotFound();
            }
            return View(zone);
        }

        // GET: Zones/Create
        public ActionResult Create()
        {
            ViewBag.zone_id = new SelectList(db.Sujet, "sujet_id", "maladie");
            ViewBag.zone_id = new SelectList(db.Zone_Modification, "zone_modification_id", "modification");
            return View();
        }

        // POST: Zones/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "zone_id,nom,date_creation,type,composition,exposition,paillage,emplacement,structures,observations")] Zone zone)
        {
            if (ModelState.IsValid)
            {
                db.Zone.Add(zone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.zone_id = new SelectList(db.Sujet, "sujet_id", "maladie", zone.zone_id);
            ViewBag.zone_id = new SelectList(db.Zone_Modification, "zone_modification_id", "modification", zone.zone_id);
            return View(zone);
        }

        // GET: Zones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zone zone = db.Zone.Find(id);
            if (zone == null)
            {
                return HttpNotFound();
            }
            ViewBag.zone_id = new SelectList(db.Sujet, "sujet_id", "maladie", zone.zone_id);
            ViewBag.zone_id = new SelectList(db.Zone_Modification, "zone_modification_id", "modification", zone.zone_id);
            return View(zone);
        }

        // POST: Zones/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "zone_id,nom,date_creation,type,composition,exposition,paillage,emplacement,structures,observations")] Zone zone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.zone_id = new SelectList(db.Sujet, "sujet_id", "maladie", zone.zone_id);
            ViewBag.zone_id = new SelectList(db.Zone_Modification, "zone_modification_id", "modification", zone.zone_id);
            return View(zone);
        }

        // GET: Zones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zone zone = db.Zone.Find(id);
            if (zone == null)
            {
                return HttpNotFound();
            }
            return View(zone);
        }

        // POST: Zones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zone zone = db.Zone.Find(id);
            db.Zone.Remove(zone);
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
