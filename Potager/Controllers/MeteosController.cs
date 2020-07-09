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
    public class MeteosController : Controller
    {
        private PotagerDBEntities db = new PotagerDBEntities();

        // GET: Meteos
        public ActionResult Index()
        {
            var meteo = db.Meteo.Include(m => m.Terrain);
            return View(meteo.ToList());
        }

        // GET: Meteos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meteo meteo = db.Meteo.Find(id);
            if (meteo == null)
            {
                return HttpNotFound();
            }
            return View(meteo);
        }

        // GET: Meteos/Create
        public ActionResult Create()
        {
            ViewBag.meteo_id = new SelectList(db.Terrain, "terrain_id", "ville");
            return View();
        }

        // POST: Meteos/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "meteo_id,potager_id,date,temperature,pluviometrie,vent,humidite,terrain_id")] Meteo meteo)
        {
            if (ModelState.IsValid)
            {
                db.Meteo.Add(meteo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.meteo_id = new SelectList(db.Terrain, "terrain_id", "ville", meteo.meteo_id);
            return View(meteo);
        }

        // GET: Meteos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meteo meteo = db.Meteo.Find(id);
            if (meteo == null)
            {
                return HttpNotFound();
            }
            ViewBag.meteo_id = new SelectList(db.Terrain, "terrain_id", "ville", meteo.meteo_id);
            return View(meteo);
        }

        // POST: Meteos/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "meteo_id,potager_id,date,temperature,pluviometrie,vent,humidite,terrain_id")] Meteo meteo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meteo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.meteo_id = new SelectList(db.Terrain, "terrain_id", "ville", meteo.meteo_id);
            return View(meteo);
        }

        // GET: Meteos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meteo meteo = db.Meteo.Find(id);
            if (meteo == null)
            {
                return HttpNotFound();
            }
            return View(meteo);
        }

        // POST: Meteos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meteo meteo = db.Meteo.Find(id);
            db.Meteo.Remove(meteo);
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
